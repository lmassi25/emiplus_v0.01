using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;

namespace Emiplus.View.Food
{
    public partial class AdicionaisDispon : Form
    {
        private Item _modelItem = new Item();
        private PedidoItem _modelPedidoItem = new PedidoItem();

        public AdicionaisDispon()
        {
            InitializeComponent();
            Eventos();
        }

        public static int IdItem { get; set; }
        public static int IdPedidoItem { get; set; }
        public static string AddonSelected { get; set; }
        public static double ValorAddon { get; set; }

        private void SetHeadersAdicionais(DataGridView table)
        {
            table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Selecione",
                Name = "Selecione",
                FlatStyle = FlatStyle.Standard,
                CellTemplate = new DataGridViewCheckBoxCell(),
                Width = 60
            };
            table.Columns.Insert(0, checkColumn);

            table.Columns[1].Name = "ID";
            table.Columns[1].Visible = false;

            table.Columns[2].Name = "Adicional";
            table.Columns[2].Width = 120;
            table.Columns[2].MinimumWidth = 120;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;
        }

        private void SetContentTableAdicionais(DataGridView table)
        {
            table.Rows.Clear();

            if (!string.IsNullOrEmpty(_modelItem.Adicional))
            {
                var addons = _modelItem.Adicional.Split(',');
                foreach (var id in addons)
                {
                    var data = new ItemAdicional().FindAll().Where("id", id).WhereFalse("excluir")
                        .FirstOrDefault<ItemAdicional>();
                    if (data.Count() > 0)
                        table.Rows.Add(
                            false,
                            data.Id,
                            data.Title,
                            Validation.FormatPrice(Validation.ConvertToDouble(data.Valor), true)
                        );
                }
            }

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadAddonsSelected(string adicional)
        {
            foreach (DataGridViewRow item in GridLista.Rows)
                if (!string.IsNullOrEmpty(adicional))
                {
                    var addons = adicional.Split(',');
                    foreach (var id in addons)
                        if (Validation.ConvertToInt32(item.Cells["ID"].Value) == Validation.ConvertToInt32(id))
                            item.Cells["Selecione"].Value = true;
                }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    Support.UpDownDataGrid(true, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }


        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Shown += (s, e) =>
            {
                Refresh();

                if (IdItem > 0)
                    _modelItem = _modelItem.FindById(IdItem).FirstOrDefault<Item>();

                if (IdPedidoItem > 0)
                    _modelPedidoItem = _modelPedidoItem.FindById(IdPedidoItem).FirstOrDefault<PedidoItem>();

                SetHeadersAdicionais(GridLista);
                SetContentTableAdicionais(GridLista);

                if (IdPedidoItem > 0)
                    LoadAddonsSelected(_modelPedidoItem.Adicional);

                if (!string.IsNullOrEmpty(AddonSelected))
                    LoadAddonsSelected(AddonSelected);
            };

            btnSalvar.Click += (s, e) =>
            {
                var addon = new StringBuilder();
                double sumValor = 0;
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                    {
                        sumValor += Validation.ConvertToDouble(item.Cells["Valor"].Value);

                        if (string.IsNullOrEmpty(addon.ToString()))
                        {
                            addon.Append(Validation.ConvertToInt32(item.Cells["ID"].Value).ToString());
                            continue;
                        }

                        addon.Append($",{Validation.ConvertToInt32(item.Cells["ID"].Value)}");
                    }

                ValorAddon = sumValor;
                AddonSelected = addon.ToString();

                DialogResult = DialogResult.OK;
                Close();
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    GridLista.SelectedRows[0].Cells["Selecione"].Value = (bool) GridLista.SelectedRows[0].Cells["Selecione"].Value == false;
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}
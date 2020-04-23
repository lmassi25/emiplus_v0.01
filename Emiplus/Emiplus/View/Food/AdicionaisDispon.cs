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
        public static int IdItem { get; set; }
        public static int IdPedidoItem { get; set; }
        public static string AddonSelected { get; set; }
        public static double ValorAddon { get; set; }

        private Model.Item _modelItem = new Item();
        private Model.PedidoItem _modelPedidoItem = new PedidoItem();

        public AdicionaisDispon()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersAdicionais(DataGridView Table)
        {
            Table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Selecione";
            checkColumn.Name = "Selecione";
            checkColumn.FlatStyle = FlatStyle.Standard;
            checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
            checkColumn.Width = 60;
            Table.Columns.Insert(0, checkColumn);

            Table.Columns[1].Name = "ID";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Adicional";
            Table.Columns[2].Width = 120;
            Table.Columns[2].MinimumWidth = 120;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Width = 100;
            Table.Columns[3].Visible = true;
        }

        private void SetContentTableAdicionais(DataGridView table)
        {
            table.Rows.Clear();

            if (!string.IsNullOrEmpty(_modelItem.Adicional))
            {
                string[] addons = _modelItem.Adicional.Split(',');
                foreach (string id in addons)
                {
                    ItemAdicional data = new ItemAdicional().FindAll().Where("id", id).WhereFalse("excluir")
                        .FirstOrDefault<ItemAdicional>();
                    if (data.Count() > 0)
                    {
                        table.Rows.Add(
                            false,
                            data.Id,
                            data.Title,
                            Validation.FormatPrice(Validation.ConvertToDouble(data.Valor), true)
                        );
                    }
                }
            }

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadAddonsSelected(string adicional)
        {
            foreach (DataGridViewRow item in GridLista.Rows)
            {
                if (!string.IsNullOrEmpty(adicional))
                {
                    string[] addons = adicional.Split(',');
                    foreach (string id in addons)
                    {
                        if (Validation.ConvertToInt32(item.Cells["ID"].Value) == Validation.ConvertToInt32(id))
                        {
                            item.Cells["Selecione"].Value = true;
                        }
                    }
                }
            }
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                Refresh();
                
                if (IdItem > 0)
                    _modelItem = _modelItem.FindById(IdItem).FirstOrDefault<Item>();

                if (IdPedidoItem > 0)
                    _modelPedidoItem = _modelPedidoItem.FindById(IdPedidoItem).FirstOrDefault<Model.PedidoItem>();

                SetHeadersAdicionais(GridLista);
                SetContentTableAdicionais(GridLista);

                if (IdPedidoItem > 0)
                    LoadAddonsSelected(_modelPedidoItem.Adicional);

                if (!string.IsNullOrEmpty(AddonSelected))
                    LoadAddonsSelected(AddonSelected);
            };

            btnSalvar.Click += (s, e) =>
            {
                StringBuilder Addon = new StringBuilder();
                double SumValor = 0;
                foreach (DataGridViewRow item in GridLista.Rows)
                {
                    if ((bool)item.Cells["Selecione"].Value)
                    {
                        SumValor += Validation.ConvertToDouble(item.Cells["Valor"].Value);

                        if (string.IsNullOrEmpty(Addon.ToString()))
                        {
                            Addon.Append(Validation.ConvertToInt32(item.Cells["ID"].Value).ToString());
                            continue;
                        }

                        Addon.Append($",{Validation.ConvertToInt32(item.Cells["ID"].Value)}");
                    }
                }

                ValorAddon = SumValor;
                AddonSelected = Addon.ToString();

                DialogResult = DialogResult.OK;
                Close();
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;
                    }
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}

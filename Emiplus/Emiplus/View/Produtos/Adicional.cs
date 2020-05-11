using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class Adicional : Form
    {
        public List<int> ListAdicionais = new List<int>();

        public Adicional()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

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

        private async Task SetContentTableAsync(DataGridView table)
        {
            table.Rows.Clear();

            var adicionais = new ItemAdicional().FindAll().WhereFalse("excluir");

            if (!string.IsNullOrEmpty(search.Text))
                adicionais.Where("title", "like", $"%{search.Text}%");

            var data = adicionais.Get<ItemAdicional>();
            if (data.Any())
                foreach (var item in data)
                    table.Rows.Add(
                        false,
                        item.Id,
                        item.Title,
                        Validation.FormatPrice(Validation.ConvertToDouble(item.Valor), true)
                    );

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void EditProduct(bool create = false)
        {
            if (EditAllProducts.FormOpen)
                return;

            if (create)
            {
                AddAdicional.Id = 0;
                OpenForm.Show<AddAdicional>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                AddAdicional.Id = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddAdicional>(this);
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

                case Keys.Enter:
                    EditProduct();
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
            Masks.SetToUpper(this);

            Load += (s, e) => search.Focus();

            Shown += async (s, e) =>
            {
                Refresh();

                SetHeadersTable(GridLista);
                await SetContentTableAsync(GridLista);
            };

            btnAdicionar.Click += (s, e) => EditProduct(true);
            btnEditar.Click += (s, e) => EditProduct();

            btnRemover.Click += (s, e) =>
            {
                ListAdicionais.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        ListAdicionais.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a deletar os ADICIONAIS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in ListAdicionais)
                        new ItemAdicional().Remove(item);

                    SetContentTableAsync(GridLista);
                }

                btnRemover.Visible = false;
                btnEditar.Visible = true;
                btnAdicionar.Visible = true;
            };

            search.TextChanged += (s, e) => SetContentTableAsync(GridLista);

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
            btnExit.Click += (s, e) => Close();

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool) GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditar.Visible = false;
                        btnAdicionar.Visible = false;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        var hideBtns = false;
                        var hideBtnsTop = true;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool) item.Cells["Selecione"].Value)
                            {
                                hideBtns = true;
                                hideBtnsTop = false;
                            }

                        btnRemover.Visible = hideBtns;
                        btnEditar.Visible = hideBtnsTop;
                        btnAdicionar.Visible = hideBtnsTop;
                    }
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
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class Adicional : Form
    {
        public List<int> listAdicionais = new List<int>();

        public Adicional()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
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

        private async Task SetContentTableAsync(DataGridView Table)
        {
            Table.Rows.Clear();

            var adicionais = new Model.ItemAdicional().FindAll().WhereFalse("excluir");
            
            if (!string.IsNullOrEmpty(search.Text))
                adicionais.Where("title", "like", $"%{search.Text}%");

            IEnumerable<Model.ItemAdicional> data = adicionais.Get<Model.ItemAdicional>();
            if (data.Count() > 0) {
                foreach (Model.ItemAdicional item in data)
                {
                    Table.Rows.Add(
                        false,
                        item.Id,
                        item.Title,
                        Validation.FormatPrice(Validation.ConvertToDouble(item.Valor), true)
                    );
                }
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                listAdicionais.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool)item.Cells["Selecione"].Value == true)
                        listAdicionais.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));
                
                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar os ADICIONAIS selecionados, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in listAdicionais)
                        new Model.ItemAdicional().Remove(item);
                    
                    SetContentTableAsync(GridLista);
                }

                btnRemover.Visible = false;
                btnEditar.Visible = true;
                btnAdicionar.Visible = true;
            };
            
            search.TextChanged += (s, e) => SetContentTableAsync(GridLista);

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
            btnExit.Click += (s, e) => Close();

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditar.Visible = false;
                        btnAdicionar.Visible = false;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        bool hideBtns = false;
                        bool hideBtnsTop = true;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool)item.Cells["Selecione"].Value == true)
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

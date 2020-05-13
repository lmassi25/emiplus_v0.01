using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotLiquid;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Reports;
using SqlKata.Execution;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Produtos : Form
    {
        private readonly Item _controller = new Item();

        private IEnumerable<dynamic> dataTable;

        public List<int> ListProdutos = new List<int>();

        private readonly Timer timer = new Timer(Configs.TimeLoading);
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public Produtos()
        {
            InitializeComponent();
            Eventos();

            ToolHelp.Show("Pesquise pelo produto utilizando a Descrição, Cód. de Barras ou Categoria do Produto.",
                pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        public static int IdPdtSelecionado { get; set; }

        private void DataTableStart()
        {
            GridListaProdutos.Visible = false;
            Loading.Size = new Size(GridListaProdutos.Width, GridListaProdutos.Height);
            Loading.Visible = true;
            workerBackground.RunWorkerAsync();
        }

        private async void DataTable()
        {
            var shownrRegistros = resultadosPorPage.SelectedItem.ToString() == "Todos" ? 99999999 : Validation.ConvertToInt32(resultadosPorPage.SelectedItem);

            await SetContentTableAsync(GridListaProdutos, null, search.Text, shownrRegistros);

            var totalRegistros = new Model.Item().Query().SelectRaw("COUNT(ID) as TOTAL").Where("Excluir", 0)
                .Where("Tipo", "Produtos").FirstOrDefault();
            nrRegistros.Text = $@"Exibindo: {GridListaProdutos.Rows.Count} de {totalRegistros?.TOTAL ?? 0} registros";
        }

        private void EditProduct(bool create = false)
        {
            if (EditAllProducts.FormOpen)
                return;

            if (create)
            {
                IdPdtSelecionado = 0;
                OpenForm.Show<AddProduct>(this);
                return;
            }

            if (GridListaProdutos.SelectedRows.Count > 0)
            {
                IdPdtSelecionado = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddProduct>(this);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridListaProdutos);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    Support.UpDownDataGrid(true, GridListaProdutos);
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

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 8;

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

            table.Columns[2].Name = "Categoria";
            table.Columns[2].Width = 150;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Cód. de Barras";
            table.Columns[3].Width = 130;
            table.Columns[3].Visible = true;

            table.Columns[4].Name = "Referência";
            table.Columns[4].Width = 100;
            table.Columns[4].Visible = true;

            table.Columns[5].Name = "Descrição";
            table.Columns[5].Width = 120;
            table.Columns[5].MinimumWidth = 120;
            table.Columns[5].Visible = true;

            table.Columns[6].Name = "Custo";
            table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[6].Width = 100;
            table.Columns[6].Visible = true;

            table.Columns[7].Name = "Venda";
            table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[7].Width = 100;
            table.Columns[7].Visible = true;

            table.Columns[8].Name = "Estoque Atual";
            table.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[8].Width = 120;
            table.Columns[8].Visible = true;
        }

        private async Task SetContentTableAsync(DataGridView table, IEnumerable<dynamic> data = null, string searchText = "", int nrRegistros = 50)
        {
            table.Rows.Clear();

            if (data == null)
            {
                var dados = await _controller.GetDataTable(searchText, nrRegistros);
                data = dados;
            }

            foreach (var item in data)
                table.Rows.Add(
                    false,
                    item.ID,
                    item.CATEGORIA,
                    item.CODEBARRAS,
                    item.REFERENCIA,
                    item.NOME,
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPRA), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA), true),
                    Validation.FormatMedidas(item.MEDIDA, Validation.ConvertToDouble(item.ESTOQUEATUAL))
                );

            table.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                Refresh();

                resultadosPorPage.SelectedItem = "50";
                search.Select();
                DataTableStart();
                SetHeadersTable(GridListaProdutos);
            };

            btnEditAll.Click += (s, e) =>
            {
                ListProdutos.Clear();

                foreach (DataGridViewRow item in GridListaProdutos.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        ListProdutos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                EditAllProducts.ListProducts = ListProdutos;
                OpenForm.Show<EditAllProducts>(this);

                btnMarcarCheckBox.Text = @"Marcar Todos";
                btnRemover.Visible = false;
                btnEditAll.Visible = false;
                btnEditar.Visible = true;
                btnAdicionar.Visible = true;
            };

            btnAdicionar.Click += (s, e) => EditProduct(true);
            btnEditar.Click += (s, e) => EditProduct();
            GridListaProdutos.DoubleClick += (s, e) => EditProduct();

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridListaProdutos.Visible = false;
            };

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridListaProdutos.Rows)
                    if (btnMarcarCheckBox.Text == "Marcar Todos")
                    {
                        if ((bool) item.Cells["Selecione"].Value == false)
                        {
                            item.Cells["Selecione"].Value = true;
                            btnRemover.Visible = true;
                            btnEditAll.Visible = true;
                            btnEditar.Visible = false;
                            btnAdicionar.Visible = false;
                        }
                    }
                    else
                    {
                        item.Cells["Selecione"].Value = false;
                        btnRemover.Visible = false;
                        btnEditAll.Visible = false;
                        btnEditar.Visible = true;
                        btnAdicionar.Visible = true;
                    }

                btnMarcarCheckBox.Text = btnMarcarCheckBox.Text == @"Marcar Todos" ? "Desmarcar Todos" : "Marcar Todos";
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);

            workerBackground.DoWork += async (s, e) => { dataTable = await _controller.GetDataTable(); };

            workerBackground.RunWorkerCompleted += async (s, e) =>
            {
                await SetContentTableAsync(GridListaProdutos, dataTable);

                Loading.Visible = false;
                GridListaProdutos.Visible = true;
            };

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker) delegate
            {
                DataTable();
                Loading.Visible = false;
                GridListaProdutos.Visible = true;
                Refresh();
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };

            resultadosPorPage.SelectedValueChanged += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };

            imprimir.Click += async (s, e) => await RenderizarAsync();

            btnRemover.Click += (s, e) =>
            {
                ListProdutos.Clear();
                foreach (DataGridViewRow item in GridListaProdutos.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        ListProdutos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a deletar os PRODUTOS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in ListProdutos)
                        new Model.Item().Remove(item, false);

                    DataTable();
                }

                btnMarcarCheckBox.Text = @"Marcar Todos";
                btnRemover.Visible = false;
                btnEditAll.Visible = false;
                btnEditar.Visible = true;
                btnAdicionar.Visible = true;
            };

            GridListaProdutos.CellClick += (s, e) =>
            {
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool) GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditAll.Visible = true;
                        btnEditar.Visible = false;
                        btnAdicionar.Visible = false;
                    }
                    else
                    {
                        GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value = false;

                        var hideBtns = false;
                        var hideBtnsTop = true;
                        foreach (DataGridViewRow item in GridListaProdutos.Rows)
                            if ((bool) item.Cells["Selecione"].Value)
                            {
                                hideBtns = true;
                                hideBtnsTop = false;
                            }

                        btnRemover.Visible = hideBtns;
                        btnEditAll.Visible = hideBtns;
                        btnEditar.Visible = hideBtnsTop;
                        btnAdicionar.Visible = hideBtnsTop;
                    }
                }
            };

            GridListaProdutos.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridListaProdutos.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }

        private async Task RenderizarAsync()
        {
            var f = new OptionsReports();
            if (f.ShowDialog() != DialogResult.OK)
                return;

            var dados = await _controller.GetDataTable(search.Text, f.NrRegistros, f.TodosRegistros, f.OrdemBy, f.Inativos);
            double totalcompras = 0, totalvendas = 0, totalestoque = 0;
            var data = new ArrayList();
            foreach (var item in dados)
            {
                totalcompras = totalcompras + Validation.ConvertToDouble(item.VALORCOMPRA) *
                    Validation.ConvertToDouble(item.ESTOQUEATUAL);
                totalvendas = totalvendas + Validation.ConvertToDouble(item.VALORVENDA) *
                    Validation.ConvertToDouble(item.ESTOQUEATUAL);
                totalestoque = totalestoque + Validation.ConvertToDouble(item.ESTOQUEATUAL);

                data.Add(new
                {
                    item.ID,
                    item.NOME,
                    item.REFERENCIA,
                    item.CODEBARRAS,
                    CUSTO = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPRA)),
                    VENDA = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA)),
                    item.ESTOQUEATUAL,
                    item.CATEGORIA
                });
            }

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Produtos.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                CUSTO = Validation.FormatPrice(totalcompras),
                VENDA = Validation.FormatPrice(totalvendas),
                ESTOQUE = totalestoque
            }));

            Browser.htmlRender = render;
            using (var browser = new Browser())
            {
                browser.ShowDialog();
            }
        }
    }
}
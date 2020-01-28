using DotLiquid;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Produtos : Form
    {
        public static int idPdtSelecionado { get; set; }
        private Controller.Item _controller = new Controller.Item();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        private Timer timer = new Timer(Configs.TimeLoading);

        public List<int> listProdutos = new List<int>();

        public Produtos()
        {
            InitializeComponent();
            Eventos();

            ToolHelp.Show("Pesquise pelo produto utilizando a Descrição, Cód. de Barras ou Categoria do Produto.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        private void DataTableStart()
        {
            GridListaProdutos.Visible = false;
            Loading.Size = new System.Drawing.Size(GridListaProdutos.Width, GridListaProdutos.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void DataTable()
        {
            await SetContentTableAsync(GridListaProdutos, null, search.Text);
            nrRegistros.Text = $"Exibindo: {GridListaProdutos.Rows.Count} registros";
        }
        private void EditProduct(bool create = false)
        {
            if (create)
            {
                idPdtSelecionado = 0;
                OpenForm.Show<AddProduct>(this);
                return;
            }

            if (GridListaProdutos.SelectedRows.Count > 0)
            {
                idPdtSelecionado = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
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

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 8;

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

            Table.Columns[2].Name = "Categoria";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Cód. de Barras";
            Table.Columns[3].Width = 130;
            Table.Columns[3].Visible = true;

            Table.Columns[4].Name = "Referência";
            Table.Columns[4].Width = 100;
            Table.Columns[4].Visible = true;

            Table.Columns[5].Name = "Descrição";
            Table.Columns[5].Width = 120;
            Table.Columns[5].MinimumWidth = 120;
            Table.Columns[5].Visible = true;

            Table.Columns[6].Name = "Custo";
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[6].Width = 100;
            Table.Columns[6].Visible = true;

            Table.Columns[7].Name = "Venda";
            Table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[7].Width = 100;
            Table.Columns[7].Visible = true;

            Table.Columns[8].Name = "Estoque Atual";
            Table.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[8].Width = 120;
            Table.Columns[8].Visible = true;
        }

        private async Task SetContentTableAsync(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "")
        {
            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await _controller.GetDataTable(SearchText);
                Data = dados;
            }

            foreach (dynamic item in Data)
            {
                Table.Rows.Add(
                    false,
                    item.ID,
                    item.CATEGORIA,
                    item.CODEBARRAS,
                    item.REFERENCIA,
                    item.NOME,
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPRA), false),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA), true),
                    Validation.FormatMedidas(item.MEDIDA, Validation.ConvertToDouble(item.ESTOQUEATUAL))
                );
            }
            
            Table.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                search.Select();
                DataTableStart();
                SetHeadersTable(GridListaProdutos);
            };

            btnEditAll.Click += (s, e) =>
            {
                listProdutos.Clear();

                foreach (DataGridViewRow item in GridListaProdutos.Rows)
                    if ((bool)item.Cells["Selecione"].Value == true)
                        listProdutos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                EditAllProducts.listProducts = listProdutos;
                OpenForm.Show<EditAllProducts>(this);
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
            search.KeyPress += (s, e) => e.KeyChar = Char.ToUpper(e.KeyChar);

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridListaProdutos.Rows)
                {
                    if ((bool)item.Cells["Selecione"].Value == true)
                    {
                        item.Cells["Selecione"].Value = false;
                        btnMarcarCheckBox.Text = "Marcar Todos";
                        btnRemover.Visible = false;
                        btnEditAll.Visible = false;
                    }
                    else
                    {
                        item.Cells["Selecione"].Value = true;
                        btnMarcarCheckBox.Text = "Desmarcar Todos";
                        btnRemover.Visible = true;
                        btnEditAll.Visible = true;
                    }
                }
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            WorkerBackground.DoWork += async (s, e) =>
            {
                dataTable = await _controller.GetDataTable();
            };

            WorkerBackground.RunWorkerCompleted += async (s, e) =>
            {
                await SetContentTableAsync(GridListaProdutos, dataTable);

                Loading.Visible = false;
                GridListaProdutos.Visible = true;
            };

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker)delegate
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

            imprimir.Click += async (s, e) => await RenderizarAsync();

            btnRemover.Click += (s, e) =>
            {
                listProdutos.Clear();
                foreach (DataGridViewRow item in GridListaProdutos.Rows)
                    if ((bool)item.Cells["Selecione"].Value == true)
                        listProdutos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar os produtos selecionados, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in listProdutos)
                        new Model.Item().Remove(item, false);

                    DataTable();
                }
            };

            GridListaProdutos.CellClick += (s, e) =>
            {
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditAll.Visible = true;
                    }
                    else
                    {
                        GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value = false;

                        bool hideBtns = false;
                        foreach (DataGridViewRow item in GridListaProdutos.Rows)
                            if ((bool)item.Cells["Selecione"].Value == true)
                                hideBtns = true;

                        btnRemover.Visible = hideBtns;
                        btnEditAll.Visible = hideBtns;
                    }
                }
            };

            GridListaProdutos.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridListaProdutos.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }

        private async Task RenderizarAsync()
        {
            IEnumerable<dynamic> dados = await _controller.GetDataTable(search.Text);

            ArrayList data = new ArrayList();
            foreach (var item in dados)
            {
                data.Add(new
                {
                    ID = item.ID,
                    NOME = item.NOME,
                    REFERENCIA = item.REFERENCIA,
                    CODEBARRAS = item.CODEBARRAS,
                    CUSTO = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPRA)),
                    VENDA = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA)),
                    ESTOQUEATUAL = item.ESTOQUEATUAL,
                    CATEGORIA = item.CATEGORIA
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
                Emissao = DateTime.Now.ToString("dd/MM/yyyy")
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Servicos : Form
    {
        private readonly Item _controller = new Item();

        private IEnumerable<dynamic> dataTable;
        public List<int> ListProdutos = new List<int>();

        private readonly Timer timer = new Timer(Configs.TimeLoading);
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public Servicos()
        {
            InitializeComponent();
            Eventos();

            ToolHelp.Show("Pesquise pelo serviço utilizando a Descrição, Referência ou Categoria do Serviço.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        private void DataTableStart()
        {
            GridLista.Visible = false;
            Loading.Size = new Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            workerBackground.RunWorkerAsync();
        }

        private async void DataTable()
        {
            await SetContentTableAsync(GridLista, null, search.Text);
            var totalRegistros = new Model.Item().Query().SelectRaw("COUNT(ID) as TOTAL").Where("Excluir", 0)
                .Where("Tipo", "Serviços").FirstOrDefault();
            nrRegistros.Text = $@"Exibindo: {GridLista.Rows.Count} de {totalRegistros.TOTAL ?? 0} registros";
        }

        private void EditProduct(bool create = false)
        {
            if (create)
            {
                AddServicos.idSelecionado = 0;
                OpenForm.Show<AddServicos>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                AddServicos.idSelecionado = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddServicos>(this);
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

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});

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

            table.Columns[2].Name = "Referência";
            table.Columns[2].Width = 100;

            table.Columns[3].Name = "Descrição";
            table.Columns[3].Width = 120;
            table.Columns[3].MinimumWidth = 120;

            table.Columns[4].Name = "Custo";
            table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[4].Width = 100;

            table.Columns[5].Name = "Venda";
            table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[5].Width = 100;
        }

        private async Task SetContentTableAsync(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "")
        {
            Table.Rows.Clear();

            if (Data == null)
            {
                var dados = await _controller.GetDataTableServicos(SearchText);
                Data = dados;
            }

            foreach (var item in Data)
                Table.Rows.Add(
                    false,
                    item.ID,
                    item.REFERENCIA,
                    item.NOME,
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPRA), false),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA), true)
                );

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                search.Select();
                SetHeadersTable(GridLista);
                DataTableStart();
            };

            btnAdicionar.Click += (s, e) => EditProduct(true);
            btnEditar.Click += (s, e) => EditProduct();
            GridLista.DoubleClick += (s, e) => EditProduct();

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridLista.Visible = false;
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);

            using (var b = workerBackground)
            {
                b.DoWork += async (s, e) => { dataTable = await _controller.GetDataTableServicos(); };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await SetContentTableAsync(GridLista, dataTable);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                    {
                        item.Cells["Selecione"].Value = false;
                        btnMarcarCheckBox.Text = @"Marcar Todos";
                        btnRemover.Visible = false;
                        btnEditar.Enabled = true;
                        btnAdicionar.Enabled = true;
                    }
                    else
                    {
                        item.Cells["Selecione"].Value = true;
                        btnMarcarCheckBox.Text = @"Desmarcar Todos";
                        btnRemover.Visible = true;
                        btnEditar.Enabled = false;
                        btnAdicionar.Enabled = false;
                    }
            };

            btnRemover.Click += (s, e) =>
            {
                ListProdutos.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        ListProdutos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a deletar os SERVIÇOS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in ListProdutos)
                        new Model.Item().Remove(item, false);

                    DataTable();
                }

                btnMarcarCheckBox.Text = @"Marcar Todos";
                btnRemover.Visible = false;
                btnEditar.Enabled = true;
                btnAdicionar.Enabled = true;
            };

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker) delegate
            {
                DataTable();
                Loading.Visible = false;
                GridLista.Visible = true;
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool) GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditar.Enabled = false;
                        btnAdicionar.Enabled = false;
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
                        btnEditar.Enabled = hideBtnsTop;
                        btnAdicionar.Enabled = hideBtnsTop;
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

            imprimir.Click += async (s, e) => await RenderizarAsync();
        }

        private async Task RenderizarAsync()
        {
            //IEnumerable<dynamic> dados = await _controller.GetDataTable(search.Text);

            //ArrayList data = new ArrayList();
            //foreach (var item in dados)
            //{
            //    data.Add(new
            //    {
            //        ID = item.ID,
            //        NOME = item.NOME,
            //        REFERENCIA = item.REFERENCIA,
            //        CODEBARRAS = item.CODEBARRAS,
            //        CUSTO = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPRA)),
            //        VENDA = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA)),
            //        ESTOQUEATUAL = item.ESTOQUEATUAL,
            //        CATEGORIA = item.CATEGORIA
            //    });
            //}

            //var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Produtos.html"));
            //var render = html.Render(Hash.FromAnonymousObject(new
            //{
            //    INCLUDE_PATH = Program.PATH_BASE,
            //    URL_BASE = Program.PATH_BASE,
            //    Data = data,
            //    NomeFantasia = Settings.Default.empresa_nome_fantasia,
            //    Logo = Settings.Default.empresa_logo,
            //    Emissao = DateTime.Now.ToString("dd/MM/yyyy")
            //}));

            //Browser.htmlRender = render;
            //var f = new Browser();
            //f.ShowDialog();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Emiplus.View.Comercial
{
    /// <summary>
    ///     Responsavel por Clientes, Fornecedores e Transportadoras
    /// </summary>
    public partial class Clientes : Form
    {
        private readonly Pessoa _controller = new Pessoa();

        private IEnumerable<dynamic> dataTable;
        public List<int> ListProdutos = new List<int>();

        private readonly Timer timer = new Timer(Configs.TimeLoading);
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public Clientes()
        {
            InitializeComponent();
            Eventos();

            label1.Text = $@"{Home.pessoaPage}:";
            label6.Text = Home.pessoaPage;

            switch (Home.pessoaPage)
            {
                case "Fornecedores":
                    pictureBox2.Image = Resources.box;
                    label8.Text = @"Produtos";
                    label2.Text = @"Gerencie os Fornecedores da sua empresa aqui! Adicione, edite ou delete um Fornecedor.";
                    break;
                case "Transportadoras":
                    pictureBox2.Image = Resources.box;
                    label8.Text = @"Produtos";
                    label2.Text =
                        @"Gerencie as Transportadoras da sua empresa aqui! Adicione, edite ou delete uma Transportadora.";
                    break;
                case "Entregadores":
                    pictureBox2.Image = Resources.waiter;
                    label8.Text = @"Alimentação";
                    label2.Text = @"Gerencie os Entregadores da sua empresa aqui! Adicione, edite ou delete um Entregador.";
                    break;
            }
        }

        public static int Id { get; set; }

        private async void DataTable()
        {
            var shownrRegistros = resultadosPorPage.SelectedItem.ToString() == "Todos" ? 99999999 : Validation.ConvertToInt32(resultadosPorPage.SelectedItem);

            await SetContentTableAsync(GridLista, null, search.Text, shownrRegistros);

            var totalRegistros = new Model.Pessoa().Query().SelectRaw("COUNT(ID) as TOTAL").Where("Excluir", 0)
                .Where("ID", "!=", 1).Where("Tipo", Home.pessoaPage).FirstOrDefault();
            nrRegistros.Text = $@"Exibindo: {GridLista.Rows.Count} de {totalRegistros.TOTAL ?? 0} registros";
        }

        private void EditClientes(bool create = false)
        {
            if (create)
            {
                Clientes.Id = 0;
                OpenForm.Show<AddClientes>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                Id = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddClientes>(this);
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
                    EditClientes();
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

            table.Columns[2].Name = "Nome / Razão social";
            table.Columns[2].MinimumWidth = 150;

            table.Columns[3].Name = "Nome Fantasia";
            table.Columns[3].Width = 150;

            table.Columns[4].Name = "CPF / CNPJ";
            table.Columns[4].Width = 150;

            table.Columns[5].Name = "RG / IE";
            table.Columns[5].Width = 150;
        }

        private async Task SetContentTableAsync(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "", int nrRegistros = 50)
        {
            Table.Rows.Clear();

            if (Data == null)
            {
                var dados = await _controller.GetDataTableClientes(SearchText, nrRegistros);
                Data = dados;
            }

            foreach (var item in Data)
                Table.Rows.Add(
                    false,
                    item.ID,
                    item.NOME,
                    item.FANTASIA,
                    item.CPF,
                    item.RG
                );

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) => { Refresh(); };

            Shown += (s, e) =>
            {
                resultadosPorPage.SelectedItem = "50";
                search.Select();
                SetHeadersTable(GridLista);

                DataTable();
            };

            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridLista.Visible = false;
            };

            search.KeyDown += KeyDowns;

            btnAdicionar.Click += (s, e) => EditClientes(true);
            btnEditar.Click += (s, e) => EditClientes();
            GridLista.DoubleClick += (s, e) => EditClientes();
            GridLista.KeyDown += KeyDowns;

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();
            label8.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);

            using (var b = workerBackground)
            {
                b.DoWork += async (s, e) => { dataTable = await _controller.GetDataTableClientes(); };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await SetContentTableAsync(GridLista, dataTable);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker) delegate
            {
                DataTable();
                Loading.Visible = false;
                GridLista.Visible = true;
                Refresh();
            });

            resultadosPorPage.SelectedValueChanged += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridLista.Rows)
                    if (btnMarcarCheckBox.Text == @"Marcar Todos")
                    {
                        if ((bool) item.Cells["Selecione"].Value == false)
                        {
                            item.Cells["Selecione"].Value = true;
                            btnRemover.Visible = true;
                            btnEditar.Visible = false;
                            btnAdicionar.Visible = false;
                        }
                    }
                    else
                    {
                        item.Cells["Selecione"].Value = false;
                        btnRemover.Visible = false;
                        btnEditar.Visible = true;
                        btnAdicionar.Visible = true;
                    }

                btnMarcarCheckBox.Text = btnMarcarCheckBox.Text == @"Marcar Todos" ? "Desmarcar Todos" : "Marcar Todos";
            };

            btnRemover.Click += (s, e) =>
            {
                ListProdutos.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        ListProdutos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!",
                    $"Você está prestes a deletar os {Home.pessoaPage} selecionados, continuar?",
                    AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in ListProdutos)
                        new Model.Pessoa().Remove(item, false);

                    DataTable();
                }

                btnMarcarCheckBox.Text = @"Marcar Todos";
                btnRemover.Visible = false;
                btnEditar.Visible = true;
                btnAdicionar.Visible = true;
            };

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

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };

            imprimir.Click += async (s, e) => await RenderizarAsync();
        }

        private async Task RenderizarAsync()
        {
            var f = new OptionsReports();
            if (f.ShowDialog() != DialogResult.OK)
                return;

            var dados = await _controller.GetDataTableClientes(search.Text, f.NrRegistros, f.TodosRegistros, f.OrdemBy,
                f.Inativos);

            var data = new ArrayList();
            foreach (var item in dados)
                data.Add(new
                {
                    item.ID,
                    item.NOME,
                    item.FANTASIA,
                    item.CPF,
                    item.RG
                });

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Pessoas.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                Titulo = Home.pessoaPage
            }));

            Browser.htmlRender = render;
            using (var browser = new Browser())
            {
                browser.ShowDialog();
            }
        }
    }
}
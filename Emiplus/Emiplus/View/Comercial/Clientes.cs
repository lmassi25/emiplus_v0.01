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
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Comercial
{
    /// <summary>
    /// Responsavel por Clientes, Fornecedores e Transportadoras
    /// </summary>
    public partial class Clientes : Form
    {
        public static int Id { get; set; }
        private Controller.Pessoa _controller = new Controller.Pessoa();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        public Clientes()
        {
            InitializeComponent();
            Eventos();

            label1.Text = Home.pessoaPage + ":";
            label6.Text = Home.pessoaPage;

            if (Home.pessoaPage == "Fornecedores")
            {
                pictureBox2.Image = Properties.Resources.box;
                label8.Text = "Produtos";
                label2.Text = "Gerencie os Fornecedores da sua empresa aqui! Adicione, edite ou delete um Fornecedor.";
            }
            else if (Home.pessoaPage == "Transportadoras")
            {
                pictureBox2.Image = Properties.Resources.box;
                label8.Text = "Produtos";
                label2.Text = "Gerencie as Transportadoras da sua empresa aqui! Adicione, edite ou delete uma Transportadora.";
            }
        }

        private async void DataTable() => await _controller.SetTableClientes(GridLista, null, search.Text);

        private void EditClientes(bool create = false)
        {
            if (create)
            {
                Id = 0;
                OpenForm.Show<AddClientes>(this);
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
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

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += (s, e) =>
            {
                search.Select();
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

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _controller.GetDataTableClientes();
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _controller.SetTableClientes(GridLista, dataTable);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker)delegate {
                DataTable();
                Loading.Visible = false;
                GridLista.Visible = true;
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };

            imprimir.Click += async (s, e) => await RenderizarAsync();
        }

        private async Task RenderizarAsync()
        {
            IEnumerable<dynamic> dados = await _controller.GetDataTableClientes(search.Text);

            ArrayList data = new ArrayList();
            foreach (var item in dados)
            {
                data.Add(new
                {
                    ID = item.ID,
                    NOME = item.NOME,
                    FANTASIA = item.FANTASIA,
                    CPF = item.CPF,
                    RG = item.RG
                });
            }

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
            var f = new Browser();
            f.ShowDialog();
        }
    }
}

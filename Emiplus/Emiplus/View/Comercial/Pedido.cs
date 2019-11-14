using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Fiscal;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

using Timer = System.Timers.Timer;

namespace Emiplus.View.Comercial
{
    /// <summary>
    /// Responsavel por Vendas, Compras, Orçamentos, Consignações, Devoluções
    /// </summary>
    public partial class Pedido : Form
    {
        private Controller.Pedido _cPedido = new Controller.Pedido();
        private Model.Pessoa _mPessoa = new Model.Pessoa();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public Pedido()
        {
            InitializeComponent();
            Eventos();

            label3.Text = Home.pedidoPage;
            label1.Text = Home.pedidoPage;

            if (Home.pedidoPage == "Orçamentos")
                label2.Text = "Gerencie os orçamenos aqui! Adicione, edite ou delete um orçamento.";
            else if (Home.pedidoPage == "Consignações")
                label2.Text = "Gerencie as consignações aqui! Adicione, edite ou delete uma consignação.";
            else if (Home.pedidoPage == "Devoluções")
                label2.Text = "Gerencie as devoluções aqui! Adicione, edite ou delete uma devolução.";
            else if (Home.pedidoPage == "Compras")
            {
                label2.Text = "Gerencie as compras aqui! Adicione, edite ou delete uma compra.";
                label11.Text = "Procurar por fornecedor";
            }
            else if (Home.pedidoPage == "Notas")
                label2.Text = "Gerencie as Notas aqui! Adicione, edite ou delete uma Nota.";

            dataInicial.Text = DateTime.Now.ToString();
            dataFinal.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// Autocomplete do campo de busca de pessoas.
        /// </summary>
        private void AutoCompletePessoas()
        {
            var data = _mPessoa.Query();

            data.Select("id", "nome");
            data.Where("excluir", 0);

            switch (Home.pedidoPage)
            {
                case "Compras":
                    data.Where("tipo", "Fornecedores");
                    break;
                case "Consignações":
                    data.Where("tipo", "Clientes");
                    break;
                case "Devoluções":
                    data.Where("tipo", "Clientes");
                    break;
                default:
                    data.Where("tipo", "Clientes");
                    break;
            }

            foreach (var itens in data.Get())
            {
                if (itens.NOME != "Novo registro")
                    collection.Add(itens.NOME, itens.ID);
            }

            BuscarPessoa.AutoCompleteCustomSource = collection;
        }

        /// <summary>
        /// Autocomplete do campo de busca de usuários.
        /// </summary>
        private void AutoCompleteUsers()
        {
            var users = new ArrayList();

            var userId = Settings.Default.user_sub_user == 0 ? Settings.Default.user_id : Settings.Default.user_sub_user;
            var dataUser = new RequestApi().URL($"{Program.URL_BASE}/api/listall/{Program.TOKEN}/{userId}").Content().Response();

            users.Add(new { Id = "0", Nome = "Todos" });
            foreach (var item in dataUser)
            {
                var nameComplete = $"{item.Value["name"].ToString()} {item.Value["lastname"].ToString()}";
                users.Add(new { Id = item.Value["id"].ToString(), Nome = nameComplete });
            }

            Usuarios.DataSource = users;
            Usuarios.DisplayMember = "Nome";
            Usuarios.ValueMember = "Id";
        }

        private void DataTableStart()
        {
            BuscarPessoa.Select();

            GridLista.Visible = false;
            Loading.Size = new System.Drawing.Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void Filter()
        {
            int excluir = 0;

            if (filterRemovido.Checked)
                excluir = 1;

            await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, null, BuscarPessoa.Text, excluir);
        }   

        private void EditPedido(bool create = false)
        {
            if (create)
            {
                if (Home.pedidoPage == "Notas")
                {
                    Nota.Id = 0;
                    Nota nota = new Nota();
                    nota.ShowDialog();
                    return;
                }
                else
                {
                    AddPedidos.Id = 0;
                    AddPedidos NovoPedido = new AddPedidos();
                    NovoPedido.ShowDialog();
                    return;
                }
            }
            else if (GridLista.SelectedRows.Count > 0)
            {
                if (Home.pedidoPage == "Notas")
                {
                    Nota.Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    Nota nota = new Nota();
                    nota.ShowDialog();
                }
                else
                {
                    DetailsPedido.idPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    DetailsPedido detailsPedido = new DetailsPedido();
                    detailsPedido.ShowDialog();
                }
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
                    EditPedido();
                    break;
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            { 
                DataTableStart();
                BuscarPessoa.Select();
                AutoCompletePessoas();
                AutoCompleteUsers();

                //dataInicial.Text = DateTime.Today.AddMonths(-1).ToString();
                dataInicial.Text = DateTime.Now.ToString();
                dataFinal.Text = DateTime.Now.ToString();

                filterTodos.Checked = true;
            };

            BuscarPessoa.KeyDown += KeyDowns;
            BuscarPessoa.TextChanged += (s, e) =>
            {
                //timer.Stop();
                //timer.Start();
                //Loading.Visible = true;
                //GridLista.Visible = false;
            };

            btnSearch.Click += (s, e) => Filter();

            btnAdicionar.Click += (s, e) =>
            {
                if (Home.pedidoPage == "Orçamentos")
                    Home.pedidoPage = "Orçamentos";
                
                EditPedido(true);
            };

            btnEditar.Click += (s, e) => EditPedido();
            GridLista.DoubleClick += (s, e) =>EditPedido();
            GridLista.KeyDown += KeyDowns;

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _cPedido.GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, dataTable, BuscarPessoa.Text);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => BuscarPessoa.Invoke((MethodInvoker)delegate {
                Filter();
                Loading.Visible = false;
                GridLista.Visible = true;
            });

            BuscarPessoa.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                Filter();
            };
        }
    }
}

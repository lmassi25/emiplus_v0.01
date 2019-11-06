using DotLiquid;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Reports
{
    public partial class EstoqueEntradaSaida : Form
    {
        private Model.Item _mItem = new Model.Item();

        // AutoComplete
        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        public EstoqueEntradaSaida()
        {
            InitializeComponent();
            Eventos();
        }

        private void DataTableStart()
        {
            GridLista.Visible = false;
            Loading.Size = new Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void DataTable() => await SetTable(GridLista);

        /// <summary>
        /// Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
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

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new Model.ItemEstoqueMovimentacao().Query();
            
            if (!noFilterData.Checked)
            {
                model.Where("ITEM_MOV_ESTOQUE.criado", ">=", Validation.ConvertDateToSql(dataInicial.Text))
                .Where("ITEM_MOV_ESTOQUE.criado", "<=", Validation.ConvertDateToSql(dataFinal.Text));
            }

            if (Validation.ConvertToInt32(Locais.SelectedValue) >= 1)
            {
                var local = "";
                if (Validation.ConvertToInt32(Locais.SelectedValue) == 1)
                    local = "Cadastro de Produto";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 2)
                    local = "Vendas";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 3)
                    local = "Orçamentos";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 4)
                    local = "Consignações";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 5)
                    local = "Devoluções";

                model.Where("ITEM_MOV_ESTOQUE.local", local);
            }

            if (Validation.ConvertToInt32(Usuarios.SelectedValue) >= 1)
            {
                model.Where("ITEM_MOV_ESTOQUE.id_usuario", Validation.ConvertToInt32(Usuarios.SelectedValue));
            }

            if (!filterAll.Checked) {
                if (filterAdicionado.Checked)
                {
                    model.Where("ITEM_MOV_ESTOQUE.TIPO", "A");
                }

                if (filterRemovido.Checked)
                {
                    model.Where("ITEM_MOV_ESTOQUE.TIPO", "R");
                }
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                model.Where("ITEM_MOV_ESTOQUE.id_item", collection.Lookup(BuscarProduto.Text));
            }

            model.RightJoin("ITEM", "ITEM.id", "ITEM_MOV_ESTOQUE.id_item");
            model.OrderByDesc("ITEM_MOV_ESTOQUE.ID");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "Produto";
            Table.Columns[0].Width = 150;

            Table.Columns[1].Name = "Usuário";
            Table.Columns[1].Width = 150;

            Table.Columns[2].Name = "Quantidade";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Ação";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Local";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Estoque Anterior";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                var dataUser = new RequestApi().URL($"{Program.URL_BASE}/api/list/{Program.TOKEN}/{item.ID_USUARIO}").Content().Response();
                string userName = "";
                if (dataUser["erro"] == null)
                    userName = $"{dataUser["user"]["name"].ToString()} {dataUser["user"]["lastname"].ToString()}";
                else
                    userName = "Usuário não encontrado";
                
                Table.Rows.Add(
                    item.NOME,
                    $"{userName}",
                    item.QUANTIDADE,
                    (item.TIPO == "R") ? "Retirar" : "Adicionar",
                    item.LOCAL,
                    item.ANTERIOR
                );
            }

            Table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Load += (s, e) => {
                BuscarProduto.Select();
                DataTableStart();
                AutoCompleteItens();
                AutoCompleteUsers();

                dataInicial.Text = DateTime.Today.AddMonths(-1).ToString();
                dataFinal.Text = DateTime.Now.ToString();
                GetDataTable();

                var origens = new ArrayList();
                origens.Add(new { Id = "0", Nome = "Todos" });
                origens.Add(new { Id = "1", Nome = "Cadastro de Produtos" });
                origens.Add(new { Id = "2", Nome = "Vendas" });
                origens.Add(new { Id = "3", Nome = "Orçamentos" });
                origens.Add(new { Id = "4", Nome = "Consignações" });
                origens.Add(new { Id = "5", Nome = "Devoluções" });

                Locais.DataSource = origens;
                Locais.DisplayMember = "Nome";
                Locais.ValueMember = "Id";
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSearch.Click += (s, e) =>
            {
                DataTable();
            };

            imprimir.Click += async (s, e) =>
            {
                IEnumerable<dynamic> dados = await GetDataTable();

                ArrayList data = new ArrayList();

                foreach (var item in dados)
                {
                    var dataUser = new RequestApi().URL($"{Program.URL_BASE}/api/list/{Program.TOKEN}/{item.ID_USUARIO}").Content().Response();
                    string userName = "";
                    if (dataUser["erro"] == null)
                        userName = $"{dataUser["user"]["name"].ToString()} {dataUser["user"]["lastname"].ToString()}";
                    else
                        userName = "Usuário não encontrado";

                    data.Add(new
                    {
                        Nome = item.NOME,
                        Usuario = userName,
                        Quantidade = item.QUANTIDADE,
                        Tipo = (item.TIPO == "R") ? "Retirar" : "Adicionar",
                        Local = item.LOCAL,
                        EstoqueAnterior = item.ANTERIOR
                    });
                }

                var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\View\Reports\html\EstoqueEntradaSaida.html"));
                var render = html.Render(Hash.FromAnonymousObject(new
                {
                    INCLUDE_PATH = Program.PATH_BASE,
                    URL_BASE = Program.PATH_BASE,
                    Data = data,
                    noFilterData = noFilterData.Checked,
                    dataInicial = dataInicial.Text,
                    dataFinal = dataFinal.Text
                }));

                Browser.htmlRender = render;
                var f = new Browser();
                f.ShowDialog();
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");

            using (var b = WorkerBackground)
            {
                //b.DoWork += async (s, e) =>
                //{
                //    dataTable = await GetDataTable();
                //};

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await SetTable(GridLista, dataTable);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => BuscarProduto.Invoke((MethodInvoker)delegate {
                DataTable();
                Loading.Visible = false;
                GridLista.Visible = true;
            });

            BuscarProduto.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };
        }
    }
}

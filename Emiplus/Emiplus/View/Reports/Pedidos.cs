using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Reports
{
    public partial class Pedidos : Form
    {
        private Model.Pessoa _mPessoa = new Model.Pessoa();

        public string screen;

        // AutoComplete
        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public Pedidos()
        {
            InitializeComponent();
            Eventos();
        }

        private async Task DataTableAsync() => await SetTable(GridLista);

        /// <summary>
        /// Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompletePessoas()
        {
            var data = _mPessoa.Query();

            data.Select("id", "nome");
            data.Where("excluir", 0);

            switch (screen)
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
            Usuarios.DataSource = (new Model.Usuarios()).GetAllUsers();
            Usuarios.DisplayMember = "Nome";
            Usuarios.ValueMember = "Id";
        }

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new Model.Pedido().Query();

            if (!noFilterData.Checked)
            {
                model.Where("PEDIDO.emissao", ">=", Validation.ConvertDateToSql(dataInicial.Value, true))
                .Where("PEDIDO.emissao", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));
            }

            if (Validation.ConvertToInt32(Usuarios.SelectedValue) >= 1)
            {
                model.Where("PEDIDO.id_usuario", Validation.ConvertToInt32(Usuarios.SelectedValue));
            }

            if (filterRemovido.Checked)
            {
                model.Where("PEDIDO.excluir", "1");
            }
            else
            {
                model.Where("PEDIDO.excluir", "0");
            }

            if (collection.Lookup(BuscarPessoa.Text) > 0)
            {
                model.Where("PEDIDO.cliente", collection.Lookup(BuscarPessoa.Text));
            }

            switch (screen)
            {
                case "Compras":
                    model.Where("PEDIDO.TIPO", "Compras");
                    break;

                case "Orçamentos":
                    model.Where("PEDIDO.TIPO", "Orcamentos");
                    break;

                case "Consignações":
                    model.Where("PEDIDO.TIPO", "Consignacoes");
                    break;

                case "Devoluções":
                    model.Where("PEDIDO.TIPO", "Devolucoes");
                    break;

                default:
                    model.Where("PEDIDO.TIPO", "Vendas");
                    break;
            }

            model.LeftJoin("PESSOA", "PESSOA.ID", "PEDIDO.CLIENTE");
            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "PEDIDO.id_usuario");
            model.Select("PEDIDO.*", "PESSOA.NOME AS nome_cliente", "USUARIOS.nome as nome_user");
            //model.Limit(200);
            model.OrderByDesc("PEDIDO.ID");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "#";
            Table.Columns[0].Width = 50;

            Table.Columns[1].Name = "Emissão";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Cliente";
            Table.Columns[2].Width = 200;

            Table.Columns[3].Name = "Total";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Usuário";
            Table.Columns[4].Width = 100;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    Validation.ConvertDateToForm(item.EMISSAO),
                    item.NOME_CLIENTE,
                    Validation.FormatPrice(item.TOTAL, false),
                    item.NOME_USER
                );
            }

            Table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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

            Load += (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                screen = Home.pedidoPage;

                switch (screen)
                {
                    case "Compras":
                        t1.Text = "Compras";
                        t2.Text = "Procurar fornecedor";
                        t3.Text = "Histórico de Compras";
                        t4.Text = "Consulte o histórico de compras.";
                        break;

                    case "Consignações":
                        t1.Text = "Consignações";
                        t2.Text = "Procurar cliente";
                        t3.Text = "Histórico de Consignações";
                        t4.Text = "Consulte o histórico de consignações.";
                        break;

                    case "Devoluções":
                        t1.Text = "Devoluções";
                        t2.Text = "Procurar cliente";
                        t3.Text = "Histórico de Devoluções";
                        t4.Text = "Consulte o histórico de devoluções.";
                        break;

                    default:
                        t1.Text = "Vendas";
                        t2.Text = "Procurar cliente";
                        t3.Text = "Histórico de Vendas";
                        t4.Text = "Consulte o histórico de vendas.";
                        break;
                }

                BuscarPessoa.Select();
                AutoCompletePessoas();
                AutoCompleteUsers();

                dataInicial.Text = DateTime.Today.AddMonths(-1).ToString();
                dataFinal.Text = DateTime.Now.ToString();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSearch.Click += async (s, e) =>
            {
                label13.Visible = false;
                await DataTableAsync();
            };

            imprimir.Click += async (s, e) => await RenderizarAsync();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }

        private async Task RenderizarAsync()
        {
            IEnumerable<dynamic> dados = await GetDataTable();

            ArrayList data = new ArrayList();
            foreach (var item in dados)
            {
                data.Add(new
                {
                    Nome = item.NOME,
                    Usuario = item.NOME_USER,
                    Quantidade = item.QUANTIDADE,
                    Tipo = (item.TIPO == "R") ? "-" : "+",
                    Local = item.LOCAL,
                    EstoqueAnterior = item.ANTERIOR,
                    EstoqueTotal = (item.TIPO == "R") ? item.ANTERIOR - item.QUANTIDADE : item.ANTERIOR + item.QUANTIDADE,
                    Referencia = item.REFERENCIA,
                    Criado = Validation.ConvertDateToForm(item.CRIADO, true)
                });
            }

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\EstoqueEntradaSaida.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                noFilterData = noFilterData.Checked,
                dataInicial = dataInicial.Text,
                dataFinal = dataFinal.Text
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}
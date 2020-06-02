using System;
using System.Collections;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class PedidoTotais : Form
    {
        private readonly KeyedAutoCompleteStringCollection collectionItem = new KeyedAutoCompleteStringCollection();
        private readonly KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public PedidoTotais()
        {
            InitializeComponent();
            Eventos();
        }

        private void AutoCompletePessoas()
        {
            var data = new Pessoa().Query();

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
                if (itens.NOME != "Novo registro")
                    collection.Add(itens.NOME, itens.ID);

            BuscarPessoa.AutoCompleteCustomSource = collection;
        }

        /// <summary>
        ///     Autocomplete do campo de busca de usuários.
        /// </summary>
        private void AutoCompleteUsers()
        {
            Usuarios.DataSource = new Usuarios().GetAllUsers();
            Usuarios.DisplayMember = "Nome";
            Usuarios.ValueMember = "Id";
        }

        public void GetDataTablePedidos(string tipo, string dataStart, string dataEnd, bool excluir = false,
            int status = 0,
            int usuario = 0, int cliente = 0)
        {
            var query = new Model.Pedido().Query();

            query.SelectRaw("count(pedido.id) as pedidos, sum(pedido.total) as pedidototal, sum(pedido.desconto) as descontototal, sum(pedido.frete) as pedidofrete")
                .Where("pedido.excluir", excluir ? 1 : 0)
                .Where("pedido.emissao", ">=", Validation.ConvertDateToSql(dataStart))
                .Where("pedido.emissao", "<=", Validation.ConvertDateToSql(dataEnd));

            if (!tipo.Contains("Notas"))
                query.Where("pedido.tipo", tipo);

            if (usuario != 0)
                query.Where("pedido.colaborador", usuario);

            if (cliente != 0)
                query.Where("pedido.cliente", cliente);

            if (status != 99)
                query.Where("pedido.status", status);

            var dados = query.FirstOrDefault<dynamic>();

            if (dados != null)
            {
                label21.Visible = false;
                label6.Visible = true;
                label6.Text = $@"Dados do periódio {dataInicial.Value:dd/MM/yy} até {dataFinal.Value:dd/MM/yy}";

                label14.Visible = true;
                label7.Visible = true;
                label7.Text = dados.PEDIDOS.ToString() ?? "0";

                label16.Visible = true;
                label15.Visible = true;
                label15.Text = Validation.FormatPrice(Validation.ConvertToDouble(dados.PEDIDOTOTAL), true);
                
                label18.Visible = true;
                label17.Visible = true;
                label17.Text = Validation.FormatPrice(Validation.ConvertToDouble(dados.DESCONTOTOTAL), true);

                label20.Visible = true;
                label19.Visible = true;
                label19.Text = Validation.FormatPrice(Validation.ConvertToDouble(dados.PEDIDOFRETE), true);
            }
        }
        
        private void FilterAsync()
        {
            GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text,
                filterRemovido.Checked,
                Validation.ConvertToInt32(Status.SelectedValue),
                Validation.ConvertToInt32(Usuarios.SelectedValue),
                collection.Lookup(BuscarPessoa.Text));

        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                AutoCompletePessoas();
                AutoCompleteUsers();

                dataInicial.Text = DateTime.Now.ToString();
                dataFinal.Text = DateTime.Now.ToString();

                var status = new ArrayList {new {ID = 99, NOME = "Todos"}};
                switch (Home.pedidoPage)
                {
                    case "Notas":
                    case "Cupons":
                    {
                        if (Home.pedidoPage == "Notas")
                            status.Add(new {ID = 1, NOME = "Pendentes"});

                        status.Add(new {ID = 2, NOME = "Autorizadas"});
                        status.Add(new {ID = 3, NOME = "Canceladas"});
                        break;
                    }
                    case "Orçamentos":
                    case "Devoluções":
                    case "Consignações":
                        status.Add(new {ID = 0, NOME = "Pendente"});
                        status.Add(new {ID = 1, NOME = "Finalizado"});
                        break;
                    default:
                        status.Add(new {ID = 2, NOME = "Recebimento Pendente"});
                        status.Add(new {ID = 1, NOME = @"Finalizado\Recebido"});
                        break;
                }

                Status.DataSource = status;
                Status.DisplayMember = "NOME";
                Status.ValueMember = "ID";
                Status.SelectedValue = 99;
            };

            btnSearch.Click += (s, e) => { FilterAsync(); };

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();
            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
        }
    }
}
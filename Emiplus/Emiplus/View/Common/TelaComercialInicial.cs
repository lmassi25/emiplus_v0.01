using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Comercial;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaComercialInicial : Form
    {
        public TelaComercialInicial()
        {
            InitializeComponent();

            Eventos();
        }

        private void Eventos()
        {
            Clientes.Click += (s, e) =>
            {
                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            Pedidos.Click += (s, e) =>
            {
                var Caixa = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id).FirstOrDefault();
                if (Caixa == null)
                {
                    Alert.Message("Oppss!", "Você NÃO possui um caixa aberto, abra um caixa para iniciar uma venda.", Alert.AlertType.warning);
                    return;
                }

                Home.pedidoPage = "Vendas";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            Orcamentos.Click += (s, e) =>
            {
                Home.pedidoPage = "Orçamentos";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            Consignacoes.Click += (s, e) =>
            {
                Home.pedidoPage = "Consignações";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            Devolucoes.Click += (s, e) =>
            {
                Home.pedidoPage = "Devoluções";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };
            
            VendasRel.Click += (s, e) =>
            {
                Home.pedidoPage = "Vendas";
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };

            ProdutosVendidos.Click += (s, e) =>
            {
                Home.pedidoPage = "Vendas";
                Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                ProdVendidos.ShowDialog();
            };

            ConsignacoesRel.Click += (s, e) =>
            {
                Home.pedidoPage = "Consignações";
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };

            ProdutosConsignados.Click += (s, e) =>
            {
                Home.pedidoPage = "Consignações";
                Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                ProdVendidos.ShowDialog();
            };

            DevolucoesRel.Click += (s, e) =>
            {
                Home.pedidoPage = "Devoluções";
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };

            ProdutosDevolvidos.Click += (s, e) =>
            {
                Home.pedidoPage = "Devoluções";
                Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                ProdVendidos.ShowDialog();
            };
        }
    }
}

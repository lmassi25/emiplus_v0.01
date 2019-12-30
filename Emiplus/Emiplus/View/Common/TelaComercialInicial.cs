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
                if (UserPermission.SetControl(Clientes, pictureBox11, "com_clientes"))
                    return;

                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            Pedidos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Pedidos, pictureBox5, "com_novavenda"))
                    return;

                Home.pedidoPage = "Vendas";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            Orcamentos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Orcamentos, pictureBox3, "com_novoorcamento"))
                    return;

                Home.pedidoPage = "Orçamentos";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            Consignacoes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Consignacoes, pictureBox4, "com_novaconsig"))
                    return;

                Home.pedidoPage = "Consignações";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            Devolucoes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Devolucoes, pictureBox6, "com_novadevo"))
                    return;

                Home.pedidoPage = "Devoluções";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };
            
            VendasRel.Click += (s, e) =>
            {
                if (UserPermission.SetControl(VendasRel, pictureBox12, "com_vendas"))
                    return;

                Home.pedidoPage = "Vendas";
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };

            ProdutosVendidos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(ProdutosVendidos, pictureBox13, "com_pdtvendidos"))
                    return;

                Home.pedidoPage = "Vendas";
                Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                ProdVendidos.ShowDialog();
            };

            ConsignacoesRel.Click += (s, e) =>
            {
                if (UserPermission.SetControl(ConsignacoesRel, pictureBox9, "com_consig"))
                    return;

                Home.pedidoPage = "Consignações";
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };

            ProdutosConsignados.Click += (s, e) =>
            {
                //if (UserPermission.SetControl(ProdutosConsignados, pictureBox10, "com_pdtconsignados"))
                //    return;

                //Home.pedidoPage = "Consignações";
                //Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                //ProdVendidos.ShowDialog();
            };

            DevolucoesRel.Click += (s, e) =>
            {
                if (UserPermission.SetControl(DevolucoesRel, pictureBox15, "com_devolucoes"))
                    return;

                Home.pedidoPage = "Devoluções";
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };

            OrcamentosRel.Click += (s, e) =>
            {
                if (UserPermission.SetControl(OrcamentosRel, pictureBox7, "com_orcamentos"))
                    return;

                Home.pedidoPage = "Orçamentos";
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };

            ProdutosOrcados.Click += (s, e) =>
            {
                //if (UserPermission.SetControl(ProdutosOrcados, pictureBox8, "com_pdtorcados"))
                //    return;

                //Home.pedidoPage = "Orçamentos";
                //Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                //ProdVendidos.ShowDialog();
            };
        }
    }
}

using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Comercial;
using Emiplus.View.Reports;
using SqlKata.Execution;
using Pedido = Emiplus.View.Comercial.Pedido;

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
            OS.Click += (s, e) =>
            {
                //if (UserPermission.SetControl(Clientes, pictureBox11, "com_clientes"))
                //    return;

                AddOs.Id = 0;
                OpenForm.Show<AddOs>(this);
            };

            OSRel.Click += (s, e) =>
            {
                //if (UserPermission.SetControl(Pedidos, pictureBox5, "com_novavenda"))
                //    return;

                Home.pedidoPage = "Ordens de Servico";
                OpenForm.Show<Pedido>(this);
            };

            btnPdv.Click += (s, e) =>
            {
                Home.pedidoPage = "Vendas";
                AddPedidos.Id = 0;
                AddPedidos.PDV = true;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            Clientes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Clientes, pictureBox11, "com_clientes"))
                    return;

                Home.pessoaPage = "Clientes";
                OpenForm.Show<Clientes>(this);
            };

            Pedidos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Pedidos, pictureBox5, "com_novavenda"))
                    return;

                Home.pedidoPage = "Vendas";
                AddPedidos.Id = 0;
                AddPedidos.PDV = false;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            Orcamentos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Orcamentos, pictureBox3, "com_novoorcamento"))
                    return;

                Home.pedidoPage = "Orçamentos";
                AddPedidos.Id = 0;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            Consignacoes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Consignacoes, pictureBox4, "com_novaconsig"))
                    return;

                Home.pedidoPage = "Consignações";
                AddPedidos.Id = 0;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            Devolucoes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Devolucoes, pictureBox6, "com_novadevo"))
                    return;

                Home.pedidoPage = "Devoluções";
                AddPedidos.Id = 0;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            VendasRel.Click += (s, e) =>
            {
                if (UserPermission.SetControl(VendasRel, pictureBox12, "com_vendas"))
                    return;

                Home.pedidoPage = "Vendas";
                OpenForm.Show<Pedido>(this);
            };

            ProdutosVendidos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(ProdutosVendidos, pictureBox13, "com_pdtvendidos"))
                    return;

                Home.pedidoPage = "Vendas";
                OpenForm.Show<ProdutosVendidos>(this);
                //Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                //ProdVendidos.ShowDialog();
            };

            ConsignacoesRel.Click += (s, e) =>
            {
                if (UserPermission.SetControl(ConsignacoesRel, pictureBox9, "com_consig"))
                    return;

                Home.pedidoPage = "Consignações";
                OpenForm.Show<Pedido>(this);
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
                OpenForm.Show<Pedido>(this);
            };

            OrcamentosRel.Click += (s, e) =>
            {
                if (UserPermission.SetControl(OrcamentosRel, pictureBox7, "com_orcamentos"))
                    return;

                Home.pedidoPage = "Orçamentos";
                OpenForm.Show<Pedido>(this);
            };

            ProdutosOrcados.Click += (s, e) =>
            {
                //if (UserPermission.SetControl(ProdutosOrcados, pictureBox8, "com_pdtorcados"))
                //    return;

                //Home.pedidoPage = "Orçamentos";
                //Reports.ProdutosVendidos ProdVendidos = new Reports.ProdutosVendidos();
                //ProdVendidos.ShowDialog();
            };

            Comissoes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Comissoes, pictureBox14, "com_comissoes"))
                    return;

                var usuarios = new Usuarios().FindByUserId(Settings.Default.user_id).FirstOrDefault<Usuarios>();
                if (usuarios.Sub_user == 0)
                {
                    OpenForm.Show<Comissão>(this);
                }
                else
                {
                    DetalhesComissao.idUser = Settings.Default.user_id;
                    OpenForm.Show<DetalhesComissao>(this);
                }
            };
        }
    }
}
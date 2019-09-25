using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class Pedido : Form
    {
        private Controller.Pedido _cPedido = new Controller.Pedido();

        public Pedido()
        {
            InitializeComponent();
            Events();

            label3.Text = Home.pedidoPage;
            label1.Text = Home.pedidoPage;

            if (Home.pedidoPage == "Orçamentos")
                label2.Text = "Gerencie os orçamenos aqui! Adicione, edite ou delete um orçamento.";
            else if (Home.pedidoPage == "Consignações")
                label2.Text = "Gerencie as consignações aqui! Adicione, edite ou delete uma consignação.";
            else if (Home.pedidoPage == "Devoluções")
                label2.Text = "Gerencie as devoluções aqui! Adicione, edite ou delete uma devolução.";
            else if (Home.pedidoPage == "Compras")
                label2.Text = "Gerencie as compras aqui! Adicione, edite ou delete uma compra.";

            dataInicial.Text = DateTime.Now.ToString();
            dataFinal.Text = DateTime.Now.ToString();
        }

        private void Filter() => _cPedido.GetDataTablePedidos(GridLista, Home.pedidoPage, search.Text, dataInicial.Text, dataFinal.Text);

        private void EditPedido(bool create = false)
        {
            if (create)
            {
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                DetailsPedido.idPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                DetailsPedido detailsPedido = new DetailsPedido();
                detailsPedido.ShowDialog();
            }
        }

        private void Events()
        {
            Load += (s, e) => Filter();
            search.TextChanged += (s, e) => Filter();
            btnSearch.Click += (s, e) => Filter();

            btnAdicionar.Click += (s, e) => EditPedido(true);
            btnEditar.Click += (s, e) => EditPedido();
            GridLista.DoubleClick += (s, e) =>EditPedido();

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}

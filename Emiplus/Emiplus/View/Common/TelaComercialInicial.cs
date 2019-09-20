using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaComercialInicial : Form
    {
        public TelaComercialInicial()
        {
            InitializeComponent();
        }

        private void Clientes_Click(object sender, System.EventArgs e)
        {
            Home.pessoaPage = "Clientes";
            OpenForm.Show<Comercial.Clientes>(this);
        }

        private void NovoPedido_Click(object sender, System.EventArgs e)
        {
            Home.pedidoPage = "Vendas";
            AddPedidos.Id = 0;
            AddPedidos NovoPedido = new AddPedidos();
            NovoPedido.ShowDialog();
        }

        private void Pedidos_Click(object sender, System.EventArgs e)
        {
            Pedido Pedido = new Pedido();
            Pedido.ShowDialog();
        }
    }
}

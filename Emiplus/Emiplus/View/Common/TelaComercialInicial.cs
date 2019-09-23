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

            Events();
        }

        private void Events()
        {
            Clientes.Click += (s, e) =>
            {
                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            NovoPedido.Click += (s, e) =>
            {
                Home.pedidoPage = "Vendas";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            Pedidos.Click += (s, e) =>
            {
                Pedido Pedido = new Pedido();
                Pedido.ShowDialog();
            };
        }
    }
}

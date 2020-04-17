using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaFood : Form
    {
        public TelaFood()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            btnAddItem.Click += (s, e) =>
            {
                AddItemMesa form = new AddItemMesa();
                form.ShowDialog();
            };

            Clientes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Clientes, pictureBox11, "com_clientes"))
                    return;

                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            btnEntregadores.Click += (s, e) =>
            {
                Home.pessoaPage = "Entregadores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            Pedidos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Pedidos, pictureBox5, "com_novavenda"))
                    return;

                Home.pedidoPage = "Balcao";
                AddPedidos.Id = 0;
                AddPedidos.PDV = false;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            btnDelivery.Click += (s, e) =>
            {
                Home.pedidoPage = "Delivery";
                AddPedidos.Id = 0;
                AddPedidos.PDV = false;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            VendasRel.Click += (s, e) =>
            {
                Home.pedidoPage = "Food";
                OpenForm.Show<Food.Pedidos>(this);
            };

            btnCadastrarMesa.Click += (s, e) =>
            {
                OpenForm.Show<Food.Mesas>(this);
            };

            Mesas.Click += (s, e) =>
            {
                OpenForm.Show<Mesas>(this);
            };
        }
    }
}

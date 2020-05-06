using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using Emiplus.View.Food;
using Mesas = Emiplus.View.Food.Mesas;

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
                var form = new AddItemMesa();
                form.ShowDialog();
            };

            Clientes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Clientes, pictureBox11, "com_clientes"))
                    return;

                Home.pessoaPage = "Clientes";
                OpenForm.Show<Clientes>(this);
            };

            btnEntregadores.Click += (s, e) =>
            {
                Home.pessoaPage = "Entregadores";
                OpenForm.Show<Clientes>(this);
            };

            Pedidos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Pedidos, pictureBox5, "com_novavenda"))
                    return;

                Home.pedidoPage = "Balcao";
                AddPedidos.Id = 0;
                AddPedidos.PDV = false;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            btnDelivery.Click += (s, e) =>
            {
                Home.pedidoPage = "Delivery";
                AddPedidos.Id = 0;
                AddPedidos.PDV = false;
                var novoPedido = new AddPedidos();
                novoPedido.ShowDialog();
            };

            VendasRel.Click += (s, e) =>
            {
                Home.pedidoPage = "Food";
                OpenForm.Show<Pedidos>(this);
            };

            btnCadastrarMesa.Click += (s, e) => { OpenForm.Show<Mesas>(this); };

            Mesas.Click += (s, e) => { OpenForm.Show<Comercial.Mesas>(this); };
        }
    }
}
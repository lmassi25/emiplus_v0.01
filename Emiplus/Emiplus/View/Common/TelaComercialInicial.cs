using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;

namespace Emiplus.View.Common
{
    public partial class TelaComercialInicial : Form
    {
        public static string page { get; set; }
        public TelaComercialInicial()
        {
            InitializeComponent();
        }

        private void Clientes_Click(object sender, System.EventArgs e)
        {
            page = "Clientes";
            OpenForm.Show<Comercial.Clientes>(this);
        }

        private void Transportadoras_Click(object sender, System.EventArgs e)
        {
            page = "Transportadoras";
            OpenForm.Show<Comercial.Clientes>(this);
        }

        private void Fornecedores_Click(object sender, System.EventArgs e)
        {
            page = "Fornecedores";
            OpenForm.Show<Comercial.Clientes>(this);
        }

        private void NovoPedido_Click(object sender, System.EventArgs e)
        {
            Pedidos Pedidos = new Pedidos();
            Pedidos.ShowDialog();
        }
    }
}

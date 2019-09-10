using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalClientes : Form
    {

        public static int Id { get; set; }
        public static string page { get; set; }

        private Controller.Pedido _controller = new Controller.Pedido();

        public PedidoModalClientes()
        {
            InitializeComponent();
        }

        private void PedidoModalClientes_Load(object sender, EventArgs e)
        {
        }

        private void DataTable()
        {
            _controller.GetDataTableClients(GridListaClientes, search.Text);
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            DataTable();
        }

        private void Search_Enter(object sender, EventArgs e)
        {
            DataTable();
        }

        private void NovoCliente_Click(object sender, EventArgs e)
        {
            Id = 0;
            page = "Clientes";
            AddClientes f = new AddClientes();
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

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
            f.btnSalvarText = "Salvar e Inserir";
            f.btnSalvarWidth = 150;
            f.btnSalvarLocation = 590;
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog() == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                Id = AddClientes.Id; // Retorna ID do registro de cliente
                Close();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSelecionar_Click(object sender, EventArgs e)
        {
            if (GridListaClientes.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Id = Convert.ToInt32(GridListaClientes.SelectedRows[0].Cells["ID"].Value);
                Close();
            }
        }
    }
}

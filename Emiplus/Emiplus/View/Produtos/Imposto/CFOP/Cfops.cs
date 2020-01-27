using System;
using System.Windows.Forms;

namespace Emiplus.View.Produtos.Imposto.CFOP
{
    public partial class Cfops : Form
    {
        public static int Id { get; set; }
        public static string page { get; set; }

        private Controller.Pedido _controller = new Controller.Pedido();

        public Cfops()
        {
            InitializeComponent();
        }

        private void DataTable()
        {
            //_controller.GetDataTableClients(GridListaCfops, search.Text);
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            DataTable();
        }

        private void Search_Enter(object sender, EventArgs e)
        {
            DataTable();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSelecionar_Click(object sender, EventArgs e)
        {
            if (GridListaCfops.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Id = Convert.ToInt32(GridListaCfops.SelectedRows[0].Cells["ID"].Value);
                Close();
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
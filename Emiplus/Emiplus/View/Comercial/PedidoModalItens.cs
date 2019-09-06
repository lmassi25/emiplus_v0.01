using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalItens : Form
    {
        private string searchItemTexto = AddPedidos.searchItemTexto;
        private Controller.Item _controllerItem = new Controller.Item();

        public PedidoModalItens()
        {
            InitializeComponent();

            buscarProduto.Text = searchItemTexto;
        }

        private void DataTable()
        {
            _controllerItem.GetDataTable(GridListaProdutos, buscarProduto.Text);
        }

        private void BuscarProduto_TextChanged(object sender, EventArgs e)
        {
            DataTable();
        }

        private void SearchItem_Click(object sender, EventArgs e)
        {
            DataTable();
        }

        private void GridListaProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
            }
        }

        private void BuscarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GridListaProdutos.Focus();
            }
        }

        private void PedidoModalItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("ola");
            }
        }
    }
}

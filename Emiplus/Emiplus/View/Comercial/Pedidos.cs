using Emiplus.Data.Helpers;
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
    public partial class Pedidos : Form
    {
        public Pedidos()
        {
            InitializeComponent();
        }

        private void Pedidos_Load(object sender, EventArgs e)
        {
        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Produto_Click(object sender, EventArgs e)
        {
            var BackColor = Color.FromArgb(249, 249, 249);
            panelTwo.BackColor = BackColor;
            label4.BackColor = BackColor;
            label5.BackColor = BackColor;
            label6.BackColor = BackColor;
            label7.BackColor = BackColor;
            label8.BackColor = BackColor;
            label9.BackColor = BackColor;
           addProduto.BackColor = BackColor;
            button3.BackColor = BackColor;
            panel3.BackColor = Color.White;
            panel2.BackColor = Color.White;
        }

        private void Produto_Leave(object sender, EventArgs e)
        {
            var BackColor = Color.White;
            panelTwo.BackColor = Color.White;
            label4.BackColor = BackColor;
            label5.BackColor = BackColor;
            label6.BackColor = BackColor;
            label7.BackColor = BackColor;
            label8.BackColor = BackColor;
            label9.BackColor = BackColor;
            addProduto.BackColor = BackColor;
            button3.BackColor = BackColor;
            panel3.BackColor = Color.FromArgb(249, 249, 249);
            panel2.BackColor = Color.FromArgb(249, 249, 249);
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            OpenForm.Show<PedidoPagamentos>(this);
        }

        private void GridListaProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

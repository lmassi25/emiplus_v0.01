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

        private void Cliente_Click(object sender, EventArgs e)
        {
            var BackColor = Color.FromArgb(249, 249, 249); 
            panelOne.BackColor = BackColor;
            label1.BackColor = BackColor;
            pictureBox2.BackColor = BackColor;
        }

        private void Cliente_Leave(object sender, EventArgs e)
        {
            var BackColor = Color.White;
            panelOne.BackColor = Color.White;
            label1.BackColor = BackColor;
            pictureBox2.BackColor = BackColor;
        }

        private void Produto_Click(object sender, EventArgs e)
        {
            var BackColor = Color.FromArgb(249, 249, 249);
            panelTwo.BackColor = BackColor;
            pictureBox1.BackColor = BackColor;
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
            pictureBox1.BackColor = BackColor;
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

        private void PedidoSimples_Click(object sender, EventArgs e)
        {
            panelOne.Visible = false;

        }
    }
}

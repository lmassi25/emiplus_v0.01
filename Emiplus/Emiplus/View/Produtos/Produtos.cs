using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

// Biblioteca para mover tela

namespace Emiplus.View.Produtos
{
    public partial class Produtos : Form
    {
        public Produtos()
        {
            InitializeComponent();
        }

        private void AdicionarProduto_Click(object sender, EventArgs e)
        {
            OpenForm.Show<AddProduct>(this);
        }

        private void ExitForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

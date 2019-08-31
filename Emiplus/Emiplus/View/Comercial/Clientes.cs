using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

namespace Emiplus.View.Comercial
{
    public partial class Clientes : Form
    {
        private string page = TelaComercialInicial.page;
        public Clientes()
        {
            InitializeComponent();
            label1.Text = page + ":";
            label6.Text = page;
            Console.WriteLine(page);
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            OpenForm.Show<AddClientes>(this);
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class Pedido : Form
    {
        public static int Id { get; set; }

        public Pedido()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            Id = 0;
            AddPedidos NovoPedido = new AddPedidos();
            NovoPedido.ShowDialog();
        }
    }
}

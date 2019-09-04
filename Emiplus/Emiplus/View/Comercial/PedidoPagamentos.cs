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
    public partial class PedidoPagamentos : Form
    {
        public PedidoPagamentos()
        {
            InitializeComponent();

        }

        private void PedidoPagamentos_Load(object sender, EventArgs e)
        {
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PedidoPagamentos_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Debito_Click(object sender, EventArgs e)
        {
            PedidoPayCartao Cartao = new PedidoPayCartao();
            Cartao.ShowDialog();
        }

        private void Credito_Click(object sender, EventArgs e)
        {
            PedidoPayParcelado Crediario = new PedidoPayParcelado();
            Crediario.ShowDialog();
        }

        private void Dinheiro_Click(object sender, EventArgs e)
        {
            PedidoPayDinheiro Dinheiro = new PedidoPayDinheiro();
            Dinheiro.ShowDialog();
        }
    }
}

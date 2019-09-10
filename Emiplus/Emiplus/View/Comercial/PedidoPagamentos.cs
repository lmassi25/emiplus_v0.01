using System;
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
            PedidoPayCartao Cartao = new PedidoPayCartao();
            Cartao.ShowDialog();
        }

        private void Dinheiro_Click(object sender, EventArgs e)
        {
            PedidoPayDinheiro Dinheiro = new PedidoPayDinheiro();
            Dinheiro.ShowDialog();
        }

        private void Desconto_Click(object sender, EventArgs e)
        {
            PedidoPayDesconto Desconto = new PedidoPayDesconto();
            Desconto.ShowDialog();
        }

        private void Acrescimo_Click(object sender, EventArgs e)
        {
            PedidoPayAcrescimo Acrescimo = new PedidoPayAcrescimo();
            Acrescimo.ShowDialog();
        }

        private void Boleto_Click(object sender, EventArgs e)
        {
            PedidoPayParcelado Crediario = new PedidoPayParcelado();
            Crediario.ShowDialog();
        }

        private void Crediario_Click(object sender, EventArgs e)
        {
            PedidoPayParcelado Crediario = new PedidoPayParcelado();
            Crediario.ShowDialog();
        }
    }
}

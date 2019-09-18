using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;
using SqlKata;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPagamentos : Form
    {
        public static int IdPedido = AddPedidos.Id;

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();

        // Dinheiro = 1
        // Cheque = 2
        // Cartão de débito = 3
        // Cartão de crédito = 4
        // Crédario = 5
        // Boleto = 6

        public PedidoPagamentos()
        {
            InitializeComponent();

            Dinheiro.Focus();
            Dinheiro.Select();
            Dinheiro.KeyDown += KeyDowns;
            Cheque.KeyDown += KeyDowns;
            Debito.KeyDown += KeyDowns;
            Credito.KeyDown += KeyDowns;
            Crediario.KeyDown += KeyDowns;
            Boleto.KeyDown += KeyDowns;
            Desconto.KeyDown += KeyDowns;
            Acrescimo.KeyDown += KeyDowns;

            btnClose.KeyDown += KeyDowns;
            btnCFeSat.KeyDown += KeyDowns;
            btnNfe.KeyDown += KeyDowns;
            btnImprimir.KeyDown += KeyDowns;
            btnConcluir.KeyDown += KeyDowns;
        }

        private void PedidoPagamentos_Load(object sender, EventArgs e)
        {
            Dinheiro.Select();
            Dinheiro.Focus();

            LoadTotais();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadTotais()
        {
            pagar.Text = Validation.FormatPrice(1000, true);

            pagamentos.Text = Validation.FormatPrice(1000, true);
            troco.Text = Validation.FormatPrice(1000, true);
            
            var dataPedido = _mPedido.FindById(IdPedido).First();
            total.Text = Validation.FormatPrice(Validation.ConvertToDouble(dataPedido.TOTAL), true);
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

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    Dinheiro_Click(sender, e);
                    break;
                case Keys.F2:
                    //BuscarProduto.Focus();
                    break;
                case Keys.F3:
                    Debito_Click(sender, e);
                    break;
                case Keys.F4:
                    Credito_Click(sender, e);
                    break;
                case Keys.F5:
                    Crediario_Click(sender, e);
                    break;
                case Keys.F6:
                    Boleto_Click(sender, e);
                    break;
                case Keys.F7:
                    Desconto_Click(sender, e);
                    break;
                case Keys.F8:
                    Acrescimo_Click(sender, e);
                    break;
                case Keys.F9:
                    //TelaPagamentos();
                    break;
                case Keys.F10:
                    //TelaPagamentos();
                    break;
                case Keys.F11:
                    //TelaPagamentos();
                    break;
                case Keys.F12:
                    //TelaPagamentos();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}

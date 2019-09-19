using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;
using SqlKata;
using SqlKata.Execution;
using System.Threading;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPagamentos : Form
    {
        public static int atualiza { get; set; }
        public static int IdPedido = AddPedidos.Id;

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();

        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

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
            
            KeyDown += KeyDowns;
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

            panelTelaDinheiro.btnFaltando.Text = $"[Enter] {Validation.FormatPrice(_controllerTitulo.GetRestante(IdPedido))} (Faltando)";
            panelTelaDinheiro.btnFaltando.Click += (s, e) =>
            {
                panelTelaDinheiro.valor.Text = _controllerTitulo.GetRestante(IdPedido).ToString();
                panelTelaDinheiro.btnFaltando.Text = "[Enter] 00,00 (Faltando)";
            };

            panelTelaDinheiro.btnSalvar.Click += (s, e) => {
                panelTelaDinheiro.AddPagamento();
                AtualizarDados();
            };
        }

        private void PedidoPagamentos_Load(object sender, EventArgs e)
        {
            Dinheiro.Select();
            Dinheiro.Focus();

            AtualizarDados();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void AtualizarDados()
        {
            _controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido);

            pagamentos.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(IdPedido), true);
            total.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(IdPedido), true);
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
            panelTelaDinheiro.Visible = true;
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
            if (Crediario.ShowDialog() == DialogResult.OK)
            {
                //GetTitulos();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    panelTelaDinheiro.valor.Text = "2,00";
                    break;
                case Keys.B:
                    panelTelaDinheiro.valor.Text = "5,00";
                    break;
                case Keys.C:
                    panelTelaDinheiro.valor.Text = "10,00";
                    break;
                case Keys.D:
                    panelTelaDinheiro.valor.Text = "20,00";
                    break;
                case Keys.E:
                    panelTelaDinheiro.valor.Text = "50,00";
                    break;
                case Keys.F:
                    panelTelaDinheiro.valor.Text = "100,00";
                    break;
                case Keys.G:
                    panelTelaDinheiro.valor.Clear();
                    break;
                case Keys.Enter:
                    panelTelaDinheiro.AddPagamento();
                    break;
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

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while(atualiza == 0)
            {
                Thread.Sleep(10);
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            AtualizarDados();
        }

    }
}

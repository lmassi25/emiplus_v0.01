using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;
using Emiplus.View.Common;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPagamentos : Form
    {
        private int IdPedido = AddPedidos.Id;

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();

        private Controller.Titulo _controllerTitulo = new Controller.Titulo();
        
        public PedidoPagamentos()
        {
            InitializeComponent();

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

            Events();

            TelaReceber.Visible = false;
        }

        private void PedidoPagamentos_Load(object sender, EventArgs e)
        {
            AtualizarDados();
        }
        
        public void AtualizarDados()
        {
            _controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido);

            pagamentos.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(IdPedido), true);
            total.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(IdPedido), true);
        }

        private void bCancelar()
        {
            TelaReceber.Visible = false;
        }

        private void bSalvar()
        {            
            switch (lTipo.Text)
            {
                case "Dinheiro":
                    _controllerTitulo.AddPagamento(IdPedido, 1, valor.Text, iniciar.Text);
                    break;
                case "Cheque":
                    _controllerTitulo.AddPagamento(IdPedido, 2, valor.Text, iniciar.Text, parcelas.Text);
                    break;
                case "Cartão de Débito":
                    _controllerTitulo.AddPagamento(IdPedido, 3, valor.Text, iniciar.Text);
                    break;
                case "Cartão de Crédito":
                    _controllerTitulo.AddPagamento(IdPedido, 4, valor.Text, iniciar.Text, parcelas.Text);
                    break;
                case "Crediário":
                    _controllerTitulo.AddPagamento(IdPedido, 5, valor.Text, iniciar.Text, parcelas.Text);
                    break;
                case "Boleto":
                    _controllerTitulo.AddPagamento(IdPedido, 6, valor.Text, iniciar.Text, parcelas.Text);
                    break;
            }

            TelaReceber.Visible = false;
            AtualizarDados();
        }

        private void Campos(int tipo = 0)
        {
            valor.Text = "";
            parcelas.Text = "";
            iniciar.Text = "";

            if (tipo == 1)
            {
                label9.Visible = false;
                pictureBox15.Visible = false;
                parcelas.Visible = false;

                label8.Visible = false;
                pictureBox2.Visible = false;
                iniciar.Visible = false;
            }
            else
            {
                label9.Visible = true;
                pictureBox15.Visible = true;
                parcelas.Visible = true;

                label8.Visible = true;
                pictureBox2.Visible = true;
                iniciar.Visible = true;
            }

            valor.Text = Validation.FormatPrice(_controllerTitulo.GetRestante(IdPedido));
        }

        /// <summary>
        /// Eventos Click
        /// </summary>
        public void Events()
        {
            Debito.Click += (s, e) =>
            {
                TelaReceber.Visible = true;
                lTipo.Text = "Cartão de Débito";
                Campos(1);
                valor.Select();
            };

            Credito.Click += (s, e) =>
            {
                TelaReceber.Visible = true;
                lTipo.Text = "Cartão de Crédito";
                Campos(0);
                valor.Select();
            };

            Dinheiro.Click += (s, e) =>
            {
                TelaReceber.Visible = true;
                lTipo.Text = "Dinheiro";
                Campos(1);
                valor.Select();
            };

            Desconto.Click += (s, e) =>
            {
                PedidoPayDesconto Desconto = new PedidoPayDesconto();
                Desconto.ShowDialog();
            };

            Acrescimo.Click += (s, e) =>
            {
                PedidoPayAcrescimo Acrescimo = new PedidoPayAcrescimo();
                Acrescimo.ShowDialog();
            };

            Boleto.Click += (s, e) =>
            {
                TelaReceber.Visible = true;
                lTipo.Text = "Boleto";
                Campos(0);
                valor.Select();
            };

            Crediario.Click += (s, e) =>
            {
                TelaReceber.Visible = true;
                lTipo.Text = "Crediário";
                Campos(0);
                valor.Select();
            };

            Cheque.Click += (s, e) =>
            {
                TelaReceber.Visible = true;
                lTipo.Text = "Cheque";
                Campos(0);
                valor.Select();
            };

            btnCancelar.Click += (s, e) =>
            {
                bCancelar();
            };

            btnSalvar.Click += (s, e) =>
            {
                bSalvar();
            };

            btnClose.Click += (s, e) =>
            {
                Close();
            };

            iniciar.KeyPress += (s, e) =>
            {
                Eventos.MaskBirthday(s, e);
            };

            iniciar.KeyPress += (s, e) =>
            {
                Eventos.MaskBirthday(s, e);
            };

            valor.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Eventos.MaskPrice(ref txt);
            };
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.A:
                //    panelTelaDinheiro.valor.Text = "2,00";
                //    break;
                //case Keys.B:
                //    panelTelaDinheiro.valor.Text = "5,00";
                //    break;
                //case Keys.C:
                //    panelTelaDinheiro.valor.Text = "10,00";
                //    break;
                //case Keys.D:
                //    panelTelaDinheiro.valor.Text = "20,00";
                //    break;
                //case Keys.E:
                //    panelTelaDinheiro.valor.Text = "50,00";
                //    break;
                //case Keys.F:
                //    panelTelaDinheiro.valor.Text = "100,00";
                //    break;
                //case Keys.G:
                //    panelTelaDinheiro.valor.Clear();
                //    break;
                case Keys.Enter:
                    bSalvar();
                    break;
                case Keys.F1:
                    //Dinheiro_Click(sender, e);
                    break;
                case Keys.F2:
                    //BuscarProduto.Focus();
                    break;
                case Keys.F3:
                    //Debito_Click(sender, e);
                    break;
                case Keys.F4:
                    //Credito_Click(sender, e);
                    break;
                case Keys.F5:
                    //Crediario_Click(sender, e);
                    break;
                case Keys.F6:
                    //Boleto_Click(sender, e);
                    break;
                case Keys.F7:
                    //Desconto_Click(sender, e);
                    break;
                case Keys.F8:
                    //Acrescimo_Click(sender, e);
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

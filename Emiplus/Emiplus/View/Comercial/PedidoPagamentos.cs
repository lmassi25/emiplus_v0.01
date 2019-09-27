using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPagamentos : Form
    {
        private int IdPedido = AddPedidos.Id;

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Titulo _mTitulo = new Model.Titulo();

        private Controller.Titulo _controllerTitulo = new Controller.Titulo();
        
        public PedidoPagamentos()
        {
            InitializeComponent();
            Eventos();

            TelaReceber.Visible = false;
        }
        
        public void AtualizarDados()
        {
            Dinheiro.Select();

            _controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido);

            pagamentos.Text = Validation.FormatPrice(_controllerTitulo.GetTotalDesconto(IdPedido), true);
            troco.Text = Validation.FormatPrice(_controllerTitulo.GetTroco(IdPedido), true).Replace("-", "");
            pagamentos.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(IdPedido), true);
            total.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(IdPedido), true);
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
                Info1.Visible = false;
                parcelas.Visible = false;

                label8.Visible = false;
                Info2.Visible = false;
                iniciar.Visible = false;
            }
            else
            {
                label9.Visible = true;
                Info1.Visible = true;
                parcelas.Visible = true;

                label8.Visible = true;
                Info2.Visible = true;
                iniciar.Visible = true;
            }

            valor.Text = Validation.FormatPrice(_controllerTitulo.GetRestante(IdPedido));
        }

        private void JanelasRecebimento(string formaPgto)
        {
            TelaReceber.Visible = true;
            lTipo.Text = formaPgto;
            valor.Select();

            if (formaPgto == "Cartão de Débito" || formaPgto == "Dinheiro")
            {
                Campos(1);
                return;
            }

            Campos(0);
        }

        private void JanelaDesconto()
        {
            PedidoPayDesconto.idPedido = IdPedido;
            PedidoPayDesconto Desconto = new PedidoPayDesconto();
            if (Desconto.ShowDialog() == DialogResult.OK)
            {
                AtualizarDados();
            }
        }
        private void JanelaAcrescimo()
        {
            //PedidoPayAcrescimo.idPedido = IdPedido;
            PedidoPayAcrescimo Acrescimo = new PedidoPayAcrescimo();
            Acrescimo.ShowDialog();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    bSalvar();
                    break;
                case Keys.F1:
                    JanelasRecebimento("Dinheiro");
                    break;
                case Keys.F2:
                    JanelasRecebimento("Cheque");
                    break;
                case Keys.F3:
                    JanelasRecebimento("Cartão de Débito");
                    break;
                case Keys.F4:
                    JanelasRecebimento("Cartão de Crédito");
                    break;
                case Keys.F5:
                    JanelasRecebimento("Crediário");
                    break;
                case Keys.F6:
                    JanelasRecebimento("Boleto");
                    break;
                case Keys.F7:
                    JanelaDesconto();
                    break;
                case Keys.F8:
                    JanelaAcrescimo();
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
                    TelaReceber.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
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
            btnSalvar.KeyDown += KeyDowns;
            btnCancelar.KeyDown += KeyDowns;
            valor.KeyDown += KeyDowns;
            parcelas.KeyDown += KeyDowns;
            iniciar.KeyDown += KeyDowns;

            Load += (s, e) => AtualizarDados();

            Debito.Click += (s, e) => JanelasRecebimento("Cartão de Débito");
            Credito.Click += (s, e) => JanelasRecebimento("Cartão de Crédito");
            Dinheiro.Click += (s, e) => JanelasRecebimento("Dinheiro");
            Boleto.Click += (s, e) => JanelasRecebimento("Boleto");
            Crediario.Click += (s, e) => JanelasRecebimento("Crediário");
            Cheque.Click += (s, e) => JanelasRecebimento("Cheque");

            Desconto.Click += (s, e) => JanelaDesconto();
            Acrescimo.Click += (s, e) => JanelaAcrescimo();

            btnSalvar.Click += (s, e) => bSalvar();
            btnCancelar.Click += (s, e) => TelaReceber.Visible = false;

            btnClose.Click += (s, e) => Close();

            iniciar.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            iniciar.KeyPress += (s, e) => Masks.MaskBirthday(s, e);

            valor.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            GridListaFormaPgtos.CellDoubleClick += (s, e) =>
            {
                if (GridListaFormaPgtos.Columns[e.ColumnIndex].Name == "colExcluir")
                {
                    Console.WriteLine(GridListaFormaPgtos.CurrentRow.Cells[4].Value);
                    if (Convert.ToString(GridListaFormaPgtos.CurrentRow.Cells[4].Value) != "")
                    {
                        int id = Validation.ConvertToInt32(GridListaFormaPgtos.CurrentRow.Cells[0].Value);
                        _mTitulo.Remove(id);
                        AtualizarDados();
                    }
                }
            };
        }
    }
}

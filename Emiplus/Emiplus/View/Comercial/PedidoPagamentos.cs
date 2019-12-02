using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Fiscal.TelasNota;
using SqlKata.Execution;
using System;
using System.Windows.Forms;
using System.Linq;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPagamentos : Form
    {
        private int IdPedido = AddPedidos.Id;
        public static bool hideFinalizar { get; set; } = false;

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Titulo _mTitulo = new Model.Titulo();
        private bool TelaESC { get; set; }

        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

        private Controller.Fiscal _controllerFiscal = new Controller.Fiscal();

        public PedidoPagamentos()
        {
            InitializeComponent();

            Eventos();

            TelaReceber.Visible = false;

            if (Home.pedidoPage == "Orçamentos")
            {
                label13.Text = $"Dados do Orçamento: {IdPedido}";
                label10.Text = "Siga as etapas abaixo para criar um orçamento!";
            }
            else if (Home.pedidoPage == "Consignações")
            {
                label13.Text = $"Dados da Consignação: {IdPedido}";
                label10.Text = "Siga as etapas abaixo para criar uma consignãção!";
            }
            else if (Home.pedidoPage == "Devoluções")
            {
                label13.Text = $"Dados da Devolução: {IdPedido}";
                label10.Text = "Siga as etapas abaixo para criar uma devolução!";
            }
            else
            {
                label13.Text = $"Dados da Venda: {IdPedido}";
                label10.Text = "Siga as etapas abaixo para adicionar uma venda!";
            }

            if (Home.pedidoPage == "Compras")
            {
                label13.Text = $"Dados da Compra: {IdPedido}";
                label10.Text = "Siga as etapas abaixo para adicionar uma compra!";

                label5.Text = "Pagamentos";
                enviarEmail.Visible = false;
                btnNfe.Visible = false;
                button21.Visible = false;
                btnCFeSat.Visible = false;
                button22.Visible = false;
            }
            else
                hideFinalizar = false;

            if (hideFinalizar)
            {
                btnConcluir.Visible = false;
                button19.Visible = false;
                btnImprimir.Left = 835;
                button20.Left = 830;
            }           
        }

        public void AtualizarDados()
        {
            _controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido);

            discount.Text = Validation.FormatPrice(_controllerTitulo.GetTotalDesconto(IdPedido), true);
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

        public void Concluir()
        {
            Model.Pedido Pedido = _mPedido.FindById(IdPedido).First<Model.Pedido>();
            Pedido.Id = IdPedido;
            if (_mPedido.Total < _controllerTitulo.GetLancados(IdPedido))
            {
                //AlertOptions.Message("Atenção!", "Total da venda é diferente do total recebido. Verifique os lançamentos.", AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                //return;
                Pedido.status = 2; //RECEBIMENTO PENDENTE
            }
            else
            {
                Pedido.status = 1; //FINALIZADO\RECEBIDO
            }
            Pedido.Save(Pedido);

            Alert.Message("Pronto!", "Finalizado com sucesso.", Alert.AlertType.success);

            AddPedidos.btnFinalizado = true;

            if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
            {
                PedidoImpressao print = new PedidoImpressao();
                print.Print(IdPedido);
            }

            Application.OpenForms["AddPedidos"].Close();
            Close();
        }

        public void Nfe()
        {
            var checkNota = new Model.Nota().FindByIdPedido(IdPedido).Get();

            if (checkNota.Count() == 0)
            {
                OpcoesNfe.idPedido = IdPedido;
                OpcoesNfe f = new OpcoesNfe();
                f.Show();
            }

            foreach (var item in checkNota)
            {
                if (item.STATUS == null)
                {
                    OpcoesNfe.idPedido = IdPedido;
                    OpcoesNfe f = new OpcoesNfe();
                    f.Show();
                }
                else
                {
                    OpcoesNfeRapida.idPedido = IdPedido;
                    OpcoesNfeRapida f = new OpcoesNfeRapida();
                    f.Show();
                }
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    bSalvar();
                    break;
                case Keys.F1:
                    TelaESC = true;
                    JanelasRecebimento("Dinheiro");
                    break;
                case Keys.F2:
                    TelaESC = true;
                    JanelasRecebimento("Cheque");
                    break;
                case Keys.F3:
                    TelaESC = true;
                    JanelasRecebimento("Cartão de Débito");
                    break;
                case Keys.F4:
                    TelaESC = true;
                    JanelasRecebimento("Cartão de Crédito");
                    break;
                case Keys.F5:
                    TelaESC = true;
                    JanelasRecebimento("Crediário");
                    break;
                case Keys.F6:
                    TelaESC = true;
                    JanelasRecebimento("Boleto");
                    break;
                case Keys.F7:

                    break;
                case Keys.F8:

                    break;
                case Keys.F9:

                    break;
                case Keys.F10:
                    Nfe();
                    break;
                case Keys.F11:
                    PedidoImpressao print = new PedidoImpressao();
                    print.Print(IdPedido);
                    break;
                case Keys.F12:
                    Concluir();
                    break;
                case Keys.Escape:
                    if (TelaESC)
                    {
                        TelaReceber.Visible = false;
                        TelaESC = false;
                    }
                    else
                        Close();
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

            //btnClose.KeyDown += KeyDowns;

            btnCFeSat.KeyDown += (s, e) =>
            {
                KeyDowns(s, e);
                MessageBox.Show(_controllerFiscal.Emitir(357, "CFe"));
            };

            btnNfe.KeyDown += KeyDowns;
            btnImprimir.KeyDown += KeyDowns;
            btnConcluir.KeyDown += KeyDowns;
            btnSalvar.KeyDown += KeyDowns;
            btnCancelar.KeyDown += KeyDowns;
            valor.KeyDown += KeyDowns;
            parcelas.KeyDown += KeyDowns;
            iniciar.KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                AtualizarDados();

                Dinheiro.Focus();
                Dinheiro.Select();

                if (AddPedidos.telapagamentos)
                {
                    Application.OpenForms["AddPedidos"].Close();
                    Application.OpenForms["PedidoPagamentos"].Focus();
                    AddPedidos.telapagamentos = false;
                }
            };            

            btnImprimir.Click += (s, e) =>
            {
                PedidoImpressao print = new PedidoImpressao();
                print.Print(IdPedido);
            };

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

            btnClose.Click += (s, e) =>
            {
                AddPedidos.Id = IdPedido;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();

                Close();
            };

            btnConcluir.Click += (s, e) =>
            {
                Concluir();
            };
            
            iniciar.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            iniciar.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            valor.KeyPress += (s, e) => Masks.MaskDouble(s, e);

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

            btnNfe.Click += (s, e) =>
            {
                Nfe();
            };

            FormClosing += (s, e) =>
            {
                AddPedidos.btnFinalizado = false;
            };
        }

        private void PedidoPagamentos_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Ativo: PedidoPagamentos");            
        }
    }
}

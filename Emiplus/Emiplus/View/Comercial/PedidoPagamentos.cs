using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Fiscal.TelasNota;
using SqlKata.Execution;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

        private int CaixaAnterior;

        public PedidoPagamentos()
        {
            InitializeComponent();

            Eventos();

            Resolution.SetScreenMaximized(this);

            AddPedidos.btnFinalizado = false;
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
            else if (Home.pedidoPage == "Compras")
            {
                label13.Text = $"Dados da Compra: {IdPedido}";
                label10.Text = "Siga as etapas abaixo para adicionar uma compra!";

                label15.Text = "Á Pagar";
                label1.Text = "Pagamentos";
                //enviarEmail.Visible = false;
                btnNfe.Visible = false;
                button21.Visible = false;
                btnCFeSat.Visible = false;
                button22.Visible = false;
            }
            else
            {
                label13.Text = $"Dados da Venda: {IdPedido}";
                label10.Text = "Siga as etapas abaixo para adicionar uma venda!";
            }

            if (hideFinalizar)
            {
                btnCFeSat.Visible = false;
                button22.Visible = false;

                btnNfe.Visible = false;
                button21.Visible = false;

                btnConcluir.Visible = false;
                button19.Visible = false;

                //btnImprimir.Left = 835;
                //button20.Left = 830;
                btnImprimir.Visible = false;
                button20.Visible = false;
            }
        }

        public void AtualizarDados()
        {
            _mPedido = _mPedido.FindById(IdPedido).FirstOrDefault<Model.Pedido>();
            _controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido);

            dynamic devolucoes = _mPedido.Query()
                .SelectRaw("SUM(total) as total")
                .Where("excluir", 0)
                .Where("tipo", "Devoluções")
                .Where("Venda", IdPedido)
                .FirstOrDefault<Model.Pedido>();

            acrescimos.Text = Validation.FormatPrice(_controllerTitulo.GetTotalFrete(IdPedido), true);
            discount.Text = Validation.FormatPrice((_controllerTitulo.GetTotalDesconto(IdPedido) + Validation.ConvertToDouble(devolucoes.Total)), true);
            troco.Text = Validation.FormatPrice(_controllerTitulo.GetTroco(IdPedido), true).Replace("-", "");
            pagamentos.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(IdPedido), true);
            total.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(IdPedido), true);

            var aPagar = _controllerTitulo.GetTotalPedido(IdPedido) - _controllerTitulo.GetLancados(IdPedido);
            if (_controllerTitulo.GetLancados(IdPedido) < _controllerTitulo.GetTotalPedido(IdPedido)) {
                aPagartxt.Text = Validation.FormatPrice(aPagar, true);
            } else
            {
                aPagartxt.Text = "R$ 0,00";
            }

            if (_controllerTitulo.GetLancados(IdPedido) > 0)
                Desconto.Enabled = false;
            else
                Desconto.Enabled = true;

            if (aPagar == 0)
            {
                label15.BackColor = Color.FromArgb(46, 204, 113);
                aPagartxt.BackColor = Color.FromArgb(46, 204, 113);
                visualPanel1.BackColorState.Enabled = Color.FromArgb(46, 204, 113);
                visualPanel1.Border.Color = Color.FromArgb(39, 192, 104);
                this.Refresh();
            }
            else
            {
                label15.BackColor = Color.FromArgb(255, 40, 81);
                aPagartxt.BackColor = Color.FromArgb(255, 40, 81);
                visualPanel1.BackColorState.Enabled = Color.FromArgb(255, 40, 81);
                visualPanel1.Border.Color = Color.FromArgb(241, 33, 73);
                this.Refresh();
            }
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

            if (CaixaAnterior > 0)
                Home.idCaixa = CaixaAnterior;

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

            //dynamic devolucoes = _mPedido.Query()
            //    .SelectRaw("SUM(total) as total")
            //    .Where("excluir", 0)
            //    .Where("tipo", "Devoluções")
            //    .Where("Venda", IdPedido)
            //    .FirstOrDefault<Model.Pedido>();

            valor.Text = Validation.FormatPrice(_controllerTitulo.GetRestante(IdPedido));
        }

        private void JanelasRecebimento(string formaPgto)
        {
            if (!CheckCaixa())
                return;

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
            if (!CheckCaixa())
                return;

            PedidoPayDesconto.idPedido = IdPedido;
            using (PedidoPayDesconto Desconto = new PedidoPayDesconto()) {
                Desconto.TopMost = true;
                if (Desconto.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private void JanelaAcrescimo()
        {
            if (!CheckCaixa())
                return;

            PedidoPayAcrescimo.idPedido = IdPedido;
            using (PedidoPayAcrescimo Acrescimo = new PedidoPayAcrescimo()) {
                Acrescimo.TopMost = true;
                if (Acrescimo.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private void JanelaDevolucao()
        {
            if (!CheckCaixa())
                return;

            PedidoPayDevolucao.idPedido = IdPedido;
            using (PedidoPayDevolucao f = new PedidoPayDevolucao()) {
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private bool CheckCaixa()
        {
            if (Home.idCaixa == 0)
            {
                Alert.Message("Ação não permitida", "É necessário abrir ou vincular um caixa para continuar", Alert.AlertType.warning);
                return false;
            }
            else
            {
                CaixaAnterior = Home.idCaixa;
                if (_mPedido.Id_Caixa > 0)
                    Home.idCaixa = _mPedido.Id_Caixa;
            }

            return true;
        }

        /// <summary>
        /// True para imprimir, false para não imprimir
        /// </summary>
        public void Concluir(bool imprimir = true)
        {
            if (Home.pedidoPage == "Vendas" && aPagartxt.Text != "R$ 0,00")
            {
                Alert.Message("Ação não permitida", "É necessário informar recebimentos para finalizar", Alert.AlertType.warning);
                return;
            }

            Model.Pedido Pedido = _mPedido.FindById(IdPedido).FirstOrDefault<Model.Pedido>();
            if (Pedido != null)
            {
                Pedido.Id = IdPedido;

                if (_controllerTitulo.GetLancados(IdPedido) < Pedido.Total)
                    Pedido.status = 2; //RECEBIMENTO PENDENTE
                else
                    Pedido.status = 1; //FINALIZADO\RECEBIDO

                if (Pedido.Save(Pedido))
                {
                    Alert.Message("Pronto!", "Finalizado com sucesso.", Alert.AlertType.success);
                    AddPedidos.btnFinalizado = true;
                }
            }

            if (Home.pedidoPage == "Compras")
            {
                imprimir = false;
                Application.OpenForms["AddPedidos"].Close();
                Close();
            }

            if (imprimir)
            {
                if (AlertOptions.Message("Impressão!", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                    new Controller.Pedido().Imprimir(IdPedido);

                try
                {
                    Application.OpenForms["AddPedidos"].Close();

                    if (IniFile.Read("RetomarVenda", "Comercial") == "True")
                    {
                        AddPedidos.Id = 0;
                        AddPedidos reopen = new AddPedidos();
                        reopen.TopMost = true;
                        reopen.Show();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                DialogResult = DialogResult.OK;
                Application.OpenForms["PedidoPagamentos"].Close();
            }
        }

        public void Nfe()
        {
            if (Home.pedidoPage == "Vendas" && aPagartxt.Text != "R$ 0,00")
            {
                Alert.Message("Ação não permitida", "É necessário informar recebimentos para finalizar", Alert.AlertType.warning);
                return;
            }

            Concluir(false);

            OpcoesNfeRapida.idPedido = IdPedido;
            OpcoesNfeRapida.idNota = 0;
            OpcoesNfeRapida f = new OpcoesNfeRapida();
            f.TopMost = true;
            f.Show();
        }

        public void Cfe()
        {
            if (Home.pedidoPage == "Vendas" && aPagartxt.Text != "R$ 0,00")
            {
                Alert.Message("Ação não permitida", "É necessário informar recebimentos para continuar", Alert.AlertType.warning);
                return;
            }

            Concluir(false);

            OpcoesCfe.idNota = 0;

            var checkNota = new Model.Nota().FindByIdPedidoUltReg(IdPedido, "", "CFe").FirstOrDefault<Model.Nota>();
            if (checkNota == null)
            {
                Model.Nota _modelNota = new Model.Nota();

                _modelNota.Id = 0;
                _modelNota.Tipo = "CFe";
                _modelNota.Status = "Pendente";
                _modelNota.id_pedido = IdPedido;
                _modelNota.Save(_modelNota, false);
                checkNota = _modelNota;
            }

            if (checkNota != null)
                OpcoesCfe.idNota = checkNota.Id;
            else
                return;

            if (checkNota.Status == "Autorizada" || checkNota.Status == "Autorizado")
            {
                OpcoesCfe.idPedido = IdPedido;
                OpcoesCfe f = new OpcoesCfe();
                f.TopMost = true;
                f.Show();
                
            }
            else if (checkNota.Status == "Cancelada" || checkNota.Status == "Cancelado")
            {
                var result = AlertOptions.Message("Atenção!", "Existem registro(s) de cupon(s) cancelado(s) a partir desta venda. Deseja gerar um novo cupom?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    Model.Nota _modelNota = new Model.Nota();

                    _modelNota.Id = 0;
                    _modelNota.Tipo = "CFe";
                    _modelNota.Status = "Pendente";
                    _modelNota.id_pedido = IdPedido;
                    _modelNota.Save(_modelNota, false);

                    checkNota = _modelNota;

                    OpcoesCfeEmitir.fecharTelas = true;

                    OpcoesCfeCpf.idPedido = IdPedido;
                    OpcoesCfeCpf.emitir = true;
                    OpcoesCfeCpf f = new OpcoesCfeCpf();
                    f.TopMost = true;
                    f.Show();
                }
            }
            else if (checkNota.Status == "Pendente")
            {
                OpcoesCfeEmitir.fecharTelas = true;

                OpcoesCfeCpf.idPedido = IdPedido;
                OpcoesCfeCpf.emitir = true;
                OpcoesCfeCpf f = new OpcoesCfeCpf();
                f.TopMost = true;
                f.Show();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            if (!CheckCaixa())
                return;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    bSalvar();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F1:
                    TelaESC = true;
                    JanelasRecebimento("Dinheiro");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F2:
                    TelaESC = true;
                    JanelasRecebimento("Cheque");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F3:
                    TelaESC = true;
                    JanelasRecebimento("Cartão de Débito");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F4:
                    TelaESC = true;
                    JanelasRecebimento("Cartão de Crédito");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F5:
                    TelaESC = true;
                    JanelasRecebimento("Crediário");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F6:
                    TelaESC = true;
                    JanelasRecebimento("Boleto");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F7:

                    break;

                case Keys.F8:

                    break;

                case Keys.F9:
                    Cfe();
                    break;

                case Keys.F10:

                    if (!Support.CheckForInternetConnection())
                    {
                        Alert.Message("Opps", "Você está sem conexão com a internet.", Alert.AlertType.warning);
                        return;
                    }

                    Nfe();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F11:
                    new Controller.Pedido().Imprimir(IdPedido);
                    //new PedidoImpressao().Print(IdPedido);
                    break;

                case Keys.F12:
                    Concluir();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Escape:
                    if (TelaESC)
                    {
                        TelaReceber.Visible = false;
                        TelaESC = false;
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        AddPedidos.btnVoltar = true;
                        Close();
                    }
                    break;
            }
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            Load += (s, e) =>
            {
                AtualizarDados();

                AddPedidos.btnFinalizado = false;

                Dinheiro.Focus();
                Dinheiro.Select();
            };

            FormClosing += (s, e) =>
            {
                if (AddPedidos.btnFinalizado)
                {
                    try
                    {
                        Application.OpenForms["AddPedidos"].Close();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            };

            KeyDown += KeyDowns;
            Dinheiro.KeyDown += KeyDowns;
            Cheque.KeyDown += KeyDowns;
            Debito.KeyDown += KeyDowns;
            Credito.KeyDown += KeyDowns;
            Crediario.KeyDown += KeyDowns;
            Boleto.KeyDown += KeyDowns;
            Desconto.KeyDown += KeyDowns;
            Acrescimo.KeyDown += KeyDowns;
            GridListaFormaPgtos.KeyDown += KeyDowns;

            btnCFeSat.KeyDown += (s, e) =>
            {
                if (UserPermission.SetControl(btnCFeSat, pictureBox1, "fiscal_emissaocfe"))
                    return;

                KeyDowns(s, e);
            };

            btnNfe.KeyDown += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    Alert.Message("Opps", "Você está sem conexão com a internet.", Alert.AlertType.warning);
                    return;
                }

                if (UserPermission.SetControl(btnNfe, pictureBox6, "fiscal_emissaonfe"))
                    return;

                KeyDowns(s, e);
            };

            btnImprimir.KeyDown += KeyDowns;
            btnConcluir.KeyDown += KeyDowns;
            btnSalvar.KeyDown += KeyDowns;
            btnCancelar.KeyDown += KeyDowns;
            valor.KeyDown += KeyDowns;
            parcelas.KeyDown += KeyDowns;
            iniciar.KeyDown += KeyDowns;

            btnImprimir.Click += (s, e) =>
            {
                new Controller.Pedido().Imprimir(IdPedido);
            };

            Debito.Click += (s, e) => JanelasRecebimento("Cartão de Débito");
            Credito.Click += (s, e) => JanelasRecebimento("Cartão de Crédito");
            Dinheiro.Click += (s, e) => JanelasRecebimento("Dinheiro");
            Boleto.Click += (s, e) => JanelasRecebimento("Boleto");
            Crediario.Click += (s, e) => JanelasRecebimento("Crediário");
            Cheque.Click += (s, e) => JanelasRecebimento("Cheque");

            Desconto.Click += (s, e) => JanelaDesconto();
            Acrescimo.Click += (s, e) => JanelaAcrescimo();
            Devolucao.Click += (s, e) => JanelaDevolucao();

            btnSalvar.Click += (s, e) => bSalvar();
            btnCancelar.Click += (s, e) => TelaReceber.Visible = false;

            btnClose.Click += (s, e) =>
            {
                AddPedidos.btnVoltar = true;
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
                    if (Convert.ToString(GridListaFormaPgtos.CurrentRow.Cells[4].Value) != "")
                    {
                        int id = Validation.ConvertToInt32(GridListaFormaPgtos.CurrentRow.Cells[0].Value);
                        _mTitulo.Remove(id);
                        AtualizarDados();
                    }
                }
            };

            btnClearRecebimentos.Click += (s, e) =>
            {
                foreach (DataGridViewRow row in GridListaFormaPgtos.Rows)
                {
                    if (Convert.ToString(row.Cells[0].Value) != "")
                    {
                        _mTitulo.Remove(Validation.ConvertToInt32(row.Cells[0].Value), "ID", false);
                    }
                }

                AtualizarDados();
            };

            btnNfe.Click += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    Alert.Message("Opps", "Você está sem conexão com a internet.", Alert.AlertType.warning);
                    return;
                }

                if (UserPermission.SetControl(btnNfe, pictureBox6, "fiscal_emissaonfe"))
                    return;

                Nfe();
            };

            btnCFeSat.Click += (s, e) =>
            {
                if (UserPermission.SetControl(btnCFeSat, pictureBox1, "fiscal_emissaocfe"))
                    return;

                Cfe();
            };
        }
    }
}
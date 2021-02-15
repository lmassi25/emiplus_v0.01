using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Financeiro;
using Emiplus.View.Fiscal.TelasNota;
using SqlKata.Execution;
using Nota = Emiplus.Model.Nota;
using PedidoItem = Emiplus.Model.PedidoItem;
using Taxas = Emiplus.Model.Taxas;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPagamentos : Form
    {
        private readonly Titulo _controllerTitulo = new Titulo();
        private readonly PedidoItem _mPedidoItens = new PedidoItem();
        private readonly Model.Titulo _mTitulo = new Model.Titulo();

        private readonly int idPedido = AddPedidos.Id;
        private readonly MaskedTextBox mtxt = new MaskedTextBox();
        private readonly TextBox mtxt2 = new TextBox();

        private Model.Pedido _mPedido = new Model.Pedido();

        private int caixaAnterior;

        public PedidoPagamentos()
        {
            InitializeComponent();
            Eventos();
        }

        public static bool HideFinalizar { get; set; } = false;
        private bool TelaEsc { get; set; }
        private bool Recebimentos { get; set; }

        private static bool PayVerify { get; set; }

        public void AtualizarDados(bool grid = true)
        {
            _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

            if (grid)
                _controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, idPedido);

            dynamic devolucoes = _mPedido.Query()
                .SelectRaw("SUM(total) as total")
                .Where("excluir", 0)
                .Where("tipo", "Devoluções")
                .Where("Venda", idPedido)
                .FirstOrDefault<Model.Pedido>();

            acrescimos.Text = Validation.FormatPrice(_controllerTitulo.GetTotalFrete(idPedido), true);
            discount.Text =
                Validation.FormatPrice(
                    _controllerTitulo.GetTotalDesconto(idPedido) + Validation.ConvertToDouble(devolucoes.Total), true);
            troco.Text = Validation.FormatPrice(_controllerTitulo.GetTroco(idPedido), true).Replace("-", "");
            pagamentos.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(idPedido), true);
            total.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(idPedido), true);

            var aPagar = Validation.RoundTwo(_controllerTitulo.GetTotalPedido(idPedido) -
                                             _controllerTitulo.GetLancados(idPedido));

            aPagartxt.Text = _controllerTitulo.GetLancados(idPedido) < _controllerTitulo.GetTotalPedido(idPedido)
                ? Validation.FormatPrice(aPagar, true)
                : "R$ 0,00";

            if (_controllerTitulo.GetLancados(idPedido) > 0)
            {
                Desconto.Enabled = false;
                Acrescimo.Enabled = false;
                Devolucao.Enabled = false;
            }
            else
            {
                Desconto.Enabled = true;
                Acrescimo.Enabled = true;
                Devolucao.Enabled = true;
            }

            if (aPagar <= 0)
            {
                label15.BackColor = Color.FromArgb(46, 204, 113);
                aPagartxt.BackColor = Color.FromArgb(46, 204, 113);
                visualPanel1.BackColorState.Enabled = Color.FromArgb(46, 204, 113);
                visualPanel1.Border.Color = Color.FromArgb(39, 192, 104);
                Recebimentos = false;
                Dinheiro.Enabled = false;
                Cheque.Enabled = false;
                Debito.Enabled = false;
                Credito.Enabled = false;
                Crediario.Enabled = false;
                Boleto.Enabled = false;

                Refresh();
            }
            else
            {
                label15.BackColor = Color.FromArgb(255, 40, 81);
                aPagartxt.BackColor = Color.FromArgb(255, 40, 81);
                visualPanel1.BackColorState.Enabled = Color.FromArgb(255, 40, 81);
                visualPanel1.Border.Color = Color.FromArgb(241, 33, 73);
                Recebimentos = true;
                Dinheiro.Enabled = true;
                Cheque.Enabled = true;
                Debito.Enabled = true;
                Credito.Enabled = true;
                Crediario.Enabled = true;
                Boleto.Enabled = true;

                Refresh();
            }
        }

        private void BSalvar()
        {
            switch (lTipo.Text)
            {
                case "Dinheiro":
                    _controllerTitulo.AddPagamento(idPedido, 1, valor.Text, iniciar.Text);
                    break;

                case "Cheque":
                    _controllerTitulo.AddPagamento(idPedido, 2, valor.Text, iniciar.Text, parcelas.Text);
                    break;

                case "Cartão de Débito":
                    _controllerTitulo.AddPagamento(idPedido, 3, valor.Text, iniciar.Text, "1",
                        Validation.ConvertToInt32(taxas.SelectedValue));
                    break;

                case "Cartão de Crédito":
                    _controllerTitulo.AddPagamento(idPedido, 4, valor.Text, iniciar.Text, parcelas.Text,
                        Validation.ConvertToInt32(taxas.SelectedValue));
                    break;

                case "Crediário":
                    _controllerTitulo.AddPagamento(idPedido, 5, valor.Text, iniciar.Text, parcelas.Text);
                    break;

                case "Boleto":
                    _controllerTitulo.AddPagamento(idPedido, 6, valor.Text, iniciar.Text, parcelas.Text);
                    break;
            }

            PedidoModalDividirConta.ValorDividido = 0;
            PayVerify = false;

            TelaReceber.Visible = false;

            if (caixaAnterior > 0)
                Home.idCaixa = caixaAnterior;

            _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
            if (_mPedido != null)
            {
                _mPedido.status = _controllerTitulo.GetLancados(idPedido) < Validation.Round(_mPedido.Total) ? 2 : 1;
                _mPedido.Save(_mPedido);
            }

            AtualizarDados();
        }

        private void JanelasRecebimento(string formaPgto)
        {
            TelaReceber.Refresh();

            if (!Recebimentos)
                return;

            if (!CheckCaixa())
                return;

            TelaReceber.Visible = true;
            lTipo.Text = formaPgto;
            valor.Select();

            valor.Text = "";
            parcelas.Text = "";
            iniciar.Text = "";

            switch (formaPgto)
            {
                case "Dinheiro":
                    label5.Visible = false;
                    taxas.Visible = false;

                    label9.Visible = false;
                    Info1.Visible = false;
                    parcelas.Visible = false;

                    label8.Visible = false;
                    Info2.Visible = false;
                    iniciar.Visible = false;
                    break;
                case "Cartão de Crédito":
                    label5.Visible = true;
                    taxas.Visible = true;
                    label5.Location = new Point(15, 262);
                    taxas.Location = new Point(15, 281);

                    label9.Visible = true;
                    Info1.Visible = true;
                    parcelas.Visible = true;

                    label8.Visible = true;
                    Info2.Visible = true;
                    iniciar.Visible = true;
                    break;
                case "Cartão de Débito":
                    label5.Visible = true;
                    taxas.Visible = true;
                    label5.Location = label9.Location;
                    taxas.Location = parcelas.Location;

                    label9.Visible = false;
                    Info1.Visible = false;
                    parcelas.Visible = false;

                    label8.Visible = false;
                    Info2.Visible = false;
                    iniciar.Visible = false;
                    break;
                case "Cheque":
                case "Crediário":
                case "Boleto":
                    label5.Visible = false;
                    taxas.Visible = false;

                    label9.Visible = true;
                    Info1.Visible = true;
                    parcelas.Visible = true;

                    label8.Visible = true;
                    Info2.Visible = true;
                    iniciar.Visible = true;
                    break;
            }

            valor.Text = PedidoModalDividirConta.ValorDividido <= 0
                ? Validation.FormatPrice(_controllerTitulo.GetRestante(idPedido))
                : Validation.FormatPrice(PedidoModalDividirConta.ValorDividido);
        }

        private void JanelaDesconto()
        {
            if (!CheckCaixa())
                return;

            PedidoPayDesconto.idPedido = idPedido;
            using (var desconto = new PedidoPayDesconto())
            {
                desconto.TopMost = true;
                if (desconto.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private void JanelaAcrescimo()
        {
            if (!CheckCaixa())
                return;

            PedidoPayAcrescimo.idPedido = idPedido;
            using (var acrescimo = new PedidoPayAcrescimo())
            {
                acrescimo.TopMost = true;
                if (acrescimo.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private void JanelaDevolucao()
        {
            if (!CheckCaixa())
                return;

            PedidoPayDevolucao.idPedido = idPedido;
            using (var f = new PedidoPayDevolucao())
            {
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                    AtualizarDados();
            }
        }

        private bool CheckCaixa()
        {
            if (Home.idCaixa == 0)
            {
                Alert.Message("Ação não permitida", "É necessário abrir ou vincular um caixa para continuar",
                    Alert.AlertType.warning);

                var f = new AbrirCaixa {TopMost = true};
                f.ShowDialog();

                return false;
            }

            caixaAnterior = Home.idCaixa;
            if (_mPedido.Id_Caixa > 0)
                Home.idCaixa = _mPedido.Id_Caixa;

            return true;
        }

        /// <summary>
        ///     True para imprimir, false para não imprimir
        /// </summary>
        public void Concluir(bool imprimir = true)
        {
            if (Home.pedidoPage == "Vendas" && aPagartxt.Text != @"R$ 0,00")
            {
                Alert.Message("Ação não permitida", "É necessário informar recebimentos para finalizar",
                    Alert.AlertType.warning);

                return;
            }

            var pedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
            if (pedido != null)
            {
                pedido.Id = idPedido;
                pedido.status = _controllerTitulo.GetLancados(idPedido) < pedido.Total ? 2 : 1;
                if (pedido.Save(pedido))
                    AddPedidos.BtnFinalizado = true;
            }

            if (Home.pedidoPage == "Compras")
            {
                imprimir = false;
                Application.OpenForms["AddPedidos"]?.Close();
                Close();
            }

            if (imprimir)
            {
                if (AlertOptions.Message("Impressão!", "Deseja imprimir?", AlertBig.AlertType.info,
                    AlertBig.AlertBtn.YesNo, true))
                    new Controller.Pedido().Imprimir(idPedido);

                Application.OpenForms["AddPedidos"]?.Close();

                if (IniFile.Read("RetomarVenda", "Comercial") == "True")
                {
                    AddPedidos.Id = 0;
                    var reopen = new AddPedidos {TopMost = true};
                    reopen.Show();
                }

                DialogResult = DialogResult.OK;
                Application.OpenForms["PedidoPagamentos"]?.Close();
            }
        }

        public void Nfe()
        {
            if (Home.pedidoPage == "Vendas" && aPagartxt.Text != @"R$ 0,00")
            {
                Alert.Message("Ação não permitida", "É necessário informar recebimentos para finalizar",
                    Alert.AlertType.warning);
                return;
            }

            Concluir(false);

            OpcoesNfeRapida.idPedido = idPedido;
            OpcoesNfeRapida.idNota = 0;
            var f = new OpcoesNfeRapida {TopMost = true};
            f.Show();
        }

        public void Cfe()
        {
            if (Home.pedidoPage == "Vendas" && aPagartxt.Text != @"R$ 0,00")
            {
                Alert.Message("Ação não permitida", "É necessário informar recebimentos para continuar",
                    Alert.AlertType.warning);
                return;
            }

            Concluir(false);

            OpcoesCfe.idNota = 0;

            var checkNota = new Nota().FindByIdPedidoUltReg(idPedido, "", "CFe").FirstOrDefault<Nota>();
            if (checkNota == null)
            {
                var _modelNota = new Nota
                {
                    Id = 0, 
                    Tipo = "CFe", 
                    Status = "Pendente", 
                    id_pedido = idPedido
                };
                if (!_modelNota.Save(_modelNota, false))
                {
                    Alert.Message("Ação não permitida",
                        "Problema com tabela Notas. Entre em contato com o suporte técnico!", Alert.AlertType.warning);
                    return;
                }

                checkNota = _modelNota;
            }

            if (checkNota != null)
                OpcoesCfe.idNota = checkNota.Id;
            else
                return;

            switch (checkNota.Status)
            {
                case "Falha":
                    Alert.Message("Ação não permitida", "Entre em contato com o suporte técnico", Alert.AlertType.warning);
                    break;
                case "Autorizada":
                case "Autorizado":
                {
                    OpcoesCfe.idPedido = idPedido;
                    var f = new OpcoesCfe {TopMost = true};
                    f.Show();
                    break;
                }
                case "Cancelada":
                case "Cancelado":
                {
                    var result = AlertOptions.Message("Atenção!",
                        "Existem registro(s) de cupon(s) cancelado(s) a partir desta venda. Deseja gerar um novo cupom?",
                        AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        var _modelNota = new Nota
                        {
                            Id = 0,
                            Tipo = "CFe", 
                            Status = "Pendente", 
                            id_pedido = idPedido
                        };
                        _modelNota.Save(_modelNota, false);

                        checkNota = _modelNota;

                        OpcoesCfeEmitir.fecharTelas = true;

                        OpcoesCfeCpf.idPedido = idPedido;
                        OpcoesCfeCpf.emitir = true;
                        var f = new OpcoesCfeCpf {TopMost = true};
                        f.Show();
                    }

                    break;
                }
                case "Pendente":
                {
                    OpcoesCfeEmitir.fecharTelas = true;

                    OpcoesCfeCpf.idPedido = idPedido;
                    OpcoesCfeCpf.emitir = true;
                    var f = new OpcoesCfeCpf {TopMost = true};
                    f.Show();
                    break;
                }
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            if (!CheckCaixa())
                return;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    BSalvar();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F1:
                    TelaEsc = true;
                    JanelasRecebimento("Dinheiro");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F2:
                    TelaEsc = true;
                    JanelasRecebimento("Cheque");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F3:
                    TelaEsc = true;
                    JanelasRecebimento("Cartão de Débito");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F4:
                    TelaEsc = true;
                    JanelasRecebimento("Cartão de Crédito");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F5:
                    TelaEsc = true;
                    JanelasRecebimento("Crediário");
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F6:
                    TelaEsc = true;
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
                    new Controller.Pedido().Imprimir(idPedido);
                    //new PedidoImpressao().Print(IdPedido);
                    break;

                case Keys.F12:
                    Concluir();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Escape:
                    if (TelaEsc)
                    {
                        TelaReceber.Visible = false;
                        TelaEsc = false;
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        if (_controllerTitulo.GetLancados(idPedido) > 0)
                        {
                            var text = Home.pedidoPage == "Compras" ? "pagamentos" : "recebimentos";
                            var message = AlertOptions.Message("Atenção",
                                $"Os {text} lançados serão apagados,\ndeseja continuar?", AlertBig.AlertType.warning,
                                AlertBig.AlertBtn.YesNo);
                            if (message)
                            {
                                foreach (DataGridViewRow row in GridListaFormaPgtos.Rows)
                                    if (Convert.ToString(row.Cells[0].Value) != "")
                                        _mTitulo.Remove(Validation.ConvertToInt32(row.Cells[0].Value), "ID", false);

                                AtualizarDados();

                                _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                                _mPedido.status = 2;
                                _mPedido.Save(_mPedido);

                                Close();
                            }
                            else
                            {
                                return;
                            }
                        }

                        e.SuppressKeyPress = true;
                        AddPedidos.BtnVoltar = true;
                        Close();
                    }

                    break;
            }
        }

        /// <summary>
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            Load += (s, e) =>
            {
                PayVerify = true;

                if (AddPedidos.PDV)
                {
                    // btn NF-e
                    btnNfe.Visible = false;
                    button21.Visible = false;
                    pictureBox6.Visible = false;
                }
            };

            Shown += (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                AddPedidos.BtnFinalizado = false;
                TelaReceber.Visible = false;

                switch (Home.pedidoPage)
                {
                    case "Orçamentos":
                        label13.Text = $@"Dados do Orçamento: {idPedido}";
                        label10.Text = @"Siga as etapas abaixo para criar um orçamento!";
                        break;
                    case "Consignações":
                        label13.Text = $@"Dados da Consignação: {idPedido}";
                        label10.Text = @"Siga as etapas abaixo para criar uma consignãção!";
                        break;
                    case "Devoluções":
                        label13.Text = $@"Dados da Devolução: {idPedido}";
                        label10.Text = @"Siga as etapas abaixo para criar uma devolução!";
                        break;
                    case "Compras":
                        label13.Text = $@"Dados da Compra: {idPedido}";
                        label10.Text = @"Siga as etapas abaixo para adicionar uma compra!";

                        label15.Text = @"Á Pagar";
                        label1.Text = @"Pagamentos";
                        //enviarEmail.Visible = false;
                        btnNfe.Visible = false;
                        button21.Visible = false;
                        btnCFeSat.Visible = false;
                        button22.Visible = false;
                        break;
                    default:
                        label13.Text = $@"Dados da Venda: {idPedido}";
                        label10.Text = @"Siga as etapas abaixo para adicionar uma venda!";
                        break;
                }

                if (HideFinalizar)
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

                if (IniFile.Read("Alimentacao", "Comercial") == "True")
                    btnDividir.Visible = true;

                mtxt.Visible = false;
                mtxt2.Visible = false;

                GridListaFormaPgtos.Controls.Add(mtxt);
                GridListaFormaPgtos.Controls.Add(mtxt2);

                AtualizarDados();

                AddPedidos.BtnFinalizado = false;

                Dinheiro.Focus();
                Dinheiro.Select();

                var taxasSource = new ArrayList();
                var dataTaxa = new Taxas().FindAll(new[] {"id", "nome"}).WhereFalse("excluir").Get<Taxas>();
                taxasSource.Add(new {Id = 0, Nome = "SELECIONE"});
                if (dataTaxa.Any())
                    foreach (var item in dataTaxa)
                        taxasSource.Add(new {item.Id, item.Nome});

                taxas.DataSource = taxasSource;
                taxas.DisplayMember = "Nome";
                taxas.ValueMember = "Id";
            };

            FormClosing += (s, e) =>
            {
                if (AddPedidos.BtnFinalizado) Application.OpenForms["AddPedidos"]?.Close();
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

            btnImprimir.Click += (s, e) => { new Controller.Pedido().Imprimir(idPedido); };

            Debito.Click += (s, e) => JanelasRecebimento("Cartão de Débito");
            Credito.Click += (s, e) => JanelasRecebimento("Cartão de Crédito");
            Dinheiro.Click += (s, e) => JanelasRecebimento("Dinheiro");
            Boleto.Click += (s, e) => JanelasRecebimento("Boleto");
            Crediario.Click += (s, e) => JanelasRecebimento("Crediário");
            Cheque.Click += (s, e) => JanelasRecebimento("Cheque");

            Desconto.Click += (s, e) => JanelaDesconto();
            Acrescimo.Click += (s, e) => JanelaAcrescimo();
            Devolucao.Click += (s, e) => JanelaDevolucao();

            btnSalvar.Click += (s, e) => BSalvar();
            btnCancelar.Click += (s, e) =>
            {
                PayVerify = true;
                TelaReceber.Visible = false;
            };

            btnClose.Click += (s, e) =>
            {
                if (_controllerTitulo.GetLancados(idPedido) > 0)
                {
                    var text = Home.pedidoPage == "Compras" ? "pagamentos" : "recebimentos";
                    var message = AlertOptions.Message("Atenção",
                        $"Os {text} lançados serão apagados,\n deseja continuar?", AlertBig.AlertType.warning,
                        AlertBig.AlertBtn.YesNo);
                    if (message)
                    {
                        foreach (DataGridViewRow row in GridListaFormaPgtos.Rows)
                            if (Convert.ToString(row.Cells[0].Value) != "")
                                _mTitulo.Remove(Validation.ConvertToInt32(row.Cells[0].Value), "ID", false);

                        AtualizarDados();

                        _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                        _mPedido.status = 2;
                        _mPedido.Save(_mPedido);

                        Close();
                    }
                    else
                    {
                        return;
                    }
                }

                AddPedidos.BtnVoltar = true;
                Close();
            };

            btnDividir.Click += (s, e) =>
            {
                var itens = new ArrayList();
                if (PayVerify)
                {
                    var dataItens = _mPedidoItens.FindAll().Where("pedido", idPedido).WhereFalse("excluir")
                        .Get<PedidoItem>();
                    if (dataItens.Any())
                        foreach (var item in dataItens)
                            itens.Add(new
                            {
                                item.Id,
                                item.Item,
                                item.xProd,
                                item.CEan,
                                item.CProd,
                                item.Quantidade,
                                item.Total
                            });

                    PedidoModalDividirConta.Itens = itens;
                }

                var form = new PedidoModalDividirConta {TopMost = true};
                if (form.ShowDialog() == DialogResult.OK)
                    valor.Text = Validation.FormatPrice(PedidoModalDividirConta.ValorDividido);
            };

            btnConcluir.Click += (s, e) => { Concluir(); };

            iniciar.KeyPress += Masks.MaskBirthday;
            iniciar.KeyPress += Masks.MaskBirthday;
            valor.KeyPress += (s, e) => Masks.MaskDouble(s, e);

            valor.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            mtxt2.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            GridListaFormaPgtos.CellDoubleClick += (s, e) =>
            {
                if (GridListaFormaPgtos.Columns[e.ColumnIndex].Name == "colExcluir")
                    if (Convert.ToString(GridListaFormaPgtos.CurrentRow.Cells[4].Value) != "")
                    {
                        var id = Validation.ConvertToInt32(GridListaFormaPgtos.CurrentRow.Cells[0].Value);
                        _mTitulo.Remove(id);
                        AtualizarDados();
                    }
            };

            GridListaFormaPgtos.CellBeginEdit += (s, e) =>
            {
                if (e.ColumnIndex == 2)
                {
                    //-----mtxt

                    mtxt.Mask = @"##/##/####";

                    var rec = GridListaFormaPgtos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    mtxt.Location = rec.Location;
                    mtxt.Size = rec.Size;
                    mtxt.Text = "";

                    if (GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value != null)
                        mtxt.Text = GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value.ToString();

                    mtxt.Visible = true;
                }

                if (e.ColumnIndex == 3)
                {
                    //-----mtxt2

                    var rec = GridListaFormaPgtos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    mtxt2.Location = rec.Location;
                    mtxt2.Size = rec.Size;
                    mtxt2.Text = "";

                    if (GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value != null)
                        mtxt2.Text = GridListaFormaPgtos[e.ColumnIndex, e.RowIndex].Value.ToString();

                    mtxt2.Visible = true;
                }
            };

            GridListaFormaPgtos.CellEndEdit += (s, e) =>
            {
                if (mtxt.Visible)
                {
                    GridListaFormaPgtos.CurrentCell.Value = mtxt.Text;
                    mtxt.Visible = false;
                }

                if (mtxt2.Visible)
                {
                    GridListaFormaPgtos.CurrentCell.Value = mtxt2.Text;
                    mtxt2.Visible = false;
                }

                var ID = Validation.ConvertToInt32(GridListaFormaPgtos.Rows[e.RowIndex].Cells["colID"].Value);
                if (ID == 0)
                    return;

                var titulo = new Model.Titulo().FindById(ID).FirstOrDefault<Model.Titulo>();

                if (titulo == null)
                    return;

                DateTime parsed;
                if (DateTime.TryParse(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column1"].Value.ToString(),
                    out parsed))
                    titulo.Vencimento =
                        Validation.ConvertDateToSql(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column1"].Value);
                else
                    GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column1"].Value =
                        Validation.ConvertDateToForm(titulo.Vencimento);

                titulo.Total = Validation.ConvertToDouble(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column3"].Value);
                titulo.Recebido =
                    Validation.ConvertToDouble(GridListaFormaPgtos.Rows[e.RowIndex].Cells["Column3"].Value);

                if (titulo.Save(titulo, false))
                {
                    //_controllerTitulo.GetDataTableTitulos(GridListaFormaPgtos, IdPedido); 
                    Alert.Message("Pronto!", "Recebimento atualizado com sucesso.", Alert.AlertType.success);
                    AtualizarDados(false);
                }
                else
                {
                    Alert.Message("Opsss!", "Algo deu errado ao atualizar o recebimento.", Alert.AlertType.error);
                }
            };

            GridListaFormaPgtos.Scroll += (s, e) =>
            {
                if (mtxt.Visible)
                {
                    var rec = GridListaFormaPgtos.GetCellDisplayRectangle(GridListaFormaPgtos.CurrentCell.ColumnIndex,
                        GridListaFormaPgtos.CurrentCell.RowIndex, true);
                    mtxt.Location = rec.Location;
                }

                if (mtxt2.Visible)
                {
                    var rec = GridListaFormaPgtos.GetCellDisplayRectangle(GridListaFormaPgtos.CurrentCell.ColumnIndex,
                        GridListaFormaPgtos.CurrentCell.RowIndex, true);
                    mtxt2.Location = rec.Location;
                }
            };

            btnClearRecebimentos.Click += (s, e) =>
            {
                foreach (DataGridViewRow row in GridListaFormaPgtos.Rows)
                    if (Convert.ToString(row.Cells[0].Value) != "")
                        _mTitulo.Remove(Validation.ConvertToInt32(row.Cells[0].Value), "ID", false);

                AtualizarDados();

                _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                _mPedido.status = 2;
                _mPedido.Save(_mPedido);
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
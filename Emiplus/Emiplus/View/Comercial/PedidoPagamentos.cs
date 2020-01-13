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

                label5.Text = "Pagamentos";
                enviarEmail.Visible = false;
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

            if (_controllerTitulo.GetLancados(IdPedido) > 0)
                Desconto.Enabled = false;
            else
                Desconto.Enabled = true;
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

            dynamic devolucoes = _mPedido.Query()
                .SelectRaw("SUM(total) as total")
                .Where("excluir", 0)
                .Where("tipo", "Devoluções")
                .Where("Venda", IdPedido)
                .FirstOrDefault<Model.Pedido>();

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
                AtualizarDados();
        }

        private void JanelaAcrescimo()
        {
            PedidoPayAcrescimo.idPedido = IdPedido;
            PedidoPayAcrescimo Acrescimo = new PedidoPayAcrescimo();
            if (Acrescimo.ShowDialog() == DialogResult.OK)
                AtualizarDados();
        }

        private void JanelaDevolucao()
        {
            PedidoPayDevolucao.idPedido = IdPedido;
            PedidoPayDevolucao f = new PedidoPayDevolucao();
            if (f.ShowDialog() == DialogResult.OK)
                AtualizarDados();
        }

        public void Concluir(int imprimir = 1)
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

            if (Home.pedidoPage == "Compras")
            {
                imprimir = 0;
                Application.OpenForms["AddPedidos"].Close();
                Close();
            }
                
            if (imprimir == 1)
            {
                if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                {
                    new Controller.Pedido().Imprimir(IdPedido);
                    //PedidoImpressao print = new PedidoImpressao();
                    //print.Print(IdPedido);
                }

                try
                {
                    Application.OpenForms["AddPedidos"].Close();
                }
                catch (Exception)
                {

                    throw;
                }

                Close();
            }
        }

        public void Nfe()
        {
            Concluir(0);

            OpcoesNfeRapida.idPedido = IdPedido;
            OpcoesNfeRapida f = new OpcoesNfeRapida();
            f.Show();
        }

        public void Cfe()
        {
            Concluir(0);

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
                    f.Show();
                }
            }
            else if (checkNota.Status == "Pendente")
            {
                OpcoesCfeEmitir.fecharTelas = true;

                OpcoesCfeCpf.idPedido = IdPedido;
                OpcoesCfeCpf.emitir = true;
                OpcoesCfeCpf f = new OpcoesCfeCpf();
                f.Show();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
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
                    Nfe();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F11:
                    new PedidoImpressao().Print(IdPedido);
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
                if (UserPermission.SetControl(btnCFeSat, pictureBox1, "fiscal_emissaocfe"))
                    return;

                KeyDowns(s, e);
            };

            btnNfe.KeyDown += (s, e) =>
            {
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

            Load += (s, e) =>
            {
                AtualizarDados();

                AddPedidos.btnFinalizado = false;

                Dinheiro.Focus();
                Dinheiro.Select();
            };            

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
        }

        private void PedidoPagamentos_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Ativo: PedidoPagamentos");            
        }
    }
}
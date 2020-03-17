﻿using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Fiscal.TelasNota;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class DetailsPedido : Form
    {
        #region V

        private Model.Pedido _modelPedido = new Model.Pedido();

        private Model.PessoaContato _modelPessoaContato = new Model.PessoaContato();
        private Model.PessoaEndereco _modelPessoaAddr = new Model.PessoaEndereco();
        private Model.Pessoa _modelPessoa = new Model.Pessoa();
        private Model.Usuarios _modelUsuario = new Model.Usuarios();

        private Model.PedidoItem _modelPedidoItem = new Model.PedidoItem();
        private Controller.PedidoItem _controllerPedidoItem = new Controller.PedidoItem();
        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

        #endregion V

        public static int idPedido { get; set; }

        private int pessoaID;

        public DetailsPedido()
        {
            InitializeComponent();
            Eventos();

            Resolution.SetScreenMaximized(this);

            if (idPedido > 0)
                LoadData();

            switch (Home.pedidoPage)
            {
                case "Devoluções":
                    label1.Text = "Detalhes da Devolução:";
                    label2.Text = "Confira nessa tela todas as informações da sua devolução.";
                    label6.Text = "Detalhes da Devolução";
                    label3.Text = "Devoluções";
                    button21.Visible = false;
                    button22.Visible = false;
                    btnNfe.Visible = false;
                    btnCFeSat.Visible = false;
                    btnPgtosLancado.Visible = false;
                    label11.Visible = false;
                    label43.Visible = false;
                    label41.Visible = false;
                    txtTroco.Visible = false;
                    txtRecebimento.Visible = false;
                    txtAcrescimo.Visible = false;
                    nrPedido.Left = 542;
                    break;

                case "Compras":
                    label1.Text = "Detalhes da Compra:";
                    label2.Text = "Confira nessa tela todas as informações da sua compra.";
                    label6.Text = "Detalhes da Compra";
                    label3.Text = "Compras";
                    label8.Text = "Fornecedor";
                    button22.Visible = false;
                    btnCFeSat.Visible = false;
                    label43.Text = "Total Pago:";
                    btnPgtosLancado.Text = "Ver Pagamentos Lançados!";
                    nrPedido.Left = 510;
                    btnNfe.Visible = false;
                    button21.Visible = false;
                    SelecionarCliente.Text = "Alterar fornecedor";
                    break;
            }

            PedidoItem.impostos = false;
        }

        public void Nfe()
        {
            OpcoesNfeRapida.idPedido = idPedido;
            OpcoesNfeRapida.idNota = 0;
            OpcoesNfeRapida f = new OpcoesNfeRapida();
            f.Show();
        }

        public void Cfe(int tipo = 0)
        {
            OpcoesCfe.idNota = 0;

            if (tipo == 1)
                OpcoesCfe.tipo = "NFCe";
            else
                OpcoesCfe.tipo = "";

            var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", tipo == 1 ? "NFCe" : "CFe").FirstOrDefault<Model.Nota>();
            if (checkNota == null)
            {
                Model.Nota _modelNota = new Model.Nota();

                _modelNota.Id = 0;
                _modelNota.Tipo = tipo == 1 ? "NFCe" : "CFe";
                _modelNota.Status = "Pendente";
                _modelNota.id_pedido = idPedido;
                if (!_modelNota.Save(_modelNota, false))
                {
                    Alert.Message("Ação não permitida", "Problema com tabela Notas. Entre em contato com o suporte técnico!", Alert.AlertType.warning);
                    return;
                }

                checkNota = _modelNota;
            }

            if (checkNota != null)
                OpcoesCfe.idNota = checkNota.Id;
            else
                return;

            if (checkNota.Status == "Falha")
            {
                Alert.Message("Ação não permitida", "Entre em contato com o suporte técnico", Alert.AlertType.warning);
                return;
            }
            else if (checkNota.Status == "Autorizada" || checkNota.Status == "Autorizado")
            {
                OpcoesCfe.idPedido = idPedido;
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
                    _modelNota.Tipo = tipo == 1 ? "NFCe" : "CFe";
                    _modelNota.Status = "Pendente";
                    _modelNota.id_pedido = idPedido;
                    _modelNota.Save(_modelNota, false);

                    checkNota = _modelNota;

                    OpcoesCfeEmitir.fecharTelas = false;

                    OpcoesCfeCpf.idPedido = idPedido;
                    OpcoesCfeCpf.emitir = true;
                    OpcoesCfeCpf f = new OpcoesCfeCpf();
                    f.Show();
                }
            }
            else if (checkNota.Status == "Pendente")
            {
                OpcoesCfeEmitir.fecharTelas = false;

                OpcoesCfeCpf.idPedido = idPedido;
                OpcoesCfeCpf.emitir = true;
                OpcoesCfeCpf f = new OpcoesCfeCpf();
                f.Show();
            }
        }

        public void Nfse()
        {
            OpcoesNfse.idPedido = idPedido;
            OpcoesNfse.idNota = 0;
            OpcoesNfse f = new OpcoesNfse();
            f.Show();
        }

        private void LoadData()
        {
            _modelPedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

            nrPedido.Text = idPedido.ToString("D5");
            aberto.Text = Validation.ConvertDateToForm(_modelPedido.Criado, true);
            txtEntrega.Text = Validation.FormatPrice(_modelPedido.Frete, true);
            txtDesconto.Text = Validation.FormatPrice(_modelPedido.Desconto, true);

            txtTroco.Text = Validation.FormatPrice(_controllerTitulo.GetTroco(idPedido), true);
            txtSubtotal.Text = Validation.FormatPrice(_controllerTitulo.GetTotalProdutos(idPedido), true);
            txtPagar.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(idPedido), true);
            txtAcrescimo.Text = Validation.FormatPrice(_controllerTitulo.GetTotalFrete(idPedido), true);
            txtRecebimento.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(idPedido), true);
            caixa.Text = _modelPedido.Id_Caixa.ToString();

            if (_modelPedido.Cliente > 0)
            {
                Model.Pessoa pessoa = _modelPessoa.FindById(_modelPedido.Cliente).Select("id", "nome").FirstOrDefault<Model.Pessoa>();
                if (pessoa != null)
                {
                    pessoaID = pessoa.Id;
                    cliente.Text = pessoa.Nome;
                }
            }

            if (_modelPedido.Colaborador > 0)
            {
                Model.Usuarios data = _modelUsuario.FindByUserId(_modelPedido.Colaborador).FirstOrDefault<Model.Usuarios>();
                if (data != null)
                    vendedor.Text = data.Nome;
            }

            if (_modelPedido.status != 0)
            {
                panel7.BackColor = Color.FromArgb(215, 90, 74);
                label7.Text = "Fechado";
            }

            if (!string.IsNullOrEmpty(_modelPedido.Observacao))
            {
                label14.Visible = true;
                obs.Visible = true;
                obs.Text = _modelPedido.Observacao;
            }

            _controllerPedidoItem.GetDataTableItens(GridLista, idPedido);

            var checkCupom = new Model.Nota().Query().Where("nota.status", "Autorizada").Where("nota.id_pedido", idPedido).Where("nota.tipo", "CFe").FirstOrDefault();
            if (checkCupom != null)
            {
                labelCfe.Text = checkCupom.NR_NOTA.ToString();
            }

            var checkNota = new Model.Nota().Query().Where("nota.status", "Autorizada").Where("nota.id_pedido", idPedido).Where("nota.tipo", "NFe").FirstOrDefault();
            if (checkNota != null)
            {
                if(checkNota.NR_NOTA != null)
                    labelNfe.Text = checkNota.NR_NOTA.ToString();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;

                case Keys.F9:
                    if (UserPermission.SetControlVisual(btnCFeSat, pictureBox6, "fiscal_emissaocfe"))
                        return;

                    Cfe();
                    break;

                case Keys.F10:
                    if (!Support.CheckForInternetConnection())
                    {
                        Alert.Message("Opps", "Você está sem conexão com a internet.", Alert.AlertType.warning);
                        return;
                    }

                    if (UserPermission.SetControlVisual(btnNfe, pictureBox4, "fiscal_emissaonfe"))
                        return;

                    Nfe();
                    break;

                case Keys.F11:
                    new PedidoImpressao().Print(idPedido);
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            btnExit.Click += (s, e) => Close();

            Load += (s, e) =>
            {
                Model.Pedido Pedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                Pedido.Id = idPedido;
                Console.WriteLine(_modelPedido.Total);
                Console.WriteLine(_controllerTitulo.GetLancados(idPedido));
                if (_controllerTitulo.GetLancados(idPedido) < Validation.Round(_modelPedido.Total))
                {
                    Pedido.status = 2; //RECEBIMENTO PENDENTE
                }
                else
                    Pedido.status = 1; //FINALIZADO\RECEBIDO

                Pedido.Save(Pedido);
            };

            Activated += (s, e) =>
            {
                LoadData();
            };

            btnPgtosLancado.Click += (s, e) =>
            {
                OpenPedidoPagamentos();
            };

            btnRemove.Click += (s, e) =>
            {
                if (labelCfe.Text != "N/D" || labelNfe.Text != "N/D")
                {
                    Alert.Message("Ação não permitida", "Existem documentos fiscais vinculados!", Alert.AlertType.warning);
                    return;
                }

                var result = AlertOptions.Message("Atenção!", "Deseja realmente apagar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var remove = new Controller.Pedido();
                    remove.Remove(idPedido);
                    Close();
                }
            };

            btnImprimir.Click += (s, e) =>
            {
                OptionBobinaA4 f = new OptionBobinaA4();
                string tipo = "";
                f.TopMost = true;
                DialogResult formResult = f.ShowDialog();

                if (formResult == DialogResult.OK)
                {
                    tipo = "Folha A4";
                    new Controller.Pedido().Imprimir(idPedido, tipo);
                }
                else if (formResult == DialogResult.Cancel)
                {
                    tipo = "Bobina 80mm";
                    new Controller.Pedido().Imprimir(idPedido, tipo);
                }
            };

            btnNfe.Click += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    Alert.Message("Opps", "Você está sem conexão com a internet.", Alert.AlertType.warning);
                    return;
                }

                if (UserPermission.SetControlVisual(btnNfe, pictureBox4, "fiscal_emissaonfe"))
                    return;

                Nfe();
            };

            btnCFeSat.Click += (s, e) =>
            {
                if (UserPermission.SetControlVisual(btnCFeSat, pictureBox6, "fiscal_emissaocfe"))
                    return;

                Cfe();
                //Cfe(1);
            };

            btnNfse.Click += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    Alert.Message("Opps", "Você está sem conexão com a internet.", Alert.AlertType.warning);
                    return;
                }

                //if (UserPermission.SetControl(btnNfe, pictureBox4, "fiscal_emissaonfe"))
                //    return;

                Nfse();
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("http://ajuda.emiplus.com.br/");

            SelecionarCliente.Click += (s, e) =>
            {
                var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                if (checkNota != null)
                {
                    if (checkNota.Status == "Autorizada")
                    {
                        Alert.Message("Ação não permitida", "Existem documentos fiscais vinculados!", Alert.AlertType.warning);
                        return;
                    }
                }

                ModalClientes();
            };

            SelecionarColaborador.Click += (s, e) =>
            {
                var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Model.Nota>();
                if (checkNota != null)
                {
                    if (checkNota.Status == "Autorizada")
                    {
                        Alert.Message("Ação não permitida", "Existem documentos fiscais vinculados!", Alert.AlertType.warning);
                        return;
                    }
                }

                ModalColaborador();
            };

            impostos.CheckStateChanged += (s, e) =>
            {
                if (impostos.Checked)
                    PedidoItem.impostos = true;
                else
                    PedidoItem.impostos = false;

                _controllerPedidoItem.GetDataTableItens(GridLista, idPedido);
            };
        }

        private void OpenPedidoPagamentos()
        {
            AddPedidos.Id = idPedido;
            PedidoPagamentos.hideFinalizar = true;
            OpcoesCfeEmitir.fecharTelas = false;
            PedidoPagamentos pagamentos = new PedidoPagamentos();
            pagamentos.ShowDialog();
            LoadData();
        }

        private void ModalClientes()
        {
            PedidoModalClientes form = new PedidoModalClientes();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _modelPedido.Id = idPedido;
                _modelPedido.Cliente = PedidoModalClientes.Id;
                _modelPedido.Save(_modelPedido);
                LoadData();
            }
        }

        public void ModalColaborador()
        {
            PedidoModalVendedor form = new PedidoModalVendedor();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _modelPedido.Id = idPedido;
                _modelPedido.Colaborador = PedidoModalVendedor.Id;
                _modelPedido.Save(_modelPedido);
                LoadData();
            }
        }
    }
}
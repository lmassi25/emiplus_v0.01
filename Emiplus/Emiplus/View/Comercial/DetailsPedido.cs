using DotLiquid;
using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Financeiro;
using Emiplus.View.Fiscal.TelasNota;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        #endregion

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
                    panel8.Visible = false;
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
            OpcoesNfeRapida f = new OpcoesNfeRapida();
            f.Show();
        }

        public void Cfe()
        {
            OpcoesCfe.idNota = 0;

            var checkNota = new Model.Nota().FindByIdPedidoUltReg(idPedido, "", "CFe").FirstOrDefault<Model.Nota>();
            if (checkNota == null)
            {
                Model.Nota _modelNota = new Model.Nota();

                _modelNota.Id = 0;
                _modelNota.Tipo = "CFe";
                _modelNota.Status = "Pendente";
                _modelNota.id_pedido = idPedido;
                _modelNota.Save(_modelNota, false);

                checkNota = _modelNota;
            }

            if (checkNota != null)
                OpcoesCfe.idNota = checkNota.Id;
            else
                return;

            if (checkNota.Status == "Autorizada" || checkNota.Status == "Autorizado")
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
                    _modelNota.Tipo = "CFe";
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

            _controllerPedidoItem.GetDataTableItens(GridLista, idPedido);

            var checkCupom = new Model.Nota().Query().Where("nota.status", "Autorizada").Where("nota.id_pedido", idPedido).Where("nota.tipo", "CFe").FirstOrDefault();
            if (checkCupom != null)
            {
                labelCfe.Text = checkCupom.NR_NOTA.ToString();
            }

            var checkNota = new Model.Nota().Query().Where("nota.status", "Autorizada").Where("nota.id_pedido", idPedido).Where("nota.tipo", "NFe").FirstOrDefault();
            if (checkNota != null)
            {
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
                    if (UserPermission.SetControl(btnNfe, pictureBox4, "fiscal_emissaonfe"))
                        return;

                    Cfe();
                    break;
                case Keys.F10:
                    if (UserPermission.SetControl(btnCFeSat, pictureBox6, "fiscal_emissaocfe"))
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

            btnPgtosLancado.Click += (s, e) =>
            {
                if (labelCfe.Text != "N/D" || labelNfe.Text != "N/D")
                {
                    Alert.Message("Ação não permitida", "Existem documentos fiscais vinculados!", Alert.AlertType.warning);
                    return;
                }

                if (Home.idCaixa == 0 && Home.pedidoPage == "Vendas")
                {
                    var result = AlertOptions.Message("Atenção!", "É necessário ter o caixa aberto para lançar recebimentos. Deseja ABRIR o caixa?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        AbrirCaixa f = new AbrirCaixa();
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            OpenPedidoPagamentos();
                        }
                    }
                }
                else
                {
                    OpenPedidoPagamentos();
                }
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
                new Controller.Pedido().Imprimir(idPedido);
            };

            btnNfe.Click += (s, e) =>
            {
                if (UserPermission.SetControl(btnNfe, pictureBox4, "fiscal_emissaonfe"))
                    return;

                Nfe();
            };

            btnCFeSat.Click += (s, e) =>
            {
                if (UserPermission.SetControl(btnCFeSat, pictureBox6, "fiscal_emissaocfe"))
                    return;

                Cfe();
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
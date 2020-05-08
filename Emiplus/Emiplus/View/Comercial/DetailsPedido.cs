using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using Emiplus.View.Fiscal.TelasNota;
using Emiplus.View.Reports;
using SqlKata.Execution;
using Nota = Emiplus.Model.Nota;
using PedidoItem = Emiplus.Controller.PedidoItem;
using Pessoa = Emiplus.Model.Pessoa;
using Titulo = Emiplus.Controller.Titulo;

namespace Emiplus.View.Comercial
{
    public partial class DetailsPedido : Form
    {
        private readonly PedidoItem _controllerPedidoItem = new PedidoItem();
        private readonly Titulo _controllerTitulo = new Titulo();
        private Model.Pedido _modelPedido = new Model.Pedido();

        private readonly Pessoa _modelPessoa = new Pessoa();

        private readonly Usuarios _modelUsuario = new Usuarios();

        public DetailsPedido()
        {
            InitializeComponent();
            Eventos();

            Resolution.SetScreenMaximized(this);

            if (idPedido > 0)
                LoadData();

            switch (Home.pedidoPage)
            {
                case "Balcao":
                    label1.Text = @"Detalhes do Pedido:";
                    label2.Text = @"Confira nessa tela todas as informações do pedido.";
                    nrPedido.Left = 288;
                    btnStatus.Visible = true;
                    label7.Text = @"Pedido Fechado";
                    label19.Visible = true;
                    break;

                case "Delivery":
                    label1.Text = @"Detalhes do Pedido:";
                    label2.Text = @"Confira nessa tela todas as informações do pedido.";
                    label10.Text = @"Entregador";
                    nrPedido.Left = 288;
                    btnStatus.Visible = true;
                    label7.Text = @"Pedido Fechado";
                    label19.Visible = true;
                    break;

                case "Devoluções":
                    label1.Text = @"Detalhes da Devolução:";
                    label2.Text = @"Confira nessa tela todas as informações da sua devolução.";
                    label6.Text = @"Detalhes da Devolução";
                    label3.Text = @"Devoluções";
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
                    nrPedido.Left = 320;
                    break;

                case "Compras":
                    label1.Text = @"Detalhes da Compra:";
                    label2.Text = @"Confira nessa tela todas as informações da sua compra.";
                    label6.Text = @"Detalhes da Compra";
                    label3.Text = @"Compras";
                    label8.Text = @"Fornecedor";
                    button22.Visible = false;
                    btnCFeSat.Visible = false;
                    label43.Text = @"Total Pago:";
                    btnPgtosLancado.Text = @"Ver Pagamentos Lançados!";
                    nrPedido.Left = 315;
                    btnNfe.Visible = false;
                    button21.Visible = false;
                    SelecionarCliente.Text = @"Alterar fornecedor";
                    break;

                case "Remessas":
                    label1.Text = @"Detalhes da Remessa:";
                    label2.Text = @"Confira nessa tela todas as informações da remessa.";
                    label6.Text = @"Detalhes da Remessa";
                    label3.Text = @"Remessas";
                    label8.Text = @"Empresa";
                    button22.Visible = false;
                    btnCFeSat.Visible = false;
                    label43.Text = @"Total Pago:";
                    btnPgtosLancado.Visible = false;
                    btnPgtosLancado.Text = @"Ver Pagamentos Lançados!";
                    nrPedido.Left = 315;
                    btnNfe.Visible = false;
                    button21.Visible = false;
                    SelecionarCliente.Visible = false;
                    SelecionarColaborador.Visible = false;
                    SelecionarCliente.Text = @"Alterar fornecedor";
                    btnNfse.Visible = false;
                    button2.Visible = false;
                    pictureBox5.Visible = false;
                    label12.Text = @"Status:";
                    label16.Visible = false;
                    labelCfe.Visible = false;
                    break;
            }

            PedidoItem.impostos = false;
        }

        public static int idPedido { get; set; }

        public void Nfe()
        {
            OpcoesNfeRapida.idPedido = idPedido;
            OpcoesNfeRapida.idNota = 0;
            var f = new OpcoesNfeRapida();
            f.Show();
        }

        public void Cfe(int tipo = 0)
        {
            OpcoesCfe.idNota = 0;
            OpcoesCfe.tipo = tipo == 1 ? "NFCe" : "";

            var checkNota = new Nota().FindByIdPedidoUltReg(idPedido, "", tipo == 1 ? "NFCe" : "CFe").FirstOrDefault<Nota>();
            if (checkNota == null)
            {
                var _modelNota = new Nota
                {
                    Id = 0,
                    Tipo = tipo == 1 ? "NFCe" : "CFe", 
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
                    var f = new OpcoesCfe();
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
                            Tipo = tipo == 1 ? "NFCe" : "CFe", 
                            Status = "Pendente", 
                            id_pedido = idPedido
                        };

                        _modelNota.Save(_modelNota, false);

                        checkNota = _modelNota;

                        OpcoesCfeEmitir.fecharTelas = false;

                        OpcoesCfeCpf.idPedido = idPedido;
                        OpcoesCfeCpf.emitir = true;
                        var f = new OpcoesCfeCpf();
                        f.Show();
                    }

                    break;
                }
                case "Pendente":
                {
                    OpcoesCfeEmitir.fecharTelas = false;

                    OpcoesCfeCpf.idPedido = idPedido;
                    OpcoesCfeCpf.emitir = true;
                    var f = new OpcoesCfeCpf();
                    f.Show();
                    break;
                }
            }
        }

        public void Nfse()
        {
            OpcoesNfse.idPedido = idPedido;
            OpcoesNfse.idNota = 0;
            var f = new OpcoesNfse();
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

            if (Home.pedidoPage == "Remessas")
            {
                labelNfe.Text = _modelPedido.campoa;
                cliente.Text = _modelPedido.campob;
            }
            else
            {
                if (_modelPedido.Cliente > 0)
                {
                    var pessoa = _modelPessoa.FindById(_modelPedido.Cliente).Select("id", "nome")
                        .FirstOrDefault<Pessoa>();
                    if (pessoa != null)
                    {
                        cliente.Text = pessoa.Nome;
                    }
                }
            }

            if (_modelPedido.Colaborador > 0)
            {
                if (Home.pedidoPage == "Delivery")
                {
                    var pessoa = _modelPessoa.FindById(_modelPedido.Id_Transportadora).Select("id", "nome")
                        .FirstOrDefault<Pessoa>();
                    if (pessoa != null)
                        vendedor.Text = pessoa.Nome;
                }
                else
                {
                    var data = _modelUsuario.FindByUserId(_modelPedido.Colaborador).FirstOrDefault<Usuarios>();
                    if (data != null)
                        vendedor.Text = data.Nome;
                }
            }

            if (_modelPedido.status != 0)
            {
                panel7.BackColor = Color.FromArgb(215, 90, 74);
                label7.Text = @"Fechado";
            }

            if (!string.IsNullOrEmpty(_modelPedido.Observacao))
            {
                label14.Visible = true;
                obs.Visible = true;
                obs.Text = _modelPedido.Observacao;
            }

            _controllerPedidoItem.GetDataTableItens(GridLista, idPedido);

            var checkCupom = new Nota().Query().Where("nota.status", "Autorizada").Where("nota.id_pedido", idPedido)
                .Where("nota.tipo", "CFe").FirstOrDefault();
            if (checkCupom != null) labelCfe.Text = checkCupom.NR_NOTA.ToString();

            var checkNota = new Nota().Query().Where("nota.status", "Autorizada").Where("nota.id_pedido", idPedido)
                .Where("nota.tipo", "NFe").FirstOrDefault();
            if (checkNota != null)
                if (checkNota.NR_NOTA != null)
                    labelNfe.Text = checkNota.NR_NOTA.ToString();
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

        private void LoadStatus()
        {
            var status = new ArrayList
            {
                new {Id = "0", Nome = "Selecione"},
                new {Id = "FAZENDO", Nome = "Fazendo"},
                new {Id = "PRONTO", Nome = "Pronto / Para Retirar"},
                new {Id = "ENTREGANDO", Nome = "Saiu para Entrega"},
                new {Id = "FINALIZADO", Nome = "Finalizado / Entregue"}
            };

            Status.DataSource = status;
            Status.DisplayMember = "Nome";
            Status.ValueMember = "Id";
        }

        private string GetStatus(string status)
        {
            switch (status)
            {
                case "FAZENDO":
                    return "Fazendo";
                case "PRONTO":
                    return "Pronto / Para Retirar";
                case "ENTREGANDO":
                    return "Saiu para Entrega";
                case "FINALIZADO":
                    return "Finalizado / Entregue";
            }

            return "";
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            btnExit.Click += (s, e) => Close();

            Load += (s, e) =>
            {
                LoadStatus();

                var pedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                pedido.Id = idPedido;
                pedido.status = _controllerTitulo.GetLancados(idPedido) < Validation.Round(_modelPedido.Total) ? 2 : 1;

                if (Home.pedidoPage == "Delivery" || Home.pedidoPage == "Balcao")
                {
                    Status.SelectedValue = pedido.campoa;
                    label19.Text = GetStatus(pedido.campoa);
                }

                pedido.Save(pedido);
            };

            Activated += (s, e) => { LoadData(); };

            btnPgtosLancado.Click += (s, e) => { OpenPedidoPagamentos(); };

            btnRemove.Click += (s, e) =>
            {
                if (labelCfe.Text != @"N/D" || labelNfe.Text != @"N/D")
                {
                    Alert.Message("Ação não permitida", "Existem documentos fiscais vinculados!",
                        Alert.AlertType.warning);
                    return;
                }

                var result = AlertOptions.Message("Atenção!", "Deseja realmente apagar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var remove = new Controller.Pedido();
                    remove.Remove(idPedido);
                    Close();
                }
            };

            btnImprimir.Click += (s, e) =>
            {
                var f = new OptionBobinaA4();
                string tipo;
                f.TopMost = true;
                var formResult = f.ShowDialog();

                switch (formResult)
                {
                    case DialogResult.OK:
                        tipo = "Folha A4";
                        new Controller.Pedido().Imprimir(idPedido, tipo);
                        break;
                    case DialogResult.Cancel:
                        tipo = "Bobina 80mm";
                        new Controller.Pedido().Imprimir(idPedido, tipo);
                        break;
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
                var checkNota = new Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Nota>();
                if (checkNota != null)
                    if (checkNota.Status == "Autorizada")
                    {
                        Alert.Message("Ação não permitida", "Existem documentos fiscais vinculados!",
                            Alert.AlertType.warning);
                        return;
                    }

                ModalClientes();
            };

            SelecionarColaborador.Click += (s, e) =>
            {
                var checkNota = new Nota().FindByIdPedidoUltReg(idPedido, "", "NFe").FirstOrDefault<Nota>();
                if (checkNota != null)
                    if (checkNota.Status == "Autorizada")
                    {
                        Alert.Message("Ação não permitida", "Existem documentos fiscais vinculados!",
                            Alert.AlertType.warning);
                        return;
                    }

                ModalColaborador();
            };

            impostos.CheckStateChanged += (s, e) =>
            {
                PedidoItem.impostos = impostos.Checked;

                _controllerPedidoItem.GetDataTableItens(GridLista, idPedido);
            };

            btnStatus.Click += (s, e) =>
            {
                pictureBox7.Visible = true;
                visualPanel1.Visible = true;
            };

            btnCancelStatus.Click += (s, e) =>
            {
                pictureBox7.Visible = false;
                visualPanel1.Visible = false;
            };

            btnSaveStatus.Click += (s, e) =>
            {
                if (Status.SelectedValue.ToString() == "Selecione")
                {
                    Alert.Message("Opss", "Selecione um status.", Alert.AlertType.error);
                    return;
                }

                var pedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                pedido.Id = idPedido;
                pedido.campoa = Status.SelectedValue.ToString();
                label19.Text = GetStatus(pedido.campoa);
                if (pedido.Save(pedido))
                {
                    Alert.Message("Pronto", "Status atualizado.", Alert.AlertType.success);
                    pictureBox7.Visible = false;
                    visualPanel1.Visible = false;
                }
            };
        }

        private void OpenPedidoPagamentos()
        {
            AddPedidos.Id = idPedido;
            PedidoPagamentos.HideFinalizar = true;
            OpcoesCfeEmitir.fecharTelas = false;
            var pagamentos = new PedidoPagamentos();
            pagamentos.ShowDialog();
            LoadData();
        }

        private void ModalClientes()
        {
            if (Home.pedidoPage == "Delivery")
                PedidoModalClientes.page = "Clientes";

            var form = new PedidoModalClientes();
            if (form.ShowDialog() != DialogResult.OK)
                return;

            _modelPedido.Id = idPedido;
            _modelPedido.Cliente = PedidoModalClientes.Id;
            _modelPedido.Save(_modelPedido);
            LoadData();
        }

        public void ModalColaborador()
        {
            if (Home.pedidoPage == "Delivery")
            {
                PedidoModalClientes.page = "Entregadores";
                var form = new PedidoModalClientes {TopMost = true};
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                _modelPedido.Id = idPedido;
                _modelPedido.Id_Transportadora = PedidoModalClientes.Id;
                _modelPedido.Save(_modelPedido);
                LoadData();
            }
            else
            {
                var form = new PedidoModalVendedor();
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                _modelPedido.Id = idPedido;
                _modelPedido.Colaborador = PedidoModalVendedor.Id;
                _modelPedido.Save(_modelPedido);
                LoadData();
            }
        }
    }
}
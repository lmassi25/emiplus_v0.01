using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Food;
using Emiplus.View.Reports;
using SqlKata.Execution;
using Estoque = Emiplus.Controller.Estoque;
using Imposto = Emiplus.Controller.Imposto;
using Item = Emiplus.Model.Item;
using PedidoItem = Emiplus.Model.PedidoItem;
using Pessoa = Emiplus.Model.Pessoa;

namespace Emiplus.View.Comercial
{
    public partial class AddPedidos : Form
    {
        private readonly Pessoa _mCliente = new Pessoa();

        private readonly Item _mItem = new Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private readonly PedidoItem _mPedidoItens = new PedidoItem();
        private readonly Usuarios _mUsuario = new Usuarios();

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        private FullScreenMode fullScreenMode;

        public AddPedidos()
        {
            InitializeComponent();
            Eventos();
        }

        private int ModoRapAva { get; set; }

        private int ModoRapAvaConfig { get; set; }

        private static string CachePage { get; set; }

        public static bool BtnFinalizado { get; set; } // Alimenta quando o botão finalizado for clicado
        public static bool BtnVoltar { get; set; } // Alimenta quando o botão finalizado for clicado

        public static bool PDV { get; set; }

        public static bool Telapedidos { get; set; }
        public static bool Telapagamentos { get; set; } // Alimenta quando o botão finalizado for clicado

        public static int Id { get; set; } // id pedido
        public static int IdPedidoItem { get; set; } // Id item datagrid

        private void LoadData()
        {
            _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
            if (_mPedido == null)
            {
                Alert.Message("Opps", "Não encontramos o registro.", Alert.AlertType.info);
                return;
            }

            IDCaixa.Text = Home.idCaixa.ToString();

            if (Home.pedidoPage == "Orçamentos")
            {
                PDV = false;

                label2.Text = $@"Dados do Orçamento: {Id}";
                label3.Text = @"Siga as etapas abaixo para criar um orçamento!";
                btnConcluir.Text = @"Finalizar";
                btnConcluir.Refresh();
                pictureBox8.Visible = false;
                label12.Visible = false;
                IDCaixa.Visible = false;
                btnDelete.Text = @"Apagar";
                btnGerarVenda.Visible = true;

                if (_mPedido.status == 1)
                {
                    btnConcluir.Text = @"Reabrir";

                    BuscarProduto.Enabled = false;
                    Quantidade.Enabled = false;
                    Preco.Enabled = false;
                    Medidas.Enabled = false;
                    DescontoPorcentagem.Enabled = false;
                    DescontoReais.Enabled = false;
                    addProduto.Enabled = false;
                }
                else
                {
                    btnQuantidade.Visible = true;
                    btnQuantidade.Location = btnDelete.Location;
                    btnConcluir.Text = @"Finalizar";
                    btnDelete.Visible = false;
                    btnGerarVenda.Visible = false;
                }
            }
            else if (Home.pedidoPage == "Delivery")
            {
                PDV = false;

                label17.Text = @"Entregador";
                pictureBox10.Image = Resources.deliveryman;

                btnGerarVenda.Visible = false;
                btnDelete.Visible = false;
                label2.Text = $@"Dados do Delivery: {Id}";
                label3.Text = @"Siga as etapas abaixo para criar um novo pedido!";
                btnConcluir.Text = @"Receber";

                if (Home.idCaixa == 0) btnConcluir.Text = @"Finalizar";

                if (Home.idCaixa != 0)
                {
                    imprimir.Visible = false;
                    button2.Visible = false;
                }
            }
            else if (Home.pedidoPage == "Consignações")
            {
                PDV = false;

                label2.Text = $@"Dados da Consignação: {Id}";
                label3.Text = @"Siga as etapas abaixo para criar uma consignação!";
                btnConcluir.Text = @"Finalizar";
                pictureBox8.Visible = false;
                label12.Visible = false;
                IDCaixa.Visible = false;
                btnDelete.Text = @"Apagar";
                btnGerarVenda.Visible = true;

                if (_mPedido.status == 1)
                {
                    btnConcluir.Text = @"Reabrir";

                    BuscarProduto.Enabled = false;
                    Quantidade.Enabled = false;
                    Preco.Enabled = false;
                    Medidas.Enabled = false;
                    DescontoPorcentagem.Enabled = false;
                    DescontoReais.Enabled = false;
                    addProduto.Enabled = false;
                }
                else
                {
                    btnConcluir.Text = @"Finalizar";
                    btnDelete.Visible = false;
                    btnGerarVenda.Visible = false;
                }
            }
            else if (Home.pedidoPage == "Compras")
            {
                PDV = false;
                label15.Text = @"Fornecedor:";

                pictureBox8.Visible = false;
                label12.Visible = false;
                IDCaixa.Visible = false;
                label2.Text = $@"Dados da Compra: {Id}";
                label3.Text = @"Siga as etapas abaixo para adicionar uma compra!";
                btnConcluir.Text = @"Pagamento";
                btnDelete.Visible = false;
            }
            else if (Home.pedidoPage == "Remessas")
            {
                PDV = false;

                label15.Text = @"Empresa:";
                pictureBox8.Visible = false;
                label12.Visible = false;
                IDCaixa.Visible = false;
                label2.Text = $@"Dados da Remessa: {Id}";
                label3.Text = @"Siga as etapas abaixo para fazer uma remessa!";
                btnConcluir.Text = @"Finalizar";
                btnDelete.Visible = false;
                nomeCliente.Text = @"Selecione uma empresa";
                pictureBox9.Image = Resources.skyscrapper;
                SelecionarCliente.Location = new Point(720, 15);
            }
            else if (Home.pedidoPage == "Devoluções")
            {
                PDV = false;

                label2.Text = $@"Dados da Troca: {Id}";
                label3.Text = @"Siga as etapas abaixo para criar uma troca!";
                btnConcluir.Text = @"Finalizar";

                btnDelete.Visible = false;
                btnGerarVenda.Visible = false;

                pictureBox8.Visible = true;
                pictureBox8.Image = Resources.voucher;
                label12.Visible = true;
                label12.Text = @"Voucher";
                IDCaixa.Visible = true;

                if (string.IsNullOrEmpty(_mPedido.Voucher))
                    _mPedido.Voucher = new Controller.Pedido().RandomString(4);

                IDCaixa.Text = _mPedido.Voucher;

                if (_mPedido.status == 1)
                {
                    btnConcluir.Text = @"Reabrir";

                    BuscarProduto.Enabled = false;
                    Quantidade.Enabled = false;
                    Preco.Enabled = false;
                    Medidas.Enabled = false;
                    DescontoPorcentagem.Enabled = false;
                    DescontoReais.Enabled = false;
                    addProduto.Enabled = false;
                }
                else
                {
                    btnConcluir.Text = @"Finalizar";
                    btnDelete.Visible = false;
                }
            }
            else
            {
                btnGerarVenda.Visible = false;
                btnDelete.Visible = false;
                label2.Text = $@"Dados da Venda: {Id}";
                label3.Text = @"Siga as etapas abaixo para criar um novo pedido!";
                btnConcluir.Text = @"Receber";

                if (Home.idCaixa == 0) btnConcluir.Text = @"Finalizar";

                if (Home.idCaixa != 0)
                {
                    imprimir.Visible = false;
                    button2.Visible = false;
                }
            }

            // Carrega a Grid com os itens
            if (PDV)
                new Controller.PedidoItem().GetDataTableItensCompact(GridListaProdutos, Id);
            else
                new Controller.PedidoItem().GetDataTableItens(GridListaProdutos, Id);

            LoadCliente();
            LoadColaboradorCaixa();
            LoadTotais();

            ToolHelp.Show(
                "Insira o código de barras ou descrição do produto. " + Environment.NewLine +
                " Para adicionar observações a descrição do item coloque um '+' e adicione as informações.",
                pictureBox3, ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        /// <summary>
        ///     Carrega o cliente selecionado.
        /// </summary>
        private void LoadCliente()
        {
            if (Home.pedidoPage == "Remessas")
                return;

            if (_mPedido.Cliente <= 0)
                return;

            _mPedido.Save(_mPedido);
            var data = _mCliente.FindById(_mPedido.Cliente).FirstOrDefault<Pessoa>();
            if (data == null)
                return;

            if (Home.pedidoPage == "Compras" && data.Nome == "Consumidor Final")
                return;

            nomeCliente.Text = data.Nome;
        }

        /// <summary>
        ///     Carrega o vendedor selecionado.
        /// </summary>
        private void LoadColaboradorCaixa()
        {
            if (Home.pedidoPage == "Delivery")
            {
                if (_mPedido.Id_Transportadora <= 0)
                    return;

                _mPedido.Save(_mPedido);

                var data = _mCliente.FindById(_mPedido.Id_Transportadora).FirstOrDefault<Usuarios>();
                if (data == null)
                    return;

                nomeVendedor.Text = data.Nome;
            }
            else
            {
                if (_mPedido.Colaborador <= 0)
                    return;

                _mPedido.Save(_mPedido);

                var data = _mUsuario.FindByUserId(_mPedido.Colaborador).FirstOrDefault<Usuarios>();

                if (data == null)
                    return;

                nomeVendedor.Text = data.Nome;
            }
        }

        /// <summary>
        ///     Carrega e adiciona os itens no pedido!
        ///     Function: collection.Lookup recupera o ID
        /// </summary>
        private void LoadItens()
        {
            if (BuscarProduto.Text.Length > 0)
            {
                var item = _mItem.FindAll()
                    .Where("excluir", 0)
                    .Where("tipo", "Produtos")
                    .Where("codebarras", BuscarProduto.Text)
                    .OrWhere("referencia", BuscarProduto.Text)
                    .FirstOrDefault<Item>();

                if (item != null)
                    BuscarProduto.Text = item.Nome;
                else
                    ModalItens(); // Abre modal de Itens caso não encontre nenhum item no autocomplete, ou pressionando Enter.
            }

            // Valida a busca pelo produto e faz o INSERT, gerencia também o estoque e atualiza os totais
            AddItem();

            PedidoModalItens.NomeProduto = "";
        }

        /// <summary>
        ///     Atualiza o pedido com as somas totais.
        /// </summary>
        private void LoadTotais()
        {
            itens.Text = $@"Itens: {GridListaProdutos.RowCount}";

            var data = _mPedido.SaveTotais(_mPedidoItens.SumTotais(Id));
            _mPedido.Save(data);

            subTotal.Text = Validation.FormatPrice(_mPedido.GetTotal(), true);
            totaisDescontos.Text = $@"Totais descontos: {Validation.FormatPrice(_mPedido.GetDesconto(), true)}";
        }

        /// <summary>
        ///     Validação para tela de Pagamentos.
        /// </summary>
        private void TelaPagamentos()
        {
            Telapagamentos = true;

            if (GridListaProdutos.SelectedRows.Count <= 0)
            {
                AlertOptions.Message("Atenção!",
                    "Seu pedido não contém itens! Adicione pelo menos 1 item para prosseguir.", AlertBig.AlertType.info,
                    AlertBig.AlertBtn.OK);
                return;
            }

            if (Home.pedidoPage == "Delivery")
                if (nomeCliente.Text == @"Não informado" || nomeCliente.Text == @"Consumidor Final" || nomeCliente.Text == @"N/D")
                {
                    AlertOptions.Message("Atenção!", "Selecione um cliente para prosseguir.", AlertBig.AlertType.info,
                        AlertBig.AlertBtn.OK);
                    return;
                }

            if (!string.IsNullOrEmpty(IniFile.Read("TrocasCliente", "Comercial")))
                if (IniFile.Read("TrocasCliente", "Comercial") == "False")
                    if (Home.pedidoPage == "Devoluções")
                        if (nomeCliente.Text == @"Não informado" || nomeCliente.Text == @"Consumidor Final" || nomeCliente.Text == @"N/D")
                        {
                            AlertOptions.Message("Atenção!",
                                "Sua troca não contém cliente! Adicione um cliente para prosseguir.",
                                AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                            return;
                        }

            if (Home.pedidoPage == "Compras")
                if (nomeCliente.Text == @"Não informado" || nomeCliente.Text == @"Consumidor Final" || nomeCliente.Text == @"N/D")
                {
                    AlertOptions.Message("Atenção!",
                        "Sua compra não contém fornecedor! Adicione um fornecedor para prosseguir.",
                        AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                    return;
                }

            if (Home.pedidoPage == "Remessas")
            {
                if (nomeCliente.Text == @"Selecione uma empresa")
                {
                    AlertOptions.Message("Atenção!",
                        "Sua remessa não contém uma empresa selecionada! Selecione uma empresa para prosseguir.",
                        AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                    return;
                }

                _mPedido.Tipo = "Remessas";
                _mPedido.campoa = "Enviando";
                _mPedido.campob = nomeCliente.Text;
                if (_mPedido.Save(_mPedido))
                {
                    BtnFinalizado = true;
                    Close();
                    return;
                }

                Alert.Message("Opss", "Problema ao finalizar remessa", Alert.AlertType.error);
                return;
            }

            if (btnConcluir.Text == @"Reabrir")
            {
                BuscarProduto.Enabled = true;
                Quantidade.Enabled = true;
                Preco.Enabled = true;
                Medidas.Enabled = true;
                DescontoPorcentagem.Enabled = true;
                DescontoReais.Enabled = true;
                addProduto.Enabled = true;

                btnDelete.Visible = false;
                btnGerarVenda.Visible = false;

                _mPedido.Id = Id;
                _mPedido.status = 0;
                if (_mPedido.Save(_mPedido))
                {
                    Alert.Message("Tudo certo!", "Reaberto com sucesso.", Alert.AlertType.success);
                    btnConcluir.Text = @"Finalizar";
                    BuscarProduto.Select();
                }
                else
                {
                    Alert.Message("Opps!", "Erro ao reabrir.", Alert.AlertType.error);
                }

                return;
            }

            if (btnConcluir.Text == @"Finalizar" && Home.idCaixa == 0 && Home.pedidoPage == "Vendas") //Necessário para vendas balcão
            {
                BtnFinalizado = true;

                _mPedido.Id = Id;
                _mPedido.Tipo = "Vendas";
                _mPedido.status = 2; //RECEBIMENTO PENDENTE
                if (_mPedido.Save(_mPedido))
                {
                    if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info,
                        AlertBig.AlertBtn.YesNo, true))
                    {
                        var print = new PedidoImpressao();
                        if (print.Print(Id))
                            Close();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Alert.Message("Opps!", "Erro ao finalizar.", Alert.AlertType.error);
                }

                return;
            }

            PedidoPagamentos.HideFinalizar = false;
            var f = new PedidoPagamentos {TopMost = true};

            _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
            _mPedido.Id = Id;

            switch (Home.pedidoPage)
            {
                case "Compras":
                    _mPedido.Tipo = "Compras";
                    if (_mPedido.Save(_mPedido))
                    {
                        BtnFinalizado = true;
                        f.ShowDialog();
                        return;
                    }

                    Alert.Message("Opss", "Problema ao finalizar Compra", Alert.AlertType.error);
                    break;

                case "Devoluções":
                    _mPedido.Tipo = "Devoluções";
                    _mPedido.status = 1; //FINALIZADO
                    if (_mPedido.Save(_mPedido))
                    {
                        if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                        {
                            Alert.Message("Tudo certo!", "Devolução gerada com sucesso.", Alert.AlertType.success);
                            new Controller.Pedido().Imprimir(Id);
                        }

                        BtnFinalizado = true;
                        Close();
                        return;
                    }

                    Alert.Message("Opss", "Problema ao finalizar Devolução", Alert.AlertType.error);
                    break;

                case "Consignações":
                    _mPedido.Tipo = "Consignações";
                    _mPedido.status = 1; //FINALIZADO
                    if (_mPedido.Save(_mPedido))
                    {
                        if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                        {
                            Alert.Message("Tudo certo!", "Consignação gerada com sucesso.", Alert.AlertType.success);
                            new Controller.Pedido().Imprimir(Id);
                        }

                        BtnFinalizado = true;
                        Close();

                        return;
                    }

                    Alert.Message("Opss", "Problema ao finalizar Consignação", Alert.AlertType.error);
                    break;

                case "Orçamentos":
                    _mPedido.Tipo = "Orçamentos";
                    _mPedido.status = 1; //FINALIZADO
                    if (_mPedido.Save(_mPedido))
                    {
                        if (Home.pedidoPage == "Orçamentos")
                            new Estoque(Id, Home.pedidoPage, "Finalizar").Remove().Pedido();

                        if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                        {
                            Alert.Message("Tudo certo!", "Orçamento gerado com sucesso.", Alert.AlertType.success);
                            new Controller.Pedido().Imprimir(Id);
                        }

                        BtnFinalizado = true;

                        Close();
                        return;
                    }

                    Alert.Message("Opss", "Problema ao finalizar Orçamento", Alert.AlertType.error);
                    break;

                case "Delivery":
                    _mPedido.Tipo = "Delivery";
                    _mPedido.campoa = "FAZENDO";
                    _mPedido.status = 1; //FINALIZADO
                    if (_mPedido.Save(_mPedido))
                    {
                        BtnFinalizado = true;
                        f.Show();
                    }

                    break;

                case "Balcao":
                    _mPedido.Tipo = "Balcao";
                    _mPedido.campoa = "FAZENDO";
                    _mPedido.status = 1; //FINALIZADO
                    if (_mPedido.Save(_mPedido))
                    {
                        BtnFinalizado = true;
                        f.Show();
                    }

                    break;

                default:
                    BtnFinalizado = true;
                    f.Show();
                    break;
            }
        }

        /// <summary>
        ///     Altera o modo do pedido, para avançado e simples.
        ///     1 = Avançado, 0 = Simples
        /// </summary>
        private void AlterarModo()
        {
            if (ModoRapAva == 1)
            {
                ModoRapAva = 0;
                panelAvancado.Visible = false;
                ModoRapido.Text = @"Modo Avançado (F1) ?";
            }
            else
            {
                ModoRapAva = 1;
                panelAvancado.Visible = true;
                ModoRapido.Text = @"Modo Rápido (F1) ?";
            }
        }

        /// <summary>
        ///     Janela para selecionar Cliente no pedido.
        /// </summary>
        public void ModalClientes()
        {
            if (Home.pedidoPage == "Remessas")
            {
                var form = new ModalEmpresas();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _mPedido.Id = Id;
                    _mPedido.empresa = ModalEmpresas.Id;
                    _mPedido.Save(_mPedido);
                    nomeCliente.Text = ModalEmpresas.RazaoSocial;
                }
            }
            else
            {
                if (Home.pedidoPage == "Delivery")
                    PedidoModalClientes.page = "Clientes";

                var form = new PedidoModalClientes {TopMost = true};
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _mPedido.Id = Id;
                    _mPedido.Cliente = PedidoModalClientes.Id;
                    _mPedido.Save(_mPedido);
                    LoadData();
                }
            }

            BuscarProduto.Select();
        }

        /// <summary>
        ///     Janela para selecionar vendedor no pedido.
        /// </summary>
        public void ModalColaborador()
        {
            if (Home.pedidoPage == "Delivery")
            {
                PedidoModalClientes.page = "Entregadores";
                var form = new PedidoModalClientes {TopMost = true};
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _mPedido.Id = Id;
                    _mPedido.Id_Transportadora = PedidoModalClientes.Id;
                    _mPedido.Save(_mPedido);
                    LoadData();
                }
            }
            else
            {
                var form = new PedidoModalVendedor {TopMost = true};
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _mPedido.Id = Id;
                    _mPedido.Colaborador = PedidoModalVendedor.Id;
                    _mPedido.Save(_mPedido);
                    LoadData();
                }
            }

            BuscarProduto.Select();
        }

        /// <summary>
        ///     Janela para selecionar itens não encontrados.
        /// </summary>
        private void ModalItens()
        {
            if (collection.Lookup(NomeProduto()[0]) != 0)
                return;

            if (Application.OpenForms["PedidoModalItens"] is PedidoModalItens)
                return;

            PedidoModalItens.txtSearch = NomeProduto()[0];
            var form = new PedidoModalItens {TopMost = true};
            if (form.ShowDialog() != DialogResult.OK)
                return;

            BuscarProduto.Text = PedidoModalItens.NomeProduto;
            Preco.Text = Validation.FormatPrice(PedidoModalItens.ValorVendaProduto);
            PedidoModalItens.NomeProduto = "";

            if (!(Math.Abs(PedidoModalItens.ValorVendaProduto) < 0) || ModoRapAva != 0)
                return;

            AlterarModo();
            ModoRapAvaConfig = 1;
        }

        /// <summary>
        ///     Limpa os input text.
        /// </summary>
        private void ClearForms()
        {
            BuscarProduto.Clear();
            Quantidade.Clear();
            Preco.Clear();
            DescontoPorcentagem.Clear();
            DescontoReais.Clear();
        }

        public string[] NomeProduto()
        {
            var nomeProduto = new string[2];

            var checkNome = BuscarProduto.Text.Split(new[] {" + ", "+"}, StringSplitOptions.None);

            nomeProduto[0] = checkNome[0];
            if (checkNome.Length == 1)
                nomeProduto[1] = "";
            else
                nomeProduto[1] = checkNome[1];

            return nomeProduto;
        }

        /// <summary>
        ///     Adiciona item ao pedido, controla o estoque e atualiza os totais.
        /// </summary>
        private void AddItem()
        {
            if (collection.Lookup(NomeProduto()[0]) > 0 && string.IsNullOrEmpty(PedidoModalItens.NomeProduto))
            {
                var itemId = collection.Lookup(NomeProduto()[0]);
                var item = _mItem.FindById(itemId).WhereFalse("excluir").FirstOrDefault<Item>();

                var titleAttr = "";
                var idAttr = 0;
                var itemEstoque = new ItemEstoque().FindAll().WhereFalse("excluir").Where("item", itemId)
                    .Get<ItemEstoque>();
                if (itemEstoque.Any())
                {
                    AddAtributo.idProduto = itemId;
                    var form = new AddAtributo {TopMost = true};
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        idAttr = AddAtributo.idAttr;
                        var attrTitle = new ItemEstoque().FindAll().WhereFalse("excluir").Where("id", idAttr)
                            .FirstOrDefault<ItemEstoque>();
                        if (attrTitle != null)
                            titleAttr = attrTitle.Title;
                    }
                    else
                    {
                        return;
                    }
                }

                if (ModoRapAva == 0)
                    Medidas.SelectedItem = item.Medida;

                var quantidadeTxt = Validation.ConvertToDouble(Quantidade.Text);
                var descontoReaisTxt = Validation.ConvertToDouble(DescontoReais.Text);
                var descontoPorcentagemTxt = Validation.ConvertToDouble(DescontoPorcentagem.Text);
                var medidaTxt = Medidas.Text;
                var priceTxt = Validation.ConvertToDouble(Preco.Text);

                #region Controle de estoque

                var controlarEstoque = IniFile.Read("ControlarEstoque", "Comercial");
                if (!string.IsNullOrEmpty(controlarEstoque) && controlarEstoque == "True")
                    if (item.EstoqueAtual <= 0)
                    {
                        Alert.Message("Opps", "Você está sem estoque desse produto.", Alert.AlertType.warning);
                        return;
                    }

                if (Math.Abs(priceTxt) < 0)
                    if (descontoReaisTxt > item.ValorVenda || descontoReaisTxt > item.Limite_Desconto ||
                        descontoPorcentagemTxt > 101)
                    {
                        Alert.Message("Opps", "Não é permitido dar um desconto maior que o valor do item.",
                            Alert.AlertType.warning);
                        return;
                    }

                if (priceTxt > 0)
                    if (descontoReaisTxt > priceTxt || descontoPorcentagemTxt >= 101)
                    {
                        Alert.Message("Opps", "Não é permitido dar um desconto maior que o valor do item.",
                            Alert.AlertType.warning);
                        return;
                    }

                double limiteDescontoIni = 0;
                if (!string.IsNullOrEmpty(IniFile.Read("LimiteDesconto", "Comercial")))
                    limiteDescontoIni = Validation.ConvertToDouble(IniFile.Read("LimiteDesconto", "Comercial"));

                if (Math.Abs(item.Limite_Desconto) > 0)
                {
                    if (descontoReaisTxt > item.Limite_Desconto)
                    {
                        Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.",
                            Alert.AlertType.warning);
                        return;
                    }

                    if (priceTxt > 0)
                    {
                        var porcentagemValor = priceTxt / 100 * descontoPorcentagemTxt;
                        if (porcentagemValor > item.Limite_Desconto)
                        {
                            Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.",
                                Alert.AlertType.warning);
                            return;
                        }
                    }

                    if (Math.Abs(priceTxt) < 0)
                    {
                        var porcentagemValor = item.ValorVenda / 100 * descontoPorcentagemTxt;
                        if (porcentagemValor > item.Limite_Desconto)
                        {
                            Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.",
                                Alert.AlertType.warning);
                            return;
                        }
                    }
                }
                else
                {
                    if (Math.Abs(limiteDescontoIni) > 0)
                    {
                        if (descontoReaisTxt > limiteDescontoIni)
                        {
                            Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.",
                                Alert.AlertType.warning);
                            return;
                        }

                        if (Math.Abs(priceTxt) < 0)
                        {
                            var porcentagemValor = item.ValorVenda / 100 * descontoPorcentagemTxt;
                            if (porcentagemValor > limiteDescontoIni)
                            {
                                Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.",
                                    Alert.AlertType.warning);
                                return;
                            }
                        }

                        if (priceTxt > 0)
                        {
                            var porcentagemValor = priceTxt / 100 * descontoPorcentagemTxt;
                            if (porcentagemValor > limiteDescontoIni)
                            {
                                Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.",
                                    Alert.AlertType.warning);
                                return;
                            }
                        }
                    }
                }

                #endregion

                var pedidoItem = new PedidoItem();
                pedidoItem.SetId(0)
                    .SetTipo(item.Tipo)
                    .SetPedidoId(Id)
                    .SetAdicionalNomePdt(NomeProduto()[1])
                    .SetTitleAtributo(titleAttr)
                    .SetAtributo(idAttr)
                    .SetItem(item)
                    .SetQuantidade(quantidadeTxt)
                    .SetMedida(medidaTxt)
                    .SetDescontoReal(descontoReaisTxt);

                if (!pedidoItem.SetValorVenda(priceTxt))
                {
                    if (ModoRapAva == 0)
                    {
                        AlterarModo();
                        ModoRapAvaConfig = 1;
                    }

                    Preco.Select();
                    Preco.Focus();
                    return;
                }

                pedidoItem.SetDescontoPorcentagens(descontoPorcentagemTxt);
                pedidoItem.SomarTotal();

                if (Home.pedidoPage == "Remessas")
                    pedidoItem.Status = "Remessa";

                if (Home.pedidoPage == "Delivery" || Home.pedidoPage == "Balcao")
                    pedidoItem.Status = "FAZENDO";

                pedidoItem.Save(pedidoItem);

                if (item.Tipo == "Produtos")
                    if (Home.pedidoPage != "Orçamentos")
                    {
                        new Imposto().SetImposto(pedidoItem.GetLastId());

                        // Class Estoque -> Se for igual 'Compras', adiciona a quantidade no estoque do Item, se não Remove a quantidade do estoque do Item
                        if (Home.pedidoPage == "Compras")
                            new Estoque(pedidoItem.GetLastId(), Home.pedidoPage, $"Adicionar Produto {titleAttr}").Add()
                                .Item();
                        else if (Home.pedidoPage == "Devoluções")
                            new Estoque(pedidoItem.GetLastId(), Home.pedidoPage, $"Adicionar Produto {titleAttr}").Add()
                                .Item();
                        else if (Home.pedidoPage == "Remessas")
                            new Estoque(pedidoItem.GetLastId(), Home.pedidoPage, $"Remessa do Produto {titleAttr}")
                                .Remove().Item();
                        else
                            new Estoque(pedidoItem.GetLastId(), Home.pedidoPage, $"Adicionar Produto {titleAttr}")
                                .Remove().Item();
                    }

                // Carrega a Grid com o Item adicionado acima.
                if (PDV)
                    new Controller.PedidoItem().GetDataTableItensCompact(GridListaProdutos, Id);
                else
                    new Controller.PedidoItem().GetDataTableItens(GridListaProdutos, Id);

                // Atualiza o total do pedido, e os totais da tela
                LoadTotais();

                // Limpa os campos
                ClearForms();

                // Verifica se modo é avançado e altera para modo simples, apenas se modo simples for padrão
                if (ModoRapAva == 1 && ModoRapAvaConfig == 1)
                {
                    AlterarModo();
                    ModoRapAvaConfig = 0;
                }

                BuscarProduto.Select();
            }
        }

        /// <summary>
        ///     Adiciona os eventos de 'KeyDown' a todos os controls com a function KeyDowns
        /// </summary>
        private void KeyDowns(object sender, KeyEventArgs e)
        {
            //BeginInvoke(new Action(() =>
            //{
            //}));
            //e.SuppressKeyPress = true;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(false, GridListaProdutos);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Down:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(true, GridListaProdutos);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F1:
                    AlterarModo();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F2:
                    BuscarProduto.Focus();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F3:

                    if (_mPedido.status == 1)
                        if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" ||
                            Home.pedidoPage == "Consignações")
                        {
                            Alert.Message("Ação não permitida", "Não é permitido cancelar o produto.",
                                Alert.AlertType.warning);
                            return;
                        }

                    if (GridListaProdutos.SelectedRows.Count > 0)
                        if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                        {
                            var itemName = GridListaProdutos.SelectedRows[0].Cells["Descrição"].Value;

                            var result = AlertOptions.Message("Atenção!", $"Cancelar o item ('{itemName}') no pedido?",
                                AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                            if (result)
                            {
                                var idPedidoItem =
                                    Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);

                                GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);

                                if (Home.pedidoPage == "Compras")
                                    new Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Remove().Item();
                                else if (Home.pedidoPage == "Remessas")
                                    new Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Add().Item();
                                else if (Home.pedidoPage == "Devoluções")
                                    new Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Remove().Item();
                                else
                                    new Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Add().Item();

                                _mPedidoItens.Remove(idPedidoItem);

                                LoadTotais();
                            }
                        }

                    e.SuppressKeyPress = true;
                    break;

                case Keys.F4:
                    AddAdicionais();
                    break;

                case Keys.F5:
                    string tipo;
                    var f = new OptionBobinaA4{TopMost = true};
                    var formResult = f.ShowDialog();
                    if (formResult == DialogResult.OK)
                    {
                        tipo = "Folha A4";
                        new Controller.Pedido().Imprimir(Id, tipo);
                    }
                    else if (formResult == DialogResult.Cancel)
                    {
                        tipo = "Bobina 80mm";
                        new Controller.Pedido().Imprimir(Id, tipo);
                    }

                    break;

                case Keys.F7:
                    ModalClientes();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F8:
                    ModalColaborador();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F10:
                    TelaPagamentos();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F11:
                    var fullScreen = new FullScreen();

                    if (fullScreenMode == FullScreenMode.No)
                    {
                        fullScreen.EnterFullScreenMode(this);
                        fullScreenMode = FullScreenMode.Yes;
                    }
                    else
                    {
                        fullScreen.LeaveFullScreenMode(this);
                        fullScreenMode = FullScreenMode.No;
                    }

                    break;
            }
        }

        private void AddAdicionais()
        {
            if (Home.pedidoPage == "Delivery" || Home.pedidoPage == "Balcao")
            {
                AdicionaisDispon.ValorAddon = 0;
                AdicionaisDispon.AddonSelected = "";
                AdicionaisDispon.IdPedidoItem =
                    Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                AdicionaisDispon.IdItem =
                    Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["Item"].Value);
                var form = new AdicionaisDispon();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var getValor = Validation.ConvertToDouble(GridListaProdutos.SelectedRows[0].Cells["Unitário"].Value
                        .ToString().Replace("R$ ", ""));
                    GridListaProdutos.SelectedRows[0].Cells["Total"].Value =
                        Validation.FormatPrice(getValor + AdicionaisDispon.ValorAddon, true);

                    var idPedidoItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                    var upAddon = _mPedidoItens.FindById(idPedidoItem).FirstOrDefault<PedidoItem>();
                    if (upAddon != null)
                    {
                        upAddon.ValorVenda = getValor + AdicionaisDispon.ValorAddon;
                        upAddon.TotalVenda = getValor + AdicionaisDispon.ValorAddon;
                        upAddon.Total = getValor + AdicionaisDispon.ValorAddon;
                        upAddon.Adicional = AdicionaisDispon.AddonSelected;
                        upAddon.Save(upAddon, false);
                    }

                    LoadTotais();
                }
            }
        }

        /// <summary>
        ///     Adiciona os eventos nos Controls do form.
        /// </summary>
        private void Eventos()
        {
            KeyDown += KeyDowns;
            BuscarProduto.KeyDown += KeyDowns;
            Quantidade.KeyDown += KeyDowns;
            Preco.KeyDown += KeyDowns;
            Medidas.KeyDown += KeyDowns;
            DescontoPorcentagem.KeyDown += KeyDowns;
            DescontoReais.KeyDown += KeyDowns;
            panel1.KeyDown += KeyDowns;
            btnCancelarProduto.KeyDown += KeyDowns;
            btnDelete.KeyDown += KeyDowns;
            imprimir.KeyDown += KeyDowns;
            btnConcluir.KeyDown += KeyDowns;
            SelecionarCliente.KeyDown += KeyDowns;
            SelecionarColaborador.KeyDown += KeyDowns;
            addProduto.KeyDown += KeyDowns;
            GridListaProdutos.KeyDown += KeyDowns;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                if (PDV)
                {
                    btnObs.Visible = false;

                    if (IniFile.Read("ShowImagePDV", "Comercial") == "True")
                    {
                        // panel Imagem do produto
                        panel3.Visible = true;

                        // datagridview Items
                        panel4.Location = new Point(424, 100);
                        panel4.Width = 560;
                        panel4.Height = 418;
                    }
                    else if (IniFile.Read("ShowImagePDV", "Comercial") == "False")
                    {
                        // panel Imagem do produto
                        panel3.Visible = false;

                        // datagridview Items
                        panel4.Location = new Point(20, 100);
                        panel4.Width = 964;
                        panel4.Height = 418;
                    }

                    // panel Logo PDV
                    panel5.Visible = true;
                }
                else
                {
                    // panel Logo PDV
                    panel5.Visible = false;

                    // datagridview Items
                    panel4.Location = new Point(20, 100);
                    panel4.Width = 964;
                    panel4.Height = 418;

                    // panel SubTotal
                    visualPanel1.Width = 964;
                    visualPanel1.Location = new Point(20, 543);
                    itens.Location = new Point(20, 596);
                }
            };

            Shown += (s, e) =>
            {
                Refresh();

                CachePage = Home.pedidoPage;

                Resolution.SetScreenMaximized(this);
                BuscarProduto.Select();

                // Autocomplete de produtos
                collection = _mItem.AutoComplete();
                BuscarProduto.AutoCompleteCustomSource = collection;

                if (Id > 0)
                {
                    LoadData();
                }
                else
                {
                    _mPedido.Id = 0;
                    _mPedido.Cliente = 1;
                    _mPedido.Colaborador = Settings.Default.user_id;
                    _mPedido.Tipo = Home.pedidoPage;

                    if (Home.pedidoPage == "Balcao" || Home.pedidoPage == "Delivery")
                        _mPedido.campoa = "FAZENDO";

                    if (Home.pedidoPage == "Remessas")
                        _mPedido.campoc = Settings.Default.empresa_razao_social;

                    if (_mPedido.Save(_mPedido))
                    {
                        Id = _mPedido.GetLastId();
                        _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
                        LoadData();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar Pedido.", Alert.AlertType.error);
                        Close();
                    }
                }

                Medidas.DataSource = Support.GetMedidas();
                Refresh();
            };

            btnConcluir.Click += (s, e) => { TelaPagamentos(); };

            btnGerarVenda.Click += (s, e) =>
            {
                var f = new PedidoPagamentos {TopMost = true};

                _mPedido = _mPedido.FindById(Id).First<Model.Pedido>();
                _mPedido.Id = Id;
                _mPedido.Tipo = "Vendas";
                if (_mPedido.Save(_mPedido))
                {
                    BuscarProduto.Enabled = true;
                    Quantidade.Enabled = true;
                    Preco.Enabled = true;
                    Medidas.Enabled = true;
                    DescontoPorcentagem.Enabled = true;
                    DescontoReais.Enabled = true;
                    addProduto.Enabled = true;

                    Alert.Message("Tudo certo!", "Venda gerada com sucesso.", Alert.AlertType.success);
                    Home.pedidoPage = "Vendas";
                    LoadData();
                }
            };

            ModoRapido.Click += (s, e) => AlterarModo();
            SelecionarCliente.Click += (s, e) => ModalClientes();
            SelecionarColaborador.Click += (s, e) => ModalColaborador();

            addProduto.Click += (s, e) => LoadItens();

            BuscarProduto.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (ModoRapAva == 1)
                        if (!string.IsNullOrEmpty(NomeProduto()[0]))
                        {
                            var item = _mItem.FindById(collection.Lookup(NomeProduto()[0])).FirstOrDefault<Item>();
                            if (item != null)
                            {
                                Preco.Text = Validation.FormatPrice(item.ValorVenda);
                                Medidas.SelectedItem = item.Medida;
                            }

                            Quantidade.Focus();
                            return;
                        }

                    if (string.IsNullOrEmpty(NomeProduto()[0]))
                        ModalItens();
                    else
                        LoadItens();
                }
            };

            Quantidade.KeyPress += (s, e) => Masks.MaskDouble(s, e);
            txtQtd.KeyPress += (s, e) => Masks.MaskDouble(s, e);

            Preco.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            Preco.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };

            Quantidade.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter)
                    return;

                if (string.IsNullOrEmpty(NomeProduto()[0]))
                    BuscarProduto.Focus();
                else if (ModoRapAva == 1 && !string.IsNullOrEmpty(NomeProduto()[0]))
                    Preco.Focus();
                else
                    LoadItens();
            };

            string tipo;
            imprimir.Click += (s, e) =>
            {
                var f = new OptionBobinaA4 {TopMost = true};
                var formResult = f.ShowDialog();

                if (formResult == DialogResult.OK)
                {
                    tipo = "Folha A4";
                    new Controller.Pedido().Imprimir(Id, tipo);
                }
                else if (formResult == DialogResult.Cancel)
                {
                    tipo = "Bobina 80mm";
                    new Controller.Pedido().Imprimir(Id, tipo);
                }
            };

            DescontoPorcentagem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };

            DescontoReais.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };

            btnCancelarProduto.Click += (s, e) =>
            {
                //if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações" && _mPedido.status == 1)
                if (_mPedido.status == 1)
                {
                    Alert.Message("Ação não permitida", "Não é permitido cancelar o produto.", Alert.AlertType.warning);
                    return;
                }

                if (GridListaProdutos.SelectedRows.Count > 0)
                    if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                    {
                        var itemName = GridListaProdutos.SelectedRows[0].Cells["Descrição"].Value;

                        var result = AlertOptions.Message("Atenção!", $"Cancelar o item ('{itemName}') no pedido?",
                            AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                        if (result)
                        {
                            var idPedidoItem =
                                Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);

                            GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);

                            if (Home.pedidoPage != "Compras")
                                new Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Add().Item();
                            else if (Home.pedidoPage == "Remessas")
                                new Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Add().Item();
                            else
                                new Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Remove().Item();

                            _mPedidoItens.Remove(idPedidoItem);

                            LoadTotais();
                        }
                    }
            };

            if (PDV)
                GridListaProdutos.SelectionChanged += (s, e) =>
                {
                    if (GridListaProdutos.SelectedRows.Count > 0)
                    {
                        if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                        {
                            var idPedidoItem =
                                Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);

                            var idItem = _mPedidoItens.FindById(idPedidoItem).FirstOrDefault<PedidoItem>();
                            if (idItem != null)
                            {
                                var dataItem = _mItem.FindById(idItem.Item).FirstOrDefault<Item>();
                                if (dataItem != null)
                                {
                                    imgProduct.Image = File.Exists($@"{Program.PATH_IMAGE}\Imagens\{dataItem.Image}") ? Image.FromFile($@"{Program.PATH_IMAGE}\Imagens\{dataItem.Image}") : Resources.sem_imagem;

                                    nameProduct.Text = dataItem.Nome;
                                }
                            }
                        }
                    }
                    else
                    {
                        nameProduct.Text = @"NENHUM PRODUTO SELECIONADO";
                        imgProduct.Image = Resources.sem_imagem;
                    }
                };

            GridListaProdutos.CellDoubleClick += (s, e) => { AddAdicionais(); };

            btnDelete.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Deseja realmente apagar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var remove = new Controller.Pedido();
                    remove.Remove(Id);
                    Close();
                }
            };

            btnObs.Click += (s, e) =>
            {
                AddObservacao.idPedido = Id;
                var f = new AddObservacao {TopMost = true};
                f.Show();
            };

            btnQuantidade.Click += (s, e) =>
            {
                visualPanel2.Visible = true;
                pictureBox7.Visible = true;
            };

            btnSaveQtd.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtQtd.Text) || Validation.ConvertToDouble(txtQtd.Text) <= 0)
                {
                    Alert.Message("Opps", "Coloque uma quantidade válida.", Alert.AlertType.info);
                    return;
                }

                if (GridListaProdutos.SelectedRows.Count <= 0)
                {
                    Alert.Message("Opps", "Selecione um item na tabela.", Alert.AlertType.info);
                    return;
                }

                var idPedidoItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                var valorVenda = Validation.ConvertToDouble(GridListaProdutos.SelectedRows[0].Cells["Unitário"].Value
                    .ToString().Replace("R$ ", ""));
                var data = new PedidoItem().FindAll().Where("id", idPedidoItem).FirstOrDefault<PedidoItem>();
                if (data != null)
                {
                    var qtd = Validation.ConvertToDouble(txtQtd.Text);
                    data.Quantidade = qtd;
                    data.Total = valorVenda * qtd - data.DescontoItem;
                    data.TotalVenda = valorVenda * qtd;
                    data.Save(data);
                }

                LoadTotais();
                new Controller.PedidoItem().GetDataTableItens(GridListaProdutos, Id);

                txtQtd.Text = "";
                visualPanel2.Visible = false;
                pictureBox7.Visible = false;
            };

            btnCancelQtd.Click += (s, e) =>
            {
                visualPanel2.Visible = false;
                pictureBox7.Visible = false;
            };

            FormClosing += (s, e) =>
            {
                PDV = false;

                if (Home.pedidoPage == "Delivery")
                {
                    if (nomeCliente.Text == @"Não informado" || nomeCliente.Text == @"Consumidor Final" ||
                        nomeCliente.Text == @"N/D")
                    {
                        var valid = AlertOptions.Message("Atenção!",
                            "Para prosseguir com o pedido, adicione um cliente ou o pedido será excluído.\nExcluir Pedido?",
                            AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                        if (!valid)
                        {
                            e.Cancel = true;
                            return;
                        }

                        _mPedido.Remove(Id);

                        return;
                    }

                    return;
                }

                if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" ||
                    Home.pedidoPage == "Consignações" || Home.pedidoPage == "Delivery")
                    if (_mPedido.status == 1)
                        BtnFinalizado = true;

                if (BtnVoltar)
                    BtnVoltar = false;
                //return;

                if (!BtnFinalizado)
                {
                    Home.pedidoPage = CachePage;
                    var result = AlertOptions.Message("Atenção!",
                        "Você está prestes a excluir!" + Environment.NewLine + "Deseja continuar?",
                        AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        if (Home.pedidoPage != "Orçamentos")
                        {
                            if (Home.pedidoPage == "Compras")
                                new Estoque(Id, Home.pedidoPage, "Fechamento de Tela").Remove().Pedido();
                            else if (Home.pedidoPage == "Devoluções")
                                new Estoque(Id, Home.pedidoPage, "Fechamento de Tela").Remove().Pedido();
                            else
                                new Estoque(Id, Home.pedidoPage, "Fechamento de Tela").Add().Pedido();
                        }

                        _mPedido.Remove(Id);
                        return;
                    }

                    e.Cancel = true;
                }

                BtnFinalizado = false;
            };
        }

        private enum FullScreenMode
        {
            Yes,
            No
        }
    }
}
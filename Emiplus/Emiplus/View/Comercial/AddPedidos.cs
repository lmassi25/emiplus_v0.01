using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddPedidos : Form
    {
        #region V

        private int ModoRapAva { get; set; }

        private int ModoRapAvaConfig { get; set; }

        private static string CachePage { get; set; }

        public static bool btnFinalizado { get; set; } // Alimenta quando o botão finalizado for clicado

        public static bool telapedidos { get; set; }
        public static bool telapagamentos { get; set; } // Alimenta quando o botão finalizado for clicado

        public static int Id { get; set; } // id pedido
        public static int IdPedidoItem { get; set; } // Id item datagrid

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Usuarios _mUsuario = new Model.Usuarios();
        private Model.ItemEstoqueMovimentacao _mItemEstoque = new Model.ItemEstoqueMovimentacao();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        #endregion

        public AddPedidos()
        {
            InitializeComponent();
            CachePage = Home.pedidoPage;

            if (Id > 0)
                _mPedido = _mPedido.FindById(Id).First<Model.Pedido>();

            Eventos();

            BuscarProduto.Select();
        }

        private void LoadData()
        {
            IDCaixa.Text = Home.idCaixa.ToString();

            if (Home.pedidoPage == "Orçamentos")
            {
                label2.Text = $"Dados do Orçamento: {Id}";
                label3.Text = "Siga as etapas abaixo para criar um orçamento!";
                btnConcluir.Text = "Finalizar";
                pictureBox8.Visible = false;
                label12.Visible = false;
                IDCaixa.Visible = false;
                btnDelete.Text = "Apagar Orçamento";
                btnGerarVenda.Visible = true;

                if (_mPedido.status == 1)
                {
                    btnConcluir.Text = "Reabrir";

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
                    btnConcluir.Text = "Finalizar";
                    btnDelete.Visible = false;
                    btnGerarVenda.Visible = false;
                }   
            }
            else if (Home.pedidoPage == "Consignações")
            {
                label2.Text = $"Dados da Consignação: {Id}";
                label3.Text = "Siga as etapas abaixo para criar uma consignação!";
                btnConcluir.Text = "Finalizar";
                pictureBox8.Visible = false;
                label12.Visible = false;
                IDCaixa.Visible = false;
                btnDelete.Text = "Apagar Consignação";
                btnGerarVenda.Visible = true;

                if (_mPedido.status == 1)
                {
                    btnConcluir.Text = "Reabrir";

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
                    btnConcluir.Text = "Finalizar";
                    btnDelete.Visible = false;
                    btnGerarVenda.Visible = false;
                }
            }
            else if (Home.pedidoPage == "Compras")
            {
                label15.Text = "Fornecedor:";

                pictureBox8.Visible = false;
                label12.Visible = false;
                IDCaixa.Visible = false;
                label2.Text = $"Dados da Compra: {Id}";
                label3.Text = "Siga as etapas abaixo para adicionar uma compra!";
                btnConcluir.Text = "Pagamento";
                btnDelete.Visible = false;
            }
            else if (Home.pedidoPage == "Devoluções")
            {
                label2.Text = $"Dados da Troca: {Id}";
                label3.Text = "Siga as etapas abaixo para criar uma troca!";
                btnConcluir.Text = "Finalizar";
                
                btnDelete.Visible = false;
                btnGerarVenda.Visible = false;

                pictureBox8.Visible = true;
                pictureBox8.Image = Properties.Resources.voucher;
                label12.Visible = true;
                label12.Text = "Voucher";
                IDCaixa.Visible = true;
                _mPedido.Voucher = new Controller.Pedido().RandomString(4);
                IDCaixa.Text = _mPedido.Voucher;

                if (_mPedido.status == 1)
                {
                    btnConcluir.Text = "Reabrir";

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
                    btnConcluir.Text = "Finalizar";
                    btnDelete.Visible = false;
                }
            }
            else
            {
                btnGerarVenda.Visible = false;
                btnDelete.Visible = false;
                label2.Text = $"Dados da Venda: {Id}";
                label3.Text = "Siga as etapas abaixo para adicionar uma venda!";
                btnConcluir.Text = "Receber";

                if (Home.idCaixa == 0)
                {
                    btnConcluir.Text = "Finalizar";
                }

                if (Home.idCaixa != 0)
                {
                    imprimir.Visible = false;
                    button2.Visible = false;
                }
            }

            // Carrega a Grid com os itens
            new Controller.PedidoItem().GetDataTableItens(GridListaProdutos, Id);

            LoadCliente();
            LoadColaboradorCaixa();
            LoadTotais();

            ToolHelp.Show("Insira o código de barras ou descrição do produto.", pictureBox3, ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        /// <summary>
        /// Carrega o cliente selecionado.
        /// </summary>
        private void LoadCliente()
        {
            if (_mPedido.Cliente > 0)
            {
                _mPedido.Save(_mPedido);
                var data = _mCliente.FindById(_mPedido.Cliente).FirstOrDefault<Model.Pessoa>();

                if (data == null)
                    return;

                if (Home.pedidoPage == "Compras" && data.Nome == "Consumidor Final")
                    return;

                nomeCliente.Text = data.Nome;
            }
        }

        /// <summary>
        /// Carrega o vendedor selecionado.
        /// </summary>
        private void LoadColaboradorCaixa()
        {
            if (_mPedido.Colaborador > 0)
            {
                _mPedido.Save(_mPedido);

                var data = _mUsuario.FindByUserId(_mPedido.Colaborador).FirstOrDefault<Model.Usuarios>();

                if (data == null)
                    return;

                nomeVendedor.Text = data.Nome;
            }
        }

        /// <summary>
        /// Carrega e adiciona os itens no pedido!
        /// Function: collection.Lookup recupera o ID
        /// </summary>
        private void LoadItens()
        {
            if (BuscarProduto.Text.Length > 0)
            {
                Model.Item item = _mItem.FindAll().Where("excluir", 0).Where("tipo", "Produtos").Where("codebarras", BuscarProduto.Text)
                    .OrWhere("referencia", BuscarProduto.Text).FirstOrDefault<Model.Item>();
                if (item != null)
                {
                    BuscarProduto.Text = item.Nome;
                }
                else
                {
                    // Abre modal de Itens caso não encontre nenhum item no autocomplete, ou pressionando Enter.
                    ModalItens();
                }
            }

            // Valida a busca pelo produto e faz o INSERT, gerencia também o estoque e atualiza os totais 
            AddItem();

            PedidoModalItens.NomeProduto = "";
        }

        /// <summary>
        /// Atualiza o pedido com as somas totais.
        /// </summary>
        private void LoadTotais()
        {
            itens.Text = "Itens: " + GridListaProdutos.RowCount.ToString();

            var data = _mPedido.SaveTotais(_mPedidoItens.SumTotais(Id));
            _mPedido.Save(data);

            subTotal.Text = Validation.FormatPrice(_mPedido.GetTotal(), true);
            totaisDescontos.Text = "Totais descontos: " + Validation.FormatPrice(_mPedido.GetDesconto(), true);
        }

        /// <summary>
        /// Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                if (!String.IsNullOrEmpty(itens.NOME))
                    collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        /// <summary>
        /// Validação para tela de Pagamentos.
        /// </summary>
        private void TelaPagamentos()
        {
            AddPedidos.telapagamentos = true;

            if (GridListaProdutos.SelectedRows.Count <= 0)
            {
                AlertOptions.Message("Atenção!", "Seu pedido não contém itens! Adicione pelo menos 1 item para prosseguir.", AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                return;
            }

            if(Home.pedidoPage == "Compras" || Home.pedidoPage == "Devoluções")
            {
                if (nomeCliente.Text == "Não informado" || nomeCliente.Text == "Consumidor Final" || nomeCliente.Text == "N/D")
                {
                    if(Home.pedidoPage == "Compras")
                        AlertOptions.Message("Atenção!", "Sua compra não contém fornecedor! Adicione um fornecedor para prosseguir.", AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                    else if (Home.pedidoPage == "Devoluções")
                        AlertOptions.Message("Atenção!", "Sua troca não contém cliente! Adicione um cliente para prosseguir.", AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                    return;
                }
            }

            if (btnConcluir.Text == "Reabrir")
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
                _mPedido.Save(_mPedido);

                Alert.Message("Tudo certo!", "Reaberto com sucesso.", Alert.AlertType.success);
                btnConcluir.Text = "Finalizar";

                BuscarProduto.Select();

                return;
            }

            if (btnConcluir.Text == "Finalizar" && Home.idCaixa == 0 && Home.pedidoPage == "Vendas") //Necessário para vendas balcão
            {
                btnFinalizado = true;
                _mPedido.Id = Id;
                _mPedido.Tipo = "Vendas";
                _mPedido.status = 1; //RECEBIMENTO PENDENTE
                _mPedido.Save(_mPedido);

                Alert.Message("Pronto!", "Finalizado com sucesso.", Alert.AlertType.success);

                if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                {
                    PedidoImpressao print = new PedidoImpressao();
                    print.Print(Id);
                }

                Close();
                return;
            }

            PedidoPagamentos f = new PedidoPagamentos();
            _mPedido = _mPedido.FindById(Id).First<Model.Pedido>();
            _mPedido.Id = Id;

            switch (Home.pedidoPage)
            {
                case "Compras":
                    _mPedido.Tipo = "Compras";
                    if (_mPedido.Save(_mPedido))
                    {
                        btnFinalizado = true;                        
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
                            new Controller.Pedido().Imprimir(Id);
                        }

                        Alert.Message("Tudo certo!", "Devolução gerada com sucesso.", Alert.AlertType.success);
                        btnFinalizado = true;
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
                            new Controller.Pedido().Imprimir(Id);
                        }

                        Alert.Message("Tudo certo!", "Consignação gerada com sucesso.", Alert.AlertType.success);
                        btnFinalizado = true;
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
                        if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                        {
                            new Controller.Pedido().Imprimir(Id);
                        }

                        Alert.Message("Tudo certo!", "Orçamento gerado com sucesso.", Alert.AlertType.success);
                        btnFinalizado = true;
                        Close();
                        return;
                    }
                    Alert.Message("Opss", "Problema ao finalizar Orçamento", Alert.AlertType.error);
                    break;
                default:
                    btnFinalizado = true;
                    f.Show();
                    break;
            }
        }

        /// <summary>
        /// Altera o modo do pedido, para avançado e simples. 
        /// 1 = Avançado, 0 = Simples
        /// </summary>
        private void AlterarModo()
        {
            if (ModoRapAva == 1)
            {
                ModoRapAva = 0;
                panelAvancado.Visible = false;
                ModoRapido.Text = "Modo Avançado (F1) ?";
            }
            else
            {
                ModoRapAva = 1;
                panelAvancado.Visible = true;
                ModoRapido.Text = "Modo Rápido (F1) ?";
            }
        }

        /// <summary>
        /// Janela para selecionar Cliente no pedido.
        /// </summary>
        public void ModalClientes()
        {
            PedidoModalClientes form = new PedidoModalClientes();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _mPedido.Id = Id;
                _mPedido.Cliente = PedidoModalClientes.Id;
                _mPedido.Save(_mPedido);
                LoadData();
            }

            BuscarProduto.Select();
        }

        /// <summary>
        /// Janela para selecionar vendedor no pedido.
        /// </summary>
        public void ModalColaborador()
        {
            PedidoModalVendedor form = new PedidoModalVendedor();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _mPedido.Id = Id;
                _mPedido.Colaborador = PedidoModalVendedor.Id;
                _mPedido.Save(_mPedido);
                LoadData();
            }

            BuscarProduto.Select();
        }

        /// <summary>
        /// Janela para selecionar itens não encontrados.
        /// </summary>
        private void ModalItens()
        {
            if (collection.Lookup(BuscarProduto.Text) == 0)
            {
                if ((Application.OpenForms["PedidoModalItens"] as PedidoModalItens) == null)
                {
                    PedidoModalItens.txtSearch = BuscarProduto.Text;
                    PedidoModalItens form = new PedidoModalItens();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        BuscarProduto.Text = PedidoModalItens.NomeProduto;

                        if (PedidoModalItens.ValorVendaProduto == 0 && ModoRapAva == 0)
                        {
                            AlterarModo();
                            ModoRapAvaConfig = 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Limpa os input text.
        /// </summary>
        private void ClearForms()
        {
            BuscarProduto.Clear();
            Quantidade.Clear();
            Preco.Clear();
            DescontoPorcentagem.Clear();
            DescontoReais.Clear();
        }

        /// <summary>
        /// Adiciona item ao pedido, controla o estoque e atualiza os totais.
        /// </summary>
        private void AddItem()
        {
            if (collection.Lookup(BuscarProduto.Text) > 0 && String.IsNullOrEmpty(PedidoModalItens.NomeProduto))
            {
                var itemId = collection.Lookup(BuscarProduto.Text);
                Model.Item item = _mItem.FindById(itemId).Where("excluir", 0).Where("tipo", "Produtos").First<Model.Item>();

                double QuantidadeTxt = Validation.ConvertToDouble(Quantidade.Text);
                double DescontoReaisTxt = Validation.ConvertToDouble(DescontoReais.Text);
                double DescontoPorcentagemTxt = Validation.ConvertToDouble(DescontoPorcentagem.Text);
                string MedidaTxt = Medidas.Text;
                double PriceTxt = Validation.ConvertToDouble(Preco.Text);

                var pedidoItem = new Model.PedidoItem();
                pedidoItem.SetId(0)
                    .SetTipo("Produtos")
                    .SetPedidoId(Id)
                    .SetItem(item)
                    .SetQuantidade(QuantidadeTxt)
                    .SetMedida(MedidaTxt)
                    .SetDescontoReal(DescontoReaisTxt);

                if (!pedidoItem.SetValorVenda(PriceTxt))
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

                pedidoItem.SetDescontoPorcentagens(DescontoPorcentagemTxt);
                pedidoItem.SomarTotal();
                pedidoItem.Save(pedidoItem);

                new Controller.Imposto().SetImposto(pedidoItem.GetLastId());

                // Class Estoque -> Se for igual 'Compras', adiciona a quantidade no estoque do Item, se não Remove a quantidade do estoque do Item
                if (Home.pedidoPage == "Compras" || Home.pedidoPage == "Devoluções")
                    new Controller.Estoque(pedidoItem.GetLastId(), Home.pedidoPage, "Adicionar Produto").Add().Item();
                else
                    new Controller.Estoque(pedidoItem.GetLastId(), Home.pedidoPage, "Adicionar Produto").Remove().Item();

                // Carrega a Grid com o Item adicionado acima.
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
        /// Adiciona os eventos de 'KeyDown' a todos os controls com a function KeyDowns
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

                    if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações" && _mPedido.status == 1)
                    {
                        Alert.Message("Ação não permitida", "Não é permitido cancelar produto", Alert.AlertType.warning);
                        return;
                    }

                    if (GridListaProdutos.SelectedRows.Count > 0)
                    {
                        if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                        {
                            var itemName = GridListaProdutos.SelectedRows[0].Cells["Nome do Produto"].Value;

                            var result = AlertOptions.Message("Atenção!", $"Cancelar o item ('{itemName}') no pedido?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                            if (result)
                            {
                                var idPedidoItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                                _mPedidoItens.Remove(idPedidoItem);

                                GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);

                                if (Home.pedidoPage != "Compras")
                                    new Controller.Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar Produto").Add().Item();
                                else
                                    new Controller.Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar Produto").Remove().Item();

                                LoadTotais();
                            }
                        }
                    }
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F5:
                    new Controller.Pedido().Imprimir(Id);
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
            }
        }

        /// <summary>
        /// Adiciona os eventos nos Controls do form.
        /// </summary>
        private void Eventos()
        {
            BuscarProduto.Select();

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

            Load += (s, e) =>
            {
                AutoCompleteItens();

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

                Medidas.DataSource = new List<String> { "UN", "KG", "PC", "MÇ", "BD", "DZ", "GR", "L", "ML", "M", "M2", "ROLO", "CJ", "SC", "CX", "FD", "PAR", "PR", "KIT", "CNT", "PCT" };                
            };

            btnConcluir.Click += (s, e) =>
            {
                TelaPagamentos();
            };

            btnGerarVenda.Click += (s, e) =>
            {
                PedidoPagamentos f = new PedidoPagamentos();
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
                    return;
                }
            };

            ModoRapido.Click += (s, e) => AlterarModo();
            SelecionarCliente.Click += (s, e) => ModalClientes();
            SelecionarColaborador.Click += (s, e) => ModalColaborador();

            addProduto.Click += (s, e) => LoadItens();
            BuscarProduto.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };

            Preco.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };
            Preco.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };

            Quantidade.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (String.IsNullOrEmpty(BuscarProduto.Text))
                        BuscarProduto.Focus();
                    else if (ModoRapAva == 1 && !String.IsNullOrEmpty(BuscarProduto.Text))
                        Preco.Focus();
                    else
                    {
                        LoadItens();
                        ClearForms();
                    }
                }
            };

            imprimir.Click += (s, e) =>
            {
                new Controller.Pedido().Imprimir(Id);
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
                if ((Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações") && _mPedido.status == 1)
                {
                    Alert.Message("Ação não permitida", "Não é permitido cancelar produto", Alert.AlertType.warning);
                    return;
                }

                if (GridListaProdutos.SelectedRows.Count > 0)
                {
                    if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                        IdPedidoItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                }

                PedidoModalCancelItem cancel = new PedidoModalCancelItem();
                if (cancel.ShowDialog() == DialogResult.OK)
                    GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);
            };

            btnDelete.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Deseja realmente apagar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var remove = new Controller.Pedido();
                    remove.Remove(Id);
                    Close();
                }
            };

            FormClosing += (s, e) =>
            {
                if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações")
                {
                    if (_mPedido.status == 1)
                        btnFinalizado = true;
                }

                if (!btnFinalizado)
                {
                    Home.pedidoPage = CachePage;
                    var result = AlertOptions.Message("Atenção!", "Você está prestes a excluir! Deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        new Controller.Estoque(Id, Home.pedidoPage, "Fechamento de Tela").Add().Pedido();
                        _mPedido.Remove(Id);
                        return;
                    }

                    e.Cancel = true;
                }
                btnFinalizado = false;
            };
        }
    }
}

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
        private int ModoRapAva { get; set; }
        private int ModoRapAvaConfig { get; set; }
        private static string CachePage { get; set; }
        public static bool btnFinalizado { get; set; } // Alimenta quando o botão finalizado for clicado

        public static int Id { get; set; } // id pedido
        public static int IdPedidoItem { get; set; } // Id item datagrid

        private Model.Item _mItem = new Model.Item();
        
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();

        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Usuarios _mUsuario = new Model.Usuarios();

        private Model.ItemEstoqueMovimentacao _mItemEstoque = new Model.ItemEstoqueMovimentacao();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public AddPedidos()
        {
            InitializeComponent();
            CachePage = Home.pedidoPage;
            Eventos();
        }

        private void LoadData()
        {
            IDCaixa.Text = Home.idCaixa.ToString();

            if (Home.pedidoPage == "Orçamentos") {
                label2.Text = $"Dados do Orçamento: {Id}";
                label3.Text = "Siga as etapas abaixo para criar um novo orçamento!";
                btnConcluir.Text = "Gerar Venda";
            } else if (Home.pedidoPage == "Consignações") {
                label2.Text = $"Dados da Consignação: {Id}";
                label3.Text = "Siga as etapas abaixo para criar uma nova consignãção!";
                btnConcluir.Text = "Gerar Venda";
            } else if (Home.pedidoPage == "Compras") {
                label2.Text = $"Dados da Compra: {Id}";
                label3.Text = "Siga as etapas abaixo para adicionar uma nova compra!";
                btnConcluir.Text = "Pagamento";
            } else {                
                label2.Text = $"Dados da Venda: {Id}";
                label3.Text = "Siga as etapas abaixo para adicionar uma nova venda!";
                btnConcluir.Text = "Receber";

                if (Home.idCaixa == 0)
                {
                    btnConcluir.Text = "Finalizar";
                }
            }

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
                var data = _mCliente.FindById(_mPedido.Cliente).First<Model.Pessoa>();
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
                var data = _mUsuario.FindByUserId(_mPedido.Colaborador).First<Model.Usuarios>();
                nomeVendedor.Text = data.Nome;
            }
        }

        /// <summary>
        /// Carrega e adiciona os itens no pedido!
        /// Function: collection.Lookup recupera o ID
        /// </summary>
        private void LoadItens()
        {
            // Abre modal de Itens caso não encontre nenhum item no autocomplete, ou pressionando Enter.
            ModalItens();

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
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        /// <summary>
        /// Validação para tela de Pagamentos.
        /// </summary>
        private void TelaPagamentos()
        {
            if (GridListaProdutos.SelectedRows.Count <= 0)
            {
                AlertOptions.Message("Atenção!", "Seu pedido não contém itens! Adicione pelo menos 1 item para prosseguir.", AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                return;
            }

            if (btnConcluir.Text == "Finalizar")
            {
                btnFinalizado = true;
                _mPedido = _mPedido.FindById(Id).First<Model.Pedido>();
                _mPedido.Id = Id;
                _mPedido.Tipo = "Vendas";
                _mPedido.status = 1; //RECEBIMENTO PENDENTE
                _mPedido.Save(_mPedido);

                Alert.Message("Pronto!", "Finalizado com sucesso.", Alert.AlertType.success);

                if (AlertOptions.Message("Impressão?", "Deseja imprimir?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo, true))
                {
                    
                }

                Close();
            }

            if (Home.pedidoPage == "Compras")
            {
                Model.Pedido Pedido = _mPedido.FindById(Id).First<Model.Pedido>();
                Pedido.Tipo = "Compras";
                Pedido.Save(Pedido);

                OpenForm.Show<PedidoPagamentos>(this);
            }
            else
            {
                Model.Pedido Pedido = _mPedido.FindById(Id).First<Model.Pedido>();
                Pedido.Tipo = "Vendas";
                Pedido.Save(Pedido);

                if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Consignações")
                {
                    Home.pedidoPage = "Vendas";
                    Alert.Message("Tudo certo!", "Venda gerada com sucesso.", Alert.AlertType.success);
                    LoadData();
                }
                else
                {
                    OpenForm.Show<PedidoPagamentos>(this);
                }
            }

            //if (Home.pedidoPage != "Compras")
            //{
            //    var Pedido = _mPedido.FindById(Id).First<Model.Pedido>();
            //    Pedido.Tipo = "Vendas";
            //    Pedido.Save(Pedido);

            //    if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Consignações")
            //    {
            //        Home.pedidoPage = "Vendas";
            //        Alert.Message("Tudo certo!", "Venda gerada com sucesso.", Alert.AlertType.success);
            //        LoadData();
            //    }
            //    else
            //    {
            //        OpenForm.Show<PedidoPagamentos>(this);
            //    }
            //}
            //else
            //{
            //    var Pedido = _mPedido.FindById(Id).First<Model.Pedido>();
            //    Pedido.Tipo = "Compras";
            //    Pedido.Save(Pedido);

            //    OpenForm.Show<PedidoPagamentos>(this);
            //}
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
        private void ModalClientes()
        {
            PedidoModalClientes form = new PedidoModalClientes();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _mPedido.Id = Id;
                _mPedido.Cliente = PedidoModalClientes.Id;
                _mPedido.Save(_mPedido);
                LoadData();
            }
        }

        /// <summary>
        /// Janela para selecionar vendedor no pedido.
        /// </summary>
        private void ModalColaborador()
        {
            PedidoModalVendedor form = new PedidoModalVendedor();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _mPedido.Id = Id;
                _mPedido.Colaborador = PedidoModalVendedor.Id;
                _mPedido.Save(_mPedido);
                LoadData();
            }
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
                if (Home.pedidoPage == "Compras")
                    new Controller.Estoque(pedidoItem.GetLastId(), 0, Home.pedidoPage).Add().Item();
                else
                    new Controller.Estoque(pedidoItem.GetLastId(), 0, Home.pedidoPage).Remove().Item();

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
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(false, GridListaProdutos);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(true, GridListaProdutos);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    Close();
                    break;
                case Keys.F1:
                    AlterarModo();
                    break;
                case Keys.F2:
                    BuscarProduto.Focus();
                    break;
                case Keys.F3:

                    if (GridListaProdutos.SelectedRows.Count > 0)
                    {
                        if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                        {
                            var itemName = GridListaProdutos.SelectedRows[0].Cells["Nome do Produto"].Value;
                            if (MessageBox.Show($"Cancelar o item ('{itemName}') no pedido?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                var idPedidoItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                                _mPedidoItens.Remove(idPedidoItem);

                                GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);

                                if (Home.pedidoPage != "Compras")
                                    new Controller.Estoque(idPedidoItem, 0, Home.pedidoPage).Add().Item();
                                else
                                    new Controller.Estoque(idPedidoItem, 0, Home.pedidoPage).Remove().Item();

                                LoadTotais();
                            }
                        }
                    }

                    break;
                case Keys.F7:
                    ModalClientes();
                    break;
                case Keys.F8:
                    ModalColaborador();
                    break;
                case Keys.F10:
                    TelaPagamentos();
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
            ModoRapido.KeyDown += KeyDowns;
            BuscarProduto.KeyDown += KeyDowns;
            SelecionarCliente.KeyDown += KeyDowns;
            SelecionarColaborador.KeyDown += KeyDowns;
            GridListaProdutos.KeyDown += KeyDowns;
            btnConcluir.KeyDown += KeyDowns;
            Quantidade.KeyDown += KeyDowns;

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

            btnConcluir.Click += (s, e) => TelaPagamentos();
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
                    if (String.IsNullOrEmpty(BuscarProduto.Text)) BuscarProduto.Focus();
                    else if (ModoRapAva == 1 && !String.IsNullOrEmpty(BuscarProduto.Text)) Preco.Focus();
                    else { LoadItens(); ClearForms(); }
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
                if (GridListaProdutos.SelectedRows.Count > 0)
                {
                    if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                        IdPedidoItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                }

                PedidoModalCancelItem cancel = new PedidoModalCancelItem();
                if (cancel.ShowDialog() == DialogResult.OK)
                    GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);
            };

            FormClosing += (s, e) =>
            {
                if (!btnFinalizado)
                {
                    Home.pedidoPage = CachePage;
                    if (Home.pedidoPage == "Compras" || Home.pedidoPage == "Vendas")
                    {
                        if (MessageBox.Show($"Você está prestes a excluir o Pedido! Deseja continuar?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            new Controller.Estoque(Id, 0, Home.pedidoPage).Add().Pedido();
                            _mPedido.Remove(Id);
                            return;
                        }

                        e.Cancel = true;
                    }
                }
                btnFinalizado = false;
            };
        }
    }
}

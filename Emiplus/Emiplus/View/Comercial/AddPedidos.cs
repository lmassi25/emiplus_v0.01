using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
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
        public static int IdItem { get; set; } // Id item datagrid
        public static string searchItemTexto { get; set; }

        public static int Id { get; set; }

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.ItemEstoqueMovimentacao _mItemEstoque = new Model.ItemEstoqueMovimentacao();

        private Controller.PedidoItem _controllerPedidoItem = new Controller.PedidoItem();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public AddPedidos()
        {
            InitializeComponent();
            Events();
        }
        
        private void LoadData()
        {
            label2.Text = "Dados do Pedido: " + Id;

            LoadCliente();
            LoadColaboradorCaixa();
            LoadTotais();
        }

        /// <summary>
        /// Carrega o cliente selecionado.
        /// </summary>
        private void LoadCliente()
        {
            if (_mPedido.Cliente > 0)
            {
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
                var data = _mCliente.FindById(_mPedido.Colaborador).First<Model.Pessoa>();
                nomeVendedor.Text = data.Nome;
            }
        }

        /// <summary>
        /// Carrega e adiciona os itens no pedido!
        /// Function: collection.Lookup recupera o ID
        /// </summary>
        private void LoadItens()
        {
            if (collection.Lookup(BuscarProduto.Text) == 0)
            {
                if ((Application.OpenForms["PedidoModalItens"] as PedidoModalItens) == null)
                {
                    searchItemTexto = BuscarProduto.Text;
                    PedidoModalItens form = new PedidoModalItens();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        BuscarProduto.Text = PedidoModalItens.NomeProduto;

                        if (PedidoModalItens.ValorVendaProduto == 0)
                        {
                            if (ModoRapAva == 0)
                            {
                                AlterarModo();
                                ModoRapAvaConfig = 1;
                            }
                        }
                    }
                }
            }

            if (collection.Lookup(BuscarProduto.Text) > 0 && String.IsNullOrEmpty(PedidoModalItens.NomeProduto))
            {
                var item = _mItem.FindById(collection.Lookup(BuscarProduto.Text)).Where("excluir", 0).Where("tipo", "Produtos").First<Model.Item>();

                var pedidoItem = new Model.PedidoItem();

                pedidoItem.SetId(0)
                    .SetTipo("Produtos")
                    .SetPedidoId(Id)
                    .SetItem(item)
                    .SetQuantidade(Validation.ConvertToDouble(Quantidade.Text))
                    .SetMedida(Medidas.Text)
                    .SetDescontoReal(Validation.ConvertToDouble(DescontoReais.Text));

                var valorVenda = pedidoItem.SetValorVenda(Validation.ConvertToDouble(Preco.Text));
                if (!valorVenda)
                {
                    Alert.Message("Oppss", "É necessário definir um valor de venda.", Alert.AlertType.info);
                    if (ModoRapAva == 0)
                    {
                        AlterarModo();
                        ModoRapAvaConfig = 1;
                    }
                    Preco.Select();
                    return;
                }

                pedidoItem.SomarProdutosTotal();
                pedidoItem.SetDescontoPorcentagens(Validation.ConvertToDouble(DescontoPorcentagem.Text));
                pedidoItem.SomarTotal();
                pedidoItem.Save(pedidoItem);

                _mItemEstoque.SetUsuario(0)
                    .SetQuantidade(Validation.ConvertToDouble(Quantidade.Text) == 0 ? 1 : Validation.ConvertToDouble(Quantidade.Text))
                    .SetTipo("R")
                    .SetLocal("Tela de Vendas")
                    .SetItem(item)
                    .Save(_mItemEstoque);

                _controllerPedidoItem.GetDataTableItens(GridListaProdutos, Id);

                LoadTotais();
                
                ClearForms();

                if (ModoRapAva == 1 && ModoRapAvaConfig == 1)
                {
                    AlterarModo();
                    ModoRapAvaConfig = 0;
                }
            }

            PedidoModalItens.NomeProduto = "";
            BuscarProduto.Select();
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
                MessageBox.Show("Seu pedido não contém itens! Adicione pelo menos 1 item para prosseguir para os recebimentos.", "Opsss");
                return;
            }

            OpenForm.Show<PedidoPagamentos>(this);
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
                                var IdItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                                _mPedidoItens.Remove(IdItem);
                                GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);

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
        private void Events()
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
                    _mPedido.Id = Id;
                    _mPedido.Cliente = 1;
                    _mPedido.Tipo = Home.pedidoPage;
                    if (_mPedido.Save(_mPedido))
                    {
                        Id = _mPedido.GetLastId();
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
                Eventos.MaskPrice(ref txt);
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
                        IdItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                }

                PedidoModalCancelItem cancel = new PedidoModalCancelItem();
                if (cancel.ShowDialog() == DialogResult.OK)
                    LoadItens();
            };
        }
    }
}

using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.View.Common;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddPedidos : Form
    {
        private int ModoRapAva { get; set; }
        private int ModoRapAvaConfig { get; set; }
        public static int IdItem { get; set; } // Id item datagrid

        public static int Id;

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();

        private Controller.PedidoItem _controllerPedidoItem = new Controller.PedidoItem();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public static string searchItemTexto { get; set; }

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

        private void LoadCliente()
        {
            if (_mPedido.Cliente > 0)
            {
                var data = _mCliente.FindById(_mPedido.Cliente).First<Model.Pessoa>();
                nomeCliente.Text = data.Nome;
            }
        }

        private void LoadColaboradorCaixa()
        {
            if (_mPedido.Colaborador > 0)
            {
                var data = _mCliente.FindById(_mPedido.Colaborador).First<Model.Pessoa>();
                nomeVendedor.Text = data.Nome;
            }
        }

        // collection.Lookup recupera o ID
        private void LoadItens()
        {
            if (collection.Lookup(BuscarProduto.Text) == 0)
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

        private void LoadTotais()
        {
            itens.Text = "Itens: " + GridListaProdutos.RowCount.ToString();
            // SELECT SUM(total), SUM(desconto), SUM(totalvenda), SUM(frete), SUM(icmsbase), SUM(icmsvlr), SUM(icmsstbase), SUM(icmsstvlr), SUM(ipivlr), SUM(pisvlr), SUM(cofinsvlr)
            // total = (totalvenda, frete, icmsbase, icmsvlr, icmsstbase, icmsstvlr, ipivlr, pisvlr, cofinsvlr) - desconto
            //Math.Round(value, 2);

            var query = _mPedidoItens.Query().SelectRaw("SUM(total) AS total, SUM(desconto) AS desconto, SUM(totalvenda) AS totalvenda, SUM(frete) AS frete, SUM(icmsbase) AS icmsbase, SUM(icmsvlr) AS icmsvlr," +
                " SUM(icmsstbase) AS icmsstbase, SUM(icmsstvlr) as icmsstvlr, SUM(ipivlr) AS ipivlr, SUM(pisvlr) AS pisvlr, SUM(cofinsvlr) AS cofinsvlr")
                .Where("pedido", Id).Where("excluir", 0).Get();

            double Produtos = 0, Frete = 0, Desconto = 0, IPI = 0, ICMSBASE = 0, ICMS = 0, ICMSSTBASE = 0, ICMSST = 0, COFINS = 0, PIS = 0, TOTAL = 0;

            for (int i = 0; i < query.Count(); i++)
            {
                //total do produto  = quantidade * valorvenda
                //total = (totaldoproduto + impostos + frete) - desconto
                var data = query.ElementAt(i);
                //var somar = data.TOTALVENDA + data.FRETE + data.ICMSSTVLR + data.IPIVLR;
                //var subtrair = data.DESCONTO;
                //Totais = somar - subtrair;

                Produtos = Validation.ConvertToDouble(data.TOTALVENDA);
                Frete = Validation.ConvertToDouble(data.FRETE);
                Desconto = Validation.ConvertToDouble(data.DESCONTO);
                IPI = Validation.ConvertToDouble(data.IPIVLR);
                ICMSBASE = Validation.ConvertToDouble(data.ICMSBASE);
                ICMS = Validation.ConvertToDouble(data.ICMSVLR);
                ICMSSTBASE = Validation.ConvertToDouble(data.ICMSSTBASE);
                ICMSST = Validation.ConvertToDouble(data.ICMSSTVLR);
                COFINS = Validation.ConvertToDouble(data.COFINSVLR);
                PIS = Validation.ConvertToDouble(data.PISVLR);
            }
            
            _mPedido.Id = Id;

            _mPedido.Produtos = Produtos;

            _mPedido.Frete = Frete;

            //PEDIDO_ITEM.DESCONTO = PEDIDO_ITEM.DESCONTOITEM + PEDIDO_ITEM.DESCONTOPEDIDO
            //PEDIDO.DESCONTO = SUM(PEDIDO_ITEM.DESCONTO)

            //PEDIDO.DESCONTOLANCADO = form PedidoPayDesconto
            //PEDIDO.DESCONTOLANCADO = PEGAR O VALOR DO DESCONTO(TELA PedidoPayDesconto), DIVIDIR ENTRE OS ITENS DE FORMA PROPORCIONAL E LANÇAR O RESULTADO NO CAMPO PEDIDO_ITEM.DESCONTOPEDIDO DE CADA UM (CRIAR)

            _mPedido.Desconto = Desconto; // não é o desconto lançado no pedido 

            _mPedido.IPI = IPI;

            _mPedido.ICMSBASE = ICMSBASE;
            _mPedido.ICMS = ICMS;

            _mPedido.ICMSSTBASE = ICMSSTBASE;
            _mPedido.ICMSST = ICMSST;

            _mPedido.COFINS = COFINS;
            _mPedido.PIS = PIS;

            TOTAL = (Produtos + Frete + IPI + ICMSST) - Desconto;
            _mPedido.Total = TOTAL;

            _mPedido.Save(_mPedido);

            subTotal.Text = Validation.FormatPrice(TOTAL, true);

            totaisDescontos.Text = "Totais descontos: " + Validation.FormatPrice(Desconto, true);
        }
        
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }
        
        private void TelaPagamentos()
        {
            if (GridListaProdutos.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Seu pedido não contém itens! Adicione pelo menos 1 item para prosseguir para os recebimentos.", "Opsss");
                return;
            }

            OpenForm.Show<PedidoPagamentos>(this);
        }

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

        private void ClearForms()
        {
            BuscarProduto.Clear();
            Quantidade.Clear();
            Preco.Clear();
            DescontoPorcentagem.Clear();
            DescontoReais.Clear();
        }

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

        private void Events()
        {
            BuscarProduto.Select();

            this.KeyDown += KeyDowns;
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
            BuscarProduto.Enter += (s, e) => LoadItens();
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
                    {
                        IdItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                    }
                }

                PedidoModalCancelItem cancel = new PedidoModalCancelItem();
                if (cancel.ShowDialog() == DialogResult.OK)
                {
                    LoadItens();
                }
            };
        }
    }
}

using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddPedidos : Form
    {
        private int ModoRapAva { get; set; }
        public int Id = Pedido.Id;

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Pessoa _mColaborador = new Model.Pessoa();

        private Controller.Pedido _controllerPedido = new Controller.Pedido();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public static string searchItemTexto { get; set; }

        public AddPedidos()
        {
            InitializeComponent();

            BuscarProduto.Select();

            this.KeyDown += KeyDowns;
            ModoRapido.KeyDown += KeyDowns;
            BuscarProduto.KeyDown += KeyDowns;
            SelecionarCliente.KeyDown += KeyDowns;
            SelecionarColaborador.KeyDown += KeyDowns;
            GridListaProdutos.KeyDown += KeyDowns;
            btnConcluir.KeyDown += KeyDowns;
            Quantidade.KeyDown += KeyDowns;
        }
        
        private void AddPedidos_Load(object sender, EventArgs e)
        {
            AutoCompleteItens();

            if (Id > 0)
            {
                LoadData();
            }
            else
            {
                _mPedido.Id = Id;
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
        }
        
        private void LoadData()
        {
            label2.Text = "Dados do Pedido: " + Id;

            LoadCliente();
            LoadColaboradorCaixa();
            LoadTabelaPreco();
            //LoadItens();
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
                var data = _mColaborador.FindById(_mPedido.Colaborador).First<Model.Pessoa>();
                nomeVendedor.Text = data.Nome;
            }
        }

        private void LoadTabelaPreco()
        {

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
                    //if (!String.IsNullOrEmpty(PedidoModalItens.NomeProduto))
                    //{
                        BuscarProduto.Text = PedidoModalItens.NomeProduto;
                    //}
                }
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                var item = _mItem.FindById(collection.Lookup(BuscarProduto.Text)).Where("excluir", 0).Where("tipo", 0).First<Model.Item>();

                var pedidoItem = new Model.PedidoItem();

                pedidoItem.SetId(0)
                .SetPedidoId(Id)
                .SetItem(item)
                .SetQuantidade(Validation.ConvertToDouble(Quantidade.Text))
                .SetValorVenda(Validation.ConvertToDouble(Preco.Text))
                .SetMedida(Medidas.Text)
                .SomarProdutosTotal()
                .SetDescontoReal(Validation.ConvertToDouble(DescontoReais.Text))
                .SetDescontoPorcentagens(Validation.ConvertToDouble(DescontoPorcentagem.Text))
                .SomarTotal()
                .Save(pedidoItem);

                _controllerPedido.GetDataTableItens(GridListaProdutos, item.Id, pedidoItem);
                itens.Text = "Itens: " + GridListaProdutos.RowCount.ToString();

                ClearForms();
            }
            
            BuscarProduto.Select();
        }

        private void LoadTotais()
        {
            var query = new Query("PEDIDO_ITEM")
                 .Select("GroupId")
                 .SelectRaw("SUM(`Age`)")
                 .GroupBy("GroupId");
        }
        
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", 0).Get();

            foreach (var itens in item)
            {
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }
        
        private void Produto_Click(object sender, EventArgs e)
        {
            var BackColor = Color.FromArgb(249, 249, 249);
            panelTwo.BackColor = BackColor;
            label4.BackColor = BackColor;
            label5.BackColor = BackColor;
            label6.BackColor = BackColor;
            label7.BackColor = BackColor;
            label8.BackColor = BackColor;
            label9.BackColor = BackColor;
            addProduto.BackColor = BackColor;
            panel2.BackColor = Color.White;
        }

        private void Produto_Leave(object sender, EventArgs e)
        {
            var BackColor = Color.White;
            panelTwo.BackColor = Color.White;
            label4.BackColor = BackColor;
            label5.BackColor = BackColor;
            label6.BackColor = BackColor;
            label7.BackColor = BackColor;
            label8.BackColor = BackColor;
            label9.BackColor = BackColor;
            addProduto.BackColor = BackColor;
            panel2.BackColor = Color.FromArgb(249, 249, 249);
        }

        private void TelaPagamentos()
        {
            if (btnConcluir.Enabled == true)
            {
                OpenForm.Show<PedidoPagamentos>(this);
            }
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            TelaPagamentos();
        }

        private void AlterarModo()
        {
            if (ModoRapAva == 1)
            {
                ModoRapAva = 0;
                panelAvancado.Visible = false;
                ModoRapido.Text = "Modo Avançado (F1) ?";
                Quantidade.Enabled = false;
                Quantidade.TabStop = true;
            }
            else
            {
                ModoRapAva = 1;
                panelAvancado.Visible = true;
                ModoRapido.Text = "Modo Rápido (F1) ?";
                Quantidade.Enabled = true;
            }
        }

        private void ModoRapido_Click(object sender, EventArgs e)
        {
            AlterarModo();
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

        private void SelecionarCliente_Click(object sender, EventArgs e)
        {
            ModalClientes();
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

        private void SelecionarColaborador_Click(object sender, EventArgs e)
        {
            ModalColaborador();
        }

        private void BuscarProduto_Enter(object sender, EventArgs e)
        {
            LoadItens();
        }

        private void BuscarProduto_TextChanged(object sender, EventArgs e)
        {
        }

        private void BuscarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadItens();
                BuscarProduto.Clear();
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
                case Keys.F7:
                    ModalClientes();
                    break;
                case Keys.F8:
                    ModalColaborador();
                    break;
                case Keys.F10:
                    TelaPagamentos();
                    break;
                case Keys.Enter:

                    if (Validation.Event(sender, GridListaProdutos))
                    {
                        MessageBox.Show(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                    }

                    break;
            }
        }

        private void Preco_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            Eventos.MaskPrice(ref txt);
        }

        private void Quantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (String.IsNullOrEmpty(BuscarProduto.Text)) BuscarProduto.Focus();
                else if (ModoRapAva == 1 && !String.IsNullOrEmpty(BuscarProduto.Text)) Preco.Focus();
                else { LoadItens(); ClearForms(); }
            }
        }
    }
}

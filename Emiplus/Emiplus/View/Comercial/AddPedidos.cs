using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata.Execution;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddPedidos : Form
    {
        private int ModoRapAva { get; set; }
        public int Id = Pedido.Id;

        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.Pessoa _mColaborador = new Model.Pessoa();

        private Controller.Pedido _controllerPedido = new Controller.Pedido();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public static string searchItemTexto { get; set; }

        public AddPedidos()
        {
            InitializeComponent();
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
                _mCliente.FindById(_mPedido.Cliente).First<Model.Pessoa>();
                nomeCliente.Text = _mCliente.Nome;
            }
        }

        private void LoadColaboradorCaixa()
        {
            if (_mPedido.Colaborador > 0)
            {
                _mColaborador.FindById(_mPedido.Colaborador).First<Model.Pessoa>();
                nomeVendedor.Text = _mColaborador.Nome;
            }
        }

        private void LoadTabelaPreco()
        {

        }

        private void AutoCompleteItens()
        {
            var item = new Model.Item().Query().Select("id", "nome")
                .Where("excluir", 0)
                .Where("tipo", 0)
                .Get();

            foreach (var itens in item)
            {
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private void LoadItens()
        {
            _controllerPedido.GetDataTableItens(GridListaProdutos, collection.Lookup(BuscarProduto.Text));

            if (collection.Lookup(BuscarProduto.Text) == 0)
            {
                searchItemTexto = BuscarProduto.Text;
                PedidoModalItens form = new PedidoModalItens();
                form.ShowDialog();
            }
        }

        private void Pedidos_Load(object sender, EventArgs e)
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
        }

        private void Label11_Click(object sender, EventArgs e)
        {

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

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            OpenForm.Show<PedidoPagamentos>(this);
        }

        private void GridListaProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AlterarModo()
        {
            if (ModoRapAva == 1)
            {
                ModoRapAva = 0;
                panelAvancado.Visible = true;
                ModoRapido.Text = "Modo Rápido (F1) ?";
                Quantidade.Enabled = true;
            }
            else
            {
                ModoRapAva = 1;
                panelAvancado.Visible = false;
                ModoRapido.Text = "Modo Avançado (F1) ?";
                btnAlterarQtd.Visible = true;
                btnAlterarQtd.Top = 52;
                btnAlterarQtd.TabIndex = 2;
                Quantidade.Enabled = false;
                Quantidade.TabStop = true;
            }
        }

        private void ModoRapido_Click(object sender, EventArgs e)
        {
            AlterarModo();
        }

        private void SelecionarCliente_Click(object sender, EventArgs e)
        {
            PedidoModalClientes form = new PedidoModalClientes();
            form.ShowDialog();
        }

        private void SelecionarColaborador_Click(object sender, EventArgs e)
        {
            PedidoModalVendedor form = new PedidoModalVendedor();
            form.ShowDialog();
        }

        private void BuscarProduto_Enter(object sender, EventArgs e)
        {

            LoadItens();
        }

        private void BuscarProduto_TextChanged(object sender, EventArgs e)
        {
        }

        private void BuscarProduto_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BuscarProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadItens();
            }
        }

        private void searchItem_Click(object sender, EventArgs e)
        {
            PedidoModalItens form = new PedidoModalItens();
            form.ShowDialog();
        }

        private void BtnAlterarQtd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ola");
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.F1:
                    MessageBox.Show("selecionar o item");
                    break;
                case Keys.F10:
                    MessageBox.Show("selecionar o item");
                    break;
                case Keys.Enter:

                    if (Validation.Event(sender, GridListaProdutos))
                    {
                        MessageBox.Show(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                    }

                    break;
            }
        }
    }
}

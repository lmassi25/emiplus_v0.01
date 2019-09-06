using Emiplus.Data.Helpers;
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
            LoadItens();
        }

        private void LoadCliente()
        {
            if(_mPedido.Cliente > 0)
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

        private void LoadItens()
        {

        }

        private void Pedidos_Load(object sender, EventArgs e)
        {            
            if(Id > 0)
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
            button3.BackColor = BackColor;
            panel3.BackColor = Color.White;
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
            button3.BackColor = BackColor;
            panel3.BackColor = Color.FromArgb(249, 249, 249);
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
                ModoRapido.Text = "Modo Rápido (F5) ?";
                Quantidade.Enabled = true;
            }
            else
            {
                ModoRapAva = 1;
                panelAvancado.Visible = false;
                ModoRapido.Text = "Modo Avançado (F5) ?";
                btnAlterarQtd.Visible = true;
                btnAlterarQtd.Top = 53;
                Quantidade.Enabled = false;
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
    }
}

using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Windows.Forms;
using Emiplus.View.Common;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;

namespace Emiplus.View.Comercial
{
    using Model;
    using System.Collections.Generic;

    public partial class AddClientes : Form
    {
        private Controller.Pessoa _controller = new Controller.Pessoa();
        private Pessoa _modelPessoa = new Pessoa();
        private PessoaEndereco _modelAddress = new PessoaEndereco();
        private PessoaContato _modelContact = new PessoaContato();

        private string page = TelaComercialInicial.page;
        public static int Id { get; set; }
        public static int IdAddress { get; set; }
        public static int IdContact { get; set; }

        private int Backspace = 0;

        private int IdClientePedido = PedidoModalClientes.Id; // Tela pedidos
        private string pageClientePedido = PedidoModalClientes.page; // Tela pedidos

        public AddClientes()
        {
            InitializeComponent();
            Id = Clientes.Id;

            if (String.IsNullOrEmpty(page) && Validation.IsNumber(Id))
            {
                Id = 0;
                page = "Clientes";
            }

            label6.Text = page;
            label1.Text = "Adicionar " + page;
            label1.Left = 307;
            pictureBox2.Left = 284;

            if (page == "Fornecedores")
            {
                label1.Left = 322;
                pictureBox2.Left = 299;
            }

            tabControl1.TabPages.Remove(tabTransporte); // Tab 'Transporte'
            if (page == "Transportadoras")
            {
                label1.Left = 338;
                pictureBox2.Left = 315;
                tabControl1.TabPages.Add(tabTransporte);
            }

            //if(Id == 0)
            //{
            //    data.Criado = DateTime.Now;
            //    if (Data(data).Create() == 1)
            //}

            if (Id > 0)
            {
                tabControl1.Visible = true;
                btnRemover.Visible = true;
                LoadData();
            }
        }

        private void DataTableAddress()
        {
            _controller.GetDataTableEnderecos(ListaEnderecos, Id);
        }

        private void DataTableContatos()
        {
            _controller.GetDataTableContato(ListaContatos, Id);
        }

        private void GetContato()
        {
            if (ListaContatos.SelectedCells.Count == 0)
            {
                Alert.Message("Opss", "Selecione um contato para editar!", Alert.AlertType.info);
                return;
            }

            IdContact = Convert.ToInt32(ListaContatos.SelectedRows[0].Cells["ID"].Value);
            AddClienteContato form = new AddClienteContato();
            if (form.ShowDialog() == DialogResult.OK)
                SetFocus();
        }

        private void GetEndereco()
        {
            if (ListaEnderecos.SelectedCells.Count == 0)
            {
                Alert.Message("Opss", "Selecione um endereço para editar!", Alert.AlertType.info);
                return;
            }

            IdAddress = Convert.ToInt32(ListaEnderecos.SelectedRows[0].Cells["ID"].Value);
            AddClienteEndereco form = new AddClienteEndereco();
            if (form.ShowDialog() == DialogResult.OK)
                SetFocus();
        }

        private void LoadData()
        {
            _modelPessoa = _modelPessoa.FindById(Id).First<Pessoa>();

            nomeRS.Text = _modelPessoa.Nome;
            nomeFantasia.Text = _modelPessoa.Fantasia;
            nascimento.Text = _modelPessoa.Aniversario;
            cpfCnpj.Text = _modelPessoa.CPF;
            rgIE.Text = _modelPessoa.RG;
            pessoaJF.SelectedItem = _modelPessoa.Pessoatipo;
            Isento.Checked = _modelPessoa.Isento == 1 ? true : false;
        }

        private void BtnAdicionarEndereco_Click(object sender, EventArgs e)
        {
            IdAddress = 0;
            AddClienteEndereco form = new AddClienteEndereco();
            if (form.ShowDialog() == DialogResult.OK)
                SetFocus();
        }

        private void BtnEditarEndereco_Click(object sender, EventArgs e)
        {
            GetEndereco();
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdicionarContato_Click(object sender, EventArgs e)
        {
            IdContact = 0;
            AddClienteContato form = new AddClienteContato();
            if (form.ShowDialog() == DialogResult.OK)
                Focus();
        }

        private void AddClientes_Load(object sender, EventArgs e)
        {
            DataTableAddress();
            DataTableContatos();

            pessoaJF.DataSource = new List<String> { "Física", "Jurídica" };
            pessoaJF.SelectedItem = "Física";

            SetFocus();
        }

        private void AddClientes_Activated(object sender, EventArgs e)
        {
            DataTableAddress();
            DataTableContatos();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja realmente excluir o cliente?", "Atenção!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (_modelPessoa.Remove(Id))
                    Close();
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            _modelPessoa.Id = Id;
            _modelPessoa.Tipo = page;
            _modelPessoa.Nome = nomeRS.Text;
            _modelPessoa.Fantasia = nomeFantasia.Text;
            _modelPessoa.Aniversario = nascimento.Text;
            _modelPessoa.CPF = cpfCnpj.Text;
            _modelPessoa.RG = rgIE.Text;
            _modelPessoa.Pessoatipo = pessoaJF.Text;
            _modelPessoa.Isento = Isento.Checked ? 1 : 0;

            if (_modelPessoa.Save(_modelPessoa))
            {
                tabControl1.Visible = true;
                Id = _modelPessoa.GetLastId();
                Close();
            }
        }

        private void BtnEditarContato_Click(object sender, EventArgs e)
        {
            GetContato();
        }

        private void VisualGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CpfCnpj_TextChanged(object sender, EventArgs e)
        {
            ChangeMask();
        }

        private void CpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                Backspace = 1;
            }
            else
            {
                Backspace = 0;
            }
        }

        private void ChangeMask()
        {
            if (cpfCnpj.Text != "")
            {
                if (Backspace == 0)
                {
                    cpfCnpj.Text = Validation.ChangeMaskCPFCNPJ(cpfCnpj.Text, pessoaJF.Text);
                    cpfCnpj.Select(cpfCnpj.Text.Length, 0);
                }
            }
        }

        private void NomeRS_Enter(object sender, EventArgs e)
        {
            DataTableAddress();
            DataTableContatos();
        }
        
        private void PessoaJF_SelectionChangeCommitted(object sender, EventArgs e)
        {            
            if (pessoaJF.Text == "Física")
            {
                Isento.Checked = true;
            }
            else
            {
                Isento.Checked = false;
            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            
        }

        private void Label6_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void SetFocus()
        {
            nomeRS.Focus();
        }

        private void ListaEnderecos_DoubleClick(object sender, EventArgs e)
        {
            GetEndereco();
        }

        private void ListaContatos_DoubleClick(object sender, EventArgs e)
        {
            GetContato();
        }
    }
}
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Windows.Forms;

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

        public static int Id { get; set; }
        public static int IdAddress { get; set; }
        public static int IdContact { get; set; }

        private int IdClientePedido = PedidoModalClientes.Id; // Tela pedidos
        private string pageClientePedido = PedidoModalClientes.page; // Tela pedidos

        public string btnSalvarText
        {
            get
            {
                return btnSalvar.Text;
            }
            set
            {
                btnSalvar.Text = value;
            }
        }
        public int btnSalvarWidth
        {
            get
            {
                return btnSalvar.Width;
            }
            set
            {
                btnSalvar.Width = value;
            }
        }
        public int btnSalvarLocation
        {
            get
            {
                return btnSalvar.Left;
            }
            set
            {
                btnSalvar.Left = value;
            }
        }

        public AddClientes()
        {
            InitializeComponent();
            Id = Clientes.Id;

            if (String.IsNullOrEmpty(Home.pessoaPage) && Validation.IsNumber(Id))
            {
                Id = 0;
                Home.pessoaPage = "Clientes";
            }

            label6.Text = Home.pessoaPage;
            label1.Text = "Adicionar " + Home.pessoaPage;
            label1.Left = 307;
            pictureBox2.Left = 284;

            if (Home.pessoaPage == "Fornecedores")
            {
                label1.Left = 343;
                pictureBox2.Left = 319;
            }

            credencial.TabPages.Remove(tabTransporte); // Tab 'Transporte'
            if (Home.pessoaPage == "Transportadoras")
            {
                label1.Left = 359;
                pictureBox2.Left = 335;
                credencial.TabPages.Add(tabTransporte);
                uf.DataSource = new List<String> { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
            }

            if (Id == 0)
            {
                _modelPessoa.Id = Id;
                _modelPessoa.Tipo = Home.pessoaPage;
                _modelPessoa.Nome = "Novo registro";
                if (_modelPessoa.Save(_modelPessoa))
                {
                    Id = _modelPessoa.GetLastId();
                }
            }

            if (Id > 0)
            {
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
            nomeFantasia.Text = _modelPessoa.Fantasia == null ? "" : _modelPessoa.Fantasia;
            nascimento.Text = _modelPessoa.Aniversario == null ? "" : Validation.ConvertDateToForm(_modelPessoa.Aniversario);
            cpfCnpj.Text = _modelPessoa.CPF == null ? "" : _modelPessoa.CPF;
            rgIE.Text = _modelPessoa.RG == null ? "" : _modelPessoa.RG;
            pessoaJF.SelectedItem = _modelPessoa.Pessoatipo == null ? "" : _modelPessoa.Pessoatipo;
            Isento.Checked = _modelPessoa.Isento == 1 ? true : false;

            if (Home.pessoaPage == "Transportadoras")
            {
                placa.Text = _modelPessoa.Transporte_placa == null ? "" : _modelPessoa.Transporte_placa;
                uf.Text = _modelPessoa.Transporte_uf == null ? "" : _modelPessoa.Transporte_placa;
                rntc.Text = _modelPessoa.Transporte_rntc == null ? "" : _modelPessoa.Transporte_placa;
            }
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
                SetFocus();
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
            var result = MessageBox.Show("Deseja realmente excluir?", "Atenção!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (_modelPessoa.Remove(Id))
                    Close();
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            _modelPessoa.Id = Id;
            _modelPessoa.Tipo = Home.pessoaPage;
            _modelPessoa.Nome = nomeRS.Text;
            _modelPessoa.Fantasia = nomeFantasia.Text;
            _modelPessoa.Aniversario = Validation.ConvertDateToSQL(nascimento.Text);
            _modelPessoa.CPF = cpfCnpj.Text;
            _modelPessoa.RG = rgIE.Text;
            _modelPessoa.Pessoatipo = pessoaJF.Text;
            _modelPessoa.Isento = Isento.Checked ? 1 : 0;

            if (Home.pessoaPage == "Transportadoras")
            {
                _modelPessoa.Transporte_placa = placa.Text;
                _modelPessoa.Transporte_uf = uf.Text;
                _modelPessoa.Transporte_rntc = rntc.Text;
            }

            if (_modelPessoa.Save(_modelPessoa))
            {
                Id = _modelPessoa.GetLastId();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BtnEditarContato_Click(object sender, EventArgs e)
        {
            GetContato();
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

        private void CpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (pessoaJF.Text == "Física")
            {
                Eventos.MaskCPF(sender, e);
            }

            if (pessoaJF.Text == "Jurídica")
            {
                Eventos.MaskCNPJ(sender, e);
            }
        }

        private void Nascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Eventos.MaskBirthday(sender, e);
        }
    }
}
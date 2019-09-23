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
            Events();

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
                    Id = _modelPessoa.GetLastId();
            }

            if (Id > 0)
                LoadData();
        }

        private void DataTableAddress()
        {
            _controller.GetDataTableEnderecos(ListaEnderecos, Id);
        }

        private void DataTableContatos()
        {
            _controller.GetDataTableContato(ListaContatos, Id);
        }

        private void GetContato(bool create = false)
        {
            if (create)
            {
                IdContact = 0;
                AddClienteContato addContact = new AddClienteContato();
                if (addContact.ShowDialog() == DialogResult.OK) {
                    SetFocus();
                    DataTableContatos();
                }
                return;
            }

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

        private void GetEndereco(bool create = false)
        {
            if (create)
            {
                IdAddress = 0;
                AddClienteEndereco addAddr = new AddClienteEndereco();
                if (addAddr.ShowDialog() == DialogResult.OK) {
                    SetFocus();
                    DataTableAddress();
                }
                return;
            }

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

        private void SetFocus()
        {
            nomeRS.Focus();
        }

        private void Events()
        {
            Load += (s, e) =>
            {
                DataTableAddress();
                DataTableContatos();

                pessoaJF.DataSource = new List<String> { "Física", "Jurídica" };
                pessoaJF.SelectedItem = "Física";

                SetFocus();
            };
            Activated += (s, e) =>
            {
                DataTableAddress();
                DataTableContatos();
            };

            btnSalvar.Click += (s, e) =>
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
            };
            btnRemover.Click += (s, e) =>
            {
                var result = MessageBox.Show("Deseja realmente excluir?", "Atenção!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (_modelPessoa.Remove(Id))
                        Close();
                }
            };

            btnAdicionarEndereco.Click += (s, e) => GetEndereco(true);
            btnEditarEndereco.Click += (s, e) => GetEndereco();
            ListaEnderecos.DoubleClick += (s, e) => GetEndereco();

            btnAdicionarContato.Click += (s, e) => GetContato(true);
            btnEditarContato.Click += (s, e) => GetContato();
            ListaContatos.DoubleClick += (s, e) => GetContato();

            pessoaJF.SelectionChangeCommitted += (s, e) =>
            {
                if (pessoaJF.Text == "Física")
                    Isento.Checked = true;
                else
                    Isento.Checked = false;
            };

            cpfCnpj.KeyPress += (s, e) =>
            {
                if (pessoaJF.Text == "Física")
                    Eventos.MaskCPF(s, e);

                if (pessoaJF.Text == "Jurídica")
                    Eventos.MaskCNPJ(s, e);
            };
            nascimento.KeyPress += (s, e) => Eventos.MaskBirthday(s, e);

            btnExit.Click += (s, e) => Close();
            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
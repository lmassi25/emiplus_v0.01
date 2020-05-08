using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata.Execution;
using Pessoa = Emiplus.Controller.Pessoa;

namespace Emiplus.View.Comercial
{
    public partial class AddClientes : Form
    {
        private readonly Pessoa _controller = new Pessoa();
        private Model.Pessoa _modelPessoa = new Model.Pessoa();

        private int IdClientePedido = PedidoModalClientes.Id; // Tela pedidos
        private string pageClientePedido = PedidoModalClientes.page; // Tela pedidos

        public AddClientes()
        {
            InitializeComponent();
            Eventos();
        }

        public static int Id { get; set; }
        public static int IdAddress { get; set; }
        public static int IdContact { get; set; }
        private static bool disableRGie { get; set; } = true;

        public string btnSalvarText
        {
            get => btnSalvar.Text;
            set => btnSalvar.Text = value;
        }

        public int btnSalvarWidth
        {
            get => btnSalvar.Width;
            set => btnSalvar.Width = value;
        }

        public int btnSalvarLocation
        {
            get => btnSalvar.Left;
            set => btnSalvar.Left = value;
        }

        public int btnAtivoLocation
        {
            get => Ativo.Left;
            set => Ativo.Left = value;
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
                var addContact = new AddClienteContato {TopMost = true};
                if (addContact.ShowDialog() != DialogResult.OK)
                    return;

                SetFocus();
                DataTableContatos();
                return;
            }

            if (ListaContatos.SelectedCells.Count == 0)
            {
                Alert.Message("Opss", "Selecione um contato para editar!", Alert.AlertType.info);
                return;
            }

            IdContact = Convert.ToInt32(ListaContatos.SelectedRows[0].Cells["ID"].Value);
            var form = new AddClienteContato {TopMost = true};
            if (form.ShowDialog() == DialogResult.OK)
                SetFocus();
        }

        private void GetEndereco(bool create = false)
        {
            if (create)
            {
                IdAddress = 0;
                var addAddr = new AddClienteEndereco {TopMost = true};
                if (addAddr.ShowDialog() != DialogResult.OK)
                    return;

                SetFocus();
                DataTableAddress();

                return;
            }

            if (ListaEnderecos.SelectedCells.Count == 0)
            {
                Alert.Message("Opss", "Selecione um endereço para editar!", Alert.AlertType.info);
                return;
            }

            IdAddress = Convert.ToInt32(ListaEnderecos.SelectedRows[0].Cells["ID"].Value);
            var form = new AddClienteEndereco {TopMost = true};
            if (form.ShowDialog() == DialogResult.OK)
                SetFocus();
        }

        private void LoadData()
        {
            _modelPessoa = _modelPessoa.FindById(Id).First<Model.Pessoa>();

            nomeRS.Text = _modelPessoa?.Nome ?? "";
            nomeFantasia.Text = _modelPessoa?.Fantasia ?? "";
            nascimento.Text = Validation.ConvertDateToForm(_modelPessoa?.Aniversario) ?? "";
            cpfCnpj.Text = _modelPessoa?.CPF ?? "";
            rgIE.Text = _modelPessoa?.RG ?? "";
            pessoaJF.Text = _modelPessoa?.Pessoatipo ?? "Física";
            Isento.Checked = _modelPessoa?.Isento == 1;

            if (Home.pessoaPage == "Transportadoras")
            {
                placa.Text = _modelPessoa?.Transporte_placa ?? "";
                uf.Text = _modelPessoa?.Transporte_uf ?? "";
                rntc.Text = _modelPessoa?.Transporte_rntc ?? "";
            }

            ToolHelp.Show(
                "Nome Fantasia, também conhecido como Nome de Fachada ou Marca Empresarial," + Environment.NewLine +
                "é o nome popular de uma empresa, e pode ou não ser igual à sua razão social.", pictureBox14,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "A sigla RNTRC significa Registro Nacional de Transportadores de Cargas." + Environment.NewLine +
                "É um certificado público feito pela Agência Nacional de Transportes Terrestres (ANTT).", pictureBox10,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");

            switch (Home.pessoaPage)
            {
                default:
                    ToolHelp.Show(
                        "Caso o cliente seja isento de inscrição estadual" + Environment.NewLine + "marque esta opção.",
                        pictureBox13, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                    break;

                case "Fornecedores":
                    ToolHelp.Show(
                        "Caso o fornecedor seja isento de inscrição estadual" + Environment.NewLine +
                        "marque esta opção.", pictureBox13, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                    break;

                case "Transportadoras":
                    ToolHelp.Show(
                        "Caso a transportadora seja isento de inscrição estadual" + Environment.NewLine +
                        "marque esta opção.", pictureBox13, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                    break;
            }
        }

        private void SetFocus() => nomeRS.Focus();

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                Id = Clientes.Id;

                if (string.IsNullOrEmpty(Home.pessoaPage) && Id.IsNumber())
                {
                    Id = 0;
                    Home.pessoaPage = "Clientes";
                }

                label6.Text = Home.pessoaPage;
                label1.Text = @"Adicionar " + Home.pessoaPage;
                label1.Left = 307;
                pictureBox2.Left = 284;
                label4.Text = @"Comercial";

                credencial.TabPages.Remove(tabTransporte); // Tab 'Transporte'
                switch (Home.pessoaPage)
                {
                    case "Fornecedores":
                        pictureBox1.Image = Resources.box;
                        label4.Text = @"Produtos";
                        label1.Left = 343;
                        pictureBox2.Left = 319;
                        break;
                    case "Transportadoras":
                        pictureBox1.Image = Resources.box;
                        label4.Text = @"Produtos";
                        label1.Left = 359;
                        pictureBox2.Left = 335;
                        credencial.TabPages.Add(tabTransporte);
                        uf.DataSource = Support.GetEstados();
                        break;
                }

                DataTableAddress();
                DataTableContatos();

                pessoaJF.DataSource = new List<string> {"Física", "Jurídica"};

                Isento.Checked = pessoaJF.Text == @"Física";
                SetFocus();

                if (Id > 0)
                    LoadData();

                if (Id == 0)
                {
                    _modelPessoa.Id = Id;
                    _modelPessoa.Tipo = Home.pessoaPage;
                    _modelPessoa.Nome = "NOVO REGISTRO";
                    if (_modelPessoa.Save(_modelPessoa))
                        Id = _modelPessoa.GetLastId();
                }
            };

            Activated += (s, e) =>
            {
                DataTableAddress();
                DataTableContatos();
            };

            btnSalvar.Click += (s, e) =>
            {
                if (IniFile.Read("UserNoDocument", "Comercial") == "False" && Home.pessoaPage == "Clientes")
                    if (!string.IsNullOrEmpty(cpfCnpj.Text))
                    {
                        var data = _modelPessoa.Query().Where("id", "!=", Id).Where("CPF", cpfCnpj.Text)
                            .Where("tipo", Home.pessoaPage).Where("excluir", 0).FirstOrDefault();
                        if (data != null)
                        {
                            Alert.Message("Oppss", "Já existe um registro cadastrado com esse CPF/CNPJ.",
                                Alert.AlertType.error);
                            return;
                        }
                    }

                if (Home.pessoaPage != "Clientes")
                    if (!string.IsNullOrEmpty(cpfCnpj.Text))
                    {
                        var data = _modelPessoa.Query().Where("id", "!=", Id).Where("CPF", cpfCnpj.Text)
                            .Where("tipo", Home.pessoaPage).Where("excluir", 0).FirstOrDefault();
                        if (data != null)
                        {
                            Alert.Message("Oppss", "Já existe um registro cadastrado com esse CPF/CNPJ.",
                                Alert.AlertType.error);
                            return;
                        }
                    }

                if (!string.IsNullOrEmpty(nomeRS.Text))
                {
                    var data = _modelPessoa.Query().Where("id", "!=", Id).Where("NOME", nomeRS.Text)
                        .Where("tipo", Home.pessoaPage).Where("excluir", 0).FirstOrDefault();
                    if (data != null)
                    {
                        Alert.Message("Oppss", "Já existe um registro cadastrado com esse NOME.",
                            Alert.AlertType.error);
                        return;
                    }
                }

                _modelPessoa.Id = Id;
                _modelPessoa.Tipo = Home.pessoaPage;
                _modelPessoa.Nome = nomeRS.Text;
                _modelPessoa.Fantasia = nomeFantasia.Text;
                _modelPessoa.Aniversario = Validation.ConvertDateToSql(nascimento.Text);
                _modelPessoa.CPF = cpfCnpj.Text;
                _modelPessoa.RG = rgIE.Text;
                _modelPessoa.Pessoatipo = pessoaJF.Text;
                _modelPessoa.Isento = Isento.Checked ? 1 : 0;

                if (!Isento.Checked)
                    if (pessoaJF.Text == @"Jurídica" && string.IsNullOrEmpty(rgIE.Text))
                    {
                        Alert.Message("Oppss", "Inscrição estadual é obrigatório para pessoa jurídica.",
                            Alert.AlertType.warning);
                        return;
                    }

                if (Home.pessoaPage == "Transportadoras")
                {
                    _modelPessoa.Transporte_placa = placa.Text;
                    _modelPessoa.Transporte_uf = uf.Text;
                    _modelPessoa.Transporte_rntc = rntc.Text;
                }

                _modelPessoa.ativo = Ativo.Toggled ? 0 : 1;

                if (!_modelPessoa.Save(_modelPessoa))
                    return;

                Id = _modelPessoa.GetLastId();
                DialogResult = DialogResult.OK;
                Close();
            };

            btnRemover.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Deseja realmente excluir?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (!result)
                    return;

                if (_modelPessoa.Remove(Id))
                    Close();
            };

            btnAdicionarEndereco.Click += (s, e) => GetEndereco(true);
            btnEditarEndereco.Click += (s, e) => GetEndereco();
            ListaEnderecos.DoubleClick += (s, e) => GetEndereco();
            nomeRS.Enter += (s, e) => DataTableAddress();

            btnAdicionarContato.Click += (s, e) => GetContato(true);
            btnEditarContato.Click += (s, e) => GetContato();
            ListaContatos.DoubleClick += (s, e) => GetContato();
            nomeRS.Enter += (s, e) => DataTableContatos();

            pessoaJF.SelectedIndexChanged += (s, e) => { Isento.Checked = pessoaJF.Text == @"Física"; };

            cpfCnpj.KeyPress += (s, e) =>
            {
                switch (pessoaJF.Text)
                {
                    case "Física":
                        Masks.MaskCPF(s, e);
                        break;
                    case "Jurídica":
                        Masks.MaskCNPJ(s, e);
                        break;
                }
            };

            Isento.Click += (s, e) =>
            {
                if (disableRGie)
                {
                    if (pessoaJF.Text != @"Jurídica")
                        rgIE.Enabled = false;

                    disableRGie = false;
                }
                else
                {
                    rgIE.Enabled = true;
                    disableRGie = true;
                }
            };

            nascimento.KeyPress += Masks.MaskBirthday;
            rgIE.KeyPress += (s, e) => Masks.MaskOnlyNumberAndChar(s, e, 13);
            nomeRS.KeyPress += (s, e) => Masks.MaskOnlyNumberAndChar(s, e, 50);
            nomeFantasia.KeyPress += (s, e) => Masks.MaskOnlyNumberAndChar(s, e, 50);

            btnExit.Click += (s, e) => CloseForm();

            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
            btnVideoAjuda.Click += (s, e) => Support.Video("https://www.youtube.com/watch?v=6o9mR7oNp70");
        }

        private void CloseForm()
        {
            var dataProd = _modelPessoa.Query().Where("id", Id).Where("atualizado", "01.01.0001, 00:00:00.000")
                .FirstOrDefault();
            if (dataProd != null)
            {
                var result = AlertOptions.Message("Atenção!", "Você não salvou esse registro, deseja deletar?",
                    AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var data = _modelPessoa.Remove(Id);
                    if (data)
                        Close();
                }

                nomeRS.Focus();
                return;
            }

            Close();
        }
    }
}
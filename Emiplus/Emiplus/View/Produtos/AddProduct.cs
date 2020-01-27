using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddProduct : Form
    {
        public static int idPdtSelecionado { get; set; }
        private Item _modelItem = new Item();
        private Controller.Item _controllerItem = new Controller.Item();

        private BackgroundWorker backOn = new BackgroundWorker();
        private IEnumerable<dynamic> categorias { get; set; }
        private IEnumerable<dynamic> fornecedores { get; set; }
        private IEnumerable<dynamic> impostos { get; set; }
        private IEnumerable<dynamic> impostos2 { get; set; }

        public AddProduct()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadFornecedores()
        {
            Fornecedor.Refresh();

            //var fornecedor = new Pessoa().FindAll().Where("tipo", "Fornecedores").WhereFalse("excluir").OrderByDesc("nome").Get();
            if (fornecedores.Count() > 0)
            {
                Fornecedor.DataSource = fornecedores;
                Fornecedor.DisplayMember = "NOME";
                Fornecedor.ValueMember = "ID";
            }
        }

        private void Start()
        {
            ToolHelp.Show("Para selecionar a categoria do produto, a mesma deve estar cadastrada previamente.\nPara cadastrar uma nova categoria acesse Produtos>Categorias>Adicionar.", pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Descreva seu produto... Lembre-se de utilizar as características do produto." + Environment.NewLine + "Utilize informações como Marca, Tamanho, Cor etc. ", pictureBox5, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Para selecionar o imposto do produto, o mesmo deve estar cadastrado previamente." + Environment.NewLine + "Para cadastrar um novo imposto acesse Produtos>Impostos>Adicionar. ", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Para selecionar o fornecedor do produto, o mesmo deve estar cadastrado previamente." + Environment.NewLine + "Para cadastrar um novo Fornecedor acesse Produtos>Fornecedores>Adicionar.", pictureBox14, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Digite a quantidade mínima que você deve ter em estoque deste produto.", pictureBox7, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Digite a quantidade que você tem em estoque atualmente." + Environment.NewLine + "Para inserir a quantidade atual em estoque clique no botao Alterar Estoque.", pictureBox8, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Descrição adicional para o produto.", pictureBox10, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Atribua um limite para lançar descontos a este item. O Valor irá influenciar nos descontos em reais e porcentagens.", pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            //ActiveControl = nome;

            //var cat = new Categoria().FindAll().Where("tipo", "Produtos").WhereFalse("excluir").OrderByDesc("nome").Get();
            if (categorias.Count() > 0)
            {
                Categorias.DataSource = categorias;
                Categorias.DisplayMember = "NOME";
                Categorias.ValueMember = "ID";
            }

            LoadFornecedores();

            Medidas.DataSource = Support.GetMedidas();

            //var imposto = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (impostos.Count() > 0)
            {
                ImpostoNFE.DataSource = impostos;
                ImpostoNFE.DisplayMember = "NOME";
                ImpostoNFE.ValueMember = "ID";
            }

            ImpostoNFE.SelectedValue = 0;

            //var imposto2 = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (impostos2.Count() > 0)
            {
                ImpostoCFE.DataSource = impostos2;
                ImpostoCFE.DisplayMember = "NOME";
                ImpostoCFE.ValueMember = "ID";
            }

            ImpostoCFE.SelectedValue = 0;

            Origens.DataSource = Support.GetOrigens();
            Origens.DisplayMember = "Nome";
            Origens.ValueMember = "Id";

            filterMaisRecentes.Checked = true;
            filterTodos.Checked = false;
        }

        private void LoadEstoque()
        {
            _modelItem = _modelItem.FindById(idPdtSelecionado).FirstOrDefault<Item>();
            estoqueatual.Text = Validation.FormatMedidas(_modelItem.Medida, _modelItem.EstoqueAtual);
        }

        private void LoadData()
        {
            _modelItem = _modelItem.FindById(idPdtSelecionado).FirstOrDefault<Item>();

            nome.Text = _modelItem?.Nome ?? "";
            codebarras.Text = _modelItem?.CodeBarras ?? "";
            referencia.Text = _modelItem?.Referencia ?? "";
            valorcompra.Text = Validation.Price(_modelItem.ValorCompra);
            valorvenda.Text = Validation.Price(_modelItem.ValorVenda);

            LoadEstoque();

            if (nome.Text.Length > 2)
                btnEstoque.Enabled = true;

            if (_modelItem.Impostoid > 0)
            {
                ImpostoNFE.SelectedValue = _modelItem.Impostoid;
                chkImpostoNFE.Checked = true;
            }
            else
            {
                ImpostoNFE.Enabled = false;
            }

            if (_modelItem.Impostoidcfe > 0)
            {
                ImpostoCFE.SelectedValue = _modelItem.Impostoidcfe;
                chkImpostoCFE.Checked = true;
            }
            else
            {
                ImpostoCFE.Enabled = false;
            }

            cest.Text = _modelItem?.Cest ?? "";
            ncm.Text = _modelItem?.Ncm ?? "";

            if (_modelItem.Origem != null)
                Origens.SelectedValue = _modelItem.Origem;

            if (_modelItem.Medida != null)
                Medidas.SelectedItem = _modelItem.Medida;

            Categorias.SelectedValue = _modelItem.Categoriaid;

            Fornecedor.SelectedValue = _modelItem.Fornecedor;

            aliq_federal.Text = Validation.Price(_modelItem.AliqFederal);
            aliq_estadual.Text = Validation.Price(_modelItem.AliqEstadual);
            aliq_municipal.Text = Validation.Price(_modelItem.AliqMunicipal);

            txtLimiteDesconto.Text = Validation.Price(_modelItem.Limite_Desconto);

            DataTableEstoque();
        }

        private void Save()
        {
            if (!string.IsNullOrEmpty(nome.Text))
            {
                var data = _modelItem.Query().Where("id", "!=", idPdtSelecionado).Where("tipo", "Produtos").Where("nome", nome.Text).Where("excluir", 0).FirstOrDefault();
                if (data != null)
                {
                    Alert.Message("Oppss", "Já existe um produto cadastrado com esse NOME.", Alert.AlertType.error);
                    return;
                }
            }

            codebarras.Text = codebarras.Text.Trim();
            if (!string.IsNullOrEmpty(codebarras.Text))
            {
                if (codebarras.Text.Length <= 3)
                {
                    Alert.Message("Oppss", "Código de barras é muito pequeno.", Alert.AlertType.error);
                    return;
                }

                var data = _modelItem.Query().Where("id", "!=", idPdtSelecionado).Where("codebarras", codebarras.Text).Where("excluir", 0).FirstOrDefault();
                if (data != null)
                {
                    Alert.Message("Oppss", "Já existe um produto cadastrado com esse código de barras.", Alert.AlertType.error);
                    return;
                }
            }
            else
            {
                var result = AlertOptions.Message("Atenção!", "É necessário preencher o código de barras, deseja gerar um código automático?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                    codebarras.Text = Validation.CodeBarrasRandom();
                else
                    return;
            }

            _modelItem.Id = idPdtSelecionado;
            _modelItem.Tipo = "Produtos";
            _modelItem.Nome = nome.Text;
            _modelItem.CodeBarras = codebarras.Text;
            _modelItem.Referencia = referencia.Text;
            _modelItem.ValorCompra = Validation.ConvertToDouble(valorcompra.Text);
            _modelItem.ValorVenda = Validation.ConvertToDouble(valorvenda.Text);
            _modelItem.EstoqueMinimo = Validation.ConvertToDouble(estoqueminimo.Text);
            _modelItem.Medida = Medidas.Text;

            _modelItem.Cest = cest.Text;
            _modelItem.Ncm = ncm.Text;
            _modelItem.AliqFederal = Validation.ConvertToDouble(aliq_federal.Text);
            _modelItem.AliqEstadual = Validation.ConvertToDouble(aliq_estadual.Text);
            _modelItem.AliqMunicipal = Validation.ConvertToDouble(aliq_municipal.Text);
            _modelItem.Limite_Desconto = Validation.ConvertToDouble(txtLimiteDesconto.Text);
            
            if (Categorias.SelectedValue != null)
                _modelItem.Categoriaid = (int)Categorias.SelectedValue;

            if (Fornecedor.SelectedValue != null)
                _modelItem.Fornecedor = (int)Fornecedor.SelectedValue;

            if (ImpostoNFE.SelectedValue != null)
                _modelItem.Impostoid = (int)ImpostoNFE.SelectedValue;
            else
                _modelItem.Impostoid = 0;

            if (ImpostoCFE.SelectedValue != null)
                _modelItem.Impostoidcfe = (int)ImpostoCFE.SelectedValue;
            else
                _modelItem.Impostoidcfe = 0;

            if (Origens.SelectedValue != null)
                _modelItem.Origem = Origens.SelectedValue.ToString();

            if (_modelItem.Save(_modelItem))
                Close();
        }

        private void DataTableEstoque()
        {
            if (filterMaisRecentes.Checked == true)
                _controllerItem.GetDataTableEstoque(listaEstoque, idPdtSelecionado, 10);
            else
                _controllerItem.GetDataTableEstoque(listaEstoque, idPdtSelecionado);
        }

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
                this.BeginInvoke((MethodInvoker)delegate
                {
                    idPdtSelecionado = Produtos.idPdtSelecionado;
                    backOn.RunWorkerAsync();
                });
            };

            label6.Click += (s, e) => Close();
            btnExit.Click += (s, e) =>
            {
                var dataProd = _modelItem.Query().Where("id", idPdtSelecionado).Where("atualizado", "01.01.0001, 00:00:00.000").WhereNull("codebarras").FirstOrDefault();
                if (dataProd != null)
                {
                    var result = AlertOptions.Message("Atenção!", "Esse produto não foi editado, deseja deletar?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        var data = _modelItem.Remove(idPdtSelecionado);
                        if (data)
                            Close();
                    }

                    nome.Focus();
                }

                Close();
            };

            btnSalvar.Click += (s, e) => Save();
            btnRemover.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar um produto, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var data = _modelItem.Remove(idPdtSelecionado);
                    if (data)
                        Close();
                }
            };

            btnEstoque.Click += (s, e) =>
            {
                _modelItem.Nome = nome.Text;
                _modelItem.Save(_modelItem, false);

                AddEstoque f = new AddEstoque();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadEstoque();

                    estoqueminimo.Focus();
                    DataTableEstoque();
                }
            };

            valorcompra.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            valorvenda.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            txtLimiteDesconto.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            btnAddFornecedor.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                Comercial.AddClientes.Id = 0;
                Comercial.AddClientes f = new Comercial.AddClientes();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadFornecedores();
                }
            };

            estoqueminimo.KeyPress += (s, e) => Masks.MaskDouble(s, e);
            codebarras.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 20);
            referencia.KeyPress += (s, e) => Masks.MaskOnlyNumberAndChar(s, e, 50);

            ncm.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 8);
            cest.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 7);

            nome.TextChanged += (s, e) =>
            {
                if (nome.Text.Length >= 2)
                    btnEstoque.Visible = true;
                else
                    btnEstoque.Visible = false;
            };

            nome.KeyPress += (s, e) =>
            {
                Masks.MaskMaxLength(s, e, 100);
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            chkImpostoNFE.Click += (s, e) =>
            {
                if (chkImpostoNFE.Checked == true)
                {
                    ImpostoNFE.Enabled = true;
                }
                else
                {
                    ImpostoNFE.Enabled = false;
                    ImpostoNFE.SelectedValue = 0;
                }
            };

            chkImpostoCFE.Click += (s, e) =>
            {
                if (chkImpostoCFE.Checked == true)
                {
                    ImpostoCFE.Enabled = true;
                }
                else
                {
                    ImpostoCFE.Enabled = false;
                    ImpostoCFE.SelectedValue = 0;
                }
            };

            filterTodos.Click += (s, e) => DataTableEstoque();

            filterMaisRecentes.Click += (s, e) => DataTableEstoque();

            backOn.DoWork += (s, e) =>
             {
                 _modelItem = _modelItem.FindById(idPdtSelecionado).FirstOrDefault<Item>();
                 categorias = new Categoria().FindAll().Where("tipo", "Produtos").WhereFalse("excluir").OrderByDesc("nome").Get();
                 fornecedores = new Pessoa().FindAll().Where("tipo", "Fornecedores").WhereFalse("excluir").OrderByDesc("nome").Get();
                 impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
                 impostos2 = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
             };

            backOn.RunWorkerCompleted += (s, e) =>
            {
                Start();

                if (idPdtSelecionado > 0)
                {
                    LoadData();
                }
                else
                {
                    _modelItem = new Model.Item();
                    _modelItem.Tipo = "Produtos";
                    _modelItem.Id = idPdtSelecionado;
                    if (_modelItem.Save(_modelItem, false))
                    {
                        idPdtSelecionado = _modelItem.GetLastId();
                        LoadData();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar.", Alert.AlertType.error);
                        Close();
                    }
                }
            };
        }
    }
}
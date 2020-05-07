using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using SqlKata.Execution;
using Categoria = Emiplus.Model.Categoria;
using Pessoa = Emiplus.Model.Pessoa;

namespace Emiplus.View.Produtos
{
    public partial class EditAllProducts : Form
    {
        public static List<int> ListProducts = new List<int>();
        private Model.Item _modelItem = new Model.Item();

        private readonly BackgroundWorker backOn = new BackgroundWorker();

        public EditAllProducts()
        {
            InitializeComponent();

            FormOpen = true;

            Eventos();
        }

        private IEnumerable<dynamic> fornecedores { get; set; }
        private ArrayList categorias { get; set; }
        private IEnumerable<dynamic> impostos { get; set; }
        private IEnumerable<dynamic> impostos2 { get; set; }

        public static bool FormOpen { get; set; }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Categoria";
            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Referência";
            Table.Columns[2].Width = 100;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Descrição";
            Table.Columns[3].Width = 120;
            Table.Columns[3].MinimumWidth = 120;
            Table.Columns[3].Visible = true;

            Table.Columns[4].Name = "Custo";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;
            Table.Columns[4].Visible = true;

            Table.Columns[5].Name = "Venda";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;
            Table.Columns[5].Visible = true;
        }

        private async Task SetContentTableAsync(DataGridView Table)
        {
            Table.Rows.Clear();

            foreach (var item in ListProducts)
            {
                var items = _modelItem.FindById(item).Get();

                foreach (var data in items)
                    Table.Rows.Add(
                        data.ID,
                        data.CATEGORIA,
                        data.REFERENCIA,
                        data.NOME,
                        Validation.FormatPrice(Validation.ConvertToDouble(data.VALORCOMPRA), false),
                        Validation.FormatPrice(Validation.ConvertToDouble(data.VALORVENDA), true)
                    );
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadFornecedores()
        {
            Fornecedor.DataSource = new Pessoa().GetAll("Fornecedores");
            Fornecedor.DisplayMember = "Nome";
            Fornecedor.ValueMember = "Id";
            Fornecedor.Refresh();
        }

        private void LoadImpostoOne()
        {
            impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (impostos.Any())
            {
                ImpostoNFE.DataSource = impostos;
                ImpostoNFE.DisplayMember = "NOME";
                ImpostoNFE.ValueMember = "ID";
            }

            ImpostoNFE.SelectedValue = 0;
        }

        private void LoadImpostoTwo()
        {
            impostos2 = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (impostos2.Any())
            {
                ImpostoCFE.DataSource = impostos2;
                ImpostoCFE.DisplayMember = "NOME";
                ImpostoCFE.ValueMember = "ID";
            }

            ImpostoCFE.SelectedValue = 0;
        }

        private void Start()
        {
            ToolHelp.Show(
                "Para selecionar a categoria do produto, a mesma deve estar cadastrada previamente.\nPara cadastrar uma nova categoria acesse Produtos>Categorias>Adicionar.",
                pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "Para selecionar o imposto do produto, o mesmo deve estar cadastrado previamente." +
                Environment.NewLine + "Para cadastrar um novo imposto acesse Produtos>Impostos>Adicionar. ",
                pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "Para selecionar o fornecedor do produto, o mesmo deve estar cadastrado previamente." +
                Environment.NewLine + "Para cadastrar um novo Fornecedor acesse Produtos>Fornecedores>Adicionar.",
                pictureBox14, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Descrição adicional para o produto.", pictureBox10, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "Atribua um limite para lançar descontos a este item. O Valor irá influenciar nos descontos em reais e porcentagens.",
                pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            Categorias.DataSource = categorias;
            Categorias.DisplayMember = "Nome";
            Categorias.ValueMember = "Id";

            LoadFornecedores();

            Medidas.DataSource = Support.GetMedidas();

            if (impostos.Any())
            {
                ImpostoNFE.DataSource = impostos;
                ImpostoNFE.DisplayMember = "NOME";
                ImpostoNFE.ValueMember = "ID";
            }

            ImpostoNFE.SelectedValue = 0;

            if (impostos2.Any())
            {
                ImpostoCFE.DataSource = impostos2;
                ImpostoCFE.DisplayMember = "NOME";
                ImpostoCFE.ValueMember = "ID";
            }

            ImpostoCFE.SelectedValue = 0;

            Origens.DataSource = Support.GetOrigens();
            Origens.DisplayMember = "Nome";
            Origens.ValueMember = "Id";
        }

        private void Save()
        {
            foreach (var item in ListProducts)
            {
                _modelItem = _modelItem.FindById(item).FirstOrDefault<Model.Item>();
                if (!string.IsNullOrEmpty(valorcompra.Text))
                    _modelItem.ValorCompra = Validation.ConvertToDouble(valorcompra.Text);

                if (!string.IsNullOrEmpty(valorvenda.Text))
                    _modelItem.ValorVenda = Validation.ConvertToDouble(valorvenda.Text);

                if (!string.IsNullOrEmpty(Medidas.Text))
                    _modelItem.Medida = Medidas.Text;

                if (!string.IsNullOrEmpty(cest.Text))
                    _modelItem.Cest = cest.Text;

                if (!string.IsNullOrEmpty(ncm.Text))
                    _modelItem.Ncm = ncm.Text;

                if (!string.IsNullOrEmpty(aliq_federal.Text))
                    _modelItem.AliqFederal = Validation.ConvertToDouble(aliq_federal.Text);

                if (!string.IsNullOrEmpty(aliq_estadual.Text))
                    _modelItem.AliqEstadual = Validation.ConvertToDouble(aliq_estadual.Text);

                if (!string.IsNullOrEmpty(aliq_municipal.Text))
                    _modelItem.AliqMunicipal = Validation.ConvertToDouble(aliq_municipal.Text);

                if (!string.IsNullOrEmpty(txtLimiteDesconto.Text))
                    _modelItem.Limite_Desconto = Validation.ConvertToDouble(txtLimiteDesconto.Text);

                _modelItem.Categoriaid = Validation.ConvertToInt32(Categorias.SelectedValue);
                _modelItem.Fornecedor = Validation.ConvertToInt32(Fornecedor.SelectedValue);

                if (ImpostoNFE.SelectedValue != null)
                    _modelItem.Impostoid = (int) ImpostoNFE.SelectedValue;
                else
                    _modelItem.Impostoid = 0;

                if (ImpostoCFE.SelectedValue != null)
                    _modelItem.Impostoidcfe = (int) ImpostoCFE.SelectedValue;
                else
                    _modelItem.Impostoidcfe = 0;

                if (Origens.SelectedValue != null)
                    _modelItem.Origem = Origens.SelectedValue.ToString();

                _modelItem.Save(_modelItem, false);
            }

            ListProducts = null;
            Close();
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
                Refresh();

                BeginInvoke((MethodInvoker) delegate { backOn.RunWorkerAsync(); });

                SetHeadersTable(GridListaProdutos);
            };

            label6.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSalvar.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Confirmar alterações nos produtos?",
                    AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                    Save();
            };

            valorcompra.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            valorvenda.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            txtLimiteDesconto.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            btnAddFornecedor.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                AddClientes.Id = 0;
                var f = new AddClientes
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen
                };
                if (f.ShowDialog() == DialogResult.OK)
                    LoadFornecedores();
            };

            ncm.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 8);
            cest.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 7);

            chkImpostoNFE.Click += (s, e) =>
            {
                if (chkImpostoNFE.Checked)
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
                if (chkImpostoCFE.Checked)
                {
                    ImpostoCFE.Enabled = true;
                }
                else
                {
                    ImpostoCFE.Enabled = false;
                    ImpostoCFE.SelectedValue = 0;
                }
            };

            backOn.DoWork += (s, e) =>
            {
                categorias = new Categoria().GetAll("Produtos");
                fornecedores = new Pessoa().FindAll().Where("tipo", "Fornecedores").WhereFalse("excluir")
                    .OrderByDesc("nome").Get();
                impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
                impostos2 = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            };

            backOn.RunWorkerCompleted += async (s, e) =>
            {
                Start();

                await SetContentTableAsync(GridListaProdutos);
            };

            btnAddImpostoOne.Click += (s, e) =>
            {
                Impostos.idImpSelected = 0;
                var f = new AddImpostos
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen
                };
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                LoadImpostoOne();
                LoadImpostoTwo();
            };

            btnAddImpostoTwo.Click += (s, e) =>
            {
                Impostos.idImpSelected = 0;
                var f = new AddImpostos
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen
                };
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                LoadImpostoOne();
                LoadImpostoTwo();
            };

            btnAddCategoria.Click += (s, e) =>
            {
                Home.CategoriaPage = "Produtos";
                var f = new AddCategorias
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true
                };
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                Categorias.DataSource = new Categoria().GetAll("Produtos");
                Categorias.Refresh();
            };

            FormClosing += (s, e) => { FormOpen = false; };
        }
    }
}
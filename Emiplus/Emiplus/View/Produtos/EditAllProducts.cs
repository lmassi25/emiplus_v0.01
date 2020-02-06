using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class EditAllProducts : Form
    {
        private Model.Item _modelItem = new Model.Item();
        private Controller.Item _controllerItem = new Controller.Item();

        private BackgroundWorker backOn = new BackgroundWorker();

        private IEnumerable<dynamic> fornecedores { get; set; }
        private IEnumerable<dynamic> categorias { get; set; }
        private IEnumerable<dynamic> impostos { get; set; }
        private IEnumerable<dynamic> impostos2 { get; set; }

        public static List<int> listProducts = new List<int>();

        public EditAllProducts()
        {
            InitializeComponent();
            Eventos();
        }
        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
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

            foreach (var item in listProducts)
            {
                var items = _modelItem.FindById(item).Get();

                foreach (dynamic data in items)
                {
                    Table.Rows.Add(
                        data.ID,
                        data.CATEGORIA,
                        data.REFERENCIA,
                        data.NOME,
                        Validation.FormatPrice(Validation.ConvertToDouble(data.VALORCOMPRA), false),
                        Validation.FormatPrice(Validation.ConvertToDouble(data.VALORVENDA), true)
                    );
                }
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadFornecedores()
        {
            Fornecedor.Refresh();

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
            ToolHelp.Show("Para selecionar o imposto do produto, o mesmo deve estar cadastrado previamente." + Environment.NewLine + "Para cadastrar um novo imposto acesse Produtos>Impostos>Adicionar. ", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Para selecionar o fornecedor do produto, o mesmo deve estar cadastrado previamente." + Environment.NewLine + "Para cadastrar um novo Fornecedor acesse Produtos>Fornecedores>Adicionar.", pictureBox14, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Descrição adicional para o produto.", pictureBox10, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Atribua um limite para lançar descontos a este item. O Valor irá influenciar nos descontos em reais e porcentagens.", pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            if (categorias.Count() > 0)
            {
                Categorias.DataSource = categorias;
                Categorias.DisplayMember = "NOME";
                Categorias.ValueMember = "ID";
            }

            LoadFornecedores();

            Medidas.DataSource = Support.GetMedidas();

            if (impostos.Count() > 0)
            {
                ImpostoNFE.DataSource = impostos;
                ImpostoNFE.DisplayMember = "NOME";
                ImpostoNFE.ValueMember = "ID";
            }

            ImpostoNFE.SelectedValue = 0;

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
        }

        private void Save()
        {
            foreach (var item in listProducts)
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

                _modelItem.Save(_modelItem, false);
            }

            listProducts = null;
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

                this.BeginInvoke((MethodInvoker)delegate
                {
                    backOn.RunWorkerAsync();
                });

                SetHeadersTable(GridListaProdutos);
            };

            label6.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSalvar.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Confirmar alterações nos produtos?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                    Save();
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

            ncm.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 8);
            cest.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 7);

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

            backOn.DoWork += (s, e) =>
            {
                categorias = new Model.Categoria().FindAll().Where("tipo", "Produtos").WhereFalse("excluir").OrderByDesc("nome").Get();
                fornecedores = new Model.Pessoa().FindAll().Where("tipo", "Fornecedores").WhereFalse("excluir").OrderByDesc("nome").Get();
                impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
                impostos2 = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            };

            backOn.RunWorkerCompleted += async (s, e) =>
            {
                Start();

                await SetContentTableAsync(GridListaProdutos);
            };
        }
    }
}

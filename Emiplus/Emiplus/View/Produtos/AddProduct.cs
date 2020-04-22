﻿using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using Newtonsoft.Json;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddProduct : Form
    {
        public static int idPdtSelecionado { get; set; }
        private Item _modelItem = new Item();
        private Controller.Item _controllerItem = new Controller.Item();

        private BackgroundWorker backOn = new BackgroundWorker();
        private ArrayList categorias { get; set; }
        private IEnumerable<dynamic> impostos { get; set; }
        private IEnumerable<dynamic> impostos2 { get; set; }

        private OpenFileDialog ofd = new OpenFileDialog();

        public AddProduct()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadFornecedores()
        {
            Fornecedor.DataSource = new Pessoa().GetAll("Fornecedores");
            Fornecedor.DisplayMember = "Nome";
            Fornecedor.ValueMember = "Id";
        }

        private void LoadImpostoOne()
        {
            impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (impostos.Count() > 0)
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
            if (impostos2.Count() > 0)
            {
                ImpostoCFE.DataSource = impostos2;
                ImpostoCFE.DisplayMember = "NOME";
                ImpostoCFE.ValueMember = "ID";
            }

            ImpostoCFE.SelectedValue = 0;
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

            Categorias.DataSource = categorias;
            Categorias.DisplayMember = "Nome";
            Categorias.ValueMember = "Id";
          
            LoadFornecedores();

            Medidas.DataSource = Support.GetMedidas();

            LoadImpostoOne();
            LoadImpostoTwo();

            Origens.DataSource = Support.GetOrigens();
            Origens.DisplayMember = "Nome";
            Origens.ValueMember = "Id";

            filterMaisRecentes.Checked = true;
            filterTodos.Checked = false;
        }

        private void SetHeadersAdicionais(DataGridView Table)
        {
            Table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Selecione";
            checkColumn.Name = "Selecione";
            checkColumn.FlatStyle = FlatStyle.Standard;
            checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
            checkColumn.Width = 60;
            Table.Columns.Insert(0, checkColumn);

            Table.Columns[1].Name = "ID";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Adicional";
            Table.Columns[2].Width = 120;
            Table.Columns[2].MinimumWidth = 120;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Width = 100;
            Table.Columns[3].Visible = true;
        }

        private void SetContentTableAdicionais(DataGridView Table)
        {
            Table.Rows.Clear();

            IEnumerable<Model.ItemAdicional> data = new Model.ItemAdicional().FindAll().WhereFalse("excluir").Get<Model.ItemAdicional>();
            if (data.Count() > 0) {
                foreach (Model.ItemAdicional item in data)
                {
                    Table.Rows.Add(
                        false,
                        item.Id,
                        item.Title,
                        Validation.FormatPrice(Validation.ConvertToDouble(item.Valor), true)
                    );
                }
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            estoqueminimo.Text = Validation.FormatMedidas(_modelItem.Medida, _modelItem.EstoqueMinimo);
            LoadEstoque();
            CustoMedio();

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

            Categorias.SelectedValue = _modelItem.Categoriaid.ToString();
            Fornecedor.SelectedValue = _modelItem.Fornecedor.ToString();

            Ativo.Toggled = _modelItem.ativo == 1 ? false : true;

            aliq_federal.Text = Validation.Price(_modelItem.AliqFederal);
            aliq_estadual.Text = Validation.Price(_modelItem.AliqEstadual);
            aliq_municipal.Text = Validation.Price(_modelItem.AliqMunicipal);

            txtLimiteDesconto.Text = Validation.Price(_modelItem.Limite_Desconto);

            if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}"))
            {
                var imageAsByteArray = File.ReadAllBytes($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}");
                imageProduct.Image = byteArrayToImage(imageAsByteArray);
                pathImage.Text = $@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}";
                btnRemoverImage.Visible = true;
            }

            DataTableEstoque();
            SetContentTableAdicionais(GridAdicionais);

            foreach (DataGridViewRow item in GridAdicionais.Rows)
            {
                if (!string.IsNullOrEmpty(_modelItem.Adicional))
                {
                    string[] addons = _modelItem.Adicional.Split(',');
                    foreach (string id in addons)
                    {
                        if (Validation.ConvertToInt32(item.Cells["ID"].Value) == Validation.ConvertToInt32(id))
                        {
                            item.Cells["Selecione"].Value = true;
                        }
                    }
                }
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void CustoMedio()
        {
            var data = DateTime.Today.AddMonths(-3).ToString();

            dynamic dados = new PedidoItem()
                .Query()
                .Join("pedido", "pedido.id", "pedido_item.pedido")
                .SelectRaw("SUM(pedido_item.valorvenda) as valorvenda, COUNT(pedido_item.ID) as ID")
                .Where("pedido.tipo", "Compras")
                .Where("pedido_item.item", _modelItem.Id)
                .WhereFalse("pedido_item.excluir")
                .WhereFalse("pedido.excluir")
                .Where("pedido_item.criado", ">", Validation.ConvertDateToSql(data) + " 00:00:00")
                .FirstOrDefault();

            if (dados.ID != 0)
                custoMedio.Text = Validation.Price(Validation.ConvertToDouble(dados.ID) / Validation.ConvertToDouble(dados.VALORVENDA));
            else
                custoMedio.Text = "0,00";
        }
        
        private void Save()
        {
            if (!string.IsNullOrEmpty(nome.Text))
            {
                if (_modelItem.ExistsName(nome.Text, false, idPdtSelecionado))
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

                if (_modelItem.ExistsCodeBarras(codebarras.Text, false, idPdtSelecionado))
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
            
            if (string.IsNullOrEmpty(_modelItem.Ncm) || _modelItem.Ncm != "0")
            {
                if (aliq_federal.Text == "0,00" || aliq_estadual.Text == "0,00")
                {
                    var data = new IBPT()
                    .SetCodeBarras(_modelItem.CodeBarras)
                    .SetDescricao(_modelItem.Nome)
                    .SetMedida(_modelItem.Medida)
                    .SetValor(_modelItem.ValorVenda)
                    .SetCodigoNCM(_modelItem.Ncm)
                    .GetDados();

                    if (data != null)
                    {
                        if (data.Message != null)
                        {
                            aliq_federal.Text = "0";
                            aliq_estadual.Text = "0";
                            aliq_municipal.Text = "0";
                        }
                        else
                        {
                            var s = JsonConvert.DeserializeObject(data.ToString());

                            aliq_federal.Text = Validation.Price(s.Nacional.Value);
                            aliq_estadual.Text = Validation.Price(s.Estadual.Value);
                            aliq_municipal.Text = Validation.Price(s.Municipal.Value);
                        }
                    }
                }
            }

            if (_modelItem.Ncm == "0")
            {
                aliq_federal.Text = "";
                aliq_estadual.Text = "";
                aliq_municipal.Text = "";
            }

            _modelItem.AliqFederal = Validation.ConvertToDouble(aliq_federal.Text);
            _modelItem.AliqEstadual = Validation.ConvertToDouble(aliq_estadual.Text);
            _modelItem.AliqMunicipal = Validation.ConvertToDouble(aliq_municipal.Text);
            _modelItem.Limite_Desconto = Validation.ConvertToDouble(txtLimiteDesconto.Text);
            
            _modelItem.Categoriaid = Validation.ConvertToInt32(Categorias.SelectedValue);
            
            _modelItem.Fornecedor = Validation.ConvertToInt32(Fornecedor.SelectedValue);

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

            if (Ativo.Toggled)
                _modelItem.ativo = 0;
            else
                _modelItem.ativo = 1;

            StringBuilder Addon = new StringBuilder();
            foreach (DataGridViewRow item in GridAdicionais.Rows)
            {
                if ((bool)item.Cells["Selecione"].Value == true)
                {
                    if (string.IsNullOrEmpty(Addon.ToString()))
                    {
                        Addon.Append(Validation.ConvertToInt32(item.Cells["ID"].Value).ToString());
                        continue;
                    }

                    Addon.Append($",{Validation.ConvertToInt32(item.Cells["ID"].Value)}");
                }
            }

            _modelItem.Adicional = Addon.ToString();

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
                Refresh();

                this.BeginInvoke((MethodInvoker)delegate
                {
                    idPdtSelecionado = Produtos.idPdtSelecionado;
                    backOn.RunWorkerAsync();
                });

                SetHeadersAdicionais(GridAdicionais);
                nome.Focus();
            };

            btnExit.Click += (s, e) =>
            {
                var dataProd = _modelItem.Query().Where("id", idPdtSelecionado).Where("atualizado", "01.01.0001, 00:00:00.000").WhereNull("codebarras").FirstOrDefault();
                if (dataProd != null)
                {
                    var result = AlertOptions.Message("Atenção!", "Esse produto não foi editado, deseja deletar?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        var data = _modelItem.Remove(idPdtSelecionado, false);
                        if (data)
                            Close();
                    }

                    nome.Focus();
                    return;
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
                if (new Model.Item().ValidarDados(_modelItem))
                    return;

                AddEstoque f = new AddEstoque();
                f.TopMost = true;
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

            btnAddCategoria.Click += (s, e) =>
            {
                Home.CategoriaPage = "Produtos";
                AddCategorias f = new AddCategorias();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Categorias.DataSource = new Categoria().GetAll("Produtos");
                    Categorias.Refresh();
                }
            };

            btnAddFornecedor.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                Comercial.AddClientes.Id = 0;
                Comercial.AddClientes f = new Comercial.AddClientes();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                    LoadFornecedores();
            };

            btnAddImpostoOne.Click += (s, e) =>
            {
                Impostos.idImpSelected = 0;
                AddImpostos f = new AddImpostos();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadImpostoOne();
                    LoadImpostoTwo();
                }
            };

            btnAddImpostoTwo.Click += (s, e) =>
            {
                Impostos.idImpSelected = 0;
                AddImpostos f = new AddImpostos();
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadImpostoOne();
                    LoadImpostoTwo();
                }
            };

            valorvenda.TextChanged += (s, e) =>
            {
                if (Validation.ConvertToDouble(valorcompra.Text) == 0)
                    return;

                if (Validation.ConvertToDouble(valorvenda.Text) == 0)
                    return;

                var media = ((Validation.ConvertToDouble(valorvenda.Text) - Validation.ConvertToDouble(valorcompra.Text)) * 100) / Validation.ConvertToDouble(valorcompra.Text);
                precoMedio.Text = $"{Validation.ConvertToDouble(Validation.RoundTwo(media))}%";
            };

            valorcompra.TextChanged += (s, e) =>
            {
                if (Validation.ConvertToDouble(valorcompra.Text) == 0)
                    return;

                if (Validation.ConvertToDouble(valorvenda.Text) == 0)
                    return;

                var media = ((Validation.ConvertToDouble(valorvenda.Text) - Validation.ConvertToDouble(valorcompra.Text)) * 100) / Validation.ConvertToDouble(valorcompra.Text);
                precoMedio.Text = Validation.Price(media);
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

            btnRemoverImage.Click += (s, e) =>
            {
                _modelItem.Id = idPdtSelecionado;

                if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}"))
                    File.Delete($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}");

                _modelItem.Image = "";
                _modelItem.Save(_modelItem, false);

                imageProduct.Image = Properties.Resources.sem_imagem;
                pathImage.Text = "";
                btnRemoverImage.Visible = false;
                Alert.Message("Pronto!", "Imagem removida com sucesso.", Alert.AlertType.success);
            };

            btnImage.Click += (s, e) =>
            {
                ofd.RestoreDirectory = true;
                ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!ofd.CheckFileExists)
                    {
                        Alert.Message("Opps", "Não encontramos a imagem selecionada. Tente Novamente!", Alert.AlertType.error);
                        return;
                    }

                    string path = ofd.InitialDirectory + ofd.FileName;
                    string ext = Path.GetExtension(ofd.FileName);

                    if (File.Exists(path))
                    {
                        if (!Directory.Exists(Program.PATH_IMAGE + @"\Imagens"))
                            Directory.CreateDirectory(Program.PATH_IMAGE + @"\Imagens");
                        
                        string nameImage = $"{Validation.CleanString(nome.Text).Replace(" ", "-")}{ext}";

                        if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{nameImage}"))
                            File.Delete($@"{Program.PATH_IMAGE}\Imagens\{nameImage}");
                        
                        File.Copy(path, $@"{Program.PATH_IMAGE}\Imagens\{nameImage}");

                        _modelItem.Id = idPdtSelecionado;
                        _modelItem.Image = nameImage;
                        _modelItem.Save(_modelItem, false);

                        var imageAsByteArray = File.ReadAllBytes($@"{Program.PATH_IMAGE}\Imagens\{nameImage}");
                        imageProduct.Image = byteArrayToImage(imageAsByteArray);
                        pathImage.Text = $@"{Program.PATH_IMAGE}\Imagens\{nameImage}";
                        btnRemoverImage.Visible = true;
                        Alert.Message("Pronto!", "Imagem atualizada com sucesso.", Alert.AlertType.success);
                    }
                    else
                    {
                        Alert.Message("Opps", "Não foi possível copiar a imagem. Tente novamente.", Alert.AlertType.error);
                        return;
                    }
                }
            };

            filterTodos.Click += (s, e) => DataTableEstoque();
            filterMaisRecentes.Click += (s, e) => DataTableEstoque();

            selecionarNCM.Click += (s, e) =>
            {
                ModalNCM f = new ModalNCM();
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    ncm.Text = ModalNCM.NCM;
                }
            };

            backOn.DoWork += (s, e) =>
            {
                 _modelItem = _modelItem.FindById(idPdtSelecionado).FirstOrDefault<Item>();
                 categorias = new Categoria().GetAll("Produtos");
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

            btnVariacao.Click += (s, e) =>
            {
                ModalVariacao.idProduto = idPdtSelecionado;
                ModalVariacao form = new ModalVariacao();
                form.ShowDialog();
            };

            GridAdicionais.CellClick += (s, e) =>
            {
                if (GridAdicionais.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)GridAdicionais.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridAdicionais.SelectedRows[0].Cells["Selecione"].Value = true;
                    }
                    else
                    {
                        GridAdicionais.SelectedRows[0].Cells["Selecione"].Value = false;
                    }
                }
            };

            GridAdicionais.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridAdicionais.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridAdicionais.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridAdicionais.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using Newtonsoft.Json;
using SqlKata.Execution;
using Pessoa = Emiplus.Model.Pessoa;

namespace Emiplus.View.Produtos
{
    public partial class AddProduct : Form
    {
        /// <summary>
        /// Background worker para carregar dados do produto sem travar a tela
        /// </summary>
        private readonly BackgroundWorker backOn = new BackgroundWorker();

        /// <summary>
        /// Background worker para carregar estoques na tabela listaEstoque
        /// </summary>
        private readonly BackgroundWorker workerBackEstoque = new BackgroundWorker();

        /// <summary>
        /// Variavel usada para abrir o selecionador de arquivos, usado para imagem do produto
        /// </summary>
        private readonly OpenFileDialog ofd = new OpenFileDialog();

        private Item _modelItem = new Item();

        public AddProduct()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        /// ID do produto, usado para manipular o registro no banco
        /// </summary>
        public static int IdPdtSelecionado { get; set; }

        /// <summary>
        /// Array com os fornecedores, usado em segundo plano
        /// </summary>
        private ArrayList ListFornecedores { get; set; }

        /// <summary>
        /// Array com todas as categorias, usado em segundo plano
        /// </summary>
        private ArrayList ListCategorias { get; set; }
        private IEnumerable<dynamic> Impostos { get; set; }
        private IEnumerable<dynamic> Impostos2 { get; set; }

        /// <summary>
        /// Array com registros do estoque, usado para armazenar os registros em segundo plano e exibir assim que carregar
        /// </summary>
        private IEnumerable<dynamic> ListEstoque { get; set; }

        /// <summary>
        /// Variavel usada para mostrar quantidade de estoque a ser exibido na gridEstoque
        /// </summary>
        private static int LimitShowStock { get; set; }
        
        private void LoadFornecedores()
        {
            Fornecedor.DataSource = new Pessoa().GetAll("Fornecedores");
            Fornecedor.DisplayMember = "Nome";
            Fornecedor.ValueMember = "Id";
        }

        private void LoadImpostoOne()
        {
            Impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (Impostos.Any())
            {
                ImpostoNFE.DataSource = Impostos;
                ImpostoNFE.DisplayMember = "NOME";
                ImpostoNFE.ValueMember = "ID";
            }

            ImpostoNFE.SelectedValue = 0;
        }

        private void LoadImpostoTwo()
        {
            Impostos2 = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (Impostos2.Any())
            {
                ImpostoCFE.DataSource = Impostos2;
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
                "Descreva seu produto... Lembre-se de utilizar as características do produto." + Environment.NewLine +
                "Utilize informações como Marca, Tamanho, Cor etc. ", pictureBox5, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "Para selecionar o imposto do produto, o mesmo deve estar cadastrado previamente." +
                Environment.NewLine + "Para cadastrar um novo imposto acesse Produtos>Impostos>Adicionar. ",
                pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "Para selecionar o fornecedor do produto, o mesmo deve estar cadastrado previamente." +
                Environment.NewLine + "Para cadastrar um novo Fornecedor acesse Produtos>Fornecedores>Adicionar.",
                pictureBox14, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Digite a quantidade mínima que você deve ter em estoque deste produto.", pictureBox7,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "Digite a quantidade que você tem em estoque atualmente." + Environment.NewLine +
                "Para inserir a quantidade atual em estoque clique no botao Alterar Estoque.", pictureBox8,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Descrição adicional para o produto.", pictureBox10, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show(
                "Atribua um limite para lançar descontos a este item. O Valor irá influenciar nos descontos em reais e porcentagens.",
                pictureBox11, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            Categorias.DataSource = ListCategorias;
            Categorias.DisplayMember = "Nome";
            Categorias.ValueMember = "Id";

            Fornecedor.DataSource = ListFornecedores;
            Fornecedor.DisplayMember = "Nome";
            Fornecedor.ValueMember = "Id";

            Medidas.DataSource = Support.GetMedidas();

            LoadImpostoOne();
            LoadImpostoTwo();

            Origens.DataSource = Support.GetOrigens();
            Origens.DisplayMember = "Nome";
            Origens.ValueMember = "Id";

            filterMaisRecentes.Checked = true;
            filterTodos.Checked = false;
        }

        private static void SetHeadersAdicionais(DataGridView table)
        {
            table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Selecione",
                Name = "Selecione",
                FlatStyle = FlatStyle.Standard,
                CellTemplate = new DataGridViewCheckBoxCell(),
                Width = 60
            };
            table.Columns.Insert(0, checkColumn);

            table.Columns[1].Name = "ID";
            table.Columns[1].Visible = false;

            table.Columns[2].Name = "Adicional";
            table.Columns[2].Width = 120;
            table.Columns[2].MinimumWidth = 120;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;
        }

        /// <summary>
        /// Adiciona colunas na tabela dos Combos
        /// </summary>
        /// <param name="table"></param>
        private static void SetHeadersCombo(DataGridView table)
        {
            table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] { true });
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Selecione",
                Name = "Selecione",
                FlatStyle = FlatStyle.Standard,
                CellTemplate = new DataGridViewCheckBoxCell(),
                Width = 60
            };
            table.Columns.Insert(0, checkColumn);

            table.Columns[1].Name = "ID";
            table.Columns[1].Visible = false;

            table.Columns[2].Name = "Combo";
            table.Columns[2].Width = 120;
            table.Columns[2].MinimumWidth = 120;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;
        }

        /// <summary>
        /// Alimenta a grid dos adicionais
        /// </summary>
        /// <param name="table"></param>
        private static void SetContentTableAdicionais(DataGridView table)
        {
            table.Rows.Clear();

            var data = new ItemAdicional().FindAll().WhereFalse("excluir").Get<ItemAdicional>();
            if (data.Any())
                foreach (var item in data)
                    table.Rows.Add(
                        false,
                        item.Id,
                        item.Title,
                        Validation.FormatPrice(item.Valor, true)
                    );

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// Alimenta a grid dos combos
        /// </summary>
        /// <param name="table"></param>
        private static void SetContentTableCombos(DataGridView table)
        {
            table.Rows.Clear();

            var data = new ItemCombo().FindAll().WhereFalse("excluir").Get<ItemCombo>();
            if (data.Any())
                foreach (var item in data)
                    table.Rows.Add(
                        false,
                        item.Id,
                        item.Nome,
                        Validation.FormatPrice(item.ValorVenda, true)
                    );

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadEstoque()
        {
            _modelItem = _modelItem.FindById(IdPdtSelecionado).FirstOrDefault<Model.Item>();
            estoqueatual.Text = Validation.FormatMedidas(_modelItem.Medida, _modelItem.EstoqueAtual);
        }

        private void LoadData()
        {
            _modelItem = _modelItem.FindById(IdPdtSelecionado).FirstOrDefault<Model.Item>();

            nome.Text = _modelItem?.Nome ?? "";
            codebarras.Text = _modelItem?.CodeBarras ?? "";
            referencia.Text = _modelItem?.Referencia ?? "";
            valorcompra.Text = Validation.Price(_modelItem?.ValorCompra ?? 0);
            valorvenda.Text = Validation.Price(_modelItem?.ValorVenda ?? 0);
            estoqueminimo.Text = Validation.FormatMedidas(_modelItem?.Medida ?? "UN", _modelItem?.EstoqueMinimo ?? 0);
            inf_adicional.Text = _modelItem?.InfAdicional ?? "";
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

            Ativo.Toggled = _modelItem.ativo != 1;

            aliq_federal.Text = Validation.Price(_modelItem.AliqFederal);
            aliq_estadual.Text = Validation.Price(_modelItem.AliqEstadual);
            aliq_municipal.Text = Validation.Price(_modelItem.AliqMunicipal);

            txtLimiteDesconto.Text = Validation.Price(_modelItem.Limite_Desconto);

            if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}"))
            {
                var imageAsByteArray = File.ReadAllBytes($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}");
                imageProduct.Image = Support.ByteArrayToImage(imageAsByteArray);
                pathImage.Text = $@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}";
                btnRemoverImage.Visible = true;
            }

            DataTableEstoque();

            SetContentTableAdicionais(GridAdicionais);
            foreach (DataGridViewRow item in GridAdicionais.Rows)
                if (!string.IsNullOrEmpty(_modelItem.Adicional))
                {
                    var addons = _modelItem.Adicional.Split(',');
                    foreach (var id in addons)
                        if (Validation.ConvertToInt32(item.Cells["ID"].Value) == Validation.ConvertToInt32(id))
                            item.Cells["Selecione"].Value = true;
                }

            SetContentTableCombos(GridCombos);
            foreach (DataGridViewRow item in GridCombos.Rows)
                if (!string.IsNullOrEmpty(_modelItem.Combos))
                {
                    var combos = _modelItem.Combos.Split(',');
                    foreach (var id in combos)
                        if (Validation.ConvertToInt32(item.Cells["ID"].Value) == Validation.ConvertToInt32(id))
                            item.Cells["Selecione"].Value = true;
                }
        }

        private void CustoMedio()
        {
            /*var data = DateTime.Today.AddMonths(-3).ToString();

            var dados = new PedidoItem()
                .Query()
                .Join("pedido", "pedido.id", "pedido_item.pedido")
                .SelectRaw("SUM(pedido_item.valorvenda) as valorvenda, SUM(pedido_item.QUANTIDADE) as ID")
                .Where("pedido.tipo", "Compras")
                .Where("pedido_item.item", _modelItem.Id)
                .WhereFalse("pedido_item.excluir")
                .WhereFalse("pedido.excluir")
                .Where("pedido_item.criado", ">", Validation.ConvertDateToSql(data) + " 00:00:00")
                .FirstOrDefault();*/

            var dados = new PedidoItem()
                .Query()
                .Join("pedido", "pedido.id", "pedido_item.pedido")
                .SelectRaw("SUM(pedido_item.total) as valorvenda, SUM(pedido_item.QUANTIDADE) as ID")
                .Where("pedido.tipo", "Compras")
                .Where("pedido_item.item", _modelItem.Id)
                .WhereFalse("pedido_item.excluir")
                .WhereFalse("pedido.excluir")
                .FirstOrDefault();

            if (dados.ID != 0)
            {
                double aux_custo = Validation.ConvertToDouble(dados.VALORVENDA) / Validation.ConvertToDouble(dados.ID);
                custoMedio.Text = Validation.Price(aux_custo);
            }                
            else
                custoMedio.Text = "0,00";
        }

        private void Save()
        {
            if (!string.IsNullOrEmpty(nome.Text))
                if (_modelItem.ExistsName(nome.Text, false, IdPdtSelecionado))
                {
                    Alert.Message("Oppss", "Já existe um produto cadastrado com esse NOME.", Alert.AlertType.error);
                    return;
                }

            codebarras.Text = codebarras.Text.Trim();
            if (!string.IsNullOrEmpty(codebarras.Text))
            {
                if (codebarras.Text.Length <= 3)
                {
                    Alert.Message("Oppss", "Código de barras é muito pequeno.", Alert.AlertType.error);
                    return;
                }

                if (_modelItem.ExistsCodeBarras(codebarras.Text, false, IdPdtSelecionado))
                {
                    Alert.Message("Oppss", "Já existe um produto cadastrado com esse código de barras.",
                        Alert.AlertType.error);
                    return;
                }
            }
            else
            {
                var result = AlertOptions.Message("Atenção!",
                    "É necessário preencher o código de barras, deseja gerar um código automático?",
                    AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                    codebarras.Text = Validation.CodeBarrasRandom();
                else
                    return;
            }

            _modelItem.Id = IdPdtSelecionado;
            _modelItem.Tipo = "Produtos";
            _modelItem.Nome = nome.Text;
            _modelItem.CodeBarras = codebarras.Text;
            _modelItem.Referencia = referencia.Text;
            _modelItem.ValorCompra = Validation.ConvertToDouble(valorcompra.Text);
            _modelItem.ValorVenda = Validation.ConvertToDouble(valorvenda.Text);
            _modelItem.EstoqueMinimo = Validation.ConvertToDouble(estoqueminimo.Text);
            _modelItem.Medida = Medidas.Text;
            _modelItem.InfAdicional = inf_adicional.Text;

            _modelItem.Cest = cest.Text;
            _modelItem.Ncm = ncm.Text;

            /*if (Support.CheckForInternetConnection())
            {
                if (string.IsNullOrEmpty(_modelItem.Ncm) || _modelItem.Ncm != "0")
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

                                aliq_federal.Text = Validation.Price(s?.Nacional.Value);
                                aliq_estadual.Text = Validation.Price(s?.Estadual.Value);
                                aliq_municipal.Text = Validation.Price(s?.Municipal.Value);
                            }
                        }
                    }
            }*/

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
                _modelItem.Impostoid = (int) ImpostoNFE.SelectedValue;
            else
                _modelItem.Impostoid = 0;

            if (ImpostoCFE.SelectedValue != null)
                _modelItem.Impostoidcfe = (int) ImpostoCFE.SelectedValue;
            else
                _modelItem.Impostoidcfe = 0;

            if (Origens.SelectedValue != null)
                _modelItem.Origem = Origens.SelectedValue.ToString();

            _modelItem.ativo = Ativo.Toggled ? 0 : 1;

            var addon = new StringBuilder();
            foreach (DataGridViewRow item in GridAdicionais.Rows)
                if ((bool) item.Cells["Selecione"].Value)
                {
                    if (string.IsNullOrEmpty(addon.ToString()))
                    {
                        addon.Append(Validation.ConvertToInt32(item.Cells["ID"].Value).ToString());
                        continue;
                    }

                    addon.Append($",{Validation.ConvertToInt32(item.Cells["ID"].Value)}");
                }

            _modelItem.Adicional = addon.ToString();

            var combos = new StringBuilder();
            foreach (DataGridViewRow item in GridCombos.Rows)
                if ((bool)item.Cells["Selecione"].Value)
                {
                    if (string.IsNullOrEmpty(combos.ToString()))
                    {
                        combos.Append(Validation.ConvertToInt32(item.Cells["ID"].Value).ToString());
                        continue;
                    }

                    combos.Append($",{Validation.ConvertToInt32(item.Cells["ID"].Value)}");
                }

            _modelItem.Combos = combos.ToString();

            if (_modelItem.Save(_modelItem))
                Close();
        }

        private void DataTableEstoque()
        {
            LimitShowStock = filterMaisRecentes.Checked ? 10 : 0;
            workerBackEstoque.RunWorkerAsync();
        }

        public void GetDataTableEstoque(DataGridView table)
        {
            table.ColumnCount = 8;

            table.Columns[0].Name = "ID";
            table.Columns[0].Visible = false;

            table.Columns[1].Name = "Entrada/Saída";
            table.Columns[1].Width = 100;

            table.Columns[2].Name = "Quantidade";
            table.Columns[2].Width = 100;

            table.Columns[3].Name = "Data/Hora";
            table.Columns[3].Width = 120;

            table.Columns[4].Name = "Usuário";
            table.Columns[4].Width = 120;

            table.Columns[5].Name = "Obs.";
            table.Columns[5].MinimumWidth = 120;
            table.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            table.Columns[6].Name = "Tela";
            table.Columns[6].MinimumWidth = 120;
            table.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            table.Columns[7].Name = "Pedido";
            table.Columns[7].Width = 50;

            table.Rows.Clear();

            foreach (var item in ListEstoque)
            {
                table.Rows.Add(
                    item.ID,
                    item.TIPO == "A" ? "Adicionado" : "Removido",
                    item.QUANTIDADE,
                    string.Format("{0:d/M/yyyy HH:mm}", item.CRIADO),
                    item.NOME_USER,
                    item.OBSERVACAO,
                    item.LOCAL,
                    item.ID_PEDIDO
                );
            }
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

                BeginInvoke((MethodInvoker) delegate
                {
                    IdPdtSelecionado = Produtos.IdPdtSelecionado;
                    backOn.RunWorkerAsync();
                });

                SetHeadersAdicionais(GridAdicionais);
                SetHeadersCombo(GridCombos);
                nome.Focus();
            };

            menuEstoque.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelEstoque, menuEstoque);
            label27.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelEstoque, menuEstoque);
            pictureBox12.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelEstoque, menuEstoque);

            menuImpostos.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelImpostos, menuImpostos);
            label35.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelImpostos, menuImpostos);
            pictureBox16.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelImpostos, menuImpostos);

            menuAdicionais.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelAdicionais, menuAdicionais);
            label30.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelAdicionais, menuAdicionais);
            pictureBox13.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelAdicionais, menuAdicionais);

            menuCombo.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelCombo, menuCombo);
            label33.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelCombo, menuCombo);
            pictureBox17.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelCombo, menuCombo);

            menuInfoAdicionais.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelInfoAdicionais, menuInfoAdicionais);
            label31.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelInfoAdicionais, menuInfoAdicionais);
            pictureBox15.Click += (s, e) => Support.DynamicPanel(flowLayoutPanel, panelInfoAdicionais, menuInfoAdicionais);

            btnExit.Click += (s, e) =>
            {
                var dataProd = _modelItem.Query().Where("id", IdPdtSelecionado)
                    .Where("atualizado", "01.01.0001, 00:00:00.000").WhereNull("codebarras").FirstOrDefault();
                if (dataProd != null)
                {
                    var result = AlertOptions.Message("Atenção!", "Esse produto não foi editado, deseja deletar?",
                        AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        var data = _modelItem.Remove(IdPdtSelecionado, false);
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
                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar um produto, continuar?",
                    AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var data = _modelItem.Remove(IdPdtSelecionado);
                    if (data)
                        Close();
                }
            };

            btnEstoque.Click += (s, e) =>
            {
                _modelItem.Nome = nome.Text;
                if (new Model.Item().ValidarDados(_modelItem))
                    return;

                var f = new AddEstoque {TopMost = true};
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadEstoque();

                    estoqueminimo.Focus();
                    DataTableEstoque();
                }
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

            btnAddCategoria.Click += (s, e) =>
            {
                Home.CategoriaPage = "Produtos";
                var f = new AddCategorias
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true
                };
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Categorias.DataSource = new Categoria().GetAll("Produtos");
                    Categorias.Refresh();
                }
            };

            btnAddFornecedor.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                AddClientes.Id = 0;
                var f = new AddClientes
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true
                };
                if (f.ShowDialog() == DialogResult.OK)
                    LoadFornecedores();
            };

            btnAddImpostoOne.Click += (s, e) =>
            {
                View.Produtos.Impostos.idImpSelected = 0;
                var f = new AddImpostos
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true
                };
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadImpostoOne();
                    LoadImpostoTwo();
                }
            };

            btnAddImpostoTwo.Click += (s, e) =>
            {
                View.Produtos.Impostos.idImpSelected = 0;
                var f = new AddImpostos
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true
                };
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

                var media =
                    (Validation.ConvertToDouble(valorvenda.Text) - Validation.ConvertToDouble(valorcompra.Text)) * 100 /
                    Validation.ConvertToDouble(valorcompra.Text);
                precoMedio.Text = $"{Validation.ConvertToDouble(Validation.RoundTwo(media))}%";
            };

            valorcompra.TextChanged += (s, e) =>
            {
                if (Validation.ConvertToDouble(valorcompra.Text) == 0)
                    return;

                if (Validation.ConvertToDouble(valorvenda.Text) == 0)
                    return;

                var media =
                    (Validation.ConvertToDouble(valorvenda.Text) - Validation.ConvertToDouble(valorcompra.Text)) * 100 /
                    Validation.ConvertToDouble(valorcompra.Text);
                precoMedio.Text = Validation.Price(media);
            };

            estoqueminimo.KeyPress += (s, e) => Masks.MaskDouble(s, e);
            codebarras.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 20);
            referencia.KeyPress += (s, e) => Masks.MaskOnlyNumberAndChar(s, e, 50);
            ncm.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 8);
            cest.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 7);

            nome.TextChanged += (s, e) => { btnEstoque.Visible = nome.Text.Length >= 2; };

            nome.KeyPress += (s, e) => { Masks.MaskMaxLength(s, e, 100); };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);

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

            btnRemoverImage.Click += (s, e) =>
            {
                _modelItem.Id = IdPdtSelecionado;

                if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}"))
                    File.Delete($@"{Program.PATH_IMAGE}\Imagens\{_modelItem.Image}");

                _modelItem.Image = "";
                _modelItem.Save(_modelItem, false);

                imageProduct.Image = Resources.sem_imagem;
                pathImage.Text = "";
                btnRemoverImage.Visible = false;
                Alert.Message("Pronto!", "Imagem removida com sucesso.", Alert.AlertType.success);
            };

            btnImage.Click += (s, e) =>
            {
                ofd.RestoreDirectory = true;
                ofd.Filter = @"Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!ofd.CheckFileExists)
                    {
                        Alert.Message("Opps", "Não encontramos a imagem selecionada. Tente Novamente!",
                            Alert.AlertType.error);
                        return;
                    }

                    var path = ofd.InitialDirectory + ofd.FileName;
                    var ext = Path.GetExtension(ofd.FileName);

                    if (File.Exists(path))
                    {
                        if (!Directory.Exists(Program.PATH_IMAGE + @"\Imagens"))
                            Directory.CreateDirectory(Program.PATH_IMAGE + @"\Imagens");

                        var nameImage = $"{Validation.CleanString(nome.Text).Replace(" ", "-")}{ext}";

                        if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{nameImage}"))
                            File.Delete($@"{Program.PATH_IMAGE}\Imagens\{nameImage}");

                        File.Copy(path, $@"{Program.PATH_IMAGE}\Imagens\{nameImage}");

                        _modelItem.Id = IdPdtSelecionado;
                        _modelItem.Image = nameImage;
                        _modelItem.Save(_modelItem, false);

                        var imageAsByteArray = File.ReadAllBytes($@"{Program.PATH_IMAGE}\Imagens\{nameImage}");
                        imageProduct.Image = Support.ByteArrayToImage(imageAsByteArray);
                        pathImage.Text = $@"{Program.PATH_IMAGE}\Imagens\{nameImage}";
                        btnRemoverImage.Visible = true;
                        Alert.Message("Pronto!", "Imagem atualizada com sucesso.", Alert.AlertType.success);
                    }
                    else
                    {
                        Alert.Message("Opps", "Não foi possível copiar a imagem. Tente novamente.",
                            Alert.AlertType.error);
                    }
                }
            };

            filterTodos.Click += (s, e) => DataTableEstoque();
            filterMaisRecentes.Click += (s, e) => DataTableEstoque();

            selecionarNCM.Click += (s, e) =>
            {
                var f = new ModalNCM {TopMost = true};
                if (f.ShowDialog() == DialogResult.OK) ncm.Text = ModalNCM.NCM;
            };

            backOn.DoWork += (s, e) =>
            {
                _modelItem = _modelItem.FindById(IdPdtSelecionado).FirstOrDefault<Model.Item>();
                ListCategorias = new Categoria().GetAll("Produtos");
                ListFornecedores = new Pessoa().GetAll("Fornecedores");
                Impostos = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
                Impostos2 = new Model.Imposto().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            };

            backOn.RunWorkerCompleted += (s, e) =>
            {
                Start();

                if (IdPdtSelecionado > 0)
                {
                    LoadData();
                }
                else
                {
                    _modelItem = new Item {Tipo = "Produtos", Id = IdPdtSelecionado};
                    if (_modelItem.Save(_modelItem, false))
                    {
                        IdPdtSelecionado = _modelItem.GetLastId();
                        LoadData();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar.", Alert.AlertType.error);
                        Close();
                    }
                }
            };

            workerBackEstoque.DoWork += (s, e) =>
            {
                var query = new ItemEstoqueMovimentacao().Query()
                    .LeftJoin("USUARIOS", "USUARIOS.id_user", "ITEM_MOV_ESTOQUE.id_usuario")
                    .Select("ITEM_MOV_ESTOQUE.*", "USUARIOS.id_user", "USUARIOS.nome as nome_user")
                    .Where("id_item", IdPdtSelecionado)
                    .OrderByDesc("criado");

                if (LimitShowStock > 0) query.Limit(LimitShowStock);
                
                ListEstoque = query.Get();
            };
            
            workerBackEstoque.RunWorkerCompleted += (s, e) =>
            {
                GetDataTableEstoque(listaEstoque);
            };

            btnVariacao.Click += (s, e) =>
            {
                ModalVariacao.idProduto = IdPdtSelecionado;
                var form = new ModalVariacao();
                form.ShowDialog();
            };

            GridAdicionais.CellContentClick += (s, e) =>
            {
                if (GridAdicionais.Columns[e.ColumnIndex].Name == "Selecione")
                    GridAdicionais.SelectedRows[0].Cells["Selecione"].Value = (bool) GridAdicionais.SelectedRows[0].Cells["Selecione"].Value == false;
            };

            GridAdicionais.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridAdicionais.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridAdicionais.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridAdicionais.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };

            GridCombos.CellContentClick += (s, e) =>
            {
                if (GridCombos.Columns[e.ColumnIndex].Name == "Selecione")
                    GridCombos.SelectedRows[0].Cells["Selecione"].Value = (bool)GridCombos.SelectedRows[0].Cells["Selecione"].Value == false;
            };

            GridCombos.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridCombos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridCombos.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridCombos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}
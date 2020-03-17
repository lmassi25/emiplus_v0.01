using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaProdutos : Form
    {
        #region V

        private int ModoRapAva { get; set; }
        private static int Id { get; set; }
        private int ModoRapAvaConfig { get; set; }

        public static int idImposto { get; set; }
        public static string NCM { get; set; }

        private Model.Item _mItem = new Model.Item();
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();
        private Model.Nota _mNota = new Model.Nota();

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        #endregion V

        public TelaProdutos()
        {
            InitializeComponent();
            Id = Nota.Id;
            _mNota = _mNota.FindById(Id).FirstOrDefault<Model.Nota>();

            if (_mNota == null)
            {
                Alert.Message("Ação não permitida", "Referência de Pedido não identificada", Alert.AlertType.warning);
                return;
            }

            _mPedido = _mPedido.FindById(_mNota.id_pedido).FirstOrDefault<Model.Pedido>();

            DisableCampos();
            Eventos();
        }

        /// <summary>
        /// Carrega e adiciona os itens no pedido!
        /// Function: collection.Lookup recupera o ID
        /// </summary>
        private void LoadItens()
        {
            if (BuscarProduto.Text.Length > 0)
            {
                Model.Item item = _mItem.FindAll()
                    .Where("excluir", 0)
                    .Where("tipo", "Produtos")
                    .Where("codebarras", BuscarProduto.Text)
                    .OrWhere("referencia", BuscarProduto.Text)
                    .FirstOrDefault<Model.Item>();

                if (item != null)
                {
                    BuscarProduto.Text = item.Nome;
                }
                else
                    ModalItens(); // Abre modal de Itens caso não encontre nenhum item no autocomplete, ou pressionando Enter.
            }

            // Valida a busca pelo produto e faz o INSERT, gerencia também o estoque e atualiza os totais
            AddItem();

            PedidoModalItens.NomeProduto = "";
        }

        private void DisableCampos()
        {
            if (Nota.disableCampos)
            {
                BuscarProduto.Enabled = false;
                Quantidade.Enabled = false;
                Preco.Enabled = false;
                Medidas.Enabled = false;
                DescontoPorcentagem.Enabled = false;
                DescontoReais.Enabled = false;
                addProduto.Enabled = false;
            }
        }

        /// <summary>
        /// Atualiza o pedido com as somas totais.
        /// </summary>
        private void LoadTotais()
        {
            qtdItens.Text = GridListaProdutos.RowCount.ToString();

            Model.Pedido data = _mPedido.SaveTotais(_mPedidoItens.SumTotais(_mNota.id_pedido));
            _mPedido.Save(data);

            totalNfe.Text = Validation.FormatPrice(_mPedido.Total);
            descontoNfe.Text = Validation.FormatPrice(_mPedido.Desconto);
            freteNfe.Text = Validation.FormatPrice(_mPedido.Frete);
            vlrSeguro.Text = Validation.FormatPrice(_mPedido.Seguro);
            vlrProdutos.Text = Validation.FormatPrice(_mPedido.Produtos);

            baseICMS.Text = Validation.FormatPrice(_mPedido.ICMSBASE);
            vlrICMS.Text = Validation.FormatPrice(_mPedido.ICMS);
            ICMSST.Text = Validation.FormatPrice(_mPedido.ICMSSTBASE);
            vlrICMSST.Text = Validation.FormatPrice(_mPedido.ICMSST);
            vlrIPI.Text = Validation.FormatPrice(_mPedido.IPI);
            vlrPIS.Text = Validation.FormatPrice(_mPedido.PIS);
            vlrCOFINS.Text = Validation.FormatPrice(_mPedido.COFINS);
            vlrDespesas.Text = Validation.FormatPrice(_mPedido.Despesa);

            var tributos = _mPedidoItens.SumTotais(_mNota.id_pedido);
            vlrTributos.Text = Validation.FormatPrice(tributos["FEDERAL"] + tributos["ESTADUAL"] + tributos["MUNICIPAL"]);
        }

        /// <summary>
        /// Janela para selecionar itens não encontrados.
        /// </summary>
        private void ModalItens()
        {
            if (collection.Lookup(nomeProduto()[0]) == 0)
            {
                if ((Application.OpenForms["PedidoModalItens"] as PedidoModalItens) == null)
                {
                    PedidoModalItens.txtSearch = nomeProduto()[0];
                    PedidoModalItens form = new PedidoModalItens();
                    form.TopMost = true;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        BuscarProduto.Text = PedidoModalItens.NomeProduto;
                        Preco.Text = Validation.FormatPrice(PedidoModalItens.ValorVendaProduto);
                        PedidoModalItens.NomeProduto = "";

                        if (PedidoModalItens.ValorVendaProduto == 0 && ModoRapAva == 0)
                        {
                            AlterarModo();
                            ModoRapAvaConfig = 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Limpa os input text.
        /// </summary>
        private void ClearForms()
        {
            BuscarProduto.Clear();
            Quantidade.Clear();
            Preco.Clear();
            DescontoPorcentagem.Clear();
            DescontoReais.Clear();
        }

        /// <summary>
        /// Altera o modo do pedido, para avançado e simples.
        /// 1 = Avançado, 0 = Simples
        /// </summary>
        private void AlterarModo()
        {
            if (ModoRapAva == 1)
            {
                ModoRapAva = 0;
                panelAvancado.Visible = false;
                ModoRapido.Text = "Modo Avançado (F1) ?";
            }
            else
            {
                ModoRapAva = 1;
                panelAvancado.Visible = true;
                ModoRapido.Text = "Modo Rápido (F1) ?";
            }
        }

        public string[] nomeProduto()
        {
            string[] nomeProduto = new string[2];

            string[] checkNome = BuscarProduto.Text.Split(new string[] { " + ", "+" }, StringSplitOptions.None);

            nomeProduto[0] = checkNome[0];
            if (checkNome.Length == 1)
                nomeProduto[1] = "";
            else
                nomeProduto[1] = checkNome[1];

            return nomeProduto;
        }

        /// <summary>
        /// Adiciona item ao pedido, controla o estoque e atualiza os totais.
        /// </summary>
        private void AddItem()
        {
            if (collection.Lookup(nomeProduto()[0]) > 0 && String.IsNullOrEmpty(PedidoModalItens.NomeProduto))
            {
                var itemId = collection.Lookup(nomeProduto()[0]);
                Model.Item item = _mItem.FindById(itemId).WhereFalse("excluir").Where("tipo", "Produtos").FirstOrDefault<Model.Item>();

                if (ModoRapAva == 0)
                    Medidas.SelectedItem = item.Medida;

                double QuantidadeTxt = Validation.ConvertToDouble(Quantidade.Text);
                double DescontoReaisTxt = Validation.ConvertToDouble(DescontoReais.Text);
                double DescontoPorcentagemTxt = Validation.ConvertToDouble(DescontoPorcentagem.Text);
                string MedidaTxt = Medidas.Text;
                double PriceTxt = Validation.ConvertToDouble(Preco.Text);

                #region Controle de estoque
                var controlarEstoque = IniFile.Read("ControlarEstoque", "Comercial");
                if (!string.IsNullOrEmpty(controlarEstoque) && controlarEstoque == "True")
                {
                    if (item.EstoqueAtual <= 0)
                    {
                        Alert.Message("Opps", "Você está sem estoque desse produto.", Alert.AlertType.warning);
                        return;
                    }
                }

                if (PriceTxt == 0)
                {
                    if (DescontoReaisTxt > item.ValorVenda || DescontoReaisTxt > item.Limite_Desconto || DescontoPorcentagemTxt > 101)
                    {
                        Alert.Message("Opps", "Não é permitido dar um desconto maior que o valor do item.", Alert.AlertType.warning);
                        return;
                    }
                }

                if (PriceTxt > 0)
                {
                    if (DescontoReaisTxt > PriceTxt || DescontoPorcentagemTxt >= 101)
                    {
                        Alert.Message("Opps", "Não é permitido dar um desconto maior que o valor do item.", Alert.AlertType.warning);
                        return;
                    }
                }

                double LimiteDescontoIni = 0;
                if (!String.IsNullOrEmpty(IniFile.Read("LimiteDesconto", "Comercial")))
                    LimiteDescontoIni = Validation.ConvertToDouble(IniFile.Read("LimiteDesconto", "Comercial"));

                if (item.Limite_Desconto != 0)
                {
                    if (DescontoReaisTxt > item.Limite_Desconto)
                    {
                        Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.", Alert.AlertType.warning);
                        return;
                    }

                    if (PriceTxt > 0)
                    {
                        var porcentagemValor = (PriceTxt / 100 * DescontoPorcentagemTxt);
                        if (porcentagemValor > item.Limite_Desconto)
                        {
                            Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.", Alert.AlertType.warning);
                            return;
                        }
                    }

                    if (PriceTxt == 0)
                    {
                        var porcentagemValor = (item.ValorVenda / 100 * DescontoPorcentagemTxt);
                        if (porcentagemValor > item.Limite_Desconto)
                        {
                            Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.", Alert.AlertType.warning);
                            return;
                        }
                    }
                }
                else
                {
                    if (LimiteDescontoIni != 0)
                    {
                        if (DescontoReaisTxt > LimiteDescontoIni)
                        {
                            Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.", Alert.AlertType.warning);
                            return;
                        }

                        if (PriceTxt == 0)
                        {
                            var porcentagemValor = (item.ValorVenda / 100 * DescontoPorcentagemTxt);
                            if (porcentagemValor > LimiteDescontoIni)
                            {
                                Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.", Alert.AlertType.warning);
                                return;
                            }
                        }

                        if (PriceTxt > 0)
                        {
                            var porcentagemValor = (PriceTxt / 100 * DescontoPorcentagemTxt);
                            if (porcentagemValor > LimiteDescontoIni)
                            {
                                Alert.Message("Opps", "Não é permitido dar um desconto maior que o permitido.", Alert.AlertType.warning);
                                return;
                            }
                        }
                    }
                }
                #endregion

                var pedidoItem = new Model.PedidoItem();
                pedidoItem.SetId(0)
                    .SetTipo("Produtos")
                    .SetPedidoId(_mNota.id_pedido)
                    .SetAdicionalNomePdt(nomeProduto()[1])
                    .SetItem(item)
                    .SetQuantidade(QuantidadeTxt)
                    .SetMedida(MedidaTxt)
                    .SetDescontoReal(DescontoReaisTxt);

                if (!pedidoItem.SetValorVenda(PriceTxt))
                {
                    if (ModoRapAva == 0)
                    {
                        AlterarModo();
                        ModoRapAvaConfig = 1;
                    }

                    Preco.Select();
                    Preco.Focus();
                    return;
                }

                pedidoItem.SetDescontoPorcentagens(DescontoPorcentagemTxt);
                pedidoItem.SomarTotal();
                pedidoItem.Save(pedidoItem);

                if (item.Tipo == "Produtos")
                {
                    new Controller.Imposto().SetImposto(pedidoItem.GetLastId());

                    // Class Estoque -> Se for igual 'Compras', adiciona a quantidade no estoque do Item, se não Remove a quantidade do estoque do Item
                    if (Home.pedidoPage == "Compras" || Home.pedidoPage == "Devoluções")
                        new Controller.Estoque(pedidoItem.GetLastId(), Home.pedidoPage, "Adicionar Produto").Add().Item();
                    else
                        new Controller.Estoque(pedidoItem.GetLastId(), Home.pedidoPage, "Adicionar Produto").Remove().Item();
                }

                new Controller.Imposto().SetImposto(pedidoItem.GetLastId());

                // Carrega a Grid com o Item adicionado acima.
                GetDataTableItens(GridListaProdutos, _mNota.id_pedido);

                // Atualiza o total do pedido, e os totais da tela
                LoadTotais();

                // Limpa os campos
                ClearForms();

                // Verifica se modo é avançado e altera para modo simples, apenas se modo simples for padrão
                if (ModoRapAva == 1 && ModoRapAvaConfig == 1)
                {
                    AlterarModo();
                    ModoRapAvaConfig = 0;
                }

                BuscarProduto.Select();
            }
        }

        /// <summary>
        /// Adiciona os eventos de 'KeyDown' a todos os controls com a function KeyDowns
        /// </summary>
        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(false, GridListaProdutos);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(true, GridListaProdutos);
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    Close();
                    break;

                case Keys.F1:
                    AlterarModo();
                    break;

                case Keys.F2:
                    BuscarProduto.Focus();
                    break;

                case Keys.F3:
                    if (GridListaProdutos.SelectedRows.Count > 0)
                    {
                        if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                        {
                            var itemName = GridListaProdutos.SelectedRows[0].Cells["Descrição"].Value;

                            var result = AlertOptions.Message("Atenção!", $"Cancelar o item ('{itemName}') no pedido?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                            if (result)
                            {
                                var idPedidoItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);

                                GridListaProdutos.Rows.RemoveAt(GridListaProdutos.SelectedRows[0].Index);

                                if (Home.pedidoPage != "Compras")
                                    new Controller.Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Add().Item();
                                else
                                    new Controller.Estoque(idPedidoItem, Home.pedidoPage, "Atalho F3 Cancelar").Remove().Item();

                                _mPedidoItens.Remove(idPedidoItem);

                                LoadTotais();
                            }
                        }
                    }
                    break;
            }
        }

        private void Eventos()
        {
            BuscarProduto.Select();

            KeyDown += KeyDowns;
            ModoRapido.KeyDown += KeyDowns;
            BuscarProduto.KeyDown += KeyDowns;
            GridListaProdutos.KeyDown += KeyDowns;
            Quantidade.KeyDown += KeyDowns;
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                // Autocomplete de produtos
                collection = _mItem.AutoComplete("Produtos");
                BuscarProduto.AutoCompleteCustomSource = collection;

                Medidas.DataSource = Support.GetMedidas();

                SetHeadersTable(GridListaProdutos);
                GetDataTableItens(GridListaProdutos, _mNota.id_pedido);
                LoadTotais();
                ClearForms();
                BuscarProduto.Select();

                if (_mNota.Status != "Pendente")
                {
                    progress5.Visible = false;
                    pictureBox1.Visible = false;
                    label9.Visible = false;
                }
            };

            ModoRapido.Click += (s, e) => AlterarModo();

            Next.Click += (s, e) => OpenForm.Show<TelaFrete>(this);

            Back.Click += (s, e) => Close();

            addProduto.Click += (s, e) => LoadItens();

            BuscarProduto.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (ModoRapAva == 1)
                    {
                        if (!string.IsNullOrEmpty(nomeProduto()[0]))
                        {
                            var item = _mItem.FindById(collection.Lookup(nomeProduto()[0])).FirstOrDefault<Model.Item>();
                            if (item != null)
                            {
                                Preco.Text = Validation.FormatPrice(item.ValorVenda);
                                Medidas.SelectedItem = item.Medida;
                            }

                            Quantidade.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(nomeProduto()[0]))
                        ModalItens();
                    else
                        LoadItens();
                }
            };

            GridListaProdutos.DoubleClick += (s, e) =>
            {
                if (GridListaProdutos.SelectedRows.Count > 0)
                {
                    EditProduct.idPdt = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                    EditProduct.nrItem = Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["#"].Value);
                    EditProduct f = new EditProduct();
                    f.TopMost = true;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        GetDataTableItens(GridListaProdutos, _mNota.id_pedido);
                        LoadTotais();
                    }
                }
            };

            Preco.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            Preco.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };

            Quantidade.KeyPress += (s, e) => Masks.MaskDouble(s, e);
            Quantidade.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (String.IsNullOrEmpty(nomeProduto()[0]))
                        BuscarProduto.Focus();
                    else if (ModoRapAva == 1 && !String.IsNullOrEmpty(nomeProduto()[0]))
                        Preco.Focus();
                    else
                        LoadItens();
                }
            };

            DescontoPorcentagem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };
            DescontoReais.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadItens();
            };

            AlterarImposto.Click += (s, e) =>
            {
                if (GridListaProdutos.SelectedRows.Count > 0)
                {
                    AlterarImposto f = new AlterarImposto();
                    f.TopMost = true;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (idImposto > 0)
                        {
                            foreach (DataGridViewRow item in GridListaProdutos.Rows)
                                if ((bool)item.Cells["Selecione"].Value == true)
                                    new Controller.Imposto().SetImposto(Validation.ConvertToInt32(item.Cells["ID"].Value), idImposto, "NFe", NCM);
                        }

                        GetDataTableItens(GridListaProdutos, _mNota.id_pedido);
                    }

                    NCM = "";
                    idImposto = 0;
                }

                BuscarProduto.Select();
            };

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridListaProdutos.Rows)
                {
                    if (btnMarcarCheckBox.Text == "Marcar Todos")
                    {
                        if ((bool)item.Cells["Selecione"].Value == false)
                        {
                            item.Cells["Selecione"].Value = true;
                            AlterarImposto.Visible = true;
                        }
                    }
                    else
                    {
                        item.Cells["Selecione"].Value = false;
                        AlterarImposto.Visible = false;
                    }
                }

                if (btnMarcarCheckBox.Text == "Marcar Todos")
                    btnMarcarCheckBox.Text = "Desmarcar Todos";
                else
                    btnMarcarCheckBox.Text = "Marcar Todos";
            };

            GridListaProdutos.CellClick += (s, e) =>
            {
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value = true;
                        AlterarImposto.Visible = true;
                    }
                    else
                    {
                        GridListaProdutos.SelectedRows[0].Cells["Selecione"].Value = false;

                        bool hideBtns = false;
                        foreach (DataGridViewRow item in GridListaProdutos.Rows)
                            if ((bool)item.Cells["Selecione"].Value == true)
                            {
                                hideBtns = true;
                            }

                        AlterarImposto.Visible = hideBtns;
                    }
                }
            };

            GridListaProdutos.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridListaProdutos.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridListaProdutos.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 18;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Selecione";
            checkColumn.Name = "Selecione";
            checkColumn.FlatStyle = FlatStyle.Standard;
            checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
            checkColumn.Width = 60;
            Table.Columns.Insert(0, checkColumn);

            Table.Columns[1].Name = "ID";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "#";
            Table.Columns[2].Width = 50;
            Table.Columns[2].MinimumWidth = 50;
            Table.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Table.Columns[3].Name = "Código";
            Table.Columns[3].Width = 100;
            Table.Columns[3].Visible = false;

            Table.Columns[4].Name = "Descrição";
            Table.Columns[4].MinimumWidth = 150;

            Table.Columns[5].Name = "Quantidade";
            Table.Columns[5].Width = 100;
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[6].Name = "Unitário";
            Table.Columns[6].Width = 100;
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[7].Name = "Desconto";
            Table.Columns[7].Width = 100;
            Table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[8].Name = "Frete";
            Table.Columns[8].Width = 100;
            Table.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[9].Name = "NCM";
            Table.Columns[9].Width = 100;
            Table.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[10].Name = "CFOP";
            Table.Columns[10].Width = 100;
            Table.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[11].Name = "Origem";
            Table.Columns[11].Width = 100;
            Table.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[12].Name = "ICMS";
            Table.Columns[12].Width = 100;
            Table.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[13].Name = "IPI";
            Table.Columns[13].Width = 100;
            Table.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[14].Name = "PIS";
            Table.Columns[14].Width = 100;
            Table.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[15].Name = "COFINS";
            Table.Columns[15].Width = 100;
            Table.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[16].Name = "Federal";
            Table.Columns[16].Width = 100;
            Table.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[17].Name = "Estadual";
            Table.Columns[17].Width = 100;
            Table.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[18].Name = "Total";
            Table.Columns[18].Width = 100;
            Table.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            bool impostos = true;
            Table.Columns[9].Visible = impostos;
            Table.Columns[10].Visible = impostos;
            Table.Columns[11].Visible = impostos;
            Table.Columns[12].Visible = impostos;
            Table.Columns[13].Visible = impostos;
            Table.Columns[14].Visible = impostos;
            Table.Columns[15].Visible = impostos;
            Table.Columns[16].Visible = impostos;
            Table.Columns[17].Visible = impostos;
        }

        public void GetDataTableItens(DataGridView Table, int idPedido)
        {
            Table.Rows.Clear();

            if (idPedido <= 0)
                return;

            var itens = new Controller.PedidoItem().GetDataItens(idPedido);

            int count = 1;
            foreach (var data in itens)
            {
                Table.Rows.Add(
                    false,
                    data.ID,
                    count++,
                    data.REFERENCIA,
                    data.XPROD,
                    data.QUANTIDADE + " " + data.MEDIDA,
                    Validation.FormatPrice(Validation.ConvertToDouble(data.VALORVENDA), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.DESCONTO), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.FRETE), true),
                    String.IsNullOrEmpty(data.NCM) ? "0" : data.NCM,
                    String.IsNullOrEmpty(data.CFOP) ? "0" : data.CFOP,
                    String.IsNullOrEmpty(data.ORIGEM) ? "N/D" : data.ORIGEM,
                    String.IsNullOrEmpty(data.ICMS) ? "0" : data.ICMS,
                    String.IsNullOrEmpty(data.IPI) ? "0" : data.IPI,
                    String.IsNullOrEmpty(data.PIS) ? "0" : data.PIS,
                    String.IsNullOrEmpty(data.COFINS) ? "0" : data.COFINS,
                    Validation.FormatPrice(Validation.ConvertToDouble(data.FEDERAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.ESTADUAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.TOTAL), true)
                );
            }

            Table.Sort(Table.Columns[2], ListSortDirection.Descending);
            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
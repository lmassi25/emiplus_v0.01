using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarProdutos : Form
    {
        private ImportarNfe dataNfe = new ImportarNfe();
        private Model.Item _mItem = new Model.Item();
        private Model.Pessoa _mPessoa = new Model.Pessoa();
        private Model.PessoaContato _mPessoaContato = new Model.PessoaContato();
        private Model.PessoaEndereco _mPessoaAddr = new Model.PessoaEndereco();

        public static ArrayList produtos = new ArrayList();
        public static ArrayList fornecedores = new ArrayList();

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public ImportarProdutos()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        /// Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private int IDFornecedor(dynamic item)
        {
            int idFornecedor = 0;
            string cnpj = item.GetFornecedor().CPFcnpj;
            var p = _mPessoa.Query().Select("*").Where("cpf", cnpj).FirstOrDefault<Model.Pessoa>();
            if (p != null)
            {
                idFornecedor = p.Id;
                return idFornecedor;
            }
            else
            {
                Model.Pessoa fornecedorCadastro = new Model.Pessoa();
                fornecedorCadastro.Id = 0;
                fornecedorCadastro.Tipo = "Fornecedores";
                fornecedorCadastro.Pessoatipo = "Jurídica";
                fornecedorCadastro.CPF = item.GetFornecedor().CPFcnpj;
                fornecedorCadastro.Nome = item.GetFornecedor().razaoSocial;
                fornecedorCadastro.RG = item.GetFornecedor().IE;
                if (fornecedorCadastro.Save(fornecedorCadastro, false))
                {
                    idFornecedor = fornecedorCadastro.GetLastId();

                    Model.PessoaEndereco fornecedorAddr = new Model.PessoaEndereco();
                    fornecedorAddr.Id = 0;
                    fornecedorAddr.Id_pessoa = idFornecedor;
                    fornecedorAddr.Cep = item.GetFornecedor().Addr_CEP;
                    fornecedorAddr.Rua = item.GetFornecedor().Addr_Rua;
                    fornecedorAddr.Estado = item.GetFornecedor().Addr_UF;
                    fornecedorAddr.Cidade = item.GetFornecedor().Addr_Cidade;
                    fornecedorAddr.Nr = item.GetFornecedor().Addr_Nr;
                    fornecedorAddr.Bairro = item.GetFornecedor().Addr_Bairro;
                    fornecedorAddr.IBGE = item.GetFornecedor().Addr_IBGE;
                    fornecedorAddr.Pais = "Brasil";
                    fornecedorAddr.Save(fornecedorAddr, false);

                    if (!string.IsNullOrEmpty(item.GetFornecedor().Email) || !string.IsNullOrEmpty(item.GetFornecedor().Tel))
                    {
                        Model.PessoaContato fornecedorContato = new Model.PessoaContato();
                        fornecedorContato.Id = 0;
                        fornecedorContato.Contato = "Contato 1";
                        fornecedorContato.Id_pessoa = idFornecedor;
                        fornecedorContato.Email = item.GetFornecedor().Email;
                        fornecedorContato.Telefone = item.GetFornecedor().Tel;
                        fornecedorContato.Save(fornecedorContato, false);
                    }

                    return idFornecedor;
                }
            }

            return 0;
        }

        /// <summary>
        /// Pega os produtos das notas e cria um novo arraylist
        /// </summary>
        /// <returns></returns>
        private ArrayList GetAllProdutos()
        {
            ArrayList allProdutos = new ArrayList();

            var count = dataNfe.GetNotas();
            if (count.Count > 0)
            {
                foreach (Controller.ImportarNfe item in count)
                {
                    int idFornecedor = IDFornecedor(item);
                    foreach (dynamic pdt in item.GetProdutos())
                    {
                        object produtos = new
                        {
                            pdt,
                            Fornecedor = idFornecedor
                        };

                        allProdutos.Insert(0, produtos);
                    }
                }

                return allProdutos;
            }

            return null;
        }

        private void LoadProdutos()
        {
            var pdt = GetAllProdutos();
            SetDataTable(GridLista, pdt);
        }

        /// <summary>
        /// Table dos produtos
        /// </summary>
        /// <param name="Table">GridLista</param>
        /// <param name="dataProdutos">array dos produtos</param>
        private void SetDataTable(DataGridView Table, ArrayList dataProdutos)
        {
            Table.ColumnCount = 12;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            {
                checkColumn.HeaderText = "Importar";
                checkColumn.Name = "Importar";
                checkColumn.FlatStyle = FlatStyle.Standard;
                checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
                checkColumn.Width = 60;
            }
            Table.Columns.Insert(0, checkColumn);

            DataGridViewImageColumn columnImg = new DataGridViewImageColumn();
            {
                columnImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnImg.HeaderText = "Cadastrado";
                columnImg.Name = "Cadastrado";
                columnImg.Width = 70;
            }
            Table.Columns.Insert(1, columnImg);

            Table.Columns[2].Name = "Referência";
            Table.Columns[2].Width = 70;

            Table.Columns[3].Name = "Cód. de Barras";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Descrição";
            Table.Columns[4].Width = 130;

            Table.Columns[5].Name = "Medida";
            Table.Columns[5].Width = 60;

            if (ImportarNfe.optionSelected != 1)
                Table.Columns[6].Name = "Qtd. (+)";
            else
                Table.Columns[6].Name = "Qtd.";
            Table.Columns[6].Width = 60;
            if (ImportarNfe.optionSelected == 1)
                Table.Columns[6].Visible = false;

            Table.Columns[7].Name = "Vlr. Compra";
            Table.Columns[7].Width = 100;
            Table.Columns[7].ReadOnly = true;

            Table.Columns[8].Name = "Vlr. Venda";
            Table.Columns[8].Width = 100;
            Table.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[8].CellTemplate.Style.BackColor = Color.Beige;

            DataGridViewImageColumn columnImgEdit = new DataGridViewImageColumn();
            {
                columnImgEdit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnImgEdit.HeaderText = "Editar";
                columnImgEdit.Name = "Editar";
                columnImgEdit.Width = 70;
            }
            Table.Columns.Insert(9, columnImgEdit);

            Table.Columns[10].Name = "ID";
            Table.Columns[10].Visible = false;

            Table.Columns[11].Name = "CATEGORIAID";
            Table.Columns[11].Visible = false;

            Table.Columns[12].Name = "EDITADO";
            Table.Columns[12].Visible = false;

            Table.Columns[13].Name = "IDVINCULO";
            Table.Columns[13].Visible = false;

            Table.Columns[14].Name = "Fornecedor";
            Table.Columns[14].Visible = false;

            Table.Rows.Clear();

            foreach (dynamic item in dataProdutos)
            {
                dynamic findItem = null;
                if (FindItem(item.pdt.CodeBarras) != null)
                {
                    findItem = FindItem(item.pdt.CodeBarras);
                }

                bool goBack = false;
                int rowIndex = -1;
                foreach (DataGridViewRow row in Table.Rows)
                    if (row.Cells["Cód. de Barras"].Value.ToString().Equals(item.pdt.CodeBarras))
                    {
                        rowIndex = row.Index;
                        goBack = true;
                        var getQtd = GridLista.Rows[rowIndex].Cells[6].Value;
                        var getValorCompra = GridLista.Rows[rowIndex].Cells["Vlr. Compra"].Value;
                        //GridLista.Rows[rowIndex].Cells["Vlr. Compra"].Value = Validation.FormatPrice(Validation.ConvertToDouble(getValorCompra) + Validation.ConvertToDouble(FormatPriceXml(item.pdt.VlrCompra)));
                        GridLista.Rows[rowIndex].Cells[6].Value = Validation.ConvertToDouble(getQtd) + Validation.ConvertToDouble(Validation.FormatPriceXml(item.pdt.Quantidade));
                    }

                if (goBack)
                    continue;

                Table.Rows.Add(
                    true,
                    findItem != null ? new Bitmap(Properties.Resources.success16x) : new Bitmap(Properties.Resources.error16x),
                    item.pdt.Referencia,
                    item.pdt.CodeBarras,
                    item.pdt.Descricao,
                    item.pdt.Medida,
                    Validation.FormatMedidas(item.pdt.Medida, Validation.ConvertToDouble(Validation.FormatPriceXml(item.pdt.Quantidade))),
                    Validation.FormatPriceXml(item.pdt.VlrCompra),
                    findItem != null ? Validation.FormatPrice(Validation.ConvertToDouble(findItem.VALORVENDA)) : "00,00",
                    new Bitmap(Properties.Resources.edit16x),
                    findItem != null ? findItem.ID : 0,
                    findItem != null ? findItem.CATEGORIAID : "",
                    "0",
                    "0",
                    item.Fornecedor
                );
            }

            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// Vincular ou editar produto
        /// </summary>
        private void VincularProduto(bool edit = false)
        {
            if (edit)
            {
                Model.Item _mItem = new Model.Item();

                panelVinculacao.Visible = true;
                panelVinculacao.Location = new Point(34, 453);
                label10.Visible = false;
                pictureBox4.Visible = false;
                BuscarProduto.Visible = false;
                btnVincular.Visible = false;
                label9.Text = "Edite o produto selecionado abaixo";

                if (ImportarNfe.optionSelected == 1)
                {
                    label21.Visible = false;
                    pictureBox8.Visible = false;
                    estoqueatual.Visible = false;
                    label23.Visible = false;
                    estoqueProduto.Visible = false;
                    label24.Visible = false;
                    novoEstoque.Visible = false;
                }

                string id = GridLista.SelectedRows[0].Cells["ID"].Value.ToString();
                string editado = GridLista.SelectedRows[0].Cells["EDITADO"].Value.ToString();

                // TRÁS ALGUNS DADOS DO BANCO CASO EXISTA ALGUM REGISTRO, SE NÃO, PEGA OS DADOS DO DATAGRID "PARA EDITAR"
                _mItem = _mItem.Query().Select("*").Where("ID", id).Where("excluir", 0).FirstOrDefault<Model.Item>();
                if (_mItem != null && editado == "0")
                {
                    nome.Text = _mItem?.Nome ?? "";
                    codebarras.Text = _mItem?.CodeBarras ?? "";
                    referencia.Text = _mItem?.Referencia ?? "";
                    valorcompra.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                    estoqueatual.Text = GridLista.SelectedRows[0].Cells[6].Value.ToString();
                    valorvenda.Text = Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorVenda));
                    valorCompraAtual.Text = Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorCompra), true);
                    estoqueProduto.Text = _mItem.EstoqueAtual.ToString();
                    novoEstoque.Text = (Validation.ConvertToDouble(estoqueatual.Text) + Validation.ConvertToDouble(estoqueProduto.Text)).ToString();

                    if (_mItem.Medida != null)
                        Medidas.SelectedItem = _mItem.Medida;

                    Categorias.SelectedValue = _mItem.Categoriaid;
                }
                else
                {
                    nome.Text = GridLista.SelectedRows[0].Cells["Descrição"].Value.ToString();
                    codebarras.Text = GridLista.SelectedRows[0].Cells["Cód. de Barras"].Value.ToString();
                    referencia.Text = GridLista.SelectedRows[0].Cells["Referência"].Value.ToString();
                    valorcompra.Text = Validation.FormatPrice(Validation.ConvertToDouble(GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value));
                    estoqueatual.Text = GridLista.SelectedRows[0].Cells[6].Value.ToString();
                    valorvenda.Text = GridLista.SelectedRows[0].Cells["Vlr. Venda"].Value.ToString();
                    valorCompraAtual.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                    estoqueProduto.Text = "N/D";

                    Categorias.SelectedValue = GridLista.SelectedRows[0].Cells["CATEGORIAID"].Value;
                    novoEstoque.Text = estoqueatual.Text;
                    Medidas.SelectedItem = GridLista.SelectedRows[0].Cells["Medida"].Value.ToString();
                }

                return;
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                if (GridLista.SelectedRows.Count > 0)
                {
                    _mItem = new Model.Item();

                    panelVinculacao.Visible = true;

                    int id = collection.Lookup(BuscarProduto.Text); // id produto
                    string editado = GridLista.SelectedRows[0].Cells["EDITADO"].Value.ToString();

                    _mItem = _mItem.Query().Select("*").Where("ID", id).Where("excluir", 0).FirstOrDefault<Model.Item>();
                    if (_mItem != null && editado == "0")
                    {
                        IDPDT.Text = _mItem?.Id.ToString() ?? "0";
                        nome.Text = _mItem?.Nome ?? "";
                        codebarras.Text = _mItem?.CodeBarras ?? "";
                        referencia.Text = _mItem?.Referencia ?? "";
                        valorcompra.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                        estoqueatual.Text = GridLista.SelectedRows[0].Cells[6].Value.ToString();
                        valorvenda.Text = Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorVenda));
                        valorCompraAtual.Text = Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorCompra), true);
                        estoqueProduto.Text = _mItem.EstoqueAtual.ToString();
                        novoEstoque.Text = (Validation.ConvertToDouble(estoqueatual.Text) + Validation.ConvertToDouble(estoqueProduto.Text)).ToString();

                        if (_mItem.Medida != null)
                            Medidas.SelectedItem = _mItem.Medida;

                        Categorias.SelectedValue = _mItem.Categoriaid;
                    }
                    else
                    {
                        Alert.Message("Oppss", "Não encontramos o produto.", Alert.AlertType.warning);
                    }

                    return;
                }

                Alert.Message("Oppss", "Selecione um produto para vincular.", Alert.AlertType.warning);
            }
        }

        /// <summary>
        /// Salva os dados na datagrid
        /// </summary>
        private void SalvarProduto()
        {
            GridLista.SelectedRows[0].Cells["Descrição"].Value = nome.Text;
            GridLista.SelectedRows[0].Cells["Cód. de Barras"].Value = codebarras.Text;
            GridLista.SelectedRows[0].Cells["Referência"].Value = referencia.Text;
            GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value = valorcompra.Text;

            if (ImportarNfe.optionSelected == 1)
                GridLista.SelectedRows[0].Cells[6].Value = 0;

            if (ImportarNfe.optionSelected == 2)
                GridLista.SelectedRows[0].Cells[6].Value = estoqueatual.Text;

            GridLista.SelectedRows[0].Cells["Medida"].Value = Medidas.Text;
            GridLista.SelectedRows[0].Cells["Vlr. Venda"].Value = valorvenda.Text;

            if (Categorias.SelectedValue != null)
                GridLista.SelectedRows[0].Cells["CATEGORIAID"].Value = (int)Categorias.SelectedValue;

            GridLista.SelectedRows[0].Cells["EDITADO"].Value = "1";
            GridLista.SelectedRows[0].Cells["IDVINCULO"].Value = IDPDT.Text;

            if (!string.IsNullOrEmpty(IDPDT.Text))
                GridLista.SelectedRows[0].Cells["Cadastrado"].Value = new Bitmap(Properties.Resources.success16x);

            GridLista.Enabled = true;
        }

        private dynamic FindItem(string codeBarras)
        {
            var findItem = _mItem.Query().Select("*").Where("codebarras", codeBarras).Where("excluir", 0).FirstOrDefault();
            if (findItem != null)
                return findItem;

            return null;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                LoadProdutos();
                AutoCompleteItens();

                var cat = new Model.Categoria().FindAll().Where("tipo", "Produtos").WhereFalse("excluir").OrderByDesc("nome").Get();
                if (cat.Count() > 0)
                {
                    Categorias.DataSource = cat;
                    Categorias.DisplayMember = "NOME";
                    Categorias.ValueMember = "ID";
                }

                Medidas.DataSource = Support.GetMedidas();

                if (ImportarNfe.optionSelected == 1)
                {
                    BuscarProduto.Visible = false;
                    label10.Visible = false;
                    pictureBox4.Visible = false;
                    btnVincular.Visible = false;
                    label9.Text = "Edite o produto selecionado abaixo";
                }
            };

            btnVincular.Click += (s, e) => VincularProduto();

            GridLista.CellClick += (s, e) =>
            {
                BuscarProduto.Enabled = true;

                if (GridLista.Columns[e.ColumnIndex].Name == "Editar")
                {
                    VincularProduto(true);
                    GridLista.Enabled = false;
                }

                if (GridLista.Columns[e.ColumnIndex].Name == "Importar")
                {
                    if ((bool)GridLista.SelectedRows[0].Cells["Importar"].Value == false)
                        GridLista.SelectedRows[0].Cells["Importar"].Value = true;
                    else
                        GridLista.SelectedRows[0].Cells["Importar"].Value = false;
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Importar" || GridLista.Columns[e.ColumnIndex].Name == "Editar")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Importar" || GridLista.Columns[e.ColumnIndex].Name == "Editar")
                    dataGridView.Cursor = Cursors.Default;
            };

            btnSalvarVinculacao.Click += (s, e) =>
            {
                SalvarProduto();

                if (ImportarNfe.optionSelected != 1)
                {
                    label10.Visible = true;
                    pictureBox4.Visible = true;
                    BuscarProduto.Visible = true;
                    btnVincular.Visible = true;
                    label9.Text = "Vincular a produtos existentes";
                    panelVinculacao.Location = new Point(34, 517);
                }

                panelVinculacao.Visible = false;
            };

            btnImportar.Click += (s, e) =>
            {
                produtos.Clear();

                int i = -1;
                foreach (DataGridViewRow item in GridLista.Rows)
                {
                    i++;
                    if ((bool)item.Cells["Importar"].Value == true)
                    {
                        int id = Validation.ConvertToInt32(item.Cells["IDVINCULO"].Value);
                        double estoque = 0;
                        string codeBarras = item.Cells["Cód. de Barras"].Value.ToString();

                        if (!string.IsNullOrEmpty(codeBarras))
                        {
                            Model.Item _mItem = new Model.Item();
                            _mItem = _mItem.Query().Select("*").Where("codebarras", codeBarras).Where("excluir", 0).FirstOrDefault<Model.Item>();
                            if (_mItem != null)
                            {
                                id = _mItem.Id;
                                estoque = _mItem.EstoqueAtual;
                            }
                        }

                        estoque = Validation.ConvertToDouble(item.Cells[6].Value) + estoque;

                        if (ImportarNfe.optionSelected == 1)
                            estoque = 0;

                        if (ImportarNfe.optionSelected == 3)
                            estoque = Validation.ConvertToDouble(item.Cells[6].Value);

                        produtos.Add(new
                        {
                            Ordem = i,
                            Id = id,
                            Referencia = item.Cells["Referência"].Value.ToString(),
                            CodeBarras = item.Cells["Cód. de Barras"].Value.ToString(),
                            Nome = item.Cells["Descrição"].Value.ToString(),
                            Medida = item.Cells["Medida"].Value.ToString(),
                            Estoque = estoque,
                            CategoriaId = Validation.ConvertToInt32(item.Cells["CATEGORIAID"].Value),
                            ValorCompra = Validation.ConvertToDouble(item.Cells["Vlr. Compra"].Value),
                            ValorVenda = Validation.ConvertToDouble(item.Cells["Vlr. Venda"].Value),
                            Fornecedor = item.Cells["Fornecedor"].Value.ToString()
                        });
                    }
                }

                if (ImportarNfe.optionSelected == 3)
                    OpenForm.Show<ImportarPagamentos>(this);
                else
                    OpenForm.Show<ImportarProdutosConcluido>(this);
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

                Validation.WarningInput(txt, warning);
            };

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridLista.Rows)
                {
                    if ((bool)item.Cells["Importar"].Value == true)
                    {
                        item.Cells["Importar"].Value = false;
                        btnMarcarCheckBox.Text = "Marcar Todos";
                    }
                    else
                    {
                        item.Cells["Importar"].Value = true;
                        btnMarcarCheckBox.Text = "Desmarcar Todos";
                    }
                }
            };

            Back.Click += (s, e) => Close();
        }
    }
}
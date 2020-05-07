using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Comercial;
using SqlKata.Execution;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarProdutos : Form
    {
        public static ArrayList produtos = new ArrayList();
        public static ArrayList fornecedores = new ArrayList();
        private Item _mItem = new Item();
        private readonly Pessoa _mPessoa = new Pessoa();

        private readonly KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();
        private readonly ImportarNfe dataNfe = new ImportarNfe();
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public ImportarProdutos()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        ///     Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item) collection.Add(itens.NOME, itens.ID);

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private int IdFornecedor(dynamic item)
        {
            var idFornecedor = 0;
            string cnpj = item.GetFornecedor().CPFcnpj;
            var p = _mPessoa.Query().Select("*").Where("cpf", cnpj).FirstOrDefault<Pessoa>();
            if (p != null)
            {
                idFornecedor = p.Id;
                return idFornecedor;
            }

            var fornecedorCadastro = new Pessoa
            {
                Id = 0,
                Tipo = "Fornecedores",
                Pessoatipo = "Jurídica",
                CPF = item.GetFornecedor().CPFcnpj,
                Nome = item.GetFornecedor().razaoSocial,
                RG = item.GetFornecedor().IE
            };
            if (!fornecedorCadastro.Save(fornecedorCadastro, false))
                return 0;

            idFornecedor = fornecedorCadastro.GetLastId();

            var fornecedorAddr = new PessoaEndereco
            {
                Id = 0,
                Id_pessoa = idFornecedor,
                Cep = item.GetFornecedor().Addr_CEP,
                Rua = item.GetFornecedor().Addr_Rua,
                Estado = item.GetFornecedor().Addr_UF,
                Cidade = item.GetFornecedor().Addr_Cidade,
                Nr = item.GetFornecedor().Addr_Nr,
                Bairro = item.GetFornecedor().Addr_Bairro,
                IBGE = item.GetFornecedor().Addr_IBGE,
                Pais = "Brasil"
            };
            fornecedorAddr.Save(fornecedorAddr, false);

            if (string.IsNullOrEmpty(item.GetFornecedor().Email) && string.IsNullOrEmpty(item.GetFornecedor().Tel))
                return idFornecedor;

            var fornecedorContato = new PessoaContato
            {
                Id = 0,
                Contato = "Contato 1",
                Id_pessoa = idFornecedor,
                Email = item.GetFornecedor().Email,
                Telefone = item.GetFornecedor().Tel
            };
            fornecedorContato.Save(fornecedorContato, false);

            return idFornecedor;

        }

        /// <summary>
        ///     Pega os produtos das notas e cria um novo arraylist
        /// </summary>
        /// <returns></returns>
        private ArrayList GetAllProdutos()
        {
            var allProdutos = new ArrayList();

            var count = dataNfe.GetNotas();
            if (count.Count > 0)
            {
                foreach (Controller.ImportarNfe item in count)
                {
                    var idFornecedor = IdFornecedor(item);
                    foreach (var pdt in item.GetProdutos())
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
        ///     Table dos produtos
        /// </summary>
        /// <param name="Table">GridLista</param>
        /// <param name="dataProdutos">array dos produtos</param>
        private void SetDataTable(DataGridView Table, ArrayList dataProdutos)
        {
            Table.ColumnCount = 13;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn();
            {
                checkColumn.HeaderText = @"Importar";
                checkColumn.Name = "Importar";
                checkColumn.FlatStyle = FlatStyle.Standard;
                checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
                checkColumn.Width = 60;
            }
            Table.Columns.Insert(0, checkColumn);

            var columnImg = new DataGridViewImageColumn();
            {
                columnImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnImg.HeaderText = @"Cadastrado";
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

            Table.Columns[6].Name = ImportarNfe.optionSelected != 1 ? "Qtd. (+)" : "Qtd.";
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

            var columnImgEdit = new DataGridViewImageColumn();
            {
                columnImgEdit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnImgEdit.HeaderText = @"Editar";
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

            Table.Columns[15].Name = "NCM";
            Table.Columns[15].Visible = false;

            Table.Rows.Clear();

            foreach (dynamic item in dataProdutos)
            {
                var goBack = false;
                var rowIndex = -1;
                foreach (DataGridViewRow row in Table.Rows)
                    if (row.Cells["Cód. de Barras"].Value.ToString() != "SEM GTIN")
                        if (row.Cells["Cód. de Barras"].Value.ToString().Equals(item.pdt.CodeBarras))
                        {
                            rowIndex = row.Index;
                            goBack = true;
                            var getQtd = GridLista.Rows[rowIndex].Cells[6].Value;
                            var getValorCompra = GridLista.Rows[rowIndex].Cells["Vlr. Compra"].Value;
                            //GridLista.Rows[rowIndex].Cells["Vlr. Compra"].Value = Validation.FormatPrice(Validation.ConvertToDouble(getValorCompra) + Validation.ConvertToDouble(FormatPriceXml(item.pdt.VlrCompra)));
                            GridLista.Rows[rowIndex].Cells[6].Value =
                                Validation.ConvertToDouble(getQtd) +
                                Validation.ConvertToDouble(Validation.FormatPriceXml(item.pdt.Quantidade));
                        }

                if (goBack)
                    continue;

                var codeBarrasUniq = item.pdt.CodeBarras == "SEM GTIN" ? Validation.CodeBarrasRandom() : (string) item.pdt.CodeBarras;

                dynamic findItem = null;
                if (FindItem(codeBarrasUniq, item.pdt.Descricao) != null)
                    findItem = FindItem(codeBarrasUniq, item.pdt.Descricao);

                Table.Rows.Add(
                    true,
                    findItem != null ? new Bitmap(Resources.success16x) : new Bitmap(Resources.error16x),
                    item.pdt.Referencia,
                    findItem != null ? findItem.CODEBARRAS : codeBarrasUniq,
                    item.pdt.Descricao,
                    item.pdt.Medida,
                    Validation.FormatMedidas(item.pdt.Medida,
                        Validation.ConvertToDouble(Validation.FormatPriceXml(item.pdt.Quantidade))),
                    Validation.FormatPriceXml(item.pdt.VlrCompra),
                    findItem != null
                        ? Validation.FormatPrice(Validation.ConvertToDouble(findItem.VALORVENDA))
                        : "00,00",
                    new Bitmap(Resources.edit16x),
                    findItem != null ? findItem.ID : 0,
                    findItem != null ? findItem.CATEGORIAID : "",
                    "0",
                    "0",
                    item.Fornecedor,
                    item.pdt.NCM
                );
            }

            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        ///     Vincular ou editar produto
        /// </summary>
        private void VincularProduto(bool edit = false)
        {
            if (edit)
            {
                var _mItem = new Item();

                panelVinculacao.Visible = true;
                panelVinculacao.Location = new Point(34, 453);
                label10.Visible = false;
                pictureBox4.Visible = false;
                BuscarProduto.Visible = false;
                btnVincular.Visible = false;
                label9.Text = @"Edite o produto selecionado abaixo";

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

                var id = GridLista.SelectedRows[0].Cells["ID"].Value.ToString();
                var editado = GridLista.SelectedRows[0].Cells["EDITADO"].Value.ToString();

                // TRÁS ALGUNS DADOS DO BANCO CASO EXISTA ALGUM REGISTRO, SE NÃO, PEGA OS DADOS DO DATAGRID "PARA EDITAR"
                _mItem = _mItem.Query().Select("*").Where("ID", id).Where("excluir", 0).FirstOrDefault<Item>();
                if (_mItem != null && editado == "0")
                {
                    nome.Text = _mItem.Nome ?? "";
                    codebarras.Text = _mItem?.CodeBarras ?? "";
                    referencia.Text = _mItem?.Referencia ?? "";
                    valorcompra.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                    estoqueatual.Text = GridLista.SelectedRows[0].Cells[6].Value.ToString();
                    valorvenda.Text = Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorVenda));
                    valorCompraAtual.Text =
                        Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorCompra), true);
                    estoqueProduto.Text = _mItem.EstoqueAtual.ToString();
                    novoEstoque.Text =
                        (Validation.ConvertToDouble(estoqueatual.Text) +
                         Validation.ConvertToDouble(estoqueProduto.Text)).ToString();

                    if (_mItem.Medida != null)
                        Medidas.SelectedItem = _mItem.Medida;

                    Categorias.SelectedValue = _mItem.Categoriaid;
                }
                else
                {
                    nome.Text = GridLista.SelectedRows[0].Cells["Descrição"].Value.ToString();
                    codebarras.Text = GridLista.SelectedRows[0].Cells["Cód. de Barras"].Value.ToString();
                    referencia.Text = GridLista.SelectedRows[0].Cells["Referência"].Value.ToString();
                    valorcompra.Text =
                        Validation.FormatPrice(
                            Validation.ConvertToDouble(GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value));
                    estoqueatual.Text = GridLista.SelectedRows[0].Cells[6].Value.ToString();
                    valorvenda.Text = GridLista.SelectedRows[0].Cells["Vlr. Venda"].Value.ToString();
                    valorCompraAtual.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                    estoqueProduto.Text = @"N/D";

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
                    _mItem = new Item();

                    panelVinculacao.Visible = true;

                    var id = collection.Lookup(BuscarProduto.Text); // id produto
                    var editado = GridLista.SelectedRows[0].Cells["EDITADO"].Value.ToString();

                    _mItem = _mItem.Query().Select("*").Where("ID", id).Where("excluir", 0).FirstOrDefault<Item>();
                    if (_mItem != null && editado == "0")
                    {
                        IDPDT.Text = _mItem?.Id.ToString();
                        nome.Text = _mItem?.Nome ?? "";
                        codebarras.Text = _mItem?.CodeBarras ?? "";
                        referencia.Text = _mItem?.Referencia ?? "";
                        valorcompra.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                        estoqueatual.Text = GridLista.SelectedRows[0].Cells[6].Value.ToString();
                        valorvenda.Text = Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorVenda));
                        valorCompraAtual.Text =
                            Validation.FormatPrice(Validation.ConvertToDouble(_mItem.ValorCompra), true);
                        estoqueProduto.Text = _mItem.EstoqueAtual.ToString();
                        novoEstoque.Text =
                            (Validation.ConvertToDouble(estoqueatual.Text) +
                             Validation.ConvertToDouble(estoqueProduto.Text)).ToString();

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
        ///     Janela para selecionar itens não encontrados.
        /// </summary>
        private void ModalItens()
        {
            if (collection.Lookup(BuscarProduto.Text) == 0)
                if (Application.OpenForms["PedidoModalItens"] as PedidoModalItens == null)
                {
                    PedidoModalItens.txtSearch = BuscarProduto.Text;
                    var form = new PedidoModalItens {TopMost = true};
                    if (form.ShowDialog() == DialogResult.OK) BuscarProduto.Text = PedidoModalItens.NomeProduto;
                }
        }

        /// <summary>
        ///     Salva os dados na datagrid
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
                GridLista.SelectedRows[0].Cells["CATEGORIAID"].Value = (int) Categorias.SelectedValue;

            GridLista.SelectedRows[0].Cells["EDITADO"].Value = "1";
            GridLista.SelectedRows[0].Cells["IDVINCULO"].Value = IDPDT.Text;

            if (!string.IsNullOrEmpty(IDPDT.Text))
                GridLista.SelectedRows[0].Cells["Cadastrado"].Value = new Bitmap(Resources.success16x);

            GridLista.Enabled = true;
        }

        private dynamic FindItem(string codeBarras, string nome)
        {
            var findItem = _mItem.Query().Select("*")
                .Where(q => q.Where("nome", nome).OrWhere("codebarras", codeBarras)).Where("excluir", 0)
                .FirstOrDefault();
            if (findItem != null)
                return findItem;

            return null;
        }

        private void Eventos()
        {
            workerBackground.DoWork += (s, e) => GridLista.Invoke((MethodInvoker) LoadProdutos);

            workerBackground.RunWorkerCompleted += (s, e) =>
            {
                label2.Visible = false;
                pictureBox2.Visible = false;
                GridLista.Visible = true;
            };

            Shown += (s, e) =>
            {
                Refresh();
                AutoCompleteItens();
                pictureBox2.Visible = true;

                var cat = new Categoria().FindAll().Where("tipo", "Produtos").WhereFalse("excluir").OrderByDesc("nome").Get();
                if (cat.Any())
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
                    label9.Text = @"Edite o produto selecionado abaixo";
                }


                pictureBox2.Visible = true;
                GridLista.Visible = false;
                workerBackground.RunWorkerAsync();
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
                    GridLista.SelectedRows[0].Cells["Importar"].Value = (bool) GridLista.SelectedRows[0].Cells["Importar"].Value == false;
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Importar" ||
                    GridLista.Columns[e.ColumnIndex].Name == "Editar")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Importar" ||
                    GridLista.Columns[e.ColumnIndex].Name == "Editar")
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
                    label9.Text = @"Vincular a produtos existentes";
                    panelVinculacao.Location = new Point(34, 517);
                }

                panelVinculacao.Visible = false;
            };

            btnImportar.Click += (s, e) =>
            {
                produtos.Clear();

                var i = -1;
                foreach (DataGridViewRow item in GridLista.Rows)
                {
                    i++;
                    if ((bool) item.Cells["Importar"].Value)
                    {
                        var id = Validation.ConvertToInt32(item.Cells["IDVINCULO"].Value);
                        double estoque = 0;
                        var codeBarras = item.Cells["Cód. de Barras"].Value.ToString();
                        var id_sync = 0;

                        if (!string.IsNullOrEmpty(codeBarras))
                        {
                            var _mItem = new Item();
                            _mItem = _mItem.Query().Select("*").Where("codebarras", codeBarras).Where("excluir", 0)
                                .FirstOrDefault<Item>();
                            if (_mItem != null)
                            {
                                id = _mItem.Id;
                                estoque = _mItem.EstoqueAtual;
                                id_sync = _mItem.id_sync;
                            }
                        }

                        estoque = Validation.ConvertToDouble(item.Cells[6].Value) + estoque;

                        switch (ImportarNfe.optionSelected)
                        {
                            case 1:
                                estoque = 0;
                                break;
                            case 3:
                                estoque = Validation.ConvertToDouble(item.Cells[6].Value);
                                break;
                        }

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
                            Fornecedor = item.Cells["Fornecedor"].Value.ToString(),
                            NCM = item.Cells["NCM"].Value.ToString(),
                            idSync = id_sync
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
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);
            };

            valorvenda.TextChanged += (s, e) =>
            {
                var txt = (TextBox) s;
                Masks.MaskPrice(ref txt);

                Validation.WarningInput(txt, warning);
            };

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridLista.Rows)
                    if (btnMarcarCheckBox.Text == @"Marcar Todos")
                    {
                        if ((bool) item.Cells["Importar"].Value == false) item.Cells["Importar"].Value = true;
                    }
                    else
                    {
                        item.Cells["Importar"].Value = false;
                    }

                btnMarcarCheckBox.Text = btnMarcarCheckBox.Text == @"Marcar Todos" ? @"Desmarcar Todos" : @"Marcar Todos";
            };

            BuscarProduto.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter)
                    return;

                if (!string.IsNullOrEmpty(BuscarProduto.Text))
                {
                    var item = _mItem.FindById(collection.Lookup(BuscarProduto.Text)).FirstOrDefault<Item>();
                    if (item != null)
                        BuscarProduto.Text = item.Nome;

                    ModalItens();

                    return;
                }

                if (string.IsNullOrEmpty(BuscarProduto.Text))
                    ModalItens();
                else
                    VincularProduto();
            };

            Back.Click += (s, e) => Close();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata.Execution;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarProdutos : Form
    {
        private ImportarNfe dataNfe = new ImportarNfe();
        private Model.Item _mItem = new Model.Item();
        public static ArrayList produtos = new ArrayList();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

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
                    foreach (dynamic pdt in item.GetProdutos())
                    {
                        allProdutos.Insert(0, pdt);
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
            Table.ColumnCount = 11;

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

            Table.Columns[6].Name = "Qtd.";
            Table.Columns[6].Width = 60;

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

            Table.Rows.Clear();

            foreach (dynamic item in dataProdutos)
            {
                dynamic findItem = null;
                if (FindItem(item.CodeBarras) != null)
                {
                    findItem = FindItem(item.CodeBarras);
                }

                Table.Rows.Add(
                    true,
                    findItem != null ? new Bitmap(Properties.Resources.success16x) : new Bitmap(Properties.Resources.error16x),
                    item.Referencia,
                    item.CodeBarras,
                    item.Descricao,
                    item.Medida,
                    FormatPriceXml(item.Quantidade),
                    FormatPriceXml(item.VlrCompra),
                    findItem != null ? Validation.FormatPrice(Validation.ConvertToDouble(findItem.VALORVENDA)) : "00,00",
                    new Bitmap(Properties.Resources.edit16x),
                    findItem != null ? findItem.ID : 0,
                    findItem != null ? findItem.CATEGORIAID : "",
                    "0",
                    "0"
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
                _mItem = new Model.Item();
                
                panelVinculacao.Visible = true;
                panelVinculacao.Location = new Point(34, 453);
                label10.Visible = false;
                pictureBox4.Visible = false;
                BuscarProduto.Visible = false;
                btnVincular.Visible = false;
                label9.Text = "Edite o produto selecionado abaixo";

                string id = GridLista.SelectedRows[0].Cells["ID"].Value.ToString();
                string editado = GridLista.SelectedRows[0].Cells["EDITADO"].Value.ToString();
                
                // TRÁS ALGUNS DADOS DO BANCO CASO EXISTA ALGUM REGISTRO, SE NÃO, PEGA OS DADOS DO DATAGRID "PARA EDITAR"
                _mItem = _mItem.Query().Select("*").Where("ID", id).WhereFalse("excluir").FirstOrDefault<Model.Item>();
                if (_mItem != null && editado == "0")
                {
                    nome.Text = _mItem?.Nome ?? "";
                    codebarras.Text = _mItem?.CodeBarras ?? "";
                    referencia.Text = _mItem?.Referencia ?? "";
                    valorcompra.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                    estoqueatual.Text = GridLista.SelectedRows[0].Cells["Qtd."].Value.ToString();
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
                    estoqueatual.Text = GridLista.SelectedRows[0].Cells["Qtd."].Value.ToString();
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

                    _mItem = _mItem.Query().Select("*").Where("ID", id).WhereFalse("excluir").FirstOrDefault<Model.Item>();
                    if (_mItem != null && editado == "0")
                    {
                        IDPDT.Text = _mItem?.Id.ToString() ?? "0";
                        nome.Text = _mItem?.Nome ?? "";
                        codebarras.Text = _mItem?.CodeBarras ?? "";
                        referencia.Text = _mItem?.Referencia ?? "";
                        valorcompra.Text = GridLista.SelectedRows[0].Cells["Vlr. Compra"].Value.ToString();
                        estoqueatual.Text = GridLista.SelectedRows[0].Cells["Qtd."].Value.ToString();
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
                GridLista.SelectedRows[0].Cells["Qtd."].Value = 0;

            if (ImportarNfe.optionSelected == 2)
                GridLista.SelectedRows[0].Cells["Qtd."].Value = estoqueatual.Text;

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
            var findItem = _mItem.Query().Select("*").Where("codebarras", codeBarras).WhereFalse("excluir").FirstOrDefault();
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

                Medidas.DataSource = new List<String> { "UN", "KG", "PC", "MÇ", "BD", "DZ", "GR", "L", "ML", "M", "M2", "ROLO", "CJ", "SC", "CX", "FD", "PAR", "PR", "KIT", "CNT", "PCT" };
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

                label10.Visible = true;
                pictureBox4.Visible = true;
                BuscarProduto.Visible = true;
                btnVincular.Visible = true;
                label9.Text = "Vincular a produtos existentes";
                panelVinculacao.Location = new Point(34, 517);
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
                            _mItem = _mItem.Query().Select("*").Where("codebarras", codeBarras).WhereFalse("excluir").FirstOrDefault<Model.Item>();
                            if (_mItem != null)
                            {
                                id = _mItem.Id;
                                estoque = _mItem.EstoqueAtual;
                            }
                            else
                                _mItem = new Model.Item();
                        }

                        if (id != 0)
                            _mItem = _mItem.Query().Select("*").Where("ID", id).WhereFalse("excluir").FirstOrDefault<Model.Item>();
                            
                        //_mItem.Id = id;
                        //_mItem.Referencia = item.Cells["Referência"].Value.ToString();
                        //_mItem.CodeBarras = item.Cells["Cód. de Barras"].Value.ToString();
                        //_mItem.Nome = item.Cells["Descrição"].Value.ToString();
                        //_mItem.Medida = item.Cells["Medida"].Value.ToString();
                        //_mItem.EstoqueAtual = Validation.ConvertToDouble(item.Cells["Qtd."].Value) + estoque;
                        //_mItem.Categoriaid = Validation.ConvertToInt32(item.Cells["CATEGORIAID"].Value);
                        //_mItem.ValorCompra = Validation.ConvertToDouble(item.Cells["Vlr. Compra"].Value);
                        //_mItem.ValorVenda = Validation.ConvertToDouble(item.Cells["Vlr. Venda"].Value);

                        //if (_mItem.Save(_mItem, false))
                        //    success = true;
                        //else
                        //    success = false;

                        produtos.Add(new
                        {
                            Ordem = i,
                            Id = id,
                            Referencia = item.Cells["Referência"].Value.ToString(),
                            CodeBarras = item.Cells["Cód. de Barras"].Value.ToString(),
                            Nome = item.Cells["Descrição"].Value.ToString(),
                            Medida = item.Cells["Medida"].Value.ToString(),
                            Estoque = Validation.ConvertToDouble(item.Cells["Qtd."].Value) + estoque,
                            CategoriaId = Validation.ConvertToInt32(item.Cells["CATEGORIAID"].Value),
                            ValorCompra = Validation.ConvertToDouble(item.Cells["Vlr. Compra"].Value),
                            ValorVenda = Validation.ConvertToDouble(item.Cells["Vlr. Venda"].Value)
                        });
                    }
                }

                OpenForm.Show<TelasImportarNfe.ImportarProdutosConcluido>(this);

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

                WarningInput(txt, warning);
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
        
        public static string FormatPriceXml(string value)
        {
            string p1 = "", p2 = ",00";

            p1 = value.Substring(0, value.IndexOf('.'));

            if (value.Substring(value.IndexOf('.'), (value.Length - value.IndexOf('.'))).Length >= 3)
                p2 = value.Substring(value.IndexOf('.'), 3).Replace(".", ",");
            
            return Validation.FormatPrice(Validation.ConvertToDouble(p1 + p2));
        }

        private void WarningInput(TextBox textbox, PictureBox img)
        {
            if (String.IsNullOrEmpty(textbox.Text) || textbox.Text == "0,00")
            {
                img.Image = Properties.Resources.warning16x;
            }
            else
            {
                img.Image = Properties.Resources.success16x;
            }
        }
    }
}

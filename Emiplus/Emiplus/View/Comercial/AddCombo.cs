using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Food;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class AddCombo : Form
    {
        private readonly Categoria _mCategoria = new Categoria();
        private Item _mItem = new Item();
        private readonly ItemCombo _mItemCombo = new ItemCombo();

        /// <summary>
        ///     Armazena todos ids dos combos, categorias e produtos
        /// </summary>
        private readonly ArrayList listProdutos = new ArrayList();

        /// <summary>
        ///     Armazena os ids dos itens selecionados
        /// </summary>
        private readonly List<int> listProdutosSelecionados = new List<int>();

        public AddCombo()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        ///     Recupera o ID do produto, para encontrar os combos
        /// </summary>
        public static int IdProduto { get; set; }

        /// <summary>
        ///     Adiciona as colunas na tabela dos itens
        /// </summary>
        /// <param name="table"></param>
        private void SetHeadersTableItens(DataGridView table)
        {
            table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            table.RowTemplate.Height = 50;
            table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Incluir",
                Name = "Incluir",
                FlatStyle = FlatStyle.Standard,
                CellTemplate = new DataGridViewCheckBoxCell(),
                Width = 60
            };
            table.Columns.Insert(0, checkColumn);

            var photo = new DataGridViewImageColumn
            {
                Image = Resources.sem_imagem,
                HeaderText = @"Imagem",
                Name = "Photo",
                Width = 60,
                DefaultCellStyle = {Alignment = DataGridViewContentAlignment.MiddleCenter},
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            table.Columns.Insert(1, photo);

            table.Columns[2].Name = "ID";
            table.Columns[2].Visible = false;

            table.Columns[3].Name = "Item";
            table.Columns[3].Width = 150;
            table.Columns[3].Visible = true;

            table.Columns[4].Name = "Valor";
            table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[4].Width = 100;
            table.Columns[4].Visible = true;

            table.Columns[5].Name = "Estoque Atual";
            table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[5].Width = 120;
            table.Columns[5].Visible = true;

            table.Columns[6].Name = "AddonSelected";
            table.Columns[6].Width = 100;
            table.Columns[6].Visible = false;

            table.Columns[7].Name = "Unitario";
            table.Columns[7].Width = 100;
            table.Columns[7].Visible = false;

            var imgDividir = new DataGridViewImageColumn
            {
                Image = Resources.menu20x,
                Name = "Adicional",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            table.Columns.Add(imgDividir);

            table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        ///     Adiciona as colunas na tabela para exibir os itens da categoria
        /// </summary>
        /// <param name="table"></param>
        private void SetHeadersTableItensCategoria(DataGridView table)
        {
            table.ColumnCount = 4;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            table.RowTemplate.Height = 50;
            table.RowHeadersVisible = false;

            var photo = new DataGridViewImageColumn
            {
                Image = Resources.sem_imagem,
                HeaderText = @"Imagem",
                Name = "Photo",
                Width = 60,
                DefaultCellStyle = {Alignment = DataGridViewContentAlignment.MiddleCenter},
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            table.Columns.Insert(0, photo);

            table.Columns[1].Name = "ID";
            table.Columns[1].Visible = false;

            table.Columns[2].Name = "Item";
            table.Columns[2].Width = 150;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;

            table.Columns[4].Name = "Estoque Atual";
            table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[4].Width = 120;
            table.Columns[4].Visible = true;

            var img = new DataGridViewImageColumn
            {
                Image = Resources.plus20x,
                Name = "Incluir",
                Width = 60,
                DefaultCellStyle = {Alignment = DataGridViewContentAlignment.MiddleCenter}
            };
            table.Columns.Add(img);

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        ///     Carrega os itens na tabela
        /// </summary>
        /// <param name="table"></param>
        private void LoadDataTableItens()
        {
            if (listProdutos.Count == listProdutosSelecionados.Count)
            {
                label1.Visible = false;
                panel4.Visible = false;
                panel3.Height = 345;

                return;
            }

            //var countCat = listProdutos.Cast<dynamic>().Count(itens => itens.Tipo == "Categoria");
            //if (countCat > 1)
            //    btnProximo.Visible = true;

            foreach (dynamic itens in listProdutos)
            {
                int idItem = Validation.ConvertToInt32(itens.Id);

                if (listProdutosSelecionados.Contains(idItem)) continue;

                if (itens.Tipo == "Produto")
                {
                    var dataItem = _mItem.FindById(idItem).FirstOrDefault<Item>();
                    if (dataItem != null)
                    {
                        Image photo = null;
                        if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{dataItem.Image}"))
                        {
                            var imageAsByteArray = File.ReadAllBytes($@"{Program.PATH_IMAGE}\Imagens\{dataItem.Image}");
                            photo = Support.ByteArrayToImage(imageAsByteArray);
                        }

                        GridListaItens.Rows.Add(
                            true,
                            photo,
                            dataItem.Id,
                            dataItem.Nome,
                            Validation.FormatPrice(dataItem.ValorVenda, true),
                            Validation.FormatMedidas(dataItem.Medida, Validation.ConvertToDouble(dataItem.EstoqueAtual)),
                            "",
                            Validation.FormatPrice(dataItem.ValorVenda, true),
                            Resources.plus20x
                        );
                    }
                }

                if (itens.Tipo == "Categoria")
                {
                    label1.Visible = true;
                    panel4.Visible = true;
                    panel3.Height = 128;

                    var dataCat = _mCategoria.FindById(idItem).FirstOrDefault<Categoria>();
                    if (dataCat != null)
                        label1.Text = $@"Itens da categoria: {dataCat.Nome}";

                    var dataItens = _mItem.FindAll(new[]
                            {"id", "excluir", "nome", "valorvenda", "estoqueatual", "categoriaid"})
                        .WhereFalse("excluir").Where("tipo", "Produtos")
                        .Where("categoriaid", idItem)
                        .Get<Item>();

                    foreach (var data in dataItens)
                    {
                        Image photo = null;
                        if (File.Exists($@"{Program.PATH_IMAGE}\Imagens\{data.Image}"))
                        {
                            var imageAsByteArray = File.ReadAllBytes($@"{Program.PATH_IMAGE}\Imagens\{data.Image}");
                            photo = Support.ByteArrayToImage(imageAsByteArray);
                        }

                        GridListaSelectItens.Rows.Add(
                            photo,
                            data.Id,
                            data.Nome,
                            Validation.FormatPrice(data.ValorVenda, true),
                            Validation.FormatMedidas(data.Medida, Validation.ConvertToDouble(data.EstoqueAtual))
                        );
                    }

                    listProdutosSelecionados.Add(idItem);
                    break;
                }

                listProdutosSelecionados.Add(idItem);
            }
        }

        /// <summary>
        ///     Função utilizada para separar ids dos combos
        /// </summary>
        private void SepareIds()
        {
            var dataCombo = _mItemCombo.FindById(Validation.ConvertToInt32(Combos.SelectedValue.ToString()))
                .FirstOrDefault<ItemCombo>();
            if (dataCombo == null)
                return;

            txtComboValor.Text = $@"Valor do Combo: {Validation.FormatPrice(dataCombo.ValorVenda, true)}";

            var itens = dataCombo.Produtos.Split('|');
            if (!itens.Any())
                return;

            listProdutos.Add(new {Tipo = "Produto", Id = $"{IdProduto}"});
            foreach (var item in itens)
            {
                if (item.Contains("P:"))
                    listProdutos.Add(new {Tipo = "Produto", Id = $"{item.Replace("P:", "")}"});

                if (item.Contains("C:"))
                    listProdutos.Add(new {Tipo = "Categoria", Id = $"{item.Replace("C:", "")}"});
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

        /// <summary>
        ///     Manipula todos os eventos do form
        /// </summary>
        private void Eventos()
        {
            Shown += (s, e) =>
            {
                KeyDown += KeyDowns;
                KeyPreview = true;

                if (IdProduto > 0)
                    _mItem = _mItem.FindById(IdProduto).FirstOrDefault<Item>();
                else
                    return;

                Combos.DataSource = _mItemCombo.GetCombos(_mItem.Combos);
                Combos.DisplayMember = "Nome";
                Combos.ValueMember = "Id";

                SetHeadersTableItens(GridListaItens);
                SetHeadersTableItensCategoria(GridListaSelectItens);
            };

            btnCombo.Click += (s, e) =>
            {
                if (Combos.SelectedValue.ToString() == "0")
                {
                    Alert.Message("Opps", "Selecione um combo válido.", Alert.AlertType.error);
                    return;
                }

                listProdutos.Clear();
                listProdutosSelecionados.Clear();
                GridListaItens.Rows.Clear();
                GridListaSelectItens.Rows.Clear();
                SepareIds();
                LoadDataTableItens();
            };

            btnInserir.Click += (s, e) => { };

            btnContinuar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Ignore;
                Close();
            };

            GridListaSelectItens.CellClick += (s, e) =>
            {
                if (GridListaSelectItens.Columns[e.ColumnIndex].Name == "Incluir")
                {
                    GridListaItens.Rows.Add(
                        true,
                        GridListaSelectItens.SelectedRows[0].Cells["Photo"].Value,
                        GridListaSelectItens.SelectedRows[0].Cells["ID"].Value,
                        GridListaSelectItens.SelectedRows[0].Cells["Item"].Value,
                        GridListaSelectItens.SelectedRows[0].Cells["Valor"].Value,
                        GridListaSelectItens.SelectedRows[0].Cells["Estoque Atual"].Value,
                        "",
                        GridListaSelectItens.SelectedRows[0].Cells["Valor"].Value,
                        Resources.plus20x
                    );

                    GridListaSelectItens.Rows.Clear();
                    LoadDataTableItens();

                    Alert.Message("Pronto", "Item adicionado.", Alert.AlertType.success);
                }
            };

            GridListaSelectItens.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridListaSelectItens.Columns[e.ColumnIndex].Name == "Incluir")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridListaSelectItens.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridListaSelectItens.Columns[e.ColumnIndex].Name == "Incluir")
                    dataGridView.Cursor = Cursors.Default;
            };

            GridListaItens.CellClick += (s, e) =>
            {
                if (GridListaItens.Columns[e.ColumnIndex].Name == "Incluir")
                {
                    GridListaItens.SelectedRows[0].Cells["Incluir"].Value = (bool)GridListaItens.SelectedRows[0].Cells["Incluir"].Value == false;
                }

                if (GridListaItens.Columns[e.ColumnIndex].Name == "Adicional")
                {
                    AdicionaisDispon.ValorAddon = 0;
                    AdicionaisDispon.AddonSelected = GridListaItens.SelectedRows[0].Cells["AddonSelected"].Value != null
                        ? GridListaItens.SelectedRows[0].Cells["AddonSelected"].Value.ToString()
                        : "";
                    AdicionaisDispon.IdPedidoItem = 0;
                    AdicionaisDispon.IdItem = Validation.ConvertToInt32(GridListaItens.SelectedRows[0].Cells["ID"].Value);
                    var form = new AdicionaisDispon {TopMost = true};
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var getValor = Validation.ConvertToDouble(GridListaItens.SelectedRows[0].Cells["Unitario"].Value
                            .ToString().Replace("R$ ", ""));
                        GridListaItens.SelectedRows[0].Cells["Valor"].Value = Validation.FormatPrice(getValor + AdicionaisDispon.ValorAddon);
                        GridListaItens.SelectedRows[0].Cells["AddonSelected"].Value = AdicionaisDispon.AddonSelected;
                    }
                }
            };

            GridListaItens.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridListaItens.Columns[e.ColumnIndex].Name == "Incluir" ||
                    GridListaItens.Columns[e.ColumnIndex].Name == "Adicional")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridListaItens.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridListaItens.Columns[e.ColumnIndex].Name == "Incluir" ||
                    GridListaItens.Columns[e.ColumnIndex].Name == "Adicional")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}
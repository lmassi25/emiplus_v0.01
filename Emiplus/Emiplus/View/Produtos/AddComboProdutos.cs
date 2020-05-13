using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class AddComboProdutos : Form
    {
        /// <summary>
        /// Model - Item Combo
        /// </summary>
        private ItemCombo _mItemCombo = new ItemCombo();

        /// <summary>
        /// Lista com todas as categorias, usado para o autocomplete
        /// </summary>
        private KeyedAutoCompleteStringCollection _listCategorias = new KeyedAutoCompleteStringCollection();

        /// <summary>
        /// Lista com todas as produtos, usado para o autocomplete
        /// </summary>
        private KeyedAutoCompleteStringCollection _listProdutos = new KeyedAutoCompleteStringCollection();

        /// <summary>
        /// Armazena os ID da grid para remoção das linhas selecionadas
        /// </summary>
        public List<int> ListItens = new List<int>();

        public AddComboProdutos()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        ///     ID para manipular combo no banco de dados
        /// </summary>
        public static int IdCombo { get; set; }

        /// <summary>
        /// Preenche os header da tabela para visualização dos itens adicionados
        /// </summary>
        /// <param name="table"></param>
        private static void SetHeadersItens(DataGridView table)
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
            
            table.Columns[2].Name = "Item";
            table.Columns[2].Width = 120;
            table.Columns[2].MinimumWidth = 120;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// Auto complete dos produtos
        /// </summary>
        private void AutoCompleteItens()
        {
            _listProdutos = new Item().AutoComplete("Produtos");
            txtAutoComplete.AutoCompleteCustomSource = _listProdutos;
        }
        
        /// <summary>
        /// Auto complete das categorias
        /// </summary>
        private void AutoCompleteCategorias()
        {
            _listCategorias = new Categoria().AutoComplete("Produtos");
            txtAutoComplete.AutoCompleteCustomSource = _listCategorias;
        }

        /// <summary>
        ///     Func responsavel por carregar todos os dados do Combo no form
        /// </summary>
        private void LoadData()
        {
            _mItemCombo = _mItemCombo.FindById(IdCombo).FirstOrDefault<ItemCombo>();
            if (_mItemCombo != null)
            {
                nome.Text = _mItemCombo?.Nome ?? "";
                valorvenda.Text = Validation.Price((double) _mItemCombo?.ValorVenda);

                if (string.IsNullOrEmpty(_mItemCombo.Produtos))
                    return;

                var itens = _mItemCombo?.Produtos.Split('|');
                foreach (var id in itens)
                {
                    if (id.Contains("P:"))
                    {
                        var item = new Item().FindById(Validation.ConvertToInt32(id.Replace("P:", ""))).FirstOrDefault<Item>();

                        dataGridItens.Rows.Add(
                            false,
                            id,
                            item.Nome,
                            Validation.FormatPrice(item.ValorVenda, true)
                        );
                    }

                    if (id.Contains("C:"))
                    {
                        var categoria = new Categoria().FindById(Validation.ConvertToInt32(id.Replace("C:", ""))).FirstOrDefault<Categoria>();

                        dataGridItens.Rows.Add(
                            false,
                            id,
                            categoria.Nome,
                            ""
                        );
                    }
                }
            }
        }

        /// <summary>
        /// Adiciona os itens selecionados a tabela
        /// </summary>
        private void AddItensTable()
        {
            switch (Tipo.SelectedItem.ToString())
            {
                case "Produtos" when _listProdutos.Lookup(txtAutoComplete.Text) != 0:
                    var item = new Item().FindById(_listProdutos.Lookup(txtAutoComplete.Text)).FirstOrDefault<Item>();

                    dataGridItens.Rows.Add(
                        false,
                        $"p:{item.Id}",
                        item.Nome,
                        Validation.FormatPrice(item.ValorVenda, true)
                    );

                    break;
                case "Categorias" when _listCategorias.Lookup(txtAutoComplete.Text) != 0:
                    var categoria = new Categoria().FindById(_listCategorias.Lookup(txtAutoComplete.Text)).FirstOrDefault<Categoria>();

                    dataGridItens.Rows.Add(
                        false,
                        $"c:{categoria.Id}",
                        categoria.Nome,
                        ""
                    );

                    break;
            }

            txtAutoComplete.Text = "";
            txtAutoComplete.Select();
        }

        /// <summary>
        /// Salva os itens adicionados a tabela no banco de dados
        /// </summary>
        private void SaveItensTable()
        {
            if (dataGridItens.Rows.Count < 0)
            {
                Alert.Message("Opps", "Selecione pelo menos 1 item para salvar o combo.", Alert.AlertType.error);
                return;
            }

            var ids = dataGridItens.Rows.Cast<DataGridViewRow>().Aggregate("", (current, item) => current + $"|{item.Cells["ID"].Value}");

            _mItemCombo.Nome = nome.Text;
            _mItemCombo.Produtos = ids.Substring(1);
            _mItemCombo.ValorVenda = Validation.ConvertToDouble(valorvenda.Text);
            if (!_mItemCombo.Save(_mItemCombo))
                return;

            Close();
        }

        /// <summary>
        /// Function com atalhos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    AddItensTable();
                    break;
            }
        }

        /// <summary>
        /// Manipula todos os eventos do form
        /// </summary>
        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                ToolHelp.Show(
                    "Você pode selecionar um produto ou então selecionar uma categoria inteira de produtos.",
                    pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");

                Tipo.SelectedIndex = 0;
                SetHeadersItens(dataGridItens);

                if (IdCombo > 0)
                {
                    LoadData();
                }
                else
                {
                    _mItemCombo = new ItemCombo {Id = 0};
                    _mItemCombo.Save(_mItemCombo);
                }
            };

            valorvenda.TextChanged += (s, e) =>
            {
                var txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            btnContinue.Click += (s, e) =>
            {
                switch (Tipo.SelectedItem.ToString())
                {
                    case "Produtos":
                        AutoCompleteItens();
                        break;
                    case "Categorias":
                        AutoCompleteCategorias();
                        break;
                }

                txtAutoComplete.Enabled = true;
                btnIncluir.Visible = true;
            };

            btnIncluir.Click += (s, e) => AddItensTable();
            btnSalvar.Click += (s, e) => SaveItensTable();

            btnExit.Click += (s, e) => Close();

            btnRemoverSelecionados.Click += (s, e) =>
            {
                var toBeDeleted = new List<DataGridViewRow>();
                toBeDeleted.Clear();

                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a remover os ITENS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (DataGridViewRow item in dataGridItens.Rows)
                    {
                        Console.WriteLine(item.Cells["Selecione"].Value);
                        if ((bool)item.Cells["Selecione"].Value) toBeDeleted.Add(item);
                    }

                    toBeDeleted.ForEach(d => dataGridItens.Rows.Remove(d));
                }

                btnRemoverSelecionados.Visible = false;
            };

            dataGridItens.CellContentClick += (s, e) =>
            {
                if (dataGridItens.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)dataGridItens.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        dataGridItens.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemoverSelecionados.Visible = true;
                    }
                    else
                    {
                        dataGridItens.SelectedRows[0].Cells["Selecione"].Value = false;

                        var hideBtns = false;
                        foreach (DataGridViewRow item in dataGridItens.Rows)
                            if ((bool)item.Cells["Selecione"].Value)
                                hideBtns = true;

                        btnRemoverSelecionados.Visible = hideBtns;
                    }
                }
            };

            dataGridItens.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (dataGridItens.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            dataGridItens.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (dataGridItens.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}
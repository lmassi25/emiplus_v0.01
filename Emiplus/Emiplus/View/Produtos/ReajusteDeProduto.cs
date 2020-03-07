using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class ReajusteDeProduto : Form
    {
        private Model.Item _mItem = new Model.Item();
        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public ReajusteDeProduto()
        {
            InitializeComponent();
            Eventos();
        }

        private async Task DataTableAsync() => await SetTable(GridLista);

        /// <summary>
        /// Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                if (!String.IsNullOrEmpty(itens.NOME))
                    collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private void AutoCompleteFornecedorCategorias()
        {
            var cats = new ArrayList();
            cats.Add(new { Id = "0", Nome = "Todas" });

            var cat = new Model.Categoria().FindAll().WhereFalse("excluir").Where("tipo", "=", "Produtos").OrderByDesc("nome").Get();
            foreach (var item in cat)
            {
                cats.Add(new { Id = $"{item.ID}", Nome = $"{item.NOME}" });
            }

            Categorias.DataSource = cats;
            Categorias.DisplayMember = "Nome";
            Categorias.ValueMember = "Id";

            var fornecedores = new ArrayList();
            fornecedores.Add(new { Id = "0", Nome = "Todos" });

            var fornecedor = new Model.Pessoa().FindAll().Where("tipo", "Fornecedores").WhereFalse("excluir").OrderByDesc("nome").Get();
            foreach (var item in fornecedor)
            {
                fornecedores.Add(new { Id = $"{item.ID}", Nome = $"{item.NOME}" });
            }

            Fornecedor.DataSource = fornecedores;
            Fornecedor.DisplayMember = "Nome";
            Fornecedor.ValueMember = "Id";
        }

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new Model.Item().Query();

            if (Validation.ConvertToInt32(Categorias.SelectedValue) >= 1)
            {
                model.Where("ITEM.CATEGORIAID", Validation.ConvertToInt32(Categorias.SelectedValue));
            }

            if (Validation.ConvertToInt32(Fornecedor.SelectedValue) >= 1)
            {
                model.Where("ITEM.FORNECEDOR", Validation.ConvertToInt32(Fornecedor.SelectedValue));
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                model.Where("ITEM.id", collection.Lookup(BuscarProduto.Text));
            }

            model.LeftJoin("CATEGORIA", "CATEGORIA.id", "ITEM.CATEGORIAID");
            model.LeftJoin("PESSOA", "PESSOA.id", "ITEM.FORNECEDOR");
            model.Where("ITEM.excluir", 0);
            model.Where("ITEM.nome", "<>", "");
            model.SelectRaw("ITEM.id, ITEM.nome, ITEM.medida, ITEM.valorvenda, ITEM.estoqueatual, ITEM.CATEGORIAID, CATEGORIA.NOME as CAT_NAME, PESSOA.NOME as FORNECEDOR_NAME");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            var cats = new ArrayList();
            var cat = new Model.Categoria().FindAll().WhereFalse("excluir").Where("tipo", "=", "Produtos").OrderByDesc("nome").Get();
            foreach (var item in cat)
                cats.Add(new { Id = $"{item.ID}", Nome = $"{item.NOME}" });

            var fornecedores = new ArrayList();
            var fornecedor = new Pessoa().FindAll().Where("tipo", "Fornecedores").WhereFalse("excluir").OrderByDesc("nome").Get();
            foreach (var item in fornecedor)
                fornecedores.Add(new { Id = $"{item.ID}", Nome = $"{item.NOME}" });

            var Medidas = new List<String> { "UN", "KG", "PC", "MÇ", "BD", "DZ", "GR", "L", "ML", "M", "M2", "ROLO", "CJ", "SC", "CX", "FD", "PAR", "PR", "KIT", "CNT", "PCT" };

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);
                int n = Table.Rows.Add();

                DataGridViewComboBoxCell cellMedidas = new DataGridViewComboBoxCell();
                if (Medidas.Count > 0)
                {
                    cellMedidas.Style.NullValue = item.MEDIDA;
                    cellMedidas.DataSource = Medidas;
                }

                DataGridViewComboBoxCell cellCategorias = new DataGridViewComboBoxCell();
                if (cats.Count > 0)
                {
                    cellCategorias.ValueMember = "Id";
                    cellCategorias.DisplayMember = "Nome";
                    cellCategorias.Style.NullValue = item.CAT_NAME;
                    cellCategorias.DataSource = cats;
                }

                DataGridViewComboBoxCell cellFornecedores = new DataGridViewComboBoxCell();
                if (fornecedores.Count > 0)
                {
                    cellFornecedores.ValueMember = "Id";
                    cellFornecedores.DisplayMember = "Nome";
                    cellFornecedores.Style.NullValue = item.FORNECEDOR_NAME;
                    cellFornecedores.DataSource = fornecedores;
                }

                Table.Rows[n].Cells[0].Value = item.ID;
                Table.Rows[n].Cells[1].Value = item.NOME;
                Table.Rows[n].Cells[2] = cellMedidas;
                Table.Rows[n].Cells[3] = cellCategorias;
                Table.Rows[n].Cells[4] = cellFornecedores;
                Table.Rows[n].Cells[5].Value = item.VALORVENDA;
                Table.Rows[n].Cells[6].Value = item.ESTOQUEATUAL;
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

            Shown += async (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                await DataTableAsync();
                Refresh();
                AutoCompleteItens();
                AutoCompleteFornecedorCategorias();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSearch.Click += async (s, e) =>
            {
                await DataTableAsync();
            };

            GridLista.CellEndEdit += (s, e) =>
            {
                int ID = Validation.ConvertToInt32(GridLista.Rows[e.RowIndex].Cells["ID"].Value);
                string NOME = GridLista.Rows[e.RowIndex].Cells["Descricao"].Value != null ? GridLista.Rows[e.RowIndex].Cells["Descricao"].Value.ToString() : "";
                string MEDIDA = Convert.ToString((GridLista.Rows[0].Cells["Medida"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                int CATEGORIA = Validation.ConvertToInt32(GridLista.Rows[e.RowIndex].Cells["Categoria"].Value);
                int FORNECEDORES = Validation.ConvertToInt32(GridLista.Rows[e.RowIndex].Cells["Fornecedores"].Value);
                string VALORVENDA = GridLista.Rows[e.RowIndex].Cells["ValorVenda"].Value != null ? GridLista.Rows[e.RowIndex].Cells["ValorVenda"].Value.ToString() : "0";
                string ESTOQUEATUAL = GridLista.Rows[e.RowIndex].Cells["Estoque"].Value != null ? GridLista.Rows[e.RowIndex].Cells["Estoque"].Value.ToString() : "0";

                _mItem = _mItem.FindById(ID).FirstOrDefault<Model.Item>();
                _mItem.Id = ID;
                _mItem.Nome = NOME;
                _mItem.Medida = MEDIDA;

                if (CATEGORIA != 0)
                    _mItem.Categoriaid = CATEGORIA;

                if (FORNECEDORES != 0)
                    _mItem.Fornecedor = FORNECEDORES;

                _mItem.ValorVenda = Validation.ConvertToDouble(VALORVENDA);
                _mItem.EstoqueAtual = Validation.ConvertToDouble(ESTOQUEATUAL);

                if (_mItem.Save(_mItem, false))
                    Alert.Message("Pronto!", "Produto atualizado com sucesso.", Alert.AlertType.success);
                else
                    Alert.Message("Opsss!", "Algo deu errado ao atualizar o produto.", Alert.AlertType.error);
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }
    }
}
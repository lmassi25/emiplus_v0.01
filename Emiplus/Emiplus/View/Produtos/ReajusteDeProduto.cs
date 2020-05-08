using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class ReajusteDeProduto : Form
    {
        private Item _mItem = new Item();
        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public ReajusteDeProduto()
        {
            InitializeComponent();
            Eventos();
        }

        private async Task DataTableAsync()
        {
            await SetTable(GridLista);
        }

        /// <summary>
        ///     Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            collection = _mItem.AutoComplete("Produtos");
            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private void AutoCompleteFornecedorCategorias()
        {
            Categorias.DataSource = new Categoria().GetAll("Produtos");
            Categorias.DisplayMember = "Nome";
            Categorias.ValueMember = "Id";

            Fornecedor.DataSource = new Pessoa().GetAll("Fornecedores");
            Fornecedor.DisplayMember = "Nome";
            Fornecedor.ValueMember = "Id";
        }

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new Item().Query();

            if (Validation.ConvertToInt32(Categorias.SelectedValue) >= 1)
                model.Where("ITEM.CATEGORIAID", Validation.ConvertToInt32(Categorias.SelectedValue));

            if (Validation.ConvertToInt32(Fornecedor.SelectedValue) >= 1)
                model.Where("ITEM.FORNECEDOR", Validation.ConvertToInt32(Fornecedor.SelectedValue));

            if (collection.Lookup(BuscarProduto.Text) > 0)
                model.Where("ITEM.id", collection.Lookup(BuscarProduto.Text));

            model.LeftJoin("CATEGORIA", "CATEGORIA.id", "ITEM.CATEGORIAID");
            model.LeftJoin("PESSOA", "PESSOA.id", "ITEM.FORNECEDOR");
            model.Where("ITEM.excluir", 0);
            model.Where("ITEM.nome", "<>", "");
            model.Where("ITEM.tipo", "Produtos");
            model.SelectRaw(
                "ITEM.id, ITEM.nome, ITEM.medida, ITEM.valorvenda, ITEM.estoqueatual, ITEM.CATEGORIAID, CATEGORIA.NOME as CAT_NAME, PESSOA.NOME as FORNECEDOR_NAME");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView table, IEnumerable<dynamic> data = null)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            table.RowHeadersVisible = false;
            table.Rows.Clear();

            if (data == null)
            {
                var dados = await GetDataTable();
                data = dados;
            }

            for (var i = 0; i < data.Count(); i++)
            {
                var item = data.ElementAt(i);
                var n = table.Rows.Add();

                var cellMedidas = new DataGridViewComboBoxCell
                {
                    Style = {NullValue = item.MEDIDA},
                    DataSource = Support.GetMedidas()
                };

                var cellCategorias = new DataGridViewComboBoxCell
                {
                    ValueMember = "Id",
                    DisplayMember = "Nome",
                    Style = {NullValue = item.CAT_NAME},
                    DataSource = new Categoria().GetAll("Produtos")
                };

                var cellFornecedores = new DataGridViewComboBoxCell
                {
                    ValueMember = "Id",
                    DisplayMember = "Nome",
                    Style = {NullValue = item.FORNECEDOR_NAME},
                    DataSource = new Pessoa().GetAll("Fornecedores")
                };

                table.Rows[n].Cells[0].Value = item.ID;
                table.Rows[n].Cells[1].Value = item.NOME;
                table.Rows[n].Cells[2] = cellMedidas;
                table.Rows[n].Cells[3] = cellCategorias;
                table.Rows[n].Cells[4] = cellFornecedores;
                table.Rows[n].Cells[5].Value = item.VALORVENDA;
                table.Rows[n].Cells[6].Value = item.ESTOQUEATUAL;
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

            btnSearch.Click += async (s, e) => { await DataTableAsync(); };

            GridLista.CellEndEdit += (s, e) =>
            {
                var id = Validation.ConvertToInt32(GridLista.Rows[e.RowIndex].Cells["ID"].Value);
                var nome = GridLista.Rows[e.RowIndex].Cells["Descricao"].Value != null
                    ? GridLista.Rows[e.RowIndex].Cells["Descricao"].Value.ToString()
                    : "";
                var medida = Convert.ToString((GridLista.Rows[0].Cells["Medida"] as DataGridViewComboBoxCell).FormattedValue?.ToString());
                var categoria = Validation.ConvertToInt32(GridLista.Rows[e.RowIndex].Cells["Categoria"].Value);
                var fornecedores = Validation.ConvertToInt32(GridLista.Rows[e.RowIndex].Cells["Fornecedores"].Value);
                var valorvenda = GridLista.Rows[e.RowIndex].Cells["ValorVenda"].Value != null
                    ? GridLista.Rows[e.RowIndex].Cells["ValorVenda"].Value.ToString()
                    : "0";
                var estoqueatual = GridLista.Rows[e.RowIndex].Cells["Estoque"].Value != null
                    ? GridLista.Rows[e.RowIndex].Cells["Estoque"].Value.ToString()
                    : "0";

                _mItem = _mItem.FindById(id).FirstOrDefault<Item>();
                _mItem.Id = id;
                _mItem.Nome = nome;
                _mItem.Medida = medida;

                if (categoria != 0)
                    _mItem.Categoriaid = categoria;

                if (fornecedores != 0)
                    _mItem.Fornecedor = fornecedores;

                _mItem.ValorVenda = Validation.ConvertToDouble(valorvenda);
                _mItem.EstoqueAtual = Validation.ConvertToDouble(estoqueatual);

                if (_mItem.Save(_mItem, false))
                    Alert.Message("Pronto!", "Produto atualizado com sucesso.", Alert.AlertType.success);
                else
                    Alert.Message("Opsss!", "Algo deu errado ao atualizar o produto.", Alert.AlertType.error);
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }
    }
}
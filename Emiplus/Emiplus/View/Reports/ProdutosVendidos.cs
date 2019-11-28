using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Reports
{
    public partial class ProdutosVendidos : Form
    {
        private Model.Item _mItem = new Model.Item();
        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public ProdutosVendidos()
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
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }


        private void AutoCompleteFornecedorCategorias()
        {
            var cats = new ArrayList();
            cats.Add(new { Id = "0", Nome = "Todas" });

            var cat = new Model.Categoria().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
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

            if (!noFilterData.Checked)
            {
                model.Where("PEDIDO.emissao", ">=", Validation.ConvertDateToSql(dataInicial.Value, true))
                .Where("PEDIDO.emissao", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));
            }

            if (Validation.ConvertToInt32(Categorias.SelectedValue) >= 1)
            {
                model.Where("ITEM.CATEGORIAID", Validation.ConvertToInt32(Categorias.SelectedValue));
            }

            if (Validation.ConvertToInt32(Fornecedor.SelectedValue) >= 1)
            {
                model.Where("ITEM.FORNECEDOR", Validation.ConvertToInt32(Fornecedor.SelectedValue));
            }

            if (!filterAll.Checked)
            {
                if (maisVendidos.Checked)
                {
                    model.OrderByRaw("TotalVendas DESC");
                }

                if (menosVendidos.Checked)
                {
                    model.OrderByRaw("TotalVendas ASC");
                }
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                model.Where("ITEM.id", collection.Lookup(BuscarProduto.Text));
            }

            model.Where("PEDIDO.TIPO", Home.pedidoPage);
            model.Join("PEDIDO_ITEM", "PEDIDO_ITEM.item", "ITEM.id", "=", "inner join");
            model.Join("PEDIDO", "PEDIDO.id", "PEDIDO_ITEM.pedido", "=", "inner join");
            model.LeftJoin("CATEGORIA", "CATEGORIA.id", "ITEM.CATEGORIAID");
            model.LeftJoin("PESSOA", "PESSOA.id", "ITEM.FORNECEDOR");
            model.SelectRaw("ITEM.ID, ITEM.NOME, ITEM.REFERENCIA, ITEM.FORNECEDOR, ITEM.CATEGORIAID, ITEM.TIPO, ITEM.MEDIDA, PEDIDO_ITEM.item, SUM(PEDIDO_ITEM.QUANTIDADE) as TotalVendas, PESSOA.NOME AS FORNECEDOR_NAME, CATEGORIA.NOME AS CAT_NAME");
            model.GroupBy("item.id", "item.nome", "item.fornecedor", "item.categoriaid", "ITEM.TIPO", "ITEM.REFERENCIA", "ITEM.MEDIDA", "PEDIDO_ITEM.item", "PESSOA.NOME", "CATEGORIA.NOME");
            model.Limit(200);
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "Descrição";
            Table.Columns[0].Width = 150;

            Table.Columns[1].Name = "Qtd.";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Medida";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Fornecedor";
            Table.Columns[3].Width = 150;

            Table.Columns[4].Name = "Categoria";
            Table.Columns[4].Width = 150;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.NOME,
                    item.TOTALVENDAS,
                    item.MEDIDA,
                    item.FORNECEDOR_NAME,
                    item.CAT_NAME
                );
            }

            Table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

            Load += (s, e) => 
            {
                switch (Home.pedidoPage)
                {
                    case "Consignações":
                        maisVendidos.Text = "Mais Consignado";
                        menosVendidos.Text = "Menos Consignado";
                        label1.Text = "Produtos Consignados";
                        label2.Text = "Consulte os produtos consignados aqui.";
                        label3.Text = "Produtos Consignados";
                        break;
                    case "Devoluções":
                        maisVendidos.Text = "Mais Devolvido";
                        menosVendidos.Text = "Menos Devolvido";
                        label1.Text = "Produtos Devolvidos";
                        label2.Text = "Consulte os produtos devolvidos aqui.";
                        label3.Text = "Produtos Devolvidos";
                        break;
                }

                filterAll.Checked = true;
                AutoCompleteItens();
                AutoCompleteFornecedorCategorias();

                //dataInicial.Text = DateTime.Today.AddMonths(-1).ToString();
                dataInicial.Text = DateTime.Now.ToString();
                dataFinal.Text = DateTime.Now.ToString();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSearch.Click += async (s, e) =>
            {
                label13.Visible = false;
                await DataTableAsync();
            };

            imprimir.Click += async (s, e) => await RenderizarAsync();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }

        private async Task RenderizarAsync()
        {
            IEnumerable<dynamic> dados = await GetDataTable();

            ArrayList data = new ArrayList();
            foreach (var item in dados)
            {
                data.Add(new
                {
                    Nome = item.NOME,
                    Referencia = item.REFERENCIA,
                    QtdVendido = item.TOTALVENDAS,
                    Medida = item.MEDIDA,
                    Categoria = item.CAT_NAME,
                    Fornecedor = item.FORNECEDOR_NAME
                });
            }

            string tipo_aux = "";

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\View\Reports\html\ProdutosVendidos.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                noFilterData = noFilterData.Checked,
                dataInicial = dataInicial.Text,
                dataFinal = dataFinal.Text,
                Titulo = tipo_aux
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}

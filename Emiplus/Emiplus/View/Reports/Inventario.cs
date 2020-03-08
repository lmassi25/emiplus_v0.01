using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
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
    public partial class Inventario : Form
    {
        private Model.Item _mItem = new Model.Item();

        public Inventario()
        {
            InitializeComponent();
            Eventos();
        }

        private async Task DataTableAsync() => await SetTable(GridLista);

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
                fornecedores.Add(new { Id = $"{item.ID}", Nome = $"{item.NOME}" });

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

            model.LeftJoin("CATEGORIA", "CATEGORIA.id", "ITEM.CATEGORIAID");
            model.LeftJoin("PESSOA", "PESSOA.id", "ITEM.fornecedor");
            model.LeftJoin("ESTOQUE", "estoque.id_item", "ITEM.id");
            model.Select("ITEM.*", "PESSOA.NOME AS FORNECEDOR_NAME", "CATEGORIA.NOME AS CAT_NAME", "ESTOQUE.estoque");
            model.Where("ITEM.TIPO", "Produtos");
            model.Where("ITEM.EXCLUIR", 0);
            model.WhereRaw($"CAST(ESTOQUE.criado as DATE) = '{Validation.ConvertDateToSql(dataInicial.Text)}'");

            model.OrderBy("ITEM.NOME");

            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 7;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "Descrição";
            Table.Columns[0].Width = 200;
            Table.Columns[0].MinimumWidth = 200;

            Table.Columns[1].Name = "Valor Compra";
            Table.Columns[1].Width = 80;

            Table.Columns[2].Name = "Valor Venda";
            Table.Columns[2].Width = 80;

            Table.Columns[3].Name = "Estoque";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Medida";
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Categoria";
            Table.Columns[5].Width = 150;

            Table.Columns[6].Name = "Revendedor";
            Table.Columns[6].Width = 150;

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
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPPRA), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA), true),
                    dataInicial.Text != DateTime.Now.ToString("dd/MM/yyyy") ? Validation.FormatMedidas(item.MEDIDA, Validation.ConvertToDouble(item.ESTOQUE)) : Validation.FormatMedidas(item.MEDIDA, Validation.ConvertToDouble(item.ESTOQUEATUAL)),
                    item.MEDIDA,
                    item.CAT_NAME,
                    item.FORNECEDOR_NAME
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

            Shown += async (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                AutoCompleteFornecedorCategorias();

                dataInicial.Text = DateTime.Now.ToString();

                await DataTableAsync();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSearch.Click += async (s, e) =>
            {
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
                    ValorCompra = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPPRA), true),
                    ValorVenda = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA), true),
                    Estoque = item.ESTOQUEATUAL,
                    Medida = item.MEDIDA,
                    Categoria = item.CAT_NAME,
                    Fornecedor = item.FORNECEDOR_NAME
                });
            }

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Inventario.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
            }));

            Browser.htmlRender = render;
            using (var f = new Browser())
                f.ShowDialog();
        }
    }
}
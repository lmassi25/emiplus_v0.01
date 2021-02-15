using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotLiquid;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Reports
{
    public partial class ProdutosVendidos : Form
    {
        private readonly Item _mItem = new Item();
        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();
        
        public ProdutosVendidos()
        {
            InitializeComponent();
            Eventos();
        }

        private async Task DataTableAsync()
        {
            await SetTable(GridLista);
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

            model.Where("PEDIDO.excluir", "=", "0");
            model.Where("PEDIDO_ITEM.excluir", "=", "0");

            if (!noFilterData.Checked)
                model.Where("PEDIDO.emissao", ">=", Validation.ConvertDateToSql(dataInicial.Value, true))
                    .Where("PEDIDO.emissao", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));

            if (Validation.ConvertToInt32(Categorias.SelectedValue) >= 1)
                model.Where("ITEM.CATEGORIAID", Validation.ConvertToInt32(Categorias.SelectedValue));

            if (Validation.ConvertToInt32(Fornecedor.SelectedValue) >= 1)
                model.Where("ITEM.FORNECEDOR", Validation.ConvertToInt32(Fornecedor.SelectedValue));

            if (!filterAll.Checked)
            {
                if (maisVendidos.Checked) model.OrderByRaw("TotalVendas DESC");

                if (menosVendidos.Checked) model.OrderByRaw("TotalVendas ASC");
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
                model.Where("ITEM.id", collection.Lookup(BuscarProduto.Text));

            model.Where("PEDIDO.TIPO", Home.pedidoPage);
            model.Join("PEDIDO_ITEM", "PEDIDO_ITEM.item", "ITEM.id");
            model.Join("PEDIDO", "PEDIDO.id", "PEDIDO_ITEM.pedido");
            model.LeftJoin("CATEGORIA", "CATEGORIA.id", "ITEM.CATEGORIAID");
            model.LeftJoin("PESSOA", "PESSOA.id", "ITEM.FORNECEDOR");
            model.SelectRaw(
                "ITEM.ID, ITEM.NOME, ITEM.REFERENCIA, ITEM.FORNECEDOR, ITEM.CATEGORIAID, ITEM.TIPO, ITEM.MEDIDA, PEDIDO_ITEM.item, SUM(PEDIDO_ITEM.TOTALCOMPRA) as SumCompras, SUM(PEDIDO_ITEM.TOTALVENDA) as SumVendas, SUM(PEDIDO_ITEM.QUANTIDADE) as TotalVendas, PESSOA.NOME AS FORNECEDOR_NAME, CATEGORIA.NOME AS CAT_NAME");
            model.GroupBy("item.id", "item.nome", "item.fornecedor", "item.categoriaid", "ITEM.TIPO", "ITEM.REFERENCIA",
                "ITEM.MEDIDA", "PEDIDO_ITEM.item", "PESSOA.NOME", "CATEGORIA.NOME");
            
            //model.Limit(200);
            
            return model.GetAsync<dynamic>();
        }

        public Task<IEnumerable<dynamic>> GetDataTable2()
        {
            var model = new PedidoItem().Query();

            model.Join("PEDIDO", "PEDIDO.id", "PEDIDO_ITEM.pedido");

            //model.Join("PEDIDO_ITEM", "PEDIDO_ITEM.item", "ITEM.id");            
            //model.LeftJoin("CATEGORIA", "CATEGORIA.id", "ITEM.CATEGORIAID");
            //model.LeftJoin("PESSOA", "PESSOA.id", "ITEM.FORNECEDOR");

            model.Where("PEDIDO.TIPO", Home.pedidoPage);
            model.Where("PEDIDO.excluir", "=", "0");
            model.Where("PEDIDO_ITEM.excluir", "=", "0");
            model.Where("PEDIDO.emissao", ">=", Validation.ConvertDateToSql(dataInicial.Value, true));
            model.Where("PEDIDO.emissao", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));

            model.SelectRaw(
                "PEDIDO_ITEM.item as item, " +
                "PEDIDO_ITEM.cprod as cprod, " +
                "PEDIDO_ITEM.XPROD as produto, " +
                "PEDIDO_ITEM.MEDIDA as produto, " +
                "SUM(PEDIDO_ITEM.TOTALCOMPRA) as SumCompras, " +
                "SUM(PEDIDO_ITEM.TOTAL) as SumVendas, " +
                "SUM(PEDIDO_ITEM.QUANTIDADE) as TotalVendas");

            model.GroupBy("PEDIDO_ITEM.item", "PEDIDO_ITEM.cprod", "PEDIDO_ITEM.XPROD", "PEDIDO_ITEM.MEDIDA");

            //model.Limit(200);

            return model.GetAsync<dynamic>();
        }

        public Task<IEnumerable<dynamic>> GetDataTableTotal()
        {
            var model = new PedidoItem().Query();

            model.Join("PEDIDO", "PEDIDO.id", "PEDIDO_ITEM.pedido");

            //model.Join("PEDIDO_ITEM", "PEDIDO_ITEM.item", "ITEM.id");            
            //model.LeftJoin("CATEGORIA", "CATEGORIA.id", "ITEM.CATEGORIAID");
            //model.LeftJoin("PESSOA", "PESSOA.id", "ITEM.FORNECEDOR");

            model.Where("PEDIDO.TIPO", Home.pedidoPage);
            model.Where("PEDIDO.excluir", "=", "0");
            model.Where("PEDIDO_ITEM.excluir", "=", "0");
            model.Where("PEDIDO.emissao", ">=", Validation.ConvertDateToSql(dataInicial.Value, true));
            model.Where("PEDIDO.emissao", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));

            model.SelectRaw("SUM(PEDIDO_ITEM.TOTALCOMPRA) as SumCompras, " +
                "SUM(PEDIDO_ITEM.TOTAL) as SumVendas, " +
                "SUM(PEDIDO_ITEM.QUANTIDADE) as TotalVendas");

            //model.Limit(200);

            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView table, IEnumerable<dynamic> data = null)
        {
            table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            table.Columns[0].Name = "Descrição";
            table.Columns[0].Width = 150;

            table.Columns[1].Name = "Qtd.";
            table.Columns[1].Width = 100;

            table.Columns[2].Name = "Medida";
            table.Columns[2].Width = 100;

            table.Columns[3].Name = "Fornecedor";
            table.Columns[3].Width = 150;

            table.Columns[4].Name = "Categoria";
            table.Columns[4].Width = 150;

            table.Rows.Clear();

            if (data == null)
            {
                var dados = await GetDataTable();
                data = dados;
            }

            for (var i = 0; i < data.Count(); i++)
            {
                var item = data.ElementAt(i);

                table.Rows.Add(
                    item.NOME,
                    item.TOTALVENDAS,
                    item.MEDIDA,
                    item.FORNECEDOR_NAME,
                    item.CAT_NAME
                );
            }

            table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

                switch (Home.pedidoPage)
                {
                    case "Consignações":
                        maisVendidos.Text = @"Mais Consignado";
                        menosVendidos.Text = @"Menos Consignado";
                        label1.Text = @"Produtos Consignados";
                        label2.Text = @"Consulte os produtos consignados aqui.";
                        label3.Text = @"Produtos Consignados";
                        break;

                    case "Devoluções":
                        maisVendidos.Text = @"Mais Devolvido";
                        menosVendidos.Text = @"Menos Devolvido";
                        label1.Text = @"Produtos Devolvidos";
                        label2.Text = @"Consulte os produtos devolvidos aqui.";
                        label3.Text = @"Produtos Devolvidos";
                        break;

                    case "Orçamentos":
                        maisVendidos.Text = @"Mais Orçado";
                        menosVendidos.Text = @"Menos Orçado";
                        label1.Text = @"Produtos Orçados";
                        label2.Text = @"Consulte os produtos orçados aqui.";
                        label3.Text = @"Produtos Orçados";
                        break;
                }

                filterAll.Checked = true;

                // Autocomplete de produtos
                collection = _mItem.AutoComplete("Produtos");
                BuscarProduto.AutoCompleteCustomSource = collection;

                AutoCompleteFornecedorCategorias();

                dataInicial.Text = DateTime.Now.ToString();
                dataFinal.Text = DateTime.Now.ToString();

                await DataTableAsync();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSearch.Click += async (s, e) => { await DataTableAsync(); };

            //imprimir.Click += async (s, e) => await RenderizarAsync();

            imprimir.Click += async (s, e) => 
            {
                SelectionReports.screen = "Produtos Vendidos";
                using (var f = new SelectionReports())
                {
                    f.ShowDialog();

                    switch (SelectionReports.report)
                    {
                        case "02 - Margem":
                            Renderizar2Async();
                            break;
                        default:
                            RenderizarAsync();
                            break;
                    }
                }
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
        }

        private async Task RenderizarAsync()
        {
            var dados = await GetDataTable();

            var data = new ArrayList();
            foreach (var item in dados)
                data.Add(new
                {
                    Nome = item.NOME,
                    Referencia = item.REFERENCIA,
                    QtdVendido = item.TOTALVENDAS,
                    Medida = item.MEDIDA,
                    Categoria = item.CAT_NAME,
                    Fornecedor = item.FORNECEDOR_NAME
                });

            var tipo_aux = "";

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\ProdutosVendidos.html"));
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
            using (var f = new Browser())
            {
                f.ShowDialog();
            }
        }

        private async Task Renderizar2Async()
        {
            var dados = await GetDataTable2();

            double t_compra = 0, t_venda = 0, t_diff = 0;

            var data = new ArrayList();
            foreach (var item in dados)
            {
                string compra = "0,00", venda = "0,00", a_diff = "", a_margem = ""; double diff = 0, margem = 0;

                compra = (Validation.ConvertToDouble(item.SUMCOMPRAS) > 0) ? Validation.FormatPrice(Validation.ConvertToDouble(item.SUMCOMPRAS)) : "0,00";
                venda = (Validation.ConvertToDouble(item.SUMVENDAS) > 0) ? Validation.FormatPrice(Validation.ConvertToDouble(item.SUMVENDAS)) : "0,00";
                
                if (Validation.ConvertToDouble(item.SUMCOMPRAS) > 0 && Validation.ConvertToDouble(item.SUMVENDAS) > 0)
                {
                    diff = Validation.Round(Validation.ConvertToDouble(item.SUMVENDAS) - Validation.ConvertToDouble(item.SUMCOMPRAS));
                    margem = Validation.Round((diff * 100) / Validation.ConvertToDouble(item.SUMCOMPRAS));
                }

                a_diff = Validation.FormatPrice(diff);
                a_margem = Validation.FormatPrice(margem);

                /*data.Add(new
                {
                    Nome = item.NOME,
                    Referencia = item.REFERENCIA,
                    QtdVendido = item.TOTALVENDAS,
                    Medida = item.MEDIDA,
                    Categoria = item.CAT_NAME,
                    Fornecedor = item.FORNECEDOR_NAME,
                    SumCompras = compra,
                    SumVendas = venda,
                    SumDiff = a_diff,
                    SumMargem = a_margem
                });*/
                
                data.Add(new
                {
                    Nome = item.PRODUTO,
                    Referencia = item.CPROD,
                    QtdVendido = item.TOTALVENDAS,
                    Medida = "",
                    Categoria = "",
                    Fornecedor = "",
                    SumCompras = compra,
                    SumVendas = venda,
                    SumDiff = a_diff,
                    SumMargem = a_margem
                });

                t_compra += Validation.ConvertToDouble(item.SUMCOMPRAS);
                t_venda += Validation.ConvertToDouble(item.SUMVENDAS);
                t_diff += Validation.ConvertToDouble(diff);
            }

            var dadosTotal = await GetDataTableTotal();
            foreach (var item in dadosTotal)
            {
                t_compra = Validation.ConvertToDouble(item.SUMCOMPRAS);
                t_venda = Validation.ConvertToDouble(item.SUMVENDAS);

                double diff = Validation.Round(t_venda - t_compra);
                t_diff = Validation.ConvertToDouble(diff);
            }

            string tipo_aux = "", aux_compra = Validation.FormatPrice(t_compra), aux_venda = Validation.FormatPrice(t_venda), aux_diff = Validation.FormatPrice(t_diff);

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\ProdutosVendidosMargem.html"));
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
                Titulo = tipo_aux,
                total_compra = aux_compra,
                total_venda = aux_venda,
                total_diff = aux_diff,
            }));

            Browser.htmlRender = render;
            using (var f = new Browser())
            {
                f.ShowDialog();
            }
        }
    }
}
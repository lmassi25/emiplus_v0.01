using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.View.Reports;
using RazorEngine;
using RazorEngine.Templating;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Etiquetas : Form
    {

        private Model.Item _mItem = new Model.Item();
        private Controller.Etiqueta _controller = new Controller.Etiqueta();

        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        public Etiquetas()
        {
            InitializeComponent();
            Eventos();
        }

        private void Start()
        {
            var origens = new ArrayList();
            origens.Add(new { Id = "10", Nome = "Pimaco 10, CARTA - Cod. 6283" });
            origens.Add(new { Id = "30", Nome = "Pimaco 30, CARTA - Cod. 6280" });
            origens.Add(new { Id = "60", Nome = "Pimaco 60, CARTA - Cod. 6089" });

            modelos.DataSource = origens;
            modelos.DisplayMember = "Nome";
            modelos.ValueMember = "Id";
        }

        private void DataTableStart()
        {
            GridLista.Visible = false;
            Loading.Size = new System.Drawing.Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void DataTable() => await _controller.SetTable(GridLista, null);

        private void LoadItens()
        {
            int count = new Model.Etiqueta().Count();
            qtdAdd.Text = count.ToString();
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

        private void AddItem()
        {
            int count = new Model.Etiqueta().Count();
            if (count >= Validation.ConvertToInt32(modelos.SelectedValue))
            {
                Alert.Message("Oppss", "A quantidade de produtos já está no limite. Altere o modelo da etiqueta.", Alert.AlertType.warning);
                return;
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                var itemId = collection.Lookup(BuscarProduto.Text);
                Model.Item item = _mItem.FindById(itemId).Where("excluir", 0).Where("tipo", "Produtos").First<Model.Item>();

                Model.Etiqueta etiqueta = new Model.Etiqueta();
                etiqueta.id_item = item.Id;
                etiqueta.quantidade = 1;
                etiqueta.Save(etiqueta);
            }
        }

        /// <summary>
        /// Limpa os input text.
        /// </summary>
        private void ClearForms()
        {
            BuscarProduto.Clear();
            Quantidade.Clear();
        }

        private void Render()
        {
            //dynamic model = new ExpandoObject();
            //model.Etiqueta = "etiqueta" + modelos.SelectedValue.ToString() + ".css";
            //model.Colunas = string.IsNullOrEmpty(colunas.Text) ? 0 : Validation.ConvertToInt32(colunas.Text);

            var itens = new Model.Etiqueta().Query()
                .LeftJoin("item", "item.id", "etiqueta.id_item")
                .Where("item.excluir", 0)
                .OrderByDesc("etiqueta.criado")
                .Limit(Validation.ConvertToInt32(modelos.SelectedValue) - Validation.ConvertToInt32(colunas.Text))
                .Get<dynamic>();

            //object renderItem = new {};
            //IDictionary<string, string> renderItem = new Dictionary<string, string>();

            //List<dynamic> origens = new List<dynamic>();
            //foreach (var item in itens)
            //{
            //    origens.Add(new { Nome = item.NOME, Code = item.REFERENCIA });
            //}

            //IEnumerable<dynamic> testes = origens;
            
            var Etiqueta = "etiqueta" + modelos.SelectedValue.ToString() + ".css";

            StringBuilder builder = new StringBuilder();
            builder.Append("<!DOCTYPE html>");
            builder.Append("<html lang='pt-br' xmlns='http://www.w3.org/1999/xhtml'>");
            builder.Append("<head>");
            builder.Append("    <meta charset='utf-8' />");
            builder.Append($"    <link href='https://www.emiplus.com.br/razor/css/{Etiqueta}?v=20' rel='stylesheet' />");
            builder.Append("</head>");
            builder.Append($"<body class='letter'>");
            builder.Append("    <section class='sheet padding-10mm'>");
            builder.Append("        <article>");
            builder.Append("            <table>");
            builder.Append("                <tr>");

            var Colunas = string.IsNullOrEmpty(colunas.Text) ? 0 : Validation.ConvertToInt32(colunas.Text);
            for (int i = 0; i < Colunas; i++)
            {
                builder.Append("<td></td>");
            }

            foreach (var item in itens)
            {
                builder.Append($"<td>{item.NOME}</td>");
            }
            builder.Append("                </tr>");
            builder.Append("            </table>");
            builder.Append("        </article>");
            builder.Append("    </section>");
            builder.Append("</body>");
            builder.Append("</html>");

            var r = builder.ToString().Replace("{", "").Replace("}","");

            //model.Item = origens;

            //foreach (var items in origens)
            //{
            //    Console.WriteLine(items.Nome);
            //}

            //model.Item = new Model.Etiqueta().Query()
            //    .LeftJoin("item", "item.id", "etiqueta.id_item")
            //    .Where("item.excluir", 0)
            //    .OrderByDesc("etiqueta.criado")
            //    .Limit(Validation.ConvertToInt32(modelos.SelectedValue) - Validation.ConvertToInt32(colunas.Text))
            //    .Get<dynamic>();

            //var render = Engine.Razor.RunCompile(File.ReadAllText(Program.PATH_BASE + @"\View\Reports\html\Etiqueta.cshtml"), "templateKey", null, (object)model);

            Browser.htmlRender = r;
            var f = new Browser();
            f.ShowDialog();
        }

        /// <summary>
        /// Adiciona os eventos nos Controls do form.
        /// </summary>
        private void Eventos()
        {

            Load += (s, e) =>
            {
                BuscarProduto.Select();
                BuscarProduto.Focus();

                AutoCompleteItens();
                Start();
                DataTableStart();
                LoadItens();
            };

            addProduto.Click += (s, e) =>
            {
                for (int i = 0; i < Validation.ConvertToInt32(Quantidade.Text); i++)
                {
                    AddItem();
                }

                // Limpa os campos
                ClearForms();
                DataTable();
                LoadItens();
            };

            imprimir.Click += (s, e) => Render();

            modelos.SelectedValueChanged += (s, e) =>
            {
                label9.Text = modelos.SelectedValue.ToString();
            };

            btnClean.Click += (s, e) =>
            {
                new Model.Etiqueta().Clean();
                DataTable();
                LoadItens();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _controller.GetDataTable();
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    LoadItens();
                    await _controller.SetTable(GridLista, dataTable);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
        }
    }
}
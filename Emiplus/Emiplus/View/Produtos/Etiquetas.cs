using DotLiquid;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            Loading.Size = new Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void DataTable() => await _controller.SetTable(GridLista, null);

        private void LoadItens()
        {
            int count = new Model.Etiqueta().Count();
            qtdAdd.Text = count.ToString();
            
            if (count <= 10)
                modelos.SelectedValue = "10";

            if (count <= 30)
                modelos.SelectedValue = "30";

            if (count >= 31)
                modelos.SelectedValue = "60";
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
            var itens = new Model.Etiqueta().Query()
                .LeftJoin("item", "item.id", "etiqueta.id_item")
                .Where("item.excluir", 0)
                .OrderByDesc("etiqueta.criado")
                .Limit(Validation.ConvertToInt32(modelos.SelectedValue) - Validation.ConvertToInt32(colunas.Text))
                .Get<dynamic>();

            var Etiqueta = "etiqueta" + modelos.SelectedValue.ToString() + ".css";
            var Colunas = string.IsNullOrEmpty(colunas.Text) ? 0 : Validation.ConvertToInt32(colunas.Text);

            bool logo = false;
            if (modelos.SelectedValue.ToString() == "10")
                logo = true;

            bool hideLogoHtml = false;
            if (hideLogo.Checked)
                hideLogoHtml = true;

            bool hideRefHtml = false;
            if (hideRef.Checked)
                hideRefHtml = true;

            bool hideCodeHtml = false;
            if (hideCode.Checked)
                hideCodeHtml = true;

            string logoUrl = Settings.Default.empresa_logo;

            ArrayList t = new ArrayList();
            foreach (var item in itens)
            {
                var codeImageBar = "";
                if (!String.IsNullOrEmpty(item.REFERENCIA))
                {
                    BarcodeLib.TYPE typeBarCode;
                    if (Regex.Matches(item.REFERENCIA, @"[a-zA-Z]").Count == 0)
                        typeBarCode = BarcodeLib.TYPE.EAN13;
                    else
                        typeBarCode = BarcodeLib.TYPE.CODE128;

                    Image img = (new BarcodeLib.Barcode()).Encode(typeBarCode, item.REFERENCIA, Color.Black, Color.White, 195, 105);
                    codeImageBar = ImageToBase64(img, ImageFormat.Png);
                }

                t.Add(new {
                    Nome = item.NOME,
                    Ref = item.REFERENCIA,
                    Price = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA)),
                    Code = codeImageBar
                });
            }

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\View\Reports\html\Etiqueta.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                urlBase = Program.URL_BASE,
                ModeloCss = Etiqueta,
                mitens = t,
                Colunas = Colunas,
                showLogo = logo,
                LogoUrl = logoUrl,
                logoHtml = hideLogoHtml,
                refHtml = hideRefHtml,
                codeHtml = hideCodeHtml
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }

        public string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to base 64 string
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
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
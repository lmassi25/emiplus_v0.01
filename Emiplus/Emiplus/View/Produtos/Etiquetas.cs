using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BarcodeLib;
using DotLiquid;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Reports;
using SqlKata.Execution;
using Item = Emiplus.Model.Item;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Etiquetas : Form
    {
        private readonly Etiqueta _controller = new Etiqueta();
        private readonly Item _mItem = new Item();

        private readonly KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        private IEnumerable<dynamic> dataTable;

        private readonly Timer timer = new Timer(Configs.TimeLoading);
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public Etiquetas()
        {
            InitializeComponent();
            Eventos();

            ToolHelp.Show("Informe a quantidade de posições que você já utilizou na folha atual.", pictureBox2,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Selecione o modelo da etiqueta.", pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Digite a quantidade de etiquetas que deseja gerar.", pictureBox5, ToolHelp.ToolTipIcon.Info,
                "Ajuda!");
        }

        private void Start()
        {
            var origens = new ArrayList
            {
                new {Id = "10", Nome = "Pimaco 10, CARTA - Cod. 6283"},
                new {Id = "30", Nome = "Pimaco 30, CARTA - Cod. 6280"},
                new {Id = "60", Nome = "Pimaco 60, CARTA - Cod. 6089"}
            };

            modelos.DataSource = origens;
            modelos.DisplayMember = "Nome";
            modelos.ValueMember = "Id";
        }

        private void DataTableStart()
        {
            GridLista.Visible = false;
            Loading.Size = new Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            workerBackground.RunWorkerAsync();
        }

        private async void DataTable()
        {
            await _controller.SetTable(GridLista);
        }

        private void LoadItens()
        {
            var count = new Model.Etiqueta().Count();
            qtdAdd.Text = count.ToString();

            if (count <= 10)
                modelos.SelectedValue = "10";

            if (count <= 30)
                modelos.SelectedValue = "30";

            if (count >= 31)
                modelos.SelectedValue = "60";
        }

        /// <summary>
        ///     Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item) collection.Add(itens.NOME, itens.ID);

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private void AddItem()
        {
            var count = new Model.Etiqueta().Count();
            if (count >= Validation.ConvertToInt32(modelos.SelectedValue))
            {
                Alert.Message("Oppss", "A quantidade de produtos já está no limite. Altere o modelo da etiqueta.",
                    Alert.AlertType.warning);
                return;
            }

            if (collection.Lookup(BuscarProduto.Text) <= 0)
                return;

            var itemId = collection.Lookup(BuscarProduto.Text);
            var item = _mItem.FindById(itemId).Where("excluir", 0).Where("tipo", "Produtos").First<Item>();
            labelEstoque.Text = $"Estoque: {item.EstoqueAtual}";

            var etiqueta = new Model.Etiqueta
            {
                id_item = item.Id,
                quantidade = 1
            };
            etiqueta.Save(etiqueta);
        }

        /// <summary>
        ///     Limpa os input text.
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

            var Etiqueta = "etiqueta" + modelos.SelectedValue + ".css";
            var Colunas = string.IsNullOrEmpty(colunas.Text) ? 0 : Validation.ConvertToInt32(colunas.Text);

            var paddingEtiqueta = "";
            if (modelos.SelectedValue.ToString() == "10")
                paddingEtiqueta =
                    $"{IniFile.Read("Pimaco10Top", "ETIQUETAS")}mm {IniFile.Read("Pimaco10Right", "ETIQUETAS")}mm {IniFile.Read("Pimaco10Bottom", "ETIQUETAS")}mm {IniFile.Read("Pimaco10Left", "ETIQUETAS")}mm";

            if (modelos.SelectedValue.ToString() == "30")
                paddingEtiqueta =
                    $"{IniFile.Read("Pimaco30Top", "ETIQUETAS")}mm {IniFile.Read("Pimaco30Right", "ETIQUETAS")}mm {IniFile.Read("Pimaco30Bottom", "ETIQUETAS")}mm {IniFile.Read("Pimaco30Left", "ETIQUETAS")}mm";

            if (modelos.SelectedValue.ToString() == "60")
                paddingEtiqueta =
                    $"{IniFile.Read("Pimaco60Top", "ETIQUETAS")}mm {IniFile.Read("Pimaco60Right", "ETIQUETAS")}mm {IniFile.Read("Pimaco60Bottom", "ETIQUETAS")}mm {IniFile.Read("Pimaco60Left", "ETIQUETAS")}mm";


            bool logo = modelos.SelectedValue.ToString() == "10";
            bool hideLogoHtml = hideLogo.Checked;
            bool hideRefHtml = hideRef.Checked;
            bool hideCodeHtml = hideCode.Checked;

            var logoUrl = Settings.Default.empresa_logo;
            var aux_codbar = "";

            var t = new ArrayList();
            foreach (var item in itens)
            {
                var codeImageBar = "";
                aux_codbar = "";

                if (!string.IsNullOrEmpty(item.CODEBARRAS) && string.IsNullOrEmpty(aux_codbar))
                    aux_codbar = item.CODEBARRAS;

                if (!string.IsNullOrEmpty(item.REFERENCIA) && string.IsNullOrEmpty(aux_codbar))
                    aux_codbar = item.REFERENCIA;

                if (!string.IsNullOrEmpty(aux_codbar))
                {
                    var typeBarCode = Regex.Matches(aux_codbar, @"[a-zA-Z]").Count > 0 ? TYPE.EAN13 : TYPE.CODE128;

                    var img = new Barcode().Encode(typeBarCode, aux_codbar, Color.Black, Color.White, 195, 105);
                    codeImageBar = ImageToBase64(img, ImageFormat.Png);
                }

                t.Add(new
                {
                    Nome = item.NOME,
                    Ref = item.REFERENCIA,
                    Price = Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA)),
                    Code = codeImageBar,
                    Nr = item.CODEBARRAS
                });
            }

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Etiqueta.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                urlBase = Program.URL_BASE,
                ModeloCss = Etiqueta,
                mitens = t,
                Colunas,
                showLogo = logo,
                LogoUrl = logoUrl,
                logoHtml = hideLogoHtml,
                refHtml = hideRefHtml,
                codeHtml = hideCodeHtml,
                padding = paddingEtiqueta
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }

        public string ImageToBase64(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                var imageBytes = ms.ToArray();

                // Convert byte[] to base 64 string
                var base64String = Convert.ToBase64String(imageBytes);
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
            Masks.SetToUpper(this);

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
                for (var i = 0; i < Validation.ConvertToInt32(Quantidade.Text); i++) AddItem();

                // Limpa os campos
                ClearForms();
                DataTable();
                LoadItens();
            };

            imprimir.Click += (s, e) => Render();

            modelos.SelectedValueChanged += (s, e) => { label9.Text = modelos.SelectedValue.ToString(); };

            btnClean.Click += (s, e) =>
            {
                new Model.Etiqueta().Clean();
                DataTable();
                LoadItens();
            };

            BuscarProduto.TextChanged += (s, e) =>
            {
                labelEstoque.Visible = false;
                if (collection.Lookup(BuscarProduto.Text) <= 0)
                    return;

                labelEstoque.Visible = true;
                var itemId = collection.Lookup(BuscarProduto.Text);
                var item = _mItem.FindById(itemId).Where("excluir", 0).Where("tipo", "Produtos").First<Item>();
                labelEstoque.Text = $"Estoque atual: {item.EstoqueAtual}";
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            using (var b = workerBackground)
            {
                b.DoWork += async (s, e) => { dataTable = await _controller.GetDataTable(); };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    LoadItens();
                    await _controller.SetTable(GridLista, dataTable);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;

            btnVideoAjuda.Click += (s, e) => Support.Video("https://www.youtube.com/watch?v=_ybEHAVTXUA");
        }
    }
}
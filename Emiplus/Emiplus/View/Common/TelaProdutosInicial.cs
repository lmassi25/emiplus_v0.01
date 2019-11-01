using Emiplus.Data.Helpers;
using Emiplus.View.Reports;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Dynamic;
using System.IO;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaProdutosInicial : Form
    {
        public TelaProdutosInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Produtos.Click += (s, e) => OpenForm.Show<Produtos.Produtos>(this);

            Etiquetas.Click += (s, e) => OpenForm.Show<Produtos.Etiquetas>(this);

            Categorias.Click += (s, e) => OpenForm.Show<Produtos.Categorias>(this);

            Impostos.Click += (s, e) => OpenForm.Show<Produtos.Impostos>(this);

            fornecedores.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            transportadoras.Click += (s, e) =>
            {
                Home.pessoaPage = "Transportadoras";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            Compras.Click += (s, e) =>
            {
                Home.pedidoPage = "Compras";
                Comercial.Pedido Pedido = new Comercial.Pedido();
                Pedido.ShowDialog();
            };

            button2.Click += (s, e) =>
            {
                dynamic model = new ExpandoObject();
                model.Name = "Matt";

                var render = Engine.Razor.RunCompile(File.ReadAllText(Program.PATH_BASE + @"\View\Reports\html\Etiqueta10.cshtml"), "templateKey", null, (object)model);

                Browser.htmlRender = render;
                var f = new Browser();
                f.ShowDialog();
            };
        }
    }
}

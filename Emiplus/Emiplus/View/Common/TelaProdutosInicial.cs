using Emiplus.Data.Helpers;
using Emiplus.View.Reports;
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

            HistoricoEntradaSaida.Click += (s, e) =>
            {
                EstoqueEntradaSaida Estoque = new EstoqueEntradaSaida();
                Estoque.ShowDialog();
            };

            Estoque.Click += (s, e) =>
            {
                Inventario Estoque = new Inventario();
                Estoque.ShowDialog();
            };

            ProdutosVendidos.Click += (s, e) =>
            {
                ProdutosVendidos ProdVendidos = new ProdutosVendidos();
                ProdVendidos.ShowDialog();
            };
        }
    }
}

using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaProdutosInicial : Form
    {
        public TelaProdutosInicial()
        {
            InitializeComponent();
        }

        private void Produtos_Click(object sender, EventArgs e)
        {
            OpenForm.Show<Produtos.Produtos>(this);
        }

        private void Categorias_Click(object sender, EventArgs e)
        {
            OpenForm.Show<Produtos.Categorias>(this);
        }

        private void Impostos_Click(object sender, EventArgs e)
        {
            OpenForm.Show<Produtos.Impostos>(this);
        }

        private void Fornecedores_Click(object sender, EventArgs e)
        {
            Home.pessoaPage = "Fornecedores";
            OpenForm.Show<Comercial.Clientes>(this);
        }

        private void Transportadoras_Click(object sender, EventArgs e)
        {
            Home.pessoaPage = "Transportadoras";
            OpenForm.Show<Comercial.Clientes>(this);
        }
    }
}

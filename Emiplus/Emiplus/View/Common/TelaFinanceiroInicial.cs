using Emiplus.Data.Helpers;
using Emiplus.View.Financeiro;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaFinanceiroInicial : Form
    {
        public TelaFinanceiroInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Clientes.Click += (s, e) =>
            {
                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            Categorias.Click += (s, e) =>
            {
                Home.CategoriaPage = "Financeiro";
                OpenForm.Show<Produtos.Categorias>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            aReceber.Click += (s, e) =>
            {
                Home.financeiroPage = "Receber";
                OpenForm.Show<Titulos>(this);
            };

            aPagar.Click += (s, e) =>
            {
                Home.financeiroPage = "Pagar";
                OpenForm.Show<Titulos>(this);
            };

            novoRecebimento.Click += (s, e) =>
            {
                EditarTitulo.IdTitulo = 0;
                Home.financeiroPage = "Receber";
                OpenForm.Show<EditarTitulo>(this);
            };

            novoPagamento.Click += (s, e) =>
            {
                EditarTitulo.IdTitulo = 0;
                Home.financeiroPage = "Pagar";
                OpenForm.Show<EditarTitulo>(this);
            };
            
            AbrirCaixa.Click += (s, e) =>
            {
                AbrirCaixa f = new AbrirCaixa();
                f.ShowDialog();
            };

            Caixa.Click += (s, e) =>
            {
                Caixa f = new Caixa();
                f.ShowDialog();
            };
        }
    }
}

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

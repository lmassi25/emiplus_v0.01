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

            Events();
        }

        private void Events()
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
        }
    }
}

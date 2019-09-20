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
        }

        private void AReceber_Click(object sender, System.EventArgs e)
        {
            Home.financeiroPage = "Receber";
            OpenForm.Show<Titulos>(this);
        }

        private void APagar_Click(object sender, System.EventArgs e)
        {
            Home.financeiroPage = "Pagar";
            OpenForm.Show<Titulos>(this);
        }
    }
}

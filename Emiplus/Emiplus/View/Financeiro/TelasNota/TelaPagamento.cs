using Emiplus.Data.Helpers;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro.TelasNota
{
    public partial class TelaPagamento : Form
    {
        public TelaPagamento()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Next.Click += (s, e) =>
            {
                OpenForm.Show<TelaFinal>(this);
            };

            Back.Click += (s, e) => Close();
        }
    }
}

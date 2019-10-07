using Emiplus.Data.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro.TelasNota
{
    public partial class TelaProdutos : Form
    {
        public TelaProdutos()
        {
            InitializeComponent();

            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
            };

            Next.Click += (s, e) =>
            {
                OpenForm.Show<TelaFrete>(this);
            };

            Back.Click += (s, e) => Close();
        }
    }
}

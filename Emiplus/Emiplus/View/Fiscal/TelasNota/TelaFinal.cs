using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaFinal : Form
    {
        public TelaFinal()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Back.Click += (s, e) => Close();
        }
    }
}

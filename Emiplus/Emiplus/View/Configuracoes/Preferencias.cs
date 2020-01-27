using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Preferencias : Form
    {
        public Preferencias()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            btnExit.Click += (s, e) => Close();
        }
    }
}
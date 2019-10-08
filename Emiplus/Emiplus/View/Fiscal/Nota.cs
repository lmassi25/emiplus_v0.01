using Emiplus.Data.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class Nota : Form
    {

        public Nota()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                OpenForm.ShowInPanel<TelasNota.TelaDados>(panelTelas);
            };
        }
    }
}

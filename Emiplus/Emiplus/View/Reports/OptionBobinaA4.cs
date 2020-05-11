using System.Windows.Forms;

namespace Emiplus.View.Reports
{
    public partial class OptionBobinaA4 : Form
    {
        public OptionBobinaA4()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) => { Tipo.SelectedIndex = 0; };

            btnGerar.Click += (s, e) =>
            {
                switch (Tipo.SelectedItem)
                {
                    case "Folha A4":
                        DialogResult = DialogResult.OK;
                        break;
                    case "Bobina":
                        DialogResult = DialogResult.Cancel;
                        break;
                }

                Close();
            };

            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Abort; };
        }
    }
}
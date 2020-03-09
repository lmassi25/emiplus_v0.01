using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Load += (s, e) =>
            {
                Tipo.SelectedIndex = 0;
            };

            btnGerar.Click += (s, e) =>
            {
                if (Tipo.SelectedItem == "Folha A4")
                    DialogResult = DialogResult.OK;
                else if (Tipo.SelectedItem == "Bobina")
                    DialogResult = DialogResult.Cancel;
                    
                Close();
            };

            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Abort;
            };
        }
    }
}

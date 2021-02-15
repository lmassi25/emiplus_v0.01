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
    public partial class SelectionReports : Form
    {
        public static string screen { get; set; }
        public static string report { get; set; }

        public SelectionReports()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) => 
            {
                switch (screen)
                {
                    case "Produtos Vendidos":
                        Tipo.Items.Add("01 - Todos");
                        Tipo.Items.Add("02 - Margem");
                        break;
                }
            };

            btnGerar.Click += (s, e) =>
            {
                report = Tipo.Text;
                Close();
            };

            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Abort; };
        }
    }
}

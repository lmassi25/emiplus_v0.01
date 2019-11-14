using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class DetailsCaixa : Form
    {

        public static int idCaixa { get; set; }

        public DetailsCaixa()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                caixa.Text = idCaixa.ToString();
                nrPedido.Text = idCaixa.ToString();
            };

            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }
    }
}

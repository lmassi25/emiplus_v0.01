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
    public partial class AddCaixaMov : Form
    {
        public AddCaixaMov()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            btnSalvar.Click += (s, e) =>
            {

            };

            btnCancelar.Click += (s, e) => Close();
        }
    }
}

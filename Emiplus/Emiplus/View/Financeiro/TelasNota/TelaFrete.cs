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

namespace Emiplus.View.Financeiro.TelasNota
{
    public partial class TelaFrete : Form
    {
        public TelaFrete()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Next.Click += (s, e) =>
            {
                OpenForm.Show<TelaPagamento>(this);
            };

            Back.Click += (s, e) => Close();
        }
    }
}

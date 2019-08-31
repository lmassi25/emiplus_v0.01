using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddClienteContato : Form
    {
        public AddClienteContato()
        {
            InitializeComponent();
        }

        private void BtnContatoCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

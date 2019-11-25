using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Testes
{
    public partial class Form6 : Form
    {
        private Controller.Fiscal _fiscal = new Controller.Fiscal(); 

        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_fiscal.Emitir(357, "CFe"));
        }
    }
}

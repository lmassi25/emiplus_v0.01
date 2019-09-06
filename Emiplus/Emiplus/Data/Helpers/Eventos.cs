using System;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    class Eventos : Form
    {
        public static void KeyClose(Form form, object sender, KeyEventArgs e)
        {            
            if (e.KeyValue.Equals(27))
            {
                form.Close();
            }
        }

        public static void Click(object sender, EventArgs e)
        {
            MessageBox.Show("ola");
        }
    }
}

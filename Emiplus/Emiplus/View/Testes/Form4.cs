using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Emiplus.View.Testes
{
    public partial class Form4 : Form
    {
        public string Nome {
            get
            {
                return Nome;
            }

            set
            {
                if (value == "William")
                {
                    Nome = value;
                }
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name != null ? name : "NA";
            }
            set
            {
                name = value;
            }
        }

        System.Timers.Timer timer = new System.Timers.Timer(500);

        public Form4()
        {
            InitializeComponent();

            timer.AutoReset = false;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }        

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            timer.Stop();
            timer.Start();
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Console.WriteLine("Do something here.");
            MessageBox.Show("Do something here.");
        }

    }
}

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

        public Form4()
        {
            InitializeComponent();

            var t = Name = null;
            Console.WriteLine(t);
        }
    }
}

using Emiplus.Data.Core;
using Emiplus.Data.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class Developer : Form
    {
        public Developer()
        {
            InitializeComponent();

            label2.Text = new Connect()._path;
        }
    }
}

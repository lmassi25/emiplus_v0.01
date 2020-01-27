using Emiplus.Data.Database;
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
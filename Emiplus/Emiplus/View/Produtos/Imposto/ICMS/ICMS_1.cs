using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class ICMS_1 : UserControl
    {
        public string Aliq_Value
        {
            get
            {
                return Aliq.Text;
            }
            set
            {
                Aliq.Text = value;
            }
        }

        public ICMS_1()
        {
            InitializeComponent();
        }
    }
}
using System.Windows.Forms;

namespace Emiplus.View.Produtos.Imposto.ICMS
{
    public partial class ICMS_3 : UserControl
    {
        public string Base_Value
        {
            get
            {
                return Base.Text;
            }
            set
            {
                Base.Text = value;
            }
        }

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

        public ICMS_3()
        {
            InitializeComponent();
        }
    }
}
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class ICMS_2 : UserControl
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

        public string RedBase_Value
        {
            get
            {
                return RedBase.Text;
            }
            set
            {
                RedBase.Text = value;
            }
        }

        public string IVA_Value
        {
            get
            {
                return IVA.Text;
            }
            set
            {
                IVA.Text = value;
            }
        }

        public string AliqICMS_Value
        {
            get
            {
                return AliqICMS.Text;
            }
            set
            {
                AliqICMS.Text = value;
            }
        }

        public string RedBaseST_Value
        {
            get
            {
                return RedBaseST.Text;
            }
            set
            {
                RedBaseST.Text = value;
            }
        }

        public string AliqST_Value
        {
            get
            {
                return AliqST.Text;
            }
            set
            {
                AliqST.Text = value;
            }
        }

        public ICMS_2()
        {
            InitializeComponent();
        }
    }
}
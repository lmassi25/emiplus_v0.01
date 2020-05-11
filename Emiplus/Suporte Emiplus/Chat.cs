using System.Windows.Forms;
using Emiplus.Data.Core;

namespace Suporte_Emiplus
{
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();

            var f = new ChatSupport();
            f.InitializeChromiumAsync(panelBrowser);
        }
    }
}
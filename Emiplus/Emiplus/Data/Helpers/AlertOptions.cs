using Emiplus.View.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    class AlertOptions
    {
        public static bool Message(string title, string message, AlertBig.AlertType type, AlertBig.AlertBtn btn, bool focus = false)
        {
            var result = new AlertBig(title, message, type, btn, focus);
            if (result.ShowDialog() == DialogResult.OK)
                return true;

            return false;
        }
    }
}

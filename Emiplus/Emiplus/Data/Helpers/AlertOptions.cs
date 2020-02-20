using Emiplus.View.Common;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    internal class AlertOptions
    {
        public static bool Message(string title, string message, AlertBig.AlertType type, AlertBig.AlertBtn btn, bool focus = false)
        {
            var result = new AlertBig(title, message, type, btn, focus);
            result.TopMost = true;
            if (result.ShowDialog() == DialogResult.OK)
                return true;

            return false;
        }
    }
}
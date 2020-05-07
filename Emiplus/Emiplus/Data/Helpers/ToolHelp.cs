using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    public class ToolHelp
    {
        public enum ToolTipIcon
        {
            Error,
            Info,
            Warning,
            None
        }

        public static void Show(string message, PictureBox image, ToolTipIcon icon = ToolTipIcon.Info,
            string title = null)
        {
            var tool = new ToolTip();

            if (title != null)
                tool.ToolTipTitle = title;

            switch (icon)
            {
                case ToolTipIcon.Error:
                    tool.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
                    break;

                case ToolTipIcon.Info:
                    tool.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
                    break;

                case ToolTipIcon.Warning:
                    tool.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
                    break;

                case ToolTipIcon.None:
                    tool.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
                    break;
            }

            tool.IsBalloon = true;
            tool.AutoPopDelay = 9999999;
            tool.InitialDelay = 0;
            tool.ReshowDelay = 0;
            tool.AutomaticDelay = 0;
            tool.ShowAlways = true;
            tool.SetToolTip(image, message);

            image.Cursor = Cursors.Help;
        }
    }
}
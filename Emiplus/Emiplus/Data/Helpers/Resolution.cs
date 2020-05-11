using Emiplus.Data.Core;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    internal class Resolution
    {
        private static Rectangle _resolution = Screen.PrimaryScreen.Bounds;

        /// <summary>
        /// Valida a resolução do monitor
        /// </summary>
        public static void ValidateResolution()
        {
            if (_resolution.Height < 767 || _resolution.Width < 1023)
            {
                bool result = AlertOptions.Message("Oppss", $"A resolução({_resolution.Height}) do seu monitor não cumpre os requisitos do sistema. \nRequisitos mínimos de resolução: 1024x768", View.Common.AlertBig.AlertType.warning, View.Common.AlertBig.AlertBtn.OK);
                if (result)
                    Validation.KillEmiplus();
            }
        }

        public static bool SetScreenMaximized(Form f)
        {
            if (IniFile.Read("Maximizar", "TELAS") == "true")
            {
                f.WindowState = FormWindowState.Maximized;
                return true;
            }

            if (_resolution.Height == 768)
            {
                f.WindowState = FormWindowState.Maximized;
                return true;
            }

            return false;
        }
    }
}
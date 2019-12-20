using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    class Resolution
    {
        static Rectangle resolution = Screen.PrimaryScreen.Bounds;

        /// <summary>
        /// Valida a resolução do monitor
        /// </summary>
        public static void ValidateResolution()
        {
            if (resolution.Height < 767 || resolution.Width < 1023)
            {
                bool result = AlertOptions.Message("Oppss", $"A resolução({resolution.Height}) do seu monitor não cumpre os requisitos do sistema. \nRequisitos mínimos de resolução: 1024x768", View.Common.AlertBig.AlertType.warning, View.Common.AlertBig.AlertBtn.OK);
                if (result)
                    Validation.KillEmiplus();
            }
        }

        public static bool SetScreenMaximized(Form f)
        {
            if (resolution.Height == 768)
            {
                f.WindowState = FormWindowState.Maximized;
                return true;
            }

            return false;
        }
    }
}

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Sincronizacao
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NotifyIcon ni = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Text = "Sincronização Emiplus"
            };

            Application.Run(new Sync());
        }
    }
}

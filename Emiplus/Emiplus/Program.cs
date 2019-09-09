using System;
using System.Windows.Forms;

namespace Emiplus
{
    using Data.Helpers;
    using Emiplus.View.Comercial;
    using View.Common;

    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Support().AddFont();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AddClientes());
        }
    }
}
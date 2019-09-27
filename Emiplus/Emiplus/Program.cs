namespace Emiplus
{
    using System;
    using System.Windows.Forms;
    using View.Common;

    using Emiplus.Data.Core;

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
            Application.Run(new Carregar());
        }
    }
}
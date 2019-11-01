namespace Emiplus
{
    using Emiplus.View.Financeiro;
    using Emiplus.View.Testes;
    using System;
    using System.Windows.Forms;
    using View.Common;

    static class Program
    {
        public static string URL_BASE = "https://www.emiplus.com.br";

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new View.Reports.Form2());
        }
    }
}
namespace Emiplus
{
    using Emiplus.View.Financeiro;
    using Emiplus.View.Testes;
    using System;
    using System.Windows.Forms;
    using View.Common;

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
            Application.Run(new Nota());
        }
    }
}
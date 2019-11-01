using Emiplus.View.Financeiro;
using Emiplus.View.Testes;
using System;
using System.Windows.Forms;
using Emiplus.View.Common;
using System.IO;

namespace Emiplus
{
    static class Program
    {
        public static string URL_BASE = "https://www.emiplus.com.br";
        public static string PATH_BASE { get; set; }

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string workingDirectory = Environment.CurrentDirectory;
            PATH_BASE = Directory.GetParent(workingDirectory).Parent.FullName;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Carregar());
        }
    }
}
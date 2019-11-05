using Emiplus.View.Financeiro;
using Emiplus.View.Testes;
using System;
using System.Windows.Forms;
using Emiplus.View.Common;
using System.IO;
using Emiplus.Properties;

namespace Emiplus
{
    static class Program
    {
        //public static string URL_BASE = "http://localhost/app";
        public static string URL_BASE = "https://www.emiplus.com.br";
        public static string PATH_BASE { get; set; }
        public static string UNIQUE_ID_EMPRESA = Settings.Default.empresa_unique_id;

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
            Application.Run(new Home());
        }
    }
}
using System;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Emiplus.View.Produtos;

namespace Emiplus
{
    using View.Common;
    using Data.Helpers;
    
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
            Application.Run(new Home());
        }
    }
}
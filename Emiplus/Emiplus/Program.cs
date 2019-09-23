﻿using System;
using System.Windows.Forms;

namespace Emiplus
{
    using Data.Helpers;
    using Emiplus.View.Produtos;
    using Emiplus.View.Testes;
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
            Application.Run(new Form5());
        }
    }
}
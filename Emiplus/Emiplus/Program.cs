namespace Emiplus
{
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using View.Common;
    using System.Linq;
    using SqlKata.Execution;
    using System.Diagnostics;

    static class Config
    {
        /// <summary>
        /// Retorna de forma dinamica configs do DB, atribuindo na key o seu devido valor.
        /// </summary>
        public static string Get(string key)
        {   
            var data = new Model.Config().FindAll().Where("config_key", key).First();
            return data.CONFIG_VALUE;
        }
    }

    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine(Config.Get("version"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new View.Financeiro.Nota());
        }
    }
}
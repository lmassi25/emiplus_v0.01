using Emiplus.View.Financeiro;
using Emiplus.View.Testes;
using System;
using System.Windows.Forms;
using Emiplus.View.Common;
using System.IO;
using Emiplus.Properties;
using Emiplus.View.Comercial;
using System.Collections;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;

namespace Emiplus
{
    static class Program
    {
        //public static string URL_BASE = "http://localhost/app";
        public static string URL_BASE = "https://www.emiplus.com.br";
        public static string PATH_BASE { get; set; }
        public static string UNIQUE_ID_EMPRESA = Settings.Default.empresa_unique_id;
        public static string TOKEN = "f012622defec1e2bad3b8596e0642c";
        public static ArrayList userPermissions = new ArrayList();

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            userPermissions.Clear();
            
            string workingDirectory = Environment.CurrentDirectory;
            PATH_BASE = Directory.GetParent(workingDirectory).Parent.FullName;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Carregar());
        }

        public static void SetPermissions()
        {
            var jo = new RequestApi().URL($"{Program.URL_BASE}/api/permissions/{Program.TOKEN}/{Settings.Default.user_id}").Content().Response();

            if (jo["error"] != null && jo["error"].ToString() != "")
            {
                Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                return;
            }

            if (string.IsNullOrEmpty(jo["telas"].ToString()))
                Program.userPermissions.Add(new { all = 1 });
            else
            {
                foreach (dynamic item in jo["telas"])
                    Program.userPermissions.Add(new { Key = item.Name, Value = item.Value.ToString() });
            }
        }
    }
}
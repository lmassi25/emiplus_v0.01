using System;
using System.Windows.Forms;
using Emiplus.View.Common;
using Emiplus.Properties;
using System.Collections;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using System.Threading;
using System.Diagnostics;
using RestSharp;
using Newtonsoft.Json;
using System.Text;
using Emiplus.View.Testes;
using System.Globalization;
using System.Net;

namespace Emiplus
{
    static class Program
    {
        public static string URL_BASE = "https://www.emiplus.com.br";
        //public static string URL_BASE = "http://localhost/app";
        public static CultureInfo cultura = new CultureInfo("pt-BR");

        /// <summary>
        /// Caminho definido no Config.ini
        /// </summary>
        public static string PATH_BASE { get; set; }

        /// <summary>
        /// ID unico para cada CNPJ
        /// </summary>
        public static string UNIQUE_ID_EMPRESA = Settings.Default.empresa_unique_id;

        /// <summary>
        /// Token de autenticação para API
        /// </summary>
        public static string TOKEN = "f012622defec1e2bad3b8596e0642c";
        public static ArrayList userPermissions = new ArrayList();

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
       [STAThread]
        public static void Main()
        {
            userPermissions.Clear();
            PATH_BASE = IniFile.Read("Path", "LOCAL");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            Application.ThreadException += new ThreadExceptionEventHandler(CustomExceptionHandler.OnThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Carregar());
        }

        public static void SetPermissions()
        {
            if (Support.CheckForInternetConnection())
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

                var data = JsonConvert.SerializeObject(userPermissions);

                //gravando informação em um arquivo na pasta raiz do executavel
                System.IO.StreamWriter writerJson = System.IO.File.CreateText($".\\P{Settings.Default.user_id}.json");
                writerJson.Write(data);
                writerJson.Flush();
                writerJson.Dispose();
            } 
            else
            {
                String dataJson = System.IO.File.ReadAllText($".\\P{Settings.Default.user_id}.json", Encoding.UTF8);
                userPermissions = JsonConvert.DeserializeObject<ArrayList>(dataJson);
            }
        }
    }

    internal class CustomExceptionHandler
    {
        public static void OnThreadException(object sender, ThreadExceptionEventArgs t)
        {
            Exception e = t.Exception;
            new Log().Add("EXCEPTIONS", e.GetBaseException().ToString() + Environment.NewLine + "######################################", Log.LogType.fatal);
            
            if (Support.CheckForInternetConnection())
            {
                object obj = new
                {
                    token = Program.TOKEN,
                    usuario = Settings.Default.user_name + " " + Settings.Default.user_lastname,
                    empresa = Settings.Default.empresa_razao_social,
                    name = e.GetType().Name.ToString(),
                    error = e.ToString(),
                    message = e.Message.ToString()
                };
                new RequestApi().URL(Program.URL_BASE + "/api/error").Content(obj, Method.POST).Response();
            }

            Error.errorMessage = e.Message.ToString();
            Error result = new Error();
            // Exit the program when the user clicks Abort.
            if (result.ShowDialog() == DialogResult.OK)
                Application.Exit();
        }
    }
}
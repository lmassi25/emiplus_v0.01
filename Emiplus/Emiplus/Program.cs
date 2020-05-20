using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Newtonsoft.Json;
using RestSharp;

namespace Emiplus
{
    internal static class Program
    {
        //public static string URL_BASE = "https://www.emiplus.com.br";

        public static string URL_BASE = "http://localhost/app";
        public static CultureInfo cultura = new CultureInfo("pt-BR");

        /// <summary>
        ///     ID unico para cada CNPJ
        /// </summary>
        public static string UNIQUE_ID_EMPRESA = Settings.Default.empresa_unique_id;

        /// <summary>
        ///     Token de autenticação para API
        /// </summary>
        public static string TOKEN = "f012622defec1e2bad3b8596e0642c";

        public static ArrayList UserPermissions = new ArrayList();

        /// <summary>
        ///     Caminho definido no Config.ini
        /// </summary>
        public static string PATH_BASE { get; set; }

        public static string IP_REMOTO { get; set; }
        public static string PATH_IMAGE { get; set; }

        /// <summary>
        ///     Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            UserPermissions.Clear();
            PATH_BASE = IniFile.Read("Path", "LOCAL");
            IP_REMOTO = IniFile.Read("Remoto", "LOCAL");
            PATH_IMAGE = string.IsNullOrEmpty(IP_REMOTO) ? $"{PATH_BASE}" : $@"{IP_REMOTO}\Emiplus";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            Application.ThreadException += CustomExceptionHandler.OnThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Carregar());
        }

        public static void SetPermissions()
        {
            if (Support.CheckForInternetConnection())
            {
                var jo = new RequestApi().URL($"{URL_BASE}/api/permissions/{TOKEN}/{Settings.Default.user_id}")
                    .Content().Response();

                if (jo["error"] != null && jo["error"].ToString() != "")
                {
                    Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                if (string.IsNullOrEmpty(jo["telas"]?.ToString()))
                    UserPermissions.Add(new {all = 1});
                else
                    foreach (dynamic item in jo["telas"])
                        UserPermissions.Add(new {Key = item.Name, Value = item.Value.ToString()});

                var data = JsonConvert.SerializeObject(UserPermissions);

                //gravando informação em um arquivo na pasta raiz do executavel
                var writerJson = File.CreateText($".\\P{Settings.Default.user_id}.json");
                writerJson.Write(data);
                writerJson.Flush();
                writerJson.Dispose();
            }
            else
            {
                var dataJson = File.ReadAllText($".\\P{Settings.Default.user_id}.json", Encoding.UTF8);
                UserPermissions = JsonConvert.DeserializeObject<ArrayList>(dataJson);
            }
        }
    }

    internal class CustomExceptionHandler
    {
        public static void OnThreadException(object sender, ThreadExceptionEventArgs t)
        {
            var e = t.Exception;
            new Log().Add("EXCEPTIONS",
                e.GetBaseException() + Environment.NewLine + "######################################",
                Log.LogType.fatal);

            if (Support.CheckForInternetConnection())
            {
                object obj = new
                {
                    token = Program.TOKEN,
                    usuario = Settings.Default.user_name + " " + Settings.Default.user_lastname,
                    empresa = Settings.Default.empresa_razao_social,
                    name = e.GetType().Name,
                    error = e.ToString(),
                    message = e.Message
                };
                new RequestApi().URL(Program.URL_BASE + "/api/error").Content(obj, Method.POST).Response();
            }

            Error.ErrorMessage = e.Message;
            var result = new Error();
            // Exit the program when the user clicks Abort.
            if (result.ShowDialog() == DialogResult.OK)
                Application.Exit();
        }
    }
}
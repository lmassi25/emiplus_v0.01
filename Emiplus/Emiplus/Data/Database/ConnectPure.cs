using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using FirebirdSql.Data.FirebirdClient;
using System;

namespace Emiplus.Data.Database
{
    internal class ConnectPure
    {
        public static string _path { get; set; }
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        protected ConnectPure()
        {
        }

        private static string GetDatabase()
        {
            return _path = IniFile.Read("PathDatabase", "LOCAL") + "\\EMIPLUS.FDB";
        }

        /// <summary>
        /// Método responsável por abrir a conexão com Banco de Dados
        /// </summary>
        protected static FbConnection Connection()
        {
            FbConnection conexao = null;

            try
            {
                conexao = new FbConnection(
                    $"character set=NONE;initial catalog={GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
                );

                return conexao;
            }
            catch (Exception ex)
            {
                Log Log = new Log();
                Log.Add("ConnectPure", ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
            }

            return conexao;
        }
    }
}
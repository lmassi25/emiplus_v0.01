using FirebirdSql.Data.FirebirdClient;
using Emiplus.Data.Helpers;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.IO;
using Emiplus.Data.Core;
using MySql.Data.MySqlClient;

namespace Emiplus.Data.Database
{
    class ConnectOnline
    {
        public string _path { get; set; }
        public bool update { get; set; }
        private const string _user = "root";
        private const string _pass = "";
        private const string _db = "base";
        private const string _host = "localhost";
        private const string _port = "3306";

        public QueryFactory Open()
        {
            var connection = new MySqlConnection(
                $"Host={_host};Port={_port};User={_user};Password={_pass};Database={_db};SslMode=None;Convert Zero Datetime=True"
            );

            var db = new QueryFactory(connection, new MySqlCompiler());

            db.Logger = compiled =>
            {
                Log Log = new Log();
                Log.Add("LOGGER", "Query: " + compiled.ToString(), Log.LogType.fatal);

                System.Console.WriteLine(compiled.ToString());
            };

            return db;
        }
    }
}
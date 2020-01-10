using FirebirdSql.Data.FirebirdClient;
using Emiplus.Data.Helpers;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.IO;
using Emiplus.Data.Core;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Emiplus.Data.Database
{
    class ConnectOnline
    {
        public string _path { get; set; }
        public bool update { get; set; }
        private const string _user = "root";
        private const string _pass = "mwAKo00I964wl1z8";
        private const string _db = "base";
        private const string _host = "34.95.185.27";
        private const string _port = "3306";

        public QueryFactory Open()
        {
            //var csb = new MySqlConnectionStringBuilder
            //{
            //    Server = _host,
            //    UserID = _user,
            //    Password = _pass,
            //    Database = _db,
            //    SslCert = @"C:\Users\Destech\Desktop\certificado google cloud\client-cert.pem",
            //    SslKey = @"C:\Users\Destech\Desktop\certificado google cloud\client-key.pem",
            //    SslCa = @"C:\Users\Destech\Desktop\certificado google cloud\server-ca.pem",
            //    CertificateFile = @"C:\Users\Destech\Desktop\certificado google cloud\client.pfx",
            //    SslMode = MySqlSslMode.Required,
            //    CertificateStoreLocation = MySqlCertificateStoreLocation.CurrentUser,
            //    Port = 3306,
            //    CertificatePassword = "1234"
            //};

            var connection = new MySqlConnection(
                $"Host={_host};Port={_port};user={_user};Password={_pass};Database={_db};SslMode=Required;Convert Zero Datetime=True;CertificateFile=C:\\Users\\Destech\\Desktop\\certificado google cloud\\client.pfx;CertificatePassword=1234;"
            );
            //var connection = new MySqlConnection(csb.ConnectionString);

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
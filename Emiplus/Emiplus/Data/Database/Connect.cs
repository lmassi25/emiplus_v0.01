using FirebirdSql.Data.FirebirdClient;
using Emiplus.Data.Helpers;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.IO;
using Emiplus.Data.Core;

namespace Emiplus.Data.Database
{
    class Connect
    {
        public string _path { get; set; }
        public bool update { get; set; } = false;
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        private string GetDatabase()
        {
            if (update)
            {
                if (File.Exists(Directory.GetCurrentDirectory() + @"\Update\PADRAO.fdb"))
                {
                    return _path = Directory.GetCurrentDirectory() + @"\Update\PADRAO.fdb";
                }

                if (File.Exists("C:\\Emiplus\\Update\\PADRAO.fdb"))
                {
                    return _path = "C:\\Emiplus\\Update\\PADRAO.fdb";
                }                
            }

            return _path = IniFile.Read("Path", "LOCAL") + "\\EMIPLUS.FDB";
        }

        public QueryFactory Open()
        {
            var connection = new FbConnection(
                $"character set=NONE;initial catalog={GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
            );

            var compiler = new FirebirdCompiler();
            var db = new QueryFactory(connection, compiler);

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
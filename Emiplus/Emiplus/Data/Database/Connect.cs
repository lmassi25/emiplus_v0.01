using Emiplus.Data.Helpers;
using FirebirdSql.Data.FirebirdClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.IO;

namespace Emiplus.Data.Database
{
    class Connect
    {
        private string _path = @"C:\emiplus_v0.01\EMIPLUS.FDB";
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        private string GetDatabase()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\DATABASE.txt"))
            {
                _path = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\DATABASE.txt");
            }

            if (File.Exists(_path))
            {
                return _path;
            }

            if (File.Exists(Directory.GetCurrentDirectory() + "\\EMIPLUS.FDB"))
            {
                return Directory.GetCurrentDirectory() + "\\EMIPLUS.FDB";
            }

            return _path;
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
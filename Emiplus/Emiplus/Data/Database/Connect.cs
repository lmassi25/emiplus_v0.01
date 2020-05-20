using Emiplus.Data.Core;
using FirebirdSql.Data.FirebirdClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.IO;

namespace Emiplus.Data.Database
{
    internal class Connect
    {
        public string _path { get; set; }
        public bool update { get; set; }
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        public string GetDatabase()
        {
            if (update)
            {
                if (File.Exists(IniFile.Read("Path", "LOCAL") + "\\Update\\PADRAO.fdb"))
                {
                    return _path = IniFile.Read("Path", "LOCAL") + "\\Update\\PADRAO.fdb";
                }

                if (File.Exists(Directory.GetCurrentDirectory() + @"\Update\PADRAO.fdb"))
                {
                    return _path = Directory.GetCurrentDirectory() + @"\Update\PADRAO.fdb";
                }
            }

            return _path = IniFile.Read("PathDatabase", "LOCAL") + "\\EMIPLUS.FDB";
        }

        public QueryFactory Open()
        {
            var connection = new FbConnection(
                $"character set=NONE;initial catalog={GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
            );

            var compiler = new FirebirdCompiler();
            var db = new QueryFactory(connection, compiler)
            {
                Logger = compiled => { System.Console.WriteLine(compiled.ToString()); }
            };

            return db;
        }
    }
}
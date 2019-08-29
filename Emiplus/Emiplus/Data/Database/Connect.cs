using FirebirdSql.Data.FirebirdClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Emiplus.Data.Database
{
    class Connect
    {
        private const string _path = @"C:\emiplus_v0.01\EMIPLUS.FDB";
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        public QueryFactory Open()
        {
            var connection = new FbConnection(
                $"character set=NONE;initial catalog={_path};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
            );

            var compiler = new FirebirdCompiler();
            var db = new QueryFactory(connection, compiler);
            return db;
        }
    }
}

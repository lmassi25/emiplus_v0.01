using FirebirdSql.Data.FirebirdClient;
using Emiplus.Data.Helpers;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.IO;
using Emiplus.Data.Core;
using System.Data;
using System;
using System.Windows.Forms;

namespace Emiplus.Data.Database
{
    class ConnectPure
    {
        public static string _path { get; set; }
        private const string _user = "sysdba";
        private const string _pass = "masterkey";
        private const string _db = "sysdba";
        private const string _host = "localhost";

        private static FbConnection conn;


        private FbConnection SQLCon;

        private ConnectPure() { }

        private static string GetDatabase()
        {
            return _path = IniFile.Read("PathDatabase", "LOCAL") + "\\EMIPLUS.FDB";
        }

        public void Connect()
        {
            //FbConnection SQLCon = new FbConnection(
            //    $"character set=NONE;initial catalog=C:\\Emiplus\\EMIPLUS.fdb;user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
            //);

            //FbCommand cmd;
            //FbDataReader res;
            //cmd = new FbCommand(, SQLCon);
            //cmd.ExecuteNonQuery();

            //ExecuteReader
        }

        //public void Open()
        //{
        //    SQLCon.Open();
        //    //Log Log = new Log();
        //    //Log.Add("Connect", "Path: " + GetDatabase(), Log.LogType.fatal);

        //    //var connection = new FbConnection(
        //    //    $"character set=NONE;initial catalog={GetDatabase()};user id={_user};data source={_host};user id={_db};Password={_pass};Pooling=true;Dialect=3"
        //    //);

        //    //var compiler = new FirebirdCompiler();
        //    //var db = new QueryFactory(connection, compiler);

        //    //db.Logger = compiled =>
        //    //{
        //    //    Log.Add("LOGGER", "Query: " + compiled.ToString(), Log.LogType.fatal);

        //    //    System.Console.WriteLine(compiled.ToString());
        //    //};

        //    //return db;
        //}

        /// <summary>
        /// Método responsável por abrir a conexão com Banco de Dados
        /// </summary>
        private static FbConnection Connection()
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

        /// <summary>
        /// Begin Transaction: esse comando, como o nome sugere, inicia a transação, abrindo o bloco de comandos a serem executados. 
        /// Todas as instruções que precisem ser executadas devem estar após esse comando.
        /// </summary>
        public static void Open()
        {
            if (Connection().State == ConnectionState.Closed)
            {
                conn = Connection();
                conn.Open();
            }

            conn.BeginTransaction();
        }

        public static FbConnection Get()
        {
            return conn;
        }

        /// <summary>
        /// Commit Transaction: o comando commit efetiva a transação, ou seja, persiste no banco todas as alterações efetuadas no bloco. 
        /// Após a execução do commit, não é possível reverter as modificações sofridas pelos dados na base.
        /// </summary>
        public static void Close()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();

            conn.BeginTransaction().Commit();
            conn = null;
        }

        /// <summary>
        /// Rollback Transaction: contrário ao commit, o rollback cancela a transação. 
        /// Assim, todos os comandos executados no bloco da transação são descartados e a base de dados não sofre nenhuma alteração. 
        /// Esse comando é geralmente utilizado caso ocorra algum erro na execução de uma das instruções do bloco.
        /// </summary>
        public static void RollBack()
        {
            conn.BeginTransaction().Rollback();
        }

    }
}
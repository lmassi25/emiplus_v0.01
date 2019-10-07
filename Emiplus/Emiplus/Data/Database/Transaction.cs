using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace Emiplus.Data.Database
{
    class Transaction : ConnectPure
    {
        static FbConnection conn;

        /// <summary>
        /// Begin Transaction: esse comando, como o nome sugere, inicia a transação, abrindo o bloco de comandos a serem executados. 
        /// Todas as instruções que precisem ser executadas devem estar após esse comando.
        /// </summary>
        public static void Open()
        {
            conn = Connection();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
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

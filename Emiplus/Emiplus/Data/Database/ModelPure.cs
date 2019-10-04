using Emiplus.Data.Helpers;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Data.Database
{
    //internal abstract 
    class ModelPure
    {
        protected Log Log;

        protected string Entity;
        protected object Objetos;

        public ModelPure()
        {
            

            Log = new Log();
        }

         /// <summary>
        /// Alimenta a query Create e Update com os objetos
        /// </summary>
        /// <param name="obj">Objeto com os dados(data)</param>
        /// <returns></returns>
        public ModelPure Data(object obj)
        {
            Objetos = obj;
            return this;
        }

        public void Find()
        {

        }

        public void Fetch()
        {
            try
            {
                string sql = "SELECT * FROM PESSOA";

                //using (FbCommand cmd = new FbCommand(sql, ConnectPure.Get()))
                //{
                //    ConnectPure.Open();

                //    FbDataReader readers = cmd.ExecuteReader();

                //    using (FbDataReader reader = cmd.ExecuteReader())
                //    {
                //        if (reader.Read())
                //        {
                //            var teste = reader;
                //        }
                //    }
                //    ConnectPure.Close();
                //}
                ConnectPure.Open();
                FbCommand cmd = new FbCommand(sql, ConnectPure.Get());
                
                //    ConnectPure.Open();
                    FbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                    Console.WriteLine(Convert.ToInt32(reader[0]));
                    }
                //FbDataAdapter da = new FbDataAdapter(cmd);
                    ConnectPure.Close();
                
            }
            catch (Exception ex)
            {
                Log.Add("ConnectPure", ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
            }
        }

    }
}

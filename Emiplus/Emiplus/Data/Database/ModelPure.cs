using Emiplus.Data.Helpers;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Emiplus.Data.Database
{ 
    class ModelPure
    {
        protected Log Log;

        protected string Entity;

        protected string Query;
        protected string order;

        protected Dictionary<string, string> Param;

        public ModelPure(string entity)
        {
            Entity = entity;

            Log = new Log();
        }

        /// <summary>
        /// SELECT ALL * FROM
        /// </summary>
        /// <param name="terms">Termos de WHERE</param>
        /// <param name="param">Parametros do WHERE</param>
        /// <param name="columns">Colunas para SELECT</param>
        /// <returns></returns>
        public ModelPure Find(string terms = null, string param = null, string columns = "*")
        {
            if (!string.IsNullOrEmpty(terms))
            {
                Query = $"SELECT ALL {columns} FROM {Entity} WHERE {terms}";
                Param = GetParam(param);
                return this;
            }

            Query = $"SELECT ALL {columns} FROM {Entity}";
            return this;
        }

        public FbDataReader FindById(int id, string columns = "*")
        {
            var find = Find("id = @id", $"id={id}", columns);
            return find.Fetch();
        }

        public ModelPure Order(string columnOrder)
        {
            order = $" ORDER BY {columnOrder}";
            return this;
        }

        public ModelPure Limit(int limit)
        {
            Query = Query.Replace("SELECT ALL", $"SELECT FIRST {limit} SKIP 0");
            return this;
        }

        public ModelPure Offset(int offset)
        {
            Query = Query.Replace("SKIP 0", $"SKIP {offset}");
            return this;
        }

        public FbDataReader Count(string key = "id")
        {
            var count = Find("1 = @one", "one=1", $"COUNT({key}) AS {key}");
            return count.Fetch();
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <returns></returns>
        public FbDataReader Fetch()
        {
            try
            {
                FbCommand cmd = new FbCommand(Query + order, Transaction.Get());

                if (Param != null)
                {
                    foreach (KeyValuePair<string, string> get in Param)
                    {
                        cmd.Parameters.AddWithValue($"@{get.Key}", get.Value);
                    }
                }

                FbDataReader res = cmd.ExecuteReader();
                return res;
            }
            catch (Exception ex)
            {
                Log.Add("ConnectPure", ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return null;
            }
        }

        /// <summary>
        /// Cria registro
        /// </summary>
        /// <param name="values">@tipo, @nome</param>
        /// <param name="param">tipo=Clientes&nome=Maria92</param>
        /// <returns>INT (ID)</returns>
        /// <code>Create("@tipo, @nome", "tipo=Clientes&nome=Maria92").FetchScalar()</code> 
        public ModelPure Create(string values, string param)
        {
            try
            {
                string columns = values.Replace("@", "");
                Query = $"INSERT INTO {Entity} ({columns}) VALUES ({values}) RETURNING ID;";
                Param = GetParam(param);
                return this;
            }
            catch (Exception ex)
            {
                Log.Add("ConnectPure", ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return null;
            }
        }

        /// <summary>
        /// Atualiza registro
        /// </summary>
        /// <param name="terms">ID = @id</param>
        /// <param name="values">tipo = @tipo, nome = @nome</param>
        /// <param name="param">id=26&tipo=Clientes&nome=Maria91</param>
        /// <returns></returns>
        /// <code>Update("ID = @id", "tipo = @tipo, nome = @nome", "id=26&tipo=Clientes&nome=Maria91").FetchNonQuery()</code> 
        public ModelPure Update(string terms, string values, string param)
        {
            try
            {
                Query = $"UPDATE {Entity} SET {values} WHERE {terms}";
                Param = GetParam(param);
                return this;
            }
            catch (Exception ex)
            {
                Log.Add("ConnectPure", ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return null;
            }
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <returns></returns>
        public int FetchScalar()
        {
            try
            {
                FbCommand cmd = new FbCommand(Query, Transaction.Get());

                if (Param != null)
                {
                    foreach (KeyValuePair<string, string> get in Param)
                    {
                        cmd.Parameters.AddWithValue($"@{get.Key}", get.Value);
                    }
                }

                int res = (int)cmd.ExecuteScalar();
                return res;
            }
            catch (Exception ex)
            {
                Log.Add("ConnectPure", ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return 0;
            }
        }

        /// <summary>
        /// ExecuteNonQuery()
        /// </summary>
        /// <returns>bool</returns>
        public bool FetchNonQuery()
        {
            try
            {
                FbCommand cmd = new FbCommand(Query, Transaction.Get());

                if (Param != null)
                {
                    foreach (KeyValuePair<string, string> get in Param)
                    {
                        cmd.Parameters.AddWithValue($"@{get.Key}", get.Value);
                    }
                }

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Log.Add("ConnectPure", ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return false;
            }
        }

        private Dictionary<string, string> GetParam(string parameters)
        {
            return parameters.Split('&')
                .Select(p => p.Split('='))
                .ToDictionary(p => p[0], p => p.Length > 1 ? Uri.UnescapeDataString(p[1]) : null);
        }

    }
}

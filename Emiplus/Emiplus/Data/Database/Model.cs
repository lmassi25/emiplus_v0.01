using System;
using System.Linq;
using Emiplus.Data.Helpers;
using SqlKata;
using SqlKata.Execution;

namespace Emiplus.Data.Database
{
    internal abstract class Model
    {
        protected QueryFactory Db = new Connect().Open();
        protected string Entity;
        protected Log Log;
        protected object Objetos;

        protected Model(string entity)
        {
            Entity = entity;

            Log = new Log();
        }

        public Model SetDbOnline()
        {
            Db = new ConnectOnline().Open();

            return this;
        }

        /// <summary>
        ///     Alimenta a query Create e Update com os objetos
        /// </summary>
        /// <param name="obj">Objeto com os dados(data)</param>
        /// <returns></returns>
        public Model Data(object obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties())
                if (propertyInfo.PropertyType == typeof(string))
                {
                    string[] stringArray =
                    {
                        "Tipo", "tipo", "id_empresa", "Id_empresa", "correcao", "Status", "ChaveDeAcesso",
                        "assinatura_qrcode", "senha", "email", "Nome", "Pessoatipo", "status_sync"
                    };
                    var stringToCheck = propertyInfo.Name;
                    if (!stringArray.Any(s => stringToCheck.Contains(s)))
                    {
                        var currentValue = (string) propertyInfo.GetValue(obj, null);
                        if (currentValue != null)
                            propertyInfo.SetValue(obj, currentValue.Trim().ToUpper(), null);
                    }
                }

            Objetos = obj;

            return this;
        }

        /// <summary>
        ///     Busca todos os registros
        /// </summary>
        /// <returns></returns>
        public Query FindAll(string[] columns = null)
        {
            try
            {
                columns = columns ?? new[] {"*"};
                var data = Db.Query(Entity).Select(columns);
                return data;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        public int Count()
        {
            var count = 0;
            foreach (var data in Db.Select("SELECT COUNT(ID) AS \"COUNT\" FROM " + Entity + " WHERE EXCLUIR = 0"))
                count = Validation.ConvertToInt32(data.COUNT);

            return count;
        }

        /// <summary>
        ///     Monte sua query com esse método
        /// </summary>
        /// <returns></returns>
        /// <example>
        ///     <code>
        /// Query().Select().Where().OrderBy() etc...
        /// Exemplos:
        /// Query().Get() - retorna todos registros
        /// Query().OrderBy().Get() - ordena os registros
        /// Query().WhereNull("ColunaNull").AsUpdate(Object) - Update
        /// Documentação https://sqlkata.com/docs/
        /// </code>
        /// </example>
        public Query Query()
        {
            try
            {
                var data = Db.Query(Entity);
                return data;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Buscar registro por ID
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <returns>Retorna objeto</returns>
        public Query FindById(int id)
        {
            try
            {
                var data = Db.Query(Entity).Where("ID", id);
                return data;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        public int GetLastId()
        {
            try
            {
                var idNum = 0;
                foreach (var item in Db.Select("select gen_id(GEN_" + Entity + "_ID, 0) as num from rdb$database;"))
                    idNum = Validation.ConvertToInt32(item.NUM);

                return idNum;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        public int GetNextId()
        {
            var idNum = 0;

            foreach (var item in Db.Select("select gen_id(GEN_" + Entity + "_ID, 0) as num from rdb$database;"))
                idNum = Validation.ConvertToInt32(item.NUM);

            return idNum + 1;
        }

        /// <summary>
        ///     Executa o Insert();
        /// </summary>
        /// <returns>Retorna 1 para criado e 0 para não criado.</returns>
        public int Create()
        {
            try
            {
                var data = Db.Query(Entity).Insert(Objetos);
                return data;
            }
            catch (Exception ex)
            {
                Log.Add(Entity, ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return 0;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Executa o Insert();
        /// </summary>
        /// <returns>Retorna o ID do insert.</returns>
        public int CreateGetId()
        {
            try
            {
                var data = Db.Query(Entity).InsertGetId<int>(Objetos);
                return data;
            }
            catch (Exception ex)
            {
                Log.Add(Entity, ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return 0;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Executa o Update();
        /// </summary>
        /// <param name="key">Passar ID (Key)</param>
        /// <param name="value">Passar Conteudo</param>
        /// <returns>Retorna int 1 para atualizado e 0 para não atualizado.</returns>
        public int Update(string key, int value)
        {
            try
            {
                var data = Db.Query(Entity).Where(key, value).Update(Objetos);
                return data;
            }
            catch (Exception ex)
            {
                Log.Add(Entity, ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return 0;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Executa o Delete();
        /// </summary>
        /// <param name="key">Passar ID (key) para deletar o registro</param>
        /// <param name="value">Passar Conteudo</param>
        /// <returns>Retorna int 1 para deletado e 0 para não deletado.</returns>
        public int Delete(string key, int value)
        {
            try
            {
                var data = Db.Query(Entity).Where(key, value).Delete();
                return data;
            }
            catch (Exception ex)
            {
                Log.Add(Entity, ex.Message + " | " + ex.InnerException, Log.LogType.fatal);
                return 0;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
    }
}
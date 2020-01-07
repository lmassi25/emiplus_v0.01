using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class Sync : Form
    {
        BackgroundWorker backWork = new BackgroundWorker();

        /// <summary>
        /// Acesso ao banco local
        /// </summary>
        private QueryFactory connect = new Connect().Open();

        /// <summary>
        /// Acesso ao banco online
        /// </summary>
        private QueryFactory connectOnline = new ConnectOnline().Open();

        public Sync()
        {
            InitializeComponent();
            Eventos();

            var cols = new[] { "id", "id_empresa", "tipo", "excluir", "criado", "atualizado", "deletado", "usuario", "saldo_inicial", "saldo_final", "saldo_final_informado", "observacao", "fechado", "terminal", "id_sync", "status_sync" };
            
            Console.WriteLine(Validation.RandomSecurity());

            Caixa();
        }

        /// <summary>
        /// Recupera os dados das tabelas para manipulação
        /// Função retorna os registros 'CREATE' ou 'NULL'
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        private async Task<IEnumerable<dynamic>> GetCreateDataAsync(string Table)
        {
            var baseQuery = connect.Query().Where("status_sync", "CREATE").OrWhereNull("status_sync").OrWhere("status_sync", string.Empty);

            // CREATE = criar no banco online!
            return await baseQuery.Clone().From(Table).GetAsync();
        }

        private async Task<IEnumerable<dynamic>> GetUpdateDataAsync(string Table)
        {
            var baseQuery = connect.Query().Where("status_sync", "UPDATE");

            // CREATE = criar no banco online!
            return await baseQuery.Clone().From(Table).GetAsync();
        }

        private object Columns(string Table, dynamic item)
        {
            dynamic columns = "";
            if (Table == "CAIXA")
            {
                columns = new
                {
                    id = item.ID,
                    id_empresa = item.ID_EMPRESA,
                    tipo = item.TIPO,
                    excluir = item.EXCLUIR,
                    criado = item.CRIADO,
                    atualizado = item.ATUALIZADO,
                    deletado = item.DELETADO,
                    usuario = item.USUARIO,
                    saldo_inicial = item.SALDO_INICIAL,
                    saldo_final = item.SALDO_FINAL,
                    saldo_final_informado = item.SALDO_FINAL_INFORMADO,
                    observacao = item.OBSERVACAO,
                    terminal = item.TERMINAL,
                    fechado = item.FECHADO,
                    id_sync = item.ID_SYNC,
                    status_sync = "CREATED"
                };
            }

            return columns;
        }


        #region
        private void Caixa()
        {
            string table = "CAIXA";

            // ######### CREATE #########
            var dataCreate = GetCreateDataAsync(table);
            if (dataCreate != null)
            {
                foreach (dynamic item in dataCreate.Result)
                {
                    // inserie no banco online
                    var con = connectOnline.Query(table);
                    InsertAsync(con, table, item);

                    // atualiza local (CREATE -> CREATED)
                    UpdateAsync(connect, table, item.ID, item);
                }
            }

            // ######### UPDATE #########
            var dataUpdate = GetUpdateDataAsync(table);
            if (dataUpdate != null)
            {
                foreach (dynamic item in dataUpdate.Result)
                {
                    if (CheckCreated(table, item.ID))
                    {
                        // inserie no banco online
                        var con = connectOnline.Query(table);
                        InsertAsync(con, table, item);

                        // atualiza local (UPDATE -> CREATED)
                        UpdateAsync(connect, table, item.ID, item);
                    }

                    // atualiza online (UPDATE -> CREATED)
                    UpdateAsync(connectOnline, table, item.ID, item);

                    // atualiza local (UPDATE -> CREATED)
                    UpdateAsync(connect, table, item.ID, item);
                }
            }
        }

        private async Task UpdateAsync(QueryFactory con, string table, int id, object item)
        {
            await con.Query(table).Where("id", id).UpdateAsync(Columns(table, item));
        }
        #endregion

        /// <summary>
        /// Insere os dados no banco Online
        /// </summary>
        /// <param name="con">passe a conexão com o DB online</param>
        /// <param name="table">table do banco</param>
        /// <param name="item">object para ser inserido</param>
        /// <returns></returns>
        private async Task InsertAsync(SqlKata.Query con, string table, object item)
        {
            await con.InsertAsync(Columns(table, item));
        }

        /// <summary>
        /// Checa se o 'item' já foi inserido no banco online, true = não existe, false = já existe no banco
        /// </summary>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CheckCreated(string table, int id)
        {
            var check = connectOnline.Query(table).Where("id", id).FirstOrDefault();
            if (check == null)
                return true;

            return false;
        }

        private void Eventos()
        {
            //GetCaixaAsync();

            Shown += (s, e) =>
            {
                backWork.RunWorkerAsync();
            };

            backWork.DoWork += (s, e) =>
            {

            };

            backWork.RunWorkerCompleted += (s, e) =>
            {

            };
        }
    }
}

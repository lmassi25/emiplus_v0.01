using Emiplus.Data.Core;
using Emiplus.Data.Database;
using SqlKata.Execution;
using System.Collections.Generic;
using System.ComponentModel;
using RestSharp;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Properties;
using Newtonsoft.Json;
using Emiplus.Data.Helpers;
using System.Threading;

namespace Emiplus.View.Common
{
    public partial class Sync : Form
    {
        BackgroundWorker backWork = new BackgroundWorker();

        /// <summary>
        /// Acesso ao banco local
        /// </summary>
        private QueryFactory connect = new Connect().Open();

        public Sync()
        {
            InitializeComponent();

            if (Support.CheckForInternetConnection())
                Eventos();
        }

        private async Task RunSyncAsync(string table)
        {
            // ######### CREATE #########
            var dataCreate = await GetCreateDataAsync(table);
            if (dataCreate != null)
            {
                foreach (dynamic item in dataCreate)
                {
                    // inserie no banco online
                    dynamic obj = new
                    {
                        token = Program.TOKEN,
                        id_empresa = Settings.Default.empresa_unique_id,
                        data = JsonConvert.SerializeObject(item),
                        status_sync = "CREATED"
                    };

                    var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/create").Content(obj, Method.POST).Response();
                    if (response["status"] == "OK")
                        await UpdateAsync(table, item.ID_SYNC); // atualiza local (CREATE -> CREATED)
                }
            }

            // ######### UPDATE #########
            var dataUpdate = await GetUpdateDataAsync(table);
            if (dataUpdate != null)
            {
                foreach (dynamic item in dataUpdate)
                {
                    if (CheckCreated(table, item.ID_SYNC))
                    {
                        // inserie no banco online
                        dynamic obj = new
                        {
                            token = Program.TOKEN,
                            id_empresa = Settings.Default.empresa_unique_id,
                            data = JsonConvert.SerializeObject(item),
                            status_sync = "CREATED"
                        };

                        var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/create").Content(obj, Method.POST).Response();
                        if (response["status"] == "OK")
                            await UpdateAsync(table, item.ID_SYNC); // atualiza local (CREATE -> CREATED)
                    }

                    // atualiza online (UPDATE -> CREATED)
                    if (UpdateOnline(table, item.ID_SYNC, item))
                        await UpdateAsync(table, item.ID_SYNC);  // atualiza local (UPDATE -> CREATED)
                }
            }
        }

        #region Metodos geral
        /// <summary>
        /// Recupera os dados das tabelas do sistema local para manipulação
        /// Função retorna os registros 'CREATE' ou 'NULL'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetCreateDataAsync(string Table)
        {
            var baseQuery = connect.Query().Where("status_sync", "CREATE").OrWhereNull("status_sync").OrWhere("status_sync", string.Empty).WhereNotNull("id_sync");

            return await baseQuery.Clone().From(Table).GetAsync();
        }

        /// <summary>
        /// Recupera os dados das tabelas do sistema local para manipulação
        /// Função retorna os registros 'UPDATE'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetUpdateDataAsync(string Table)
        {
            var baseQuery = connect.Query().Where("status_sync", "UPDATE").WhereNotNull("id_sync");

            return await baseQuery.Clone().From(Table).GetAsync();
        }

        /// <summary>
        /// Atualiza o registro local, UPDATE -> CREATED
        /// </summary>
        ///  private async Task UpdateAsync(string table, int id, object item) => await connect.Query(table).Where("id", id).UpdateAsync(Columns(table, item));
        private async Task UpdateAsync(string table, int id) => await connect.Query(table).Where("id_sync", id).UpdateAsync(new { status_sync = "CREATED" });

        /// <summary>
        /// Atualiza os dados no banco online
        /// </summary>
        private bool UpdateOnline(string table, int id, dynamic item)
        {
            dynamic obj = new
            {
                token = Program.TOKEN,
                id_empresa = Settings.Default.empresa_unique_id,
                data = JsonConvert.SerializeObject(item),
                status_sync = "CREATED"
            };

            var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table}/update/{id}").Content(obj, Method.POST).Response();
            if (response["status"] == "OK")
                return true;

            return false;
        }

        /// <summary>
        /// Checa se o 'item' já foi inserido no banco online, true = não existe, false = já existe no banco
        /// </summary>
        private bool CheckCreated(string table, int id)
        {
            var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table}/get/{Program.TOKEN}/{Settings.Default.empresa_unique_id}/{id}").Content().Response();
            if (response["status"].ToString() == "FAIL")
                return true;

            return false;
        }
        #endregion

        private void Eventos()
        {
            Load += (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                {
                    if (!string.IsNullOrEmpty(Settings.Default.user_dbhost))
                        timer1.Start();
                }
            };

            timer1.Tick += (s, e) =>
            {
                backWork.RunWorkerAsync();
                Home.syncActive = true;
                timer1.Stop();
            };

            backWork.DoWork += async (s, e) =>
            {
                await RunSyncAsync("categoria");
                await RunSyncAsync("caixa_mov");
                await RunSyncAsync("categoria");
                // await RunSyncAsync("etiqueta");
                // await RunSyncAsync("formapgto");
                await RunSyncAsync("imposto");
                await RunSyncAsync("item");
                await RunSyncAsync("item_mov_estoque");
                await RunSyncAsync("natureza");
                await RunSyncAsync("pessoa");
                await RunSyncAsync("pessoa_contato");
                await RunSyncAsync("pessoa_endereco");
                await RunSyncAsync("titulo");
                await RunSyncAsync("nota");
                await RunSyncAsync("pedido");
                await RunSyncAsync("pedido_item");
            };

            backWork.RunWorkerCompleted += (s, e) =>
            {

                new Log().Add("SYNC", "Sincronização", Log.LogType.fatal);

                timer1.Enabled = true;
                timer1.Start();
                Home.syncActive = false;
            };
        }
    }
}

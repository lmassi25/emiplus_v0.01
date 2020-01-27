using Emiplus.Data.Core;
using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Newtonsoft.Json;
using RestSharp;
using SqlKata.Execution;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class Sync : Form
    {
        private BackgroundWorker backWork = new BackgroundWorker();

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
            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("status_sync", "CREATE");

            return await baseQuery.Clone().From(Table).GetAsync();
        }

        /// <summary>
        /// Recupera os dados das tabelas do sistema local para manipulação
        /// Função retorna os registros 'UPDATE'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetUpdateDataAsync(string Table)
        {
            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("status_sync", "UPDATE");

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

            var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/update/{id}").Content(obj, Method.POST).Response();
            if (response["status"] == "OK")
                return true;

            return false;
        }

        /// <summary>
        /// Checa se o 'item' já foi inserido no banco online, true = não existe, false = já existe no banco
        /// </summary>
        private bool CheckCreated(string table, int id)
        {
            var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/get/{Program.TOKEN}/{Settings.Default.empresa_unique_id}/{id}").Content().Response();
            if (response["status"].ToString() == "FAIL")
                return true;

            return false;
        }

        #endregion Metodos geral

        private void SendNota()
        {
            if (string.IsNullOrEmpty(IniFile.Read("encodeNS", "DEV")))
                return;

            if (!Directory.Exists(IniFile.Read("Path", "LOCAL") + "\\Autorizadas\\temp"))
                return;

            DirectoryInfo d = new DirectoryInfo(IniFile.Read("Path", "LOCAL") + "\\Autorizadas\\temp");

            foreach (var file in d.GetFiles("*.xml"))
            {
                var contentXml = File.ReadAllLines(file.FullName);

                dynamic obj = new
                {
                    xml = contentXml[0]
                };

                var json = new RequestApi().URL("https://app.notasegura.com.br/api/invoices?token=f278b338e853ed759383cca7da6dcf22e1c61301")
                    .Content(obj, Method.POST)
                    .AddHeader("Authorization", $"Basic {IniFile.Read("encodeNS", "DEV")}")
                    .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                    .Response();

                File.Delete(file.FullName);
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                {
                    SendNota();

                    if (!string.IsNullOrEmpty(Settings.Default.user_dbhost))
                        timer1.Start();
                }
            };

            timer1.Tick += (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                {
                    if (!string.IsNullOrEmpty(Settings.Default.user_dbhost))
                    {
                        backWork.RunWorkerAsync();
                        Home.syncActive = true;
                    }
                }

                timer1.Stop();
            };

            backWork.DoWork += async (s, e) =>
            {
                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("categoria");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("caixa_mov");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("categoria");

                // await RunSyncAsync("etiqueta");
                // await RunSyncAsync("formapgto");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("imposto");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("item");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("item_mov_estoque");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("natureza");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("pessoa");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("pessoa_contato");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("pessoa_endereco");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("titulo");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("nota");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("pedido");

                if (Support.CheckForInternetConnection())
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
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

        public static bool Remessa { get; set; }

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
                    if (response["status"] == "OK") {
                        await UpdateAsync(table, item.ID_SYNC); // atualiza local (CREATE -> CREATED)
                    } else
                        new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}", Log.LogType.fatal);
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
                        else
                            new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}", Log.LogType.fatal);
                    }

                    // atualiza online (UPDATE -> CREATED)
                    if (UpdateOnline(table, item.ID_SYNC, item))
                        await UpdateAsync(table, item.ID_SYNC);  // atualiza local (UPDATE -> CREATED)
                }
            }

            var dataCreated = await GetCreatedDataAsync(table);
            if (dataCreated != null)
            {
                foreach (dynamic item in dataCreated)
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
                        if (response["status"] == "FAIL")
                            new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}", Log.LogType.fatal);
                    }
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

            if (Remessa && Table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && Table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            return await baseQuery.Clone().From(Table).GetAsync();
        }

        /// <summary>
        /// Recupera os dados das tabelas do sistema local para manipulação
        /// Função retorna os registros 'CREATED' ou 'NULL'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetCreatedDataAsync(string Table)
        {
            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("status_sync", "CREATED");

            if (Remessa && Table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && Table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            return await baseQuery.Clone().From(Table).GetAsync();
        }

        /// <summary>
        /// Recupera os dados das tabelas do sistema local para manipulação
        /// Função retorna os registros 'UPDATE'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetUpdateDataAsync(string Table)
        {
            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("status_sync", "UPDATE");

            if (Remessa && Table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && Table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            return await baseQuery.Clone().From(Table).GetAsync();
        }

        /// <summary>
        /// Atualiza o registro local, UPDATE -> CREATED
        /// </summary>
        ///  private async Task UpdateAsync(string table, int id, object item) => await connect.Query(table).Where("id", id).UpdateAsync(Columns(table, item));
        private async Task UpdateAsync(string table, int id)
        {
            await connect.Query(table).Where("id_sync", id).UpdateAsync(new { status_sync = "CREATED" });
        }

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

            new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}", Log.LogType.fatal);
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

        private void LastSync()
        {
            dynamic obj = new
            {
                token = Program.TOKEN,
                id_empresa = Settings.Default.empresa_unique_id
            };

            new RequestApi().URL(Program.URL_BASE + $"/api/lastsync").Content(obj, Method.POST).Response();
        }

        public async Task SendRemessa()
        {
            var baseQueryEnviando = connect.Query().Where("id_empresa", "!=", "").Where("campoa", "ENVIANDO");
            var dataUpdateEnviando = await baseQueryEnviando.Clone().From("pedido").GetAsync();
            if (dataUpdateEnviando != null)
            {
                foreach (dynamic item in dataUpdateEnviando)
                {
                    int idPedido = item.ID;
                    Model.Pedido update = new Model.Pedido().FindById(idPedido).FirstOrDefault<Model.Pedido>();
                    update.campoa = "ENVIADO";
                    update.Save(update);
                }
            }

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("pedido");

            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("campoa", "ENVIADO");
            var dataUpdate = await baseQuery.Clone().From("pedido").GetAsync();
            if (dataUpdate != null)
            {
                foreach (dynamic item in dataUpdate)
                {
                    dynamic response = new RequestApi().URL(Program.URL_BASE + $"/api/pedido/get/{Program.TOKEN}/{Settings.Default.empresa_unique_id}/{item.ID_SYNC}").Content().Response();
                    if (response["status"].ToString() == "OK")
                    {
                        if (response.data.campoa == "RECEBIDO")
                        {
                            int idPedido = item.ID;
                            Model.Pedido update = new Model.Pedido().FindById(idPedido).FirstOrDefault<Model.Pedido>();
                            update.campoa = "RECEBIDO";
                            update.Save(update);
                        }
                        else
                        {
                            dynamic obj = new
                            {
                                token = Program.TOKEN,
                                id_empresa = Settings.Default.empresa_unique_id
                            };

                            // atualiza online (UPDATE -> CREATED)
                            var responseP = new RequestApi().URL(Program.URL_BASE + $"/api/pedido/updateRemessa/{item.ID_SYNC}/ENVIADO").Content(obj, Method.POST).Response();
                            if (responseP["status"] != "OK")
                                new Log().Add("SYNC", $"{responseP["status"]} | Tabela: pedido - {responseP["message"]}", Log.LogType.fatal);
                        }
                    }
                }
            }

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("pedido_item");
        }

        public async Task ReceberRemessa()
        {
            var response = new RequestApi().URL(Program.URL_BASE + $"/api/pedido/remessas/{Program.TOKEN}/{Settings.Default.empresa_unique_id}").Content().Response();
            if (response["error"].ToString() == "Nenhum registro encontrado")
            {
                Alert.Message("OPPS", "Não existem remessas.", Alert.AlertType.error);
                return;
            }

            foreach (dynamic item in response)
            {
                if (string.IsNullOrEmpty(item.Value.ToString()))
                    return;

                string idEmpresa = item.Value.id_empresa;
                int idUsuario = item.Value.id_usuario;
                int idPedido = item.Value.pedido;
                int idSync = item.Value.id_sync;

                if (item.Value.itens != null)
                {
                    foreach (dynamic data in item.Value.itens)
                    {
                        string codebarras = data.Value.cean;
                        double quantidade = data.Value.quantidade;
                        Model.Item dataItem = new Model.Item().FindAll().WhereFalse("excluir").Where("codebarras", codebarras).FirstOrDefault<Model.Item>();
                        if (dataItem != null)
                        {
                            Model.ItemEstoqueMovimentacao movEstoque = new Model.ItemEstoqueMovimentacao()
                                .SetUsuario(idUsuario)
                                .SetQuantidade(quantidade)
                                .SetTipo("A")
                                .SetLocal("Remessa de estoque")
                                .SetObs($"Enviado da empresa: {idEmpresa}")
                                .SetIdPedido(idPedido)
                                .SetItem(dataItem);

                            movEstoque.Save(movEstoque);
                        }
                        else
                        {
                            Model.Item createItem = new Model.Item();
                            createItem.Id = 0;
                            createItem.Excluir = 0;
                            createItem.Tipo = "Produtos";
                            createItem.CodeBarras = codebarras;
                            createItem.Referencia = data.Value.cprod;
                            createItem.Nome = data.Value.xprod;
                            createItem.ValorCompra = data.Value.valorcompra;
                            createItem.ValorVenda = data.Value.valorvenda;
                            createItem.Ncm = data.Value.ncm;
                            createItem.ativo = 0;
                            createItem.Save(createItem);

                             Model.ItemEstoqueMovimentacao movEstoque = new Model.ItemEstoqueMovimentacao()
                                .SetUsuario(idUsuario)
                                .SetQuantidade(quantidade)
                                .SetTipo("A")
                                .SetLocal("Remessa de estoque")
                                .SetObs($"Enviado da empresa: {idEmpresa}")
                                .SetIdPedido(idPedido)
                                .SetItem(createItem);

                            movEstoque.Save(movEstoque);
                        }
                    }
                }

                dynamic obj = new
                {
                    token = Program.TOKEN,
                    id_empresa = idEmpresa
                };

                var responseP = new RequestApi().URL(Program.URL_BASE + $"/api/pedido/updateRemessa/{idSync}/RECEBIDO").Content(obj, Method.POST).Response();
                if (responseP["status"] != "OK")
                    new Log().Add("SYNC", $"{responseP["status"]} | Tabela: pedido - {responseP["message"]}", Log.LogType.fatal);
            }
        }

        public async Task StartSync()
        {
            if (Support.CheckForInternetConnection())
                await RunSyncAsync("categoria");

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("caixa");

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

            LastSync();
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
                        if (!Home.syncActive)
                        {
                            backWork.RunWorkerAsync();
                            Home.syncActive = true;
                        }
                    }
                }

                timer1.Stop();
            };

            backWork.DoWork += async (s, e) =>
            {
                await StartSync();
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
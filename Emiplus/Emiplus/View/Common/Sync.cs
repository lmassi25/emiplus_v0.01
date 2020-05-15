using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SqlKata.Execution;

namespace Emiplus.View.Common
{
    public partial class Sync : Form
    {
        private readonly BackgroundWorker backWork = new BackgroundWorker();

        /// <summary>
        ///     Acesso ao banco local
        /// </summary>
        private readonly QueryFactory connect = new Connect().Open();

        public Sync()
        {
            InitializeComponent();

            if (Support.CheckForInternetConnection())
                Eventos();
        }

        public static bool Remessa { get; set; }

        private async Task RunSyncAsync(string table)
        {
            // ######### CREATE AND UPDATE #########   
            var dataCreate = await GetCreateDataAsync(table);
            if (dataCreate.Any())
            {
                dynamic obj = new
                {
                    token = Program.TOKEN,
                    id_empresa = Settings.Default.empresa_unique_id,
                    data = JsonConvert.SerializeObject(new {items = dataCreate}),
                    status_sync = "CREATED"
                };

                var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/createJson")
                    .Content(obj, Method.POST).Response();
                if (response["status"] == "OK")
                {
                    var items = response["data"];
                    foreach (var idsync in items)
                        await UpdateAsync(table, Validation.ConvertToInt32(idsync.ToString()));
                }
                else
                    new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}", Log.LogType.fatal);
            }

            //foreach (var item in dataCreate)
            //    {
            //        // inserie no banco online
            //        dynamic obj = new
            //        {
            //            token = Program.TOKEN,
            //            id_empresa = Settings.Default.empresa_unique_id,
            //            data = JsonConvert.SerializeObject(item),
            //            status_sync = "CREATED"
            //        };

            //        var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/create")
            //            .Content(obj, Method.POST).Response();
            //        if (response["status"] == "OK")
            //            await UpdateAsync(table, item.ID_SYNC); // atualiza local (CREATE -> CREATED)
            //        else
            //            new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}",
            //                Log.LogType.fatal);
            //    }

            // ######### UPDATE #########
            //var dataUpdate = await GetUpdateDataAsync(table);
            //if (dataUpdate != null)
            //    foreach (var item in dataUpdate)
            //    {
            //        if (CheckCreated(table, item.ID_SYNC))
            //        {
            //            // inserie no banco online
            //            dynamic obj = new
            //            {
            //                token = Program.TOKEN,
            //                id_empresa = Settings.Default.empresa_unique_id,
            //                data = JsonConvert.SerializeObject(item),
            //                status_sync = "CREATED"
            //            };

            //            var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/create")
            //                .Content(obj, Method.POST).Response();
            //            if (response["status"] == "OK")
            //                await UpdateAsync(table, item.ID_SYNC); // atualiza local (CREATE -> CREATED)
            //            else
            //                new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}",
            //                    Log.LogType.fatal);
            //        }

            //        // atualiza online (UPDATE -> CREATED)
            //        if (UpdateOnline(table, item.ID_SYNC, item))
            //            await UpdateAsync(table, item.ID_SYNC); // atualiza local (UPDATE -> CREATED)
            //    }

            
                var responseC = new RequestApi()
                    .URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/getall/{Program.TOKEN}/{Settings.Default.empresa_unique_id}/created")
                    .Content().Response();
                if (responseC["status"]?.ToString() == "OK")
                {
                    if (string.IsNullOrEmpty(responseC["data"].ToString())) 
                        return;
                    
                    var dataCreated = await GetCreatedDataAsync(table);
                    if (dataCreated != null)
                        foreach (var item in dataCreated)
                        {
                            var t = responseC["data"].Children().Any(x => x["id"].Value<int>() != Validation.ConvertToInt32(item.ID));
                            if (t)
                            {
                                Console.WriteLine(Validation.ConvertToInt32(item.ID));
                            }


                            foreach (var data in responseC["data"])
                            {
                                Console.WriteLine(Validation.ConvertToInt32(data.First()["id"]));
                                Console.WriteLine(Validation.ConvertToInt32(item.ID));
                                //var t = data.First().Any(x => x["id"] != Validation.ConvertToInt32(item.ID));
                                //if (t)
                                //{
                                //    await UpdateToUpdateAsync(table, Validation.ConvertToInt32(data.First()["id"]));
                                //}

                                //if (Validation.ConvertToInt32(item.ID) != Validation.ConvertToInt32(data.First()["id"]))
                                //    await UpdateToUpdateAsync(table, Validation.ConvertToInt32(data.First()["id"]));
                            }
                        }
                }
                else
                {
                    if (responseC["status"]?.ToString() == "FAIL")
                        new Log().Add("SYNC", $"{responseC["status"]} | Tabela: {table} - {responseC["message"]}", Log.LogType.fatal);
                }


            //if (CheckCreated(table, item.ID_SYNC))
            //{
            //    // inserie no banco online
            //    dynamic obj = new
            //    {
            //        token = Program.TOKEN,
            //        id_empresa = Settings.Default.empresa_unique_id,
            //        data = JsonConvert.SerializeObject(item),
            //        status_sync = "CREATED"
            //    };

            //    var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/create")
            //        .Content(obj, Method.POST).Response();
            //    if (response["status"] == "FAIL")
            //        new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}",
            //            Log.LogType.fatal);
            //}
        }

        private void SendNota()
        {
            if (string.IsNullOrEmpty(IniFile.Read("encodeNS", "DEV")))
                return;

            if (!Directory.Exists(IniFile.Read("Path", "LOCAL") + "\\Autorizadas\\temp"))
                return;

            var d = new DirectoryInfo(IniFile.Read("Path", "LOCAL") + "\\Autorizadas\\temp");

            foreach (var file in d.GetFiles("*.xml"))
            {
                var contentXml = File.ReadAllLines(file.FullName);

                dynamic obj = new
                {
                    xml = contentXml[0]
                };

                new RequestApi()
                    .URL("https://app.notasegura.com.br/api/invoices?token=f278b338e853ed759383cca7da6dcf22e1c61301")
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

            new RequestApi().URL(Program.URL_BASE + "/api/lastsync").Content(obj, Method.POST).Response();
        }

        public async Task SendRemessa()
        {
            var baseQueryEnviando = connect.Query().Where("id_empresa", "!=", "").Where("campoa", "ENVIANDO");
            var dataUpdateEnviando = await baseQueryEnviando.Clone().From("pedido").GetAsync();
            if (dataUpdateEnviando != null)
                foreach (var item in dataUpdateEnviando)
                {
                    int idPedido = item.ID;
                    var update = new Pedido().FindById(idPedido).FirstOrDefault<Pedido>();
                    update.campoa = "ENVIADO";
                    update.Save(update);
                }

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("pedido");

            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("campoa", "ENVIADO");
            var dataUpdate = await baseQuery.Clone().From("pedido").GetAsync();
            if (dataUpdate != null)
                foreach (var item in dataUpdate)
                {
                    dynamic response = new RequestApi()
                        .URL(Program.URL_BASE +
                             $"/api/pedido/get/{Program.TOKEN}/{Settings.Default.empresa_unique_id}/{item.ID_SYNC}")
                        .Content().Response();
                    if (response["status"].ToString() == "OK")
                    {
                        if (response.data.campoa == "RECEBIDO")
                        {
                            int idPedido = item.ID;
                            var update = new Pedido().FindById(idPedido).FirstOrDefault<Pedido>();
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
                            var responseP = new RequestApi()
                                .URL(Program.URL_BASE + $"/api/pedido/updateRemessa/{item.ID_SYNC}/ENVIADO")
                                .Content(obj, Method.POST).Response();
                            if (responseP["status"] != "OK")
                                new Log().Add("SYNC",
                                    $"{responseP["status"]} | Tabela: pedido - {responseP["message"]}",
                                    Log.LogType.fatal);
                        }
                    }
                }

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("pedido_item");
        }

        public async Task ReceberRemessa()
        {
            var response = new RequestApi()
                .URL(Program.URL_BASE + $"/api/pedido/remessas/{Program.TOKEN}/{Settings.Default.empresa_unique_id}")
                .Content().Response();
            if (response["error"]?.ToString() == "Nenhum registro encontrado")
            {
                Alert.Message("OPPS", "Não existem remessas.", Alert.AlertType.error);
                return;
            }

            foreach (dynamic item in response)
            {
                if (string.IsNullOrEmpty(item.Value.ToString()))
                    return;

                string idEmpresa = item.Value.pedido.id_empresa;
                int idUsuario = item.Value.pedido.id_usuario;
                int idPedido = item.Value.pedido.id;
                int idSync = item.Value.pedido.id_sync;

                var lastId = 0;
                if (item.Value.pedido != null)
                {
                    var createPedido = new Pedido
                    {
                        Id = 0,
                        Tipo = "Remessas",
                        Excluir = 0,
                        Emissao = item.Value.pedido.emissao,
                        Cliente = item.Value.pedido.cliente,
                        Colaborador = item.Value.pedido.colaborador,
                        Total = item.Value.pedido.total,
                        Desconto = item.Value.pedido.desconto,
                        Frete = item.Value.pedido.frete,
                        Produtos = item.Value.pedido.produtos,
                        id_usuario = item.Value.pedido.id_usuario,
                        campoa = "RECEBIDO",
                        campob = item.Value.pedido.campoc,
                        Observacao = $"Remessa da empresa: {idEmpresa}"
                    };
                    createPedido.Save(createPedido);

                    lastId = createPedido.GetLastId();
                }

                if (item.Value.itens != null)
                    foreach (var data in item.Value.itens)
                    {
                        var createPedidoItem = new PedidoItem();
                        createPedidoItem.Id = 0;
                        createPedidoItem.Tipo = "Produtos";
                        createPedidoItem.Excluir = 0;
                        createPedidoItem.Pedido = lastId;

                        string codebarras = data.Value.cean;
                        double quantidade = data.Value.quantidade;
                        var dataItem = new Item().FindAll().WhereFalse("excluir").Where("codebarras", codebarras)
                            .FirstOrDefault<Item>();
                        int idItem = 0;
                        if (dataItem != null)
                        {
                            var movEstoque = new ItemEstoqueMovimentacao()
                                .SetUsuario(idUsuario)
                                .SetQuantidade(quantidade)
                                .SetTipo("A")
                                .SetLocal("Remessa de estoque")
                                .SetObs($"Enviado da empresa: {idEmpresa}")
                                .SetIdPedido(idPedido)
                                .SetItem(dataItem);

                            idItem = dataItem.Id;

                            movEstoque.Save(movEstoque);
                        }
                        else
                        {
                            var createItem = new Item
                            {
                                Id = 0,
                                Excluir = 0,
                                Tipo = "Produtos",
                                CodeBarras = codebarras,
                                Referencia = data.Value.cprod,
                                Nome = data.Value.xprod,
                                ValorCompra = data.Value.valorcompra,
                                ValorVenda = data.Value.valorvenda,
                                Ncm = data.Value.ncm,
                                ativo = 0
                            };
                            createItem.Save(createItem);

                            idItem = createItem.GetLastId();

                            var movEstoque = new ItemEstoqueMovimentacao()
                                .SetUsuario(idUsuario)
                                .SetQuantidade(quantidade)
                                .SetTipo("A")
                                .SetLocal("Remessa de estoque")
                                .SetObs($"Enviado da empresa: {idEmpresa}")
                                .SetIdPedido(idPedido)
                                .SetItem(createItem);

                            movEstoque.Save(movEstoque);
                        }

                        createPedidoItem.Item = idItem;
                        createPedidoItem.CProd = data.Value.cprod;
                        createPedidoItem.CEan = data.Value.cean;
                        createPedidoItem.xProd = data.Value.xprod;
                        createPedidoItem.Ncm = data.Value.ncm;
                        createPedidoItem.Cfop = data.Value.cfop;
                        createPedidoItem.ValorCompra = data.Value.valorcompra;
                        createPedidoItem.ValorVenda = data.Value.valorvenda;
                        createPedidoItem.Quantidade = data.Value.quantidade;
                        createPedidoItem.Medida = data.Value.medida;
                        createPedidoItem.Total = data.Value.total;
                        createPedidoItem.Desconto = data.Value.desconto;
                        createPedidoItem.DescontoItem = data.Value.descontoitem;
                        createPedidoItem.DescontoPedido = data.Value.descontopedido;
                        createPedidoItem.Frete = data.Value.frete;
                        createPedidoItem.Icms = data.Value.icms;
                        createPedidoItem.Federal = data.Value.federal;
                        createPedidoItem.Estadual = data.Value.estadual;
                        createPedidoItem.Status = data.Value.status;
                        createPedidoItem.Save(createPedidoItem, false);
                    }

                dynamic obj = new
                {
                    token = Program.TOKEN,
                    id_empresa = idEmpresa
                };

                var responseP = new RequestApi().URL(Program.URL_BASE + $"/api/pedido/updateRemessa/{idSync}/RECEBIDO")
                    .Content(obj, Method.POST).Response();
                if (responseP["status"] != "OK")
                    new Log().Add("SYNC", $"{responseP["status"]} | Tabela: pedido - {responseP["message"]}",
                        Log.LogType.fatal);
            }
        }

        public async Task StartSync()
        {
            await RunSyncAsync("item");

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("categoria");

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("caixa");

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("caixa_mov");

            if (Support.CheckForInternetConnection())
                await RunSyncAsync("categoria");

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
                    if (!string.IsNullOrEmpty(Settings.Default.user_dbhost))
                        if (!Home.syncActive)
                        {
                            backWork.RunWorkerAsync();
                            Home.syncActive = true;
                        }

                timer1.Stop();
            };

            backWork.DoWork += async (s, e) => { await StartSync(); };

            backWork.RunWorkerCompleted += (s, e) =>
            {
                new Log().Add("SYNC", "Sincronização", Log.LogType.fatal);

                timer1.Enabled = true;
                timer1.Start();
                Home.syncActive = false;
            };
        }

        #region Metodos geral

        /// <summary>
        ///     Recupera os dados das tabelas do sistema local para manipulação
        ///     Função retorna os registros 'CREATE' ou 'UPDATE'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetCreateDataAsync(string table)
        {
            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where(q => q.Where("status_sync", "CREATE").OrWhere("status_sync", "UPDATE"));
            
            if (Remessa && table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            return await baseQuery.Clone().From(table).GetAsync();
        }

        /// <summary>
        ///     Recupera os dados das tabelas do sistema local para manipulação
        ///     Função retorna os registros 'CREATED' ou 'NULL'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetCreatedDataAsync(string table)
        {
            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("status_sync", "CREATED");

            if (Remessa && table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            return await baseQuery.Clone().From(table).GetAsync();
        }

        /// <summary>
        ///     Recupera os dados das tabelas do sistema local para manipulação
        ///     Função retorna os registros 'UPDATE'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetUpdateDataAsync(string table)
        {
            var baseQuery = connect.Query().Where("id_empresa", "!=", "").Where("status_sync", "UPDATE");

            if (Remessa && table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            return await baseQuery.Clone().From(table).GetAsync();
        }

        /// <summary>
        ///     Atualiza o registro local, UPDATE -> CREATED
        /// </summary>
        /// private async Task UpdateAsync(string table, int id, object item) => await connect.Query(table).Where("id", id).UpdateAsync(Columns(table, item));
        private async Task UpdateAsync(string table, int id)
        {
            await connect.Query(table).Where("id_sync", id).UpdateAsync(new {status_sync = "CREATED"});
        }

        /// <summary>
        ///     Atualiza o registro local, CREATED -> UPDATE
        ///     Necessário para quando o registro está marcado como CREATED, mas não existe na base online
        /// </summary>
        private async Task UpdateToUpdateAsync(string table, int idsync)
        {
            await connect.Query(table).Where("id", idsync).UpdateAsync(new { status_sync = "UPDATE" });
        }

        /// <summary>
        ///     Atualiza os dados no banco online
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

            var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/update/{id}")
                .Content(obj, Method.POST).Response();
            if (response["status"] == "OK")
                return true;

            new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}", Log.LogType.fatal);
            return false;
        }

        /// <summary>
        ///     Checa se o 'item' já foi inserido no banco online, true = não existe, false = já existe no banco
        /// </summary>
        private bool CheckCreated(string table, int id)
        {
            var response = new RequestApi()
                .URL(Program.URL_BASE +
                     $"/api/{table.Replace("_", "")}/get/{Program.TOKEN}/{Settings.Default.empresa_unique_id}/{id}")
                .Content().Response();
            if (response["status"]?.ToString() == "FAIL")
                return true;

            return false;
        }

        /// <summary>
        /// Retorna todos os registros
        /// </summary>
        /// <param name="table"></param>
        /// <param name="status">CREATE, UPDATE ou CREATED</param>
        /// <returns></returns>
        private bool GetAllJson(string table, string status)
        {
            var response = new RequestApi()
                .URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/getall/{Program.TOKEN}/{Settings.Default.empresa_unique_id}/{status}")
                .Content().Response();
            if (response["status"]?.ToString() == "FAIL")
                return true;

            return false;
        }

        #endregion Metodos geral
    }
}
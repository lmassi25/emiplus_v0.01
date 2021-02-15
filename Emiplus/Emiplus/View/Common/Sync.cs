using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChoETL;
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
        //600000 - 10 minutos
        //timer_tick | StartSync | GetCreateDataAsync

        public static int sync_start { get; set; }
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
        
        private static readonly string idEmpresa = IniFile.Read("idEmpresa", "APP");

        public static bool Remessa { get; set; }

        private async Task RunSyncAsync_old(string table)
        {
            // ######### CREATE AND UPDATE #########
            // 1 - Pego os registros com status CREATE ou UPDATE e envio para o db online para ser cadastrado ou atualizado.
            // Caso o registro online seja mais recente não atualiza
            var dataCreate = await GetCreateDataAsync(table);
            if (dataCreate.Any())
            {
                dynamic obj = new
                {
                    token = Program.TOKEN,
                    id_empresa = idEmpresa,
                    data = JsonConvert.SerializeObject(new {items = dataCreate}),
                    status_sync = "CREATED",
                    controller = table.Replace("_", "")
                };

                var response = new RequestApi().URL(Program.URL_BASE + "/api/geral/createjson")
                    .Content(obj, Method.POST).Response();
                if (response["status"] == "OK")
                {
                    var items = response["data"];
                    foreach (var idsync in items)
                        await UpdateAsync(table, Validation.ConvertToInt32(idsync?.ToString()));
                }
                else
                {
                    new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}",
                        Log.LogType.fatal);
                }
            }

            #region comments

            //foreach (var item in dataCreate)
            //    {
            //        // inserie no banco online
            //        dynamic obj = new
            //        {
            //            token = Program.TOKEN,
            //            id_empresa = idEmpresa,
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
            //                id_empresa = idEmpresa,
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

            #endregion

            // 2 - Pega todos os registros online em json.
            // Pega todos os registros do banco LOCAL, faz um loop nos registros LOCAL e compara a data, se o registro online for mais recente atualiza o reigstro local
            // com os dados do registro online
            var responseC = new RequestApi()
                .URL(Program.URL_BASE +
                     $"/api/geral/getall/{Program.TOKEN}/{idEmpresa}/all/{table.Replace("_", "")}")
                .Content().Response();
            switch (responseC["status"]?.ToString())
            {
                case "OK" when string.IsNullOrEmpty(responseC["data"]?.ToString()):
                    return;
                case "FAIL" when !string.IsNullOrEmpty(responseC["message"]?.ToString()) && responseC["message"]?.ToString() == "Nenhum registro encontrado":
                    var datalocal = await GetAllDataAsync(table);
                    if (datalocal != null)
                        foreach (var item in datalocal)
                        {
                            await UpdateToUpdateAsync(table, Validation.ConvertToInt32(item.ID));
                        }

                    break;
                case "OK":
                {
                    var dataCreated = await GetAllDataAsync(table);
                    if (dataCreated != null)
                        foreach (var item in dataCreated)
                        {
                            // true = muda status para update para criar na proxima sync
                            // não existe o registro cadastrado no banco online
                            // Necessário caso o registro esteja como CREATED porém não esteja no banco online.
                            var toUpdate = true;
                            
                            //var d2 = Convert.ToDateTime(item.ATUALIZADO?.ToString());
                            //var dirs = JObject.Parse(responseC["data"].ToString())
                            //    .Descendants()
                            //    .OfType<JObject>()
                            //    .Where(x=> x["ID"] != null && x["ID"] == item.ID)
                            //    .Where(d => Convert.ToDateTime(d["ATUALIZADO"]).CompareTo(d2) >= 0)
                            //    //.Select(x => new { ID = (int)x["ID"], ATUALIZADO = (string)x["ATUALIZADO"] })
                            //    .FirstOrDefault();

                            //if (dirs == null)
                            //    await UpdateToUpdateAsync(table, Validation.ConvertToInt32(item.ID));
                            //else
                            //{
                            //    if (Convert.ToDateTime(dirs["ATUALIZADO"]).CompareTo(d2) > 0)
                            //        UpdateLocalAsync(table, dirs.ToString());
                            //}
                            
                            //continue;
                            //Console.WriteLine(dirs);
                            //Console.WriteLine(dirs?.ATUALIZADO);
                            //var dados = dirs.Find(x => x.ID == item.ID);
                            //Console.WriteLine(dados.ID);
                            //Console.WriteLine(dados.ATUALIZADO);
                                
                            foreach (var data in responseC["data"])
                            {
                                if (data == null)
                                    continue;
                                
                                // atualiza o registro no banco LOCAL caso tenha mudança no banco online
                                if (Validation.ConvertToInt32(data.First()["ID"]) == Validation.ConvertToInt32(item.ID))
                                {
                                    var d1 = Convert.ToDateTime(data.First()["ATUALIZADO"]?.ToString());
                                    var d2 = Convert.ToDateTime(item.ATUALIZADO?.ToString());
                                    
                                    // atualizar o registro local com os dados do registro online pois o registro online está mais recente
                                    if (d1.CompareTo(d2) > 0) UpdateLocalAsync(table, data.First().ToString());

                                    toUpdate = false;
                                }
                            }

                            // Muda o status do registro LOCAL para UPDATE
                            if (toUpdate)
                                await UpdateToUpdateAsync(table, Validation.ConvertToInt32(item.ID));
                        }

                    break;
                }
                case "FAIL":
                    new Log().Add("SYNC", $"{responseC["status"]} | Tabela: {table} - {responseC["message"]}",
                        Log.LogType.fatal);
                    break;
            }


            //if (CheckCreated(table, item.ID_SYNC))
            //{
            //    // inserie no banco online
            //    dynamic obj = new
            //    {
            //        token = Program.TOKEN,
            //        id_empresa = idEmpresa,
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

        private async Task RunSyncAsync(string table)
        {
            // ######### CREATE AND UPDATE #########
            #region 1 - Pego os registros com status CREATE ou UPDATE e envio para o db online para ser cadastrado ou atualizado.
            // Caso o registro online seja mais recente não atualiza
            var dataCreate = await GetCreateDataAsync(table);
            if (dataCreate.Any())
            {
                dynamic obj = new
                {
                    token = Program.TOKEN,
                    id_empresa = idEmpresa,
                    data = JsonConvert.SerializeObject(new { items = dataCreate }),
                    status_sync = "CREATED",
                    controller = table.Replace("_", "")
                };

                var response = new RequestApi().URL(Program.URL_BASE + "/api/geral/createjson").Content(obj, Method.POST).Response();
                if (response["status"] == "OK")
                {
                    var items = response["data"];
                    foreach (var idsync in items)
                        await UpdateAsync(table, Validation.ConvertToInt32(idsync?.ToString()));
                }
                else
                {
                    new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}",
                        Log.LogType.fatal);
                }

                new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}",
                        Log.LogType.info);
            }

            #endregion

            #region comments

            //foreach (var item in dataCreate)
            //    {
            //        // inserie no banco online
            //        dynamic obj = new
            //        {
            //            token = Program.TOKEN,
            //            id_empresa = idEmpresa,
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
            //                id_empresa = idEmpresa,
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

            #endregion

            #region 2 - Pega todos os registros online em json.
            // Pega todos os registros do banco LOCAL, faz um loop nos registros LOCAL e compara a data, se o registro online for mais recente atualiza o reigstro local com os dados do registro online

            //var responseC = new RequestApi().URL(Program.URL_BASE + $"/api/geral/getall/{Program.TOKEN}/{idEmpresa}/all/{table.Replace("_", "")}").Content().Response();
            var responseC = new RequestApi().URL(Program.URL_BASE + $"/api/geral/getall/{Program.TOKEN}/{idEmpresa}/all/{table.Replace("_", "")}").Content().Response();
            switch (responseC["status"]?.ToString())
            {
                case "OK" when string.IsNullOrEmpty(responseC["data"]?.ToString()):
                    return;
                case "FAIL" when !string.IsNullOrEmpty(responseC["message"]?.ToString()) && responseC["message"]?.ToString() == "Nenhum registro encontrado":
                    var datalocal = await GetAllDataAsync(table);
                    if (datalocal != null)
                        foreach (var item in datalocal)
                        {
                            await UpdateToUpdateAsync(table, Validation.ConvertToInt32(item.ID));
                        }

                    break;
                case "OK":
                    {
                        var dataCreated = await GetAllDataAsync(table);
                        if (dataCreated != null)
                            foreach (var item in dataCreated)
                            {
                                // true = muda status para update para criar na proxima sync
                                // não existe o registro cadastrado no banco online
                                // Necessário caso o registro esteja como CREATED porém não esteja no banco online.
                                var toUpdate = true;

                                //var d2 = Convert.ToDateTime(item.ATUALIZADO?.ToString());
                                //var dirs = JObject.Parse(responseC["data"].ToString())
                                //    .Descendants()
                                //    .OfType<JObject>()
                                //    .Where(x=> x["ID"] != null && x["ID"] == item.ID)
                                //    .Where(d => Convert.ToDateTime(d["ATUALIZADO"]).CompareTo(d2) >= 0)
                                //    //.Select(x => new { ID = (int)x["ID"], ATUALIZADO = (string)x["ATUALIZADO"] })
                                //    .FirstOrDefault();

                                //if (dirs == null)
                                //    await UpdateToUpdateAsync(table, Validation.ConvertToInt32(item.ID));
                                //else
                                //{
                                //    if (Convert.ToDateTime(dirs["ATUALIZADO"]).CompareTo(d2) > 0)
                                //        UpdateLocalAsync(table, dirs.ToString());
                                //}

                                //continue;
                                //Console.WriteLine(dirs);
                                //Console.WriteLine(dirs?.ATUALIZADO);
                                //var dados = dirs.Find(x => x.ID == item.ID);
                                //Console.WriteLine(dados.ID);
                                //Console.WriteLine(dados.ATUALIZADO);

                                foreach (var data in responseC["data"])
                                {
                                    if (data == null)
                                        continue;

                                    // atualiza o registro no banco LOCAL caso tenha mudança no banco online
                                    if (Validation.ConvertToInt32(data.First()["ID"]) == Validation.ConvertToInt32(item.ID))
                                    {
                                        var d1 = Convert.ToDateTime(data.First()["ATUALIZADO"]?.ToString());
                                        var d2 = Convert.ToDateTime(item.ATUALIZADO?.ToString());

                                        // atualizar o registro local com os dados do registro online pois o registro online está mais recente
                                        if (d1.CompareTo(d2) > 0) UpdateLocalAsync(table, data.First().ToString());

                                        toUpdate = false;
                                    }
                                }

                                // Muda o status do registro LOCAL para UPDATE
                                if (toUpdate)
                                    await UpdateToUpdateAsync(table, Validation.ConvertToInt32(item.ID));
                            }

                        break;
                    }
                case "FAIL":
                    new Log().Add("SYNC", $"{responseC["status"]} | Tabela: {table} - {responseC["message"]}",
                        Log.LogType.fatal);
                    break;
            }

            new Log().Add("SYNC", $"{responseC["status"]} | Tabela: {table} - {responseC["message"]}",
                        Log.LogType.info);

            #endregion

            #region comments

            //if (CheckCreated(table, item.ID_SYNC))
            //{
            //    // inserie no banco online
            //    dynamic obj = new
            //    {
            //        token = Program.TOKEN,
            //        id_empresa = idEmpresa,
            //        data = JsonConvert.SerializeObject(item),
            //        status_sync = "CREATED"
            //    };

            //    var response = new RequestApi().URL(Program.URL_BASE + $"/api/{table.Replace("_", "")}/create")
            //        .Content(obj, Method.POST).Response();
            //    if (response["status"] == "FAIL")
            //        new Log().Add("SYNC", $"{response["status"]} | Tabela: {table} - {response["message"]}",
            //            Log.LogType.fatal);
            //}

            #endregion
        }

        public void Dispose()
        {
            
        }

        public void SendNota()
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
                id_empresa = idEmpresa
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
                             $"/api/pedido/get/{Program.TOKEN}/{idEmpresa}/{item.ID_SYNC}")
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
                                id_empresa = idEmpresa
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
                .URL(Program.URL_BASE + $"/api/pedido/remessas/{Program.TOKEN}/{idEmpresa}")
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
                        var idItem = 0;
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
            if (sync_start == 0)
            {
                sync_start = 1;

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("caixa");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("caixa_mov");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("imposto");

                if (Support.CheckForInternetConnection())
                    await RunSyncAsync("categoria");

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

                sync_start = 0;
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
                timer_tick();
            };

            backWork.DoWork += async (s, e) => 
            {
                new Log().Add("SYNC", "Sincronização iniciada", Log.LogType.info);

                if (sync_start == 0)
                {
                    await StartSync();
                }
            };

            backWork.RunWorkerCompleted += (s, e) =>
            {
                new Log().Add("SYNC", "Sincronização concluída", Log.LogType.info);

                timer1.Enabled = true;
                timer1.Start();
                Home.syncActive = false;
            };
        }

        private void timer_tick()
        {
            if (Support.CheckForInternetConnection())
                if (!string.IsNullOrEmpty(Settings.Default.user_dbhost))
                    if (!Home.syncActive)
                    {
                        if (IniFile.Read("syncAuto", "APP") == "True")
                        {
                            backWork.RunWorkerAsync();
                            Home.syncActive = true;
                        }
                    }

            timer1.Stop();
        }

        #region Metodos geral

        /// <summary>
        ///     Recupera os dados das tabelas do sistema local para manipulação
        ///     Função retorna os registros 'CREATE' ou 'UPDATE'
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetCreateDataAsync(string table)
        {
            int limit = 5000;
            if (IniFile.Read("LIMIT", "SYNC") != "0")
            {
                limit = Validation.ConvertToInt32(IniFile.Read("LIMIT", "SYNC"));
            }

            limit = 500;

            var baseQuery = connect.Query().Where("id_empresa",idEmpresa)
                .Where(q => q.Where("status_sync", "CREATE").OrWhere("status_sync", "UPDATE"));

            if (Remessa && table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            baseQuery.OrderByDesc("id");

            baseQuery.Limit(limit);

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
        ///     Recupera todos os dados das tabelas do sistema local para manipulação
        /// </summary>
        private async Task<IEnumerable<dynamic>> GetAllDataAsync(string table)
        {
            int limit = 5000;
            if (IniFile.Read("LIMIT", "SYNC") != "0")
            {
                limit = Validation.ConvertToInt32(IniFile.Read("LIMIT", "SYNC"));
            }

            var baseQuery = connect.Query().Where("id_empresa", idEmpresa);

            if (Remessa && table == "pedido_item")
                baseQuery.Where("status", "Remessa");

            if (Remessa && table == "pedido")
                baseQuery.Where("tipo", "Remessas");

            baseQuery.Limit(limit);

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
        private async Task UpdateToUpdateAsync(string table, int id)
        {
            await connect.Query(table).Where("id", id).UpdateAsync(new {status_sync = "UPDATE"});
        }

        /// <summary>
        ///     Necessário para atualizar o registro local quando houve mudança no sistema online
        /// </summary>
        private void UpdateLocalAsync(string table, string data)
        {
            if (string.IsNullOrEmpty(data))
                return;
            
            switch (table)
            {
                case "categoria":
                {
                    var dados = JsonConvert.DeserializeObject<Categoria>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Categoria().Save(dados, false);
                    break;
                }
                case "caixa":
                {
                    var dados = JsonConvert.DeserializeObject<Caixa>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Caixa().Save(dados, false);
                    break;
                }
                case "caixa_mov":
                {
                    var dados = JsonConvert.DeserializeObject<CaixaMovimentacao>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new CaixaMovimentacao().Save(dados, false);
                    break;
                }
                case "imposto":
                {
                    var dados = JsonConvert.DeserializeObject<Imposto>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Imposto().Save(dados, false);
                    break;
                }
                case "item":
                {
                    var dados = JsonConvert.DeserializeObject<Item>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Item().Save(dados, false);
                    break;
                }
                case "item_mov_estoque":
                {
                    var dados = JsonConvert.DeserializeObject<ItemEstoqueMovimentacao>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new ItemEstoqueMovimentacao().Save(dados, false);
                    break;
                }
                case "natureza":
                {
                    var dados = JsonConvert.DeserializeObject<Natureza>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Natureza().Save(dados, false);
                    break;
                }
                case "pessoa":
                {
                    var dados = JsonConvert.DeserializeObject<Pessoa>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Pessoa().Save(dados, false);
                    break;
                }
                case "pessoa_contato":
                {
                    var dados = JsonConvert.DeserializeObject<PessoaContato>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new PessoaContato().Save(dados, false);
                    break;
                }
                case "pessoa_endereco":
                {
                    var dados = JsonConvert.DeserializeObject<PessoaEndereco>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new PessoaEndereco().Save(dados, false);
                    break;
                }
                case "titulo":
                {
                    var dados = JsonConvert.DeserializeObject<Titulo>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Titulo().Save(dados, false);
                    break;
                }
                case "nota":
                {
                    var dados = JsonConvert.DeserializeObject<Nota>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Nota().Save(dados, false);
                    break;
                }
                case "pedido":
                {
                    var dados = JsonConvert.DeserializeObject<Pedido>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new Pedido().Save(dados);
                    break;
                }
                case "pedido_item":
                {
                    var dados = JsonConvert.DeserializeObject<PedidoItem>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    new PedidoItem().Save(dados);
                    break;
                }
            }
        }

        /// <summary>
        ///     Atualiza os dados no banco online
        /// </summary>
        private bool UpdateOnline(string table, int id, dynamic item)
        {
            dynamic obj = new
            {
                token = Program.TOKEN,
                id_empresa = idEmpresa,
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
                     $"/api/{table.Replace("_", "")}/get/{Program.TOKEN}/{idEmpresa}/{id}")
                .Content().Response();
            if (response["status"]?.ToString() == "FAIL")
                return true;

            return false;
        }

        /// <summary>
        ///     Retorna todos os registros
        /// </summary>
        /// <param name="table"></param>
        /// <param name="status">CREATE, UPDATE ou CREATED</param>
        /// <returns></returns>
        private bool GetAllJson(string table, string status)
        {
            var response = new RequestApi()
                .URL(Program.URL_BASE +
                     $"/api/{table.Replace("_", "")}/getall/{Program.TOKEN}/{idEmpresa}/{status}")
                .Content().Response();
            if (response["status"]?.ToString() == "FAIL")
                return true;

            return false;
        }

        #endregion Metodos geral
    }
}
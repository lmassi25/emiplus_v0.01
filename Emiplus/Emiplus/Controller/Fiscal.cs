using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Fiscal;
using ESC_POS_USB_NET.Printer;
using Newtonsoft.Json.Linq;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Emiplus.Controller
{
    public class Fiscal
    {
        #region VARIAVEIS

        private int _id_empresa = 1;
        private int _id_nota;

        private Model.Pedido _pedido;
        private Model.PedidoItem _pedidoItem;

        private Model.Nota _nota;

        private IEnumerable<dynamic> itens;
        private IEnumerable<dynamic> pagamentos;

        private Model.Pessoa _emitente;
        private Model.PessoaEndereco _emitenteEndereco;
        private Model.PessoaContato _emitenteContato;

        private Model.Pessoa _destinatario;
        private Model.PessoaEndereco _destinatarioEndereco;
        private Model.PessoaContato _destinatarioContato;

        private Model.Pessoa _transportadora;
        private Model.PessoaEndereco _transportadoraEndereco;
        private Model.PessoaContato _transportadoraContato;

        private Model.Natureza _natureza;

        private static readonly HttpClient client = new HttpClient();

        private string _path;
        private string _path_enviada;
        private string _path_autorizada;

        private int _servidorNFE = 2;
        private int _servidorNFSE = 2;
        private int _servidorNFCE = 2;
        private int _servidorCFE = 2;

        private string TECNOSPEED_GRUPO = "Destech";
        private string TECNOSPEED_USUARIO = "admin";
        private string TECNOSPEED_SENHA = "LE25an10na";
        private string TECNOSPEED_CIDADEUF = "SAOJOSEDORIOPRETOSP";
        private string TECNOSPEED_SERVIDOR;

        private string _msg;

        public string requestData;
        public System.Timers.Timer aTimer = new System.Timers.Timer();

        private string chvAcesso = "", cDV = "", cNF = "", nNF = "", serie = "", layoutCFE = "", assinaturaCFE = "", layoutNFE = "", loteNFSE = "1";

        private Random rdn = new Random();
       
        #endregion V

        private JObject userData { get; set; }

        /// <summary>
        /// Atualiza Última Nota
        /// </summary>
        private async System.Threading.Tasks.Task updateUltNfeAsync()
        {
            var values = new Dictionary<string, string>
            {
                { "token", "f012622defec1e2bad3b8596e0642c" },
                { "email", Settings.Default.user_email },
                { "password", Settings.Default.user_password },
                { "id_empresa", Settings.Default.empresa_unique_id }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(Program.URL_BASE + "/api/ultimanfe", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var jo = JObject.Parse(responseString);

            var ultimaNfe = jo["ultimanfe"].ToString();

            Settings.Default.empresa_nfe_ultnfe = ultimaNfe;
            Settings.Default.Save();
        }

        /// <summary>
        /// Atualiza os Config
        /// </summary>
        private void Start(int Pedido = 0, string tipo = "NFe", int Nota = 0)
        {
            _path = IniFile.Read("Path", "LOCAL");

            if (String.IsNullOrEmpty(_path))
                _path = @"C:\Emiplus";

            _servidorNFE = Settings.Default.empresa_nfe_servidornfe;
            //_servidorNFE = 2;
            //_servidorNFE = 1;

            if (IniFile.Read("Servidor", "SAT") == "Producao")
                _servidorCFE = 1;

            switch (tipo)
            {
                case "NFe":
                    if (!Directory.Exists(_path + "\\NFe"))
                        Directory.CreateDirectory(_path + "\\NFe");

                    _path_enviada = _path + "\\NFe\\Enviadas";
                    if (!Directory.Exists(_path_enviada))
                        Directory.CreateDirectory(_path_enviada);

                    _path_autorizada = _path + "\\NFe\\Autorizadas";
                    if (!Directory.Exists(_path_autorizada))
                        Directory.CreateDirectory(_path_autorizada);

                    if (Nota > 0)
                    {
                        _nota = new Model.Nota().FindById(Nota).First<Model.Nota>();

                        if (!String.IsNullOrEmpty(_nota.ChaveDeAcesso))
                            chvAcesso = _nota.ChaveDeAcesso;
                    }
                    else if (_id_nota > 0)
                    {
                        _nota = new Model.Nota().FindById(_id_nota).First<Model.Nota>();

                        if (!String.IsNullOrEmpty(_nota.ChaveDeAcesso))
                            chvAcesso = _nota.ChaveDeAcesso;
                    }

                    break;

                case "NFCe":
                    if (!Directory.Exists(_path + "\\NFCe"))
                        Directory.CreateDirectory(_path + "\\NFCe");

                    _path_enviada = _path + "\\NFCe\\Enviadas";
                    if (!Directory.Exists(_path_enviada))
                        Directory.CreateDirectory(_path_enviada);

                    _path_autorizada = _path + "\\NFCe\\Autorizadas";
                    if (!Directory.Exists(_path_autorizada))
                        Directory.CreateDirectory(_path_autorizada);

                    if (Nota > 0)
                    {
                        _nota = new Model.Nota().FindById(Nota).First<Model.Nota>();

                        if (!String.IsNullOrEmpty(_nota.ChaveDeAcesso))
                            chvAcesso = _nota.ChaveDeAcesso;
                    }
                    else if (_id_nota > 0)
                    {
                        _nota = new Model.Nota().FindById(_id_nota).First<Model.Nota>();

                        if (!String.IsNullOrEmpty(_nota.ChaveDeAcesso))
                            chvAcesso = _nota.ChaveDeAcesso;
                    }

                    break;

                case "CFe":
                    if (!Directory.Exists(_path + "\\CFe"))
                        Directory.CreateDirectory(_path + "\\CFe");

                    _path_enviada = _path + "\\CFe\\Enviadas";
                    if (!Directory.Exists(_path_enviada))
                        Directory.CreateDirectory(_path_enviada);

                    _path_autorizada = _path + "\\CFe\\Autorizadas";
                    if (!Directory.Exists(_path_autorizada))
                        Directory.CreateDirectory(_path_autorizada);

                    //if (Pedido > 0)
                    //    _nota = new Model.Nota().FindByIdPedidoAndTipoAndStatus(Pedido, tipo).First<Model.Nota>();

                    if (!String.IsNullOrEmpty(IniFile.Read("Layout", "SAT")))
                        layoutCFE = IniFile.Read("Layout", "SAT");
                    else
                        layoutCFE = "0.08";

                    assinaturaCFE = IniFile.Read("Assinatura", "SAT");

                    break;

                case "NFSe":
                    if (!Directory.Exists(_path + "\\NFSe"))
                        Directory.CreateDirectory(_path + "\\NFSe");

                    _path_enviada = _path + "\\NFSe\\Enviadas";
                    if (!Directory.Exists(_path_enviada))
                        Directory.CreateDirectory(_path_enviada);

                    _path_autorizada = _path + "\\NFSe\\Autorizadas";
                    if (!Directory.Exists(_path_autorizada))
                        Directory.CreateDirectory(_path_autorizada);

                    //if (Nota > 0)
                    //{
                    //    _nota = new Model.Nota().FindById(Nota).First<Model.Nota>();

                    //    if (!String.IsNullOrEmpty(_nota.ChaveDeAcesso))
                    //        chvAcesso = _nota.ChaveDeAcesso;
                    //}
                    //else if (_id_nota > 0)
                    //{
                    //    _nota = new Model.Nota().FindById(_id_nota).First<Model.Nota>();

                    //    if (!String.IsNullOrEmpty(_nota.ChaveDeAcesso))
                    //        chvAcesso = _nota.ChaveDeAcesso;
                    //}                    

                    break;
            }

            if (_servidorNFE == 1)
                TECNOSPEED_SERVIDOR = "https://managersaas.tecnospeed.com.br:8081/";
            else
                TECNOSPEED_SERVIDOR = "https://managersaashom.tecnospeed.com.br:7071/";

            if (Pedido > 0)
                _pedido = new Model.Pedido().FindById(Pedido).First<Model.Pedido>();

            //_emitente = new Model.Pessoa().FindById(_id_empresa).First<Model.Pessoa>();
            _emitente = new Model.Pessoa();

            //_emitenteEndereco = new Model.PessoaEndereco().FindById(_id_empresa).First<Model.PessoaEndereco>();
            _emitenteEndereco = new Model.PessoaEndereco();

            //_emitenteContato = new Model.PessoaContato().FindById(_id_empresa).First<Model.PessoaContato>();
            _emitenteContato = new Model.PessoaContato();

            _emitente.RG = Validation.CleanStringForFiscal(Settings.Default.empresa_inscricao_estadual).Replace(" ", "");
            _emitente.CPF = Validation.CleanStringForFiscal(Settings.Default.empresa_cnpj).Replace(" ", "");

            _emitente.Nome = Settings.Default.empresa_razao_social;
            _emitente.Fantasia = Settings.Default.empresa_nome_fantasia;

            //if (Settings.Default.empresa_nfe_servidornfe.ToString() == "2" && tipo == "NFe")
            //{
            //    _emitente.RG = "647429018110";
            //    _emitente.CPF = "05681389000100";
            //    _emitente.Nome = "DESTECH DESENVOLVIMENTO E TECNOLOGIA";
            //    _emitente.Fantasia = "DESTECH DESENVOLVIMENTO E TECNOLOGIA";
            //}
                        
            if (IniFile.Read("Servidor", "SAT") == "Homologacao" && tipo == "CFe")
            {
                _emitente.CPF = "08723218000186";
                TECNOSPEED_GRUPO = "grupo-teste";
                TECNOSPEED_USUARIO = "admin";
                TECNOSPEED_SENHA = "123mudar";
            }

            _emitenteEndereco.Rua = Settings.Default.empresa_rua;
            _emitenteEndereco.Nr = Settings.Default.empresa_nr;
            _emitenteEndereco.Bairro = Settings.Default.empresa_bairro;
            _emitenteEndereco.Cidade = Settings.Default.empresa_cidade;
            _emitenteEndereco.Cep = Settings.Default.empresa_cep;
            _emitenteEndereco.IBGE = Settings.Default.empresa_ibge;
            _emitenteEndereco.Estado = Settings.Default.empresa_estado;

            //if (Settings.Default.empresa_servidornfse.ToString() == "2" && tipo == "NFSe")
            //{
            //    Settings.Default.empresa_rps = "0";
            //    Settings.Default.empresa_serienfse = "100";

            //    loteNFSE = "12345678999";

            //    TECNOSPEED_SERVIDOR = "https://managersaashom.tecnospeed.com.br:7071/";
            //    TECNOSPEED_GRUPO = "edoc";
            //    TECNOSPEED_USUARIO = "admin";
            //    TECNOSPEED_SENHA = "123mudar";
            //    TECNOSPEED_CIDADEUF = "MARINGAPR";

            //    _emitente.CPF = "08187168000160";
            //    _emitente.Nome = "DESTECH DESENVOLVIMENTO E TECNOLOGIA";
            //    _emitente.Fantasia = "DESTECH DESENVOLVIMENTO E TECNOLOGIA";

            //    _emitenteEndereco.Cep = "87020025";
            //    _emitenteEndereco.Cidade = "MARINGA";
            //    _emitenteEndereco.Estado = "PR";
            //}
        }

        /// <summary>
        /// Envia
        /// </summary>
        /// <param name="tipo">NFe, NFCe, CFe</param>
        public string Emitir(int Pedido, string tipo = "NFe", int Nota = 0, bool imposto = true)
        {
            _id_nota = Nota;

            Start(Pedido, tipo);

            try
            {
                if (tipo == "CFe" || tipo == "NFCe")
                {
                    _nota = new Model.Nota().FindByIdPedidoUltReg(Pedido, "Pendente").FirstOrDefault<Model.Nota>();

                    if (_nota == null)
                    {
                        if (Nota > 0)
                            _nota = new Model.Nota().FindById(Nota).First<Model.Nota>();
                        else if (_id_nota > 0)
                            _nota = new Model.Nota().FindById(_id_nota).First<Model.Nota>();
                    }

                    if (_nota == null)
                    {
                        //return "Não foi possível acessar a Nota/Cupom solicitado. Entre em contato com o suporte técnico.";
                        Model.Nota _modelNota = new Model.Nota();
                        _modelNota.Id = 0;
                        _modelNota.Tipo = "CFe";
                        _modelNota.Status = "Pendente";
                        _modelNota.id_pedido = Pedido;
                        if (!_modelNota.Save(_modelNota, false))
                        {
                            _msg = "Problema com tabela Notas. Entre em contato com o suporte técnico!";
                            return _msg;
                        }
                            
                        _nota = new Model.Nota().FindByIdPedidoUltReg(Pedido, "Pendente").FirstOrDefault<Model.Nota>();
                    }
                }

                //if (tipo == "NFe")
                //_nota = new Model.Nota().FindByIdPedidoUltReg(Pedido, "Pendente").FirstOrDefault<Model.Nota>();

                if (imposto)
                {
                    var itens = new Model.PedidoItem().Query()
                        .LeftJoin("item", "item.id", "pedido_item.item")
                        .Select("pedido_item.id")
                        .Where("pedido_item.pedido", Pedido)
                        .Where("pedido_item.excluir", 0)
                        .Where("pedido_item.tipo", "Produtos");

                    foreach (var item in itens.Get())
                    {
                        new Controller.Imposto().SetImposto(item.ID, 0, tipo);
                    }
                }

                CriarXML(Pedido, tipo);

                if (!String.IsNullOrEmpty(_msg))
                    return _msg;

                if (IniFile.Read("NFe", "APP") != "Uninfe")
                {
                    if (tipo == "NFe")
                    {
                        ValidarXML(Pedido, tipo);

                        if (!_msg.Contains("XML Válido"))
                            return _msg;
                    }
                }

                TransmitirXML(Pedido, tipo);
            }
            catch (Exception ex)
            {
                return "Opss.. encontramos um erro: " + ex.Message;
            }

            return _msg;
        }

        /// <summary>
        /// Cancelar
        /// </summary>
        /// <param name="tipo">NFe, NFCe, CFe</param>
        public string Cancelar(int Pedido, string tipo = "NFe", string justificativa = "", int Nota = 0)
        {
            _id_nota = Nota;
            Start(Pedido, tipo);

            try
            {
                switch (tipo)
                {
                    #region NFE

                    case "NFe":

                        if (IniFile.Read("NFe", "APP") == "Uninfe")
                        {
                            #region UNINFE

                            #region XML

                            string arqPath = _path_enviada + "\\Cancela" + Pedido + ".xml";

                            if (String.IsNullOrEmpty(_nota.protocolodeuso))
                            {
                                uninfe_consulta("", "");
                            }

                            XmlTextWriter writer = new XmlTextWriter(arqPath, null);

                            writer.WriteStartElement("envEvento");
                            writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");
                            writer.WriteAttributeString("versao", "1.00");
                            writer.WriteElementString("idLote", "0000000000" + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "5");

                            writer.WriteStartElement("evento");
                            writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");
                            writer.WriteAttributeString("versao", "1.00");

                            writer.WriteStartElement("infEvento");

                            // ID + tpEvento + chave da NF-e + nSeqEvento
                            // ID 110111 43100304678683000191550010000344924352121508 1
                            writer.WriteAttributeString("Id", "ID110111" + _nota.ChaveDeAcesso + "01");

                            writer.WriteElementString("cOrgao", "35");
                            writer.WriteElementString("tpAmb", _servidorNFE.ToString());
                            writer.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", ""));
                            writer.WriteElementString("chNFe", _nota.ChaveDeAcesso);
                            writer.WriteElementString("dhEvento", Validation.DateNowToSql() + "T" + DateTime.Now.ToString("HH:mm:ss") + DateTime.Now.ToString("zzz"));
                            writer.WriteElementString("tpEvento", "110111");
                            writer.WriteElementString("nSeqEvento", "1");
                            writer.WriteElementString("verEvento", "1.00");

                            writer.WriteStartElement("detEvento");

                            writer.WriteAttributeString("versao", "1.00");

                            writer.WriteElementString("descEvento", "Cancelamento");
                            writer.WriteElementString("nProt", _nota.protocolodeuso);
                            writer.WriteElementString("xJust", Validation.CleanStringForFiscal(justificativa));

                            writer.WriteEndElement();

                            writer.WriteEndElement();

                            writer.WriteEndElement();

                            writer.WriteEndElement();

                            writer.Close();

                            #endregion

                            uninfe_envia("cancela", arqPath);

                            #endregion
                        }
                        else
                        {
                            #region TECNOSPEED 

                            _msg = RequestCancela(tipo, justificativa);

                            if (_msg.Contains("Evento registrado e vinculado a NF-e"))
                            {
                                _msg = "NF-e cancelada com sucesso.";
                                _nota.Status = "Cancelada";
                                _nota.Save(_nota, false);
                            }

                            #endregion
                        }
                        break;

                    #endregion NFE

                    #region CFE

                    case "CFe":

                        _nota = new Model.Nota().FindByIdPedidoUltReg(Pedido, "Autorizada").FirstOrDefault<Model.Nota>();

                        if (_nota == null)
                        {
                            _msg = "Problema ao cancelar cupom fiscal. Cupom autorizado não identificado!";
                            return _msg;
                        }

                        CriarXML(Pedido, "CFe", 1);

                        var arq = new XmlDocument();

                        if (!File.Exists(_path_enviada + "\\" + Pedido + "Canc.xml"))
                        {
                            _msg = "Opss.. encontramos um erro: XML não encontrado.";
                            return _msg;
                        }

                        arq.Load(_path_enviada + "\\" + Pedido + "Canc.xml");

                        Random rdn = new Random();
                        _msg = Sat.StringFromNativeUtf8(Sat.CancelarUltimaVenda(rdn.Next(999999), GetCodAtivacao(), _nota.ChaveDeAcesso, arq.OuterXml));

                        if (_msg.Contains("Cupom cancelado com sucesso"))
                        {
                            StreamWriter txt = new StreamWriter(_path_enviada + "\\" + _pedido.Id + "Canc.txt", false, Encoding.UTF8);
                            txt.Write(_msg);
                            txt.Close();

                            if (!Directory.Exists(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00")))
                                Directory.CreateDirectory(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00"));

                            string ChaveDeAcesso = "", ChaveDeAcessoCanc = "", nr_Nota = "", assinatura_qrcode = "";

                            try
                            {
                                XmlDocument oXML = new XmlDocument();
                                oXML.LoadXml(Base64ToString(Sep_Delimitador('|', 6, _msg)));

                                ChaveDeAcesso = oXML.SelectSingleNode("/CFeCanc/infCFe").Attributes.GetNamedItem("Id").Value;
                                ChaveDeAcessoCanc = oXML.SelectSingleNode("/CFeCanc/infCFe").Attributes.GetNamedItem("chCanc").Value;

                                nr_Nota = oXML.SelectSingleNode("/CFeCanc/infCFe/ide").ChildNodes[4].InnerText;
                                assinatura_qrcode = oXML.SelectSingleNode("/CFeCanc/infCFe/ide").ChildNodes[10].InnerText;

                                var doc = XDocument.Parse(Base64ToString(Sep_Delimitador('|', 6, _msg)));
                                doc.Save(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + "\\CFeCanc" + ChaveDeAcesso.Replace("CFe", "") + ".xml");

                                //------------------------nota salva
                                if (!Directory.Exists(_path_autorizada + "\\bkp\\"))
                                    Directory.CreateDirectory(_path_autorizada + "\\bkp\\");

                                doc = XDocument.Parse(Base64ToString(Sep_Delimitador('|', 6, _msg)));
                                doc.Save(_path_autorizada + "\\bkp\\CFeCanc" + ChaveDeAcesso.Replace("CFe", "") + ".xml");
                                //------------------------nota salva

                                ////------------------------
                                _msg = RequestImport(Base64ToString(Sep_Delimitador('|', 6, _msg)));
                                ////------------------------
                            }
                            catch (Exception ex)
                            { }

                            _msg = "Cupom cancelado com sucesso";

                            _nota.Tipo = tipo;
                            _nota.Status = "Cancelado";
                            _nota.Criado = DateTime.Now;
                            //-----------------------------------N° e Chave de acesso do cupom que foi emitido e agora está cancelado
                            _nota.Serie = _nota.nr_Nota;
                            _nota.correcao = ChaveDeAcessoCanc;
                            //-----------------------------------N° e Chave de acesso do cupom que foi emitido e agora está cancelado
                            _nota.nr_Nota = nr_Nota;
                            _nota.ChaveDeAcesso = ChaveDeAcesso;
                            _nota.assinatura_qrcode = assinatura_qrcode;
                            _nota.Save(_nota, false);
                        }
                        else
                        {
                            StreamWriter txt = new StreamWriter(_path_enviada + "\\" + _pedido.Id + "Canc.txt", false, Encoding.UTF8);
                            txt.Write(_msg);
                            txt.Close();
                        }

                        break;

                        #endregion CFE
                }
            }
            catch (Exception ex)
            {
                return "Opss.. encontramos um erro: " + ex.Message;
            }

            return _msg;
        }

        /// <summary>
        /// Imprime
        /// </summary>
        /// <param name="tipo">NFe, NFCe, CFe</param>
        public string Imprimir(int Pedido, string tipo = "NFe", int Nota = 0)
        {
            _id_nota = Nota;
            Start(Pedido, tipo);

            switch (tipo)
            {
                case "CFe":

                    _nota = new Model.Nota().FindByIdPedidoUltReg(Pedido).FirstOrDefault<Model.Nota>();

                    if (_nota.Status == "Autorizada")
                    {
                        #region CFE AUTORIZADO

                        //verificar status do registro e gerar dois tipo de impressao

                        var printername = IniFile.Read("Printer", "SAT");

                        if (printername == null)
                            return "";

                        Printer printer = new Printer(printername);

                        //using (WebClient wc = new WebClient())
                        //{
                        //    byte[] originalData = wc.DownloadData("https://www.emiplus.com.br" + Settings.Default.empresa_logo);
                        //    MemoryStream stream = new MemoryStream(originalData);
                        //    System.Drawing.Image img = Validation.ResizeImage(System.Drawing.Image.FromStream(stream), 1, 1);
                        //    Bitmap bitmap = new Bitmap(img);
                        //    printer.Image(bitmap);
                        //}

                        printer.AlignCenter();
                        printer.BoldMode(_emitente.Fantasia);
                        printer.Append(_emitente.Nome);
                        printer.Append(_emitenteEndereco.Rua + ", " + _emitenteEndereco.Nr + " - " + _emitenteEndereco.Bairro);
                        printer.Append(_emitenteEndereco.Cidade + "/" + _emitenteEndereco.Estado);
                        printer.Append(_emitenteContato.Telefone);

                        printer.NewLines(2);

                        printer.BoldMode("CNPJ: " + _emitente.CPF + " IE: " + _emitente.RG);
                        printer.Separator();

                        printer.BoldMode("Extrato N°" + _nota.nr_Nota);
                        printer.BoldMode("CUPOM FISCAL ELETRÔNICO - SAT");
                        printer.Separator();

                        printer.AlignLeft();
                        printer.Append("CPF/CNPJ do Consumidor: " + _pedido.cfe_cpf);
                        printer.Separator();

                        printer.AlignCenter();
                        printer.Append("#|COD|DESC|QTD|UN|VL UNIT|VL TR*|VLR ITEM R$|");
                        printer.Separator();

                        //printer.Append(AddSpaces("<n><cod><desc><qnt><un>x<vlrunit>", "0,00"));

                        itens = new Model.PedidoItem().Query()
                        .LeftJoin("item", "pedido_item.item", "item.id")
                        .Select("item.nome", "item.referencia", "item.codebarras", "pedido_item.quantidade", "pedido_item.valorvenda", "pedido_item.medida", "pedido_item.total as total", "pedido_item.federal", "pedido_item.estadual", "pedido_item.municipal")
                        .Where("pedido_item.pedido", Pedido)
                        .Where("pedido_item.excluir", 0)
                        .Where("pedido_item.tipo", "Produtos")
                        .OrderBy("pedido_item.id")
                        .Get();

                        int count = 0;

                        foreach (var data in itens)
                        {
                            count++;
                            printer.Append(AddSpaces(count + " " + data.NOME + " " + data.QUANTIDADE + " " + data.MEDIDA + " x " + Validation.FormatDecimalPrice(data.VALORVENDA) + " (" + Validation.FormatDecimalPrice(data.FEDERAL + data.ESTADUAL + data.MUNICIPAL) + ")", Validation.FormatDecimalPrice(data.TOTAL)));
                        }

                        printer.NewLines(2);

                        printer.Append(AddSpaces("Subtotal", Validation.FormatPrice(_pedido.Produtos)));
                        printer.Append(AddSpaces("Descontos", Validation.FormatPrice(_pedido.Desconto)));
                        printer.BoldMode(AddSpaces("TOTAL R$", Validation.FormatPrice(_pedido.Total)));

                        printer.NewLines(2);

                        //pagamentos = new Model.Titulo().Query().Select("Total").Where("titulo.id_pedido", Pedido).Where("titulo.excluir", 0).Get();
                        //total = total + Validation.ConvertToDouble(data.TOTAL);

                        string formapgto = "";

                        pagamentos = new Model.Titulo().Query().Where("titulo.id_pedido", Pedido).Where("titulo.excluir", 0).Get();
                        foreach (var data in pagamentos)
                        {
                            switch (data.ID_FORMAPGTO)
                            {
                                case 1:
                                    formapgto = "Dinheiro";
                                    break;

                                case 2:
                                    formapgto = "Cheque";
                                    break;

                                case 3:
                                    formapgto = "Cartão de Débito";
                                    break;

                                case 4:
                                    formapgto = "Cartão de Crédito";
                                    break;

                                case 5:
                                    formapgto = "Crediário";
                                    break;

                                case 6:
                                    formapgto = "Boleto";
                                    break;

                                default:
                                    formapgto = "N/D";
                                    break;
                            }

                            printer.Append(AddSpaces(formapgto, Validation.FormatDecimalPrice(data.TOTAL)));
                        }

                        printer.Append(AddSpaces("Troco R$", "0,00"));

                        printer.Separator();

                        printer.AlignLeft();
                        printer.Append("OBSERVAÇÕES DO CONTRIBUINTE");
                        printer.NewLines(2);

                        printer.CondensedMode("*Valor aproximado dos tributos do item");

                        var totais = new Model.PedidoItem().Query()
                        .LeftJoin("item", "pedido_item.item", "item.id")
                        .SelectRaw("SUM(pedido_item.federal) as federal, SUM(pedido_item.estadual) as estadual, SUM(pedido_item.municipal) as municipal")
                        .Where("pedido_item.pedido", Pedido)
                        .Where("pedido_item.excluir", 0)
                        .Where("pedido_item.tipo", "Produtos")
                        .Get();

                        foreach (var data in totais)
                        {
                            printer.CondensedMode(AddSpaces("Valor aproximado dos tributos deste cupom ", Validation.FormatDecimalPrice(data.FEDERAL + data.ESTADUAL + data.MUNICIPAL, true)));
                        }

                        printer.CondensedMode("(conforme Lei Fed.12.741/2012)");

                        printer.Separator();

                        printer.AlignCenter();
                        printer.Append("SAT N° " + IniFile.Read("N_SERIE", "SAT"));
                        printer.Append(_nota.Criado.ToString());
                        printer.NewLines(2);

                        string cfeid = _nota.ChaveDeAcesso.Replace("CFe", "");
                        //cfeid = cfeid.Substring(0, 4) + " " + cfeid.Substring(4, 4) + " " + cfeid.Substring(8, 4) + " " + cfeid.Substring(12, 4) + " " + cfeid.Substring(17, 4) + " " + cfeid.Substring(21, 4) + " " + cfeid.Substring(25, 4) + " " + cfeid.Substring(29, 4) + " " + cfeid.Substring(33, 4) + " " + cfeid.Substring(37, 4) + " " + cfeid.Substring(40, 4);

                        printer.CondensedMode(cfeid);

                        printer.Code128(_nota.ChaveDeAcesso.Replace("CFe", "").Substring(0, 22));
                        printer.Code128(_nota.ChaveDeAcesso.Replace("CFe", "").Substring(22, 22));

                        //cfeid + "|" + cfeData + cfeHora + "|" + "" + "|" + cfeAssinatura;

                        string qrcode = "", total_qrcode = "";
                        total_qrcode = Validation.FormatPriceWithDot(_pedido.Total);

                        if (!String.IsNullOrEmpty(_pedido.cfe_cpf))                        
                            qrcode = _nota.ChaveDeAcesso.Replace("CFe", "") + "|" + _nota.Criado.Year.ToString("0000") + _nota.Criado.Month.ToString("00") + _nota.Criado.Day.ToString("00") + _nota.Criado.Hour.ToString("00") + _nota.Criado.Minute.ToString("00") + _nota.Criado.Second.ToString("00") + "|" + total_qrcode + "|" + _pedido.cfe_cpf + "|" + _nota.assinatura_qrcode;
                        else                        
                            qrcode = _nota.ChaveDeAcesso.Replace("CFe", "") + "|" + _nota.Criado.Year.ToString("0000") + _nota.Criado.Month.ToString("00") + _nota.Criado.Day.ToString("00") + _nota.Criado.Hour.ToString("00") + _nota.Criado.Minute.ToString("00") + _nota.Criado.Second.ToString("00") + "|" + total_qrcode + "|" + "" + "|" + _nota.assinatura_qrcode;

                        //"|20191203095133||iat1ELc5/DZYefmF7Qpb/a9rtAzGynVaLhSAhzkjv4OdqUliAro2e4u9Ep3QlploQWQMJ4dYmEDRM5TeRJ8GY5HoKmIRyQKQ/CEVN53nD5vJ3KBFmLl33n3cXRXJaRxDC6l5GBmUZx1VFBgP82FdM16V2a5CBS8bWP5etbbgsnR08t7Wf3P+R9ORVPV+Lpj2n1FQSahyyBUGGpGAES69EU5sKHVSKDfxEWsuyWm8/LnX6t/12lqYsHiAEZoDjIcYVXlbSDza2tq2mG3TRQ9AXVyxu6BT+3kATuTvMzH/9W9PkYsipu5+OShW7y88K0u5eDmMXW9+NPE2ieuLdWDG0Q=="
                        printer.QrCode(qrcode);

                        printer.NewLines(3);

                        printer.AlignCenter();
                        printer.Append("Consulte o QRCode pelo aplicativo De olho na");
                        printer.Append("nota, disponível na AppStore(Apple) e PlayStore(Android)");

                        printer.FullPaperCut();
                        printer.PrintDocument();

                        return "Impresso com sucesso!";

                        #endregion CFE AUTORIZADO
                    }
                    else if (_nota.Status == "Cancelado")
                    {
                        #region CFE CANCELADO

                        var printername = IniFile.Read("Printer", "SAT");

                        if (printername == null)
                            return "";

                        Printer printer = new Printer(printername);

                        //using (WebClient wc = new WebClient())
                        //{
                        //    byte[] originalData = wc.DownloadData("https://www.emiplus.com.br" + Settings.Default.empresa_logo);
                        //    MemoryStream stream = new MemoryStream(originalData);
                        //    System.Drawing.Image img = Validation.ResizeImage(System.Drawing.Image.FromStream(stream), 1, 1);
                        //    Bitmap bitmap = new Bitmap(img);
                        //    printer.Image(bitmap);
                        //}

                        printer.AlignCenter();
                        printer.BoldMode(_emitente.Fantasia);
                        printer.Append(_emitente.Nome);
                        printer.Append(_emitenteEndereco.Rua + ", " + _emitenteEndereco.Nr + " - " + _emitenteEndereco.Bairro);
                        printer.Append(_emitenteEndereco.Cidade + "/" + _emitenteEndereco.Estado);
                        printer.Append(_emitenteContato.Telefone);

                        printer.NewLines(2);

                        printer.BoldMode("CNPJ: " + _emitente.CPF + " IE: " + _emitente.RG);
                        printer.Separator();

                        printer.BoldMode("Extrato N°" + _nota.nr_Nota);
                        printer.BoldMode("CUPOM FISCAL ELETRONICO - SAT");
                        printer.BoldMode("CANCELADO");
                        printer.Separator();

                        printer.AlignLeft();
                        printer.BoldMode("DADOS DO CUPOM FISCAL ELETRONICO CANCELADO");
                        printer.NewLines(2);
                        printer.Append("CPF/CNPJ do Consumidor: " + _pedido.cfe_cpf);
                        printer.BoldMode(AddSpaces("TOTAL R$", Validation.FormatPrice(_pedido.Total)));
                        printer.Separator();

                        printer.AlignCenter();
                        printer.Append("SAT N° " + IniFile.Read("N_SERIE", "SAT"));
                        printer.Append(_nota.Criado.ToString());
                        printer.NewLines(2);

                        string cfeid = _nota.ChaveDeAcesso.Replace("CFe", "");
                        cfeid = cfeid.Substring(0, 4) + " " + cfeid.Substring(4, 4) + " " + cfeid.Substring(8, 4) + " " + cfeid.Substring(12, 4) + " " + cfeid.Substring(17, 4) + " " + cfeid.Substring(21, 4) + " " + cfeid.Substring(25, 4) + " " + cfeid.Substring(29, 4) + " " + cfeid.Substring(33, 4) + " " + cfeid.Substring(37, 4) + " " + cfeid.Substring(40, 4);

                        printer.CondensedMode(cfeid);

                        printer.Code128(_nota.ChaveDeAcesso.Replace("CFe", "").Substring(0, 22));
                        printer.Code128(_nota.ChaveDeAcesso.Replace("CFe", "").Substring(22, 22));

                        //cfeid + "|" + cfeData + cfeHora + "|" + "" + "|" + cfeAssinatura;

                        string qrcode = "", total_qrcode = "";
                        total_qrcode = Validation.FormatPriceWithDot(_pedido.Total);

                        if (!String.IsNullOrEmpty(_pedido.cfe_cpf))
                        {
                            qrcode = _nota.ChaveDeAcesso.Replace("CFe", "") + "|" + _nota.Criado.Year.ToString("0000") + _nota.Criado.Month.ToString("00") + _nota.Criado.Day.ToString("00") + _nota.Criado.Hour.ToString("00") + _nota.Criado.Minute.ToString("00") + _nota.Criado.Second.ToString("00") + "|" + total_qrcode + "|" + _pedido.cfe_cpf + "|" + _nota.assinatura_qrcode;
                        }
                        else
                        {
                            qrcode = _nota.ChaveDeAcesso.Replace("CFe", "") + "|" + _nota.Criado.Year.ToString("0000") + _nota.Criado.Month.ToString("00") + _nota.Criado.Day.ToString("00") + _nota.Criado.Hour.ToString("00") + _nota.Criado.Minute.ToString("00") + _nota.Criado.Second.ToString("00") + "|" + total_qrcode + "|" + "" + "|" + _nota.assinatura_qrcode;
                        }

                        //"|20191203095133||iat1ELc5/DZYefmF7Qpb/a9rtAzGynVaLhSAhzkjv4OdqUliAro2e4u9Ep3QlploQWQMJ4dYmEDRM5TeRJ8GY5HoKmIRyQKQ/CEVN53nD5vJ3KBFmLl33n3cXRXJaRxDC6l5GBmUZx1VFBgP82FdM16V2a5CBS8bWP5etbbgsnR08t7Wf3P+R9ORVPV+Lpj2n1FQSahyyBUGGpGAES69EU5sKHVSKDfxEWsuyWm8/LnX6t/12lqYsHiAEZoDjIcYVXlbSDza2tq2mG3TRQ9AXVyxu6BT+3kATuTvMzH/9W9PkYsipu5+OShW7y88K0u5eDmMXW9+NPE2ieuLdWDG0Q=="
                        printer.QrCode(qrcode);

                        printer.NewLines(3);

                        printer.AlignCenter();
                        printer.Append("Consulte o QRCode pelo aplicativo De olho na");
                        printer.Append("nota, disponível na AppStore(Apple) e PlayStore(Android)");

                        printer.FullPaperCut();
                        printer.PrintDocument();

                        return "Impresso com sucesso!";

                        #endregion CFE CANCELADO
                    }

                    break;

                case "NFe":
                    
                    BrowserNfe browser = new BrowserNfe();

                    #region NFE

                    if (_nota.ChaveDeAcesso == null)
                        return "";

                    var pdfNFE = RequestPrint(_nota.ChaveDeAcesso.Replace("CFe", ""), tipo);
                    new Log().Add("PRINTER", pdfNFE, Log.LogType.info);

                    if (pdfNFE.Contains(".pdf"))
                    {
                        if (File.Exists(_path_autorizada + "\\NFe.pdf"))
                            File.Delete(_path_autorizada + "\\NFe.pdf");

                        Thread.Sleep(1000);

                        using (var client = new WebClient())
                        {
                            client.DownloadFile(pdfNFE, _path_autorizada + "\\NFe.pdf");
                        }

                        Thread.Sleep(1000);

                        if (!File.Exists(_path_autorizada + "\\NFe.pdf"))
                        {
                            return "Arquivo não encontrado!";
                        }

                        //BrowserNfe.Render = pdf;
                        //browser.ShowDialog();

                        System.Diagnostics.Process.Start(_path_autorizada + "\\NFe.pdf");

                        return "";
                    }
                    else
                    {
                        return pdfNFE;
                    }

                    #endregion NFE
            }

            return "";
        }

        /// <summary>
        /// Enviar email
        /// </summary>
        public string EnviarEmail(int Pedido, string email, string tipo = "NFe", int Nota = 0)
        {
            _id_nota = Nota;
            Start(Pedido);

            _msg = "Opss.. Não foi possível enviar o email.";

            try
            {
                switch (tipo)
                {
                    default:
                        _msg = RequestEmail(email, tipo);
                        break;
                }
            }
            catch (Exception ex)
            {
                _msg = "Opss.. encontramos um erro: " + ex.Message;
            }

            return _msg;
        }

        /// <summary>
        /// XML
        /// </summary>
        /// <param name="tipo">NFe, NFCe, CFe</param>
        private void CriarXML(int Pedido, string tipo, int modelo = 0)
        {
            #region DADOS

            if (tipo != "CFe")
            {
                _destinatario = new Model.Pessoa().FindById(_pedido.Cliente).FirstOrDefault<Model.Pessoa>();
                if (_destinatario == null)
                {
                    _msg = "Destinatário não encontrado. É necessário informar o destinatário para emitir.";
                    return;
                }

                if (_destinatario.Nome == "Consumidor Final" && tipo != "NFCe")
                {
                    _msg = "Destinatário não encontrado. É necessário informar o destinatário para emitir.";
                    return;
                }

                if (Validation.CleanStringForFiscal(_destinatario.CPF).Replace(".", "").Replace(" ", "") == "" && tipo == "NFe")
                {
                    _msg = "CPF/CNPJ do Destinatário não encontrado. É necessário informar o destinatário para emitir.";
                    return;
                }

                if (_pedido.id_useraddress > 0)
                    _destinatarioEndereco = new Model.PessoaEndereco().FindById(_pedido.id_useraddress).FirstOrDefault<Model.PessoaEndereco>();
                else if (_pedido.Cliente > 1)
                {
                    _destinatarioEndereco = new Model.PessoaEndereco().FindByIdUser(_pedido.Cliente).FirstOrDefault<Model.PessoaEndereco>();

                    //_destinatarioContato = new Model.PessoaContato().FindById(_pedido.Cliente).First<Model.PessoaContato>();
                    if (_destinatarioEndereco == null)
                    {
                        _msg = "Endereço do destinátário não encontrado. É necessário informar o endereço do destinatário para emitir. Clique no botão 'Ver Detalhes' para alterar.";
                        return;
                    }
                }


                if (_pedido.id_natureza > 0)
                    _natureza = new Model.Natureza().FindById(_pedido.id_natureza).FirstOrDefault<Model.Natureza>();
                else
                {
                    var checkNatureza = new Model.Natureza().Query().Where("NOME", "VENDA").FirstOrDefault<Model.Natureza>();
                    if (checkNatureza == null)
                    {
                        Model.Natureza nat = new Model.Natureza();
                        nat.Id = 0;
                        nat.Nome = "VENDA";
                        nat.Save(nat);
                        if (nat.Save(nat))
                        {
                            var idNatureza = nat.GetLastId();
                            _pedido.id_natureza = idNatureza;
                            _pedido.Save(_pedido);
                        }

                        _natureza = new Model.Natureza().FindById(_pedido.id_natureza).FirstOrDefault<Model.Natureza>();
                    }
                    else
                    {
                        _pedido.id_natureza = checkNatureza.Id;
                        _pedido.Save(_pedido);

                        _natureza = new Model.Natureza().FindById(_pedido.id_natureza).FirstOrDefault<Model.Natureza>();
                    }
                }
            }

            #endregion DADOS

            string strFilePath = "";

            switch (modelo)
            {
                case 1:

                    #region XML CANCELAMENTO CFe

                    #region PATH

                    strFilePath = _path_enviada + "\\" + Pedido + "Canc.xml";

                    #endregion PATH

                    #region XML

                    var xmlCanc = new XmlTextWriter(strFilePath, Encoding.UTF8);
                    xmlCanc.Formatting = Formatting.Indented;

                    xmlCanc.WriteStartDocument(); //Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>

                    xmlCanc.WriteStartElement("CFeCanc");
                    xmlCanc.WriteStartElement("infCFe");
                    xmlCanc.WriteAttributeString("chCanc", _nota.ChaveDeAcesso);

                    xmlCanc.WriteStartElement("ide");

                    if (_servidorCFE == 2)
                    {
                        xmlCanc.WriteElementString("CNPJ", "16716114000172");
                        xmlCanc.WriteElementString("signAC", "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT");
                        xmlCanc.WriteElementString("numeroCaixa", "001");
                    }
                    else
                    {
                        xmlCanc.WriteElementString("CNPJ", "05681389000100");
                        xmlCanc.WriteElementString("signAC", assinaturaCFE);
                        //xmlCanc.WriteElementString("signAC", "UJs4qtHeDIZvEyoBlvBhIdxlQcjkt76XTQ2biRyRHFPTtfchubnuRqjEhCbcu33dR4yWMMs7eSHea8shVSCHMnPYhBxuvvHPqtetiSw1zufrq55uHv8Wx1Stb39iExxii2m24pPpbsV0xH5lM1eBs/a6Gpi6EM7+ZYA0irhHmqwo0qsq8N64nz5M7j5OfR+CzvuelsFbjf0JTCdDpGkfjDGQZwXPeHimW1y8cUriJkW4jpD+BQDZT/l3IlWdn+hspwekc+ASHYWbDX8xTSDMDm+58HXMch/4Rdj58PUxIuAxDauDILJFtF6d5yQ8irxrqOpZzfqBJRbwOtmm6bg7Pw==");
                        xmlCanc.WriteElementString("numeroCaixa", "001");
                    }

                    xmlCanc.WriteEndElement();

                    xmlCanc.WriteStartElement("emit");
                    xmlCanc.WriteEndElement();

                    if (!String.IsNullOrEmpty(_pedido.cfe_cpf))
                        xmlCanc.WriteElementString("dest", Validation.CleanStringForFiscal(_pedido.cfe_cpf).Replace(".", "").Replace(" ", ""));
                    else
                        xmlCanc.WriteElementString("dest", "");

                    xmlCanc.WriteStartElement("total");
                    xmlCanc.WriteEndElement();

                    xmlCanc.WriteEndElement();
                    xmlCanc.WriteEndElement();

                    xmlCanc.WriteEndDocument();

                    xmlCanc.Flush();
                    xmlCanc.Close();

                    #endregion XML

                    #endregion XML CANCELAMENTO CFe

                    break;

                default:

                    #region XML EMISSÃO

                    if (tipo == "NFSe")
                    {
                        #region PATH

                        strFilePath = _path_enviada + "\\NFSe" + Pedido + ".tx2";

                        #endregion PATH

                        #region TX2

                        if (File.Exists(strFilePath))
                            File.Delete(strFilePath);

                        StreamWriter txt = new StreamWriter(strFilePath, true, Encoding.UTF8);

                        txt.Write("formato=tx2" + Environment.NewLine);
                        txt.Write("padrao=TecnoNFSe" + Environment.NewLine);
                        txt.Write("NomeCidade=" + TECNOSPEED_CIDADEUF + Environment.NewLine);
                        txt.Write(Environment.NewLine);

                        txt.Write("INCLUIR" + Environment.NewLine);
                        txt.Write("NumeroLote=" + loteNFSE + Environment.NewLine);
                        txt.Write("CPFCNPJRemetente=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + Environment.NewLine);
                        txt.Write("InscricaoMunicipalRemetente=" + Validation.CleanStringForFiscal(Settings.Default.empresa_inscricao_municipal) + Environment.NewLine);
                        txt.Write("ValorTotalServicos=" + Validation.FormatPriceWithDot(_pedido.Servicos) + Environment.NewLine);
                        txt.Write("ValorTotalDeducoes=0.00" + Environment.NewLine);
                        txt.Write("ValorTotalBaseCalculo=" + Validation.FormatPriceWithDot(_pedido.Servicos) + Environment.NewLine);
                        txt.Write("SALVAR" + Environment.NewLine);
                        txt.Write(Environment.NewLine);

                        txt.Write("INCLUIRRPS" + Environment.NewLine);
                        txt.Write("SituacaoNota=1" + Environment.NewLine);
                        txt.Write("TipoRps=1" + Environment.NewLine);
                        txt.Write("SerieRps=" + Settings.Default.empresa_serienfse + Environment.NewLine);
                        txt.Write("NumeroRps=" + getLastNFSeNr() + Environment.NewLine);
                        txt.Write("DataEmissao=" + Validation.ConvertDateToSql(_pedido.Emissao) + "T" + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")+ Environment.NewLine);
                        txt.Write("Competencia=" + Validation.ConvertDateToSql(_pedido.Emissao)+ Environment.NewLine);
                        
                        txt.Write("CpfCnpjPrestador=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + Environment.NewLine);
                        txt.Write("InscricaoMunicipalPrestador=" + Validation.CleanStringForFiscal(Settings.Default.empresa_inscricao_municipal) + Environment.NewLine);
                        txt.Write("RazaoSocialPrestador=" + Validation.CleanStringForFiscal(_emitente.Nome) + Environment.NewLine);
                        txt.Write("InscricaoEstadualPrestador=" + Validation.CleanStringForFiscal(_emitente.RG) + Environment.NewLine);
                        txt.Write("InscricaoMunicipalPrestador=" + Validation.CleanStringForFiscal(Settings.Default.empresa_inscricao_municipal) + Environment.NewLine);                        
                        txt.Write("TipoLogradouroPrestador=Rua" + Environment.NewLine);
                        txt.Write("EnderecoPrestador=" + Validation.CleanStringForFiscal(_emitenteEndereco.Rua) + Environment.NewLine);
                        txt.Write("NumeroPrestador=" + Validation.CleanStringForFiscal(_emitenteEndereco.Nr) + Environment.NewLine);
                        //txt.Write("ComplementoPrestador=Complemento teste sem acento" + Environment.NewLine);
                        txt.Write("TipoBairroPrestador=Bairro" + Environment.NewLine);
                        txt.Write("BairroPrestador=" + Validation.CleanStringForFiscal(_emitenteEndereco.Bairro) + Environment.NewLine);
                        txt.Write("CodigoCidadePrestador=" + Validation.CleanStringForFiscal(_emitenteEndereco.IBGE) + Environment.NewLine);
                        txt.Write("DescricaoCidadePrestador=" + Validation.CleanStringForFiscal(_emitenteEndereco.Cidade) + Environment.NewLine);
                        txt.Write("TelefonePrestador=" + Validation.CleanStringForFiscal(_emitenteContato.Telefone) + Environment.NewLine);
                        txt.Write("EmailPrestador=" + Validation.CleanStringForFiscal(_emitenteContato.Email) + Environment.NewLine);
                        txt.Write("CepPrestador=" + Validation.CleanStringForFiscal(_emitenteEndereco.Cep) + Environment.NewLine);
                        txt.Write(Environment.NewLine);

                        txt.Write("OptanteSimplesNacional=1" + Environment.NewLine);
                        txt.Write("IncentivadorCultural=2" + Environment.NewLine);
                        txt.Write("RegimeEspecialTributacao=0" + Environment.NewLine);
                        txt.Write("NaturezaTributacao=0" + Environment.NewLine);
                        txt.Write("IncentivoFiscal=2" + Environment.NewLine);
                        txt.Write("TipoTributacao=6" + Environment.NewLine);
                        txt.Write("ExigibilidadeISS=1" + Environment.NewLine);
                        txt.Write("Operacao=A" + Environment.NewLine);
                        txt.Write(Environment.NewLine);

                        txt.Write("CodigoItemListaServico=" + Settings.Default.empresa_codigoitemnfse + Environment.NewLine);
                        txt.Write("CodigoTributacaoMunicipio=" + Settings.Default.empresa_codigotributacaonfse + Environment.NewLine);
                        //txt.Write("CodigoCnae=" + Environment.NewLine);

                        int count = 1; string linha = "";
                        itens = new Model.PedidoItem().Query()
                            .Where("pedido_item.pedido", Pedido)
                            .Where("pedido_item.excluir", 0)
                            .Where("pedido_item.tipo", "Serviços")
                            .Get();
                        foreach (var data in itens)
                        {
                            linha = linha + count + "-" + Validation.CleanStringForFiscal(data.XPROD) + " ";
                            count++;
                        }

                        txt.Write("DiscriminacaoServico=" + linha.TrimEnd() + Environment.NewLine);
                        txt.Write("MunicipioIncidencia=" + Validation.CleanStringForFiscal(_emitenteEndereco.IBGE) + Environment.NewLine);
                        txt.Write("CodigoCidadePrestacao=" + Validation.CleanStringForFiscal(_emitenteEndereco.IBGE) + Environment.NewLine);
                        txt.Write("DescricaoCidadePrestacao=" + Validation.CleanStringForFiscal(_emitenteEndereco.Cidade) + Environment.NewLine);
                        txt.Write(Environment.NewLine);

                        txt.Write("CpfCnpjTomador=" + Validation.CleanStringForFiscal(_destinatario.CPF.Replace(".", "").Replace(" ", "")) + Environment.NewLine);
                        txt.Write("RazaoSocialTomador=" + Validation.CleanStringForFiscal(_destinatario.Nome) + Environment.NewLine);
                        //txt.Write("InscricaoEstadualTomador=" + Validation.CleanStringForFiscal(_destinatario.RG) + Environment.NewLine);
                        //txt.Write("InscricaoMunicipalTomador=" + Environment.NewLine);

                        if (_destinatarioEndereco != null)
                        {
                            txt.Write("TipoLogradouroTomador=RUA" + Environment.NewLine);
                            txt.Write("EnderecoTomador=" + Validation.CleanStringForFiscal(_destinatarioEndereco.Rua) + Environment.NewLine);
                            txt.Write("NumeroTomador=" + _destinatarioEndereco.Nr + Environment.NewLine);
                            //txt.Write("ComplementoTomador=SALA 909" + Environment.NewLine);
                            txt.Write("BairroTomador=" + Validation.CleanStringForFiscal(_destinatarioEndereco.Bairro) + Environment.NewLine);
                            txt.Write("CodigoCidadeTomador=" + _destinatarioEndereco.IBGE + Environment.NewLine);
                            txt.Write("DescricaoCidadeTomador=" + Validation.CleanStringForFiscal(_destinatarioEndereco.Cidade) + Environment.NewLine);
                            txt.Write("UfTomador=" + _destinatarioEndereco.Estado + Environment.NewLine);
                            txt.Write("CepTomador=" + Validation.CleanStringForFiscal(_destinatarioEndereco.Cep) + Environment.NewLine);
                            txt.Write("PaisTomador=1058" + Environment.NewLine);
                        }

                        if(_destinatarioContato != null)
                        {
                            //txt.Write("DDDTomador=044" + Environment.NewLine);
                            txt.Write("TelefoneTomador=" + Validation.CleanStringForFiscal(_destinatarioContato.Telefone) + Environment.NewLine);
                            txt.Write("EmailTomador=" + Validation.CleanStringForFiscal(_destinatarioContato.Email) + Environment.NewLine);
                        }
                                                
                        txt.Write(Environment.NewLine);

                        txt.Write("AliquotaPIS=0.00" + Environment.NewLine);
                        txt.Write("AliquotaCOFINS=0.00" + Environment.NewLine);
                        txt.Write("AliquotaINSS=0.00" + Environment.NewLine);
                        txt.Write("AliquotaIR=0.00" + Environment.NewLine);
                        txt.Write("AliquotaCSLL=0.00" + Environment.NewLine);
                        txt.Write("ValorPIS=0.00" + Environment.NewLine);
                        txt.Write("ValorCOFINS=0.00" + Environment.NewLine);
                        txt.Write("ValorINSS=0.00" + Environment.NewLine);
                        txt.Write("ValorIR=0.00" + Environment.NewLine);
                        txt.Write("ValorCSLL=0.00" + Environment.NewLine);
                        txt.Write("OutrasRetencoes=0.00" + Environment.NewLine);
                        txt.Write("DescontoIncondicionado=0.00" + Environment.NewLine);
                        txt.Write("DescontoCondicionado=0.00" + Environment.NewLine);
                        txt.Write("ValorDeducoes=0.00" + Environment.NewLine);
                        txt.Write(Environment.NewLine);

                        txt.Write("ValorServicos=" + Validation.FormatPriceWithDot(_pedido.Servicos) + Environment.NewLine);
                        txt.Write("BaseCalculo=" + Validation.FormatPriceWithDot(_pedido.Servicos) + Environment.NewLine);
                        txt.Write("AliquotaISS=0.00" + Environment.NewLine);
                        txt.Write("ValorIss=0.00" + Environment.NewLine);
                        txt.Write("IssRetido=2" + Environment.NewLine);
                        txt.Write("ValorISSRetido=0.00" + Environment.NewLine);
                        txt.Write("ValorLiquidoNfse=" + Validation.FormatPriceWithDot(_pedido.Servicos) + Environment.NewLine);
                        txt.Write("SALVARRPS" + Environment.NewLine);

                        txt.Close();

                        #endregion
                    }
                    else
                    {
                        #region PATH

                        strFilePath = _path_enviada + "\\" + Pedido + ".xml";

                        #endregion PATH

                        #region XML

                        var xml = new XmlTextWriter(strFilePath, Encoding.UTF8);
                        xml.Formatting = Formatting.Indented;

                        xml.WriteStartDocument(); //Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>

                        if (tipo == "NFCe")
                        {
                            cNF = "25" + getLastNFeNr().ToString("000000");
                            nNF = getLastNFeNr().ToString("000000000");
                            serie = Validation.ConvertToInt32(Settings.Default.empresa_nfe_serienfe).ToString("000");
                            chvAcesso = codUF(_emitenteEndereco.Estado) + _pedido.Emissao.ToString("yy") + _pedido.Emissao.ToString("MM") + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "65" + serie + nNF + "1" + cNF;
                            cDV = CalculoCDV(chvAcesso);
                            chvAcesso = chvAcesso + "" + cDV;

                            xml.WriteStartElement("NFe");

                            xml.WriteStartElement("infNFe");
                            xml.WriteAttributeString("versao", "4.00");
                            xml.WriteAttributeString("Id", "NFe" + chvAcesso);
                        }
                        else if (tipo == "CFe")
                        {
                            xml.WriteStartElement("CFe");
                            xml.WriteStartElement("infCFe");
                            xml.WriteAttributeString("versaoDadosEnt", layoutCFE);
                        }
                        else
                        {
                            cNF = "25" + getLastNFeNr().ToString("000000");
                            nNF = getLastNFeNr().ToString("000000000");
                            serie = Validation.ConvertToInt32(Settings.Default.empresa_nfe_serienfe).ToString("000");

                            chvAcesso = (codUF(_emitenteEndereco.Estado) + _pedido.Emissao.ToString("yy") + _pedido.Emissao.ToString("MM") + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "55" + serie + nNF + "1" + cNF).Replace(" ", "");
                            cDV = CalculoCDV(chvAcesso);
                            chvAcesso = chvAcesso + "" + cDV;

                            _nota.ChaveDeAcesso = chvAcesso;

                            xml.WriteStartElement("NFe");
                            xml.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");

                            xml.WriteStartElement("infNFe");
                            xml.WriteAttributeString("versao", "4.00");
                            xml.WriteAttributeString("Id", "NFe" + chvAcesso);
                        }

                        SetIde(xml, Pedido, tipo);

                        SetEmit(xml, Pedido, tipo);

                        SetDest(xml, Pedido, tipo);

                        int count = 1;
                        itens = new Model.PedidoItem().Query()
                            .Where("pedido_item.pedido", Pedido)
                            .Where("pedido_item.excluir", 0)
                            .Where("pedido_item.tipo", "Produtos")
                            .Get();
                        foreach (var data in itens)
                        {
                            SetDet(xml, Pedido, tipo, count, data.ID);
                            count++;
                        }

                        SetTotal(xml, Pedido, tipo);

                        if (tipo != "CFe")
                            SetTransp(xml, Pedido, tipo);

                        SetPag(xml, Pedido, tipo);

                        SetInfAdic(xml, Pedido, tipo);

                        xml.WriteEndElement();
                        xml.WriteEndElement();

                        xml.WriteEndDocument();

                        xml.Flush();
                        xml.Close();

                        #endregion XML
                    }

                    #endregion XML EMISSÃO

                    break;
            }
        }

        /// <summary>
        /// Transmitir XML
        /// </summary>
        /// <param name="tipo">NFe, NFCe, CFe</param>
        private void TransmitirXML(int Pedido, string tipo = "NFe")
        {
            Boolean done = false;            
            string arqPath = "";
            XmlDocument arq = new XmlDocument();

            if (tipo == "NFSe")
                arqPath = _path_enviada + "\\NFSe" + _pedido.Id + ".tx2";
            else
                arqPath = _path_enviada + "\\" + _pedido.Id + ".xml";

            if (!File.Exists(arqPath))
            {
                _msg = "Opss.. encontramos um erro: XML não encontrado.";
                return;
            }

            switch (tipo)
            {
                #region NFSE

                case "NFSe":
                    
                    _msg = RequestSend(File.ReadAllText(arqPath), "NFSe");

                    break;

                #endregion

                #region NFE

                case "NFe":

                    arq.Load(arqPath);

                    if (IniFile.Read("NFe", "APP") == "Uninfe")
                    {
                        #region UNINFE

                        uninfe_envia("envia", arqPath);

                        #endregion
                    }
                    else
                    {
                        #region TECNOSPEED 

                        _msg = RequestSend("FORMATO=XML" + Environment.NewLine + arq.OuterXml);

                        while (!String.IsNullOrEmpty(_msg) && done == false)
                        {
                            if (_msg.Contains("já existe no banco de dados. E não pode ser alterada pois ela está REGISTRADA."))
                                _msg = RequestResolve();
                            else if (_msg.Contains("Autorizado o uso") || _msg.Contains("já existe no banco de dados. E não pode ser alterada pois ela está AUTORIZADA."))
                            {
                                _msg = "Autorizado o uso da NF-e";
                                _nota.Tipo = tipo;
                                _nota.Status = "Autorizada";
                                _nota.nr_Nota = nNF;
                                _nota.Serie = serie;
                                _nota.ChaveDeAcesso = chvAcesso;
                                _nota.Save(_nota, false);

                                updateUltNfeAsync();

                                done = true;
                            }
                            else if(String.IsNullOrEmpty(_msg))
                            {
                                _msg = RequestConsult();
                                done = true;
                            }
                            else if (_msg.Contains("Duplicidade"))
                            {
                                _msg = RequestConsult();
                                done = true;
                            }

                            if (!String.IsNullOrEmpty(_msg))
                            {
                                _msg = _msg.Replace("EXCEPTION,EspdCertStoreException,", "").Replace(@"\delimiter", "");
                                done = true;
                            }
                        }

                        #endregion
                    }

                    break;

                #endregion NFE

                #region NFCE

                case "NFCe":

                    arq.Load(arqPath);

                    #region TECNOSPEED 

                    _msg = RequestSend("FORMATO=XML" + Environment.NewLine + arq.OuterXml, "NFCe");

                    while (!String.IsNullOrEmpty(_msg) && done == false)
                    {
                        if (_msg.Contains("já existe no banco de dados. E não pode ser alterada pois ela está REGISTRADA."))
                            _msg = RequestResolve();

                        if (_msg.Contains("Autorizado o uso") || _msg.Contains("já existe no banco de dados. E não pode ser alterada pois ela está AUTORIZADA."))
                        {
                            _msg = "Autorizado o uso da NF-e";
                            _nota.Tipo = tipo;
                            _nota.Status = "Autorizada";
                            _nota.nr_Nota = nNF;
                            _nota.Serie = serie;
                            _nota.ChaveDeAcesso = chvAcesso;
                            _nota.Save(_nota, false);

                            updateUltNfeAsync();

                            done = true;
                        }

                        switch (_msg)
                        {
                            case "":
                                _msg = RequestConsult();
                                done = true;
                                break;
                                /*default:
                                    _msg = "Opss..encontramos um erro: Sua requisição não foi processada.";
                                    done = true;
                                    break;*/
                        }

                        if (!String.IsNullOrEmpty(_msg))
                        {
                            _msg = _msg.Replace("EXCEPTION,EspdCertStoreException,", "").Replace(@"\delimiter", "");
                            done = true;
                        }
                    }

                    #endregion

                    break;

                #endregion NFCE

                #region CFE

                case "CFe":

                    arq.Load(arqPath);

                    string ChaveDeAcesso = "", nr_Nota = "", assinatura_qrcode = "";

                    new Log().Add("SAT", "| Pedido " + _pedido.Id + " | Tentativa de emissão |", Log.LogType.info);

                    Random rdn = new Random();
                    _msg = Sat.StringFromNativeUtf8(Sat.EnviarDadosVenda(rdn.Next(999999), GetCodAtivacao(), arq.OuterXml));
                    
                    StreamWriter txt = new StreamWriter(_path_enviada + "\\" + _pedido.Id + "_" + Validation.RandomSecurity() + ".txt", false, Encoding.UTF8);
                    txt.Write(_msg);
                    txt.Close();
                    
                    new Log().Add("SAT", "| Pedido " + _pedido.Id + " | Retorno | " + _msg, Log.LogType.info);

                    if (String.IsNullOrEmpty(_msg))
                    {                        
                        _nota.Status = "Falha";
                        _nota.Save(_nota, false);

                        _msg = "SAT não retornou mensagem. É possível que este cupom esteja autorizado ou não. Entre em contato com o suporte técnico!";

                        break;
                    }

                    if (_msg.Contains("Emitido com sucesso"))
                    {
                        if (!Directory.Exists(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00")))
                            Directory.CreateDirectory(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00"));

                        if (!Directory.Exists(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00")))
                            Directory.CreateDirectory(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00"));

                        XmlDocument oXML = new XmlDocument();
                        oXML.LoadXml(Base64ToString(Sep_Delimitador('|', 6, _msg)));

                        ChaveDeAcesso = oXML.SelectSingleNode("/CFe/infCFe").Attributes.GetNamedItem("Id").Value;
                        nr_Nota = oXML.SelectSingleNode("/CFe/infCFe/ide").ChildNodes[4].InnerText;
                        assinatura_qrcode = oXML.SelectSingleNode("/CFe/infCFe/ide").ChildNodes[11].InnerText;

                        _nota.Tipo = tipo;
                        _nota.Criado = DateTime.Now;
                        _nota.Status = "Autorizada";
                        _nota.nr_Nota = nr_Nota;
                        _nota.ChaveDeAcesso = ChaveDeAcesso;
                        _nota.assinatura_qrcode = assinatura_qrcode;
                        _nota.nr_serie_sat = IniFile.Read("N_Serie", "SAT");
                        _nota.Save(_nota, false);

                        var doc = XDocument.Parse(Base64ToString(Sep_Delimitador('|', 6, _msg)));
                        doc.Save(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + "\\" + ChaveDeAcesso + ".xml");

                        if (!Directory.Exists(_path_autorizada + "\\bkp\\"))
                            Directory.CreateDirectory(_path_autorizada + "\\bkp\\");

                        doc = XDocument.Parse(Base64ToString(Sep_Delimitador('|', 6, _msg)));
                        doc.Save(_path_autorizada + "\\bkp\\" + ChaveDeAcesso + ".xml");

                        _msg = "Emitido com sucesso + conteudo notas";
                    }

                    break;

                    #endregion CFE
            }
        }

        /// <summary>
        /// Validar XML
        /// </summary>
        /// <param name="tipo">NFe, NFCe, CFe</param>
        private void ValidarXML(int Pedido, string tipo = "NFe")
        {
            //Boolean done = false;
            var arq = new XmlDocument();
            string arqPath = _path_enviada + "\\" + _pedido.Id + ".xml";

            if (!File.Exists(arqPath))
            {
                _msg = "Opss.. encontramos um erro: XML não encontrado.";
                return;
            }

            arq.Load(arqPath);

            switch (tipo)
            {
                #region NFE

                case "NFCe":
                case "NFe":

                    #region TECNOSPEED 

                    _msg = RequestValidate("FORMATO=XML" + Environment.NewLine + arq.OuterXml, tipo);

                    //while (!String.IsNullOrEmpty(_msg) && done == false)
                    //{
                    //    if (_msg.Contains("já existe no banco de dados. E não pode ser alterada pois ela está REGISTRADA."))
                    //        _msg = RequestResolve();

                    //    if (_msg.Contains("Autorizado o uso") || _msg.Contains("já existe no banco de dados. E não pode ser alterada pois ela está AUTORIZADA."))
                    //    {
                    //        _msg = "Autorizado o uso da NF-e";
                    //        _nota.Tipo = tipo;
                    //        _nota.Status = "Autorizada";
                    //        _nota.nr_Nota = nNF;
                    //        _nota.Serie = serie;
                    //        _nota.ChaveDeAcesso = chvAcesso;
                    //        _nota.Save(_nota, false);

                    //        updateUltNfeAsync();

                    //        done = true;
                    //    }

                    //    switch (_msg)
                    //    {
                    //        case "":
                    //            _msg = RequestConsult();
                    //            done = true;
                    //            break;
                    //            /*default:
                    //                _msg = "Opss..encontramos um erro: Sua requisição não foi processada.";
                    //                done = true;
                    //                break;*/
                    //    }

                    //    if (!String.IsNullOrEmpty(_msg))
                    //    {
                    //        _msg = _msg.Replace("EXCEPTION,EspdCertStoreException,", "").Replace(@"\delimiter", "");
                    //        done = true;
                    //    }
                    //}

                    #endregion

                    break;

                    #endregion NFE
            }
        }

        /// <summary>
        /// Emitir CCe
        /// </summary>
        public string EmitirCCe(int Pedido, int Nota = 0)
        {
            Model.Nota _notaCCe = new Model.Nota();
            _notaCCe = _notaCCe.Query().Where("status", "Transmitindo...").Where("id_pedido", Pedido).Where("excluir", 0).FirstOrDefault<Model.Nota>();
            //_notaCCe = _notaCCe.Query().Where("id", Nota).FirstOrDefault<Model.Nota>();

            Start(_notaCCe.id_pedido, "NFe", Nota);

            string SeqEvento = "";

            if (_notaCCe.Serie == null)
            {
                SeqEvento = new Controller.Nota().GetSeqCCe(Pedido, Nota);
                _notaCCe.Serie = SeqEvento;
            }

            string arqPath = _path_enviada + "\\CCe" + _pedido.Id + ".tx2";

            if (File.Exists(arqPath))
            {
                File.Delete(arqPath);
            }

            #region TX2

            StreamWriter tx2 = new StreamWriter(arqPath, true, Encoding.UTF8);

            tx2.WriteLine("Documento=CCE");
            tx2.WriteLine("ChaveNota=" + _nota.ChaveDeAcesso);
            tx2.WriteLine("dhEvento=" + Validation.DateNowToSql() + "T" + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00"));
            tx2.WriteLine("Orgao=" + codUF(_emitenteEndereco.Estado));
            tx2.WriteLine("SeqEvento=" + SeqEvento);
            tx2.WriteLine("Correcao=" + _notaCCe.correcao);
            tx2.WriteLine("Lote=" + "0000000000" + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "5");
            tx2.WriteLine("Fuso=" + DateTime.Now.ToString("zzz"));

            tx2.Close();

            #endregion TX2

            if (!File.Exists(arqPath))
            {
                _msg = "Opss.. encontramos um erro: Arquivo não encontrado.";
                return _msg;
            }

            _msg = RequestSend(File.ReadAllText(arqPath));

            if (_msg.Contains("AUTORIZADA"))
            {
                _notaCCe.Status = "Autorizada";
                _notaCCe.Save(_notaCCe, false);
            }
            else
            {
                _notaCCe.Excluir = 1;
                _notaCCe.Save(_notaCCe, false);
            }

            return _msg;
        }

        /// <summary>
        /// Imprimir XML
        /// </summary>
        public string ImprimirCCe(int Pedido, int Nota)
        {
            //Start(Pedido);

            Model.Nota _notaCCe = new Model.Nota();
            _notaCCe = _notaCCe.Query().Where("id", Nota).FirstOrDefault<Model.Nota>();

            Start(_notaCCe.id_pedido, "NFe", Nota);


            //Model.Nota _notaCCe = new Model.Nota();
            //_notaCCe = _notaCCe.Query().Where("status", "Transmitindo...").Where("id_pedido", Pedido).Where("excluir", 0).FirstOrDefault<Model.Nota>();

            BrowserNfe browser = new BrowserNfe();
            var pdf = RequestPrint(_nota.ChaveDeAcesso, "CCe");

            new Log().Add("PRINTER", pdf, Log.LogType.info);

            if (pdf.Contains(".pdf"))
            {
                if (File.Exists(_path_autorizada + "\\CCe.pdf"))
                    File.Delete(_path_autorizada + "\\CCe.pdf");

                Thread.Sleep(1000);

                using (var client = new WebClient())
                {
                    client.DownloadFile(pdf, _path_autorizada + "\\CCe.pdf");
                }

                Thread.Sleep(1000);

                if (!File.Exists(_path_autorizada + "\\CCe.pdf"))
                    return "Arquivo não encontrado!";

                //BrowserNfe.Render = _path_autorizada + "\\CCe.pdf";
                //browser.ShowDialog();

                System.Diagnostics.Process.Start(_path_autorizada + "\\CCe.pdf");

                return "";
            }
            else
            {
                return pdf;
            }
        }

        /// <summary>
        /// Emitir Inutiliza
        /// </summary>
        public string EmitirInutiliza(int Nota = 0)
        {
            Model.Nota _inutiliza = new Model.Nota();
            _inutiliza = _inutiliza.Query().Where("status", "Transmitindo...").Where("tipo", "Inutiliza").Where("excluir", 0).FirstOrDefault<Model.Nota>();

            if (_inutiliza == null)
                return "";

            Start(0, "NFe");

            _msg = RequestSendInutiliza("&Ano=" + _inutiliza.Criado.ToString("yy") + "&Serie=" + _inutiliza.Serie + "&NFIni=" + _inutiliza.nr_Nota + "&NFFin=" + _inutiliza.assinatura_qrcode + "&Justificativa=" + _inutiliza.correcao);

            if (_msg.Contains("Inutilização de número homologado"))
            {
                _inutiliza.Status = "Autorizada";
                _inutiliza.Save(_inutiliza, false);
            }

            return _msg;
        }

        #region XML

        private int getLastNFeNr()
        {
            return Validation.ConvertToInt32(Settings.Default.empresa_nfe_ultnfe) + 1;
        }

        private int getLastNFSeNr()
        {
            return Validation.ConvertToInt32(Settings.Default.empresa_rps) + 1;
        }

        private string codUF(string Estado)
        {
            switch (Estado)
            {
                case "RO":
                    return "11";
                    break;

                case "AC":
                    return "12";
                    break;

                case "AM":
                    return "13";
                    break;

                case "RR":
                    return "14";
                    break;

                case "PA":
                    return "15";
                    break;

                case "AP":
                    return "16";
                    break;

                case "TO":
                    return "17";
                    break;

                case "MA":
                    return "21";
                    break;

                case "PI":
                    return "22";
                    break;

                case "CE":
                    return "23";
                    break;

                case "RN":
                    return "24";
                    break;

                case "PB":
                    return "25";
                    break;

                case "PE":
                    return "26";
                    break;

                case "AL":
                    return "27";
                    break;

                case "SE":
                    return "28";
                    break;

                case "BA":
                    return "29";
                    break;

                case "MG":
                    return "31";
                    break;

                case "ES":
                    return "32";
                    break;

                case "RJ":
                    return "33";
                    break;

                case "SP":
                    return "35";
                    break;

                case "PR":
                    return "41";
                    break;

                case "SC":
                    return "42";
                    break;

                case "RS":
                    return "43";
                    break;

                case "MT":
                    return "51";
                    break;

                case "GO":
                    return "52";
                    break;

                case "DF":
                    return "53";
                    break;
            }

            return "0";
        }

        private void SetIde(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("ide");

            if (tipo == "CFe")
            {
                if (_servidorCFE == 2)
                {
                    xml.WriteElementString("CNPJ", "16716114000172"); //TANCA
                    xml.WriteElementString("signAC", "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT");
                    xml.WriteElementString("numeroCaixa", "001");
                }
                else
                {
                    xml.WriteElementString("CNPJ", "05681389000100"); //SH
                    xml.WriteElementString("signAC", assinaturaCFE);
                    xml.WriteElementString("numeroCaixa", "001");
                }
            }
            else
            {
                xml.WriteElementString("cUF", codUF(_emitenteEndereco.Estado));

                xml.WriteElementString("cNF", cNF);
                xml.WriteElementString("natOp", _natureza.Nome);

                if (tipo == "NFe")
                    xml.WriteElementString("mod", "55");
                else if (tipo == "NFCe")
                    xml.WriteElementString("mod", "65");

                //2019-10-08T08:52:34-03:00
                string horaSaida = "";
                if (string.IsNullOrEmpty(_pedido.HoraSaida))
                {
                    horaSaida = DateTime.Now.ToString("HH:mm:ss");
                }
                else
                {
                    horaSaida = _pedido.HoraSaida + ":00";
                }

                xml.WriteElementString("serie", string.IsNullOrEmpty(Settings.Default.empresa_nfe_serienfe) ? "0" : Settings.Default.empresa_nfe_serienfe);
                xml.WriteElementString("nNF", getLastNFeNr().ToString());
                xml.WriteElementString("dhEmi", Validation.ConvertDateToSql(_pedido.Emissao) + "T" + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + DateTime.Now.ToString("zzz"));

                if (tipo != "NFCe")
                    xml.WriteElementString("dhSaiEnt", Validation.ConvertDateToSql(_pedido.Emissao) + "T" + horaSaida + DateTime.Now.ToString("zzz"));

                //xml.WriteElementString("tpNF", _pedido.TipoNFe > 0 ? _pedido.TipoNFe.ToString() : "1");
                xml.WriteElementString("tpNF", _pedido.TipoNFe.ToString());
                xml.WriteElementString("idDest", _pedido.Destino > 0 ? _pedido.Destino.ToString() : "1");
                xml.WriteElementString("cMunFG", Settings.Default.empresa_ibge);

                if (tipo == "NFe")
                    xml.WriteElementString("tpImp", "1");
                else if (tipo == "NFCe")
                    xml.WriteElementString("tpImp", "4");

                xml.WriteElementString("tpEmis", "1");
                xml.WriteElementString("cDV", cDV);
                xml.WriteElementString("tpAmb", _servidorNFE.ToString());
                xml.WriteElementString("finNFe", _pedido.Finalidade > 0 ? _pedido.Finalidade.ToString() : "1");
                xml.WriteElementString("indFinal", "1");
                xml.WriteElementString("indPres", "1");
                xml.WriteElementString("procEmi", "0");
                xml.WriteElementString("verProc", "EMIPLUS");

                SetNFref(xml, Pedido, tipo);
            }

            xml.WriteEndElement();
        }

        private int GetUltNFE(int id_empresa)
        {
            //verifica o ultimo cadastrado no empresa e depois consulta a ultima emitida
            return 1;
        }

        private void SetNFref(XmlTextWriter xml, int Pedido, string tipo)
        {
            var notas = new Model.Nota().Query().Select("chavedeacesso").Where("Nota.id_pedido", Pedido).Where("Nota.excluir", 0).Where("Nota.tipo", "Documento").OrderBy("Nota.id").Get();
            if (notas != null)
            {
                foreach (var data in notas)
                {
                    xml.WriteStartElement("NFref");
                    xml.WriteElementString("refNFe", data.CHAVEDEACESSO);
                    xml.WriteEndElement();
                }
            }
        }

        private void SetEmit(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("emit");

            if (tipo == "CFe")
            {
                if (_servidorCFE == 2)
                {
                    xml.WriteElementString("CNPJ", "08723218000186");
                    xml.WriteElementString("IE", "149626224113");
                }
                else
                {
                    xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", ""));
                    xml.WriteElementString("IE", Validation.CleanStringForFiscal(_emitente.RG).Replace(".", ""));
                }

                xml.WriteElementString("indRatISSQN", "N");
            }
            else
            {
                xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", ""));
                xml.WriteElementString("xNome", Validation.CleanStringForFiscal(_emitente.Nome));
                xml.WriteElementString("xFant", Validation.CleanStringForFiscal(_emitente.Fantasia));

                xml.WriteStartElement("enderEmit");

                xml.WriteElementString("xLgr", Validation.CleanStringForFiscal(_emitenteEndereco.Rua));
                xml.WriteElementString("nro", Validation.CleanStringForFiscal(_emitenteEndereco.Nr));
                xml.WriteElementString("xBairro", Validation.CleanStringForFiscal(_emitenteEndereco.Bairro));
                xml.WriteElementString("cMun", Validation.CleanStringForFiscal(_emitenteEndereco.IBGE));
                xml.WriteElementString("xMun", Validation.CleanStringForFiscal(_emitenteEndereco.Cidade));
                xml.WriteElementString("UF", Validation.CleanStringForFiscal(_emitenteEndereco.Estado));
                xml.WriteElementString("CEP", Validation.CleanStringForFiscal(_emitenteEndereco.Cep));
                xml.WriteElementString("cPais", "1058");
                xml.WriteElementString("xPais", "BRASIL");
                //xml.WriteElementString("fone", "");

                xml.WriteEndElement();

                xml.WriteElementString("IE", Validation.CleanStringForFiscal(_emitente.RG).Replace(".", ""));

                //if (tipo == "NFCe")
                //{
                //    xml.WriteElementString("IM", "");
                //    xml.WriteElementString("CNAE", "");
                //}

                xml.WriteElementString("CRT", Settings.Default.empresa_crt);
            }

            xml.WriteEndElement();
        }

        private void SetDest(XmlTextWriter xml, int Pedido, string tipo)
        {
            if (tipo != "NFCe")
            {

                xml.WriteStartElement("dest");

                if (tipo == "CFe")
                {
                    if (!String.IsNullOrEmpty(_pedido.cfe_cpf))
                    {
                        if (_pedido.cfe_cpf.Length == 11)
                        {
                            xml.WriteElementString("CPF", Validation.CleanStringForFiscal(_pedido.cfe_cpf).Replace(".", "").Replace(" ", ""));
                        }
                        else
                        {
                            xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_pedido.cfe_cpf).Replace(".", "").Replace(" ", ""));
                        }
                    }
                }
                else
                {
                    if (_destinatario.Pessoatipo == "Física")
                    {
                        if (_destinatario.CPF == null || Validation.CleanStringForFiscal(_destinatario.CPF).Replace(".", "").Replace(" ", "") == "")
                            xml.WriteElementString("CPF", "00000000000");
                        else
                            xml.WriteElementString("CPF", Validation.CleanStringForFiscal(_destinatario.CPF).Replace(".", "").Replace(" ", ""));
                    }
                    else
                    {
                        if (_destinatario.CPF == null || Validation.CleanStringForFiscal(_destinatario.CPF).Replace(".", "").Replace(" ", "") == "")
                            xml.WriteElementString("CNPJ", "00000000000000");
                        else
                            xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_destinatario.CPF).Replace(".", "").Replace(" ", ""));
                    }

                    if (Settings.Default.empresa_nfe_servidornfe.ToString() == "2")
                    {
                        xml.WriteElementString("xNome", "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL");
                    }
                    else
                    {
                        xml.WriteElementString("xNome", Validation.CleanStringForFiscal(_destinatario.Nome));
                    }

                    xml.WriteStartElement("enderDest");

                    xml.WriteElementString("xLgr", Validation.CleanStringForFiscal(_destinatarioEndereco.Rua));
                    xml.WriteElementString("nro", Validation.CleanStringForFiscal(_destinatarioEndereco.Nr));
                    xml.WriteElementString("xBairro", Validation.CleanStringForFiscal(_destinatarioEndereco.Bairro));
                    xml.WriteElementString("cMun", Validation.CleanStringForFiscal(_destinatarioEndereco.IBGE));
                    xml.WriteElementString("xMun", Validation.CleanStringForFiscal(_destinatarioEndereco.Cidade));
                    xml.WriteElementString("UF", Validation.CleanStringForFiscal(_destinatarioEndereco.Estado));
                    xml.WriteElementString("CEP", Validation.CleanStringForFiscal(_destinatarioEndereco.Cep));
                    xml.WriteElementString("cPais", "1058");
                    xml.WriteElementString("xPais", "BRASIL");
                    //xml.WriteElementString("fone", "");

                    xml.WriteEndElement();

                    if (_destinatario.Isento == 1)
                        xml.WriteElementString("indIEDest", "2");
                    else if (_destinatario.Pessoatipo == "Física")
                        xml.WriteElementString("indIEDest", "9");
                    else
                        xml.WriteElementString("indIEDest", "1");

                    if (_destinatario.Isento != 1 && _destinatario.Pessoatipo == "Jurídica")
                        xml.WriteElementString("IE", Validation.CleanStringForFiscal(_destinatario.RG));
                }

                xml.WriteEndElement();

            }
        }

        private void SetRetirada(XmlTextWriter xml, int Pedido, string tipo)
        {
        }

        private void SetEntrega(XmlTextWriter xml, int Pedido, string tipo)
        {
        }

        private void SetDet(XmlTextWriter xml, int Pedido, string tipo, int n, int id)
        {
            double totalTrib = 0;
            _pedidoItem = new Model.PedidoItem().FindById(id).First<Model.PedidoItem>();

            xml.WriteStartElement("det");

            xml.WriteAttributeString("nItem", n.ToString());

            #region prod

            xml.WriteStartElement("prod");

            if (tipo == "CFe")
            {
                xml.WriteElementString("cProd", Validation.CleanStringForFiscal(_pedidoItem.Id.ToString()));

                //if (!String.IsNullOrEmpty(_pedidoItem.CEan))
                //{
                //    xml.WriteElementString("cEAN", Validation.CleanStringForFiscal(_pedidoItem.CEan));
                //}
                //else
                //{
                //    xml.WriteElementString("cEAN", "SEM GTIN");
                //}

                xml.WriteElementString("xProd", Validation.CleanStringForFiscal(_pedidoItem.xProd));
                xml.WriteElementString("NCM", Validation.CleanStringForFiscal(_pedidoItem.Ncm));
                if (!String.IsNullOrEmpty(_pedidoItem.Cest))
                    xml.WriteElementString("CEST", Validation.CleanStringForFiscal(_pedidoItem.Cest));
                xml.WriteElementString("CFOP", Validation.CleanStringForFiscal(_pedidoItem.Cfop));
                xml.WriteElementString("uCom", _pedidoItem.Medida);
                xml.WriteElementString("qCom", Validation.FormatPriceWithDot(_pedidoItem.Quantidade, 4));
                xml.WriteElementString("vUnCom", Validation.FormatPriceWithDot(_pedidoItem.ValorVenda, 2));
                xml.WriteElementString("indRegra", "A");
                xml.WriteElementString("vDesc", Validation.FormatPriceWithDot(_pedidoItem.Desconto, 2));
                xml.WriteElementString("vOutro", Validation.FormatPriceWithDot(_pedidoItem.Frete, 2));
                //if(_pedidoItem.Desconto > 0)
                //    xml.WriteElementString("vDesc", Validation.FormatPriceWithDot(_pedidoItem.Desconto));
            }
            else
            {
                xml.WriteElementString("cProd", Validation.CleanStringForFiscal(_pedidoItem.CProd));

                //if (!String.IsNullOrEmpty(_pedidoItem.CEan))
                //{
                //    xml.WriteElementString("cEAN", Validation.CleanStringForFiscal(_pedidoItem.CEan));
                //}
                //else
                //{
                //    xml.WriteElementString("cEAN", "SEM GTIN");
                //}
                xml.WriteElementString("cEAN", "SEM GTIN");

                if (tipo == "NFCe" && _servidorNFCE == 2)
                    xml.WriteElementString("xProd", "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL");
                else
                    xml.WriteElementString("xProd", Validation.CleanStringForFiscal(_pedidoItem.xProd));

                xml.WriteElementString("NCM", Validation.CleanStringForFiscal(_pedidoItem.Ncm));
                if (!String.IsNullOrEmpty(_pedidoItem.Cest))
                    xml.WriteElementString("CEST", Validation.CleanStringForFiscal(_pedidoItem.Cest));

                xml.WriteElementString("CFOP", Validation.CleanStringForFiscal(_pedidoItem.Cfop));
                xml.WriteElementString("uCom", _pedidoItem.Medida);
                xml.WriteElementString("qCom", Validation.FormatPriceWithDot(_pedidoItem.Quantidade, 2));
                xml.WriteElementString("vUnCom", Validation.FormatPriceWithDot(_pedidoItem.ValorVenda, 2));

                xml.WriteElementString("vProd", Validation.FormatPriceWithDot(_pedidoItem.Total + _pedidoItem.Desconto));

                //if(!String.IsNullOrEmpty(_pedidoItem.CEan))
                //{
                //    xml.WriteElementString("cEANTrib", Validation.CleanStringForFiscal(_pedidoItem.CEan));
                //}
                //else
                //{
                //    xml.WriteElementString("cEANTrib", "SEM GTIN");
                //}
                xml.WriteElementString("cEANTrib", "SEM GTIN");

                xml.WriteElementString("uTrib", _pedidoItem.Medida);
                xml.WriteElementString("qTrib", Validation.FormatPriceWithDot(_pedidoItem.Quantidade, 2));
                xml.WriteElementString("vUnTrib", Validation.FormatPriceWithDot(_pedidoItem.ValorVenda, 2));

                if(_pedidoItem.Desconto > 0)
                    xml.WriteElementString("vDesc", Validation.FormatPriceWithDot(_pedidoItem.Desconto, 2));

                xml.WriteElementString("indTot", "1");

                //xml.WriteElementString("xPed", "");
                //xml.WriteElementString("nItemPed", "");
            }

            xml.WriteEndElement();//prod

            #endregion prod

            #region vTotTrib

            xml.WriteStartElement("imposto");

            if (tipo != "CFe")
            {
                totalTrib = _pedidoItem.Federal + _pedidoItem.Estadual + _pedidoItem.Municipal;
                xml.WriteElementString("vTotTrib", Validation.FormatPriceWithDot(totalTrib));
            }

            #endregion vTotTrib

            #region ICMS

            xml.WriteStartElement("ICMS");

            if (String.IsNullOrEmpty(_pedidoItem.Icms))
                _pedidoItem.Icms = "0";

            if (_pedidoItem.Icms.Length == 3)
            {
                if (tipo == "CFe" && _pedidoItem.Icms == "500")
                    xml.WriteStartElement("ICMSSN102");
                else if (tipo == "NFe" && _pedidoItem.Icms == "400")
                    xml.WriteStartElement("ICMSSN102");
                else
                    xml.WriteStartElement("ICMSSN" + _pedidoItem.Icms);
            }
            else
                xml.WriteStartElement("ICMS" + _pedidoItem.Icms);

            if (tipo == "NFe" || tipo == "NFCe")
                xml.WriteElementString("orig", _pedidoItem.Origem);
            else
                xml.WriteElementString("Orig", _pedidoItem.Origem);

            switch (_pedidoItem.Icms)
            {
                case "00":
                    xml.WriteElementString("CST", "00");
                    xml.WriteElementString("modBC", "0");
                    xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.IcmsBase));
                    xml.WriteElementString("pICMS", Validation.FormatPriceWithDot(_pedidoItem.IcmsAliq));
                    xml.WriteElementString("vICMS", Validation.FormatPriceWithDot(_pedidoItem.IcmsVlr));
                    break;

                case "40":
                    xml.WriteElementString("CST", "40");
                    break;

                case "41":
                    xml.WriteElementString("CST", "41");
                    break;

                case "50":
                    xml.WriteElementString("CST", "50");
                    break;

                case "60":
                    xml.WriteElementString("CST", "60");
                    break;

                case "90":
                    xml.WriteElementString("CST", "90");
                    break;

                case "101":
                    xml.WriteElementString("CSOSN", "101");
                    xml.WriteElementString("pCredSN", Validation.FormatPriceWithDot(_pedidoItem.Icms101Aliq));
                    xml.WriteElementString("vCredICMSSN", Validation.FormatPriceWithDot(_pedidoItem.Icms101Vlr));
                    break;

                case "102":
                    xml.WriteElementString("CSOSN", "102");
                    break;

                case "201":
                    xml.WriteElementString("CSOSN", "201");
                    xml.WriteElementString("modBCST", "0");
                    xml.WriteElementString("pMVAST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("pRedBCST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vBCST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("pICMSST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vICMSST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("pCredSN", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vCredICMSSN", Validation.FormatPriceWithDot(0));
                    break;

                case "202":
                    xml.WriteElementString("CSOSN", "202");
                    xml.WriteElementString("modBCST", "0");
                    xml.WriteElementString("pMVAST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("pRedBCST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vBCST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("pICMSST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vICMSST", Validation.FormatPriceWithDot(0));
                    break;

                case "400":
                    xml.WriteElementString("CSOSN", "400");
                    break;

                case "500":
                    xml.WriteElementString("CSOSN", "500");
                    if (tipo != "CFe")
                    {
                        //xml.WriteElementString("vBCSTRet", Validation.FormatPriceWithDot(0));
                        //xml.WriteElementString("vICMSSTRet", Validation.FormatPriceWithDot(0));
                    }
                    break;

                case "900":
                    xml.WriteElementString("CSOSN", "900");
                    xml.WriteElementString("modBC", "0");
                    xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.IcmsBase));
                    xml.WriteElementString("pICMS", Validation.FormatPriceWithDot(_pedidoItem.IcmsAliq));
                    xml.WriteElementString("vICMS", Validation.FormatPriceWithDot(_pedidoItem.IcmsVlr));
                    break;
            }

            xml.WriteEndElement();

            xml.WriteEndElement();//ICMS

            #endregion ICMS

            #region IPI

            //if (String.IsNullOrEmpty(_pedidoItem.Ipi))
            //    _pedidoItem.Ipi = "0";

            if (!String.IsNullOrEmpty(_pedidoItem.Ipi) && _pedidoItem.Ipi != "0")
            {
                xml.WriteStartElement("IPI");

                xml.WriteElementString("cEnq", "999");

                xml.WriteStartElement("IPITrib");

                xml.WriteElementString("CST", _pedidoItem.Ipi);
                xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.Total));
                xml.WriteElementString("pIPI", Validation.FormatPriceWithDot(_pedidoItem.IpiAliq));
                //xml.WriteElementString("qUnid", qcom);
                //xml.WriteElementString("vUnid", vprod);
                xml.WriteElementString("vIPI", Validation.FormatPriceWithDot(_pedidoItem.IpiVlr));

                xml.WriteEndElement(); //IPITrib

                xml.WriteEndElement();//IPI
            }

            #endregion IPI

            #region II

            //xml.WriteStartElement("II");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//II

            #endregion II

            #region PIS

            //if (String.IsNullOrEmpty(_pedidoItem.Pis))
            //    _pedidoItem.Pis = "0";

            xml.WriteStartElement("PIS");

            switch (_pedidoItem.Pis)
            {
                case "01":
                    xml.WriteStartElement("PISAliq");
                    xml.WriteElementString("CST", _pedidoItem.Pis);
                    xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.Total));
                    xml.WriteElementString("pPIS", Validation.FormatPriceWithDot(_pedidoItem.PisAliq));
                    xml.WriteElementString("vPIS", Validation.FormatPriceWithDot(_pedidoItem.PisVlr));
                    xml.WriteEndElement(); //PISAliq
                    break;

                case "08":
                    xml.WriteStartElement("PISNT");
                    xml.WriteElementString("CST", _pedidoItem.Pis);
                    xml.WriteEndElement(); //PISNT
                    break;

                case "49":
                case "99":
                    xml.WriteStartElement("PISOutr");
                    xml.WriteElementString("CST", _pedidoItem.Pis);
                    xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.Total));
                    xml.WriteElementString("pPIS", Validation.FormatPriceWithDot(_pedidoItem.PisAliq));
                    xml.WriteElementString("vPIS", Validation.FormatPriceWithDot(_pedidoItem.PisVlr));
                    xml.WriteEndElement(); //PISOutr
                    break;

                default:
                    xml.WriteStartElement("PISNT");
                    xml.WriteElementString("CST", "08");
                    xml.WriteEndElement(); //PISNT
                    break;
            }

            xml.WriteEndElement();//PIS

            #endregion PIS

            #region PIS ST

            //xml.WriteStartElement("PISST");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//PISST

            #endregion PIS ST

            #region COFINS

            //if (String.IsNullOrEmpty(_pedidoItem.Cofins))
            //    _pedidoItem.Cofins = "0";

            xml.WriteStartElement("COFINS");

            switch (_pedidoItem.Cofins)
            {
                case "01":
                    xml.WriteStartElement("COFINSAliq");
                    xml.WriteElementString("CST", _pedidoItem.Cofins);
                    xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.Total));
                    xml.WriteElementString("pCOFINS", Validation.FormatPriceWithDot(_pedidoItem.CofinsAliq));
                    xml.WriteElementString("vCOFINS", Validation.FormatPriceWithDot(_pedidoItem.CofinsVlr));
                    xml.WriteEndElement(); //COFINSAliq
                    break;

                case "08":
                    xml.WriteStartElement("COFINSNT");
                    xml.WriteElementString("CST", _pedidoItem.Cofins);
                    xml.WriteEndElement(); //COFINSNT
                    break;

                case "49":
                case "99":
                    xml.WriteStartElement("COFINSOutr");
                    xml.WriteElementString("CST", _pedidoItem.Cofins);
                    xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.Total));
                    xml.WriteElementString("pCOFINS", Validation.FormatPriceWithDot(_pedidoItem.CofinsAliq));
                    xml.WriteElementString("vCOFINS", Validation.FormatPriceWithDot(_pedidoItem.CofinsVlr));
                    xml.WriteEndElement(); //COFINSOutr
                    break;

                default:
                    xml.WriteStartElement("COFINSNT");
                    xml.WriteElementString("CST", "08");
                    xml.WriteEndElement(); //COFINSNT
                    break;
            }

            xml.WriteEndElement();//COFINS

            #endregion COFINS

            #region COFINS ST

            //xml.WriteStartElement("COFINSST");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//COFINSST

            #endregion COFINS ST

            xml.WriteEndElement();//imposto

            #region infAdProd

            if (totalTrib > 0)
                xml.WriteElementString("infAdProd", "VALOR APROXIMADO DOS TRIBUTOS: FEDERAL: R$ " + Validation.FormatPriceWithDot(_pedidoItem.Federal) + " - ESTADUAL: R$ " + Validation.FormatPriceWithDot(_pedidoItem.Estadual) + " - MUNICIPAL: R$ " + Validation.FormatPriceWithDot(_pedidoItem.Municipal) + " - FONTE: IBPT");

            #endregion infAdProd

            xml.WriteEndElement();//det
        }

        private void SetTotal(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("total");

            if (tipo != "CFe")
            {
                var vNF = _pedido.Total;
                var vServicos = _pedido.Servicos;

                vNF = Validation.Round(vNF - vServicos);

                xml.WriteStartElement("ICMSTot");

                xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedido.ICMSBASE));
                xml.WriteElementString("vICMS", Validation.FormatPriceWithDot(_pedido.ICMS));
                xml.WriteElementString("vICMSDeson", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vFCP", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vBCST", Validation.FormatPriceWithDot(_pedido.ICMSSTBASE));
                xml.WriteElementString("vST", Validation.FormatPriceWithDot(_pedido.ICMSST));
                xml.WriteElementString("vFCPST", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vFCPSTRet", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vProd", Validation.FormatPriceWithDot(_pedido.Produtos));
                xml.WriteElementString("vFrete", Validation.FormatPriceWithDot(_pedido.Frete));
                xml.WriteElementString("vSeg", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vDesc", Validation.FormatPriceWithDot(_pedido.Desconto));
                xml.WriteElementString("vII", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vIPI", Validation.FormatPriceWithDot(_pedido.IPI));
                xml.WriteElementString("vIPIDevol", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vPIS", Validation.FormatPriceWithDot(_pedido.PIS));
                xml.WriteElementString("vCOFINS", Validation.FormatPriceWithDot(_pedido.COFINS));
                xml.WriteElementString("vOutro", Validation.FormatPriceWithDot(0));
                xml.WriteElementString("vNF", Validation.FormatPriceWithDot(vNF));

                var query = _pedidoItem
                    .Query()
                    .SelectRaw("SUM(Federal) AS Federal, SUM(Estadual) AS Estadual, SUM(Municipal) AS Municipal")
                    .Where("pedido", Pedido)
                    .Where("excluir", 0)
                    .Get();

                foreach (var data in query)
                {
                    xml.WriteElementString("vTotTrib", Validation.FormatPriceWithDot(Validation.ConvertToDouble(data.FEDERAL) + Validation.ConvertToDouble(data.ESTADUAL) + Validation.ConvertToDouble(data.MUNICIPAL)));
                }

                xml.WriteEndElement();//ICMSTot
            }

            xml.WriteEndElement();//total
        }

        private void SetTransp(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("transp");

            if (tipo != "NFCe")
                xml.WriteElementString("modFrete", _pedido.TipoFrete.ToString());
            else
                xml.WriteElementString("modFrete", "9");

            if (_pedido.Id_Transportadora > 0)
            {
                _transportadora = new Model.Pessoa().FindById(_pedido.Id_Transportadora).FirstOrDefault<Model.Pessoa>();
                _transportadoraEndereco = new Model.PessoaEndereco().FindByIdUser(_pedido.Id_Transportadora).FirstOrDefault<Model.PessoaEndereco>();

                xml.WriteStartElement("transporta");

                if (_transportadora.Pessoatipo == "Física")
                {
                    xml.WriteElementString("CPF", Validation.CleanStringForFiscal(_transportadora.CPF).Replace(".", "").Replace(" ", ""));
                }
                else
                {
                    xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_transportadora.CPF).Replace(".", "").Replace(" ", ""));
                }

                if (!String.IsNullOrEmpty(_transportadora.Nome))
                {
                    xml.WriteElementString("xNome", Validation.CleanStringForFiscal(_transportadora.Nome));
                }

                if (_transportadoraEndereco != null)
                {
                    if (!String.IsNullOrEmpty(_transportadoraEndereco.Rua))
                    {
                        //xml.WriteElementString("xEnder", Validation.CleanStringForFiscal(_transportadoraEndereco.Rua) + " - " + _transportadoraEndereco.Nr + " - " + Validation.CleanStringForFiscal(_transportadoraEndereco.Bairro));
                        xml.WriteElementString("xEnder", Validation.CleanStringForFiscal(_transportadoraEndereco.Rua) + " - " + _transportadoraEndereco.Nr + " - " + Validation.CleanStringForFiscal(_transportadoraEndereco.Bairro));
                    }

                    if (!String.IsNullOrEmpty(_transportadoraEndereco.Cidade))
                    {
                        xml.WriteElementString("xMun", Validation.CleanStringForFiscal(_transportadoraEndereco.Cidade));
                    }

                    if (!String.IsNullOrEmpty(_transportadoraEndereco.Estado))
                    {
                        xml.WriteElementString("UF", _transportadoraEndereco.Estado);
                    }
                }

                xml.WriteEndElement();//transporta

                if (!String.IsNullOrEmpty(_transportadora.Transporte_placa))
                {
                    xml.WriteStartElement("veicTransp");

                    xml.WriteElementString("placa", Validation.CleanStringForFiscal(_transportadora.Transporte_placa));
                    xml.WriteElementString("UF", _transportadora.Transporte_uf);

                    if (!String.IsNullOrEmpty(_transportadora.Transporte_rntc))
                    {
                        xml.WriteElementString("RNTC", _transportadora.Transporte_rntc);
                    }

                    xml.WriteEndElement();//veicTransp
                }
            }

            if (!String.IsNullOrEmpty(_pedido.Volumes_Frete))
            {
                xml.WriteStartElement("vol");

                xml.WriteElementString("qVol", _pedido.Volumes_Frete);

                if (!String.IsNullOrEmpty(_pedido.Especie_Frete))
                    xml.WriteElementString("esp", _pedido.Especie_Frete);

                if (!String.IsNullOrEmpty(_pedido.Marca_Frete))
                    xml.WriteElementString("marca", _pedido.Marca_Frete);

                xml.WriteElementString("pesoL", Validation.FormatPriceWithDot(_pedido.PesoLiq_Frete, 3));
                xml.WriteElementString("pesoB", Validation.FormatPriceWithDot(_pedido.PesoBruto_Frete, 3));

                xml.WriteEndElement();//vol
            }

            xml.WriteEndElement();//transp
        }

        private void SetPag(XmlTextWriter xml, int Pedido, string tipo)
        {
            if (tipo != "CFe")
            {
                double total = 0; int countFat = 0;

                pagamentos = new Model.Titulo().Query().Select("Total").Where("titulo.id_pedido", Pedido).Where("titulo.excluir", 0).Get();
                foreach (var data in pagamentos)
                {
                    total = total + Validation.ConvertToDouble(data.TOTAL);
                }

                if (_pedido.Total != total) //SE O PAGAMENTO LANÇADO NÃO FOI IGUAL AO TOTAL DO PEDIDO NÃO INFORMA PAGAMENTO
                {
                    xml.WriteStartElement("pag");
                    SetDetPag(xml);
                    xml.WriteEndElement();//pag
                    return;
                }

                #region FATURA + PARCELAS

                pagamentos = new Model.Titulo().Query().Where("titulo.id_pedido", Pedido).Where("titulo.excluir", 0).OrderBy("titulo.vencimento").Get();
                if (pagamentos.Count() > 0)
                {
                    countFat = 1;
                    xml.WriteStartElement("cobr");
                    SetDetPag(xml, "", Validation.FormatPriceWithDot(_pedido.Total), 1, _pedido.Id.ToString());
                }

                int count = 1;
                pagamentos = new Model.Titulo().Query().Where("titulo.id_pedido", Pedido).Where("titulo.excluir", 0).OrderBy("titulo.vencimento").Get();
                if (pagamentos.Count() > 0)
                {
                    if (countFat == 0)
                    {
                        countFat = 1;
                        xml.WriteStartElement("cobr");
                    }

                    foreach (var data in pagamentos)
                    {
                        SetDetPag(xml, data.ID_FORMAPGTO.ToString("00"), Validation.FormatPriceWithDot(data.TOTAL), 2, count.ToString("000"), Validation.ConvertDateToSql(data.VENCIMENTO));
                        count++;
                    }
                }

                #endregion FATURA + PARCELAS

                if (countFat == 1)
                {
                    xml.WriteEndElement();//cobr
                }
            }

            #region PAGAMENTOS

            if (tipo != "CFe")
            {
                pagamentos = new Model.Titulo().Query().Where("titulo.id_pedido", Pedido).Where("titulo.excluir", 0).Get();
                xml.WriteStartElement("pag");
            }
            else
            {
                pagamentos = new Model.Titulo().Query().Select("titulo.id_formapgto as id_formapgto").SelectRaw("SUM(titulo.total) as total").Where("titulo.id_pedido", Pedido).Where("titulo.excluir", 0).GroupBy("titulo.id_formapgto").Get();
                xml.WriteStartElement("pgto");
            }

            foreach (var data in pagamentos)
            {
                SetDetPag(xml, data.ID_FORMAPGTO.ToString("00"), Validation.FormatPriceWithDot(data.TOTAL), tipo == "CFe" ? 3 : 0);
            }

            #endregion PAGAMENTOS

            xml.WriteEndElement();//pag ou pgto
        }

        private void SetDetPag(XmlTextWriter xml, string formaPgto = "90", string valor = "0.00", int tag = 0, string n = "", string venc = "")
        {
            switch (tag)
            {
                case 1:
                    xml.WriteStartElement("fat");
                    xml.WriteElementString("nFat", n);
                    xml.WriteElementString("vOrig", valor);
                    xml.WriteElementString("vDesc", "0.00");
                    xml.WriteElementString("vLiq", valor);
                    xml.WriteEndElement();//fat
                    break;

                case 2:
                    xml.WriteStartElement("dup");
                    xml.WriteElementString("nDup", n);
                    xml.WriteElementString("dVenc", venc);
                    xml.WriteElementString("vDup", valor);
                    xml.WriteEndElement();//dup
                    break;

                case 3:
                    xml.WriteStartElement("MP");
                    xml.WriteElementString("cMP", formaPgto == "06" ? "99" : formaPgto);
                    xml.WriteElementString("vMP", valor);
                    xml.WriteEndElement();//MP
                    break;

                default:
                    xml.WriteStartElement("detPag");
                    xml.WriteElementString("tPag", formaPgto == "06" ? "99" : formaPgto);
                    xml.WriteElementString("vPag", valor);
                    xml.WriteEndElement();//detPag
                    break;
            }
        }

        private void SetInfAdic(XmlTextWriter xml, int Pedido, string tipo)
        {
            Boolean tag = false;

            if (!String.IsNullOrEmpty(_pedido.info_fisco))
            {
                xml.WriteStartElement("infAdic");
                xml.WriteElementString("infAdFisco", Validation.OneSpaceString(Validation.CleanString(_pedido.info_fisco)));
                tag = true;
            }

            if (!String.IsNullOrEmpty(_pedido.info_contribuinte))
            {
                if (tag == false)
                {
                    xml.WriteStartElement("infAdic");
                }
                xml.WriteElementString("infCpl", Validation.OneSpaceString(Validation.CleanString(_pedido.info_contribuinte)));
                tag = true;
            }

            if (tag)
            {
                xml.WriteEndElement();//infAdic
            }
        }

        private string CalculoCDV(string chave)
        {
            string chaveAcesso = chave;

            string chaveInvertida = ReverseString(chave);
            int[] t = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int somatorio = 0;
            int posicaoParaCalculo = 0;
            foreach (var v in chaveInvertida)
            {
                somatorio = somatorio + (int.Parse(v.ToString()) * t[posicaoParaCalculo]);
                if (posicaoParaCalculo == 7)
                {
                    posicaoParaCalculo = 0;
                }
                else
                {
                    posicaoParaCalculo += 1;
                }
            }

            int resto = somatorio % 11;
            int dv;
            if (resto == 0 || resto == 1)
            {
                dv = 0;
            }
            else
            {
                dv = (11 - resto);
            }

            return dv.ToString();
        }

        private string ReverseString(string prStringEntrada)
        {
            char[] arrChar = prStringEntrada.ToCharArray();
            Array.Reverse(arrChar);
            string invertida = new String(arrChar);

            return invertida;
        }

        #endregion XML

        #region Tecnospeed

        public string RequestConsultCpf()
        {            
            requestData = "grupo=Destech&cnpj=05681389000100&CNPJConsCad=09461157000199&uf=SP";
            string retorno = request(requestData, "nfe", "conscad", "GET");
            
            Console.WriteLine(retorno);

            return retorno;
        }

        private string RequestSendInutiliza(string tx2)
        {
            requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + tx2;
            return request(requestData, "NFe", "inutiliza", "POST");
        }

        private string RequestSendCCe(string tx2)
        {
            requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&arquivo=" + tx2;
            return request(requestData, "NFe", "envia", "POST");
        }

        private string RequestSend(string xml, string documento = "NFe")
        {
            if(documento == "NFSe")
                requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&nomecidade=" + TECNOSPEED_CIDADEUF + "&grupo="+ TECNOSPEED_GRUPO +"&arquivo=" + HttpUtility.UrlEncode(xml);
            else
                requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&arquivo=" + HttpUtility.UrlEncode(xml);

            return request(requestData, documento.ToLower(), "envia", "POST");
        }

        private string RequestValidate(string xml, string documento = "NFe")
        {
            requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo="+ TECNOSPEED_GRUPO +"&arquivo=" + HttpUtility.UrlEncode(xml);
            return request(requestData, documento, "valida", "POST");
        }

        private string RequestCancela(string documento = "NFe", string justificativa = "")
        {
            requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&ChaveNota=" + chvAcesso + "&Justificativa=" + justificativa;
            return request(requestData, documento, "cancela", "POST");
        }

        private string RequestConsult(string documento = "NFe")
        {
            requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&Campos=situacao&Filtro=" + chvAcesso;
            return request(requestData, documento, "consulta", "GET");
        }

        private string RequestConsultAutorizada(string documento = "NFe")
        {
            requestData = "encode=true&cnpj=" + _emitente.CPF + "&grupo=" + TECNOSPEED_GRUPO + "&Campos=chave,dtemissao,dtautorizacao,nrecibo,situacao,nnf,nlote,idintegracao,ambiente&Limite=1&Filtro=" + chvAcesso;
            return request(requestData, documento, "consulta", "GET");
        }

        private string RequestResolve(string documento = "NFe")
        {
            requestData = "encode=true&cnpj=" + _emitente.CPF + "&grupo=" + TECNOSPEED_GRUPO + "&ChaveNota=" + chvAcesso;
            return request(requestData, documento, "resolve", "POST");
        }

        private string RequestPrint(string chavedeacesso, string documento = "NFe")
        {
            switch (documento)
            {
                case "CFe":
                    requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&ChaveNota=" + chavedeacesso + "&Url=1";
                    break;

                case "CCe":
                    requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&ChaveNota=" + chavedeacesso + "&Url=1&Documento=CCe";
                    break;

                default:
                    requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&ChaveNota=" + chavedeacesso + "&Url=1";
                    break;
            }

            if (documento == "CFe")
                documento = "cfesat";
            if (documento == "CCe")
                documento = "NFe";

            return request(requestData, documento, "imprime", "GET");
        }

        private string RequestEmail(string email, string documento = "NFe")
        {
            string conteudoemail = "<![CDATA[<html> " +

                "<head> " +
                "<meta http-equiv='Content-Language' content='pt-br'> " +
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'> " +
                "</head> " +

                "<body> " +
                "<br/> " +
                "<br/> " +
                "<br/> " +
                "<table> " +
                "<tr> " +
                //"<th width='15%'></th> " +
                "<th style='font-family:Helvetica; font-weight:normal; text-align: left; font-size: 15px;'> " +
                "<p style='font-size: 20px;'>Olá,</p> " +
                "<p> Estamos enviando neste momento sua Nota Fiscal Eletrônica. </p> " +
                "<p> Existem arquivos em anexo neste e-mail. O arquivo de extensão PDF se trata da cópia eletrônica da Nota Fiscal. O(s) arquivo(s) de extensão XML são necessário(s) apenas para efeito de contabilidade em empresas. </p> " +
                "<p> O código chave de sua Nota Fiscal Eletrônica é " + chvAcesso + " e você pode consultar a autenticidade deste documento juntamente ao Fisco através do site <a href='http://www.nfe.fazenda.gov.br' target='_blank'>http://www.nfe.fazenda.gov.br</a> informando o número do código chave. </p> <p> *** Para visualização da sua Nota Fiscal Eletrônica em anexo, é necessário ter instalado um leitor de arquivos PDF. Caso você ainda não tenha, o download pode ser feito gratuitamente no site <a href='https://get.adobe.com/br/reader/'  target='_blank'>https://get.adobe.com/br/reader/</a> </p> " +
                "<br> <p style='font-size:25px; text-align: center;'>Obrigado.</p> <br> " +
                "<p style='font-size:10px; text-align: center;'> Desenvolvido por Emiplus <br> <a href='https://www.emiplus.com.br' style='font-size:12px;'>www.emiplus.com.br</a> </p> " +
                "</th> " +
                //"<th width='15%'></th> " +
                "</tr> " +
                "</table> " +
                "</body> " +

                "</html>]]>";

            requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&EmailDestinatario=" + email + "&ChaveNota=" + chvAcesso + "&Assunto=Nota Fiscal Eletrônica&Texto=" + conteudoemail + "&AnexaPDF=1&ConteudoHTML=1";
            return request(requestData, documento, "email", "POST");
        }

        private string RequestImport(string xml, string documento = "CFe")
        {
            requestData = "encode=true&cnpj=" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "&grupo=" + TECNOSPEED_GRUPO + "&arquivo=" + HttpUtility.UrlEncode(xml);

            if (documento == "CFe")
                return request(requestData, "cfesat", "importa", "POST");
            else
                return request(requestData, "NFe", "importa", "POST");
        }

        private string request(string _requestData, string _documento, string _route, string _method)
        {
            if (_method == "POST")
            {
                return getMessage(postRequest(_requestData, _documento, _route));
            }
            else
            {
                return getMessage(getRequest(_requestData, _documento, _route));
            }
        }

        private string getMessage(string message)
        {
            if (message.LastIndexOf(",") > 0)
            {
                string[] words = message.Split(',');
                if (words.Length == 4)
                {
                    if (!String.IsNullOrEmpty(words[3]))
                        message = words[3];
                    else if (!String.IsNullOrEmpty(words[2]))
                        message = words[2];
                }
                if (words.Length == 3)
                    message = words[2];
                if (words.Length == 9)
                {
                    message = "Chave de acesso: " + words[0] + Environment.NewLine +
                           "Situação: " + words[4] + Environment.NewLine +
                           "Data emissão: " + words[1] + Environment.NewLine +
                           "Data autorizada: " + words[2] + Environment.NewLine +
                           "N. recibo: " + words[3] + Environment.NewLine +
                           "N. nfe: " + words[5] + Environment.NewLine +
                           "N. lote: " + words[6] + Environment.NewLine +
                           "Id integração: " + words[7] + Environment.NewLine +
                           "Ambiente: " + words[8];
                }
            }

            return message;
        }

        private string getRequest(string get_requestData, string get_documento, string get_route)
        {
            string Result;

            string url = TECNOSPEED_SERVIDOR + "ManagerAPIWeb/" + get_documento.ToLower() + "/" + get_route + "?" + get_requestData;
            WebClient _request = new WebClient();
            byte[] credentials = new UTF8Encoding().GetBytes(TECNOSPEED_USUARIO + ":" + TECNOSPEED_SENHA);
            _request.Headers[HttpRequestHeader.Authorization] = "Basic " + Convert.ToBase64String(credentials);
            Console.WriteLine("Basic Auth (Authorization Header):\n\n" + _request.Headers["Authorization"]);
            Console.WriteLine("URL da Requisição:\n\n" + url);
            Result = "";
            try
            {
                Result = _request.DownloadString(url);
            }
            catch (Exception error)
            {
                Result = "Não Foi Possível Concluir a Requisição.\n" +
                    "- - - - - - - - - - - - - ERRO - - - - - - - - - - - - -\n" + error.ToString();
            }

            return Result;
        }

        private string postRequest(string post_requestData, string post_documento, string post_route)
        {
            string Result;

            try
            {
                //Cria a rota para o servidor de destino da requisição
                WebRequest request = WebRequest.Create(TECNOSPEED_SERVIDOR + "ManagerAPIWeb/" + post_documento.ToLower() + "/" + post_route);
                //Define o formato da requisição
                request.ContentType = "application/x-www-form-urlencoded";
                //Monta a hash de autorização
                byte[] credentials = new UTF8Encoding().GetBytes(TECNOSPEED_USUARIO + ":" + TECNOSPEED_SENHA);
                //Converte a hash de autorização para o header da requisição
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentials);
                request.Timeout = 180000;
                Console.WriteLine("Basic Auth (Authorization Header):\n\n" + request.Headers["Authorization"]);
                //Define o método da requisição
                request.Method = "POST";
                //Encoda o conteúdo da requisição em um array de bytes
                byte[] byteArray = Encoding.UTF8.GetBytes(post_requestData);
                //Define o tamanho da requisição
                request.ContentLength = byteArray.Length;
                Console.WriteLine("URL da Requisição:\n\n" + TECNOSPEED_SERVIDOR + "ManagerAPIWeb/" + post_documento.ToLower() + "/" + post_route);
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                //Captura a resposta da requisição
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                //Implementa um TextReader que lê caracteres de um fluxo de bytes em uma codificação específica.
                StreamReader reader = new StreamReader(dataStream);
                Result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return Result;
        }

        #endregion Tecnospeed

        #region CFE

        private string GetCodAtivacao()
        {
            if (IniFile.Read("Servidor", "SAT") == "Homologacao")
                return "12345678";
            else
                return IniFile.Read("Codigo_Ativacao", "SAT");
        }

        private static string Sep_Delimitador(char sep, int posicao, string dados)
        {
            string[] ret_dados = dados.Split(sep);
            return ret_dados[posicao];
        }

        public static string Base64ToString(string base64)  // caso queira tirar o arquivo de base 64
        {
            byte[] arq;
            //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            arq = Convert.FromBase64String(base64);
            base64 = enc.GetString(arq);
            return base64;
        }

        public static IntPtr NativeUtf8FromString(string managedString)
        {
            int len = Encoding.UTF8.GetByteCount(managedString);
            byte[] buffer = new byte[len + 1];
            Encoding.UTF8.GetBytes(managedString, 0, managedString.Length, buffer, 0);
            IntPtr nativeUtf8 = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, nativeUtf8, buffer.Length);
            return nativeUtf8;
        }

        public static string StringFromNativeUtf8(IntPtr nativeUtf8)
        {
            try
            {
                int len = 0;
                while (Marshal.ReadByte(nativeUtf8, len) != 0) ++len;
                byte[] buffer = new byte[len];
                Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Logs(int tipo = 0)
        {
            Start(0, "CFe");

            string ret = "", msg = "", caracter = "";

            if (tipo == 0)
                ret = StringFromNativeUtf8(Sat.ExtrairLogs(rdn.Next(999999), GetCodAtivacao()));
            else
                ret = StringFromNativeUtf8(Sat.ConsultarStatusOperacional(rdn.Next(999999), GetCodAtivacao()));

            if (!String.IsNullOrEmpty(ret))
            {
                if (tipo == 0)
                {
                    ret = Base64ToString(Sep_Delimitador('|', 5, ret));
                    caracter = "\n";

                    foreach (String item in ret.Split(caracter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            if (String.IsNullOrEmpty(msg))
                                msg = item.TrimEnd();
                            else
                                msg = msg + Environment.NewLine + item.TrimEnd();
                        }
                    }
                }
                else
                    msg = ret;
            }

            if (String.IsNullOrEmpty(msg))
                msg = "Não foi possível consular os logs.";

            return msg;
        }

        public string Consulta()
        {
            return StringFromNativeUtf8(Sat.ConsultarSAT(rdn.Next(999999)));
        }

        #endregion CFE

        #region CODEBAR

        [DllImport("shell32.dll", EntryPoint = "ShellExecute")]
        public static extern int ShellExecuteA(int hwnd, string lpOperation,
        string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        public static string AddSpaces(string valueF, string valueE)
        {
            if ((valueF + valueE).Length <= 48)
                return valueF + "".PadLeft(48 - (valueF.Length + valueE.Length)) + valueE;
            else
                return valueF + " " + valueE;
        }

        // Set Barcode height
        private static byte[] SetBarcodeHeight = new byte[] { 0x1D, 0x68, 0x25 };

        // Set Barcode width
        private static byte[] SetBarcodeWidth = new byte[] { 0x1D, 0x77, 0x03 };

        // Begin barcode printing
        private static byte[] EAN13BarCodeStart = new byte[] { 0x1D, 0x6B, 67, 13 };

        public static string BarcodeString(string barcode)
        {
            string s = ASCIIEncoding.ASCII.GetString(SetBarcodeHeight);
            s += ASCIIEncoding.ASCII.GetString(SetBarcodeWidth);
            s += string.Format("{0}{1}", ASCIIEncoding.ASCII.GetString(EAN13BarCodeStart), barcode);

            return s;
        }

        #endregion CODEBAR

        #region UNINFE

        private void uninfe_envia(string tipo = "envia", string arqPath = "")
        {
            Boolean encontrado = false;
            string _pathUninfe = @"C:\Emiplus\Unimake\UniNFe\" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", ""), _pathEnvio = _pathUninfe + @"\Envio", _pathRetorno = _pathUninfe + @"\Retorno";
            int count = 0;

            if (!Directory.Exists(_pathUninfe) || !Directory.Exists(_pathEnvio) || !Directory.Exists(_pathRetorno))
            {
                _msg = "Opss.. encontramos um erro: Diretórios Uninfe não encontrados.";
                return;
            }

            if (_nota == null)
            {
                _msg = "Opss.. encontramos um erro: Chave de acesso não encontrada.";
                return;
            }

            DirectoryInfo pastaRetorno = new DirectoryInfo(_pathRetorno);

            switch (tipo)
            {
                case "envia":

                    uninfe_limpa(_pathRetorno);
                    File.Copy(arqPath, _pathEnvio + "\\" + _nota.ChaveDeAcesso + "-nfe.xml");
                    _msg = "";
                    Thread.Sleep(5000);

                    break;

                case "cancela":

                    uninfe_limpa(_pathRetorno);
                    File.Copy(arqPath, _pathEnvio + "\\canc" + _nota.ChaveDeAcesso + "-ped-eve.xml");
                    _msg = "";
                    Thread.Sleep(5000);

                    break;

                case "consulta":

                    _msg = "";
                    Thread.Sleep(5000);

                    break;
            }

            while (!encontrado)
            {
                FileInfo[] findfiles = pastaRetorno.GetFiles("*");
                foreach (FileInfo fileinfo in findfiles)
                {
                    switch (tipo)
                    {
                        default:

                            if (fileinfo.Name.IndexOf("-pro-rec.xml") > 0)
                            {
                                encontrado = true;
                            }
                            else if (fileinfo.Name.IndexOf("-sit.xml") > 0)
                            {
                                encontrado = true;
                            }
                            else if (File.Exists(_pathRetorno + "\\" + _nota.ChaveDeAcesso + "-nfe.err") == true)
                            {
                                _msg = System.IO.File.ReadAllText(_pathRetorno + "\\" + _nota.ChaveDeAcesso + "-nfe.err");
                                encontrado = true;
                            }
                            else if (fileinfo.Name.IndexOf("-rec.err") > 0)
                            {
                                _msg = System.IO.File.ReadAllText(_pathRetorno + "\\" + fileinfo.Name);
                                encontrado = true;
                            }
                            else if (File.Exists(_pathRetorno + "\\canc" + _nota.ChaveDeAcesso + "-eve.err") == true)
                            {
                                _msg = System.IO.File.ReadAllText(_pathRetorno + "\\canc" + _nota.ChaveDeAcesso + "-eve.err");
                                encontrado = true;
                            }
                            else if (File.Exists(_pathRetorno + "\\canc" + _nota.ChaveDeAcesso + "-eve.xml") == true)
                            {
                                encontrado = true;
                            }

                            break;
                    }
                }

                count++;
                Thread.Sleep(2000);

                if (count == 1000)
                    break;
            }

            if (encontrado == false)
            {
                _msg = "Opss.. encontramos um erro: Arquivo de retorno não encontrado.";
                return;
            }

            if (!String.IsNullOrEmpty(_msg))
            {
                return;
            }

            FileInfo[] files = pastaRetorno.GetFiles("*");
            foreach (FileInfo fileinfo in files)
            {
                if (fileinfo.Name.IndexOf("-pro-rec.xml") > 0)
                {
                    #region AUTORIZA

                    XmlDocument oXML = new XmlDocument();
                    oXML.Load(_pathRetorno + "\\" + fileinfo.Name);

                    XmlNodeList xnList = oXML.GetElementsByTagName("infProt");

                    for (int i = 0; i < xnList.Count; i++)
                    {
                        if (xnList[i]["chNFe"].InnerText == _nota.ChaveDeAcesso)
                        {
                            if (xnList[i]["xMotivo"].InnerText == "Autorizado o uso da NF-e")
                            {
                                #region AUTORIZADA

                                _msg = "Autorizado o uso da NF-e";
                                _nota.protocolodeuso = xnList[i]["nProt"].InnerText;
                                _nota.Status = "Autorizada";
                                _nota.Save(_nota, false);

                                updateUltNfeAsync();

                                uninfe_xmlAutorizado(_nota.ChaveDeAcesso);

                                #endregion
                            }
                            else if (xnList[i]["xMotivo"].InnerText.Contains("Duplicidade de NF-e"))
                            {
                                uninfe_consulta(_pathEnvio, _pathRetorno);
                            }
                            else
                            {
                                _msg = xnList[i]["xMotivo"].InnerText;
                            }
                        }
                        else
                        {
                            _msg = "Opss.. encontramos um erro: Arquivo de retorno com Chave de Acesso diferente.";
                        }
                    }

                    #endregion
                }
                else if (fileinfo.Name.IndexOf("-sit.xml") > 0)
                {
                    #region CONSULTA 

                    Boolean fim = false;

                    XmlDocument oXML = new XmlDocument();
                    oXML.Load(_pathRetorno + "\\" + fileinfo.Name);

                    try
                    {
                        XmlNodeList xnList = oXML.GetElementsByTagName("infCanc");

                        for (int i = 0; i < xnList.Count; i++)
                        {
                            if (xnList[i]["chNFe"].InnerText == _nota.ChaveDeAcesso)
                            {
                                if (xnList[i]["xMotivo"].InnerText.Contains("Cancelamento"))
                                {
                                    fim = true;

                                    #region CANCELADA

                                    _msg = xnList[i]["xMotivo"].InnerText;
                                    _nota.protocolodeuso = xnList[i]["nProt"].InnerText;
                                    _nota.Status = "Cancelada";
                                    _nota.Save(_nota, false);

                                    #endregion

                                    uninfe_xmlAutorizado(_nota.ChaveDeAcesso);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    if (fim)
                        return;

                    try
                    {
                        XmlNodeList xnList = oXML.GetElementsByTagName("infProt");

                        for (int i = 0; i < xnList.Count; i++)
                        {
                            if (xnList[i]["chNFe"].InnerText == _nota.ChaveDeAcesso)
                            {
                                if (xnList[i]["xMotivo"].InnerText.Contains("Autorizado"))
                                {
                                    fim = true;

                                    #region AUTORIZADA

                                    _msg = "Autorizado o uso da NF-e";
                                    _nota.protocolodeuso = xnList[i]["nProt"].InnerText;
                                    _nota.Status = "Autorizada";
                                    _nota.Save(_nota, false);

                                    #endregion

                                    uninfe_xmlAutorizado(_nota.ChaveDeAcesso);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    #endregion
                }
                else if (fileinfo.Name.IndexOf("-eve.xml") > 0)
                {
                    #region CANCELADA 

                    XmlDocument oXML = new XmlDocument();
                    oXML.Load(_pathRetorno + "\\" + fileinfo.Name);
                    XmlNodeList xnList = oXML.GetElementsByTagName("infEvento");

                    for (int i = 0; i < xnList.Count; i++)
                    {
                        if (xnList[i]["chNFe"].InnerText == _nota.ChaveDeAcesso)
                        {
                            if (xnList[i]["xMotivo"].InnerText.Contains("Cancelamento"))
                            {
                                #region CANCELADA

                                _msg = xnList[i]["xMotivo"].InnerText;
                                _nota.protocolodeuso = xnList[i]["nProt"].InnerText;
                                _nota.Status = "Cancelada";
                                _nota.Save(_nota, false);

                                #endregion

                                uninfe_xmlAutorizado(_nota.ChaveDeAcesso);
                            }
                            else if (xnList[i]["xMotivo"].InnerText.Contains("Duplicidade"))
                            {
                                uninfe_consulta(_pathEnvio, _pathRetorno);
                            }
                        }
                    }

                    #endregion
                }
            }
        }

        private void uninfe_consulta(string _pathEnvio = "", string _pathRetorno = "")
        {
            string _pathUninfe = @"C:\Emiplus\Unimake\UniNFe\" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "");

            if (String.IsNullOrEmpty(_pathEnvio))
                _pathEnvio = _pathUninfe + @"\Envio";

            if (String.IsNullOrEmpty(_pathRetorno))
                _pathRetorno = _pathUninfe + @"\Retorno";

            #region DUPLICIDADE 

            uninfe_limpa(_pathRetorno);

            XmlTextWriter writer = new XmlTextWriter(_pathEnvio + "\\" + _nota.ChaveDeAcesso + "-ped-sit.xml", null);
            writer.WriteStartElement("consSitNFe");
            writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");
            writer.WriteAttributeString("versao", "4.00");
            writer.WriteElementString("tpAmb", _servidorNFE.ToString());
            writer.WriteElementString("xServ", "CONSULTAR");
            writer.WriteElementString("chNFe", _nota.ChaveDeAcesso);
            writer.WriteEndElement();
            writer.Close();

            #endregion

            uninfe_envia("consulta", _pathEnvio + "\\" + _nota.ChaveDeAcesso + "-ped-sit.xml");
        }

        private void uninfe_limpa(string _pathRetorno)
        {
            DirectoryInfo pastaRetorno = new DirectoryInfo(_pathRetorno);
            FileInfo[] Arquivos = pastaRetorno.GetFiles("*");

            #region Antes de tudo deleta os arquivos *-pro-rec.xml, *-nfe-ret.xml, *-num-lot.xml, *-rec.err, *-nfe.err

            foreach (FileInfo del in Arquivos)
            {
                del.Delete();
            }

            #endregion
        }

        private void uninfe_xmlAutorizado(string chavedeacesso)
        {
            string _pathUninfe = @"C:\Emiplus\Unimake\UniNFe\" + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "\\Enviado\\Autorizados\\20" + chavedeacesso.Substring(2, 4);
            string mensagem = "";

            if (File.Exists(_pathUninfe + "\\" + chavedeacesso + "_110111_01-procEventoNFe.xml"))
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(_pathUninfe + "\\" + chavedeacesso + "_110111_01-procEventoNFe.xml");

                //XmlNodeList xnList = oXML.GetElementsByTagName("infEvento");

                //for (int i = 0; i < xnList.Count; i++)
                //{
                //    _nota = new Model.Nota().Query().Where("CHAVEDEACESSO", chavedeacesso).Where("excluir", 0).FirstOrDefault<Model.Nota>();

                //    if (_nota == null)
                //        return;

                //    _nota.protocolodeuso = xnList[i]["nProt"].InnerText;
                //    _nota.Status = "Cancelada";
                //    _nota.Save(_nota, false);
                //}

                try
                {
                    mensagem = RequestImport(oXML.OuterXml, "NFe");

                    if (!mensagem.Contains("Importação realizada com sucesso"))
                        new Log().Add("EXCEPTIONS", mensagem + Environment.NewLine + "######################################", Log.LogType.fatal);
                }
                catch (Exception e)
                {
                    new Log().Add("EXCEPTIONS", e.GetBaseException().ToString() + Environment.NewLine + "######################################", Log.LogType.fatal);
                }

                return;
            }

            if (File.Exists(_pathUninfe + "\\" + chavedeacesso + "-procNFe.xml"))
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(_pathUninfe + "\\" + chavedeacesso + "-nfe.xml");

                XmlNodeList xnList = oXML.GetElementsByTagName("ide");

                for (int i = 0; i < xnList.Count; i++)
                {
                    _nota = new Model.Nota().Query().Where("CHAVEDEACESSO", chavedeacesso).Where("excluir", 0).FirstOrDefault<Model.Nota>();

                    if (_nota == null)
                        return;

                    _nota.nr_Nota = Validation.ConvertToInt32(xnList[i]["nNF"].InnerText).ToString("000000000");
                    _nota.Serie = Validation.ConvertToInt32(xnList[i]["serie"].InnerText).ToString("000");
                    _nota.Save(_nota, false);
                }

                try
                {
                    mensagem = RequestImport(oXML.OuterXml, "NFe");

                    if (!mensagem.Contains("Importação realizada com sucesso"))
                        new Log().Add("EXCEPTIONS", mensagem + Environment.NewLine + "######################################", Log.LogType.fatal);
                }
                catch (Exception e)
                {
                    new Log().Add("EXCEPTIONS", e.GetBaseException().ToString() + Environment.NewLine + "######################################", Log.LogType.fatal);
                }

                return;
            }

            if (File.Exists(_pathUninfe + "\\" + chavedeacesso + "-nfe.xml"))
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(_pathUninfe + "\\" + chavedeacesso + "-nfe.xml");

                XmlNodeList xnList = oXML.GetElementsByTagName("ide");

                for (int i = 0; i < xnList.Count; i++)
                {
                    _nota = new Model.Nota().Query().Where("CHAVEDEACESSO", chavedeacesso).Where("excluir", 0).FirstOrDefault<Model.Nota>();

                    if (_nota == null)
                        return;

                    _nota.nr_Nota = Validation.ConvertToInt32(xnList[i]["nNF"].InnerText).ToString("000000000");
                    _nota.Serie = Validation.ConvertToInt32(xnList[i]["serie"].InnerText).ToString("000");
                    _nota.Save(_nota, false);
                }

                try
                {
                    mensagem = RequestImport(oXML.OuterXml, "NFe");

                    if (!mensagem.Contains("Importação realizada com sucesso"))
                        new Log().Add("EXCEPTIONS", mensagem + Environment.NewLine + "######################################", Log.LogType.fatal);
                }
                catch (Exception e)
                {
                    new Log().Add("EXCEPTIONS", e.GetBaseException().ToString() + Environment.NewLine + "######################################", Log.LogType.fatal);
                }

                return;
            }
        }

        #endregion
    }
}
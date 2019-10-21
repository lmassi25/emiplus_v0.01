using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace Emiplus.Controller
{
    public class Fiscal
    {
        private Model.Pedido _pedido;
        IEnumerable<dynamic> itens;
        private Model.PedidoItem _pedidoItem;
        private int _id_empresa = 0;

        private Model.Pessoa _emitente;
        private Model.PessoaEndereco _emitenteEndereco;
        private Model.PessoaContato _emitenteContato;

        private Model.Pessoa _destinatario;
        private Model.PessoaEndereco _destinatarioEndereco;
        private Model.PessoaContato _destinatarioContato;

        private Model.Natureza _natureza;

        private string _path;
        private string _path_enviada;
        private string _path_autorizada;

        private int _servidor = 2;
        private string TECNOSPEED_GRUPO = "Destech";
        private string TECNOSPEED_USUARIO = "admin";
        private string TECNOSPEED_SENHA = "LE25an10na";

        private string TECNOSPEED_SERVIDOR;

        private string _msg;

        public string requestData;
        public System.Timers.Timer aTimer = new System.Timers.Timer();

        public Fiscal()
        {
            _path = IniFile.Read("Path", "LOCAL");

            if (String.IsNullOrEmpty(_path))
            {
                _path = @"C:\Emiplus";
            }

            if (!Directory.Exists(_path + "\\NFe"))
            {
                Directory.CreateDirectory(_path + "\\NFe");
            }

            _path_enviada = _path + "\\NFe\\Enviadas";
            if (!Directory.Exists(_path_enviada))
            {
                Directory.CreateDirectory(_path_enviada);
            }

            _path_autorizada = _path + "\\NFe\\Autorizadas";
            if (!Directory.Exists(_path_autorizada))
            {
                Directory.CreateDirectory(_path_autorizada);
            }

            if (_servidor == 1)
            {
                TECNOSPEED_SERVIDOR = "https://managersaas.tecnospeed.com.br:8081/";
            }
            else
            {
                TECNOSPEED_SERVIDOR = "https://managersaashom.tecnospeed.com.br:7071/";
            }
        }

        public string Issue(int Pedido, string tipo)
        {
            CreateXml(Pedido, tipo);
            _emitente.CPF = "05681389000100"; //teste

            string xml = "";

            var arq = new XmlDocument();
            arq.Load(_path_enviada + "\\169.xml"); //teste

            xml = arq.OuterXml;

            _msg = sendRequest("envia", "FORMATO=XML" + Environment.NewLine + xml, "NFe");

            if (_msg.Contains("já existe no banco de dados"))
                _msg = sendRequest("consulta", "FORMATO=XML" + Environment.NewLine + xml, "NFe");
        }

        /// <summary> 
        /// Cria o arquivo xml
        /// </summary>
        /// <param name="tipo">NFe, NFCe, CFe</param>        
        public void CreateXml(int Pedido, string tipo)
        {
            #region DADOS 

            _pedido = new Model.Pedido().FindById(Pedido).First<Model.Pedido>();

            if (Validation.ConvertToInt32(_pedido.id_empresa) == 0)
            {
                _id_empresa = 1;
            }
            else
            {
                _id_empresa = Validation.ConvertToInt32(_pedido.id_empresa);
            }

            _emitente = new Model.Pessoa().FindById(_id_empresa).First<Model.Pessoa>();
            _emitenteEndereco = new Model.PessoaEndereco().FindById(_id_empresa).First<Model.PessoaEndereco>();
            //_emitenteContato = new Model.PessoaContato().FindById(_id_empresa).First<Model.PessoaContato>();

            _destinatario = new Model.Pessoa().FindById(_pedido.Cliente).First<Model.Pessoa>();
            _destinatarioEndereco = new Model.PessoaEndereco().FindById(_pedido.Cliente).First<Model.PessoaEndereco>();
            //_destinatarioContato = new Model.PessoaContato().FindById(_pedido.Cliente).First<Model.PessoaContato>();

            _natureza = new Model.Natureza().FindById(_pedido.id_natureza).First<Model.Natureza>();
            #endregion

            #region PATH 

            string strFilePath = _path_enviada + "\\" + Pedido + ".xml";

            #endregion

            #region XML

            var xml = new XmlTextWriter(strFilePath, Encoding.UTF8);
            xml.Formatting = Formatting.Indented;

            xml.WriteStartDocument(); //Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>

            if (tipo == "NFCe")
            {
                xml.WriteStartElement("NFe");
            }
            else if (tipo == "CFe")
            {
                xml.WriteStartElement("CFe");
            }
            else
            {
                xml.WriteStartElement("NFe");
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

            xml.WriteEndElement();

            xml.WriteEndDocument();

            xml.Flush();
            xml.Close();

            #endregion
        }

        private void SetIde(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("ide");

            if (tipo == "CFe")
            {
                xml.WriteElementString("CNPJ", "");
                xml.WriteElementString("signAC", "");
                xml.WriteElementString("numeroCaixa", "");
            }
            else
            {
                switch (_emitenteEndereco.Estado)
                {
                    case "RO":
                        xml.WriteElementString("cUF", "11");
                        break;
                    case "AC":
                        xml.WriteElementString("cUF", "12");
                        break;
                    case "AM":
                        xml.WriteElementString("cUF", "13");
                        break;
                    case "RR":
                        xml.WriteElementString("cUF", "14");
                        break;
                    case "PA":
                        xml.WriteElementString("cUF", "15");
                        break;
                    case "AP":
                        xml.WriteElementString("cUF", "16");
                        break;
                    case "TO":
                        xml.WriteElementString("cUF", "17");
                        break;
                    case "MA":
                        xml.WriteElementString("cUF", "21");
                        break;
                    case "PI":
                        xml.WriteElementString("cUF", "22");
                        break;
                    case "CE":
                        xml.WriteElementString("cUF", "23");
                        break;
                    case "RN":
                        xml.WriteElementString("cUF", "24");
                        break;
                    case "PB":
                        xml.WriteElementString("cUF", "25");
                        break;
                    case "PE":
                        xml.WriteElementString("cUF", "26");
                        break;
                    case "AL":
                        xml.WriteElementString("cUF", "27");
                        break;
                    case "SE":
                        xml.WriteElementString("cUF", "28");
                        break;
                    case "BA":
                        xml.WriteElementString("cUF", "29");
                        break;
                    case "MG":
                        xml.WriteElementString("cUF", "31");
                        break;
                    case "ES":
                        xml.WriteElementString("cUF", "32");
                        break;
                    case "RJ":
                        xml.WriteElementString("cUF", "33");
                        break;
                    case "SP":
                        xml.WriteElementString("cUF", "35");
                        break;
                    case "PR":
                        xml.WriteElementString("cUF", "41");
                        break;
                    case "SC":
                        xml.WriteElementString("cUF", "42");
                        break;
                    case "RS":
                        xml.WriteElementString("cUF", "43");
                        break;
                    case "MT":
                        xml.WriteElementString("cUF", "51");
                        break;
                    case "GO":
                        xml.WriteElementString("cUF", "52");
                        break;
                    case "DF":
                        xml.WriteElementString("cUF", "53");
                        break;
                }
                
                xml.WriteElementString("cNF", "9" + "");
                xml.WriteElementString("natOp", _natureza.Nome);

                if (tipo == "NFe")
                {
                    xml.WriteElementString("indPag", "");
                }

                if (tipo == "NFe")
                {
                    xml.WriteElementString("mod", "55");
                }
                else if (tipo == "NFCe")
                {
                    xml.WriteElementString("mod", "65");
                }

                xml.WriteElementString("serie", "");
                xml.WriteElementString("nNF", "");
                xml.WriteElementString("dhEmi", "");
                xml.WriteElementString("dhSaiEnt", "");
                xml.WriteElementString("tpNF", _pedido.TipoNFe.ToString());
                xml.WriteElementString("idDest", "");
                xml.WriteElementString("cMunFG", "");

                if (tipo == "NFe")
                {
                    xml.WriteElementString("tpImp", "1");
                }
                else if (tipo == "NFCe")
                {
                    xml.WriteElementString("tpImp", "4");
                }
                
                xml.WriteElementString("tpEmis", "1");
                xml.WriteElementString("cDV", "");
                xml.WriteElementString("tpAmb", "");
                xml.WriteElementString("finNFe", "");
                xml.WriteElementString("indFinal", "");
                xml.WriteElementString("indPres", "");
                xml.WriteElementString("procEmi", "0");
                xml.WriteElementString("verProc", "EMIPLUS");
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

        }

        private void SetEmit(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("emit");

            if (tipo == "CFe")
            {
                xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_emitente.CPF));
                xml.WriteElementString("IE", Validation.CleanStringForFiscal(_emitente.RG));
                xml.WriteElementString("indRatISSQN", "N");
            }
            else
            {
                xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_emitente.CPF));
                xml.WriteElementString("xNome", Validation.CleanStringForFiscal(_emitente.Nome));
                xml.WriteElementString("xFant", Validation.CleanStringForFiscal(_emitente.Fantasia));

                xml.WriteStartElement("enderEmit");

                xml.WriteElementString("xLgr", Validation.CleanStringForFiscal(_emitenteEndereco.Rua));
                xml.WriteElementString("nro", Validation.CleanStringForFiscal(_emitenteEndereco.Nr));
                xml.WriteElementString("xBairro", Validation.CleanStringForFiscal(_emitenteEndereco.Bairro));
                xml.WriteElementString("cMun", "0");
                xml.WriteElementString("xMun", Validation.CleanStringForFiscal(_emitenteEndereco.Cidade));
                xml.WriteElementString("UF", _emitenteEndereco.Estado);
                xml.WriteElementString("CEP", Validation.CleanStringForFiscal(_emitenteEndereco.Cep));
                xml.WriteElementString("cPais", "1058");
                xml.WriteElementString("xPais", "BRASIL");
                //xml.WriteElementString("fone", "");

                xml.WriteEndElement();

                xml.WriteElementString("IE", _emitente.RG);

                if (tipo == "NFCe")
                {
                    xml.WriteElementString("IM", "");
                    xml.WriteElementString("CNAE", "");
                }

                xml.WriteElementString("CRT", "");
            }

            xml.WriteEndElement();
        }

        private void SetDest(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("dest");

            if (tipo == "CFe")
            {

            }
            else
            {
                xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_destinatario.CPF));
                xml.WriteElementString("xNome", Validation.CleanStringForFiscal(_destinatario.Nome));

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

                xml.WriteElementString("indIEDest", "0");
                xml.WriteElementString("IE", Validation.CleanStringForFiscal(_destinatario.RG));
            }

            xml.WriteEndElement();
        }

        private void SetRetirada(XmlTextWriter xml, int Pedido, string tipo)
        {

        }

        private void SetEntrega(XmlTextWriter xml, int Pedido, string tipo)
        {

        }

        private void SetDet(XmlTextWriter xml, int Pedido, string tipo, int n, int id)
        {
            _pedidoItem = new Model.PedidoItem().FindById(id).First<Model.PedidoItem>();

            xml.WriteStartElement("det");

            xml.WriteAttributeString("nItem", n.ToString());

            #region prod

            xml.WriteStartElement("prod");

            if (tipo == "CFe")
            {
                xml.WriteElementString("cProd", Validation.CleanStringForFiscal(_pedidoItem.CProd));
                xml.WriteElementString("cEAN", Validation.CleanStringForFiscal(_pedidoItem.CEan));
                xml.WriteElementString("xProd", Validation.CleanStringForFiscal(_pedidoItem.xProd));
                xml.WriteElementString("NCM", Validation.CleanStringForFiscal(_pedidoItem.Ncm));
                xml.WriteElementString("CFOP", Validation.CleanStringForFiscal(_pedidoItem.Ncm));
                xml.WriteElementString("uCom", _pedidoItem.Medida);
                xml.WriteElementString("qCom", Validation.FormatPriceWithDot(_pedidoItem.Quantidade));
                xml.WriteElementString("vUnCom", Validation.FormatPriceWithDot(_pedidoItem.ValorVenda));
                xml.WriteElementString("indRegra", "A");
                if(_pedidoItem.Desconto > 0)
                    xml.WriteElementString("vDesc", Validation.FormatPriceWithDot(_pedidoItem.Desconto));
            }
            else
            {
                xml.WriteElementString("cProd", Validation.CleanStringForFiscal(_pedidoItem.CProd));

                if (!String.IsNullOrEmpty(_pedidoItem.CEan))
                    xml.WriteElementString("cEAN", Validation.CleanStringForFiscal(_pedidoItem.CEan));

                xml.WriteElementString("xProd", Validation.CleanStringForFiscal(_pedidoItem.xProd));

                xml.WriteElementString("NCM", Validation.CleanStringForFiscal(_pedidoItem.Ncm));
                xml.WriteElementString("CFOP", Validation.CleanStringForFiscal(_pedidoItem.Cfop));

                xml.WriteElementString("uCom", _pedidoItem.Medida);
                xml.WriteElementString("qCom", Validation.FormatPriceWithDot(_pedidoItem.Quantidade));
                xml.WriteElementString("vUnCom", Validation.FormatPriceWithDot(_pedidoItem.ValorVenda));

                xml.WriteElementString("vProd", Validation.FormatPriceWithDot(_pedidoItem.Total));

                if(!String.IsNullOrEmpty(_pedidoItem.CEan))
                    xml.WriteElementString("cEANTrib", Validation.CleanStringForFiscal(_pedidoItem.CEan));

                xml.WriteElementString("uTrib", _pedidoItem.Medida);
                xml.WriteElementString("qTrib", Validation.FormatPriceWithDot(_pedidoItem.Quantidade));
                xml.WriteElementString("vUnTrib", Validation.FormatPriceWithDot(_pedidoItem.ValorVenda));

                xml.WriteElementString("indTot", "1");

                //xml.WriteElementString("xPed", "");
                //xml.WriteElementString("nItemPed", "");
            }

            xml.WriteEndElement();//prod

            #endregion

            xml.WriteStartElement("imposto");

            xml.WriteElementString("vTotTrib", "");

            #region ICMS

            xml.WriteStartElement("ICMS");

            //xml.WriteElementString("", "");

            xml.WriteEndElement();//ICMS

            #endregion

            #region IPI

            xml.WriteStartElement("IPI");

            //xml.WriteElementString("", "");

            xml.WriteEndElement();//IPI

            #endregion

            #region II

            //xml.WriteStartElement("II");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//II

            #endregion

            #region PIS

            xml.WriteStartElement("PIS");

            //xml.WriteElementString("", "");

            xml.WriteEndElement();//PIS

            #endregion

            #region PIS ST

            //xml.WriteStartElement("PISST");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//PISST

            #endregion

            #region COFINS

            xml.WriteStartElement("COFINS");

            //xml.WriteElementString("", "");

            xml.WriteEndElement();//COFINS

            #endregion

            #region COFINS ST

            //xml.WriteStartElement("COFINSST");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//COFINSST

            #endregion

            xml.WriteEndElement();//imposto

            xml.WriteEndElement();//det
        }

        private void SetTotal(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("total");

            xml.WriteStartElement("ICMSTot");

            xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedido.ICMSBASE));
            xml.WriteElementString("vICMS", Validation.FormatPriceWithDot(_pedido.ICMS));
            xml.WriteElementString("vICMSDeson", "0.00");
            xml.WriteElementString("vBCST", Validation.FormatPriceWithDot(_pedido.ICMSSTBASE));
            xml.WriteElementString("vST", Validation.FormatPriceWithDot(_pedido.ICMSST));
            xml.WriteElementString("vProd", Validation.FormatPriceWithDot(_pedido.Produtos));
            xml.WriteElementString("vFrete", Validation.FormatPriceWithDot(_pedido.Frete));
            xml.WriteElementString("vSeg", "0.00");
            xml.WriteElementString("vDesc", Validation.FormatPriceWithDot(_pedido.Desconto));
            xml.WriteElementString("vII", "0.00");
            xml.WriteElementString("vIPI", Validation.FormatPriceWithDot(_pedido.IPI));
            xml.WriteElementString("vPIS", Validation.FormatPriceWithDot(_pedido.PIS));
            xml.WriteElementString("vCOFINS", Validation.FormatPriceWithDot(_pedido.COFINS));
            xml.WriteElementString("vOutro", "0.00");
            xml.WriteElementString("vNF", Validation.FormatPriceWithDot(_pedido.Total));
            xml.WriteElementString("vTotTrib", "0.00");

            xml.WriteEndElement();//ICMSTot

            xml.WriteEndElement();//total
        }

        private void SetTransp(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("transp");

            xml.WriteElementString("modFrete", "");

            xml.WriteEndElement();//transp
        }

        private void SetCobr(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("cobr");

            //xml.WriteElementString("", "");

            xml.WriteEndElement();//cobr
        }

        private void SetPag(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("pag");

            xml.WriteElementString("tPag", "");
            xml.WriteElementString("vPag", "");

            xml.WriteEndElement();//pag
        }

        private void SetInfAdic(XmlTextWriter xml, int Pedido, string tipo)
        {
            xml.WriteStartElement("infAdic");

            //xml.WriteElementString("infAdFisco", "");
            xml.WriteElementString("infCpl", "");

            xml.WriteEndElement();//infAdic
        }

        /// <summary> 
        /// Envia requisição para a Tecnospeed
        /// </summary>
        /// <param name="tipo">envia</param>        
        /// <param name="xml">conteúdo xml</param>    
        /// <param name="documento">NFe</param>    
        private string sendRequest(string tipo, string xml, string documento = "NFe")
        {
            switch (tipo)
            {
                case "envia":
                    requestData = "encode=true&cnpj=" + _emitente.CPF + "&grupo=" + TECNOSPEED_GRUPO + "&arquivo=" + HttpUtility.UrlEncode(xml);
                    return request(requestData, documento, "envia", "POST");
                    break;
                /*
                    case "consulta":
                    requestData = "encode=true&cnpj=" + _emitente.CPF + "&grupo=" + TECNOSPEED_GRUPO + "&arquivo=" + HttpUtility.UrlEncode(xml);
                    return request(requestData, documento, "envia", "POST");
                    break;
                */
            }

            return "";
        }

        public string request(string _requestData, string _documento, string _route, string _method)
        {
            if (_method == "POST")
            {
                return postRequest(_requestData, _documento, _route);
            }
            else
            {
                return getRequest(_requestData, _documento, _route);
            }
        }
        
        public string getRequest(string get_requestData, string get_documento, string get_route)
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

        public string postRequest(string post_requestData, string post_documento, string post_route)
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
    }
}

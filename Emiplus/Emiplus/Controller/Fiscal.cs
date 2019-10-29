using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Newtonsoft.Json.Linq;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private static readonly HttpClient client = new HttpClient();

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
        
        string chvAcesso = "", cDV = "";

        private JObject userData { get; set; }

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
            try
            {
                CreateXml(Pedido, tipo);
                //_emitente.CPF = "05681389000100"; //teste

                string xml = "";

                var arq = new XmlDocument();
                //arq.Load(_path_enviada + "\\169.xml"); //teste

                xml = arq.OuterXml;

                _msg = sendRequest("envia", "FORMATO=XML" + Environment.NewLine + xml, "NFe");

                if (_msg.Contains("já existe no banco de dados"))
                    _msg = sendRequest("consulta", "FORMATO=XML" + Environment.NewLine + xml, "NFe");

                return "";
            }
            catch (Exception ex)
            {
                return "Opss.. encontramos um erro: " + ex.Message;
            }
        }

        private int getLastNFeNr()
        {
            return Validation.ConvertToInt32(Settings.Default.empresa_nfe_ultnfe) + 1;
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

            //_emitente = new Model.Pessoa().FindById(_id_empresa).First<Model.Pessoa>();
            _emitente = new Model.Pessoa();

            //_emitenteEndereco = new Model.PessoaEndereco().FindById(_id_empresa).First<Model.PessoaEndereco>();
            _emitenteEndereco = new Model.PessoaEndereco();

            //_emitenteContato = new Model.PessoaContato().FindById(_id_empresa).First<Model.PessoaContato>();
            _emitenteContato = new Model.PessoaContato();

            _emitente.RG = Settings.Default.empresa_inscricao_estadual;
            _emitente.CPF = Settings.Default.empresa_cnpj;
            _emitente.Nome = Settings.Default.empresa_razao_social;
            _emitente.Fantasia = Settings.Default.empresa_nome_fantasia;

            _emitenteEndereco.Rua = Settings.Default.empresa_rua;
            _emitenteEndereco.Nr = Settings.Default.empresa_nr;
            _emitenteEndereco.Bairro = Settings.Default.empresa_bairro;
            _emitenteEndereco.Cidade = Settings.Default.empresa_cidade;
            _emitenteEndereco.Cep = Settings.Default.empresa_cep;
            _emitenteEndereco.IBGE = Settings.Default.empresa_ibge;
            _emitenteEndereco.Estado = Settings.Default.empresa_estado;
            
            _destinatario = new Model.Pessoa().FindById(_pedido.Cliente).First<Model.Pessoa>();
            _destinatarioEndereco = new Model.PessoaEndereco().FindById(_pedido.Cliente).First<Model.PessoaEndereco>();
            //_destinatarioContato = new Model.PessoaContato().FindById(_pedido.Cliente).First<Model.PessoaContato>();

            _natureza = new Model.Natureza().FindById(_pedido.id_natureza).First<Model.Natureza>();
            #endregion

            #region PATH 

            string strFilePath = _path_enviada + "\\" + Pedido + ".xml";

            #endregion

            #region XML

            chvAcesso = codUF(_emitenteEndereco.Estado) + _pedido.Emissao.ToString("yy") + _pedido.Emissao.ToString("mm") + Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", "") + "55" + "00" + Settings.Default.empresa_nfe_serienfe + getLastNFeNr().ToString("0000000") + "1" + "9" + getLastNFeNr().ToString("0000000");
            cDV = CalculoCDV(chvAcesso);
            chvAcesso = chvAcesso + "" + cDV;

            var xml = new XmlTextWriter(strFilePath, Encoding.UTF8);
            xml.Formatting = Formatting.Indented;

            xml.WriteStartDocument(); //Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>

            if (tipo == "NFCe")
            {
                xml.WriteStartElement("NFe");

                xml.WriteStartElement("infNFe");
                xml.WriteAttributeString("Id", "NFe" + chvAcesso);
                xml.WriteAttributeString("versao", "4.00");
            }
            else if (tipo == "CFe")
            {
                xml.WriteStartElement("CFe");
            }
            else
            {
                xml.WriteStartElement("NFe");

                xml.WriteStartElement("infNFe");
                xml.WriteAttributeString("Id", "NFe" + chvAcesso);
                xml.WriteAttributeString("versao", "4.00");
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
            xml.WriteEndElement();

            xml.WriteEndDocument();

            xml.Flush();
            xml.Close();

            #endregion
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
                xml.WriteElementString("CNPJ", "");
                xml.WriteElementString("signAC", "");
                xml.WriteElementString("numeroCaixa", "");
            }
            else
            {
                xml.WriteElementString("cUF", codUF(_emitenteEndereco.Estado));
                
                xml.WriteElementString("cNF", "9" + "");
                xml.WriteElementString("natOp", _natureza.Nome);

                if (tipo == "NFe")
                {
                    xml.WriteElementString("mod", "55");
                }
                else if (tipo == "NFCe")
                {
                    xml.WriteElementString("mod", "65");
                }

                //2019-10-08T08:52:34-03:00
                string horaSaida = "";
                if (string.IsNullOrEmpty(_pedido.HoraSaida))
                {
                    horaSaida = DateTime.Now.ToString("HH:mm:ss");
                } else
                {
                    horaSaida = _pedido.HoraSaida + ":00";
                }

                xml.WriteElementString("serie", string.IsNullOrEmpty(Settings.Default.empresa_nfe_serienfe) ? "0" : Settings.Default.empresa_nfe_serienfe);
                xml.WriteElementString("nNF", getLastNFeNr().ToString());
                xml.WriteElementString("dhEmi", Validation.ConvertDateToSql(_pedido.Emissao) + "T" + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + DateTime.Now.ToString("zzz"));
                xml.WriteElementString("dhSaiEnt", Validation.ConvertDateToSql(_pedido.Emissao) + "T" + horaSaida + DateTime.Now.ToString("zzz"));
                xml.WriteElementString("tpNF", _pedido.TipoNFe.ToString());
                xml.WriteElementString("idDest", _pedido.Destino.ToString());
                xml.WriteElementString("cMunFG", Settings.Default.empresa_ibge);

                if (tipo == "NFe")
                    xml.WriteElementString("tpImp", "1");
                else if (tipo == "NFCe")
                    xml.WriteElementString("tpImp", "4");
                
                xml.WriteElementString("tpEmis", "1");
                xml.WriteElementString("cDV", cDV);
                xml.WriteElementString("tpAmb", Settings.Default.empresa_nfe_servidornfe.ToString());
                xml.WriteElementString("finNFe", _pedido.Finalidade.ToString());
                xml.WriteElementString("indFinal", "1");
                xml.WriteElementString("indPres", "1");
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
                xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_emitente.CPF).Replace(".", ""));
                xml.WriteElementString("IE", Validation.CleanStringForFiscal(_emitente.RG).Replace(".", ""));
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

                if (tipo == "NFCe")
                {
                    xml.WriteElementString("IM", "");
                    xml.WriteElementString("CNAE", "");
                }

                xml.WriteElementString("CRT", Settings.Default.empresa_crt);
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
                xml.WriteElementString("CNPJ", Validation.CleanStringForFiscal(_destinatario.CPF).Replace(".", ""));
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

                xml.WriteElementString("indIEDest", _destinatario.Isento == 1 ? "9" : "1");
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

            xml.WriteElementString("vTotTrib", "0.00");

            #region ICMS

            xml.WriteStartElement("ICMS");

            if(_pedidoItem.Icms.Length == 3)
            {
                xml.WriteStartElement("ICMSSN" + _pedidoItem.Icms);
            }
            else
            {
                xml.WriteStartElement("ICMS" + _pedidoItem.Icms);
            }

            switch (_pedidoItem.Icms)
            {
                case "00":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CST", "00");
                    xml.WriteElementString("modBC", "0");
                    xml.WriteElementString("vBC", Validation.FormatPriceWithDot(_pedidoItem.IcmsBase));
                    xml.WriteElementString("pICMS", Validation.FormatPriceWithDot(_pedidoItem.IcmsAliq));
                    xml.WriteElementString("vICMS", Validation.FormatPriceWithDot(_pedidoItem.IcmsVlr));
                    break;
                case "40":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CST", "40");
                    break;
                case "41":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CST", "41");
                    break;
                case "50":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CST", "50");
                    break;
                case "60":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CST", "60");
                    break;
                case "90":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CST", "90");
                    break;
                case "101":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CSOSN", "101");
                    xml.WriteElementString("pCredSN", Validation.FormatPriceWithDot(_pedidoItem.IcmsAliq));
                    xml.WriteElementString("vCredICMSSN", Validation.FormatPriceWithDot(_pedidoItem.IcmsVlr));
                    break;
                case "102":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CSOSN", "102");
                    break;
                case "201":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
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
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CSOSN", "202");
                    xml.WriteElementString("modBCST", "0");
                    xml.WriteElementString("pMVAST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("pRedBCST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vBCST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("pICMSST", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vICMSST", Validation.FormatPriceWithDot(0));
                    break;
                case "500":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CSOSN", "500");
                    xml.WriteElementString("vBCSTRet", Validation.FormatPriceWithDot(0));
                    xml.WriteElementString("vICMSSTRet", Validation.FormatPriceWithDot(0));
                    break;
                case "900":
                    xml.WriteElementString("orig", _pedidoItem.Origem);
                    xml.WriteElementString("CSOSN", "900");
                    break;
            }

            xml.WriteEndElement();

            xml.WriteEndElement();//ICMS

            #endregion

            #region IPI

            if (String.IsNullOrEmpty(_pedidoItem.Ipi))
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

            #endregion

            #region II

            //xml.WriteStartElement("II");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//II

            #endregion

            #region PIS

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
                    xml.WriteElementString("CST", "07");
                    xml.WriteEndElement(); //PISNT
                    break;
            }

            xml.WriteEndElement();//PIS

            #endregion

            #region PIS ST

            //xml.WriteStartElement("PISST");
            //xml.WriteElementString("", "");
            //xml.WriteEndElement();//PISST

            #endregion

            #region COFINS

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
                    xml.WriteElementString("CST", "07");
                    xml.WriteEndElement(); //COFINSNT
                    break;
            }

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

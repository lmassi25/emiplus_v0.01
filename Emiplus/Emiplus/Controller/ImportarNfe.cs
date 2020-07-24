using System;
using System.Collections;
using ChoETL;
using Emiplus.Data.Helpers;

namespace Emiplus.Controller
{
    internal class ImportarNfe
    {
        public ImportarNfe(string pathXml)
        {
            var config = new ChoXmlRecordConfiguration();
            config.NamespaceManager.AddNamespace("x", "http://www.portalfiscal.inf.br/nfe");

            dynamic loadNota = new ChoXmlReader(pathXml, config);
            foreach (var dataNota in loadNota)
            {
                this.dataNota = dataNota.ContainsKey("infNFe") ? dataNota.infNFe : dataNota;

                break;
            }

            LoadDados();
            LoadProdutos();
            LoadFornecedor();
            LoadPagamentos();
        }

        private dynamic dataNota { get; }

        /// <summary>
        ///     Dados da Nota
        /// </summary>
        public ArrayList Dados { get; set; }

        /// <summary>
        ///     Dados do Fornecedor
        /// </summary>
        private ArrayList Fornecedor { get; set; }

        /// <summary>
        ///     Lista de Produtos
        /// </summary>
        public ArrayList Produtos { get; set; }

        /// <summary>
        ///     Pagamentos com as parcelas
        /// </summary>
        private ArrayList Pagamentos { get; set; }

        public dynamic GetDados()
        {
            return Dados[0];
        }

        public dynamic GetProdutos()
        {
            return Produtos;
        }

        public dynamic GetFornecedor()
        {
            return Fornecedor[0];
        }

        public dynamic GetPagamentos()
        {
            return Pagamentos;
        }

        private void LoadDados()
        {
            Dados = new ArrayList
            {
                new
                {
                    Versao = dataNota.versao,
                    Nr = dataNota.ide.nNF,
                    Codigo = dataNota.ide.cNF,
                    dataNota.Id,
                    Serie = dataNota.ide.serie,
                    NaturezaOpercao = dataNota.ide.natOp,
                    Emissao = Validation.ConvertDateToForm(dataNota.ide.dhEmi, true),
                    dataNota.ide.cUF
                }
            };
        }

        private void LoadProdutos()
        {
            var produtos = dataNota.ContainsKey("dets") ? dataNota.dets : dataNota.det;

            Produtos = new ArrayList();
            if (dataNota.ContainsKey("dets"))
            {
                // Imp_ICMSSN_orig = item.imposto.ICMS[0].orig,

                foreach (var item in produtos)
                {
                    double vlrAditional = 0;
                    if (item.imposto.ICMS.ICMS10 != null)
                    {
                        var vlrImposto = Validation.ConvertToDouble(item.imposto.ICMS.ICMS10.vICMSST);
                        var qtd = Validation.ConvertToDouble(item.prod.qCom);
                        vlrAditional = vlrImposto / qtd;
                    }

                    var vlrItem = Validation.ConvertToDouble(item.prod.vUnCom.ToString().Replace(".", ","));
                    var totalItem = vlrItem + vlrAditional;
                    object obj = new
                    {
                        nrItem = item.nItem,
                        Referencia = item.prod.cProd,
                        CodeBarras = item.prod.cEAN,
                        Descricao = item.prod.xProd,
                        item.prod.NCM,
                        item.prod.CFOP,
                        Medida = item.prod.uCom,
                        Quantidade = item.prod.qCom,
                        VlrCompra = totalItem.ToString().Replace(",", ".")
                    };

                    Produtos.Add(obj);
                }
            }
            else
                Produtos.Add(
                    new
                    {
                        nrItem = produtos.nItem,
                        Referencia = produtos.prod.cProd,
                        CodeBarras = produtos.prod.cEAN,
                        Descricao = produtos.prod.xProd,
                        produtos.prod.NCM,
                        produtos.prod.CFOP,
                        Medida = produtos.prod.uCom,
                        Quantidade = produtos.prod.qCom,
                        VlrCompra = produtos.prod.vUnCom
                    });
        }

        private void LoadFornecedor()
        {
            if (dataNota.ContainsKey("emit"))
            {
                Fornecedor = new ArrayList();

                var document = "";
                var ie = "";
                var email = "";
                var fone = "";
                if (dataNota.emit.ContainsKey("CPF"))
                    document = dataNota.emit.CPF;

                if (dataNota.emit.ContainsKey("CNPJ"))
                    document = dataNota.emit.CNPJ;

                if (dataNota.emit.ContainsKey("IE"))
                    ie = dataNota.emit.IE;

                if (dataNota.emit.enderEmit.ContainsKey("fone"))
                    fone = dataNota.emit.enderEmit.fone;

                if (dataNota.emit.ContainsKey("email"))
                    email = dataNota.emit.email;

                Fornecedor.Add(
                    new
                    {
                        IE = ie,
                        CPFcnpj = document,
                        razaoSocial = dataNota.emit.xNome,
                        Addr_Rua = dataNota.emit.enderEmit.xLgr,
                        Addr_Nr = dataNota.emit.enderEmit.nro,
                        Addr_Bairro = dataNota.emit.enderEmit.xBairro,
                        Addr_IBGE = dataNota.emit.enderEmit.cMun,
                        Addr_Cidade = dataNota.emit.enderEmit.xMun,
                        Addr_UF = dataNota.emit.enderEmit.UF,
                        Addr_CEP = dataNota.emit.enderEmit.CEP,
                        Addr_cPais = dataNota.emit.enderEmit.cPais,
                        Addr_xPais = dataNota.emit.enderEmit.xPais,
                        Email = email,
                        Tel = fone
                    }
                );
            }
        }

        private void LoadPagamentos()
        {
            Pagamentos = new ArrayList();
            var datesTimes = new ArrayList();

            var dateTime = "";
            var Valor = "";
            var Tipo = "";

            if (dataNota.ContainsKey("cobr") == true)
            {
                if (dataNota.cobr is ChoDynamicObject)
                {
                    if (dataNota.cobr.ContainsKey("dups") == true)
                    {
                        var u = -1;
                        foreach (var dataCOBR in dataNota.cobr.dups)
                        {
                            u++;
                            //string dateTime = "";
                            //string Tipo = "";
                            //string Valor = "";

                            dateTime = dataCOBR.dVenc;
                            Valor = dataCOBR.vDup;

                            datesTimes.Add(dateTime);

                            Pagamentos.Add(new
                            {
                                dateTime,
                                Tipo,
                                Valor
                            });
                        }
                    }
                }
                else
                {
                    var u = -1;
                    foreach (var dataCOBR in dataNota.cobr)
                    {
                        u++;

                        dateTime = dataCOBR.dVenc;
                        Valor = dataCOBR.vDup;

                        Pagamentos.Add(new
                        {
                            dateTime,
                            Tipo,
                            Valor
                        });
                    }
                }
            }

            if (dataNota.ContainsKey("pag") == true)
            {
                Pagamentos.Clear();

                var u = -1;
                foreach (var dataCOBR in dataNota.pag)
                {
                    u++;

                    if (dataNota.ContainsKey("pag"))
                    {
                        Tipo = dataCOBR.Key == "detPag" ? (string) dataCOBR.Value.tPag : (string) dataCOBR.tPag;
                        Valor = dataCOBR.Key == "detPag" ? (string) dataCOBR.Value.vPag : (string) dataCOBR.vPag;
                    }

                    if (datesTimes.Count > 0) dateTime = datesTimes[u].ToString();

                    Pagamentos.Add(new
                    {
                        dateTime,
                        Tipo,
                        Valor
                    });
                }
            }
        }
    }
}
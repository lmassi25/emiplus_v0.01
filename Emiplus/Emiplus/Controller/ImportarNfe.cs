using ChoETL;
using Emiplus.Data.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Controller
{
    class ImportarNfe
    {
        private dynamic dataNota { get; set; }

        /// <summary>
        /// Dados da Nota
        /// </summary>
        public ArrayList Dados { get; set; }

        /// <summary>
        /// Dados do Fornecedor
        /// </summary>
        private ArrayList Fornecedor { get; set; }

        /// <summary>
        /// Lista de Produtos
        /// </summary>
        public ArrayList Produtos { get; set; }

        /// <summary>
        /// Totais dos impostos da NFe
        /// </summary>
        private ArrayList Totais { get; set; }

        /// <summary>
        /// Pagamentos com as parcelas
        /// </summary>
        private ArrayList Pagamentos { get; set; }

        public ImportarNfe(string pathXml)
        {
            ChoXmlRecordConfiguration config = new ChoXmlRecordConfiguration();
            config.NamespaceManager.AddNamespace("x", "http://www.portalfiscal.inf.br/nfe");

            dynamic loadNota = new ChoXmlReader(pathXml, config);
            foreach (dynamic dataNota in loadNota)
            {
                if (dataNota.ContainsKey("infNFe"))
                    this.dataNota = dataNota.infNFe;
                else
                    this.dataNota = dataNota;

                break;
            }

            LoadDados();
            LoadProdutos();
            LoadFornecedor();
            LoadPagamentos();
        }

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

        public ArrayList GetPagamentos()
        {
            return Pagamentos;
        }

        private void LoadDados()
        {
            Dados = new ArrayList();
            Dados.Add(new
            {
                Versao = dataNota.versao,
                Nr = dataNota.ide.nNF,
                Codigo = dataNota.ide.cNF,
                Id = dataNota.Id,
                Serie = dataNota.ide.serie,
                NaturezaOpercao = dataNota.ide.natOp,
                Emissao = Validation.ConvertDateToForm(dataNota.ide.dhEmi, true),
                cUF = dataNota.ide.cUF
            });
        }

        private void LoadProdutos()
        {
            dynamic produtos = null;
            if (dataNota.ContainsKey("dets"))
                produtos = dataNota.dets;
            else
                produtos = dataNota.det;

            Produtos = new ArrayList();
            if (dataNota.ContainsKey("dets"))
            {
                // Imp_ICMSSN_orig = item.imposto.ICMS[0].orig,

                foreach (var item in produtos)
                {
                    Produtos.Add(
                        new
                        {
                            nrItem = item.nItem,
                            Referencia = item.prod.cProd,
                            CodeBarras = item.prod.cEAN,
                            Descricao = item.prod.xProd,
                            NCM = item.prod.NCM,
                            CFOP = item.prod.CFOP,
                            Medida = item.prod.uCom,
                            Quantidade = item.prod.qCom,
                            VlrCompra = item.prod.vUnCom
                        });
                }
            }
            else
            {
                Produtos.Add(
                        new
                        {
                            nrItem = produtos.nItem,
                            Referencia = produtos.prod.cProd,
                            CodeBarras = produtos.prod.cEAN,
                            Descricao = produtos.prod.xProd,
                            NCM = produtos.prod.NCM,
                            CFOP = produtos.prod.CFOP,
                            Medida = produtos.prod.uCom,
                            Quantidade = produtos.prod.qCom,
                            VlrCompra = produtos.prod.vUnCom
                        });
            }
        }

        private void LoadFornecedor()
        {
            if (dataNota.ContainsKey("dest"))
            {
                Fornecedor = new ArrayList();

                string document = "";
                string ie = "";
                if (dataNota.dest.ContainsKey("CPF"))
                    document = dataNota.dest.CPF;

                if (dataNota.dest.ContainsKey("CNPJ"))
                    document = dataNota.dest.CNPJ;

                if (dataNota.dest.ContainsKey("IE"))
                    ie = dataNota.dest.IE;

                if (dataNota.dest.enderDest.ContainsKey("fone"))
                    ie = dataNota.dest.enderDest.fone;

                Fornecedor.Add(
                    new
                    {
                        IE = ie,
                        CPFcnpj = document,
                        razaoSocial = dataNota.dest.xNome,
                        Addr_Rua = dataNota.dest.enderDest.xLgr,
                        Addr_Nr = dataNota.dest.enderDest.nro,
                        Addr_Bairro = dataNota.dest.enderDest.xBairro,
                        Addr_IBGE = dataNota.dest.enderDest.cMun,
                        Addr_Cidade = dataNota.dest.enderDest.xMun,
                        Addr_UF = dataNota.dest.enderDest.UF,
                        Addr_CEP = dataNota.dest.enderDest.CEP,
                        Addr_cPais = dataNota.dest.enderDest.cPais,
                        Addr_xPais = dataNota.dest.enderDest.xPais
                    }
                );
            }
        }

        private void LoadPagamentos()
        {
            Pagamentos = new ArrayList();
            ArrayList datesTimes = new ArrayList();

            string dateTime = "";
            string Valor = "";
            string Tipo = "";

            if (dataNota.ContainsKey("cobr") == true)
            {
                if (dataNota.cobr is ChoETL.ChoDynamicObject)
                {
                    if (dataNota.cobr.ContainsKey("dups") == true)
                    {
                        int u = -1;
                        foreach (var dataCOBR in dataNota.cobr.dups)
                        {
                            u++;
                            //string dateTime = "";
                            //string Tipo = "";
                            //string Valor = "";

                            dateTime = dataCOBR.dVenc;
                            Valor = dataCOBR.vDup;

                            datesTimes.Add(new { dateTime });

                            Pagamentos.Add(new
                            {
                                dateTime = dateTime,
                                Tipo = Tipo,
                                Valor = Valor
                            });
                        }
                    }
                }
                else
                {
                    int u = -1;
                    foreach (var dataCOBR in dataNota.cobr)
                    {
                        u++;

                        dateTime = dataCOBR.dVenc;
                        Valor = dataCOBR.vDup;


                        Pagamentos.Add(new
                        {
                            dateTime = dateTime,
                            Tipo = Tipo,
                            Valor = Valor
                        });
                    }
                }

            }

            if (dataNota.ContainsKey("pag") == true)
            {
                Pagamentos.Clear();

                int u = -1;
                foreach (var dataCOBR in dataNota.pag)
                {
                    u++;

                    if (dataNota.ContainsKey("pag"))
                    {
                        if (dataCOBR.Key == "detPag")
                            Tipo = dataCOBR.Value.tPag;
                        else
                            Tipo = dataCOBR.tPag;

                        if (dataCOBR.Key == "detPag")
                            Valor = dataCOBR.Value.vPag;
                        else
                            Valor = dataCOBR.vPag;
                    }

                    if (datesTimes.Count > 0)
                    {
                        dateTime = datesTimes[u].ToString();
                    }

                    Pagamentos.Add(new
                    {
                        dateTime = dateTime,
                        Tipo = Tipo,
                        Valor = Valor
                    });
                }
            }
        }
    }
}

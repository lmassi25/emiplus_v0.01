using ChoETL;
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
        /// Versão 4.0
        /// </summary>
        private string versao { get; set; }
        private string Id { get; set; }
        private string Nature_Operacao { get; set; }
        private string Serie { get; set; }
        private string Nr { get; set; }
        private string Emissao { get; set; }

        //private string cnpj_CPF { get; set; }
        //private string Razao_Social { get; set; }
        //private string Addr_Rua { get; set; }
        //private string Addr_Nr { get; set; }
        //private string Addr_Bairro { get; set; }
        //private string Addr_IBGE { get; set; }
        //private string Addr_Cidade { get; set; }
        //private string Addr_UF { get; set; }
        //private string Addr_CEP { get; set; }

        /// <summary>
        /// Dados do Fornecedor
        /// </summary>
        private ArrayList Fornecedor { get; set; }

        /// <summary>
        /// Lista de Produtos
        /// </summary>
        private ArrayList Produtos { get; set; }

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

            LoadProdutos();
            LoadFornecedor();
            LoadPagamentos();
        }

        public ArrayList GetProdutos()
        {
            return Produtos;
        }

        public ArrayList GetFornecedor()
        {
            return Fornecedor;
        }

        public ArrayList GetPagamentos()
        {
            return Pagamentos;
        }

        private void LoadProdutos()
        {
            dynamic produtos = null;
            if (dataNota.ContainsKey("dets"))
                produtos = dataNota.dets;
            else
                produtos = dataNota.det;

            ArrayList Produtos = new ArrayList();
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
                ArrayList Fornecedor = new ArrayList();

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
            ArrayList Pagamentos = new ArrayList();

            if (dataNota.ContainsKey("cobr"))
            {
                dynamic cobr = dataNota.cobr;
                
                foreach (var dataCOBR in cobr)
                {

                }
            }

            if (dataNota.ContainsKey("pag"))
            {

            }
        }

    }
}

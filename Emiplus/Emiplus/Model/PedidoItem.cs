namespace Emiplus.Model
{
    using System;
    using SqlKata;
    using Data.Database;

    class PedidoItem : Model
    {
        public PedidoItem() : base("PEDIDO_ITEM") {}

        #region CAMPOS 

        //campos obrigatorios para todas as tabelas

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string EmpresaId { get; private set; }

        // referencia com a tabela Pedido

        public Pedido Pedido { get; set; }

        // referencia com a tabela Item 
        public Item Item { get; set; }

        // informações alteraveis na parte fiscal

        public string CProd { get; set; }
        public string CEan { get; set; }
        public string xProd { get; set; }               
        public string Ncm { get; set; } // 8 digitos 
        public string Cfop { get; set; } // 4 digitos
        public string Origem { get; set; } // 1 digitos

        // totais 

        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public double Quantidade { get; set; }
        public double Total { get; set; } // SOMASSE AO RESPECTIVO TOTAL
        public double Desconto { get; set; } // SOMASSE AO RESPECTIVO TOTAL
        public double DescontoItem { get; set; }
        public double DescontoPedido { get; set; }
        public double Frete { get; set; } // SOMASSE AO RESPECTIVO TOTAL
        public double Produtos { get; set; } // SOMASSE AO RESPECTIVO TOTAL
        public string Icms { get; set; } // CST CSOSN                
        public double IcmsBase { get; set; }
        public double IcmsReducaoAliq { get; set; }
        public double IcmsBaseComReducao { get; set; } // SOMASSE AO RESPECTIVO TOTAL
        public double IcmsAliq { get; set; }
        public double IcmsVlr { get; set; }  // SOMASSE AO RESPECTIVO TOTAL // VALOR DE ICMS DO ITEM 
        public double IcmsStBase { get; set; }
        public double IcmsStReducaoAliq { get; set; }
        public double IcmsStBaseComReducao { get; set; } // SOMASSE AO RESPECTIVO TOTAL
        public double IcmsStAliq { get; set; }
        public double IcmsSt { get; set; } // VALOR DE ICMSST DO ITEM  
        public string Ipi { get; set; } // CST
        public double IpiAliq { get; set; }
        public double IpiVlr { get; set; }  // SOMASSE AO RESPECTIVO TOTAL // VALOR DE IPI DO ITEM         
        public string Pis { get; set; } // CST
        public double PisAliq { get; set; }
        public double PisVlr { get; set; }  // SOMASSE AO RESPECTIVO TOTAL // VALOR DE PIS DO ITEM                 
        public string Cofins { get; set; } // CST
        public double CofinsAliq { get; set; }
        public double CofinsVlr { get; set; }  // SOMASSE AO RESPECTIVO TOTAL // VALOR DE COFINS DO ITEM    

        #endregion
    }
}

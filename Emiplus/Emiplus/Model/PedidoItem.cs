namespace Emiplus.Model
{
    using Data.Database;
    using SqlKata;
    using System;

    class PedidoItem : Model
    {
        public PedidoItem() : base("PEDIDO_ITEM") { }

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

        #region SQL CREATE
        //        CREATE TABLE PEDIDO_ITEM
        //        (
        //        id integer not null primary key,
        //        tipo integer not null,
        //        excluir integer,
        //        criado timestamp,
        //        atualizado timestamp,
        //        deletado timestamp,
        //        empresaid varchar(255),
        //        pedido integer,
        //        item integer,
        //        cprod varchar(255),
        //        cean varchar(255),
        //        xprod varchar(255),
        //        ncm varchar(255),
        //        cfop varchar(255),
        //        origem varchar(255),
        //        valorcompra numeric(18,4),
        //        valorvenda numeric(18,4),
        //        quantidade numeric(18,4),
        //        total numeric(18,4),
        //        desconto numeric(18,4),
        //        descontoitem numeric(18,4),
        //        descontopedido numeric(18,4),
        //        frete numeric(18,4),
        //        produtos numeric(18,4),
        //        icms numeric(18,4),
        //        icmsbase numeric(18,4),
        //        icmsreducaoaliq numeric(18,4),
        //        icmsbasecomreducao numeric(18,4),
        //        icmsaliq numeric(18,4),
        //        icmsvlr numeric(18,4),
        //        icmsstbase numeric(18,4),
        //        icmsstreducaoaliq numeric(18,4),
        //        icmsstbasecomreducao numeric(18,4),
        //        icmsstaliq numeric(18,4),
        //        icmsst numeric(18,4),
        //        ipi numeric(18,4),
        //        ipialiq numeric(18,4),
        //        ipivlr numeric(18,4),
        //        pis numeric(18,4),
        //        pisaliq numeric(18,4),
        //        pisvlr numeric(18,4),
        //        cofins numeric(18,4),
        //        cofinsaliq numeric(18,4),
        //        cofinsvlr numeric(18,4)
        //);

        #endregion

        #region Generator
        //                CREATE GENERATOR GEN_PEDIDO_ITEM_ID;

        //        SET TERM !! ;
        //        CREATE TRIGGER PEDIDO_ITEM_BI FOR PEDIDO_ITEM
        //        ACTIVE BEFORE INSERT POSITION 0
        //AS
        //DECLARE VARIABLE tmp DECIMAL(18,0);
        //        BEGIN
        //          IF(NEW.ID IS NULL) THEN
        //           NEW.ID = GEN_ID(GEN_PEDIDO_ITEM_ID, 1);
        //        ELSE
        //        BEGIN
        //    tmp = GEN_ID(GEN_PEDIDO_ITEM_ID, 0);
        //    if (tmp< new.ID) then
        //     tmp = GEN_ID(GEN_PEDIDO_ITEM_ID, new.ID - tmp);
        //        END
        //      END!!
        //SET TERM; !!


        #endregion

    }
}

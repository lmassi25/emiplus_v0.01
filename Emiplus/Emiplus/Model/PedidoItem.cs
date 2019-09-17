namespace Emiplus.Model
{
    using Data.Database;
    using Emiplus.Data.Helpers;
    using SqlKata;
    using SqlKata.Execution;
    using System;
    using System.Collections.Generic;

    class PedidoItem : Model
    {
        public PedidoItem() : base("PEDIDO_ITEM") { }

        #region CAMPOS 

        //campos obrigatorios para todas as tabelas

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }

        // referencia com a tabela Pedido

        public int Pedido { get; set; } // pedido id

        // referencia com a tabela Item 
        public int Item { get; set; } // item id
        [Ignore]
        public Item ItemObj { get; set; }

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
        public string Medida { get; set; }
        public double Total { get; set; } // SOMASSE AO RESPECTIVO TOTAL
        public double Desconto { get; set; } // É o resultado de DescontoItem + DescontoPedido
        public double DescontoItem { get; set; } // valor informado no item
        public double DescontoPedido { get; set; } //  desconto total lançado pelo usuário dividido pelo quantidade de itens lançados
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
        //CREATE TABLE PEDIDO_ITEM
        //(
        //id integer not null primary key,
        //tipo integer not null,
        //excluir integer,
        //criado timestamp,
        //atualizado timestamp,
        //deletado timestamp,
        //empresaid varchar(255),
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
        //  CREATE GENERATOR GEN_PEDIDO_ITEM_ID;

        //          SET TERM !! ;
        //  CREATE TRIGGER PEDIDO_ITEM_BI FOR PEDIDO_ITEM
        //  ACTIVE BEFORE INSERT POSITION 0
        //  AS
        //  DECLARE VARIABLE tmp DECIMAL(18,0);
        //  BEGIN
        //    IF(NEW.ID IS NULL) THEN
        //     NEW.ID = GEN_ID(GEN_PEDIDO_ITEM_ID, 1);
        //  ELSE
        //  BEGIN
        //      tmp = GEN_ID(GEN_PEDIDO_ITEM_ID, 0);
        //      if (tmp< new.ID) then
        //       tmp = GEN_ID(GEN_PEDIDO_ITEM_ID, new.ID - tmp);
        //  END
        //END!!
        //  SET TERM; !!


        #endregion

        public object FindByPedido(int id)
        {
            var data = Query().Where("pedido", id).Get();
            return data;
        }

        public PedidoItem SetId(int id)
        {
            Id = id;
            return this;
        }

        public PedidoItem SetPedidoId(int idPedido)
        {
            Pedido = idPedido;
            return this;
        }

        public PedidoItem SetItem(Item item)
        {
            ItemObj = item;
            Item = item.Id;

            ValorCompra = item.ValorCompra;

            Medida = item.Medida;

            return this;
        }

        public PedidoItem SetQuantidade(double quantidade)
        {
            if (Validation.IsNumber(quantidade))
            {
                Quantidade = quantidade == 0 ? 1 : quantidade;
            }

            return this;
        }

        public PedidoItem SetValorVenda(double valorVenda)
        {
            if (Validation.IsNumber(valorVenda))
            {
                ValorVenda = valorVenda == 0 ? ItemObj.ValorVenda : valorVenda;
            }

            return this;
        }

        public PedidoItem SetMedida(string medida)
        {
            Medida = String.IsNullOrEmpty(medida) ? ItemObj.Medida : medida;

            return this;
        }

        public PedidoItem SetProdutosTotal(double valor)
        {
            if (Validation.IsNumber(valor))
            {
                Produtos = valor;
            }

            return this;
        }

        public PedidoItem SetDescontoReal(double valor)
        {
            if (Validation.IsNumber(valor))
            {
                if (valor == 0)
                {
                    return this;
                }

                DescontoItem = valor;
            }

            return this;
        }

        public PedidoItem SetDescontoPorcentagens(double valor)
        {
            if (Validation.IsNumber(valor))
            {
                if (valor == 0)
                {
                    return this;
                }

                DescontoItem = (valor / 100 * (Produtos));
            }

            return this;
        }

        public PedidoItem SomarDescontoTotal()
        {
            Desconto = DescontoItem + DescontoPedido;

            return this;
        }

        public PedidoItem SomarProdutosTotal()
        {
            Produtos = Quantidade * ValorVenda;

            return this;
        }

        public PedidoItem SomarTotal()
        {
            SomarDescontoTotal();
            SomarProdutosTotal();

            Total = Produtos - Desconto;

            return this;
        }

        public bool Save(PedidoItem data)
        {
            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    return true;
                }
                else
                {
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }
            else
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    Alert.Message("Tudo certo!", "Atualizado com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }

            return true;
        }

        public bool Remove(int id)
        {
            var data = new { Excluir = 1, Deletado = DateTime.Now };
            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);
            return false;
        }
    }
}

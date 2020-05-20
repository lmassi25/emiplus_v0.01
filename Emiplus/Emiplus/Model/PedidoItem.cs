using System;
using System.Collections.Generic;
using System.Linq;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata;
using SqlKata.Execution;

namespace Emiplus.Model
{
    internal class PedidoItem : Data.Database.Model
    {
        public PedidoItem() : base("PEDIDO_ITEM")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

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

        [Ignore] public Item ItemObj { get; set; }

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
        public double Total { get; set; } // SOMA AO RESPECTIVO TOTAL
        public double Desconto { get; set; } // É o resultado de DescontoItem + DescontoPedido
        public double DescontoItem { get; set; } // valor informado no item

        public double DescontoPedido { get; set; }

        public double Frete { get; set; } // SOMA AO RESPECTIVO TOTAL
        public double TotalCompra { get; set; }
        public double TotalVenda { get; set; }
        public string Icms { get; set; } // CST CSOSN
        public double IcmsBase { get; set; }
        public double IcmsReducaoAliq { get; set; }
        public double IcmsBaseComReducao { get; set; } // SOMA AO RESPECTIVO TOTAL
        public double IcmsAliq { get; set; }
        public double IcmsVlr { get; set; } // SOMA AO RESPECTIVO TOTAL // VALOR DE ICMS DO ITEM
        public double IcmsStBase { get; set; }
        public double IcmsStReducaoAliq { get; set; }
        public double IcmsStBaseComReducao { get; set; } // SOMA AO RESPECTIVO TOTAL

        public double IcmsStAliq { get; set; }

        //public double IcmsSt { get; set; } // VALOR DE ICMSST DO ITEM
        public double Icmsstvlr { get; set; }
        public string Ipi { get; set; } // CST
        public double IpiAliq { get; set; }
        public double IpiVlr { get; set; } // SOMA AO RESPECTIVO TOTAL // VALOR DE IPI DO ITEM
        public string Pis { get; set; } // CST
        public double PisAliq { get; set; }
        public double PisVlr { get; set; } // SOMA AO RESPECTIVO TOTAL // VALOR DE PIS DO ITEM
        public string Cofins { get; set; } // CST
        public double CofinsAliq { get; set; }
        public double CofinsVlr { get; set; } // SOMA AO RESPECTIVO TOTAL // VALOR DE COFINS DO ITEM
        public string Cest { get; set; }
        public double Icms101Aliq { get; set; }
        public double Icms101Vlr { get; set; }
        public double Federal { get; set; }
        public double Estadual { get; set; }
        public double Municipal { get; set; }
        public string Info_Adicional { get; set; }
        public string Pedido_compra { get; set; }
        public string Item_Pedido_Compra { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }
        public double Devolucao { get; set; } // É o resultado de DevolucaoItem
        public double DevolucaoPedido { get; set; } // valor informado no item
        public double Seguro { get; set; }
        public double Despesa { get; set; }
        public string Mesa { get; set; }
        public string Status { get; set; }
        public int Usuario { get; set; }
        public int Atributo { get; set; }
        public string Adicional { get; set; }

        [Ignore] public string NomeAdicional { get; set; }

        [Ignore] public string TitleAtributo { get; set; }

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

        public PedidoItem SetTipo(string tipo)
        {
            Tipo = tipo;
            return this;
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

            CProd = !string.IsNullOrEmpty(item.Referencia) ? item.Referencia : item.Id.ToString();

            CEan = item.CodeBarras;
            xProd = item.Nome + " " + NomeAdicional + " " + TitleAtributo;

            ValorCompra = item.ValorCompra;

            Medida = item.Medida;
            Ncm = item.Ncm;
            Cest = item.Cest;
            Origem = item.Origem;

            return this;
        }

        public PedidoItem SetAdicionalNomePdt(string nome_adicional)
        {
            NomeAdicional = nome_adicional;

            return this;
        }

        public PedidoItem SetTitleAtributo(string atributoTitle)
        {
            TitleAtributo = atributoTitle;

            return this;
        }

        public PedidoItem SetAtributo(int atributo)
        {
            Atributo = atributo;

            return this;
        }

        public PedidoItem SetQuantidade(double quantidade)
        {
            if (quantidade.IsNumber()) Quantidade = quantidade == 0 ? 1 : quantidade;

            return this;
        }

        public bool SetValorVenda(double valorVenda)
        {
            if (valorVenda == 0 || valorVenda < 0)
            {
                if (Home.pedidoPage == "Compras")
                {
                    if (ItemObj.ValorCompra == 0 || ItemObj.ValorCompra < 0)
                    {
                        Alert.Message("Oppss", "É necessário definir um valor de compra.", Alert.AlertType.info);
                        return false;
                    }

                    ValorVenda = ItemObj.ValorCompra;
                    return true;
                }

                if (ItemObj.ValorVenda == 0 || ItemObj.ValorVenda < 0)
                {
                    Alert.Message("Oppss", "É necessário definir um valor de venda.", Alert.AlertType.info);
                    return false;
                }

                ValorVenda = ItemObj.ValorVenda;
                return true;
            }

            if (!valorVenda.IsNumber()) return false;

            ValorVenda = valorVenda;
            return true;
        }

        public PedidoItem SetMedida(string medida)
        {
            Medida = string.IsNullOrEmpty(medida) ? ItemObj.Medida : medida;

            return this;
        }

        public PedidoItem SetProdutosTotal(double valor)
        {
            if (valor.IsNumber()) TotalVenda = valor;

            return this;
        }

        public PedidoItem SetDescontoReal(double valor)
        {
            if (valor.IsNumber())
            {
                if (valor == 0) return this;

                DescontoItem = valor;
            }

            return this;
        }

        public PedidoItem SetDescontoPorcentagens(double valor)
        {
            if (valor.IsNumber())
            {
                if (valor == 0) return this;

                DescontoItem = valor / 100 * ValorVenda;
            }

            return this;
        }

        public PedidoItem SomarDescontoTotal()
        {
            Desconto = DescontoItem + DescontoPedido;

            return this;
        }

        public PedidoItem SomarDevolucaoTotal()
        {
            Devolucao = DevolucaoPedido;

            return this;
        }

        public double SomarProdutosTotal()
        {
            TotalVenda = Quantidade * ValorVenda;

            return TotalVenda;
        }

        public double SomarTotal()
        {
            SomarDescontoTotal();
            SomarDevolucaoTotal();
            SomarProdutosTotal();

            Total = TotalVenda + Frete + Seguro - (Desconto + Devolucao);

            return Total;
        }

        public Dictionary<string, double> SumTotais(int id)
        {
            var queryP = Query().SelectRaw(
                    "SUM(total) AS total, " +
                    "SUM(desconto) AS desconto, " +
                    "SUM(devolucao) AS devolucao, " +
                    "SUM(totalvenda) AS totalvenda, " +
                    "SUM(frete) AS frete, SUM(icmsbase) AS icmsbase, " +
                    "SUM(icmsvlr) AS icmsvlr, " +
                    "SUM(icmsstbase) AS icmsstbase, " +
                    "SUM(icmsstvlr) as icmsstvlr, " +
                    "SUM(ipivlr) AS ipivlr, " +
                    "SUM(pisvlr) AS pisvlr, " +
                    "SUM(seguro) AS seguro, " +
                    "SUM(despesa) AS despesa, " +
                    "SUM(federal) AS federal, " +
                    "SUM(estadual) AS estadual, " +
                    "SUM(municipal) AS municipal, " +
                    "SUM(cofinsvlr) AS cofinsvlr")
                .Where("pedido", id)
                .Where("tipo", "Produtos")
                .Where("excluir", 0)
                .Get();

            var queryS = Query().SelectRaw(
                    "SUM(total) AS total, " +
                    "SUM(desconto) AS desconto, " +
                    "SUM(devolucao) AS devolucao, " +
                    "SUM(totalvenda) AS totalvenda, " +
                    "SUM(frete) AS frete, SUM(icmsbase) AS icmsbase, " +
                    "SUM(icmsvlr) AS icmsvlr, " +
                    "SUM(icmsstbase) AS icmsstbase, " +
                    "SUM(icmsstvlr) as icmsstvlr, " +
                    "SUM(ipivlr) AS ipivlr, " +
                    "SUM(pisvlr) AS pisvlr, " +
                    "SUM(cofinsvlr) AS cofinsvlr")
                .Where("pedido", id)
                //.Where("tipo", "Servicos")
                .Where("tipo", "Serviços")
                .Where("excluir", 0)
                .Get();

            var Somas = new Dictionary<string, double> {{"Id", Validation.ConvertToDouble(id)}};

            for (var i = 0; i < queryP.Count(); i++)
            {
                var data = queryP.ElementAt(i);

                Somas.Add("Produtos", Validation.ConvertToDouble(data.TOTALVENDA));
                Somas.Add("Frete", Validation.ConvertToDouble(data.FRETE));
                Somas.Add("Desconto", Validation.ConvertToDouble(data.DESCONTO));
                Somas.Add("Devolucao", Validation.ConvertToDouble(data.DEVOLUCAO));
                Somas.Add("IPI", Validation.ConvertToDouble(data.IPIVLR));
                Somas.Add("ICMSBASE", Validation.ConvertToDouble(data.ICMSBASE));
                Somas.Add("ICMS", Validation.ConvertToDouble(data.ICMSVLR));
                Somas.Add("ICMSSTBASE", Validation.ConvertToDouble(data.ICMSSTBASE));
                Somas.Add("ICMSST", Validation.ConvertToDouble(data.ICMSSTVLR));
                Somas.Add("COFINS", Validation.ConvertToDouble(data.COFINSVLR));
                Somas.Add("PIS", Validation.ConvertToDouble(data.PISVLR));
                Somas.Add("SEGURO", Validation.ConvertToDouble(data.SEGURO));
                Somas.Add("DESPESA", Validation.ConvertToDouble(data.DESPESA));
                Somas.Add("FEDERAL", Validation.ConvertToDouble(data.FEDERAL));
                Somas.Add("ESTADUAL", Validation.ConvertToDouble(data.ESTADUAL));
                Somas.Add("MUNICIPAL", Validation.ConvertToDouble(data.MUNICIPAL));
            }

            for (var i = 0; i < queryS.Count(); i++)
            {
                var data = queryS.ElementAt(i);

                Somas.Add("Servicos", Validation.ConvertToDouble(data.TOTALVENDA));
                Somas.Add("DescontoServicos", Validation.ConvertToDouble(data.DESCONTO));
            }

            return Somas;
        }

        public bool Save(PedidoItem data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;
            data.Usuario = Settings.Default.user_id;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                    return true;
                
                if (message)
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
            }

            if (data.Id > 0)
            {
                if (!data.IgnoringDefaults)
                {
                    data.status_sync = "UPDATE";
                    data.Atualizado = DateTime.Now;
                }

                if (Data(data).Update("ID", data.Id) == 1)
                    return true;

                if (message)
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
            }

            return false;
        }

        public bool Remove(int id)
        {
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
            if (Data(data).Update("ID", id) == 1)
                return true;

            Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);
            return false;
        }
    }
}
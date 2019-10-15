﻿namespace Emiplus.Model
{
    using Data.Database;
    using Emiplus.Data.Helpers;
    using SqlKata;
    using System;
    using System.Collections.Generic;

    class Pedido : Model
    {
        public Pedido() : base("PEDIDO") {}

        #region CAMPOS 
        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }

        // referencia com a tabela Pessoa
        public int Cliente { get; set; }
        //public Pessoa Cliente { get; set; }
        public int Colaborador { get; set; }
        //public Pessoa Colaborador { get; set; }

        // totais 
        public double Total { get; set; }
        public double Desconto { get; set; }
        public double Frete { get; set; }
        public double Produtos { get; set; }
        public double ICMS { get; set; }
        public double ICMSST { get; set; }
        public double IPI { get; set; }
        public double ICMSBASE { get; set; }
        public double ICMSSTBASE { get; set; }
        public double PIS { get; set; }
        public double COFINS { get; set; }

        public int status { get; set; }
        public DateTime Emissao { get; set; }

        public string HoraSaida { get; set; }
        public int TipoNFe { get; set; }
        public int Finalidade { get; set; }
        public int Destino { get; set; }
        public int id_natureza { get; set; }
        public string info_contribuinte { get; set; }
        public string info_fisco { get; set; }
        public int id_useraddress { get; set; }

        public int TipoFrete { get; set; }
        public string Volumes_Frete { get; set; }
        public string PesoLiq_Frete { get; set; }
        public string PesoBruto_Frete { get; set; }
        public string Especie_Frete { get; set; }
        public string Marca_Frete { get; set; }
        public int Id_Transportadora { get; set; }

        #endregion

        #region SQL CREATE
        //CREATE TABLE PEDIDO
        //(
        //id integer not null primary key,
        //tipo integer not null,
        //excluir integer,
        //criado timestamp,
        //atualizado timestamp,
        //deletado timestamp,
        //empresaid varchar(255),
        //        cliente integer,
        //        colaborador integer,
        //        total numeric(18, 4),
        //        desconto numeric(18, 4),
        //        frete numeric(18, 4),
        //        produtos numeric(18, 4),
        //        icms numeric(18, 4),
        //        icmsst numeric(18, 4),
        //        ipi numeric(18, 4),
        //        icmsbase numeric(18, 4),
        //        icmstbase numeric(18, 4),
        //        pis numeric(18, 4),
        //        cofins numeric(18, 4)
        //);
        #endregion

        #region Generator
        //  CREATE GENERATOR GEN_PEDIDO_ID;

        //  SET TERM !! ;
        //  CREATE TRIGGER PEDIDO_BI FOR PEDIDO
        //  ACTIVE BEFORE INSERT POSITION 0
        //  AS
        //  DECLARE VARIABLE tmp DECIMAL(18,0);
        //  BEGIN
        //    IF(NEW.ID IS NULL) THEN
        //     NEW.ID = GEN_ID(GEN_PEDIDO_ID, 1);
        //  ELSE
        //  BEGIN
        //      tmp = GEN_ID(GEN_PEDIDO_ID, 0);
        //      if (tmp< new.ID) then
        //       tmp = GEN_ID(GEN_PEDIDO_ID, new.ID - tmp);
        //  END
        //END!!
        //  SET TERM; !!

        #endregion

        public Pedido SaveTotais(Dictionary<string, double> data)
        {
            Id = Validation.ConvertToInt32(data["Id"]);
            Produtos = data["Produtos"];
            Frete = data["Frete"];
            Desconto = data["Desconto"];
            IPI = data["IPI"];
            ICMSBASE = data["ICMSBASE"];
            ICMS = data["ICMS"];
            ICMSSTBASE = data["ICMSSTBASE"];
            ICMSST = data["ICMSST"];
            COFINS = data["COFINS"];
            PIS = data["PIS"];
            Total = (Produtos + Frete + IPI + ICMSST) - Desconto;

            return this;
        }

        public double GetDesconto()
        {
            return Desconto;
        }

        public double GetTotal()
        {
            return Total;
        }

        public bool Save(Pedido data)
        {
            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                data.Emissao = DateTime.Now;
                if (Data(data).Create() != 1)
                    return false;
            }

            if (data.Id > 0)
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) != 1)
                    return false;
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

namespace Emiplus.Model
{
    using Data.Database;
    using Emiplus.Data.Helpers;
    using SqlKata;
    using System;
    using System.Collections.Generic;

    class Pedido : Model
    {
        public Pedido() : base("PEDIDO")
        {
        }

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

        public Pedido SetDescontoReal(double valor)
        {
            if (Validation.IsNumber(valor))
            {
                if (valor == 0)
                {
                    return this;
                }

                Desconto = valor;
            }

            return this;
        }

        public Pedido SetDescontoPorcentagens(double valor)
        {
            if (Validation.IsNumber(valor))
            {
                if (valor == 0)
                {
                    return this;
                }

                Desconto = (valor / 100 * (Total));
            }

            return this;
        }

        public bool Save(Pedido data)
        {
            //if (ValidarDados(data))
            //{
            //    return false;
            //}

            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                data.Emissao = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    //Alert.Message("Tudo certo!", "Categoria salvo com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    //Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }
            else
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    //Alert.Message("Tudo certo!", "Categoria atualizada com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    //Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }

            return true;
        }

        public bool ValidarDados(Pedido data)
        {
            /*var result = ValitRules<Item>
                .Create()
                .Ensure(m => m.Nome, _ => _
                    .Required()
                    .WithMessage("Nome é obrigatorio.")
                    .MinLength(2)
                    .WithMessage("N é possivel q seu nome seja menor q 2 caracateres"))
                .For(data)
                .Validate();

            if (!result.Succeeded)
            {
                foreach (var message in result.ErrorMessages)
                {
                    Alert.Message("Opss!", message, Alert.AlertType.error);
                    return true;
                }
                return true;
            }*/

            return false;
        }
    }
}

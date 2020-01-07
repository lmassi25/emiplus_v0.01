namespace Emiplus.Model
{
    using Data.Database;
    using Emiplus.Data.Helpers;
    using SqlKata;
    using System;
    using Valit;

    class Imposto : Model
    {
        public Imposto() : base("IMPOSTO")
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
        public string id_empresa { get; private set; } = Program.UNIQUE_ID_EMPRESA;

        public string Nome { get; set; }

        public string Cfop { get; set; }

        public string Icms { get; set; } // CST CSOSN   
        public double IcmsReducaoAliq { get; set; }

        public double IcmsIva { get; set; }

        public double IcmsAliq { get; set; }

        public double IcmsStIva { get; set; }

        public double IcmsStReducaoAliq { get; set; }
        public double IcmsStAliq { get; set; }
        public string Ipi { get; set; } // CST
        public double IpiAliq { get; set; }
        public string Pis { get; set; } // CST
        public double PisAliq { get; set; }
        public string Cofins { get; set; } // CST
        public double CofinsAliq { get; set; }
        
        public int id_sync { get; set; }
        public string status_sync { get; set; }
        #endregion

        #region SQL

        //CREATE TABLE Impostos
        //(
        //    id integer not null primay key,
        //    tipo integer,
        //    excluir integer,
        //    criado timestamp,
        //    atualizado timestamp,
        //    deletado timestamp,
        //    empresaid integer,
        //    nome varchar(255),
        //    icms varchar(10),
        //    IcmsReducaoAliq numeric(18,4),
        //    IcmsAliq numeric(18,4),
        //    IcmsStIva numeric(18,4),
        //    IcmsStReducaoAliq numeric(18,4),
        //    IcmsStAliq numeric(18,4),
        //    Ipi varchar(10),
        //    IpiAliq numeric(18,4),
        //    Pis varchar(10),
        //    PisAliq numeric(18,4),
        //    Cofins varchar(10),
        //    CofinsAliq numeric(18,4),
        //);

        //CREATE GENERATOR GEN_IMPOSTO_ID;

        //SET TERM !! ;
        //        CREATE TRIGGER IMPOSTO_BI FOR IMPOSTO
        //        ACTIVE BEFORE INSERT POSITION 0
        //AS
        //DECLARE VARIABLE tmp DECIMAL(18,0);
        //        BEGIN
        //          IF(NEW.ID IS NULL) THEN
        //           NEW.ID = GEN_ID(GEN_IMPOSTO_ID, 1);
        //        ELSE
        //        BEGIN
        //    tmp = GEN_ID(GEN_IMPOSTO_ID, 0);
        //    if (tmp< new.ID) then
        //     tmp = GEN_ID(GEN_IMPOSTO_ID, new.ID - tmp);
        //        END
        //      END!!
        //SET TERM; !!

        #endregion 

        public bool Save(Imposto data)
        {
            if (ValidarDados(data))            
                return false;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    Alert.Message("Tudo certo!", "Imposto salvo com sucesso.", Alert.AlertType.success);
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
                    Alert.Message("Tudo certo!", "Imposto atualizada com sucesso.", Alert.AlertType.success);
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
                Alert.Message("Pronto!", "Imposto removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover o Imposto.", Alert.AlertType.error);
            return false;
        }

        /// <summary>
        /// <para>Valida os campos do Model</para>
        /// <para>Documentação: <see cref="https://valitdocs.readthedocs.io/en/latest/validation-rules/index.html"/> </para>
        /// </summary>
        /// <param name="data">Objeto com valor dos atributos do Model Item</param>
        /// <returns>Retorna booleano e Mensagem</returns>
        public bool ValidarDados(Imposto data)
        {
            var result = ValitRules<Imposto>
                .Create()
                .Ensure(m => m.Nome, _ => _
                    .Required()
                    .WithMessage("Preencha o título do Imposto.")
                    .MinLength(2)
                    .WithMessage("Título do Imposto é muito curto."))
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
            }

            return false;
        }
    }
}



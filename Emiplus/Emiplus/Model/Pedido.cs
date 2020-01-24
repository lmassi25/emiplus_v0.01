namespace Emiplus.Model
{
    using Data.Database;
    using Emiplus.Data.Helpers;
    using Emiplus.Properties;
    using Emiplus.View.Common;
    using SqlKata;
    using SqlKata.Execution;
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

        public string Voucher { get; set; }

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
        public double Servicos { get; set; }
        public double ICMS { get; set; }
        public double ICMSST { get; set; }
        public double IPI { get; set; }
        public double ICMSBASE { get; set; }
        public double ICMSSTBASE { get; set; }
        public double PIS { get; set; }
        public double COFINS { get; set; }
        public int status { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Saida { get; set; }
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
        public int Id_Caixa { get; set; }
        public int id_usuario { get; set; }
        public string cfe_nome { get; set; }
        public string cfe_cpf { get; set; }
        public string Chavedeacesso { get; set; }
        public int Venda { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

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
            Servicos = data["Servicos"];
            Frete = data["Frete"];
            Desconto = data["Desconto"] + data["DescontoServicos"];
            IPI = data["IPI"];
            ICMSBASE = data["ICMSBASE"];
            ICMS = data["ICMS"];
            ICMSSTBASE = data["ICMSSTBASE"];
            ICMSST = data["ICMSST"];
            COFINS = data["COFINS"];
            PIS = data["PIS"];
            Total = (Produtos + Servicos + Frete + IPI + ICMSST) - Desconto;

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

        public SqlKata.Query FindByVoucher(string voucher)
        {
            return Query().Where("voucher", voucher.ToUpper());
        }

        public bool Save(Pedido data)
        {
            data.Id_Caixa = Home.idCaixa;
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;
            data.id_usuario = Settings.Default.user_id;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Id_Caixa = Home.idCaixa;
                data.Criado = DateTime.Now;
                data.Emissao = DateTime.Now;
                data.Colaborador = Settings.Default.user_id;
                if (Data(data).Create() != 1)
                    return false;
            }
            else
            {
                data.status_sync = "UPDATE";
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) != 1)
                    return false;
            }

            return true;
        }

        public bool Remove(int id, string column = "ID", bool message = true)
        {
            var data = new { Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE" };
            if (Data(data).Update(column, id) == 1)
            {
                if (message)
                    Alert.Message("Pronto!", "Removido com sucesso.", Alert.AlertType.info);

                return true;
            }

            if (message)
                Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);

            return false;
        }
    }
}

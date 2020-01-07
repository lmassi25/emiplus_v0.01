namespace Emiplus.Model
{
    using Data.Database;
    using Data.Helpers;
    using Emiplus.Properties;
    using SqlKata;
    using System;

    internal class CaixaMovimentacao : Model
    {
        public CaixaMovimentacao() : base("CAIXA_MOV")
        {
        }

        #region CAMPOS 
        
        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string id_empresa { get; private set; } = Program.UNIQUE_ID_EMPRESA;
        public int id_caixa { get; set; }
        public int id_user { get; set; } = Settings.Default.user_id;
        public int Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public int id_formapgto { get; set; }
        public int id_categoria { get; set; }
        public int id_pessoa { get; set; }
        public string Obs { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }
        #endregion 

        public bool Save(CaixaMovimentacao data)
        {
            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    Alert.Message("Tudo certo!", "Movimentação feita com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    Alert.Message("Opss", "Erro ao adicionar movimentação, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }
            else
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    Alert.Message("Tudo certo!", "Movimentação atualizada com sucesso.", Alert.AlertType.success);
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
                Alert.Message("Pronto!", "Movimentação removida com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);
            return false;
        }
    }
}
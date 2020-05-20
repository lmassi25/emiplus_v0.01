using System;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using SqlKata;

namespace Emiplus.Model
{
    internal class CaixaMovimentacao : Data.Database.Model
    {
        public CaixaMovimentacao() : base("CAIXA_MOV")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public string id_empresa { get; private set; }
        public int id_caixa { get; set; }
        public int id_user { get; set; }
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

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

        public bool Save(CaixaMovimentacao data, bool message = true)
        {
            data.id_user = Settings.Default.user_id;
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Movimentação feita com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao adicionar movimentação, verifique os dados.", Alert.AlertType.error);
            }

            if (data.Id > 0)
            {
                if (!data.IgnoringDefaults)
                {
                    data.status_sync = "UPDATE";
                    data.Atualizado = DateTime.Now;
                }

                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Movimentação atualizada com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
            }

            return false;
        }

        public bool Remove(int id)
        {
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
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
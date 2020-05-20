using System;
using Emiplus.Data.Helpers;
using SqlKata;

namespace Emiplus.Model
{
    internal class Caixa : Data.Database.Model
    {
        public Caixa() : base("CAIXA")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public string Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }
        public int Usuario { get; set; }
        public double Saldo_Inicial { get; set; }
        public double Saldo_Final { get; set; }
        public double Saldo_Final_Informado { get; set; }
        public string Observacao { get; set; }
        public string Terminal { get; set; }
        public DateTime Fechado { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

        public bool Save(Caixa data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Caixa aberto com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao adicionar caixa, verifique os dados.", Alert.AlertType.error);
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
                        Alert.Message("Tudo certo!", "Caixa atualizado com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
            }

            return false;
        }

        public bool Remove(int id)
        {
            var data = new
            {
                Excluir = 1, 
                Deletado = DateTime.Now,
                status_sync = "UPDATE"
            };
            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Caixa removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover o caixa.", Alert.AlertType.error);
            return false;
        }
    }
}
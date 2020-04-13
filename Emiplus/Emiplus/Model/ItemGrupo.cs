namespace Emiplus.Model
{
    using Data.Database;
    using Data.Helpers;
    using SqlKata;
    using System;

    internal class ItemGrupo : Model
    {
        public ItemGrupo() : base("ITEM_GRUPOS")
        {
        }

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string id_empresa { get; private set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string Title { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public bool Save(ItemGrupo data)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;

                if (Data(data).Create() == 1)
                    Alert.Message("Tudo certo!", "Grupo salvo com sucesso.", Alert.AlertType.success);
                else
                {
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }
            

            if (data.Id != 0)
            {
                data.status_sync = "UPDATE";
                data.Atualizado = DateTime.Now;
                
                if (Data(data).Update("ID", data.Id) == 1)
                    Alert.Message("Tudo certo!", "Grupo atualizado com sucesso.", Alert.AlertType.success);
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
            var data = new {
                Excluir = 1,
                Deletado = DateTime.Now,
                status_sync = "UPDATE"
                };

            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Grupo removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover o grupo de atributos.", Alert.AlertType.error);
            return false;
        }
    }
}
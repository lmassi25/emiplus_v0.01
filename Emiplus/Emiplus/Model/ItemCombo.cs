using System;
using System.Collections;
using Emiplus.Data.Helpers;
using SqlKata;
using SqlKata.Execution;

namespace Emiplus.Model
{
    internal class ItemCombo : Data.Database.Model
    {
        public ItemCombo() : base("ITEM_COMBO")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }
        public string Nome { get; set; }
        public string Produtos { get; set; }
        public double ValorVenda { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public ArrayList GetCombos(string combos)
        {
            var listCombos = new ArrayList { new { Id = "0", Nome = "Selecione" } };
            
            var idCombos = combos.Split(',');
            foreach (var id in idCombos)
            {
                var dataCombo = FindById(Validation.ConvertToInt32(id)).WhereFalse("excluir").FirstOrDefault<ItemCombo>();
                if (dataCombo != null)
                    listCombos.Add(new { Id = $"{dataCombo.Id}", Nome = $"{dataCombo.Nome}" });
            }

            return listCombos;
        }

        public bool Save(ItemCombo data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message) Alert.Message("Tudo certo!", "Combo salvo com sucesso.", Alert.AlertType.success);
                    return true;
                }

                Alert.Message("Opss", "Erro ao criar combo, verifique os dados.", Alert.AlertType.error);
                return false;
            }

            if (data.Id > 0)
            {
                data.status_sync = "UPDATE";
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Combo de produtos atualizado com sucesso.",
                            Alert.AlertType.success);

                    return true;
                }

                Alert.Message("Opss", "Erro ao atualizar o combo, verifique os dados.", Alert.AlertType.error);
                return false;
            }

            return true;
        }

        public bool Remove(int id, bool message = true)
        {
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
            if (Data(data).Update("ID", id) == 1)
            {
                if (message)
                    Alert.Message("Pronto!", "Removido com sucesso.", Alert.AlertType.info);

                return true;
            }

            if (message)
                Alert.Message("Opss!", "Não foi possível remover o combo.", Alert.AlertType.error);

            return false;
        }
    }
}
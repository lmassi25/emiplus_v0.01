using Emiplus.Data.Helpers;
using SqlKata;
using System;

namespace Emiplus.Model
{
    using Data.Database;
    using SqlKata.Execution;

    internal class Estoque : Model
    {
        public Estoque() : base("ESTOQUE")
        {
        }

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int id_item { get; set; }
        public DateTime Criado { get; private set; }
        public string id_empresa { get; set; }
        public double estoque { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public void GerarEstoque()
        {
            dynamic items = new Item().FindAll().Where("excluir", 0).Where("tipo", "Produtos").Get();
            if (items != null) {
                foreach (dynamic data in items)
                {
                    int id = data.ID;
                    double estoque = Validation.ConvertToDouble(data.ESTOQUEATUAL);

                    dynamic itemsCheck = FindAll().Where("id_item", id).Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Now)).FirstOrDefault<Estoque>();
                    if (itemsCheck == null)
                    {
                        id_item = id;
                        this.estoque = estoque;
                        Save(this);
                    }
                }
            }
        }

        public bool Save(Estoque data)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;
            data.id_sync = Validation.RandomSecurity();
            data.status_sync = "CREATE";
            data.Criado = DateTime.Now;
            if (Data(data).Create() == 1)
                return true;

            Alert.Message("Opss", "Erro ao adicionar estoque, verifique os dados.", Alert.AlertType.error);
            return false;
        }
    }
}
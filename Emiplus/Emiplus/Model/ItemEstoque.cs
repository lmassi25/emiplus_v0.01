namespace Emiplus.Model
{
    using Data.Database;
    using Data.Helpers;
    using SqlKata;
    using System;

    internal class ItemEstoque : Model
    {
        public ItemEstoque() : base("ITEM_ESTOQUE") {}

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string id_empresa { get; private set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public int Item { get; set; }
        public string Referencia { get; set; }
        public string Codebarras { get; set; }
        public string Atributo { get; set; }
        public double Estoque { get; set; }
        public int Usuario { get; set; }
        public int Vendido { get; set; }
        public string Title { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public bool Save(ItemEstoque data)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;

                if (Data(data).Create() == 1)
                    return true;
                
                return false;
            }
            
            if (data.Id != 0)
            {
                data.status_sync = "UPDATE";
                data.Atualizado = DateTime.Now;
                
                if (Data(data).Update("ID", data.Id) == 1)
                    return true;
                
                return false;
            }

            return false;
        }

        public bool Remove(int id)
        {
            var data = new {
                Excluir = 1,
                Deletado = DateTime.Now,
                status_sync = "UPDATE"
                };

            if (Data(data).Update("ID", id) == 1)
                return true;

            return false;
        }
    }
}
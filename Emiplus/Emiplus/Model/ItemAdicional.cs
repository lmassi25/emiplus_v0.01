namespace Emiplus.Model
{
    using Data.Database;
    using Data.Helpers;
    using SqlKata;
    using System;

    internal class ItemAdicional : Model
    {
        public ItemAdicional() : base("ITEM_ADICIONAL") {}

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string id_empresa { get; private set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string Title { get; set; }
        public double Valor { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public bool Save(ItemAdicional data)
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

        public bool Remove(int id, string column = "ID")
        {
            var data = new {
                Excluir = 1,
                Deletado = DateTime.Now,
                status_sync = "UPDATE"
                };

            if (Data(data).Update(column, id) == 1)
                return true;

            return false;
        }
    }
}
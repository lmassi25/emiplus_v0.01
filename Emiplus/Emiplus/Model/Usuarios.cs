namespace Emiplus.Model
{
    using Data.Database;
    using SqlKata;
    using System;

    internal class Usuarios : Model
    {
        public Usuarios() : base("USUARIOS")
        {
        }

        #region CAMPOS 

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; } = Program.UNIQUE_ID_EMPRESA;
        public string Nome { get; set; }
        public int Id_User { get; set; }
        public int Comissao { get; set; }
        public int Sub_user { get; set; }
        public string email { get; set; }
        public string senha { get; set; }

        #endregion 

        public SqlKata.Query FindByUserId(int id)
        {
            return Query().Where("id_user", id);
        }

        public bool Save(Usuarios data)
        {
            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                if (Data(data).Create() != 1)
                {
                    return false;
                }
            }
            else
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
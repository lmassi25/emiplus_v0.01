using System;
using System.Collections.Generic;
using System.Linq;
using SqlKata;
using SqlKata.Execution;

namespace Emiplus.Model
{
    internal class Usuarios : Data.Database.Model
    {
        public Usuarios() : base("USUARIOS")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; } = Program.UNIQUE_ID_EMPRESA;
        public string Nome { get; set; }
        public int Id_User { get; set; }
        public int Comissao { get; set; }
        public int Sub_user { get; set; }
        public string email { get; set; }
        public string senha { get; set; }

        public Query FindByUserId(int id)
        {
            return Query().Where("id_user", id);
        }

        /// <summary>
        ///     Retorna todos usuarios em um ArrayList
        /// </summary>
        public List<Usuarios> GetAllUsers()
        {
            var users = new List<Usuarios> {new Usuarios {Id = 0, Nome = "Todos"}};

            var usuarios = FindAll().Where("excluir", 0).Get();
            if (usuarios != null) users.AddRange(usuarios.Select(item => new Usuarios {Id = item.ID_USER, Nome = item.NOME}));

            return users;
        }

        public bool Save(Usuarios data)
        {
            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                if (Data(data).Create() != 1) return false;
            }
            else
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) != 1) return false;
            }

            return true;
        }
    }
}
using System;
using System.Collections;
using SqlKata;
using SqlKata.Execution;

namespace Emiplus.Model
{
    internal class FormaPagamento : Data.Database.Model
    {
        public FormaPagamento() : base("FORMAPGTO")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; } = Program.UNIQUE_ID_EMPRESA;
        public string Nome { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }


        public ArrayList GetAll()
        {
            var data = new ArrayList {new {Id = "0", Nome = "SELECIONE"}};

            var findDB = Query().Where("excluir", 0).OrderByDesc("nome").Get();
            if (findDB != null)
                foreach (var item in findDB)
                    data.Add(new {Id = $"{item.ID}", Nome = $"{item.NOME}"});

            return data;
        }
    }
}
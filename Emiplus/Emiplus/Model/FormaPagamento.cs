namespace Emiplus.Model
{
    using Data.Database;
    using SqlKata;
    using SqlKata.Execution;
    using System;
    using System.Collections;

    internal class FormaPagamento : Model
    {
        public FormaPagamento() : base("FORMAPGTO")
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
        public int id_sync { get; set; }
        public string status_sync { get; set; }
        #endregion CAMPOS


        public ArrayList GetAll()
        {
            var data = new ArrayList();
            data.Add(new { Id = "0", Nome = "SELECIONE" });

            var findDB = Query().Where("excluir", 0).OrderByDesc("nome").Get();
            if (findDB != null)
            {
                foreach (var item in findDB)
                    data.Add(new { Id = $"{item.ID}", Nome = $"{item.NOME}" });
            }
            
            return data;
        }
    }
}
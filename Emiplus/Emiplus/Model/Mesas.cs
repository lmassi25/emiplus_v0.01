﻿using System;
using Emiplus.Data.Helpers;
using SqlKata;

namespace Emiplus.Model
{
    internal class Mesas : Data.Database.Model
    {
        public Mesas() : base("MESA")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public string id_empresa { get; private set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public int NrPessoas { get; set; }
        public string Mesa { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public bool Save(Mesas data)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;

                return Data(data).Create() == 1;
            }

            if (data.Id != 0)
            {
                data.status_sync = "UPDATE";
                data.Atualizado = DateTime.Now;

                return Data(data).Update("ID", data.Id) == 1;
            }

            return false;
        }

        public bool Remove(int id, string column = "ID")
        {
            var data = new
            {
                Excluir = 1,
                Deletado = DateTime.Now,
                status_sync = "UPDATE"
            };

            return Data(data).Update(column, id) == 1;
        }
    }
}
﻿using System;
using Emiplus.Data.Helpers;
using SqlKata;

namespace Emiplus.Model
{
    internal class Taxas : Data.Database.Model
    {
        public Taxas() : base("TAXAS")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public string id_empresa { get; private set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string Nome { get; set; }
        public double Taxa_Fixa { get; set; }
        public double Taxa_Credito { get; set; }
        public double Taxa_Debito { get; set; }
        public double Taxa_Parcela { get; set; }
        public int Parcela_Semjuros { get; set; }
        public int Dias_Receber { get; set; }
        public int Antecipacao_Auto { get; set; }
        public double Taxa_Antecipacao { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public bool Save(Taxas data)
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
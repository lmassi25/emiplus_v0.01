namespace Emiplus.Model
{
    using Data.Database;
    using SqlKata;
    using System;

    class Imposto : Model
    {
        public Imposto() : base("IMPOSTO")
        {

        }

        #region CAMPOS 

        //campos obrigatorios para todas as tabelas

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string EmpresaId { get; private set; }

        public string Nome { get; set; }

        public string Icms { get; set; } // CST CSOSN   
        public double IcmsReducaoAliq { get; set; }
        public double IcmsAliq { get; set; }
        public double IcmsStBase { get; set; }
        public double IcmsStReducaoAliq { get; set; }
        public double IcmsStAliq { get; set; }
        public string Ipi { get; set; } // CST
        public double IpiAliq { get; set; }
        public string Pis { get; set; } // CST
        public double PisAliq { get; set; }
        public string Cofins { get; set; } // CST
        public double CofinsAliq { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Emiplus.Model
{
    using Emiplus.Data;
    using global::Data.Database;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Validation;

    [Table("ITEM")]
    public class Item : UnitOfWork
    {
        #region CAMPOS 
        [Column("ID")]
        [Key()]
        public int Id { get; set; }

        [Required]
        [Column("TIPO")]
        public int Tipo { get; set; } //0-PRODUTO 1-SERVICO

        [Column("EXCLUIR")]
        public int Excluir { get; set; }

        [Column("DATAINSERIDO")]
        public DateTime DataInserido { get; private set; }

        [Column("DATAATUALIZADO")]
        [Required]
        public DateTime DataAtualizado { get; private set; }

        [Column("DATADELETADO")]
        public DateTime DataDeletado { get; private set; }

        [Column("EMPRESAID")]
        public int EmpresaId { get; private set; }

        [Required]
        [MaxLength(5, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("NOME")]
        public string Nome { get; set; }

        [Column("REFERENCIA")]
        public string Referencia { get; set; }

        [Column("VALORCOMPRA")]
        public double ValorCompra { get; set; }

        [Column("VALORVENDA")]
        public double ValorVenda { get; set; }

        [Column("ESTOQUEATUAL")]
        public double EstoqueAtual { get; private set; }

        #endregion

        #region SQL

        //CREATE TABLE ITEM
        //(
        //    id integer not null primary key,
        //    tipo integer not null,
        //    excluir integer not null,
        //    datainserido date,
        //    dataatualizado date,
        //    datadeletado date,
        //    empresaid integer,
        //    nome varchar(200),
        //    referencia varchar(50),
        //    valorcompra numeric(18,4),
        //    valorvenda numeric(18,4),
        //    estoqueatual numeric(18,4)
        //);

        //CREATE GENERATOR GEN_ITEM_ID;

        //SET TERM !! ;
        //        CREATE TRIGGER ITEM_BI FOR ITEM
        //        ACTIVE BEFORE INSERT POSITION 0
        //AS
        //DECLARE VARIABLE tmp DECIMAL(18,0);
        //        BEGIN
        //          IF(NEW.ID IS NULL) THEN
        //           NEW.ID = GEN_ID(GEN_ITEM_ID, 1);
        //        ELSE
        //        BEGIN
        //    tmp = GEN_ID(GEN_ITEM_ID, 0);
        //    if (tmp< new.ID) then
        //     tmp = GEN_ID(GEN_ITEM_ID, new.ID - tmp);
        //        END
        //      END!!
        //SET TERM; !!

        #endregion

        public Item bootstrap(Item data)
        {
            return data;
        }

        public bool Salvar()
        {
            using (var contexto = new ContextoData())
            {
                if (this.Id == 0)
                {
                    this.DataInserido = DateTime.Now;
                    contexto.Itens.Add(this);
                }
                else
                {
                    this.DataAtualizado = DateTime.Now;
                    contexto.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                if (contexto.SaveChanges() == 1)
                    return true;
            }

            return false;
        }

        public bool Deletar(Item data)
        {
            bool returnValue = false;

            data.Excluir = 1;
            data.DataDeletado = DateTime.Now;

            using (var contexto = new ContextoData())
            {
                contexto.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                if (contexto.SaveChanges() == 1)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        public Item GetItem(int id)
        {
            return GetItems(c => c.Id == id).FirstOrDefault();
        }

        public Item[] GetItems()
        {
            return GetItems(c => c.Id > 0);
        }

        public Item[] GetItems(Func<Item, bool> expressao)
        {
            List<Item> returnValue = null;

            using (var contexto = new ContextoData())
            {
                var q = contexto.Itens.Where(expressao);
                returnValue = new List<Item>();

                if (q.Count() > 0)
                {
                    returnValue.AddRange(q);
                }
            }

            return returnValue.ToArray();
        }
    }
}
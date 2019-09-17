﻿using Emiplus.Data.Helpers;
using SqlKata;
using System;

namespace Emiplus.Model
{
    using Data.Database;
    using Valit;

    internal class ItemEstoqueMovimentacao : Model
    {
        public ItemEstoqueMovimentacao() : base("ITEM_MOV_ESTOQUE") { }

        #region CAMPOS 

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public DateTime Criado { get; private set; }
        public string id_empresa { get; private set; }
        public int id_item { get; set; }
        public int id_usuario { get; set; }
        public double Quantidade { get; set; }
        public string observacao { get; set; }
        public string tipo { get; set; }
        public string local { get; set; }
        public double Anterior { get; set; }

        #endregion

        #region SQL CREATE
        //CREATE TABLE ITEM_MOV_ESTOQUE
        //(
        //id integer not null primary key,
        //id_item integer not null,
        //id_empresa varchar(255),
        //id_usuario integer,
        //excluir integer,
        //criado timestamp,
        //atualizado timestamp,
        //deletado timestamp,
        //quantidade numeric(18,4),
        //observacao varchar(255),
        //tipo varchar(10),
        //local varchar(255)
        //);
        #endregion

        #region SQL AUTOINCREMENT
        //        CREATE GENERATOR GEN_ITEM_MOV_ESTOQUE_ID;

        //SET TERM !! ;
        //        CREATE TRIGGER ITEM_MOV_ESTOQUE_BI FOR ITEM_MOV_ESTOQUE
        //        ACTIVE BEFORE INSERT POSITION 0
        //AS
        //DECLARE VARIABLE tmp DECIMAL(18,0);
        //        BEGIN
        //          IF(NEW.ID IS NULL) THEN
        //           NEW.ID = GEN_ID(GEN_ITEM_MOV_ESTOQUE_ID, 1);
        //        ELSE
        //        BEGIN
        //    tmp = GEN_ID(GEN_ITEM_MOV_ESTOQUE_ID, 0);
        //    if (tmp< new.ID) then
        //     tmp = GEN_ID(GEN_ITEM_MOV_ESTOQUE_ID, new.ID - tmp);
        //        END
        //      END!!
        //SET TERM; !!

        #endregion

       
        public ItemEstoqueMovimentacao SetQuantidade(double Quantidade)
        {
            this.Quantidade = Quantidade;
            return this;
        }

        public ItemEstoqueMovimentacao SetTipo(string tipo)
        {
            this.tipo = tipo;
            return this;
        }

        public ItemEstoqueMovimentacao SetLocal(string local)
        {
            this.local = local;
            return this;
        }
        public ItemEstoqueMovimentacao SetObs(string observacao)
        {
            this.observacao = observacao;
            return this;
        }

        public ItemEstoqueMovimentacao SetUsuario(int id_usuario)
        {
            this.id_usuario = id_usuario;
            return this;
        }

        public ItemEstoqueMovimentacao SetItem(Item item)
        {
            double EstoqueAtual = item.EstoqueAtual;

            id_item = item.Id;
            Anterior = item.EstoqueAtual;
            
            if (tipo == "A")
            {
                Console.WriteLine("asas");
                item.EstoqueAtual = EstoqueAtual + Quantidade;
            }

            if (tipo == "R")
            {
                item.EstoqueAtual = EstoqueAtual - Quantidade;
            }

            item.Save(item);
            return this;
        }

        public bool Save(ItemEstoqueMovimentacao data)
        {
            if (ValidarDados(data))
                return false;

            data.Criado = DateTime.Now;
            if (Data(data).Create() == 1)
            {
                Alert.Message("Tudo certo!", "Estoque adicionado com sucesso.", Alert.AlertType.success);
            }
            else
            {
                Alert.Message("Opss", "Erro ao adicionar, verifique os dados.", Alert.AlertType.error);
                return false;
            }

            return true;
        }

        public bool ValidarDados(ItemEstoqueMovimentacao data)
        {
            var result = ValitRules<ItemEstoqueMovimentacao>
                .Create()
                .Ensure(m => m.Quantidade, _ => _
                    .IsNumber()
                    .WithMessage("Coloque apenas números em 'Quantidade'."))
                .For(data)
                .Validate();

            if (!result.Succeeded)
            {
                foreach (var message in result.ErrorMessages)
                {
                    Alert.Message("Opss!", message, Alert.AlertType.error);
                    return true;
                }
                return true;
            }

            return false;
        }
    }
}
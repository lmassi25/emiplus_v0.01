﻿using System;
using Emiplus.Data.Helpers;
using SqlKata;
using Valit;

namespace Emiplus.Model
{
    internal class ItemEstoqueMovimentacao : Data.Database.Model
    {
        public ItemEstoqueMovimentacao() : base("ITEM_MOV_ESTOQUE")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public DateTime Criado { get; private set; }
        public string id_empresa { get; private set; }
        public int id_item { get; set; }
        public int id_usuario { get; set; }
        public double Quantidade { get; set; }
        public string observacao { get; set; }
        public string tipo { get; set; }
        public string local { get; set; }
        public double Anterior { get; set; }
        public int Id_Pedido { get; set; }

        public int id_sync { get; set; }
        public string status_sync { get; set; }

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

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

        public ItemEstoqueMovimentacao SetIdPedido(int id_pedido)
        {
            Id_Pedido = id_pedido;
            return this;
        }

        public ItemEstoqueMovimentacao SetItem(Item item)
        {
            var EstoqueAtual = item.EstoqueAtual;

            id_item = item.Id;
            Anterior = item.EstoqueAtual;

            switch (tipo)
            {
                case "A":
                    item.EstoqueAtual = EstoqueAtual + Quantidade;
                    break;
                case "R":
                    item.EstoqueAtual = EstoqueAtual - Quantidade;
                    break;
            }

            item.Save(item, false);
            return this;
        }

        public bool Save(ItemEstoqueMovimentacao data, bool message = true)
        {
            if (ValidarDados(data))
                return false;

            data.id_empresa = Program.UNIQUE_ID_EMPRESA;
            data.id_sync = Validation.RandomSecurity();
            data.status_sync = "CREATE";
            data.Criado = DateTime.Now;
            if (Data(data).Create() == 1)
                return true;

            if (message)
                Alert.Message("Opss", "Erro ao adicionar estoque, verifique os dados.", Alert.AlertType.error);
            
            return false;
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
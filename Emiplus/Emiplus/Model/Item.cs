using Emiplus.Data.Helpers;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emiplus.Model
{
    using Data.Database;
    using Valit;

    internal class Item : Model
    {
        public Item() : base("ITEM")
        {
        }

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime DataInserido { get; private set; }
        public DateTime DataAtualizado { get; private set; }
        public DateTime DataDeletado { get; private set; }
        public int EmpresaId { get; private set; }
        public string Nome { get; set; }
        public string Referencia { get; set; }
        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public double EstoqueAtual { get; private set; }
        public int Categoriaid { get; set; }
        

        #region SQL CREATE

        //CREATE TABLE categoria
        //(
        //id integer not null primary key,
        //tipo integer not null,
        //excluir integer not null,
        //datainserido date,
        //dataatualizado date,
        //datadeletado date,
        //empresaid integer,
        //nome varchar(200)
        //);

        #endregion SQL CREATE

        public IEnumerable<dynamic> findByEmail(string email)
        {
            return Query().Where("EMAIL", email).First();
        }

        public bool Save(Item data)
        {
            if (ValidarDados(data))
                return false;

            if (data.Id == 0)
            {
                data.DataInserido = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    Alert.Message("Tudo certo!", "Produto salvo com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }
            else
            {
                data.DataAtualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    Alert.Message("Tudo certo!", "Produto atualizado com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }

            return true;
        }

        public bool Remove(int id)
        {
            var data = new { Excluir = 1, DataDeletado = DateTime.Now };
            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover o produto.", Alert.AlertType.error);
            return false;
        }

        /// <summary>
        /// <para>Valida os campos do Model</para>
        /// <para>Documentação: <see cref="https://valitdocs.readthedocs.io/en/latest/validation-rules/index.html"/> </para>
        /// </summary>
        /// <param name="data">Objeto com valor dos atributos do Model Item</param>
        /// <returns>Retorna booleano e Mensagem</returns>
        public bool ValidarDados(Item data)
        {
            var result = ValitRules<Item>
                .Create()
                .Ensure(m => m.Nome, _ => _
                    .Required()
                    .WithMessage("Nome é obrigatorio.")
                    .MinLength(2)
                    .WithMessage("N é possivel q seu nome seja menor q 2 caracateres"))
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
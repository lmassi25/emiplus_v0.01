﻿namespace Emiplus.Model
{
    using Data.Database;
    using Data.Helpers;
    using SqlKata;
    using System;
    using System.Reflection;
    using Valit;

    internal class Categoria : Model
    {
        public Categoria() : base("CATEGORIA")
        {
        }

        #region CAMPOS 

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

        #endregion 

        public bool Save(Categoria data)
        {
            //Nome = Validation.CleanString(Nome);

            if (ValidarDados(data))
                return false;

            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    Alert.Message("Tudo certo!", "Categoria salvo com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }
            else
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    Alert.Message("Tudo certo!", "Categoria atualizada com sucesso.", Alert.AlertType.success);
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
            var data = new { Excluir = 1, Deletado = DateTime.Now };
            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Categoria removida com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover a categoria.", Alert.AlertType.error);
            return false;
        }

        /// <summary>
        /// <para>Valida os campos do Model</para>
        /// <para>Documentação: <see cref="https://valitdocs.readthedocs.io/en/latest/validation-rules/index.html"/> </para>
        /// </summary>
        /// <param name="data">Objeto com valor dos atributos do Model Item</param>
        /// <returns>Retorna booleano e Mensagem</returns>
        public bool ValidarDados(Categoria data)
        {
            var result = ValitRules<Categoria>
                .Create()
                .Ensure(m => m.Nome, _ => _
                    .Required()
                    .WithMessage("Preencha o título da categoria.")
                    .MinLength(2)
                    .WithMessage("Título da categoria é muito curto."))
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
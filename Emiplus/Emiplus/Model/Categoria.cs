﻿using System;
using System.Collections;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata;
using SqlKata.Execution;
using Valit;

namespace Emiplus.Model
{
    internal class Categoria : Data.Database.Model
    {
        public Categoria() : base("CATEGORIA")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public string Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }
        public string Nome { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

        /// <summary>
        /// Função usada para carregar as categorias nos combobox
        /// </summary>
        public ArrayList GetAll(string tipo)
        {
            var data = new ArrayList {new {Id = "0", Nome = "SELECIONE"}};

            var findDB = Query().Where("excluir", 0).Where("tipo", tipo).OrderByDesc("nome").Get();
            if (findDB != null)
                foreach (var item in findDB)
                    data.Add(new {Id = $"{item.ID}", Nome = $"{item.NOME}"});

            return data;
        }

        /// <summary>
        /// Função alimenta o colletion para autocomplete nos textbox
        /// </summary>
        public KeyedAutoCompleteStringCollection AutoComplete(string tipo = "")
        {
            var collection = new KeyedAutoCompleteStringCollection();

            var item = Query().Select("id", "nome", "tipo").Where("excluir", 0);

            if (!string.IsNullOrEmpty(tipo))
                item.Where("tipo", tipo);

            foreach (var itens in item.Get<Categoria>())
                if (!string.IsNullOrEmpty(itens.Nome))
                    collection.Add(itens.Nome, itens.Id);

            return collection;
        }

        public bool Save(Categoria data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (ValidarDados(data))
                return false;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Categoria salvo com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);

                return false;
            }

            if (data.Id > 0)
            {
                if (!data.IgnoringDefaults)
                {
                    data.status_sync = "UPDATE";
                    data.Atualizado = DateTime.Now;
                }

                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Categoria atualizada com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
                
                return false;
            }
            
            return false;
        }

        public bool Remove(int id)
        {
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Categoria removida com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover a categoria.", Alert.AlertType.error);
            return false;
        }

        /// <summary>
        ///     <para>Valida os campos do Model</para>
        ///     <para>Documentação: <see cref="https://valitdocs.readthedocs.io/en/latest/validation-rules/index.html" /> </para>
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
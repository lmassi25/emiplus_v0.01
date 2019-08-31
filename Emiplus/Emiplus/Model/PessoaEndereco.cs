namespace Emiplus.Model
{
    using Emiplus.Data.Helpers;
    using SqlKata;
    using SqlKata.Execution;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Database;
    using Valit;

    class PessoaEndereco : Model
    {
        public PessoaEndereco() : base("PESSOA_ENDERECO") {}

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Id_pessoa { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string EmpresaId { get; private set; }
        public string Titulo { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string Nr { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Pais { get; set; }

        #region SQL CREATE
        //CREATE TABLE PESSOA_ENDERECO
        //(
        //id integer not null primary key,
        //id_pessoa integer not null,
        //excluir integer,
        //criado timestamp,
        //atualizado timestamp,
        //deletado timestamp,
        //empresaid varchar(255),
        //titulo varchar(255),
        //cep varchar(255),
        //estado varchar(255),
        //cidade varchar(255),
        //rua varchar(255),
        //nr varchar(255),
        //bairro varchar(255),
        //complemento varchar(255),
        //pais varchar(255)
        //);
        #endregion

        public bool Save(PessoaEndereco data)
        {
            if (ValidarDados(data))
                return false;

            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
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
                data.Atualizado = DateTime.Now;
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
            var data = new { Excluir = 1, Deletado = DateTime.Now };
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
        public bool ValidarDados(PessoaEndereco data)
        {
            var result = ValitRules<PessoaEndereco>
                .Create()
                .Ensure(m => m.Cep, _ => _
                    .Required()
                    .WithMessage("CEP é obrigatorio."))
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

namespace Emiplus.Model
{
    using Data.Database;
    using Emiplus.Data.Helpers;
    using SqlKata;
    using System;
    using Valit;

    class PessoaContato : Model
    {
        public PessoaContato() : base("PESSOA_CONTATO") { }

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Id_pessoa { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }
        public string Contato { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        #region SQL Create
        //CREATE TABLE PESSOA_CONTATO
        //(
        //id integer not null primary key,
        //id_pessoa integer not null,
        //excluir integer,
        //criado timestamp,
        //atualizado timestamp,
        //deletado timestamp,
        //empresaid varchar(255),
        //contato varchar(255),
        //telefone varchar(255),
        //celular varchar(255),
        //email varchar(255)
        //);
        #endregion

        public SqlKata.Query FindByIdUser(int id)
        {
            return Query().Where("id_pessoa", id);
        }

        public bool Save(PessoaContato data, bool message = true)
        {
            if (ValidarDados(data))
                return false;

            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Contato salvo com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    if (message)
                        Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);

                    return false;
                }
            }
            else
            {
                data.status_sync = "UPDATE";
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Contato atualizado com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    if (message)
                        Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);

                    return false;
                }
            }

            return true;
        }

        public bool Remove(int id)
        {
            var data = new { Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE" };
            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Contato removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover o contato.", Alert.AlertType.error);
            return false;
        }

        /// <summary>
        /// <para>Valida os campos do Model</para>
        /// <para>Documentação: <see cref="https://valitdocs.readthedocs.io/en/latest/validation-rules/index.html"/> </para>
        /// </summary>
        /// <param name="data">Objeto com valor dos atributos do Model Item</param>
        /// <returns>Retorna booleano e Mensagem</returns>
        public bool ValidarDados(PessoaContato data)
        {
            var result = ValitRules<PessoaContato>
                .Create()
                .Ensure(m => m.Contato, _ => _
                    .Required()
                    .WithMessage("Título do contato é obrigatorio.")
                    .MinLength(2)
                    .WithMessage("O Título do contato é muito pequeno."))
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

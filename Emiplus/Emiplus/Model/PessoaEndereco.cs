namespace Emiplus.Model
{
    using Data.Database;
    using Emiplus.Data.Helpers;
    using SqlKata;
    using System;
    using Valit;

    class PessoaEndereco : Model
    {
        public PessoaEndereco() : base("PESSOA_ENDERECO") { }

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Id_pessoa { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; } = Program.UNIQUE_ID_EMPRESA;
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string Nr { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Pais { get; set; }
        public string IBGE { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

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

        public SqlKata.Query FindByIdUser(int id)
        {
            return Query().Where("id_pessoa", id);
        }

        public PessoaEndereco GetAddr(string cep)
        {
            if (cep.Length != 8)
            {
                Alert.Message("Opss", "CEP inválido.", Alert.AlertType.error);
                return this;
            }

            var d = new CEP();
            d.SetCep(cep);

            if (d.ValidationCep())
            {
                Estado = d.GetRetornoCorreios().uf;
                Cidade = d.GetRetornoCorreios().cidade;
                Rua = d.GetRetornoCorreios().end;
                Bairro = d.GetRetornoCorreios().bairro;
                IBGE = d.GetIBGE();
            }
            else
            {
                Estado = "";
                Cidade = "";
                Rua = "";
                Bairro = "";
                IBGE = "";
            }

            return this;
        }

        public bool Save(PessoaEndereco data, bool message = true)
        {
            if (ValidarDados(data))
                return false;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Endereço salvo com sucesso.", Alert.AlertType.success);
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
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Endereço atualizado com sucesso.", Alert.AlertType.success);
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
            var data = new { Excluir = 1, Deletado = DateTime.Now };
            if (Data(data).Update("ID", id) == 1)
            {
                Alert.Message("Pronto!", "Endereço removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover o endereço.", Alert.AlertType.error);
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
                    .WithMessage("CEP é obrigatorio.")
                    .MinLength(8)
                    .WithMessage("O CEP não tem um formato válido.")
                    .MaxLength(9)
                    .WithMessage("O CEP não tem um formato válido."))
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

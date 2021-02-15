using System;
using System.Collections;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata;
using SqlKata.Execution;
using Valit;

namespace Emiplus.Model
{
    internal class Pessoa : Data.Database.Model
    {
        public Pessoa() : base("PESSOA")
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
        public string Fantasia { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Aniversario { get; set; }
        public string Pessoatipo { get; set; }
        public int Isento { get; set; }
        public string Transporte_placa { get; set; }
        public string Transporte_uf { get; set; }
        public string Transporte_rntc { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }
        public int ativo { get; set; }

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

        public Pessoa FromCsv(string csvLine, string tipo = "Clientes")
        {
            var values = csvLine.Split(';');

            Id = 0;
            Tipo = tipo;
            Excluir = 0;
            Atualizado = DateTime.Now;
            Nome = values[0];

            var rnd = new Random();
            if (ExistsName(Nome))
            {
                Nome = Nome + " " + rnd.Next(1, 10);

                if (ExistsName(Nome))
                    Nome = Nome + " " + rnd.Next(1, 10);
            }

            Fantasia = values[1];
            RG = values[2];
            CPF = values[3];
            Aniversario = values[4] == "0000-00-00" ? null : values[4];
            Isento = Validation.ConvertToInt32(values[5]);
            Transporte_placa = values[6];
            Transporte_uf = values[7];
            Transporte_rntc = values[8];

            if (Save(this, false))
            {
                if (!string.IsNullOrEmpty(values[9]))
                {
                    var contato = new PessoaContato
                    {
                        Id_pessoa = GetLastId(),
                        Excluir = 0,
                        Contato = values[9],
                        Telefone = values[10],
                        Celular = values[11],
                        Email = values[12]
                    };
                    contato.Save(contato, false);
                }

                if (!string.IsNullOrEmpty(values[13]))
                {
                    var addr = new PessoaEndereco
                    {
                        Id_pessoa = GetLastId(),
                        Excluir = 0,
                        Cep = values[13],
                        Estado = values[14],
                        Cidade = values[15],
                        Rua = values[16],
                        Nr = values[17],
                        Complemento = values[18],
                        Bairro = values[19],
                        Pais = values[20],
                        IBGE = values[21]
                    };
                    addr.Save(addr, false);
                }
            }

            return this;
        }

        public bool ExistsName(string name, bool importacao = true, int idItem = 0)
        {
            var data = importacao
                ? Query().Where("nome", name).Where("excluir", 0).FirstOrDefault()
                : Query().Where("id", "!=", idItem).Where("nome", name).Where("excluir", 0).FirstOrDefault();

            return data != null;
        }

        public ArrayList GetAll(string tipo = "")
        {
            var data = new ArrayList {new {Id = "0", Nome = "SELECIONE"}};

            var findDB = Query()
                .Where("excluir", 0)
                .Where("nome", "!=", "Novo registro");
            if(!String.IsNullOrEmpty(tipo))
                findDB.Where("tipo", tipo);

            findDB.OrderByDesc("nome");
            if (findDB != null)
                foreach (var item in findDB.Get())
                    data.Add(new {Id = $"{item.ID}", Nome = $"{item.NOME}"});

            return data;
        }

        public bool Save(Pessoa data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (string.IsNullOrEmpty(data.Aniversario))
                data.Aniversario = null;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                    return true;

                if (message)
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
            }

            if (data.Id > 0)
            {
                if (ValidarDados(data))
                    return false;

                if (!data.IgnoringDefaults)
                {
                    data.status_sync = "UPDATE";
                    data.Atualizado = DateTime.Now;
                }

                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Atualizado com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
            }

            return false;
        }

        public bool Remove(int id, bool message = true)
        {
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
            if (Data(data).Update("ID", id) == 1)
            {
                if (message)
                    Alert.Message("Pronto!", "Removido com sucesso.", Alert.AlertType.info);

                return true;
            }

            if (message)
                Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);

            return false;
        }

        /// <summary>
        ///     <para>Valida os campos do Model</para>
        ///     <para>Documentação: <see cref="https://valitdocs.readthedocs.io/en/latest/validation-rules/index.html" /> </para>
        /// </summary>
        /// <param name="data">Objeto com valor dos atributos do Model Item</param>
        /// <returns>Retorna booleano e Mensagem</returns>
        public bool ValidarDados(Pessoa data)
        {
            if (IniFile.Read("UserNoDocument", "Comercial") == "True" && Home.pessoaPage == "Clientes")
                return false;

            var result = ValitRules<Pessoa>
                .Create()
                .Ensure(m => m.CPF, _ => _
                    .Required()
                    .WithMessage("CPF ou CNPJ é obrigatório.")
                    .MinLength(11)
                    .WithMessage("CPF ou CNPJ inválido."))
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
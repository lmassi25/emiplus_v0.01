using System;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using SqlKata;
using SqlKata.Execution;
using Valit;

namespace Emiplus.Model
{
    internal class Item : Data.Database.Model
    {
        public Item() : base("ITEM")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

        public string Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }
        public string Image { get; set; }
        public string Nome { get; set; }
        public string Referencia { get; set; }
        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public double EstoqueMinimo { get; set; }
        public double EstoqueAtual { get; set; }
        public int Categoriaid { get; set; }
        public string Medida { get; set; }
        public int Impostoid { get; set; }
        public int Impostoidcfe { get; set; }
        public string Cest { get; set; }
        public string Ncm { get; set; }
        public string Origem { get; set; }

        public double AliqFederal { get; set; }
        public double AliqEstadual { get; set; }
        public double AliqMunicipal { get; set; }

        public string InfAdicional { get; set; }
        public string CodeBarras { get; set; }
        public int Fornecedor { get; set; }
        public int Criado_por { get; set; }
        public int Atualizado_por { get; set; }
        public double Limite_Desconto { get; set; }

        public int id_sync { get; set; }
        public string status_sync { get; set; }

        public int ativo { get; set; }
        public string Atributos { get; set; }
        public string Adicional { get; set; }
        public string Combos { get; set; }

        public Item FromCsv(string csvLine, string tipo = "Produtos")
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

            Referencia = values[1];

            CodeBarras = string.IsNullOrEmpty(values[2]) ? Validation.CodeBarrasRandom() : values[2];

            if (ExistsCodeBarras(CodeBarras))
                CodeBarras = Validation.CodeBarrasRandom();

            ValorCompra = Validation.ConvertToDouble(values[3]);
            ValorVenda = Validation.ConvertToDouble(values[4]);
            EstoqueMinimo = Validation.ConvertToDouble(values[5]);
            EstoqueAtual = Validation.ConvertToDouble(values[6]);
            Medida = values[7] ?? "UN";
            Ncm = values[8];
            Cest = values[9];
            Origem = values[10];
            AliqFederal = Validation.ConvertToDouble(values[11]);
            AliqEstadual = Validation.ConvertToDouble(values[12]);
            AliqMunicipal = Validation.ConvertToDouble(values[13]);
            Limite_Desconto = Validation.ConvertToDouble(values[14]);

            Save(this, false);

            return this;
        }

        public KeyedAutoCompleteStringCollection AutoComplete(string tipo = "")
        {
            var collection = new KeyedAutoCompleteStringCollection();

            var item = Query().Select("id", "nome", "tipo").Where("excluir", 0);
            item.Where(q => q.Where("item.ativo", "0").OrWhereNull("item.ativo"));

            if (!string.IsNullOrEmpty(tipo))
                item.Where("tipo", tipo);

            foreach (var itens in item.Get())
                if (!string.IsNullOrEmpty(itens.NOME))
                    collection.Add(itens.NOME, itens.ID);

            return collection;
        }

        public bool ExistsCodeBarras(string codebarras, bool importacao = true, int idItem = 0)
        {
            var data = importacao
                ? Query().Where("codebarras", codebarras).Where("excluir", 0).FirstOrDefault()
                : Query().Where("id", "!=", idItem).Where("codebarras", codebarras).Where("excluir", 0).FirstOrDefault();

            return data != null;
        }

        public bool ExistsName(string name, bool importacao = true, int idItem = 0)
        {
            var data = importacao
                ? Query().Where("nome", name).Where("excluir", 0).FirstOrDefault()
                : Query().Where("id", "!=", idItem).Where("nome", name).Where("excluir", 0).FirstOrDefault();

            return data != null;
        }

        public bool Save(Item data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.Criado_por = Settings.Default.user_id;
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message) Alert.Message("Tudo certo!", "Produto salvo com sucesso.", Alert.AlertType.success);
                    return true;
                }

                Alert.Message("Opss", "Erro ao criar produto, verifique os dados.", Alert.AlertType.error);
                return false;
            }

            if (data.Id > 0)
            {
                if (ValidarDados(data))
                    return false;

                data.Atualizado_por = Settings.Default.user_id;
                data.status_sync = "UPDATE";
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Produto atualizado com sucesso.", Alert.AlertType.success);
                    return true;
                }

                Alert.Message("Opss", "Erro ao atualizar o produto, verifique os dados.", Alert.AlertType.error);
                return false;
            }

            return true;
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
                Alert.Message("Opss!", "Não foi possível remover o produto.", Alert.AlertType.error);

            return false;
        }

        /// <summary>
        ///     <para>Valida os campos do Model</para>
        ///     <para>Documentação: <see cref="https://valitdocs.readthedocs.io/en/latest/validation-rules/index.html" /> </para>
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
                    .WithMessage("Coloque um nome válido."))
                //.Ensure(m => m.Ncm, _ => _
                //.MaxLength(8)
                //.WithMessage("O NCM não pode ser MAIOR que 8 caracateres."))
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
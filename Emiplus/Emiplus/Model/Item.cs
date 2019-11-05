using Emiplus.Data.Helpers;
using SqlKata;
using System;

namespace Emiplus.Model
{
    using Data.Database;
    using Valit;

    internal class Item : Model
    {
        public Item() : base("ITEM") { }

        #region CAMPOS 

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; } = Program.UNIQUE_ID_EMPRESA;
        public string Nome { get; set; }
        public string Referencia { get; set; }
        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public double EstoqueMinimo { get; set; }
        public double EstoqueAtual { get; set; }
        public int Categoriaid { get; set; }
        public string Medida { get; set; }
        public int Impostoid { get; set; }
        public string Cest { get; set; }
        public string Ncm { get; set; }
        public string Origem { get; set; }

        public double AliqFederal { get; set; }
        public double AliqEstadual { get; set; }
        public double AliqMunicipal { get; set; }
        
        public string InfAdicional { get; set; }

        #endregion

        public bool Save(Item data, bool message = true)
        {
            if (ValidarDados(data))
                return false;

            if (data.Id == 0)
            {
                data.Tipo = "Produtos";
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
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    if (message) Alert.Message("Tudo certo!", "Produto atualizado com sucesso.", Alert.AlertType.success);
                    return true;
                }
                
                Alert.Message("Opss", "Erro ao atualizar o produto, verifique os dados.", Alert.AlertType.error);
                return false;
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
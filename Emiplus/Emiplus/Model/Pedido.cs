namespace Emiplus.Model
{
    using System;
    using SqlKata;
    using Valit;
    using Data.Database;
    using Data.Helpers;

    class Pedido : Model
    {
        public Pedido() : base("PEDIDO")
        {
        }

        #region CAMPOS 

        //campos obrigatorios para todas as tabelas

        [Ignore]
        [Key("ID")]
        public int Id { get; set; }
        public int Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string EmpresaId { get; private set; }

        // referencia com a tabela Pessoa

        public Pessoa Cliente { get; set; }
        public Pessoa Colaborador { get; set; }
               
        // totais 

        public double Total { get; private set; }
        public double Desconto { get; private set; }
        public double Frete { get; private set; }
        public double Produtos { get; private set; }
        public double ICMS { get; private set; }
        public double ICMSST { get; private set; }
        public double IPI { get; private set; }
        public double ICMSBASE { get; private set; }
        public double ICMSSTBASE { get; private set; }
        public double PIS { get; private set; }
        public double COFINS { get; private set; }

        #endregion

        public bool Save(Pedido data)
        {
            if (ValidarDados(data))
            {
                return false;
            }

            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    //Alert.Message("Tudo certo!", "Categoria salvo com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    //Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }
            else
            {
                data.Atualizado = DateTime.Now;
                if (Data(data).Update("ID", data.Id) == 1)
                {
                    //Alert.Message("Tudo certo!", "Categoria atualizada com sucesso.", Alert.AlertType.success);
                }
                else
                {
                    //Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
                    return false;
                }
            }

            return true;
        }

        public bool ValidarDados(Pedido data)
        {
            /*var result = ValitRules<Item>
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
            }*/

            return true;
        }
    }
}

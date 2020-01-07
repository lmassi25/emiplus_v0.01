namespace Emiplus.Model
{
    using Data.Database;
    using Data.Helpers;
    using Emiplus.Properties;
    using SqlKata;
    using System;
    using Valit;

    class Titulo : Model
    {
        public Titulo() : base("TITULO") {}

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
        public string Emissao { get; set; }
        public int Id_Categoria { get; set; }
        public int Id_Caixa { get; set; }
        public int Id_FormaPgto { get; set; }
        public int Id_Pedido { get; set; }

        public int Id_Pessoa { get; set; }
        public string Vencimento { get; set; }
        public double Total { get; set; }
        public double Recebido { get; set; }
        public string Baixa_data { get; set; }
        public double Baixa_total { get; set; }
        public double Baixa_id_formapgto { get; set; }
        public int Id_Caixa_Mov { get; set; }
        public string Obs { get; set; }
        public int id_usuario { get; set; } = Settings.Default.user_id;
        public int id_sync { get; set; }
        public string status_sync { get; set; }
        #endregion 

        public Titulo SetTipo(string tipo)
        {
            Tipo = tipo;
            return this;
        }

        public Titulo SetPedido(int idPedido)
        {
            Id_Pedido = idPedido;
            return this;
        }

        public Titulo SetFormaPgto(int idFormaPgto)
        {
            Id_FormaPgto = idFormaPgto;
            return this;
        }

        public Titulo SetCaixa(int idCaixa)
        {
            Id_Caixa = idCaixa;
            return this;
        }

        public Titulo SetCategoria(int idCat)
        {
            Id_Categoria = idCat;
            return this;
        }

        public Titulo SetEmissao(string dataEmissao)
        {
            Emissao = dataEmissao;
            return this;
        }

        public SqlKata.Query FindByPedido(int id)
        {
            var data = Query().Where("id_pedido", id);
            return data;
        }

        public bool Save(Titulo data, bool message = true)
        {
            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.Emissao = Validation.DateNowToSql();
                data.Criado = DateTime.Now;

                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Salvo com sucesso.", Alert.AlertType.success);
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
                        Alert.Message("Tudo certo!", "Atualizado com sucesso.", Alert.AlertType.success);
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

        public bool RemoveIdCaixaMov(int id)
        {
            var data = new { Excluir = 1, Deletado = DateTime.Now };
            if (Data(data).Update("ID_CAIXA_MOV", id) == 1)
            {
                Alert.Message("Pronto!", "Removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);
            return false;
        }
        
        public bool Remove(int id, string column = "ID", bool message = true)
        {
            var data = new { Excluir = 1, Deletado = DateTime.Now };
            if (Data(data).Update(column, id) == 1)
            {
                if (message)
                    Alert.Message("Pronto!", "Removido com sucesso.", Alert.AlertType.info);

                return true;
            }

            if (message)
                Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);

            return false;
        }

        public bool ValidarDados(Titulo data)
        {
            var result = ValitRules<Titulo>
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

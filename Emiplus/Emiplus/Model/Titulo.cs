using System;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using SqlKata;

namespace Emiplus.Model
{
    internal class Titulo : Data.Database.Model
    {
        public Titulo() : base("TITULO") { }

        [Ignore] [Key("ID")]
        public int Id { get; set; }

        public string Tipo { get; set; }
        public int Excluir { get; set; }
        public DateTime Criado { get; private set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }

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
        public double Valor_Liquido { get; set; }
        public string Baixa_data { get; set; }
        public double Baixa_total { get; set; }
        public int Baixa_id_formapgto { get; set; }
        public int Id_Caixa_Mov { get; set; }
        public string Obs { get; set; }
        public int id_usuario { get; set; }
        public int ID_Recorrencia_Pai { get; set; }
        public int Tipo_Recorrencia { get; set; }
        public int Qtd_Recorrencia { get; set; }
        public int Nr_Recorrencia { get; set; }
        public string Taxas { get; set; }

        public int id_sync { get; set; }
        public string status_sync { get; set; }

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

        public Query FindByPedido(int id)
        {
            var data = Query().Where("id_pedido", id);
            return data;
        }

        public bool Save(Titulo data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;
            data.id_usuario = Settings.Default.user_id;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
                //data.Emissao = Validation.DateNowToSql();
                data.Criado = DateTime.Now;

                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Salvo com sucesso.", Alert.AlertType.success);

                    return true;
                }
                
                if (message)
                    Alert.Message("Opss", "Erro ao criar, verifique os dados.", Alert.AlertType.error);
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
                        Alert.Message("Tudo certo!", "Atualizado com sucesso.", Alert.AlertType.success);

                    return true;
                }
                
                if (message)
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
            }

            return false;
        }

        public bool RemoveIdCaixaMov(int id)
        {
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
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
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
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
    }
}
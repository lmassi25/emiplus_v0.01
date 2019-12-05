using Emiplus.Data.Helpers;
using SqlKata;
using System;

namespace Emiplus.Model
{
    class Nota : Data.Database.Model
    {
        public Nota() : base("NOTA") { }

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
        public int id_pedido { get; set; }
        public string nr_Nota { get; set; }
        public string Serie { get; set; }
        public string Status { get; set; }

        public string ChaveDeAcesso { get; set; }

        #endregion

        public SqlKata.Query FindByIdPedido(int id)
        {
            return Query().Where("id_pedido", id);
        }

        public SqlKata.Query FindByIdPedidoAndTipo(int id, string tipo)
        {
            return Query().Where("tipo", tipo).Where("id_pedido", id);
        }

        public bool Save(Nota data, bool message = true)
        {
            if (data.Id == 0)
            {
                data.Criado = DateTime.Now;
                if (Data(data).Create() == 1)
                {
                    if (message)
                        Alert.Message("Tudo certo!", "Dados da Nota salvo com sucesso.", Alert.AlertType.success);
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
                        Alert.Message("Tudo certo!", "Dados da Nota atualizado com sucesso.", Alert.AlertType.success);
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
                Alert.Message("Pronto!", "Dados da nota removido com sucesso.", Alert.AlertType.info);
                return true;
            }

            Alert.Message("Opss!", "Não foi possível remover.", Alert.AlertType.error);
            return false;
        }
    }
}

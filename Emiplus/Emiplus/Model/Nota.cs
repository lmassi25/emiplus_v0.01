using Emiplus.Data.Helpers;
using SqlKata;
using SqlKata.Execution;
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
        public DateTime Criado { get; set; }
        public DateTime Atualizado { get; private set; }
        public DateTime Deletado { get; private set; }
        public string id_empresa { get; private set; }
        public int id_pedido { get; set; }
        public string nr_Nota { get; set; }
        public string Serie { get; set; }
        public string Status { get; set; }
        public string ChaveDeAcesso { get; set; }
        public string correcao { get; set; }
        public string assinatura_qrcode { get; set; }
        public int id_sync { get; set; }
        public string status_sync { get; set; }

        #endregion

        public SqlKata.Query FindByIdPedido(int id)
        {
            return Query().Where("id_pedido", id).Where("excluir", 0);
        }

        public SqlKata.Query FindByIdPedidoAndTipoAndStatus(int id, string tipo, string status = "Pendente")
        {
            return Query().Where("status", status).Where("tipo", tipo).Where("id_pedido", id).Where("excluir", 0);
        }

        public SqlKata.Query FindByIdPedidoAndTipo(int id, string tipo)
        {
            return Query().Where("tipo", tipo).Where("id_pedido", id).Where("excluir", 0);
        }
        
        public SqlKata.Query FindByIdPedidoUltReg(int Pedido, string status = "", string tipo = "")
        {
            int id = 0;

            try
            {
                var res = new Model.Nota()
                    .Query()
                    .SelectRaw("MAX(ID) as TOTAL")
                    .Where("NOTA.ID_PEDIDO", Pedido);

                if (!String.IsNullOrEmpty(status))
                    res.Where("NOTA.STATUS", status);

                if (!String.IsNullOrEmpty(tipo))
                    res.Where("NOTA.TIPO", tipo);

                foreach (var item in res.Get())
                {
                    id = Validation.ConvertToInt32(item.TOTAL);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Query().Where("NOTA.ID", id);
        }

        public bool Save(Nota data, bool message = true)
        {
            data.id_empresa = Program.UNIQUE_ID_EMPRESA;

            if (data.Id == 0)
            {
                data.id_sync = Validation.RandomSecurity();
                data.status_sync = "CREATE";
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
                data.status_sync = "UPDATE";
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
            var data = new { Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE" };
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
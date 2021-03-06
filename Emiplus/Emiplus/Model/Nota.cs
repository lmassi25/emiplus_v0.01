﻿using System;
using Emiplus.Data.Helpers;
using SqlKata;
using SqlKata.Execution;

namespace Emiplus.Model
{
    internal class Nota : Data.Database.Model
    {
        public Nota() : base("NOTA")
        {
        }

        [Ignore] [Key("ID")] public int Id { get; set; }

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
        public string protocolodeuso { get; set; }
        public int id_nota { get; set; }
        public string nr_serie_sat { get; set; }

        /// <summary>
        /// Necessário para a sincronização de dados
        /// </summary>
        [Ignore]
        public bool IgnoringDefaults { get; set; }

        public Query FindByIdPedido(int id)
        {
            return Query().Where("id_pedido", id).Where("excluir", 0);
        }

        public Query FindByIdPedidoAndTipoAndStatus(int id, string tipo, string status = "Pendente")
        {
            return Query().Where("status", status).Where("tipo", tipo).Where("id_pedido", id).Where("excluir", 0);
        }

        public Query FindByIdPedidoAndTipo(int id, string tipo)
        {
            return Query().Where("tipo", tipo).Where("id_pedido", id).Where("excluir", 0);
        }

        public Query FindByIdPedidoUltReg(int Pedido, string status = "", string tipo = "")
        {
            var id = 0;

            var res = new Nota()
                .Query()
                .SelectRaw("MAX(ID) as TOTAL")
                .Where("NOTA.ID_PEDIDO", Pedido);

            if (!string.IsNullOrEmpty(status))
                res.Where("NOTA.STATUS", status);

            if (!string.IsNullOrEmpty(tipo))
                res.Where("NOTA.TIPO", tipo);

            foreach (var item in res.Get()) id = Validation.ConvertToInt32(item.TOTAL);

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
                        Alert.Message("Tudo certo!", "Dados da Nota atualizado com sucesso.", Alert.AlertType.success);

                    return true;
                }

                if (message)
                    Alert.Message("Opss", "Erro ao atualizar, verifique os dados.", Alert.AlertType.error);
            }

            return false;
        }

        public bool Remove(int id)
        {
            var data = new {Excluir = 1, Deletado = DateTime.Now, status_sync = "UPDATE"};
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
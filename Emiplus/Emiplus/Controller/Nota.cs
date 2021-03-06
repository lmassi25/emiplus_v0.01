﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    internal class Nota
    {
        public Task<IEnumerable<dynamic>> GetDataTable(int idPedido, int idNota = 0)
        {
            return new Model.Nota().Query()
                .Where("EXCLUIR", 0)
                //.Where("id", idNota)
                .Where("id_pedido", idPedido)
                .Where("tipo", "CCe")
                .OrderByDesc("criado")
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, int idPedido, int idNota = 0)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "N°";
            Table.Columns[1].Width = 80;

            Table.Columns[2].Name = "Criado";
            Table.Columns[2].Width = 130;

            Table.Columns[3].Name = "Correção";

            Table.Columns[4].Name = "Status";
            Table.Columns[4].Width = 130;

            Table.Rows.Clear();

            var dados = await GetDataTable(idPedido, idNota);

            for (var i = 0; i < dados.Count(); i++)
            {
                var item = dados.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.SERIE,
                    item.CRIADO,
                    item.CORRECAO,
                    item.STATUS
                );
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public string GetSeqCCe(int idPedido, int idNota = 0)
        {
            var query = new Model.Nota().Query();

            query.SelectRaw("MAX(serie) as serie")
                .Where("excluir", 0)
                //.Where("id", idNota)
                .Where("id_pedido", idPedido)
                .Where("tipo", "CCe");

            foreach (var item in query.Get())
            {
                return item.SERIE == null ? "1" : (string) (Validation.ConvertToInt32(item.SERIE) + 1).ToString();
            }

            return "1";
        }

        public Task<IEnumerable<dynamic>> GetDataTableDoc(int idPedido)
        {
            return new Model.Nota().Query()
                .Where("EXCLUIR", 0)
                .Where("id_pedido", idPedido)
                .Where("tipo", "Documento")
                .OrderByDesc("criado")
                .GetAsync<dynamic>();
        }

        public async Task SetTableDoc(DataGridView Table, int idPedido)
        {
            Table.ColumnCount = 2;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Chave de Acesso";

            Table.Rows.Clear();

            var dados = await GetDataTableDoc(idPedido);

            for (var i = 0; i < dados.Count(); i++)
            {
                var item = dados.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.CHAVEDEACESSO
                );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public IEnumerable<dynamic> GetDataTableInutilizar(string status, string dataInicial, string dataFinal)
        {
            var notas = new Model.Nota();

            var data = notas.Query()
                .Select("nota.id as id", "nota.criado as criado", "nota.nr_nota as inicio",
                    "nota.assinatura_qrcode as final", "nota.serie as serie", "nota.status as status")
                .Where("nota.excluir", 0)
                .Where("nota.tipo", "Inutiliza");
            //.Where("nota.criado", ">=", Validation.ConvertDateToSql(dataInicial, true))
            //.Where("nota.criado", "<=", Validation.ConvertDateToSql(dataFinal + " 23:59", true));

            if (!string.IsNullOrEmpty(status) && status != "Todos")
            {
                data.Where("nota.status", status == "Transmitidos" ? "Transmitindo..." : "Autorizada");
            }

            return data.Get();
        }

        public void GetDataTableInutilizar(DataGridView Table, string status, string dataInicial, string dataFinal)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "N° Inicial";
            Table.Columns[1].MinimumWidth = 120;

            Table.Columns[2].Name = "N° Final";
            Table.Columns[2].MinimumWidth = 120;

            Table.Columns[3].Name = "Série";
            Table.Columns[3].MinimumWidth = 120;

            Table.Columns[4].Name = "Criado em";
            Table.Columns[4].MinimumWidth = 120;

            Table.Columns[5].Name = "Status";
            Table.Columns[5].MinimumWidth = 150;
            Table.Columns[5].Visible = true;

            Table.Rows.Clear();

            foreach (var item in GetDataTableInutilizar(status, dataInicial, dataFinal))
                Table.Rows.Add(
                    item.ID,
                    item.INICIO,
                    item.FINAL,
                    item.SERIE,
                    item.CRIADO,
                    item.STATUS == "Autorizada" ? "Inutilização de número homologado" : item.STATUS
                );

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Nota
    {
        public Task<IEnumerable<dynamic>> GetDataTable(int idPedido)
        {
            return new Model.Nota().Query()
                .Where("EXCLUIR", 0)
                .Where("id_pedido", idPedido)
                .Where("tipo", "CCe")
                .OrderByDesc("criado")
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, int idPedido)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
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
            
            IEnumerable<dynamic> dados = await GetDataTable(idPedido);

            for (int i = 0; i < dados.Count(); i++)
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

        public string GetSeqCCe(int idPedido)
        {
            var query = new Model.Nota().Query();

            query
            .SelectRaw("MAX(serie) as serie")
            .Where("excluir", 0)
            .Where("id_pedido", idPedido)
            .Where("tipo", "CCe");

            if (query == null)
                return "1";

            foreach (var item in query.Get())
            {
                if (item.SERIE == null)
                    return "1";

                return (Validation.ConvertToInt32(item.SERIE) + 1).ToString();
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
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;
            
            Table.Columns[1].Name = "Chave de Acesso";

            Table.Rows.Clear();

            IEnumerable<dynamic> dados = await GetDataTable(idPedido);

            for (int i = 0; i < dados.Count(); i++)
            {
                var item = dados.ElementAt(i);

                Table.Rows.Add(
                     item.ID,
                     item.CHAVEDEACESSO
                 );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

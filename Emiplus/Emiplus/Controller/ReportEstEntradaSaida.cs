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
    /// <summary>
    /// Controller Relatório entrada e Saída
    /// </summary>
    class ReportEstEntradaSaida
    {
        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            return new Model.ItemEstoqueMovimentacao().Query()
                .LeftJoin("ITEM", "ITEM.id", "ITEM_MOV_ESTOQUE.ID_ITEM")
                .LeftJoin("ITEM", "ITEM.id", "ITEM_MOV_ESTOQUE.ID_ITEM")
                .Where("item.excluir", 0)
                .OrderByDesc("item.criado")
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "Produto";

            Table.Columns[1].Name = "Usuário";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Quantidade";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Ação";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Local";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Estoque Anterior";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.CATEGORIA,
                    item.REFERENCIA,
                    item.NOME,
                    FormatPrice(ConvertToDouble(item.VALORCOMPRA), false),
                    FormatPrice(ConvertToDouble(item.VALORVENDA), true),
                    FormatMedida(item.MEDIDA, ConvertToDouble(item.ESTOQUEATUAL))
                );
            }

            Table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

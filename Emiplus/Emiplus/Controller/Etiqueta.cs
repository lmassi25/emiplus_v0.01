using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Emiplus.Data.Helpers.Validation;

namespace Emiplus.Controller
{
    class Etiqueta
    {
        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            return new Model.Etiqueta().Query()
                .LeftJoin("item", "item.id", "etiqueta.id_item")
                .Where("item.excluir", 0)
                .OrderByDesc("etiqueta.criado")
                .Limit(100)
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Código";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Descrição";

            Table.Columns[3].Name = "Preço";
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Quantidade";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

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
                    item.REFERENCIA,
                    item.NOME,
                    FormatPrice(ConvertToDouble(item.VALORVENDA), true),
                    item.QUANTIDADE
                );
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

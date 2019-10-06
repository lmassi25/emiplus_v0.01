using SqlKata.Execution;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Imposto
    {
        public Task<IEnumerable<dynamic>> GetDataTable(string SearchText = null)
        {
            var search = "%" + SearchText + "%";

            return new Model.Imposto().Query()
                .Where("EXCLUIR", 0)
                .Where
                (
                    q => q.WhereLike("nome", search, false)
                )
                .OrderByDesc("criado")
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "")
        {
            Table.ColumnCount = 2;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable(SearchText);
                Data = dados;
            }

            foreach (var item in Data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.NOME
                );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

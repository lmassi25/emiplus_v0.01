using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    internal class Categoria
    {
        public Task<IEnumerable<dynamic>> GetDataTable(string searchText = null)
        {
            var search = "%" + searchText + "%";

            return new Model.Categoria().Query()
                .Where("EXCLUIR", 0)
                .Where
                (
                    q => q.WhereLike("nome", search)
                )
                .Where("Tipo", Home.CategoriaPage)
                .OrderByDesc("criado")
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "")
        {
            Table.ColumnCount = 2;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";

            Table.Rows.Clear();

            if (Data == null)
            {
                var dados = await GetDataTable(SearchText);
                Data = dados;
            }

            for (var i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.NOME
                );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
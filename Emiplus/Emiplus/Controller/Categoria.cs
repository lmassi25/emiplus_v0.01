using SqlKata.Execution;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emiplus.Controller
{
    class Categoria
    {
        public async Task<dynamic> GetDataTable(string SearchText = null)
        {
            var search = "%" + SearchText + "%";

            return new Model.Categoria().Query()
                .Where("EXCLUIR", 0)
                .Where
                (
                    q => q.WhereLike("nome", search, false)
                )
                .OrderByDesc("criado").GetAsync();
        }

        public void SetTable(DataGridView Table, IEnumerable<dynamic> lista = null, string SearchText = "", int page = 0)
        {
            Table.ColumnCount = 2;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Rows.Clear();

            //var search = "%" + SearchText + "%";
            //var lista = new Model.Categoria().Query()
            //    .Where("EXCLUIR", 0)
            //    .Where
            //    (
            //        q => q.WhereLike("nome", search, false)
            //    )
            //    .OrderByDesc("criado")
            //    .Get();

            for (int i = 0; i < lista.Count(); i++)
            {
                var data = lista.ElementAt(i);
                Table.Rows.Add(
                    data.ID,
                    data.NOME
                );
            }
        }
    }
}

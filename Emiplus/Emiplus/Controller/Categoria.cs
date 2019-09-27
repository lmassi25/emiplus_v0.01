using SqlKata.Execution;
using System.Windows.Forms;
using System.Linq;

namespace Emiplus.Controller
{
    class Categoria
    {
        public void GetDataTable(DataGridView Table, string SearchText)
        {
            Table.ColumnCount = 2;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Rows.Clear();

            var search = "%" + SearchText + "%";
            var lista = new Model.Categoria().Query()
                .Where("EXCLUIR", 0)
                .Where
                (
                    q => q.WhereLike("nome", search, false)
                )
                .OrderByDesc("criado")
                .Get();

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

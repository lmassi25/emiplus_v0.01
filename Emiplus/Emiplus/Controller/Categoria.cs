using SqlKata.Execution;
using System.Windows.Forms;

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

            var categorias = new Model.Categoria();

            var search = "%" + SearchText + "%";

            var lista = categorias.Query()
                .Where("EXCLUIR", 0)
                .Where
                (   
                    q => q.WhereLike("nome", search, false)
                )
                .OrderByDesc("criado")
                .Get();

            foreach (var data in lista)
            {
                Table.Rows.Add(
                    data.ID,
                    data.NOME
                );
            }
        }
    }
}

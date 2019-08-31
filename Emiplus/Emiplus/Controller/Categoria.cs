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
            var lista2 = categorias.Query()
                .Where("EXCLUIR", 0)
                .Where(q => q.Where("nome", "like", search))
                .OrderByDesc("criado")
                .Get();

            foreach (var data in lista2)
            {
                Table.Rows.Add(
                    data.ID,
                    data.NOME
                );
            }
        }
    }
}

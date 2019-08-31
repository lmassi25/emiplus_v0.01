namespace Emiplus.Controller
{
    using System.Windows.Forms;
    using SqlKata.Execution;

    class Pessoa : Data.Core.Controller
    {
        public void GetDataTableEnderecos(DataGridView Table)
        {
            Table.ColumnCount = 4;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Título";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "CEP";
            Table.Columns[2].Width = 150;

            Table.Columns[3].Name = "Rua";
            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Rows.Clear();

            var address = new Model.PessoaEndereco();

            var lista2 = address.Query()
                .Where("EXCLUIR", 0)
                .OrderByDesc("criado")
                .Get();

            foreach (var item in lista2)
            {
                Table.Rows.Add(
                    item.ID,
                    item.TITULO,
                    item.CEP,
                    $"Rua: {item.RUA} - {item.NR} - {item.COMPLEMENTO} - {item.BAIRRO} | {item.CIDADE}/{item.ESTADO} - {item.PAIS}"
                );
            }
        }
    }
}

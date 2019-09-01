namespace Emiplus.Controller
{
    using System.Windows.Forms;
    using SqlKata.Execution;

    class Pessoa : Data.Core.Controller
    {
        public void GetDataTableClientes(DataGridView Table, string SearchText)
        {
            Table.ColumnCount = 5;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome / Razão social";
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[2].Name = "Nome Fantasia";
            Table.Columns[2].Width = 150;

            Table.Columns[3].Name = "CPF / CNPJ";
            Table.Columns[3].Width = 150;

            Table.Columns[4].Name = "RG / IE";
            Table.Columns[4].Width = 150;

            Table.Rows.Clear();

            var address = new Model.Pessoa();

            var search = "%" + SearchText + "%";
            var data = address.Query()
                .Where("EXCLUIR", 0)
                .Where("TIPO", "Clientes")
                .Where(q => 
                    q.Where("nome", "like", search)
                        .OrWhere("fantasia", "like", search)
                        .OrWhere("rg", "like", search)
                        .OrWhere("cpf", "like", search))
                .OrderByDesc("criado")
                .Get();

            foreach (var item in data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.NOME,
                    item.FANTASIA,
                    item.CPF,
                    item.RG
                );
            }
        }

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

            var data = address.Query()
                .Where("EXCLUIR", 0)
                .OrderByDesc("criado")
                .Get();

            foreach (var item in data)
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

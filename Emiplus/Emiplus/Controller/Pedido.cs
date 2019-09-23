using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Pedido
    {
        /// <summary>
        /// Alimenta grid dos colaboradores e clientes
        /// </summary>
        /// <param name="Table">Grid para alimentar</param>
        /// <param name="SearchText">Input box</param>
        /// <param name="tipo">"Clientes" ou "Colaboradores"</param>
        public void GetDataTablePessoa(DataGridView Table, string SearchText, string tipo)
        {
            Table.ColumnCount = 5;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[2].Name = "CNPJ/CPF";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "RG";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Razão Social";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Rows.Clear();

            var clientes = new Model.Pessoa();

            var search = "%" + SearchText + "%";

            var data = clientes.Query()
                .Select("id", "nome", "rg", "cpf", "fantasia")
                .Where("excluir", 0)
                .Where("tipo", tipo)
                .Where
                (
                    q => q.WhereLike("nome", search)
                        .OrWhere("fantasia", search)
                        .OrWhere("rg", search)
                        .OrWhere("cpf", search)
                )
                .OrderByDesc("criado")
                .Limit(25)
                .Get();

            foreach (var cliente in data)
            {
                Table.Rows.Add(
                    cliente.ID,
                    cliente.NOME,
                    cliente.CPF,
                    cliente.RG,
                    cliente.FANTASIA
                );
            }
        }

        public void GetDataTablePedidos(DataGridView Table, string Search, string dataInicial, string dataFinal)
        {
            Table.Rows.Clear();

            var pedidos = new Model.Pedido();

            var search = "%" + Search + "%";
            var data = pedidos.Query()
                .LeftJoin("pessoa", "pessoa.id", "pedido.cliente")
                .Select("pedido.id", "pedido.criado", "pedido.total", "pessoa.nome", "pessoa.fantasia", "pessoa.rg", "pessoa.cpf")
                .Where("pedido.excluir", 0)
                .Where("pedido.criado", ">=", dataInicial)
                .Where("pedido.criado", "<=", dataFinal)
                .Where("pedido.excluir", 0)
                .Where
                (
                    q => q.WhereLike("pessoa.nome", search, false)
                )
                .OrderByDesc("pedido.criado")
                .Get();

            foreach (var item in data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.ID,
                    item.NOME,
                    Validation.ConvertDateToForm(item.CRIADO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true)
                );
            }
        }


    }
}

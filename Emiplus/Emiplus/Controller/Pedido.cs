using Emiplus.Data.Helpers;
using Emiplus.Model;
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

        public void GetDataTableItens(DataGridView Table, int id, Model.PedidoItem pedidoItem)
        {
            Table.ColumnCount = 7;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Código";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Nome do Produto";
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[3].Name = "Quantidade";
            Table.Columns[3].Width = 100;
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[4].Name = "Unitário";
            Table.Columns[4].Width = 100;
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[5].Name = "Desconto";
            Table.Columns[5].Width = 100;
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[6].Name = "Total";
            Table.Columns[6].Width = 100;
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var item = new Model.Item();

            var itens = item.Query()
                .Where("id", id)
                .Where("excluir", 0)
                .Where("tipo", "Produtos")
                .Limit(1)
                .Get();

            foreach (var data in itens)
            {
                Table.Rows.Add(
                    data.ID,
                    data.REFERENCIA,
                    data.NOME,
                    //pedidoItem.Medida,
                    pedidoItem.Quantidade + " " + pedidoItem.Medida,
                    Validation.FormatPrice(pedidoItem.ValorVenda, true),
                    Validation.FormatPrice(pedidoItem.Desconto, true),                    
                    Validation.FormatPrice(pedidoItem.Total, true)
                );
                
            }

        }

    }
}

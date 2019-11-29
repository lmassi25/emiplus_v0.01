using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class PedidoItem : Data.Core.Controller
    {
        public IEnumerable<dynamic> GetDataItens(int idPedido)
        {
            var itens = new Model.PedidoItem().Query()
                .LeftJoin("item", "item.id", "pedido_item.item")
                .Select("pedido_item.id", "pedido_item.quantidade", "pedido_item.medida", "pedido_item.valorvenda", "pedido_item.desconto", "pedido_item.total", "item.nome", "item.referencia")
                .Where("pedido_item.pedido", idPedido)
                .Where("pedido_item.excluir", 0)
                .Where("pedido_item.tipo", "Produtos");

            return itens.Get();
        }

        /// <summary>
        /// Alimenta o Datagrid da tela de Vendas e da tela Detalhes do Pedido.
        /// </summary>
        public void GetDataTableItens(DataGridView Table, int idPedido)
        {
            Table.ColumnCount = 8;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "#";
            Table.Columns[1].Width = 50;
            Table.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Table.Columns[2].Name = "Código";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Nome do Produto";
            Table.Columns[3].MinimumWidth = 150;

            Table.Columns[4].Name = "Quantidade";
            Table.Columns[4].Width = 100;
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[5].Name = "Unitário";
            Table.Columns[5].Width = 100;
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[6].Name = "Desconto";
            Table.Columns[6].Width = 100;
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[7].Name = "Total";
            Table.Columns[7].Width = 100;
            Table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Rows.Clear();

            if (idPedido <= 0)
                return;

            //var itens = new Model.PedidoItem().Query()
            //    .LeftJoin("item", "item.id", "pedido_item.item")
            //    .Select("pedido_item.id", "pedido_item.quantidade", "pedido_item.medida", "pedido_item.valorvenda", "pedido_item.desconto", "pedido_item.total", "item.nome", "item.referencia")
            //    .Where("pedido_item.pedido", idPedido)
            //    .Where("pedido_item.excluir", 0)
            //    .Where("pedido_item.tipo", "Produtos")
            //    .Get();

            var itens = GetDataItens(idPedido);

            int count = 1;
            foreach (var data in itens)
            {
                Table.Rows.Add(
                    data.ID,
                    count++,
                    data.REFERENCIA,
                    data.NOME,
                    data.QUANTIDADE + " " + data.MEDIDA,
                    Validation.FormatPrice(Validation.ConvertToDouble(data.VALORVENDA), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.DESCONTO), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.TOTAL), true)
                );
            }

            Table.Sort(Table.Columns[1], ListSortDirection.Descending);
            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

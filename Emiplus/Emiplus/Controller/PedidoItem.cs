using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    internal class PedidoItem : Data.Core.Controller
    {
        public static bool impostos { get; set; }

        public IEnumerable<dynamic> GetDataItens(int idPedido)
        {
            var itens = new Model.PedidoItem().Query()
                .LeftJoin("item", "item.id", "pedido_item.item")
                .Select("pedido_item.id", "pedido_item.quantidade", "pedido_item.xprod", "pedido_item.medida",
                    "pedido_item.valorvenda", "pedido_item.desconto", "pedido_item.frete", "pedido_item.ncm",
                    "pedido_item.cfop", "pedido_item.origem", "pedido_item.icms", "pedido_item.ipi", "pedido_item.pis",
                    "pedido_item.cofins", "pedido_item.federal", "pedido_item.estadual", "pedido_item.total",
                    "pedido_item.status", "item.nome", "item.referencia", "pedido_item.item")
                .Where("pedido_item.pedido", idPedido)
                .Where("pedido_item.excluir", 0);
            //.Where("pedido_item.tipo", "Produtos");

            return itens.Get();
        }

        /// <summary>
        ///     Alimenta o Datagrid da tela de Vendas e da tela Detalhes do Pedido.
        /// </summary>
        public void GetDataTableItens(DataGridView Table, int idPedido)
        {
            Table.ColumnCount = 20;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "#";
            Table.Columns[1].Width = 50;
            Table.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Table.Columns[2].Name = "Código";
            Table.Columns[2].Width = 100;
            Table.Columns[2].Visible = false;

            Table.Columns[3].Name = "Descrição";
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

            Table.Columns[7].Name = "Frete";
            Table.Columns[7].Width = 100;
            Table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[8].Name = "NCM";
            Table.Columns[8].Width = 100;
            Table.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[9].Name = "CFOP";
            Table.Columns[9].Width = 100;
            Table.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[10].Name = "Origem";
            Table.Columns[10].Width = 100;
            Table.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[11].Name = "ICMS";
            Table.Columns[11].Width = 100;
            Table.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[12].Name = "IPI";
            Table.Columns[12].Width = 100;
            Table.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[13].Name = "PIS";
            Table.Columns[13].Width = 100;
            Table.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[14].Name = "COFINS";
            Table.Columns[14].Width = 100;
            Table.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Table.Columns[15].Name = "Federal";
            Table.Columns[15].Width = 100;
            Table.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[16].Name = "Estadual";
            Table.Columns[16].Width = 100;
            Table.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[17].Name = "Status";
            Table.Columns[17].Visible = false;
            Table.Columns[17].Width = 100;
            Table.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Table.Columns[18].Name = "Total";
            Table.Columns[18].Width = 100;
            Table.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Table.Columns[19].Name = "Item";
            Table.Columns[19].Visible = false;

            Table.Columns[8].Visible = impostos;
            Table.Columns[9].Visible = impostos;
            Table.Columns[10].Visible = impostos;
            Table.Columns[11].Visible = impostos;
            Table.Columns[12].Visible = impostos;
            Table.Columns[13].Visible = impostos;
            Table.Columns[14].Visible = impostos;
            Table.Columns[15].Visible = impostos;
            Table.Columns[16].Visible = impostos;

            if (Home.pedidoPage == "Delivery" || Home.pedidoPage == "Balcao")
                Table.Columns[17].Visible = true;

            Table.Rows.Clear();

            if (idPedido <= 0)
                return;

            var itens = GetDataItens(idPedido);

            var count = 1;
            foreach (var data in itens)
            {
                Table.Rows.Add(
                    data.ID,
                    count++,
                    data.REFERENCIA,
                    data.XPROD,
                    Validation.FormatMedidas(data.MEDIDA, Validation.ConvertToDouble(data.QUANTIDADE)) + " " +
                    data.MEDIDA,
                    Validation.FormatPrice(Validation.ConvertToDouble(data.VALORVENDA), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.DESCONTO), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.FRETE), true),
                    string.IsNullOrEmpty(data.NCM) ? "0" : data.NCM,
                    string.IsNullOrEmpty(data.CFOP) ? "0" : data.CFOP,
                    string.IsNullOrEmpty(data.ORIGEM) ? "N/D" : data.ORIGEM,
                    string.IsNullOrEmpty(data.ICMS) ? "0" : data.ICMS,
                    string.IsNullOrEmpty(data.IPI) ? "0" : data.IPI,
                    string.IsNullOrEmpty(data.PIS) ? "0" : data.PIS,
                    string.IsNullOrEmpty(data.COFINS) ? "0" : data.COFINS,
                    Validation.FormatPrice(Validation.ConvertToDouble(data.FEDERAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(data.ESTADUAL), true),
                    data.STATUS,
                    Validation.FormatPrice(Validation.ConvertToDouble(data.TOTAL), true),
                    data.ITEM
                );

                Table.Rows[count - 2].Selected = true;
            }

            Table.Sort(Table.Columns[1], ListSortDirection.Descending);
            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void GetDataTableItensCompact(DataGridView Table, int idPedido)
        {
            Table.ColumnCount = 4;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "#";
            Table.Columns[1].Width = 45;
            Table.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Table.Columns[2].Name = "Código";
            Table.Columns[2].Width = 100;
            Table.Columns[2].Visible = false;

            Table.Columns[3].Name = "Descrição";
            Table.Columns[3].MinimumWidth = 150;
            var dataGridViewCellStyle1 = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5, 5, 0, 5),
                WrapMode = DataGridViewTriState.True
            };

            Table.Columns[3].DefaultCellStyle = dataGridViewCellStyle1;

            Table.Rows.Clear();

            if (idPedido <= 0)
                return;

            var itens = GetDataItens(idPedido);

            var count = 1;
            foreach (var data in itens)
            {
                Table.Rows.Add(
                    data.ID,
                    count++,
                    data.REFERENCIA,
                    $"{data.XPROD} {Environment.NewLine}" +
                    $"{Validation.FormatMedidas(data.MEDIDA, Validation.ConvertToDouble(data.QUANTIDADE))} {data.MEDIDA} x {Validation.FormatPrice(Validation.ConvertToDouble(data.VALORVENDA))} (-{Validation.FormatPrice(Validation.ConvertToDouble(data.DESCONTO))}) = {Validation.FormatPrice(Validation.ConvertToDouble(data.TOTAL))}"
                );

                Table.Rows[count - 2].Selected = true;
            }

            Table.Sort(Table.Columns[1], ListSortDirection.Descending);
            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
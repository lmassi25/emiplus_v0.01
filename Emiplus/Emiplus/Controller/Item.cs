using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    public class Item : Data.Core.Controller
    {
        public void GetDataTable(DataGridView Table, string SearchText)
        {
            Table.ColumnCount = 6;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Categoria";
            Table.Columns[1].Width = 150;

            Table.Columns[2].Name = "Cód. Personalizado";
            Table.Columns[2].Width = 200;

            Table.Columns[3].Name = "Descrição";
            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[4].Name = "Custo";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Venda";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;

            Table.Rows.Clear();

            var produtos = new Model.Item();
            var categoria = new Model.Categoria();

            var search = "%" + SearchText + "%";

            var lista = produtos.Query()
                .LeftJoin("categoria", "categoria.id", "item.categoriaid")
                .Select("item.*", "categoria.nome as categoria")
                .Where("item.excluir", 0)
                .Where("item.tipo", 0)
                .Where
                (
                    q => q.WhereLike("item.nome", search, false).OrWhere("item.referencia", "like", search).OrWhere("categoria.nome", "like", search)
                )
                .OrderByDesc("item.criado")
                .Get();

            foreach (var item in lista)
            {
                Table.Rows.Add(
                    item.ID,
                    item.CATEGORIA,
                    item.REFERENCIA,
                    item.NOME,
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORCOMPRA)),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALORVENDA))
                );
            }
        }

        public void GetDataTableEstoque(DataGridView Table, int id)
        {
            Table.ColumnCount = 7;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Tipo";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Quantidade";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Data/Hora";
            Table.Columns[3].Width = 120;

            Table.Columns[4].Name = "Usuário";
            Table.Columns[4].Width = 120;

            Table.Columns[5].Name = "Obs.";
            Table.Columns[5].Width = 120;

            Table.Columns[6].Name = "Tela";
            Table.Columns[6].Width = 120;

            Table.Rows.Clear();

            var produtos = new Model.ItemEstoqueMovimentacao();

            var lista = produtos.Query()
                .Where("id_item", id)
                .OrderByDesc("criado")
                .Get();

            foreach (var item in lista)
            {
                Table.Rows.Add(
                    item.ID,
                    item.TIPO == "A" ? "Adicionado" : "Removido",
                    item.QUANTIDADE,
                    String.Format("{0:d/M/yyyy HH:mm}", item.CRIADO),
                    0,
                    item.OBSERVACAO,
                    item.LOCAL
                );
            }
        }
    }
}
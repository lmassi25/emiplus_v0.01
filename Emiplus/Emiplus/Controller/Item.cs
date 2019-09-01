using System;
using System.Windows.Forms;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    public class Item : Data.Core.Controller
    {
        public void GetDataTable(DataGridView Table, string SearchText)
        {
            Table.ColumnCount = 6;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Código";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Categoria";
            Table.Columns[2].Width = 150;

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
            var lista2 = produtos.Query()
                .LeftJoin("categoria", "categoria.id", "item.categoriaid")
                .Select("item.*", "categoria.nome as nomecat")
                .Where("item.excluir", 0)
                .Where(q => q.Where("item.nome", "like", search).OrWhere("item.referencia", "like", search))
                .OrderByDesc("item.criado")
                .Get();

            foreach (var item in lista2)
            {
                Table.Rows.Add(
                    item.ID,
                    item.REFERENCIA,
                    item.NOMECAT,
                    item.NOME,
                    "0,00",
                    "0,00"
                );
            }
        }
    }
}
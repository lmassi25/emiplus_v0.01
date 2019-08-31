﻿using System.Windows.Forms;
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

            var search = "%" + SearchText + "%";
            var lista2 = produtos.Query()
                .Where("EXCLUIR", 0)
                .Where(q => q.Where("nome", "like", search).OrWhere("referencia", "like", search))
                .OrderByDesc("criado")
                .Get();

            foreach (var item in lista2)
            {
                Table.Rows.Add(
                    item.ID,
                    item.REFERENCIA,
                    "Categoria",
                    item.NOME,
                    "0,00",
                    "0,00"
                );
            }
        }
    }
}
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.Controller
{
    class Pedido
    {
        public void GetDataTableClients(DataGridView Table, string SearchText)
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
                .Where
                (
                    q => q.WhereLike("nome", search)
                        .OrWhere("fantasia", search)
                        .OrWhere("rg", search)
                        .OrWhere("cpf", search)
                )
                .OrderByDesc("criado")
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

        public void GetDataTableItens(DataGridView Table, int id)
        {
            Table.ColumnCount = 8;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Código";
            Table.Columns[1].Width = 50;

            Table.Columns[2].Name = "Nome do Produto";
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[3].Name = "Unidade";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Quantidade";
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Descontos";
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = "Unitários";
            Table.Columns[6].Width = 100;

            Table.Columns[7].Name = "Total";
            Table.Columns[7].Width = 100;

            var item = new Model.Item();

            var itens = item.Query()
                .Where("id", id)
                .Where("excluir", 0)
                .Where("tipo", 0)
                .Get();

            foreach (var data in itens)
            {
                Table.Rows.Add(
                    data.ID,
                    data.REFERENCIA,
                    data.NOME,
                    "",
                    "",
                    "",
                    "",
                    ""
                );
            }

        }

    }
}

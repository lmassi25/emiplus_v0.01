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

        public void GetDataTableItens(DataGridView Table, string SearchText)
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

    }
}

﻿namespace Emiplus.Controller
{
    using Emiplus.View.Common;
    using SqlKata.Execution;
    using System.Windows.Forms;

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
                .Where("TIPO", Home.pessoaPage)
                .Where("ID", "!=", 1)
                .Where(q =>
                    q.Where("nome", "like", search)
                        .OrWhere("fantasia", "like", search)
                        .OrWhere("rg", "like", search)
                        .OrWhere("cpf", "like", search))
                .OrderByDesc("criado")
                .Limit(50)
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

        public void GetDataTableEnderecos(DataGridView Table, int Id)
        {
            Table.ColumnCount = 9;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Título";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "CEP";
            Table.Columns[2].Width = 150;

            Table.Columns[3].Name = "Rua";
            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[4].Name = "N°";
            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[5].Name = "Bairro";
            Table.Columns[5].Width = 300;

            Table.Columns[6].Name = "Cidade";
            Table.Columns[6].Width = 200;

            Table.Columns[7].Name = "Estado";
            Table.Columns[7].Width = 100;

            Table.Columns[8].Name = "País";
            Table.Columns[8].Width = 100;

            Table.Rows.Clear();

            var address = new Model.PessoaEndereco();

            var data = address.Query()
                .Where("EXCLUIR", 0)
                .Where("ID_PESSOA", Id)
                .OrderByDesc("criado")
                .Get();

            foreach (var item in data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.TITULO,
                    item.CEP,
                    item.RUA,
                    item.NR,
                    item.BAIRRO,
                    item.CIDADE,
                    item.ESTADO,
                    item.PAIS
                );
            }
        }

        public void GetDataTableContato(DataGridView Table, int Id)
        {
            Table.ColumnCount = 5;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Contato";
            Table.Columns[1].Width = 150;

            Table.Columns[2].Name = "Telefone";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Celular";
            Table.Columns[3].Width = 150;

            Table.Columns[4].Name = "E-mail";
            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Rows.Clear();

            var address = new Model.PessoaContato();

            var data = address.Query()
                .Where("EXCLUIR", 0)
                .Where("ID_PESSOA", Id)
                .OrderByDesc("criado")
                .Get();

            foreach (var item in data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.CONTATO,
                    item.TELEFONE,
                    item.CELULAR,
                    item.EMAIL
                );
            }
        }

    }
}

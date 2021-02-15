using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    internal class Pessoa : Data.Core.Controller
    {
        public Task<IEnumerable<dynamic>> GetDataTableClientes(string SearchText = null, int nrRegistros = 50,
            bool TodosRegistros = false, string Ordem = "Ascendente", bool Inativos = true)
        {
            var search = "%" + SearchText + "%";

            var query = new Model.Pessoa().Query()
                .Where("EXCLUIR", 0)
                .Where("TIPO", Home.pessoaPage)
                .Where("ID", "!=", 1)
                .Where(q =>
                    q.Where("nome", "like", search)
                        .OrWhere("fantasia", "like", search)
                        .OrWhere("rg", "like", search)
                        .OrWhere("cpf", "like", search));

            switch (Ordem)
            {
                case "Z-A":
                    query.OrderByDesc("nome");
                    break;
                case "A-Z":
                    query.OrderByRaw("nome ASC");
                    break;
                case "Aleatório":
                    query.OrderByRaw("RAND()");
                    break;
            }

            if (!TodosRegistros)
                query.Limit(nrRegistros);

            if (!Inativos)
                query.Where
                (
                    q => q.Where("ativo", "<>", "1").OrWhereNull("ativo")
                );

            return query.GetAsync<dynamic>();
        }

        public async Task SetTableClientes(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "")
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome / Razão social";

            Table.Columns[2].Name = "Nome Fantasia";
            Table.Columns[2].Width = 150;

            Table.Columns[3].Name = "CPF / CNPJ";
            Table.Columns[3].Width = 150;

            Table.Columns[4].Name = "RG / IE";
            Table.Columns[4].Width = 150;

            Table.Rows.Clear();

            if (Data == null)
            {
                var dados = await GetDataTableClientes(SearchText);
                Data = dados;
            }

            foreach (var item in Data)
                Table.Rows.Add(
                    item.ID,
                    item.NOME,
                    item.FANTASIA,
                    item.CPF,
                    item.RG
                );

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void GetDataTableEnderecos(DataGridView Table, int Id)
        {
            Table.ColumnCount = 8;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "CEP";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Rua";
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns[2].MinimumWidth = 150;

            Table.Columns[3].Name = "N°";
            Table.Columns[2].Width = 70;

            Table.Columns[4].Name = "Bairro";
            Table.Columns[4].Width = 150;

            Table.Columns[5].Name = "Cidade";
            Table.Columns[5].Width = 150;

            Table.Columns[6].Name = "Estado";
            Table.Columns[6].Width = 100;

            Table.Columns[7].Name = "País";
            Table.Columns[7].Width = 70;

            Table.Rows.Clear();

            var address = new PessoaEndereco();
            var data = address.Query()
                .Where("EXCLUIR", 0)
                .Where("ID_PESSOA", Id)
                .OrderByDesc("criado")
                .Get();

            foreach (var item in data)
                Table.Rows.Add(
                    item.ID,
                    item.CEP,
                    item.RUA,
                    item.NR,
                    item.BAIRRO,
                    item.CIDADE,
                    item.ESTADO,
                    item.PAIS
                );
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

            var address = new PessoaContato();

            var data = address.Query()
                .Where("EXCLUIR", 0)
                .Where("ID_PESSOA", Id)
                .OrderByDesc("criado")
                .Get();

            foreach (var item in data)
                Table.Rows.Add(
                    item.ID,
                    item.CONTATO,
                    item.TELEFONE,
                    item.CELULAR,
                    item.EMAIL
                );
        }

        public string GetEnderecos(int id)
        {
            var address = new Model.PessoaEndereco();
            var dataAdress = address.Query()
                .Where("EXCLUIR", 0)
                .Where("ID_PESSOA", id)
                .OrderByDesc("criado")
                .Get();

            string endereco = "", cidade = "";

            foreach (var itemAd in dataAdress)
            {
                endereco = itemAd.RUA;
                endereco += ", " + itemAd.NR;
                endereco += " - " + itemAd.BAIRRO;
                cidade = itemAd.CIDADE;
                cidade += " / " + itemAd.ESTADO;
            }

            return endereco + " - " + cidade; 
        }
    }
}
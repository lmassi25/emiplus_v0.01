using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Pedido
    {
        /// <summary>
        /// Alimenta grid dos clientes
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

        /// <summary>
        /// Alimenta grid dos colaboradores
        /// </summary>
        /// <param name="Table">Grid para alimentar</param>
        /// <param name="SearchText">Input box</param>
        /// <param name="tipo">"Clientes" ou "Colaboradores"</param>
        public void GetDataTableColaboradores(DataGridView Table, string SearchText, string tipo)
        {
            Table.ColumnCount = 2;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            Table.Rows.Clear();

            var clientes = new Model.Usuarios();

            var search = "%" + SearchText + "%";

            var data = clientes.Query()
                .Select("id_user", "nome")
                .Where("excluir", 0)
                .Where
                (
                    q => q.WhereLike("nome", search)
                )
                .OrderByDesc("criado")
                .Limit(25)
                .Get();

            foreach (var cliente in data)
            {
                Table.Rows.Add(
                    cliente.ID_USER,
                    cliente.NOME
                );
            }
        }

        public Task<IEnumerable<dynamic>> GetDataTablePedidos(string tipo, string dataInicial, string dataFinal, string SearchText = null, int excluir = 0)
        {
            var search = "%" + SearchText + "%";

            if(!string.IsNullOrEmpty(SearchText))
                return new Model.Pedido().Query()
                .LeftJoin("pessoa", "pessoa.id", "pedido.cliente")
                .Select("pedido.id", "pedido.criado", "pedido.total", "pessoa.nome", "pessoa.fantasia", "pessoa.rg", "pessoa.cpf")
                .Where("pedido.excluir", excluir)
                .Where("pedido.tipo", tipo)
                .Where("pedido.emissao", ">=", dataInicial)
                .Where("pedido.emissao", "<=", dataFinal)
                .Where("pedido.excluir", 0)
                .Where
                (
                    q => q.WhereLike("pessoa.nome", search, false)
                )
                .OrderByDesc("pedido.id")
                .GetAsync<dynamic>();

            return new Model.Pedido().Query()
                .LeftJoin("pessoa", "pessoa.id", "pedido.cliente")
                .Select("pedido.id", "pedido.criado", "pedido.total", "pessoa.nome", "pessoa.fantasia", "pessoa.rg", "pessoa.cpf")
                .Where("pedido.excluir", excluir)
                .Where("pedido.tipo", tipo)
                .Where("pedido.emissao", ">=", dataInicial)
                .Where("pedido.emissao", "<=", dataFinal)
                .Where("pedido.excluir", 0)
                .OrderByDesc("pedido.id")
                .GetAsync<dynamic>();
        }

        public async Task SetTablePedidos(DataGridView Table, string tipo, string dataInicial, string dataFinal, IEnumerable<dynamic> Data = null, string SearchText = null, int excluir = 0)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "N°";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Cliente";

            Table.Columns[3].Name = "Criado em";
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Total";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTablePedidos(tipo, dataInicial, dataFinal, SearchText, excluir);
                Data = dados;
            }

            foreach (var item in Data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.ID,
                    item.NOME,
                    Validation.ConvertDateToForm(item.CRIADO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true)
                );
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

    }
}

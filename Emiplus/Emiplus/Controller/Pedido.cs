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

        public Task<IEnumerable<dynamic>> GetDataTablePedidos(string tipo, string dataInicial, string dataFinal, string SearchText = null, int excluir = 0, int idPedido = 0, int status = 0, int usuario = 0)
        {
            var search = "%" + SearchText + "%";

            var query = new Model.Pedido().Query();

            query
                .LeftJoin("pessoa", "pessoa.id", "pedido.cliente")
                .LeftJoin("usuarios as colaborador", "colaborador.id_user", "pedido.colaborador")
                .LeftJoin("usuarios as usuario", "usuario.id_user", "pedido.id_usuario")
                .Select("pedido.id", "pedido.emissao", "pedido.total", "pessoa.nome", "colaborador.nome as colaborador", "usuario.nome as usuario", "pedido.criado", "pedido.excluir", "pedido.status")
                .Where("pedido.excluir", excluir)
                .Where("pedido.emissao", ">=", Validation.ConvertDateToSql(dataInicial))
                .Where("pedido.emissao", "<=", Validation.ConvertDateToSql(dataFinal));
            
            if (!tipo.Contains("Notas"))
                query.Where("pedido.tipo", tipo);

            if (tipo.Contains("Notas"))
            {
                query.LeftJoin("nota", "nota.id_pedido", "pedido.id");
                query.Select("nota.nr_nota", "nota.serie", "nota.status as statusnfe");
            }

            if (usuario != 0)
               query.Where("pedido.colaborador", usuario);

            if (status != 0)
                query.Where("pedido.status", status);

            if (idPedido != 0)
                query.Where("pedido.id", idPedido);

            if (!string.IsNullOrEmpty(SearchText))
                query.Where
                (
                    q => q.WhereLike("pessoa.nome", search, false)
                );

            query.OrderByDesc("pedido.id");

            return query.GetAsync<dynamic>();
        }

        public Task<IEnumerable<dynamic>> GetDataTableTotaisPedidos(string tipo, string dataInicial, string dataFinal, string SearchText = null, int excluir = 0)
        {
            var search = "%" + SearchText + "%";

            var query = new Model.Pedido().Query();

            query
            .LeftJoin("pessoa", "pessoa.id", "pedido.cliente")
            .LeftJoin("usuarios as colaborador", "colaborador.id_user", "pedido.colaborador")
            .LeftJoin("usuarios as usuario", "usuario.id_user", "pedido.id_usuario")

            .SelectRaw("SUM(pedido.total) as total, COUNT(pedido.id) as id")

            .Where("pedido.excluir", excluir)
            .Where("pedido.tipo", tipo)
            .Where("pedido.emissao", ">=", Validation.ConvertDateToSql(dataInicial))
            .Where("pedido.emissao", "<=", Validation.ConvertDateToSql(dataFinal));

            if (!string.IsNullOrEmpty(SearchText))
                query.Where
                (
                    q => q.WhereLike("pessoa.nome", search, false)
                );

            return query.GetAsync<dynamic>();
        }

        public async Task SetTablePedidos(DataGridView Table, string tipo, string dataInicial, string dataFinal, IEnumerable<dynamic> Data = null, string SearchText = null, int excluir = 0, int idPedido = 0, int status = 0, int usuario = 0)
        {
            Table.ColumnCount = 9;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "N°";
            Table.Columns[1].Width = 50;

            Table.Columns[2].Name = "Emissão";
            Table.Columns[2].MinimumWidth = 80;

            Table.Columns[3].Name = "Cliente";
            Table.Columns[3].Width = 150;

            Table.Columns[4].Name = "Total";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 70;
            
            Table.Columns[5].Name = "Colaborador";
            Table.Columns[5].Width = 150;

            Table.Columns[6].Name = "Criado em";
            Table.Columns[6].Width = 120;

            Table.Columns[7].Name = "Status";
            Table.Columns[7].MinimumWidth = 150;

            Table.Columns[8].Name = "EXCLUIR";
            Table.Columns[8].Visible = false;

            Table.Rows.Clear();
            
            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTablePedidos(tipo, dataInicial, dataFinal, SearchText, excluir, idPedido, status, usuario);
                Data = dados;
            }

            foreach (var item in Data)
            {
                var statusNfePedido = "";
                if (tipo == "Notas")
                {
                    statusNfePedido = item.STATUSNFE == null ? "Pendente" : item.STATUSNFE;
                }
                else
                {
                    statusNfePedido = item.STATUS == 1 ? "Recebimento Pendente" : @"Finalizado\Recebido";
                }

                Table.Rows.Add(
                    item.ID,
                    item.ID,
                    Validation.ConvertDateToForm(item.EMISSAO),
                    item.NOME,
                    Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true),
                    item.COLABORADOR,
                    item.CRIADO,
                    statusNfePedido,
                    item.EXCLUIR
                );
            }


            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

    }
}

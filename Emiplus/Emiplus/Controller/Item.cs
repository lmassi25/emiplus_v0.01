using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Emiplus.Data.Helpers.Validation;

namespace Emiplus.Controller
{
    public class Item : Data.Core.Controller
    {
        public Task<IEnumerable<dynamic>> GetDataTable(string SearchText = null, bool TodosRegistros = false, int NrRegistros = 50, string Ordem = "Ascendente")
        {
            var search = "%" + SearchText + "%";

            var query = new Model.Item().Query();
               query.LeftJoin("categoria", "categoria.id", "item.categoriaid")
                .Select("item.id", "item.nome", "item.referencia", "item.codebarras", "item.valorcompra", "item.valorvenda", "item.estoqueatual", "item.medida", "categoria.nome as categoria")
                .Where("item.excluir", 0)
                .Where("item.tipo", "Produtos")
                .Where
                (
                    q => q.WhereLike("item.nome", search, true).OrWhere("item.referencia", "like", search).OrWhere("categoria.nome", "like", search)
                );

            if (Ordem == "Descendente")
                query.OrderByDesc("item.nome");
            if (Ordem == "Ascendente")
                query.OrderByRaw("item.nome ASC");
            if (Ordem == "Aleatório")
                query.OrderByRaw("RAND()");

            if (!TodosRegistros)
                query.Limit(NrRegistros);

            return query.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "", int page = 0)
        {
            Table.ColumnCount = 8;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Categoria";
            Table.Columns[1].Width = 150;
            if (page == 1) Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Cód. de Barras";
            Table.Columns[2].Width = 130;

            Table.Columns[3].Name = "Referência";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Descrição";
            Table.Columns[4].Width = 120;
            Table.Columns[4].MinimumWidth = 120;

            Table.Columns[5].Name = "Custo";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = "Venda";
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[6].Width = 100;

            Table.Columns[7].Name = "Estoque Atual";
            Table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[7].Width = 120;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable(SearchText);
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.CATEGORIA,
                    item.CODEBARRAS,
                    item.REFERENCIA,
                    item.NOME,
                    FormatPrice(ConvertToDouble(item.VALORCOMPRA), true),
                    FormatPrice(ConvertToDouble(item.VALORVENDA), true),
                    FormatMedidas(item.MEDIDA, ConvertToDouble(item.ESTOQUEATUAL))
                );
            }

            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        public Task<IEnumerable<dynamic>> GetDataTableServicos(string SearchText = null)
        {
            var search = "%" + SearchText + "%";

            return new Model.Item().Query()
                .Select("item.id", "item.nome", "item.referencia", "item.valorcompra", "item.valorvenda")
                .Where("item.excluir", 0)
                .Where("item.tipo", "Servicos")
                .Where
                (
                    q => q.WhereLike("item.nome", search, false).OrWhere("item.referencia", "like", search)
                )
                .OrderByDesc("item.criado")
                .Limit(50)
                .GetAsync<dynamic>();
        }

        public async Task SetTableServicos(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "", int page = 0)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Referência";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Descrição";
            Table.Columns[2].Width = 120;
            Table.Columns[2].MinimumWidth = 120;

            Table.Columns[3].Name = "Custo";
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Venda";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTableServicos(SearchText);
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.REFERENCIA,
                    item.NOME,
                    FormatPrice(ConvertToDouble(item.VALORCOMPRA), false),
                    FormatPrice(ConvertToDouble(item.VALORVENDA), true)
                );
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void GetDataTableEstoque(DataGridView Table, int id, int limit = 0)
        {
            Table.ColumnCount = 8;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Entrada/Saída";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Quantidade";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Data/Hora";
            Table.Columns[3].Width = 120;

            Table.Columns[4].Name = "Usuário";
            Table.Columns[4].Width = 120;

            Table.Columns[5].Name = "Obs.";
            Table.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Columns[6].Name = "Tela";
            Table.Columns[6].Width = 120;

            Table.Columns[7].Name = "Pedido";
            Table.Columns[7].Width = 50;

            Table.Rows.Clear();

            var lista = new Model.ItemEstoqueMovimentacao().Query()
                 .LeftJoin("USUARIOS", "USUARIOS.id_user", "ITEM_MOV_ESTOQUE.id_usuario")
                 .Select("ITEM_MOV_ESTOQUE.*", "USUARIOS.id_user", "USUARIOS.nome as nome_user")
                .Where("id_item", id)
                .OrderByDesc("criado");

            if (limit > 0)
            {
                lista.Limit(limit);
            }

            for (int i = 0; i < lista.Get().Count(); i++)
            {
                var item = lista.Get().ElementAt(i);
                Table.Rows.Add(
                    item.ID,
                    item.TIPO == "A" ? "Adicionado" : "Removido",
                    item.QUANTIDADE,
                    String.Format("{0:d/M/yyyy HH:mm}", item.CRIADO),
                    item.NOME_USER,
                    item.OBSERVACAO,
                    item.LOCAL,
                    item.ID_PEDIDO
                );
            }
        }
    }
}
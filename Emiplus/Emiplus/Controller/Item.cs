﻿using SqlKata.Execution;
using System;
using System.Windows.Forms;
using System.Linq;
using static Emiplus.Data.Helpers.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace Emiplus.Controller
{
    public class Item : Data.Core.Controller
    {
        private string FormatMedida(string medida, double estoque)
        {
            if (medida == "KG")
                return FormatNumberKilo(estoque);

            return FormatNumberUnidade(estoque);
        }

        public Task<IEnumerable<dynamic>> GetDataTable(string SearchText = null)
        {
            var search = "%" + SearchText + "%";

            return new Model.Item().Query()
                .LeftJoin("categoria", "categoria.id", "item.categoriaid")
                .Select("item.id", "item.nome", "item.referencia", "item.valorcompra", "item.valorvenda", "item.estoqueatual", "item.medida", "categoria.nome as categoria")
                .Where("item.excluir", 0)
                .Where("item.tipo", "Produtos")
                .Where
                (
                    q => q.WhereLike("item.nome", search, false).OrWhere("item.referencia", "like", search).OrWhere("categoria.nome", "like", search)
                )
                .OrderByDesc("item.criado")
                .Limit(50)
                .GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null, string SearchText = "", int page = 0)
        {
            Table.ColumnCount = 7;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Categoria";
            Table.Columns[1].Width = 100;
            if (page == 1) Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Código";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Descrição";

            Table.Columns[4].Name = "Custo";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;
            if (page == 1) Table.Columns[4].Visible = false;

            Table.Columns[5].Name = "Venda";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = "Estoque Atual";
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[6].Width = 120;

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
                    item.REFERENCIA,
                    item.NOME,
                    FormatPrice(ConvertToDouble(item.VALORCOMPRA), false),
                    FormatPrice(ConvertToDouble(item.VALORVENDA), true),
                    FormatMedida(item.MEDIDA, ConvertToDouble(item.ESTOQUEATUAL))
                );
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void GetDataTableEstoque(DataGridView Table, int id)
        {
            Table.ColumnCount = 7;

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

            Table.Rows.Clear();

            var lista = new Model.ItemEstoqueMovimentacao().Query()
                .Where("id_item", id)
                .OrderByDesc("criado")
                .Get();

            for (int i = 0; i < lista.Count(); i++)
            {
                var item = lista.ElementAt(i);
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
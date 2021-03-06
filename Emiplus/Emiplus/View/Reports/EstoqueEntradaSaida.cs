﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.Properties;
using SqlKata.Execution;

namespace Emiplus.View.Reports
{
    public partial class EstoqueEntradaSaida : Form
    {
        private readonly Item _mItem = new Item();

        // AutoComplete
        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public EstoqueEntradaSaida()
        {
            InitializeComponent();
            Eventos();
        }

        private async Task DataTableAsync()
        {
            await SetTable(GridLista);
        }

        /// <summary>
        ///     Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            collection = _mItem.AutoComplete("Produtos");
            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        /// <summary>
        ///     Autocomplete do campo de busca de usuários.
        /// </summary>
        private void AutoCompleteUsers()
        {
            Usuarios.DataSource = new Usuarios().GetAllUsers();
            Usuarios.DisplayMember = "Nome";
            Usuarios.ValueMember = "Id";
        }

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new ItemEstoqueMovimentacao().Query();

            if (!noFilterData.Checked)
                model.Where("ITEM_MOV_ESTOQUE.criado", ">=", Validation.ConvertDateToSql(dataInicial.Value, true))
                    .Where("ITEM_MOV_ESTOQUE.criado", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));

            if (Validation.ConvertToInt32(Locais.SelectedValue) >= 1)
            {
                var local = "";
                if (Validation.ConvertToInt32(Locais.SelectedValue) == 1)
                    local = "Cadastro de Produto";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 2)
                    local = "Vendas";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 3)
                    local = "Orçamentos";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 4)
                    local = "Consignações";

                if (Validation.ConvertToInt32(Locais.SelectedValue) == 5)
                    local = "Devoluções";

                model.Where("ITEM_MOV_ESTOQUE.local", local);
            }

            if (Validation.ConvertToInt32(Usuarios.SelectedValue) >= 1)
                model.Where("ITEM_MOV_ESTOQUE.id_usuario", Validation.ConvertToInt32(Usuarios.SelectedValue));

            if (!filterAll.Checked)
            {
                if (filterAdicionado.Checked) model.Where("ITEM_MOV_ESTOQUE.TIPO", "A");

                if (filterRemovido.Checked) model.Where("ITEM_MOV_ESTOQUE.TIPO", "R");
            }

            if (collection.Lookup(BuscarProduto.Text) > 0)
                model.Where("ITEM_MOV_ESTOQUE.id_item", collection.Lookup(BuscarProduto.Text));

            model.LeftJoin("ITEM", "ITEM.id", "ITEM_MOV_ESTOQUE.id_item");
            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "ITEM_MOV_ESTOQUE.id_usuario");
            model.Select("ITEM_MOV_ESTOQUE.*", "ITEM.*", "USUARIOS.id_user", "USUARIOS.nome as nome_user");
            model.Limit(200);
            model.OrderByDesc("ITEM_MOV_ESTOQUE.ID");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "Descrição";
            Table.Columns[0].Width = 150;

            Table.Columns[1].Name = "Estoque";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Local";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Usuário";
            Table.Columns[3].Width = 150;

            Table.Columns[4].Name = "Data";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[4].Width = 100;

            Table.Rows.Clear();

            if (Data == null)
            {
                var dados = await GetDataTable();
                Data = dados;
            }

            for (var i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);
                var tipo = item.TIPO == "R" ? "-" : "+";
                var total = item.TIPO == "R" ? item.ANTERIOR - item.QUANTIDADE : item.ANTERIOR + item.QUANTIDADE;

                Table.Rows.Add(
                    item.NOME,
                    $"{tipo}{item.QUANTIDADE} (atual: {total})",
                    item.LOCAL,
                    item.NOME_USER,
                    Validation.ConvertDateToForm(item.CRIADO, true)
                );
            }

            Table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Shown += async (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                BuscarProduto.Select();
                AutoCompleteItens();
                AutoCompleteUsers();

                dataInicial.Text = DateTime.Today.AddMonths(-1).ToString();
                dataFinal.Text = DateTime.Now.ToString();

                var origens = new ArrayList
                {
                    new {Id = "0", Nome = "Todos"},
                    new {Id = "1", Nome = "Cadastro de Produtos"},
                    new {Id = "2", Nome = "Vendas"},
                    new {Id = "3", Nome = "Orçamentos"},
                    new {Id = "4", Nome = "Consignações"},
                    new {Id = "5", Nome = "Devoluções"}
                };

                Locais.DataSource = origens;
                Locais.DisplayMember = "Nome";
                Locais.ValueMember = "Id";

                await DataTableAsync();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnSearch.Click += async (s, e) => { await DataTableAsync(); };

            imprimir.Click += async (s, e) => await RenderizarAsync();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }

        private async Task RenderizarAsync()
        {
            var dados = await GetDataTable();

            var data = new ArrayList();
            foreach (var item in dados)
                data.Add(new
                {
                    Nome = item.NOME,
                    Usuario = item.NOME_USER,
                    Quantidade = item.QUANTIDADE,
                    Tipo = item.TIPO == "R" ? "-" : "+",
                    Local = item.LOCAL,
                    EstoqueAnterior = item.ANTERIOR,
                    EstoqueTotal = item.TIPO == "R" ? item.ANTERIOR - item.QUANTIDADE : item.ANTERIOR + item.QUANTIDADE,
                    Referencia = item.REFERENCIA,
                    Criado = Validation.ConvertDateToForm(item.CRIADO, true)
                });

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\EstoqueEntradaSaida.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                noFilterData = noFilterData.Checked,
                dataInicial = dataInicial.Text,
                dataFinal = dataFinal.Text
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}
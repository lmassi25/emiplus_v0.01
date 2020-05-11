using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotLiquid;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Reports;

namespace Emiplus.View.Financeiro
{
    public partial class Titulos : Form
    {
        private readonly Titulo _cTitulo = new Titulo();

        public List<int> listTitulos = new List<int>();
        private int tipo;

        public Titulos()
        {
            InitializeComponent();
            Eventos();
        }

        private void FilterTypes()
        {
            tipo = data.Text == @"Emissão" ? 1 : 0;

            Titulo.status = status.Text != @"Todos" ? status.Text : "";
        }

        private void Filter()
        {
            FilterTypes();

            _cTitulo.GetDataTableTitulosGeradosFilter(GridLista, Home.financeiroPage, search.Text, tipo,
                dataInicial.Text, dataFinal.Text);
        }

        private void EditTitulo(bool create = false)
        {
            if (create)
            {
                EditarTitulo.IdTitulo = 0;
                OpenForm.Show<EditarTitulo>(this);
                return;
            }

            if (GridLista.SelectedRows.Count <= 0)
                return;

            EditarTitulo.IdTitulo = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
            OpenForm.Show<EditarTitulo>(this);
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 8;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Selecione",
                Name = "Selecione",
                FlatStyle = FlatStyle.Standard,
                CellTemplate = new DataGridViewCheckBoxCell(),
                Width = 60
            };
            Table.Columns.Insert(0, checkColumn);

            Table.Columns[1].Name = "ID";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Emissão";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = Home.financeiroPage == "Receber" ? "Receber de" : "Pagar para";
            Table.Columns[3].Width = 150;

            Table.Columns[4].Name = "Forma de Pagamento";
            Table.Columns[4].Width = 160;

            Table.Columns[5].Name = "Vencimento";
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = "Total";
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[6].Width = 100;

            Table.Columns[7].Name = Home.financeiroPage == "Receber" ? "Recebido" : "Pago";
            Table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[7].Width = 100;

            Table.Columns[8].Name = "Valor Bruto";
            Table.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[8].Width = 100;
        }

        private void SetContentTable(DataGridView Table)
        {
            FilterTypes();

            Table.Rows.Clear();

            foreach (var item in _cTitulo.GetDataTableTitulosGerados(Home.financeiroPage, search.Text, tipo,
                dataInicial.Text, dataFinal.Text))
                Table.Rows.Add(
                    false,
                    item.ID,
                    Validation.ConvertDateToForm(item.EMISSAO),
                    item.NOME,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.VENCIMENTO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.VALOR_BRUTO), true)
                );

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            search.Focus();

            Load += (s, e) =>
            {
                switch (Home.financeiroPage)
                {
                    case "Receber":
                        label1.Text = @"Recebimentos";
                        label6.Text = @"Recebimentos";
                        label2.Text = @"Confira aqui todas os títulos a Receber/Recebidos da sua empresa.";
                        status.DataSource = new List<string> {"Todos", "Pendentes", "Recebidos"};
                        break;
                    case "Pagar":
                        label1.Text = @"Pagamentos";
                        label6.Text = @"Pagamentos";
                        label2.Text = @"Confira aqui todas os títulos a Pagar/Pagos da sua empresa.";
                        status.DataSource = new List<string> {"Todos", "Pendentes", "Pagos"};
                        break;
                }

                data.DataSource = new List<string> {"Vencimento", "Emissão"};

                dataInicial.Text = Validation.DateNowToSql();
                dataFinal.Text = Validation.DateNowToSql();
            };

            Shown += (s, e) =>
            {
                SetHeadersTable(GridLista);
                SetContentTable(GridLista);
            };

            search.TextChanged += (s, e) => SetContentTable(GridLista);
            search.Enter += (s, e) => SetContentTable(GridLista);
            filtrar.Click += (s, e) => SetContentTable(GridLista);

            btnAdicionar.Click += (s, e) => EditTitulo(true);
            btnEditar.Click += (s, e) => EditTitulo();
            GridLista.DoubleClick += (s, e) => EditTitulo();
            btnExit.Click += (s, e) => Close();

            btnEditAll.Click += (s, e) =>
            {
                listTitulos.Clear();

                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        listTitulos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                EditAllTitulos.listTitulos = listTitulos;
                OpenForm.Show<EditAllTitulos>(this);

                btnMarcarCheckBox.Text = @"Marcar Todos";
                btnRemover.Visible = false;
                btnEditAll.Visible = false;
                btnEditar.Enabled = true;
                btnAdicionar.Enabled = true;
            };

            btnMarcarCheckBox.Click += (s, e) =>
            {
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                    {
                        item.Cells["Selecione"].Value = false;
                        btnMarcarCheckBox.Text = @"Marcar Todos";
                        btnRemover.Visible = false;
                        btnEditAll.Visible = false;
                        btnEditar.Enabled = true;
                        btnAdicionar.Enabled = true;
                    }
                    else
                    {
                        item.Cells["Selecione"].Value = true;
                        btnMarcarCheckBox.Text = @"Desmarcar Todos";
                        btnRemover.Visible = true;
                        btnEditAll.Visible = true;
                        btnEditar.Enabled = false;
                        btnAdicionar.Enabled = false;
                    }
            };

            btnRemover.Click += (s, e) =>
            {
                listTitulos.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        listTitulos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!",
                    $"Você está prestes a deletar os {label1.Text.ToLower()} selecionados, continuar?",
                    AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in listTitulos)
                        new Model.Titulo().Remove(item, "ID", false);

                    SetContentTable(GridLista);
                }

                btnMarcarCheckBox.Text = @"Marcar Todos";
                btnRemover.Visible = false;
                btnEditAll.Visible = false;
                btnEditar.Enabled = true;
                btnAdicionar.Enabled = true;
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool) GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditAll.Visible = true;
                        btnEditar.Enabled = false;
                        btnAdicionar.Enabled = false;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        var hideBtns = false;
                        var hideBtnsTop = true;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool) item.Cells["Selecione"].Value)
                            {
                                hideBtns = true;
                                hideBtnsTop = false;
                            }

                        btnRemover.Visible = hideBtns;
                        btnEditAll.Visible = hideBtns;
                        btnEditar.Enabled = hideBtnsTop;
                        btnAdicionar.Enabled = hideBtnsTop;
                    }
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
            imprimir.Click += async (s, e) => await RenderizarAsync();
        }

        private async Task RenderizarAsync()
        {
            FilterTypes();

            var dados = _cTitulo.GetDataTableTitulosGerados(Home.financeiroPage, search.Text, tipo, dataInicial.Text,
                dataFinal.Text);

            string formatipo, clientetipo;

            if (Home.financeiroPage == "Receber")
            {
                formatipo = "Forma Receber";
                clientetipo = "Receber de";
            }
            else
            {
                formatipo = "Forma Pagar";
                clientetipo = "Pagar para";
            }

            var data = new ArrayList();
            foreach (var item in dados)
                data.Add(new
                {
                    item.ID,
                    item.FORMAPGTO,
                    EMISSAO = Validation.ConvertDateToForm(item.EMISSAO),
                    VENCIMENTO = Validation.ConvertDateToForm(item.VENCIMENTO),
                    CLIENTE = item.NOME,
                    TOTAL = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL)),
                    BAIXA_DATA = item.BAIXA_DATA != null ? Validation.ConvertDateToForm(item.BAIXA_DATA) : "",
                    RECEBIDO = Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO))
                });

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Titulos.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                dataInicial = dataInicial.Text,
                dataFinal = dataFinal.Text,
                Titulo = label1.Text,
                Formatipo = formatipo,
                Clientetipo = clientetipo
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}
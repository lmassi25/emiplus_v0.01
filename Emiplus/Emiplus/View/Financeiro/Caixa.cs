using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class Caixa : Form
    {

        private Model.Caixa _modelCaixa = new Model.Caixa();
        private Controller.Caixa _controllerCaixa = new Controller.Caixa();

        public Caixa()
        {
            InitializeComponent();
            Eventos();
        }

        private void AutoCompleteUsers()
        {
            Usuarios.DataSource = (new Model.Usuarios()).GetAllUsers();
            Usuarios.DisplayMember = "Nome";
            Usuarios.ValueMember = "Id";
        }

        private async Task DataTableAsync() => await SetTable(GridLista);

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new Model.Caixa().Query();

            model.Where("CAIXA.criado", ">=", Validation.ConvertDateToSql(dataInicial.Value, true))
                .Where("CAIXA.criado", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));

            if (Validation.ConvertToInt32(Usuarios.SelectedValue) >= 1)
            {
                model.Where("CAIXA.usuario", Validation.ConvertToInt32(Usuarios.SelectedValue));
            }

            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "CAIXA.usuario");
            model.Select("CAIXA.*", "USUARIOS.id_user", "USUARIOS.nome as nome_user");
            model.Where("CAIXA.excluir", "0");
            model.OrderByRaw("CAIXA.tipo ASC");
            model.OrderByDesc("CAIXA.criado");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Width = 60;

            Table.Columns[1].Name = "Terminal";
            Table.Columns[1].Width = 60;

            Table.Columns[2].Name = "Aberto em";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Fechado em";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Aberto por";
            Table.Columns[4].Width = 130;

            Table.Columns[5].Name = "Status";
            Table.Columns[5].Width = 80;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.TERMINAL,
                    Validation.ConvertDateToForm(item.CRIADO, true),
                    Validation.ConvertDateToForm(item.FECHADO, true),
                    item.NOME_USER,
                    item.TIPO
                );
            }

            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ShowDetailsCaixa()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                DetailsCaixa.idCaixa = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<DetailsCaixa>(this);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    Support.UpDownDataGrid(true, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Enter:
                    ShowDetailsCaixa();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += async (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                AutoCompleteUsers();

                dataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00");
                dataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy 23:59");

                await DataTableAsync();
            };

            GridLista.CellFormatting += (s, e) =>
            {
                foreach (DataGridViewRow row in GridLista.Rows)
                {
                    if (row.Cells[5].Value.ToString().Contains("Fechado"))
                    {
                        row.Cells[5].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[5].Style.ForeColor = Color.White;
                        row.Cells[5].Style.BackColor = Color.FromArgb(139, 215, 146);
                    }
                    else
                    {
                        row.Cells[5].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[5].Style.ForeColor = Color.White;
                        row.Cells[5].Style.BackColor = Color.FromArgb(255, 89, 89);
                    }
                }
            };

            btnSearch.Click += async (s, e) =>
            {
                await DataTableAsync();
            };

            GridLista.DoubleClick += (s, e) => ShowDetailsCaixa();

            label4.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");

            btnImprimir.Click += async (s, e) => await RenderizarAsync();
        }

        private ArrayList GetDataCaixas()
        {
            ArrayList dados = new ArrayList();
            dados.Clear();

            double txtSaldoInicial = 0;
            double txtEntradas = 0;
            double txtSaidas = 0;
            double txtSaldoFinal = 0;

            double txtVendasTotal = 0;
            double txtVendasAcrescimos = 0;
            double txtVendasDescontos = 0;
            double txtVendasGeradas = 0;
            double txtVendasMedia = 0;

            double txtVendasCanceladasTotal = 0;
            double txtVendasCanceladas = 0;

            double txtTotalRecebimento = 0;
            double txtDinheiro = 0;
            double txtCheque = 0;
            double txtCarDeb = 0;
            double txtCarCred = 0;
            double txtCrediario = 0;
            double txtBoleto = 0;

            foreach (DataGridViewRow item in GridLista.Rows)
            {
                if (Validation.ConvertToInt32(item.Cells["ID"].Value) != 0) {
                    _modelCaixa = _modelCaixa.FindById(Validation.ConvertToInt32(item.Cells["ID"].Value)).FirstOrDefault<Model.Caixa>();

                    txtSaldoInicial += _modelCaixa.Saldo_Inicial;
                    txtEntradas += _controllerCaixa.SumEntradas(_modelCaixa.Id);
                    txtSaidas += _controllerCaixa.SumSaidas(_modelCaixa.Id);
                    txtSaldoFinal += _controllerCaixa.SumSaldoFinal(_modelCaixa.Id);

                    txtVendasTotal += _controllerCaixa.SumVendasTotal(_modelCaixa.Id);
                    txtVendasAcrescimos += _controllerCaixa.SumVendasAcrescimos(_modelCaixa.Id);
                    txtVendasDescontos += _controllerCaixa.SumVendasDescontos(_modelCaixa.Id);
                    txtVendasGeradas += _controllerCaixa.SumVendasGeradas(_modelCaixa.Id);
                    txtVendasMedia += _controllerCaixa.SumVendasMedia(_modelCaixa.Id);

                    txtVendasCanceladasTotal += _controllerCaixa.SumVendasCanceladasTotal(_modelCaixa.Id);
                    txtVendasCanceladas += _controllerCaixa.SumVendasCanceladasGeradas(_modelCaixa.Id);

                    txtTotalRecebimento = _controllerCaixa.SumPagamentoTodos(_modelCaixa.Id);
                    txtDinheiro = _controllerCaixa.SumPagamento(_modelCaixa.Id, 1);
                    txtCheque = _controllerCaixa.SumPagamento(_modelCaixa.Id, 2);
                    txtCarDeb = _controllerCaixa.SumPagamento(_modelCaixa.Id, 3);
                    txtCarCred = _controllerCaixa.SumPagamento(_modelCaixa.Id, 4);
                    txtCrediario = _controllerCaixa.SumPagamento(_modelCaixa.Id, 5);
                    txtBoleto = _controllerCaixa.SumPagamento(_modelCaixa.Id, 6);
                }
            }

            dados.Add(new
            {
                txtSaldoInicial,
                txtEntradas,
                txtSaidas,
                txtSaldoFinal,
                txtVendasTotal,
                txtVendasAcrescimos,
                txtVendasDescontos,
                txtVendasGeradas,
                txtVendasMedia,
                txtVendasCanceladasTotal,
                txtVendasCanceladas,
                txtTotalRecebimento,
                txtDinheiro,
                txtCheque,
                txtCarDeb,
                txtCarCred,
                txtCrediario,
                txtBoleto
            });

            return dados;
        }

        private async Task RenderizarAsync()
        {
            dynamic dados = GetDataCaixas().ToArray();

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\CupomCaixaConferencia.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                CNPJ = Settings.Default.empresa_cnpj,
                Address = $"{Settings.Default.empresa_rua} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                txtResponsavel = Usuarios.Text,

                txtAberto = "N/D",
                txtFechado = "N/D",
                nrTerminal = "N/D",

                txtVendasTotal = Validation.FormatPrice(dados[0].txtVendasTotal, true),
                txtVendasAcrescimos = Validation.FormatPrice(dados[0].txtVendasAcrescimos, true),
                txtVendasDescontos = Validation.FormatPrice(dados[0].txtVendasDescontos, true),
                txtVendasMedia = Validation.FormatPrice(dados[0].txtVendasMedia, true),
                txtVendasCanceladasTotal = Validation.FormatPrice(dados[0].txtVendasCanceladasTotal, true),
                txtVendasCanceladas = dados[0].txtVendasCanceladas,
                txtDinheiro = Validation.FormatPrice(dados[0].txtDinheiro, true),
                txtCheque = Validation.FormatPrice(dados[0].txtCheque, true),
                txtCarDeb = Validation.FormatPrice(dados[0].txtCarDeb, true),
                txtCarCred = Validation.FormatPrice(dados[0].txtCarCred, true),
                txtCrediario = Validation.FormatPrice(dados[0].txtCrediario, true),
                txtBoleto = Validation.FormatPrice(dados[0].txtBoleto, true),

                txtSaldoInicial = Validation.FormatPrice(dados[0].txtSaldoInicial, true),
                txtEntradas = Validation.FormatPrice(dados[0].txtEntradas, true),
                txtSaidas = Validation.FormatPrice(dados[0].txtSaidas, true),
                txtSaldoFinal = Validation.FormatPrice(dados[0].txtSaldoFinal, true)
            }));

            Browser.htmlRender = render;
            using (var f = new Browser())
                f.ShowDialog();
        }
    }
}
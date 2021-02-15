using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using Emiplus.View.Reports;
using SqlKata.Execution;
using Pedido = Emiplus.Model.Pedido;

namespace Emiplus.View.Financeiro
{
    public partial class DetailsCaixa : Form
    {
        private readonly Controller.Caixa _controllerCaixa = new Controller.Caixa();
        private Model.Caixa _modelCaixa = new Model.Caixa();
        private readonly Usuarios _modelUsuarios = new Usuarios();

        public DetailsCaixa()
        {
            InitializeComponent();
            Eventos();
        }

        public static int idCaixa { get; set; }

        private void LoadUsuario(int idUser)
        {
            var user = _modelUsuarios.FindByUserId(idUser).FirstOrDefault();
            if (user != null)
                colaborador.Text = user.NOME;
        }

        private void LoadData()
        {
            _modelCaixa = _modelCaixa.FindById(idCaixa).FirstOrDefault<Model.Caixa>();

            caixa.Text = _modelCaixa.Id.ToString(Program.cultura);
            nrCaixa.Text = _modelCaixa.Id.ToString(Program.cultura);
            terminal.Text = _modelCaixa.Terminal;
            aberto.Text = Validation.ConvertDateToForm(_modelCaixa.Criado, true);
            label7.Text = _modelCaixa.Tipo == "Aberto" ? "Caixa Aberto" : "Caixa Fechado";

            if (_modelCaixa.Tipo == "Fechado")
            {
                panel7.BackColor = Color.FromArgb(192, 0, 0);
                txtFechado.Text = Validation.ConvertDateToForm(_modelCaixa.Fechado, true);
                FecharCaixa.Visible = false;
                btnLancamentos.Visible = false;
                btnEditar.Visible = false;
            }

            LoadUsuario(_modelCaixa.Usuario);

            txtSaldoInicial.Text = Validation.FormatPrice(_modelCaixa.Saldo_Inicial, true);
            txtEntradas.Text = Validation.FormatPrice(_controllerCaixa.SumEntradas(idCaixa), true);
            txtSaidas.Text = Validation.FormatPrice(_controllerCaixa.SumSaidas(idCaixa), true);

            //txtSaldoFinal.Text = Validation.FormatPrice(_controllerCaixa.SumSaldoFinal(idCaixa), true);
            txtSaldoFinal.Text = Validation.FormatPrice(_controllerCaixa.SumVendasTotal(idCaixa) + ((_modelCaixa.Saldo_Inicial + _controllerCaixa.SumEntradas(idCaixa)) - _controllerCaixa.SumSaidas(idCaixa)), true);

            txtVendasTotal.Text = Validation.FormatPrice(_controllerCaixa.SumVendasTotal(idCaixa), true);
            txtVendasAcrescimos.Text = Validation.FormatPrice(_controllerCaixa.SumVendasAcrescimos(idCaixa), true);
            txtVendasDescontos.Text = Validation.FormatPrice(_controllerCaixa.SumVendasDescontos(idCaixa), true);
            txtVendasGeradas.Text = _controllerCaixa.SumVendasGeradas(idCaixa).ToString();
            txtVendasMedia.Text = Validation.FormatPrice(_controllerCaixa.SumVendasMedia(idCaixa), true);

            txtVendasCanceladasTotal.Text =
                Validation.FormatPrice(_controllerCaixa.SumVendasCanceladasTotal(idCaixa), true);
            txtVendasCanceladas.Text = _controllerCaixa.SumVendasCanceladasGeradas(idCaixa).ToString();

            txtTotalRecebimento.Text = Validation.FormatPrice(_controllerCaixa.SumPagamentoTodos(idCaixa), true);
            txtDinheiro.Text = Validation.FormatPrice(_controllerCaixa.SumPagamento(idCaixa, 1), true);
            txtCheque.Text = Validation.FormatPrice(_controllerCaixa.SumPagamento(idCaixa, 2), true);
            txtCarDeb.Text = Validation.FormatPrice(_controllerCaixa.SumPagamento(idCaixa, 3), true);
            txtCarCred.Text = Validation.FormatPrice(_controllerCaixa.SumPagamento(idCaixa, 4), true);
            txtCrediario.Text = Validation.FormatPrice(_controllerCaixa.SumPagamento(idCaixa, 5), true);
            txtBoleto.Text = Validation.FormatPrice(_controllerCaixa.SumPagamento(idCaixa, 6), true);
        }

        private void LoadTotais()
        {
            LoadData();
            DataTableAsync();
        }

        private async Task DataTableAsync()
        {
            await SetTable(GridLista);
            await SetTable2(GridLista2);
        }

        public Task<IEnumerable<dynamic>> GetDataTableCaixa()
        {
            var model = new CaixaMovimentacao().Query();
            model.Where("CAIXA_MOV.id_caixa", idCaixa);
            model.WhereFalse("CAIXA_MOV.excluir");
            model.LeftJoin("FORMAPGTO", "FORMAPGTO.id", "CAIXA_MOV.id_formapgto");
            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "CAIXA_MOV.id_user");
            model.Select("FORMAPGTO.nome as nome_pgto", "CAIXA_MOV.*", "USUARIOS.NOME as USER_NAME");
            model.OrderByDesc("CAIXA_MOV.criado");
            return model.GetAsync<dynamic>();
        }

        public Task<IEnumerable<dynamic>> GetDataTableTitulo()
        {
            var model = new Titulo().Query();
            model.LeftJoin("FORMAPGTO", "FORMAPGTO.id", "TITULO.ID_FORMAPGTO");
            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "TITULO.id_usuario");
            model.LeftJoin("PEDIDO", "PEDIDO.ID", "TITULO.ID_PEDIDO");
            model.Select("FORMAPGTO.nome as nome_pgto", "TITULO.*", "USUARIOS.NOME as USER_NAME");
            model.Where("TITULO.id_caixa", idCaixa);
            model.Where("TITULO.id_caixa_mov", 0);
            model.Where("TITULO.excluir", 0);
            model.Where("PEDIDO.excluir", 0);
            //model.Where("PEDIDO.TIPO", "Vendas");
            model.OrderByDesc("TITULO.criado");
            return model.GetAsync<dynamic>();
        }

        public Task<IEnumerable<dynamic>> GetDataTablePedido()
        {
            var model = new Pedido().Query();
            model.Where("tipo", "Vendas");
            model.Where("id_caixa", idCaixa);
            model.Where("excluir", 0);
            model.OrderByDesc("pedido.id");
            return model.GetAsync<dynamic>();
        }

        private double GetDataTitulo(int idPedido)
        {
            var pedido = new Pedido().Query().Select("id_caixa").Where("id", idPedido).FirstOrDefault();
            int idCaixaPedido = pedido.ID_CAIXA ?? 0;

            var sum = new Titulo().Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_pedido", idPedido)
                .Where("id_caixa", idCaixaPedido).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL ?? 0);
        }

        public async Task<dynamic> GetDataMovAsync()
        {
            var dados = await GetDataTableCaixa();
            var dataCaixaMov = dados;

            var dadosTitulo = await GetDataTableTitulo();
            var dataTitulo = dadosTitulo;

            var objeto = new ArrayList();

            // Loop movimentações do caixa
            foreach (var item in dataCaixaMov)
                objeto.Add(new
                {
                    item.ID,
                    item.CRIADO,
                    item.DESCRICAO,
                    item.VALOR,
                    item.NOME_PGTO,
                    item.USER_NAME
                });

            // Loop titulos de vendas
            foreach (var item in dataTitulo)
                objeto.Add(new
                {
                    ID = item.ID_PEDIDO,
                    item.CRIADO,
                    DESCRICAO = $"Venda N° {item.ID_PEDIDO}",
                    VALOR = item.TOTAL,
                    item.NOME_PGTO,
                    item.USER_NAME
                });

            return objeto;
        }

        public async Task<dynamic> GetDataMovPedidosAsync()
        {
            IEnumerable<dynamic> dataPedido = await GetDataTablePedido();

            var objeto = new ArrayList();

            // Loop titulos de vendas
            foreach (var item in dataPedido)
            {
                var vendido = item.TOTAL;
                var recebido = GetDataTitulo(item.ID);

                objeto.Add(new
                {
                    item.ID,
                    item.CRIADO,
                    VENDIDO = vendido,
                    RECEBIDO = recebido
                });
            }

            return objeto;
        }

        public async Task SetTable(DataGridView Table)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Data/Hora";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Descrição";
            Table.Columns[2].MinimumWidth = 120;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].Width = 80;

            Table.Columns[4].Name = "Forma Pagto.";
            Table.Columns[4].Width = 140;

            Table.Columns[5].Name = "Usuário";
            Table.Columns[5].Width = 100;

            Table.Rows.Clear();

            foreach (var item in GetDataMovAsync().Result)
            {
                var valor = Validation.FormatPrice(Validation.ConvertToDouble(item.VALOR), true);

                Table.Rows.Add(
                    item.ID,
                    Validation.ConvertDateToForm(item.CRIADO, true),
                    item.DESCRICAO,
                    valor,
                    item.NOME_PGTO,
                    item.USER_NAME
                );
            }

            Table.Sort(Table.Columns[1], ListSortDirection.Descending);
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public async Task SetTable2(DataGridView Table)
        {
            Table.ColumnCount = 4;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "N° Venda";
            Table.Columns[0].Visible = true;

            Table.Columns[1].Name = "Data/Hora";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = "Total Vendido";
            Table.Columns[2].MinimumWidth = 120;

            Table.Columns[3].Name = "Total Recebido";
            Table.Columns[3].MinimumWidth = 120;

            Table.Rows.Clear();

            foreach (var item in GetDataMovPedidosAsync().Result)
            {
                var valor = Validation.FormatPrice(Validation.ConvertToDouble(item.VENDIDO), true);
                var valor2 = Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true);

                Table.Rows.Add(
                    item.ID,
                    Validation.ConvertDateToForm(item.CRIADO, true),
                    valor,
                    valor2
                );
            }

            Table.Sort(Table.Columns[1], ListSortDirection.Descending);
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void EditMovimentacao()
        {
            if (Restrito()) return;

            if (GridLista.SelectedRows.Count == 0)
                return;

            if (Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value) <= 0)
                return;

            if (GridLista.SelectedRows[0].Cells["Descrição"].Value.ToString().Contains("Venda"))
            {
                DetailsPedido.idPedido = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<DetailsPedido>(this);
                return;
            }

            AddCaixaMov.idMov = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
            var f = new AddCaixaMov();
            if (f.ShowDialog() == DialogResult.OK)
                LoadTotais();
        }

        private bool Restrito()
        {
            if (Home.idCaixa != idCaixa)
            {
                Alert.Message("Oppss!", "Você não tem permissão para fazer alterações nesse caixa.",
                    Alert.AlertType.warning);
                return true;
            }

            return false;
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

            Shown += async (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                Refresh();
                LoadData();

                await DataTableAsync();
            };

            if (_modelCaixa.Tipo != "Fechado")
            {
                GridLista.CellDoubleClick += (s, e) => EditMovimentacao();

                GridLista2.CellDoubleClick += (s, e) =>
                {
                    DetailsPedido.idPedido = Convert.ToInt32(GridLista2.SelectedRows[0].Cells["N° Venda"].Value);
                    OpenForm.Show<DetailsPedido>(this);
                };
            }

            btnEditar.Click += (s, e) => EditMovimentacao();

            btnLancamentos.Click += (s, e) =>
            {
                if (Restrito()) return;

                if (_modelCaixa.Tipo == "Fechado")
                {
                    Alert.Message("Oppss!", "Não é possível fazer lançamentos em um caixa fechado.",
                        Alert.AlertType.warning);
                    return;
                }

                AddCaixaMov.idCaixa = idCaixa;
                AddCaixaMov.idMov = 0;
                using (var f = new AddCaixaMov())
                {
                    if (f.ShowDialog() == DialogResult.OK)
                        LoadTotais();
                }
            };

            FecharCaixa.Click += async (s, e) =>
            {
                if (Restrito()) return;

                Financeiro.FecharCaixa.idCaixa = idCaixa;
                var f = new FecharCaixa();
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                txtFechado.Text = DateTime.Now.ToString("dd/mm/YYYY HH:mm", Program.cultura);
                panel7.BackColor = Color.FromArgb(192, 0, 0);
                label7.Text = @"Caixa Fechado";
                FecharCaixa.Enabled = false;
                btnLancamentos.Enabled = false;

                if (Financeiro.FecharCaixa.fecharImprimir)
                    await RenderizarAsync();
            };

            GridLista2.CellFormatting += (s, e) =>
            {
                foreach (DataGridViewRow row in GridLista2.Rows)
                    if (Validation.ConvertToDouble(row.Cells[3].Value) < Validation.ConvertToDouble(row.Cells[2].Value))
                    {
                        row.Cells[3].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold,
                            GraphicsUnit.Point, 0);
                        row.Cells[3].Style.ForeColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.FromArgb(255, 89, 89);
                    }
                    else
                    {
                        row.Cells[3].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold,
                            GraphicsUnit.Point, 0);
                        row.Cells[3].Style.ForeColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.FromArgb(139, 215, 146);
                    }
            };

            GridLista.CellFormatting += (s, e) =>
            {
                foreach (DataGridViewRow row in GridLista.Rows)
                    if (row.Cells[2].Value.ToString().Contains("ENTRADA") ||
                        row.Cells[2].Value.ToString().Contains("Venda"))
                    {
                        row.Cells[3].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold,
                            GraphicsUnit.Point, 0);
                        row.Cells[3].Style.ForeColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.FromArgb(139, 215, 146);
                    }
                    else
                    {
                        row.Cells[3].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold,
                            GraphicsUnit.Point, 0);
                        row.Cells[3].Style.ForeColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.FromArgb(255, 89, 89);
                    }
            };

            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");

            btnFinalizarImprimir.Click += async (s, e) => await RenderizarAsync();
        }

        private async Task RenderizarAsync()
        {
            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\CupomCaixaConferencia.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                CNPJ = Settings.Default.empresa_cnpj,
                Address =
                    $"{Settings.Default.empresa_rua} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                txtResponsavel = colaborador.Text,
                txtSaldoInicial = txtSaldoInicial.Text,
                txtAberto = aberto.Text,
                txtFechado = txtFechado.Text,
                nrTerminal = nrCaixa.Text,
                txtVendasTotal = txtVendasTotal.Text,
                txtVendasAcrescimos = txtVendasAcrescimos.Text,
                txtVendasDescontos = txtVendasDescontos.Text,
                txtVendasMedia = txtVendasMedia.Text,
                txtVendasCanceladasTotal = txtVendasCanceladasTotal.Text,
                txtVendasCanceladas = txtVendasCanceladas.Text,
                txtDinheiro = txtDinheiro.Text,
                txtCheque = txtCheque.Text,
                txtCarDeb = txtCarDeb.Text,
                txtCarCred = txtCarCred.Text,
                txtCrediario = txtCrediario.Text,
                txtBoleto = txtBoleto.Text,
                txtEntradas = txtEntradas.Text,
                txtSaidas = txtSaidas.Text,
                txtSaldoFinal = txtSaldoFinal.Text
            }));

            Browser.htmlRender = render;
            using (var f = new Browser())
            {
                f.ShowDialog();
            }
        }
    }
}
using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
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
    public partial class DetailsCaixa : Form
    {
        public static int idCaixa { get; set; }

        private Controller.Caixa _controllerCaixa = new Controller.Caixa();
        private Model.Caixa _modelCaixa = new Model.Caixa();
        private Model.CaixaMovimentacao _modelCaixaMov = new Model.CaixaMovimentacao();
        private Model.Usuarios _modelUsuarios = new Model.Usuarios();

        public DetailsCaixa()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadUsuario(int idUser)
        {
            var user = _modelUsuarios.FindByUserId(idUser).FirstOrDefault();
            if (user != null)
                colaborador.Text = user.NOME;
        }
        
        private void LoadData()
        {
            _modelCaixa = _modelCaixa.FindById(idCaixa).FirstOrDefault<Model.Caixa>();
            
            caixa.Text = _modelCaixa.Id.ToString();
            nrCaixa.Text = _modelCaixa.Id.ToString();
            terminal.Text = _modelCaixa.Terminal;
            aberto.Text = Validation.ConvertDateToForm(_modelCaixa.Criado, true);
            label7.Text = _modelCaixa.Tipo == "Aberto" ? "Caixa Aberto" : "Caixa Fechado";

            if (_modelCaixa.Tipo == "Fechado")
            {
                panel7.BackColor = Color.FromArgb(192, 0, 0);
                txtFechado.Text = Validation.ConvertDateToForm(_modelCaixa.Fechado, true);
                FecharCaixa.Enabled = false;
            }
            
            LoadUsuario(_modelCaixa.Usuario);

            txtSaldoInicial.Text = Validation.FormatPrice(_modelCaixa.Saldo_Inicial, true);
            txtEntradas.Text = Validation.FormatPrice(_controllerCaixa.SumEntradas(idCaixa), true);
            txtSaidas.Text = Validation.FormatPrice(_controllerCaixa.SumSaidas(idCaixa), true);            
            txtSaldoFinal.Text = Validation.FormatPrice(_controllerCaixa.SumSaldoFinal(idCaixa), true);

            txtVendasTotal.Text = Validation.FormatPrice(_controllerCaixa.SumVendasTotal(idCaixa), true);
            txtVendasAcrescimos.Text = Validation.FormatPrice(_controllerCaixa.SumVendasAcrescimos(idCaixa), true);
            txtVendasDescontos.Text = Validation.FormatPrice(_controllerCaixa.SumVendasDescontos(idCaixa), true);
            txtVendasGeradas.Text = _controllerCaixa.SumVendasGeradas(idCaixa).ToString();
            txtVendasMedia.Text = Validation.FormatPrice(_controllerCaixa.SumVendasMedia(idCaixa), true);

            txtVendasCanceladasTotal.Text = Validation.FormatPrice(_controllerCaixa.SumVendasCanceladasTotal(idCaixa), true);
            txtVendasCanceladas.Text = _controllerCaixa.SumVendasCanceladasGeradas(idCaixa).ToString();

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

        private async Task DataTableAsync() => await SetTable(GridLista);

        public Task<IEnumerable<dynamic>> GetDataTableCaixa()
        {
            var model = new Model.CaixaMovimentacao().Query();
            model.Where("id_caixa", idCaixa);
            model.WhereFalse("CAIXA_MOV.excluir");
            model.LeftJoin("FORMAPGTO", "FORMAPGTO.id", "CAIXA_MOV.id_formapgto");
            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "CAIXA_MOV.id_user");
            model.Select("FORMAPGTO.nome as nome_pgto", "CAIXA_MOV.*", "USUARIOS.NOME as USER_NAME");
            model.OrderByDesc("CAIXA_MOV.criado");
            return model.GetAsync<dynamic>();
        }

        public Task<IEnumerable<dynamic>> GetDataTablePedido()
        {
            var model = new Model.Titulo().Query();
            model.Where("id_caixa", idCaixa);
            model.LeftJoin("FORMAPGTO", "FORMAPGTO.id", "TITULO.ID_FORMAPGTO");
            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "TITULO.id_usuario");
            model.Select("FORMAPGTO.nome as nome_pgto", "TITULO.*", "USUARIOS.NOME as USER_NAME");
            model.OrderByDesc("TITULO.criado");
            return model.GetAsync<dynamic>();
        }

        public async Task<dynamic> GetDataMovAsync()
        {
            IEnumerable<dynamic> DataTitulo = null;
            IEnumerable<dynamic> DataCaixaMov = null;
            
            IEnumerable<dynamic> dados = await GetDataTableCaixa();
            DataCaixaMov = dados;

            IEnumerable<dynamic> dadosTitulo = await GetDataTablePedido();
            DataTitulo = dadosTitulo;

            ArrayList objeto = new ArrayList();

            // Loop movimentações do caixa
            foreach (var item in DataCaixaMov)
            {
                objeto.Add(new
                {
                    ID = item.ID,
                    CRIADO = item.CRIADO,
                    DESCRICAO = item.DESCRICAO,
                    VALOR = item.VALOR,
                    NOME_PGTO = item.NOME_PGTO,
                    USER_NAME = item.USER_NAME
                });
            }

            // Loop titulos de vendas
            foreach (var item in DataTitulo)
            {
                objeto.Add(new
                {
                    ID = item.ID_PEDIDO,
                    CRIADO = item.CRIADO,
                    DESCRICAO = $"Venda N° {item.ID_PEDIDO}",
                    VALOR = item.TOTAL,
                    NOME_PGTO = item.NOME_PGTO,
                    USER_NAME = item.USER_NAME
                });
            }

            return objeto;
        }

        public async Task SetTable(DataGridView Table)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;
            
            Table.Columns[1].Name = "Data/Hora";
            Table.Columns[1].Width = 100;
        
            Table.Columns[2].Name = "Descrição";
            Table.Columns[2].MinimumWidth = 230;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].Width = 80;

            Table.Columns[4].Name = "Forma Pagto.";
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Usuário";
            Table.Columns[5].Width = 150;

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

            Table.Sort(Table.Columns[1], System.ComponentModel.ListSortDirection.Descending);
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void EditMovimentacao()
        {
            if (Restrito()) return;

            if (GridLista.SelectedRows.Count == 0)
                return;

            if (Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value) > 0)
            {
                if (GridLista.SelectedRows[0].Cells["Descrição"].Value.ToString().Contains("Venda"))
                {
                    DetailsPedido.idPedido = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    DetailsPedido detailsPedido = new DetailsPedido();
                    detailsPedido.ShowDialog();
                    return;
                }

                AddCaixaMov.idMov = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                AddCaixaMov f = new AddCaixaMov();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadTotais();
                }
            }
        }

        private bool Restrito()
        {
            if (Home.idCaixa != idCaixa)
            {
                Alert.Message("Oppss!", "Você não tem permissão para fazer alterações nesse caixa.", Alert.AlertType.warning);
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

            Load += async (s, e) =>
            {
                LoadData();
                await DataTableAsync();
            };

            GridLista.CellDoubleClick += (s, e) => EditMovimentacao();
            btnEditar.Click += (s, e) => EditMovimentacao();

            btnLancamentos.Click += (s, e) =>
            {
                if (Restrito()) return;

                if (_modelCaixa.Tipo == "Fechado")
                {
                    Alert.Message("Oppss!", "Não é possível fazer lançamentos em um caixa fechado.", Alert.AlertType.warning);
                    return;
                }

                AddCaixaMov.idCaixa = idCaixa;
                AddCaixaMov.idMov = 0;
                var f = new AddCaixaMov();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadTotais();
                }
            };

            FecharCaixa.Click += (s, e) =>
            {
                if (Restrito()) return;

                Financeiro.FecharCaixa.idCaixa = idCaixa;
                FecharCaixa f = new FecharCaixa();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    txtFechado.Text = DateTime.Now.ToString("dd/mm/YYYY HH:mm");
                    panel7.BackColor = Color.FromArgb(192, 0, 0);
                    label7.Text = "Caixa Fechado";
                    FecharCaixa.Enabled = false;
                    btnLancamentos.Enabled = false;
                }
            };

            GridLista.CellFormatting += (s, e) =>
            {
                foreach (DataGridViewRow row in GridLista.Rows)
                {
                    if (row.Cells[2].Value.ToString().Contains("Entrada") || row.Cells[2].Value.ToString().Contains("Venda"))
                    {
                        row.Cells[3].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[3].Style.ForeColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.FromArgb(139,215,146);
                    }
                    else
                    {
                        row.Cells[3].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[3].Style.ForeColor = Color.White;
                        row.Cells[3].Style.BackColor = Color.FromArgb(255,89,89);
                    }
                }
            };

            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");

            btnFinalizarImprimir.Click += (s, e) => RenderizarAsync();
        }

        private async Task RenderizarAsync()
        {
            //if (Restrito()) return;

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\View\Reports\html\CupomCaixaConferencia.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                CNPJ = Settings.Default.empresa_cnpj,
                Address = $"{Settings.Default.empresa_rua} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
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
            var f = new Browser();
            f.ShowDialog();
        }
    }
}

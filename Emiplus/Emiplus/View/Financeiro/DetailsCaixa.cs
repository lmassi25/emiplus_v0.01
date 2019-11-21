﻿using DotLiquid;
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
            
            caixa.Text = _modelCaixa.Terminal;
            nrTerminal.Text = _modelCaixa.Terminal;
            aberto.Text = Validation.ConvertDateToForm(_modelCaixa.Criado, true);
            label7.Text = _modelCaixa.Tipo == "Aberto" ? "Caixa Aberto" : "Caixa Fechado";

            if (_modelCaixa.Tipo == "Fechado")
            {
                panel7.BackColor = Color.FromArgb(192, 0, 0);
                label9.Text = Validation.ConvertDateToForm(_modelCaixa.Fechado, true);
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
            model.Select("FORMAPGTO.nome as nome_pgto", "CAIXA_MOV.*");
            model.OrderByDesc("CAIXA_MOV.criado");
            return model.GetAsync<dynamic>();
        }

        public Task<IEnumerable<dynamic>> GetDataTablePedido()
        {
            var model = new Model.Titulo().Query();
            model.Where("id_caixa", idCaixa);
            model.LeftJoin("FORMAPGTO", "FORMAPGTO.id", "TITULO.ID_FORMAPGTO");
            model.Select("FORMAPGTO.nome as nome_pgto", "TITULO.*");
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
                    NOME_PGTO = item.NOME_PGTO
                });
            }

            // Loop titulos de vendas
            foreach (var item in DataTitulo)
            {
                objeto.Add(new
                {
                    ID = item.ID,
                    CRIADO = item.CRIADO,
                    DESCRICAO = $"Venda N° {item.ID_PEDIDO}",
                    VALOR = item.TOTAL,
                    NOME_PGTO = item.NOME_PGTO
                });
            }

            return objeto;
        }

        public async Task SetTable(DataGridView Table)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;
            
            Table.Columns[1].Name = "Data/Hora";
            Table.Columns[1].Width = 100;
            //Table.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
        
            Table.Columns[2].Name = "Descrição";
            Table.Columns[2].Width = 150;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].Width = 80;

            Table.Columns[4].Name = "Forma Pagto.";
            Table.Columns[4].Width = 100;

            Table.Rows.Clear();

            
            foreach (var item in GetDataMovAsync().Result)
            {
                var valor = Validation.FormatPrice(Validation.ConvertToDouble(item.VALOR), true);

                Table.Rows.Add(
                    item.ID,
                    Validation.ConvertDateToForm(item.CRIADO, true),
                    item.DESCRICAO,
                    valor,
                    item.NOME_PGTO
                );
            }

            Table.Sort(Table.Columns[1], System.ComponentModel.ListSortDirection.Descending);
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void EditMovimentacao()
        {
            if (GridLista.SelectedRows.Count == 0)
                return;

            if (Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value) > 0)
            {
                if (GridLista.SelectedRows[0].Cells["ID"].Value.ToString().Contains("Venda"))
                {
                    Alert.Message("", "", Alert.AlertType.warning);
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

        private void Eventos()
        {
            Load += async (s, e) =>
            {
                LoadData();
                await DataTableAsync();
            };

            GridLista.CellDoubleClick += (s, e) => EditMovimentacao();
            btnEditar.Click += (s, e) => EditMovimentacao();

            btnLancamentos.Click += (s, e) =>
            {
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
                Financeiro.FecharCaixa.idCaixa = idCaixa;
                FecharCaixa f = new FecharCaixa();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    label9.Text = DateTime.Now.ToString("dd/mm/YYYY HH:mm");
                    panel7.BackColor = Color.FromArgb(192, 0, 0);
                    label7.Text = "Caixa Fechado";
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
            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\View\Reports\html\CupomCaixaConferencia.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                txtSaldoInicial = txtSaldoInicial.Text,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Financeiro;
using LiveCharts;
using LiveCharts.Wpf;
using SqlKata.Execution;
using Caixa = Emiplus.Controller.Caixa;

namespace Emiplus.View.Common
{
    public partial class TelaInicial : Form
    {
        private readonly Titulo _mTitulo = new Titulo();
        private List<double> aPagar = new List<double>();

        private List<double> aReceber = new List<double>();

        private IEnumerable<dynamic> dataProductsEstoque;

        private readonly int Days = 6;
        private List<int> vendas = new List<int>();

        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public TelaInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private dynamic Pedidos { get; set; }
        private dynamic PedidosItens { get; set; }
        private dynamic GetTotalVendas { get; set; }
        private dynamic GetReceberHoje { get; set; }
        private dynamic GetPagarHoje { get; set; }
        private dynamic GetReceber7dias { get; set; }
        private dynamic GetPagar7dias { get; set; }
        private dynamic GetReceberAtrasado { get; set; }
        private dynamic GetPagarAtrasado { get; set; }

        private void LoadData()
        {
            totalVendas.Text = Pedidos != null ? Pedidos.TOTAL.ToString() : "0";
            itensVendidos.Text = PedidosItens.TOTAL != null ? PedidosItens.TOTAL.ToString() : "0";
            valorTotalVendas.Text = GetTotalVendas == null
                ? "R$ 00,00"
                : Validation.FormatPrice(Validation.ConvertToDouble(GetTotalVendas.TOTAL), true);

            if (GetReceberHoje != null && Pedidos != null)
                valorMedioVendas.Text = Validation.FormatPrice(Validation.ConvertToDouble(GetTotalVendas?.TOTAL / Pedidos.TOTAL), true);
            else
                valorMedioVendas.Text = @"R$ 00,00";

            aReceberHoje.Text = GetReceberHoje == null
                ? "R$ 00,00"
                : Validation.FormatPrice(Validation.ConvertToDouble(GetReceberHoje.TOTAL), true);
            aPagarHoje.Text = GetPagarHoje == null
                ? "R$ 00,00"
                : Validation.FormatPrice(Validation.ConvertToDouble(GetPagarHoje.TOTAL), true);
            aReceber7dias.Text = GetReceber7dias == null
                ? "R$ 00,00"
                : Validation.FormatPrice(Validation.ConvertToDouble(GetReceber7dias.TOTAL), true);
            aPagar7dias.Text = GetPagar7dias == null
                ? "R$ 00,00"
                : Validation.FormatPrice(Validation.ConvertToDouble(GetPagar7dias.TOTAL), true);
            aReceberAtrasado.Text = GetReceberAtrasado == null
                ? "R$ 00,00"
                : Validation.FormatPrice(Validation.ConvertToDouble(GetReceberAtrasado.TOTAL), true);
            aPagarAtrasado.Text = GetPagarAtrasado == null
                ? "R$ 00,00"
                : Validation.FormatPrice(Validation.ConvertToDouble(GetPagarAtrasado.TOTAL), true);
        }

        /// <summary>
        ///     Usado no WorkerBackground para recuperar os dados do banco e alimentar o LoadData()
        /// </summary>
        private void GetDados()
        {
            Pedidos = new Pedido().Query().SelectRaw("COUNT(ID) AS TOTAL").Where("tipo", "Vendas")
                .WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();

            PedidosItens = new PedidoItem().Query().SelectRaw("SUM(QUANTIDADE) AS TOTAL").Where("tipo", "Produtos")
                .WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            
            GetTotalVendas = new Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").Where("tipo", "Vendas")
                .WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();

            GetReceberHoje = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                .Where("tipo", "Receber")
                .Where("vencimento", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();

            GetPagarHoje = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                .Where("tipo", "Pagar")
                .Where("vencimento", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();

            GetReceber7dias = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                .Where("tipo", "Receber")
                .Where("vencimento", ">", DateTime.Now.AddDays(+1).ToString("dd.MM.yyyy"))
                .Where("vencimento", "<=", DateTime.Now.AddDays(+7).ToString("dd.MM.yyyy")).WhereNull("baixa_data")
                .FirstOrDefault();

            GetPagar7dias = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                .Where("tipo", "Pagar")
                .Where("vencimento", ">", DateTime.Now.AddDays(+1).ToString("dd.MM.yyyy"))
                .Where("vencimento", "<=", DateTime.Now.AddDays(+7).ToString("dd.MM.yyyy")).WhereNull("baixa_data")
                .FirstOrDefault();

            GetReceberAtrasado = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                .Where("tipo", "Receber")
                .Where("vencimento", "<", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();

            GetPagarAtrasado = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                .Where("tipo", "Pagar")
                .Where("vencimento", "<", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
        }

        private List<double> GetValues(string tipo)
        {
            var values = new List<double>();

            var dias = 7;
            for (var i = 0; i < dias; i++)
            {
                var data = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", tipo)
                    .Where("vencimento", DateTime.Now.AddDays(-i).ToString("dd.MM.yyyy")).WhereNull("baixa_data")
                    .FirstOrDefault();
                values.Add(data != null ? Validation.ConvertToDouble(data.TOTAL) : "00.00");
            }

            values.Reverse();

            return values;
        }

        private List<int> GetVendas()
        {
            var values = new List<int>();

            var dias = 7;
            for (var i = 0; i < dias; i++)
            {
                var data = new Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                    .Where("tipo", "Vendas")
                    .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString(), true))
                    .Where("criado", "<=",
                        Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString("yyyy-MM-dd 23:59"), true))
                    .FirstOrDefault();
                values.Add(data != null ? Validation.ConvertToInt32(data.TOTAL) : "0");
            }

            values.Reverse();

            return values;
        }

        private void LoadGrafico()
        {
            var labels = new string[7];
            for (var i = 0; i < 7; i++)
                labels[i] = DateTime.Now.AddDays(-i).ToString("dd/MM");

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Últimos 7 dias (da semana atual)",
                Labels = new[] {labels[6], labels[5], labels[4], labels[3], labels[2], labels[1], "Hoje"}
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                LabelFormatter = value => Validation.FormatPrice(value, true)
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;
        }

        private void LoadSeriesGrafico()
        {
            cartesianChart1.Series.Clear();
            var series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "A Receber",
                    PointGeometrySize = 20,
                    Values = new ChartValues<double>(aReceber),
                    Stroke = new SolidColorBrush(Color.FromRgb(51, 211, 74))
                },
                new LineSeries
                {
                    Title = "A Pagar",
                    Values = new ChartValues<double>(aPagar),
                    PointGeometrySize = 15,
                    Stroke = new SolidColorBrush(Color.FromRgb(243, 102, 36))
                },
                new LineSeries
                {
                    Title = "Vendas",
                    Values = new ChartValues<int>(vendas),
                    Stroke = new SolidColorBrush(Color.FromRgb(28, 142, 196)),
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection(20),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 0
                }
            };

            cartesianChart1.Series = series;
        }

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            table.Columns[0].Name = "ID";
            table.Columns[0].Visible = false;

            table.Columns[1].Name = "Cód. de Barras";
            table.Columns[1].Width = 130;
            table.Columns[1].Visible = true;

            table.Columns[2].Name = "Referência";
            table.Columns[2].Width = 100;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Descrição";
            table.Columns[3].Width = 120;
            table.Columns[3].MinimumWidth = 120;
            table.Columns[3].Visible = true;

            table.Columns[4].Name = "Estoque Mínimo";
            table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[4].Width = 130;
            table.Columns[4].Visible = true;

            table.Columns[5].Name = "Estoque Atual";
            table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[5].Width = 130;
            table.Columns[5].Visible = true;
        }

        private async Task SetContentTableAsync(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.Rows.Clear();

            foreach (var item in Data)
                Table.Rows.Add(
                    item.ID,
                    item.CODEBARRAS,
                    item.REFERENCIA,
                    item.NOME,
                    Validation.FormatMedidas(item.MEDIDA, Validation.ConvertToDouble(item.ESTOQUEMINIMO)),
                    Validation.FormatMedidas(item.MEDIDA, Validation.ConvertToDouble(item.ESTOQUEATUAL))
                );

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Shown += async (s, e) =>
            {
                Refresh();

                loading.Visible = true;
                workerBackground.RunWorkerAsync();

                ToolHelp.Show(
                    $"Referente ao período ({DateTime.Now.AddDays(-Days):dd/MM/yyyy}) até Hoje ({DateTime.Now:dd/MM/yyyy})",
                    pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");

                dataSemana.Text = $@"{DateTime.Now.AddDays(-Days):dd/MM/yyyy} até Hoje ({DateTime.Now:dd/MM/yyyy})";
                LoadGrafico();

                SetHeadersTable(GridLista);

                await Task.Delay(500);
                new Caixa().CheckCaixaDate();
            };

            timer1.Tick += (s, e) =>
            {
                panel1.Visible = false;
                loading.Visible = true;

                workerBackground.RunWorkerAsync();
                timer1.Enabled = true;
                timer1.Stop();
            };

            btnTodosAreceber.Click += (s, e) =>
            {
                if (UserPermission.SetControlLabel(btnTodosAreceber, pictureBox3, "fin_recebimentos"))
                    return;

                Home.financeiroPage = "Receber";
                OpenForm.Show<Titulos>(this);
            };

            btnTodosApagar.Click += (s, e) =>
            {
                if (UserPermission.SetControlLabel(btnTodosApagar, pictureBox3, "fin_pagamentos"))
                    return;

                Home.financeiroPage = "Pagar";
                OpenForm.Show<Titulos>(this);
            };

            btnNovoRecebimento.Click += (s, e) =>
            {
                Home.financeiroPage = "Receber";
                EditarTitulo.IdTitulo = 0;
                OpenForm.Show<EditarTitulo>(this);
            };

            btnNovoPagamento.Click += (s, e) =>
            {
                Home.financeiroPage = "Pagar";
                EditarTitulo.IdTitulo = 0;
                OpenForm.Show<EditarTitulo>(this);
            };

            workerBackground.DoWork += (s, e) =>
            {
                GetDados();
                new Estoque().GerarEstoque();

                aReceber = GetValues("Receber");
                aPagar = GetValues("Pagar");
                vendas = GetVendas();

                dataProductsEstoque = new Item().FindAll()
                    .WhereRaw("estoqueminimo >= estoqueatual")
                    .Where("estoqueminimo", "!=", "0")
                    .WhereFalse("excluir")
                    .Get();
            };

            workerBackground.RunWorkerCompleted += async (s, e) =>
            {
                panel1.Visible = true;
                loading.Visible = false;
                //timer1.Start();
                LoadData();
                LoadSeriesGrafico();

                await SetContentTableAsync(GridLista, dataProductsEstoque);
            };

            btnRefresh.Click += (s, e) =>
            {
                panel1.Visible = false;
                loading.Visible = true;

                workerBackground.RunWorkerAsync();
            };
        }
    }
}
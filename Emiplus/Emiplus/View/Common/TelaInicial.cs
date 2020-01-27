using Emiplus.Data.Helpers;
using Emiplus.View.Financeiro;
using LiveCharts;
using LiveCharts.Wpf;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaInicial : Form
    {
        private Model.Titulo _mTitulo = new Model.Titulo();

        private int Days = 6;

        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        private dynamic Pedidos { get; set; }
        private dynamic Pedidos_Itens { get; set; }
        private dynamic GetTotalVendas { get; set; }
        private dynamic GetReceberHoje { get; set; }
        private dynamic GetPagarHoje { get; set; }
        private dynamic GetReceber7dias { get; set; }
        private dynamic GetPagar7dias { get; set; }
        private dynamic GetReceberAtrasado { get; set; }
        private dynamic GetPagarAtrasado { get; set; }

        private List<double> aReceber = new List<double>();
        private List<double> aPagar = new List<double>();
        private List<int> Vendas = new List<int>();

        public TelaInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            totalVendas.Text = Pedidos != null ? Pedidos.TOTAL.ToString() : "0";
            itensVendidos.Text = Pedidos_Itens.TOTAL != null ? Pedidos_Itens.TOTAL.ToString() : "0";
            valorTotalVendas.Text = GetTotalVendas == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(GetTotalVendas.TOTAL), true);

            if (GetReceberHoje != null && Pedidos != null)
                valorMedioVendas.Text = Validation.FormatPrice(Validation.ConvertToDouble(GetTotalVendas.TOTAL / Pedidos.TOTAL), true);
            else
                valorMedioVendas.Text = "R$ 00,00";

            this.aReceberHoje.Text = GetReceberHoje == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(GetReceberHoje.TOTAL), true);
            this.aPagarHoje.Text = GetPagarHoje == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(GetPagarHoje.TOTAL), true);
            this.aReceber7dias.Text = GetReceber7dias == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(GetReceber7dias.TOTAL), true);
            this.aPagar7dias.Text = GetPagar7dias == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(GetPagar7dias.TOTAL), true);
            this.aReceberAtrasado.Text = GetReceberAtrasado == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(GetReceberAtrasado.TOTAL), true);
            this.aPagarAtrasado.Text = GetPagarAtrasado == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(GetPagarAtrasado.TOTAL), true);
        }

        /// <summary>
        /// Usado no WorkerBackground para recuperar os dados do banco e alimentar o LoadData()
        /// </summary>
        private void GetDados()
        {
            var Pedidos = new Model.Pedido().Query().SelectRaw("COUNT(ID) AS TOTAL").Where("tipo", "Vendas").WhereFalse("excluir")
               .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
               .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            this.Pedidos = Pedidos;

            var Pedidos_Itens = new Model.PedidoItem().Query().SelectRaw("SUM(QUANTIDADE) AS TOTAL").Where("tipo", "Produtos").WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            this.Pedidos_Itens = Pedidos_Itens;

            var Total = new Model.Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").Where("tipo", "Vendas").WhereFalse("excluir")
               .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
               .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            this.GetTotalVendas = Total;

            var aReceberHoje = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Receber")
            .Where("vencimento", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            this.GetReceberHoje = aReceberHoje;

            var aPagarHoje = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Pagar")
            .Where("vencimento", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            this.GetPagarHoje = aPagarHoje;

            var aReceber7dias = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Receber")
            .Where("vencimento", ">", DateTime.Now.AddDays(+1).ToString("dd.MM.yyyy"))
            .Where("vencimento", "<=", DateTime.Now.AddDays(+7).ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            this.GetReceber7dias = aReceber7dias;

            var aPagar7dias = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Pagar")
            .Where("vencimento", ">", DateTime.Now.AddDays(+1).ToString("dd.MM.yyyy"))
            .Where("vencimento", "<=", DateTime.Now.AddDays(+7).ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            this.GetPagar7dias = aPagar7dias;

            var aReceberAtrasado = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Receber")
            .Where("vencimento", "<", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            this.GetReceberAtrasado = aReceberAtrasado;

            var aPagarAtrasado = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Pagar")
           .Where("vencimento", "<", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            this.GetPagarAtrasado = aPagarAtrasado;
        }

        private List<double> GetValues(string tipo)
        {
            List<double> values = new List<double>();

            int dias = 7;
            for (int i = 0; i < dias; i++)
            {
                var data = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", tipo)
                .Where("vencimento", DateTime.Now.AddDays(-i).ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
                values.Add(data != null ? Validation.ConvertToDouble(data.TOTAL) : "00.00");
            }
            values.Reverse();

            return values;
        }

        private List<int> GetVendas()
        {
            List<int> values = new List<int>();

            int dias = 7;
            for (int i = 0; i < dias; i++)
            {
                var data = new Model.Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Vendas")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString("yyyy-MM-dd 23:59"), true)).FirstOrDefault();
                values.Add(data != null ? Validation.ConvertToInt32(data.TOTAL) : "0");
            }
            values.Reverse();

            return values;
        }

        private void LoadGrafico()
        {
            var labels = new string[7];
            for (int i = 0; i < 7; i++)
                labels[i] = DateTime.Now.AddDays(-i).ToString("dd/MM");

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Últimos 7 dias (da semana atual)",
                Labels = new[] { labels[6], labels[5], labels[4], labels[3], labels[2], labels[1], "Hoje" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                LabelFormatter = value => Validation.FormatPrice(value, true),
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;
        }

        private void LoadSeriesGrafico()
        {
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            series.Add(new LineSeries()
            {
                Title = "A Receber",
                PointGeometrySize = 20,
                Values = new ChartValues<double>(aReceber),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 211, 74))
            });

            series.Add(new LineSeries()
            {
                Title = "A Pagar",
                Values = new ChartValues<double>(aPagar),
                PointGeometrySize = 15,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(243, 102, 36))
            });

            series.Add(new LineSeries()
            {
                Title = "Vendas",
                Values = new ChartValues<int>(Vendas),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(28, 142, 196)),
                StrokeThickness = 1,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 0
            });

            cartesianChart1.Series = series;
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                this.Refresh();

                ToolHelp.Show($"Referente ao período {DateTime.Now.AddDays(-Days).ToString("dd/MM/yyyy")} até Hoje ({DateTime.Now.ToString("dd/MM/yyyy")})", pictureBox4, ToolHelp.ToolTipIcon.Info, "Ajuda!");

                dataSemana.Text = $"{DateTime.Now.AddDays(-Days).ToString("dd/MM/yyyy")} até Hoje ({DateTime.Now.ToString("dd/MM/yyyy")})";
                LoadGrafico();

                timer1.Start();
            };

            timer1.Tick += (s, e) =>
            {
                WorkerBackground.RunWorkerAsync();
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

            WorkerBackground.DoWork += (s, e) =>
            {
                GetDados();

                aReceber = GetValues("Receber");
                aPagar = GetValues("Pagar");
                Vendas = GetVendas();
            };

            WorkerBackground.RunWorkerCompleted += (s, e) =>
            {
                timer1.Start();
                LoadData();
                LoadSeriesGrafico();
            };
        }
    }
}
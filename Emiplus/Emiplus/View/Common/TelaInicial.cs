using Emiplus.Data.Helpers;
using Emiplus.View.Financeiro;
using LiveCharts;
using LiveCharts.Wpf;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;

namespace Emiplus.View.Common
{
    public partial class TelaInicial : Form
    {
        Model.Titulo _mTitulo = new Model.Titulo();

        int Days = 6;

        public TelaInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            var Pedidos = new Model.Pedido().Query().SelectRaw("COUNT(ID) AS TOTAL").Where("tipo", "Vendas").WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            totalVendas.Text = Pedidos.TOTAL.ToString();

            var Pedidos_Itens = new Model.PedidoItem().Query().SelectRaw("SUM(QUANTIDADE) AS TOTAL").Where("tipo", "Produtos").WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            itensVendidos.Text = Pedidos_Itens.TOTAL.ToString();

            var Total = new Model.Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").Where("tipo", "Vendas").WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-Days).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            valorTotalVendas.Text = Total.TOTAL == null ? "R$ 00,00" : Validation.FormatPrice(Validation.ConvertToDouble(Total.TOTAL), true);

            valorMedioVendas.Text = Validation.FormatPrice(Validation.ConvertToDouble(Total.TOTAL / Pedidos.TOTAL), true);

            // A receber hoje
            var aReceberHoje = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Receber")
            .Where("vencimento", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            if (aReceberHoje != null)
                this.aReceberHoje.Text = Validation.FormatPrice(Validation.ConvertToDouble(aReceberHoje.TOTAL), true);

            var aPagarHoje = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Pagar")
            .Where("vencimento", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            if (aPagarHoje != null)
                this.aPagarHoje.Text = Validation.FormatPrice(Validation.ConvertToDouble(aPagarHoje.TOTAL), true);

            var aReceber7dias = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Receber")
            .Where("vencimento", ">", DateTime.Now.AddDays(+1).ToString("dd.MM.yyyy"))
            .Where("vencimento", "<=", DateTime.Now.AddDays(+7).ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            if (aReceber7dias != null)
                this.aReceber7dias.Text = Validation.FormatPrice(Validation.ConvertToDouble(aReceber7dias.TOTAL), true);

            var aPagar7dias = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Pagar")
            .Where("vencimento", ">", DateTime.Now.AddDays(+1).ToString("dd.MM.yyyy"))
            .Where("vencimento", "<=", DateTime.Now.AddDays(+7).ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            if (aPagar7dias != null)
                this.aPagar7dias.Text = Validation.FormatPrice(Validation.ConvertToDouble(aPagar7dias.TOTAL), true);

            var aReceberAtrasado = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Receber")
            .Where("vencimento", "<", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            if (aReceberAtrasado != null)
                this.aReceberAtrasado.Text = Validation.FormatPrice(Validation.ConvertToDouble(aReceberAtrasado.TOTAL), true);

            var aPagarAtrasado = _mTitulo.Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Pagar")
            .Where("vencimento", "<", DateTime.Now.ToString("dd.MM.yyyy")).WhereNull("baixa_data").FirstOrDefault();
            if (aPagarAtrasado != null)
                this.aPagarAtrasado.Text = Validation.FormatPrice(Validation.ConvertToDouble(aPagarAtrasado.TOTAL), true);
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
                var data = new Model.Pedido().Query().SelectRaw("COUNT(ID) AS TOTAL").WhereFalse("excluir").Where("tipo", "Vendas")
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
                Title = "Últimos 7 dias (da semana)",
                Labels = new[] { labels[6], labels[5], labels[4], labels[3], labels[2], labels[1], "Hoje" },
                LabelFormatter = value => value.ToString("C")
            });
            
            cartesianChart1.LegendLocation = LegendLocation.Right;
        }

        private void LoadSeriesGrafico()
        {
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            series.Add(new LineSeries() { 
                Title = "A Pagar", 
                Values = new ChartValues<double>(GetValues("Pagar")), 
                PointGeometrySize = 15,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(243, 102, 36)),
            });

            series.Add(new LineSeries() { 
                Title = "A Receber",
                PointGeometrySize = 20,
                Values = new ChartValues<double>(GetValues("Receber")),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 211, 74)),
            });

            series.Add(new LineSeries() { 
                Title = "Vendas", 
                Values = new ChartValues<int>(GetVendas()),
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
            Load += (s, e) =>
            {
                dataSemana.Text = $"{DateTime.Now.AddDays(-Days).ToString("dd/MM/yyyy")} até Hoje ({DateTime.Now.ToString("dd/MM/yyyy")})";
                LoadData();
                LoadGrafico();
                LoadSeriesGrafico();
                timer1.Start();
            };

            timer1.Tick += (s, e) =>
            {
                LoadData();
                LoadSeriesGrafico();
                timer1.Enabled = true;
                timer1.Start();
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
        }
    }
}

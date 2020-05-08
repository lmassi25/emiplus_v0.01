using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Media;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using LiveCharts;
using LiveCharts.Wpf;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class DetalhesComissao : Form
    {
        private List<double> valor = new List<double>();

        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public DetalhesComissao()
        {
            InitializeComponent();
            Eventos();
        }

        public static int idUser { get; set; }
        private int QuantidadeDias { get; set; }
        private Usuarios Usuario { get; set; }

        private List<double> GetDados()
        {
            var values = new List<double>();

            for (var i = 0; i < QuantidadeDias; i++)
            {
                var data = new Model.Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir")
                    .Where("tipo", "Vendas").Where("colaborador", idUser)
                    .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString(), true))
                    .Where("criado", "<=",
                        Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString("yyyy-MM-dd 23:59"), true))
                    .FirstOrDefault();
                values.Add(data != null ? Validation.ConvertToInt32(data.TOTAL) : "0");
            }

            return values;
        }

        private void GetDays()
        {
            QuantidadeDias = DateTime.Parse(dataFinal.Value.ToString())
                .Subtract(DateTime.Parse(dataInicial.Value.ToString())).Days + 1;
        }

        private void LoadDados()
        {
            var Total = new Model.Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").Where("tipo", "Vendas")
                .WhereFalse("excluir").Where("colaborador", idUser)
                .Where("criado", ">=", Validation.ConvertDateToSql(dataInicial.Text, true))
                .Where("criado", "<=", Validation.ConvertDateToSql(dataFinal.Text, true)).FirstOrDefault();
            totalVendas.Text = Validation.FormatPrice(Validation.ConvertToDouble(Total.TOTAL), true);

            double vlrComissao = Validation.ConvertToDouble(Total.TOTAL) * (Usuario.Comissao / 100.0);
            totalComissao.Text = Validation.FormatPrice(vlrComissao, true);
        }

        private void LoadGrafico()
        {
            cartesianChart1.AxisY.Add(new Axis
            {
                LabelFormatter = value => Validation.FormatPrice(value, true)
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;
        }

        private void LoadLabels()
        {
            cartesianChart1.AxisX.Clear();

            var labels = new string[QuantidadeDias];
            for (var i = 0; i < QuantidadeDias; i++)
                labels[i] = DateTime.Now.AddDays(-i).ToString("dd/MM");

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Valores",
                Labels = labels
            });
        }

        private void LoadSeriesGrafico()
        {
            cartesianChart1.Series.Clear();

            var series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Valor",
                    PointGeometrySize = 10,
                    Values = new ChartValues<double>(valor),
                    Stroke = new SolidColorBrush(Color.FromRgb(51, 211, 74))
                }
            };

            cartesianChart1.Series = series;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                dataInicial.Text = DateTime.Today.AddMonths(-1).ToString();
                dataFinal.Text = DateTime.Now.ToString();

                Usuario = new Usuarios().FindAll().Where("id_user", idUser).FirstOrDefault<Usuarios>();

                LoadDados();
                GetDays();
            };

            Shown += (s, e) =>
            {
                Refresh();

                LoadGrafico();
                LoadLabels();

                workerBackground.RunWorkerAsync();
            };

            filtrar.Click += (s, e) =>
            {
                GetDays();
                LoadLabels();
                workerBackground.RunWorkerAsync();
            };

            workerBackground.DoWork += (s, e) => { valor = GetDados(); };

            workerBackground.RunWorkerCompleted += (s, e) =>
            {
                LoadDados();
                LoadSeriesGrafico();
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using LiveCharts;
using LiveCharts.Wpf;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class DetalhesComissao : Form
    {
        public static int idUser { get; set; }
        private int quantidadeDias { get; set; }
        private Model.Usuarios Usuario { get; set; }

        private List<double> Valor = new List<double>();

        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        public DetalhesComissao()
        {
            InitializeComponent();
            Eventos();
        }

        private List<double> GetDados()
        {
            List<double> values = new List<double>();

            for (int i = 0; i < quantidadeDias; i++)
            {
                var data = new Model.Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").WhereFalse("excluir").Where("tipo", "Vendas").Where("id_usuario", idUser)
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-i).ToString("yyyy-MM-dd 23:59"), true)).FirstOrDefault();
                values.Add(data != null ? Validation.ConvertToInt32(data.TOTAL) : "0");
            }
            //values.Reverse();

            return values;
        }

        private void GetDays()
        {
            quantidadeDias = (DateTime.Parse(dataFinal.Value.ToString()).Subtract(DateTime.Parse(dataInicial.Value.ToString()))).Days + 1;
        }

        private void LoadDados()
        {
            var Total = new Model.Pedido().Query().SelectRaw("SUM(TOTAL) AS TOTAL").Where("tipo", "Vendas").WhereFalse("excluir").Where("id_usuario", idUser)
               .Where("criado", ">=", Validation.ConvertDateToSql(dataInicial.Text, true))
               .Where("criado", "<=", Validation.ConvertDateToSql(dataFinal.Text, true)).FirstOrDefault();
            totalVendas.Text = Validation.FormatPrice(Validation.ConvertToDouble(Total.TOTAL), true);

            double VlrComissao = Usuario.Comissao / 100.0 * Validation.ConvertToDouble(Total.TOTAL);
            totalComissao.Text = Validation.FormatPrice(VlrComissao, true);
        }

        private void LoadGrafico()
        {
            cartesianChart1.AxisY.Add(new Axis
            {
                LabelFormatter = value => Validation.FormatPrice(value, true),
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;
        }

        private void LoadLabels()
        {
            cartesianChart1.AxisX.Clear();

            var labels = new string[quantidadeDias];
            for (int i = 0; i < quantidadeDias; i++)
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

            SeriesCollection series = new SeriesCollection();
            series.Add(new LineSeries()
            {
                Title = "Valor",
                PointGeometrySize = 10,
                Values = new ChartValues<double>(Valor),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 211, 74))
            });

            cartesianChart1.Series = series;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                dataInicial.Text = DateTime.Today.AddMonths(-1).ToString();
                dataFinal.Text = DateTime.Now.ToString();
                
                Usuario = new Model.Usuarios().FindAll().Where("id_user", idUser).FirstOrDefault<Model.Usuarios>();

                LoadDados();
                GetDays();
            };

            Shown += (s, e) =>
            {
                this.Refresh();

                LoadGrafico();
                LoadLabels();

                WorkerBackground.RunWorkerAsync();
            };

            filtrar.Click += (s, e) =>
            {
                GetDays();
                LoadLabels();
                WorkerBackground.RunWorkerAsync();
            };

            WorkerBackground.DoWork += (s, e) =>
            {
                Valor = GetDados();
            };

            WorkerBackground.RunWorkerCompleted += (s, e) =>
            {
                LoadDados();
                LoadSeriesGrafico();
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}

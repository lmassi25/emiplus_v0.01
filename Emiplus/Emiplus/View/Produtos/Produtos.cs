using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Produtos : Form
    {
        public static int idPdtSelecionado { get; set; }
        private Controller.Item _controller = new Controller.Item();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(1000);

        public Produtos()
        {
            InitializeComponent();
            Eventos();
        }

        private void DataTableStart()
        {
            GridListaProdutos.Visible = false;
            Loading.Size = new System.Drawing.Size(GridListaProdutos.Width, GridListaProdutos.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void DataTable() => await _controller.SetTable(GridListaProdutos, null, search.Text);
        
        private void EditProduct(bool create = false)
        {
            if (create)
            {
                idPdtSelecionado = 0;
                OpenForm.Show<AddProduct>(this);
                return;
            }

            if (GridListaProdutos.SelectedRows.Count > 0)
            {
                idPdtSelecionado = Convert.ToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddProduct>(this);
            }
        }

        private void Eventos()
        {
            Load += (s, e) => {
                search.Select();
                DataTableStart();
            };

            btnAdicionar.Click += (s, e) => EditProduct(true);
            btnEditar.Click += (s, e) => EditProduct();
            GridListaProdutos.DoubleClick += (s, e) => EditProduct();

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridListaProdutos.Visible = false;
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _controller.GetDataTable();
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _controller.SetTable(GridListaProdutos, dataTable);

                    Loading.Visible = false;
                    GridListaProdutos.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker) delegate {
                DataTable();
                Loading.Visible = false;
                GridListaProdutos.Visible = true;
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };
        }
    }
}
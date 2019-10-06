using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Impostos : Form
    {
        public static int idImpSelected { get; set; }
        private Controller.Imposto _controller = new Controller.Imposto();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        public Impostos()
        {
            InitializeComponent();
            Eventos();
        }

        private async void DataTable() => await _controller.SetTable(GridListaImpostos, null, search.Text);

        private void DataTableStart()
        {
            GridListaImpostos.Visible = false;
            Loading.Size = new System.Drawing.Size(GridListaImpostos.Width, GridListaImpostos.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private void EditImposto(bool create = false)
        {
            if (create)
            {
                idImpSelected = 0;
                OpenForm.Show<AddImpostos>(this);
                return;
            }

            if (GridListaImpostos.SelectedRows.Count > 0)
            {
                idImpSelected = Convert.ToInt32(GridListaImpostos.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddImpostos>(this);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridListaImpostos);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    Support.UpDownDataGrid(true, GridListaImpostos);
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    EditImposto();
                    break;
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                search.Select();
                DataTableStart();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            Adicionar.Click += (s, e) => EditImposto(true);
            Editar.Click += (s, e) => EditImposto();
            GridListaImpostos.DoubleClick += (s, e) => EditImposto();

            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridListaImpostos.Visible = false;
            };
            search.KeyDown += KeyDowns;
            GridListaImpostos.KeyDown += KeyDowns;

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _controller.GetDataTable();
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _controller.SetTable(GridListaImpostos, dataTable);

                    Loading.Visible = false;
                    GridListaImpostos.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker)delegate {
                DataTable();
                Loading.Visible = false;
                GridListaImpostos.Visible = true;
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };
        }
    }
}

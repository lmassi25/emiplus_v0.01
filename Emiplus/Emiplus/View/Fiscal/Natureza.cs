using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Fiscal
{
    public partial class Natureza : Form
    {
        public static int idSelected { get; set; }
        private Controller.Natureza _controller = new Controller.Natureza();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        public Natureza()
        {
            InitializeComponent();
            Eventos();
        }

        private void DataTableStart()
        {
            GridLista.Visible = false;
            Loading.Size = new Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void DataTable() => await _controller.SetTable(GridLista, null, search.Text);

        private void EditCategoria(bool create = false)
        {
            if (create)
            {
                idSelected = 0;
                OpenForm.Show<AddNatureza>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                idSelected = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddNatureza>(this);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridLista);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    Support.UpDownDataGrid(true, GridLista);
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    EditCategoria();
                    break;
            }
        }

        private void Eventos()
        {
            Load += (s, e) => {
                search.Select();
                DataTableStart();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            Adicionar.Click += (s, e) => EditCategoria(true);
            Editar.Click += (s, e) => EditCategoria();
            GridLista.DoubleClick += (s, e) => EditCategoria();
            GridLista.KeyDown += KeyDowns;

            search.KeyDown += KeyDowns;
            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridLista.Visible = false;
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
                    await _controller.SetTable(GridLista, dataTable);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker)delegate {
                DataTable();
                Loading.Visible = false;
                GridLista.Visible = true;
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };
        }
    }
}

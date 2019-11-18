using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Categorias : Form
    {
        public static int idCatSelected { get; set; }
        private Controller.Categoria _controller = new Controller.Categoria();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        public Categorias()
        {
            InitializeComponent();
            Eventos();

            if (Home.CategoriaPage == "Produtos")
            {
                label1.Text = "Categorias";
                label2.Text = "Se organize melhor criando categorias para seus produtos.";
            }

            if (Home.CategoriaPage == "Financeiro")
            {
                label1.Text = "Categorias de Contas";
                label2.Text = "Se organize melhor criando categorias de contas para seus produtos.";
            }
        }

        private void DataTableStart()
        {
            GridListaCategorias.Visible = false;
            Loading.Size = new System.Drawing.Size(GridListaCategorias.Width, GridListaCategorias.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void DataTable() => await _controller.SetTable(GridListaCategorias, null, search.Text);

        private void EditCategoria(bool create = false)
        {
            if (create)
            {
                idCatSelected = 0;
                OpenForm.Show<AddCategorias>(this);
                return;
            }

            if (GridListaCategorias.SelectedRows.Count > 0)
            {
                idCatSelected = Convert.ToInt32(GridListaCategorias.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddCategorias>(this);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridListaCategorias);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    Support.UpDownDataGrid(true, GridListaCategorias);
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
            GridListaCategorias.DoubleClick += (s, e) => EditCategoria();
            GridListaCategorias.KeyDown += KeyDowns;

            search.KeyDown += KeyDowns;
            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridListaCategorias.Visible = false;
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
                    await _controller.SetTable(GridListaCategorias, dataTable);

                    Loading.Visible = false;
                    GridListaCategorias.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker)delegate {
                DataTable();
                Loading.Visible = false;
                GridListaCategorias.Visible = true;
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                DataTable();
            };
        }
    }
}

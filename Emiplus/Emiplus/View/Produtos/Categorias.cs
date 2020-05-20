using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Timer = System.Timers.Timer;

namespace Emiplus.View.Produtos
{
    public partial class Categorias : Form
    {
        private readonly Categoria _controller = new Categoria();

        private IEnumerable<dynamic> dataTable;

        private readonly Timer timer = new Timer(Configs.TimeLoading);
        private readonly BackgroundWorker workerBackground = new BackgroundWorker();

        public Categorias()
        {
            InitializeComponent();
            Eventos();

            switch (Home.CategoriaPage)
            {
                case "Produtos":
                    pictureBox1.Image = Resources.box;
                    label5.Text = @"Produtos";
                    label1.Text = @"Categorias";
                    label2.Text = @"Se organize melhor criando categorias para seus produtos.";
                    break;
                case "Receitas":
                    pictureBox1.Image = Resources.money_bag__1_;
                    label5.Text = @"Financeiro";
                    label1.Text = @"Nova Receita";
                    label2.Text = @"Se organize melhor criando os tipos de receitas para lançar no financeiro.";
                    break;
                case "Despesas":
                    pictureBox1.Image = Resources.money_bag__1_;
                    label5.Text = @"Financeiro";
                    label1.Text = @"Nova Despesa";
                    label2.Text = @"Se organize melhor criando os tipos de despesas para lançar no financeiro.";
                    break;
            }
        }

        public static int idCatSelected { get; set; }

        private void DataTableStart()
        {
            GridListaCategorias.Visible = false;
            Loading.Size = new Size(GridListaCategorias.Width, GridListaCategorias.Height);
            Loading.Visible = true;
            workerBackground.RunWorkerAsync();
        }

        private async void DataTable()
        {
            await _controller.SetTable(GridListaCategorias, null, search.Text);
        }

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

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                search.Select();
                DataTableStart();
            };

            Shown += (s, e) => search.Select();

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            Adicionar.Click += (s, e) => EditCategoria(true);
            Editar.Click += (s, e) => EditCategoria();
            GridListaCategorias.DoubleClick += (s, e) => EditCategoria();
            //GridListaCategorias.KeyDown += KeyDowns;

            //search.KeyDown += KeyDowns;
            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridListaCategorias.Visible = false;
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);

            using (var b = workerBackground)
            {
                b.DoWork += async (s, e) => { dataTable = await _controller.GetDataTable(); };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _controller.SetTable(GridListaCategorias, dataTable);

                    Loading.Visible = false;
                    GridListaCategorias.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker) delegate
            {
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
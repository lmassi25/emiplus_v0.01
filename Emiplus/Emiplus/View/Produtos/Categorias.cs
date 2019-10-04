using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class Categorias : Form
    {
        public static int idCatSelected { get; set; }
        private Controller.Categoria _controller = new Controller.Categoria();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
        public Categorias()
        {
            InitializeComponent();
            Eventos();

            var d = _controller.GetDataTable();
        }

        private void DataTable() => _controller.SetTable(GridListaCategorias, null, search.Text);

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

        private void Eventos()
        {
            Load += (s, e) => {
                search.Select();
                DataTable();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            Adicionar.Click += (s, e) => EditCategoria(true);
            Editar.Click += (s, e) => EditCategoria();
            GridListaCategorias.DoubleClick += (s, e) => EditCategoria(); 

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            using (var b = WorkerBackground)
            {
                b.DoWork += (s, e) =>
                {
                    //dataTable = _controller.GetDataTable();
                    //Thread.Sleep(1000);
                };

                b.RunWorkerCompleted += (s, e) =>
                {
                    //_controller.SetTable(GridListaProdutos, dataTable);

                    //Loading.Visible = false;
                    //GridListaProdutos.Visible = true;
                };
            }
        }
    }
}

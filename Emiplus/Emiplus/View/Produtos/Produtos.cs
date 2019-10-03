using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class Produtos : Form
    {
        public static int idPdtSelecionado { get; set; }
        private Controller.Item _controller = new Controller.Item();
        private IEnumerable<dynamic> dataTabela;
        private string pesquisa;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();
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

        private void DataTable() => _controller.SetTable(GridListaProdutos, null, search.Text);
        
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

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            using (var b = WorkerBackground)
            {
                b.DoWork += (s, e) =>
                {
                    dataTabela = _controller.GetDataTable(pesquisa);
                    Thread.Sleep(1500);
                };

                b.RunWorkerCompleted += (s, e) =>
                {
                    _controller.SetTable(GridListaProdutos, dataTabela);                     
                    
                    Loading.Visible = false;
                    GridListaProdutos.Visible = true;                    
                };
            }
               
        }
    }
}
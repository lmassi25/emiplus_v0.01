using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class Produtos : Form
    {
        public static int idPdtSelecionado { get; set; }
        private Controller.Item _controller = new Controller.Item();

        public Produtos()
        {
            InitializeComponent();
            Events();
        }

        private void DataTable() => _controller.GetDataTable(GridListaProdutos, search.Text);

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

        private void Events()
        {
            Load += (s, e) => {
                search.Select();
                DataTable();
            };

            btnAdicionar.Click += (s, e) => EditProduct(true);
            btnEditar.Click += (s, e) => EditProduct();
            GridListaProdutos.DoubleClick += (s, e) => EditProduct();

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
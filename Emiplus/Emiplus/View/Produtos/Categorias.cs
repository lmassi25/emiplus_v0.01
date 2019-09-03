using System;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Produtos
{
    public partial class Categorias : Form
    {
        public static int idCatSelected { get; set; }

        private Controller.Categoria _controller = new Controller.Categoria();

        public Categorias()
        {
            InitializeComponent();
        }

        public void DataTable()
        {
            _controller.GetDataTable(GridListaCategorias, search.Text);
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            idCatSelected = 0;
            OpenForm.Show<AddCategorias>(this);
        }

        private void Categorias_Load(object sender, EventArgs e)
        {
            DataTable();
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            DataTable();
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            idCatSelected = Convert.ToInt32(GridListaCategorias.SelectedRows[0].Cells["ID"].Value);
            OpenForm.Show<AddCategorias>(this);
        }

        private void GridListaCategorias_DoubleClick(object sender, EventArgs e)
        {
            idCatSelected = Convert.ToInt32(GridListaCategorias.SelectedRows[0].Cells["ID"].Value);
            OpenForm.Show<AddCategorias>(this);
        }

        private void Categorias_Activated(object sender, EventArgs e)
        {
            DataTable();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("http://google.com");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

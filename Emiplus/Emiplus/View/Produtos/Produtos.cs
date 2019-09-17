using Emiplus.Data.Helpers;
using Emiplus.View.Common;
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
        }

        private void Start()
        {
            ActiveControl = search;
        }

        private void DataTable()
        {
            _controller.GetDataTable(GridListaProdutos, search.Text);
        }

        private void LoadId()
        {
            if (GridListaProdutos.SelectedRows.Count > 0)
            {
                idPdtSelecionado = Convert.ToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddProduct>(this);
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            idPdtSelecionado = 0;
            OpenForm.Show<AddProduct>(this);
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Produtos_Load(object sender, EventArgs e)
        {
            Start();
            DataTable();
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            DataTable();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            LoadId();
        }

        /// <summary>
        /// Editar produto com double click
        /// </summary>
        private void GridListaProdutos_DoubleClick(object sender, EventArgs e)
        {
            LoadId();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }

        private void Search_Enter(object sender, EventArgs e)
        {
            DataTable();
        }
    }
}
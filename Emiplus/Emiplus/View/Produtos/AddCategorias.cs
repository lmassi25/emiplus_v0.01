using Emiplus.Data.Helpers;
using Emiplus.Model;
using System;
using System.Windows.Forms;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class AddCategorias : Form
    {
        private int idCatSelected = Categorias.idCatSelected;
        private Categoria _modelCategoria = new Categoria();

        public AddCategorias()
        {
            InitializeComponent();

            if (idCatSelected > 0)
                LoadData();

            // ToolTips
            ToolHelp.Show("Título identificador da categoria.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        private void LoadData()
        {
            _modelCategoria = _modelCategoria.FindById(idCatSelected).First<Categoria>();

            nome.Text = _modelCategoria.Nome;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("http://google.com");
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            _modelCategoria.Id = idCatSelected;
            _modelCategoria.Nome = nome.Text;

            _modelCategoria.Save(_modelCategoria);
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            var data = _modelCategoria.Remove(idCatSelected);
            if (data)
                Close();
        }
    }
}
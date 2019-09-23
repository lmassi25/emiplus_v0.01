using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddCategorias : Form
    {
        private int idCatSelected = Categorias.idCatSelected;
        private Categoria _modelCategoria = new Categoria();

        public AddCategorias()
        {
            InitializeComponent();
            Events();

            if (idCatSelected > 0)
            {
                _modelCategoria = _modelCategoria.FindById(idCatSelected).First<Categoria>();
                nome.Text = _modelCategoria.Nome;
            }
        }

        private void Events()
        {
            Load += (s, e) =>
            {
                nome.Select();

                ToolHelp.Show("Título identificador da categoria.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            };

            btnSalvar.Click += (s, e) =>
            {
                _modelCategoria.Id = idCatSelected;
                _modelCategoria.Nome = nome.Text;

                if (_modelCategoria.Save(_modelCategoria))
                    Close();
            };
            btnRemover.Click += (s, e) =>
            {
                var data = _modelCategoria.Remove(idCatSelected);
                if (data) Close();
            };

            btnExit.Click += (s, e) => Close();
            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
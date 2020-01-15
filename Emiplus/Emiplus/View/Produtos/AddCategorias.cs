using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.Drawing;
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
            Eventos();

            if (idCatSelected > 0)
            {
                _modelCategoria = _modelCategoria.FindById(idCatSelected).First<Categoria>();
                nome.Text = _modelCategoria.Nome ?? "";
            }

            if (Home.CategoriaPage == "Produtos")
            {
                pictureBox1.Image = Properties.Resources.box;
                label1.Text = "Nova Categoria";
                label5.Text = "Produtos";
            }

            if (Home.CategoriaPage == "Receitas")
            {
                pictureBox1.Image = Properties.Resources.money_bag__1_;
                label1.Text = "Nova Receita";
                label5.Text = "Financeiro";
            }

            if (Home.CategoriaPage == "Despesas")
            {
                pictureBox1.Image = Properties.Resources.money_bag__1_;
                label1.Text = "Nova Despesa";
                label5.Text = "Financeiro";
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += (s, e) =>
            {
                nome.Select();

                ToolHelp.Show("Digite o nome que deseja para a categoria.\nVocê pode cadastrar a Marca do produto como uma categoria por exemplo.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            };

            btnSalvar.Click += (s, e) =>
            {
                _modelCategoria.Id = idCatSelected;
                _modelCategoria.Nome = nome.Text;
                _modelCategoria.Tipo = Home.CategoriaPage;

                if (_modelCategoria.Save(_modelCategoria))
                    Close();
            };
            btnRemover.Click += (s, e) =>
            {
                var data = _modelCategoria.Remove(idCatSelected);
                if (data) Close();
            };

            nome.KeyPress += (s, e) => Masks.MaskOnlyNumberAndChar(s, e, 50);

            btnExit.Click += (s, e) => Close();
            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
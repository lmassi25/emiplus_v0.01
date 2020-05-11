using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class AddCategorias : Form
    {
        private readonly Categoria _modelCategoria = new Categoria();
        private readonly int idCatSelected = Categorias.idCatSelected;

        public AddCategorias()
        {
            InitializeComponent();
            Eventos();

            if (idCatSelected > 0)
            {
                _modelCategoria = _modelCategoria.FindById(idCatSelected).First<Categoria>();
                nome.Text = _modelCategoria.Nome ?? "";
            }

            switch (Home.CategoriaPage)
            {
                case "Produtos":
                    pictureBox1.Image = Resources.box;
                    label1.Text = @"Nova Categoria";
                    label5.Text = @"Produtos";
                    break;
                case "Receitas":
                    pictureBox1.Image = Resources.money_bag__1_;
                    label1.Text = @"Nova Receita";
                    label5.Text = @"Financeiro";
                    break;
                case "Despesas":
                    pictureBox1.Image = Resources.money_bag__1_;
                    label1.Text = @"Nova Despesa";
                    label5.Text = @"Financeiro";
                    break;
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
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                nome.Select();

                ToolHelp.Show(
                    "Digite o nome que deseja para a categoria.\nVocê pode cadastrar a Marca do produto como uma categoria por exemplo.",
                    pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            };

            btnSalvar.Click += (s, e) =>
            {
                _modelCategoria.Id = idCatSelected;
                _modelCategoria.Nome = nome.Text;
                _modelCategoria.Tipo = Home.CategoriaPage;

                if (!_modelCategoria.Save(_modelCategoria))
                    return;

                DialogResult = DialogResult.OK;
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

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
        }
    }
}
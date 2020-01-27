using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class AddNatureza : Form
    {
        public static int idSelected = Natureza.idSelected;
        private Model.Natureza _model = new Model.Natureza();

        public string btnSalvarText
        {
            get
            {
                return btnSalvar.Text;
            }
            set
            {
                btnSalvar.Text = value;
            }
        }

        public int btnSalvarWidth
        {
            get
            {
                return btnSalvar.Width;
            }
            set
            {
                btnSalvar.Width = value;
            }
        }

        public int btnSalvarLocation
        {
            get
            {
                return btnSalvar.Left;
            }
            set
            {
                btnSalvar.Left = value;
            }
        }

        public AddNatureza()
        {
            InitializeComponent();
            Eventos();

            if (idSelected > 0)
            {
                _model = _model.FindById(idSelected).First<Model.Natureza>();
                nome.Text = _model.Nome;
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

                ToolHelp.Show("Título identificador da categoria.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            };

            btnSalvar.Click += (s, e) =>
            {
                _model.Id = idSelected;
                _model.Nome = nome.Text;

                if (_model.Save(_model))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
            btnRemover.Click += (s, e) =>
            {
                var data = _model.Remove(idSelected);
                if (data) Close();
            };

            btnExit.Click += (s, e) => Close();
            label6.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using SqlKata.Execution;

namespace Emiplus.View.Food
{
    public partial class CadastrarMesa : Form
    {
        private Model.Mesas _mMesas = new Model.Mesas();

        public CadastrarMesa()
        {
            InitializeComponent();
            Eventos();
        }

        public static int IdMesa { get; set; }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Save();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Save()
        {
            var check = new Model.Mesas().FindAll().Where("id", "!=", IdMesa).WhereFalse("excluir")
                .Where("mesa", mesa.Text).FirstOrDefault<Model.Mesas>();
            if (check != null)
            {
                Alert.Message("Opps", "Já existe uma mesa com esse identificador.", Alert.AlertType.error);
                return;
            }

            if (string.IsNullOrEmpty(mesa.Text))
            {
                Alert.Message("Opps", "O identificador da mesa não pode ficar vazio", Alert.AlertType.error);
                return;
            }

            _mMesas.Mesa = mesa.Text;
            _mMesas.NrPessoas = Validation.ConvertToInt32(nrPessoas.Text);
            if (_mMesas.Save(_mMesas))
            {
                mesa.Text = "";
                nrPessoas.Text = "";

                Alert.Message("Pronto", "Mesa adicionada com sucesso.", Alert.AlertType.success);

                if (IdMesa > 0)
                    Close();

                mesa.Focus();
                return;
            }

            Alert.Message("Opps", "Erro ao adicionar mesa.", Alert.AlertType.error);
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            ToolHelp.Show("Título para identificar a Mesa.", pictureBox1, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("A quantidade de pessoas que cabem na mesa.", pictureBox5, ToolHelp.ToolTipIcon.Info,
                "Ajuda!");

            Shown += (s, e) =>
            {
                mesa.Focus();

                if (IdMesa > 0)
                {
                    _mMesas = _mMesas.FindById(IdMesa).WhereFalse("excluir").FirstOrDefault<Model.Mesas>();
                    if (_mMesas == null)
                        return;

                    label11.Text = @"Editar mesa";
                    mesa.Text = _mMesas.Mesa;
                    nrPessoas.Text = _mMesas.NrPessoas.ToString();
                }
                else
                {
                    IdMesa = 0;
                }
            };

            btnSalvar.Click += (s, e) => Save();

            btnDelete.Click += (s, e) =>
            {
                if (IdMesa <= 0)
                    return;

                if (_mMesas.Remove(IdMesa))
                {
                    Alert.Message("Pronto", "Mesa removida com sucesso.", Alert.AlertType.success);

                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                Alert.Message("Opps", "Erro ao remover mesa.", Alert.AlertType.error);
            };

            FormClosing += (s, e) => { DialogResult = DialogResult.OK; };
        }
    }
}
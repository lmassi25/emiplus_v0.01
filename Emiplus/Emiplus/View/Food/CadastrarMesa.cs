using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Food
{
    public partial class CadastrarMesa : Form
    {
        public static int idMesa { get; set; }

        private Model.Mesas _mMesas = new Model.Mesas();

        public CadastrarMesa()
        {
            InitializeComponent();
            Eventos();
        }

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
            Model.Mesas check = new Model.Mesas().FindAll().Where("id", "!=", idMesa).WhereFalse("excluir").Where("mesa", mesa.Text).FirstOrDefault<Model.Mesas>();
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
                    
                    if (idMesa > 0)
                        Close();

                    mesa.Focus();
                    return;
                }

                Alert.Message("Opps", "Erro ao adicionar mesa.", Alert.AlertType.error);
                return;
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            ToolHelp.Show("Título para identificar a Mesa.", pictureBox1, ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("A quantidade de pessoas que cabem na mesa.", pictureBox5, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            Shown += (s, e) =>
            {
                mesa.Focus();

                if (idMesa > 0) {
                    _mMesas = _mMesas.FindById(idMesa).WhereFalse("excluir").FirstOrDefault<Model.Mesas>();
                    if (_mMesas != null)
                    {
                        label11.Text = "Editar mesa";
                        mesa.Text = _mMesas.Mesa;
                        nrPessoas.Text = _mMesas.NrPessoas.ToString();
                    }
                } else
                    idMesa = 0;
            };

            btnSalvar.Click += (s, e) => Save();

            btnDelete.Click += (s, e) =>
            {
                if (idMesa > 0)
                {
                    if (_mMesas.Remove(idMesa))
                    {
                        Alert.Message("Pronto", "Mesa removida com sucesso.", Alert.AlertType.success);

                        DialogResult = DialogResult.OK;
                        Close();
                        return;
                    }

                    Alert.Message("Opps", "Erro ao remover mesa.", Alert.AlertType.error);
                    return;
                }
            };

            FormClosing += (s, e) =>
            {
                DialogResult = DialogResult.OK;
            };
        }
    }
}

using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddAdicional : Form
    {
        public static int Id { get; set; }

        private Model.ItemAdicional _mItemAdicional = new Model.ItemAdicional();
    
        public AddAdicional()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadData()
        {
            _mItemAdicional = _mItemAdicional.FindAll().WhereFalse("excluir").Where("id", Id).FirstOrDefault<Model.ItemAdicional>();
            if (_mItemAdicional is object)
            {
                title.Text = _mItemAdicional.Title ?? "";
                valor.Text = Validation.Price(_mItemAdicional.Valor);
            }
        }

        private void SaveData()
        {
            _mItemAdicional.Title = title.Text;
            _mItemAdicional.Valor = Validation.ConvertToDouble(valor.Text);
            if (_mItemAdicional.Save(_mItemAdicional))
            {
                Alert.Message("Pronto", "Adicional salvo com sucesso.", Alert.AlertType.success);
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            Alert.Message("Opps", "Algo deu errado ao salvar.", Alert.AlertType.error);
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

            Shown += (s, e) =>
            {
                Refresh();

                if (Id > 0)
                {
                    LoadData();
                }
                else
                {
                    _mItemAdicional.Id = 0;
                    if (_mItemAdicional.Save(_mItemAdicional))
                    {
                        Id = _mItemAdicional.GetLastId();
                        LoadData();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar.", Alert.AlertType.error);
                        Close();
                    }
                }
            };

            btnSalvar.Click += (s, e) => SaveData();

            btnRemover.Click += (s, e) =>
            {
                if (_mItemAdicional.Remove(Id))
                {
                    Alert.Message("Pronto", "Adicional removido com sucesso.", Alert.AlertType.success);
                    Close();
                }
            };

            valor.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
            btnExit.Click += (s, e) => Close();
        }
    }
}

using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddClienteContato : Form
    {
        private int IdPessoa = AddClientes.Id;
        private int IdContact = AddClientes.IdContact;
        private PessoaContato _modelContato = new PessoaContato();

        public AddClienteContato()
        {
            InitializeComponent();
            Events();

            if (!Validation.IsNumber(IdPessoa) && IdPessoa == 0)
            {
                Alert.Message("Opss", "Não foi possível, tente novamente.", Alert.AlertType.error);
                Close();
            }

            if (IdContact > 0)
            {
                _modelContato = _modelContato.FindById(IdContact).First<PessoaContato>();

                contato.Text = _modelContato.Contato;
                celular.Text = _modelContato.Celular;
                telefone.Text = _modelContato.Telefone;
                email.Text = _modelContato.Email;
            }
        }

        private new void Events()
        {
            btnContatoSalvar.Click += (s, e) =>
            {
                _modelContato.Id = IdContact;
                _modelContato.Id_pessoa = IdPessoa;
                _modelContato.Contato = contato.Text;
                _modelContato.Celular = celular.Text;
                _modelContato.Telefone = telefone.Text;
                _modelContato.Email = email.Text;

                if (_modelContato.Save(_modelContato))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
            btnContatoDelete.Click += (s, e) =>
            {
                var result = MessageBox.Show("Deseja realmente excluir o contato?", "Atenção!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (_modelContato.Remove(IdContact))
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            };

            FormClosing += (s, e) => DialogResult = DialogResult.OK;
        }
    }
}

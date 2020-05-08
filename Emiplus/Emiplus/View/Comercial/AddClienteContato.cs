using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class AddClienteContato : Form
    {
        private PessoaContato _modelContato = new PessoaContato();
        private readonly int IdContact = AddClientes.IdContact;
        private readonly int IdPessoa = AddClientes.Id;

        public AddClienteContato()
        {
            InitializeComponent();
            Eventos();
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
                if (!IdPessoa.IsNumber() && IdPessoa == 0)
                {
                    Alert.Message("Opss", "Não foi possível, tente novamente.", Alert.AlertType.error);
                    Close();
                }

                if (IdContact <= 0)
                    return;

                _modelContato = _modelContato.FindById(IdContact).First<PessoaContato>();

                contato.Text = _modelContato.Contato ?? "";
                celular.Text = _modelContato.Celular ?? "";
                telefone.Text = _modelContato.Telefone ?? "";
                email.Text = _modelContato.Email ?? "";
            };

            btnContatoSalvar.Click += (s, e) =>
            {
                _modelContato.Id = IdContact;
                _modelContato.Id_pessoa = IdPessoa;
                _modelContato.Contato = contato.Text;
                _modelContato.Celular = celular.Text;
                _modelContato.Telefone = telefone.Text;
                _modelContato.Email = email.Text;

                if (!_modelContato.Save(_modelContato))
                    return;

                DialogResult = DialogResult.OK;
                Close();
            };

            btnContatoDelete.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Deseja realmente excluir o contato?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                if (!result)
                    return;

                if (!_modelContato.Remove(IdContact))
                    return;

                DialogResult = DialogResult.OK;
                Close();
            };

            contato.KeyPress += (s, e) => Masks.MaskOnlyNumberAndChar(s, e, 50);
            telefone.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 13);
            celular.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 12);
            email.KeyPress += (s, e) => Masks.MaskMaxLength(s, e, 50);

            FormClosing += (s, e) => DialogResult = DialogResult.OK;
        }
    }
}
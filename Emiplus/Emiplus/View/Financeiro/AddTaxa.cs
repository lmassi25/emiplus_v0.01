using System;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using SqlKata.Execution;

namespace Emiplus.View.Financeiro
{
    public partial class AddTaxa : Form
    {
        public static int Id { get; set; }

        private Model.Taxas _mTaxas = new Model.Taxas();

        public AddTaxa()
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

            Shown += (s, e) =>
            {
                if (Id > 0)
                {
                    _mTaxas = _mTaxas.FindById(Id).FirstOrDefault<Model.Taxas>();
                    if (_mTaxas.Count() > 0)
                    {
                        txtTitulo.Text = _mTaxas.Nome;
                        txtTaxaCredito.Text = Validation.FormatPrice(_mTaxas.Taxa_Credito);
                        txtTaxaDebito.Text = Validation.FormatPrice(_mTaxas.Taxa_Debito);
                        txtTarifaFixa.Text = Validation.FormatPrice(_mTaxas.Taxa_Fixa);
                    }
                }
            };

            btnSalvar.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtTitulo.Text))
                {
                    Alert.Message("Opps", "Preencha o título", Alert.AlertType.error);
                    return;
                }

                if (string.IsNullOrEmpty(txtTaxaDebito.Text))
                {
                    Alert.Message("Opps", "Preencha a taxa débito", Alert.AlertType.error);
                    return;
                }

                if (string.IsNullOrEmpty(txtTaxaCredito.Text))
                {
                    Alert.Message("Opps", "Preencha a taxa crédito", Alert.AlertType.error);
                    return;
                }

                _mTaxas.Nome = txtTitulo.Text;
                _mTaxas.Taxa_Credito = Validation.ConvertToDouble(txtTaxaCredito.Text);
                _mTaxas.Taxa_Debito = Validation.ConvertToDouble(txtTaxaDebito.Text);
                _mTaxas.Taxa_Fixa = Validation.ConvertToDouble(txtTarifaFixa.Text);
                if (_mTaxas.Save(_mTaxas))
                {
                    Alert.Message("Pronto", "Gateway de pagamento adicionado com sucesso.", Alert.AlertType.success);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                    Alert.Message("Opps", "Algo deu errado ao salvar.", Alert.AlertType.error);
            };

            txtTarifaFixa.TextChanged += MaskPrice;
            txtTaxaCredito.TextChanged += MaskPrice;
            txtTaxaDebito.TextChanged += MaskPrice;

            btnExit.Click += (s, e) => Close();
            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }

        private void MaskPrice(object s, EventArgs e)
        {
            TextBox txt = (TextBox)s;
            Masks.MaskPrice(ref txt);
        }
    }
}

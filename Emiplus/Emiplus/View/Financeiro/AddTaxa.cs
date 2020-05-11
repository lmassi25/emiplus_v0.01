using System;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using SqlKata.Execution;

namespace Emiplus.View.Financeiro
{
    public partial class AddTaxa : Form
    {
        private Model.Taxas _mTaxas = new Model.Taxas();

        public AddTaxa()
        {
            InitializeComponent();
            Eventos();

            ToolHelp.Show("Em quantos dias o dinheiro vai estar disponível para você?", pictureBox4,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");
            ToolHelp.Show("Sem acréscimo para o COMPRADOR apartir da parcela selecionada.", pictureBox5,
                ToolHelp.ToolTipIcon.Info, "Ajuda!");
        }

        public static int Id { get; set; }

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
                        txtTaxaParcela.Text = Validation.FormatPrice(_mTaxas.Taxa_Parcela);
                        Parcelas.SelectedItem = $@"{_mTaxas.Parcela_Semjuros}° Parcela";
                        diasReceber.Text = _mTaxas.Dias_Receber.ToString();
                        txtTaxaAntecipacao.Text = Validation.FormatPrice(_mTaxas.Taxa_Antecipacao);
                        checkAntecipacaoAuto.Checked = _mTaxas.Antecipacao_Auto == 1;
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
                _mTaxas.Taxa_Parcela = Validation.ConvertToDouble(txtTaxaParcela.Text);
                _mTaxas.Parcela_Semjuros =
                    Validation.ConvertToInt32(Parcelas.SelectedItem.ToString().Replace("° Parcela", ""));
                _mTaxas.Dias_Receber = Validation.ConvertToInt32(diasReceber.Text);
                _mTaxas.Taxa_Antecipacao = Validation.ConvertToDouble(txtTaxaAntecipacao.Text);
                _mTaxas.Antecipacao_Auto = checkAntecipacaoAuto.Checked ? 1 : 0;
                if (_mTaxas.Save(_mTaxas))
                {
                    Alert.Message("Pronto", "Gateway de pagamento adicionado com sucesso.", Alert.AlertType.success);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    Alert.Message("Opps", "Algo deu errado ao salvar.", Alert.AlertType.error);
                }
            };

            txtTarifaFixa.TextChanged += MaskPrice;
            txtTaxaCredito.TextChanged += MaskPrice;
            txtTaxaDebito.TextChanged += MaskPrice;
            txtTaxaParcela.TextChanged += MaskPrice;
            txtTaxaAntecipacao.TextChanged += MaskPrice;
            diasReceber.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 2);

            btnExit.Click += (s, e) => Close();
            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
        }

        private void MaskPrice(object s, EventArgs e)
        {
            var txt = (TextBox) s;
            Masks.MaskPrice(ref txt);
        }
    }
}
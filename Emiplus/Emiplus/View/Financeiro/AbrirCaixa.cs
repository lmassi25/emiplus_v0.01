using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using System.Windows.Forms;
using System.Linq;
using SqlKata.Execution;

namespace Emiplus.View.Financeiro
{
    public partial class AbrirCaixa : Form
    {
        
        private Model.Caixa _modelCaixa = new Model.Caixa();

        public AbrirCaixa()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                Caixas.SelectedValue = 1;
                Caixas.Enabled = false;

                var caixas = new Model.Caixa().Query()
                .LeftJoin("USUARIOS", "USUARIOS.ID_USER", "CAIXA.USUARIO")
                .SelectRaw("USUARIOS.NOME, CAIXA.*")
                .Where("CAIXA.tipo", "Aberto")
                .WhereFalse("CAIXA.excluir")
                .OrderByDesc("CAIXA.criado")
                .Get();

                if (caixas.Count() > 0)
                {
                    Caixas.DataSource = caixas;
                    Caixas.DisplayMember = "NOME";
                    Caixas.ValueMember = "ID";
                } else
                {
                    OutroCaixa.Enabled = false;
                    Caixas.Enabled = false;
                    label3.Visible = true;
                }

            };

            OutroCaixa.Click += (s, e) =>
            {
                btnCriar.Text = "Vincular Caixa";
                EnableDisableCampos(false, true);
            };
            MeuCaixa.Click += (s, e) =>
            {
                btnCriar.Text = "Abrir Caixa";
                EnableDisableCampos(true, false);
            };

            btnCriar.Click += (s, e) =>
            {
                if (!MeuCaixa.Checked)
                {
                    if (OutroCaixa.Checked)
                    {
                        Home.idCaixa = Validation.ConvertToInt32(Caixas.SelectedValue);

                        Alert.Message("Pronto!", "Seu usuário foi vinculado ao caixa aberto.", Alert.AlertType.success);
                        DialogResult = DialogResult.OK;
                        Close();
                        return;
                    }
                }

                _modelCaixa.Tipo = "Aberto";
                _modelCaixa.Usuario = Settings.Default.user_id;
                _modelCaixa.Saldo_Inicial = Validation.ConvertToDouble(ValorInicial.Text);
                _modelCaixa.Terminal = Terminal.Text;
                _modelCaixa.Observacao = Obs.Text;

                if (_modelCaixa.Save(_modelCaixa))
                {
                    Home.idCaixa = _modelCaixa.GetLastId();
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            ValorInicial.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };
        }

        private void EnableDisableCampos(bool ED, bool CaixasBool)
        {
            Caixas.Enabled = CaixasBool;
            ValorInicial.Enabled = ED;
            Terminal.Enabled = ED;
            Obs.Enabled = ED;
        }
    }
}

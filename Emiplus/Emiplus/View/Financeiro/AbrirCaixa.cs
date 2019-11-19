using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using System.Windows.Forms;

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
            btnCriar.Click += (s, e) =>
            {
                _modelCaixa.Tipo = "Aberto";
                _modelCaixa.Usuario = Settings.Default.user_id;
                _modelCaixa.Saldo_Inicial = Validation.ConvertToDouble(ValorInicial.Text);
                _modelCaixa.Terminal = Terminal.Text;
                _modelCaixa.Observacao = Obs.Text;
                if (_modelCaixa.Save(_modelCaixa))
                {
                    Home.idCaixa = _modelCaixa.GetLastId();
                    Close();
                }
            };

            ValorInicial.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);
            };
        }
    }
}

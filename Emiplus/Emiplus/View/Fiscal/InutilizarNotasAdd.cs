using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class InutilizarNotasAdd : Form
    {
        public static int idNota { get; set; }
        private Model.Nota _nota = new Model.Nota();
        private Controller.Nota _cNota = new Controller.Nota();

        public InutilizarNotasAdd()
        {
            InitializeComponent();
            Eventos();
        }
        
        public void Eventos()
        {
            Load += (s, e) =>
            {
                if(idNota > 0)
                {
                    _nota = new Model.Nota().FindById(idNota).FirstOrDefault<Model.Nota>();

                    inicio.Text = _nota.nr_Nota.ToString();
                    final.Text = _nota.assinatura_qrcode.ToString();
                    serie.Text = _nota.Serie.ToString();
                    justificativa.Text = _nota.correcao.ToString();
                }

                inicio.Select();
            };

            btnSalvar.Click += (s, e) =>
            {
                if (justificativa.Text.Length < 15)
                {
                    Alert.Message("Ação não permitida", "Justificativa deve conter 15 caracteres", Alert.AlertType.warning);
                    return;
                }

                _nota.Id = idNota;
                _nota.Tipo = "Inutiliza";
                _nota.Status = "Transmitindo...";
                _nota.nr_Nota = inicio.Text;
                _nota.assinatura_qrcode = final.Text;
                _nota.Serie = serie.Text;
                _nota.correcao = justificativa.Text;
                _nota.correcao = Validation.CleanStringForFiscal(Validation.OneSpaceString(_nota.correcao));
                _nota.Save(_nota, true);

                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            justificativa.TextChanged += (s, e) =>
            {
                caracteres.Text = justificativa.Text.Length.ToString();
            };
        }
    }
}

using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Comercial
{
    public partial class ModalDividirValor : Form
    {
        public ModalDividirValor()
        {
            InitializeComponent();
            Eventos();
        }

        public static double Valor { get; set; }
        public static double ValorDivido { get; set; }
        public static double ValorRestante { get; set; }

        private void Eventos()
        {
            Shown += (s, e) => { txtValorItem.Text = Validation.FormatPrice(Valor, true); };

            txtDividir.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e, 2);

            txtDividir.TextChanged += (s, e) =>
            {
                double valor = 0;
                valor = Valor / Validation.ConvertToInt32(txtDividir.Text);
                ValorRestante = Valor - valor;
                ValorDivido = Validation.ConvertToDouble(valor);
                txtValorDividido.Text = Validation.FormatPrice(ValorDivido, true);
            };

            btnContinuar.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
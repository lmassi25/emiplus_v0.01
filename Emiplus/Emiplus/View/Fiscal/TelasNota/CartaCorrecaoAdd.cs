using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class CartaCorrecaoAdd : Form
    {
        private Model.Nota _modelNota = new Model.Nota();
        public static string tela { get; set; }
        public static string justificativa { get; set; }

        public CartaCorrecaoAdd()
        {
            InitializeComponent();
            Eventos();

            if (CartaCorrecaoAdd.tela == "Cancelar")
            {
                label12.Text = "Justificativa";
            }

            if (CartaCorrecaoAdd.tela == "Email")
            {
                label12.Text = "Emails (Utilize ; (ponto e virgula) para separar os emails)";
                caracteres.Visible = false;
            }
        }

        public void Eventos()
        {
            btnSalvar.Click += (s, e) =>
            {
                if(CartaCorrecaoAdd.tela == "Cancelar")
                {
                    if (correcao.Text.Length < 15)
                    {
                        Alert.Message("Ação não permitida", "Justificativa deve conter 15 caracteres", Alert.AlertType.warning);
                        return;
                    }

                    CartaCorrecaoAdd.justificativa = correcao.Text;

                    DialogResult = DialogResult.OK;
                    Close();

                    return;
                }

                if (CartaCorrecaoAdd.tela == "Email")
                {
                    CartaCorrecaoAdd.justificativa = correcao.Text;

                    DialogResult = DialogResult.OK;
                    Close();

                    return;
                }

                if (correcao.Text.Length < 15)
                {
                    Alert.Message("Ação não permitida", "Correção deve conter 15 caracteres", Alert.AlertType.warning);
                    return;
                }

                _modelNota.Id = 0;
                _modelNota.Tipo = "CCe";
                _modelNota.Status = "Transmitindo...";
                _modelNota.id_pedido = CartaCorrecao.idPedido;
                _modelNota.correcao = correcao.Text;

                _modelNota.correcao = Validation.CleanStringForFiscal(Validation.OneSpaceString(_modelNota.correcao));

                _modelNota.Save(_modelNota, false);

                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            correcao.TextChanged += (s, e) =>
            {
                caracteres.Text = correcao.Text.Length.ToString();
            };
        }
    }
}

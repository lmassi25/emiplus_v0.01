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

        public CartaCorrecaoAdd()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            btnSalvar.Click += (s, e) =>
            {
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
                DialogResult = DialogResult.OK;
                Close();
            };
        }
    }
}

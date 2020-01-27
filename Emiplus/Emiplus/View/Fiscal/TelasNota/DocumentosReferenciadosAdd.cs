using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class DocumentosReferenciadosAdd : Form
    {
        private Model.Nota _modelNota = new Model.Nota();

        public DocumentosReferenciadosAdd()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            btnSalvar.Click += (s, e) =>
            {
                _modelNota.Id = 0;
                _modelNota.Tipo = "Documento";
                _modelNota.id_pedido = Nota.Id;
                _modelNota.ChaveDeAcesso = correcao.Text;

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
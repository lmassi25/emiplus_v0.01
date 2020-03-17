using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddObservacao : Form
    {
        public static int idPedido { get; set; }

        private Model.Pedido _mPedido = new Model.Pedido();

        public AddObservacao()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                obs.Text = _mPedido.Observacao;
            };

            btnSalvar.Click += (s, e) =>
            {
                _mPedido.Observacao = obs.Text;
                _mPedido.Save(_mPedido);
                Alert.Message("Pronto", "Observação atualizado com sucesso.", Alert.AlertType.success);
                Close();
            };

            btnCancelar.Click += (s, e) =>
            {
                Close();
            };
        }
    }
}

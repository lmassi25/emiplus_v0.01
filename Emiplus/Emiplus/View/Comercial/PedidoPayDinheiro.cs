using Emiplus.Model;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPayDinheiro : Form
    {
        public static int IdPedido = AddPedidos.Id;

        private Model.Titulo _mPagamento = new Titulo();

        public PedidoPayDinheiro()
        {
            InitializeComponent();
        }

        private void AddPagamento()
        {
            var data = new Titulo()
            {

            };
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:

                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

    }
}

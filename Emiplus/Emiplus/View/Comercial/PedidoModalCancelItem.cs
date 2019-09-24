using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalCancelItem : Form
    {
        private int IdItem = AddPedidos.IdItem;
        public PedidoModalCancelItem()
        {
            InitializeComponent();
            Events();
        }

        private void CancelItem()
        {
            if (IdItem > 0)
            {
                Model.PedidoItem item = new Model.PedidoItem();

                item.Id = IdItem;
                item.Remove(IdItem);

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    CancelItem();
                    break;
            }
        }

        private void Events()
        {
            KeyDown += KeyDowns;
            nr.KeyDown += KeyDowns;
            btnContinuar.KeyDown += KeyDowns;

            btnContinuar.Click += (s, e) => CancelItem();
        }
    }
}

using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalCancelItem : Form
    {
        private int IdPedidoItem = AddPedidos.IdPedidoItem;

        public PedidoModalCancelItem()
        {
            InitializeComponent();
            Eventos();
        }

        private void CancelItem()
        {
            if (IdPedidoItem <= 0)
                return;

            Model.PedidoItem item = new Model.PedidoItem();
            item = item.FindById(IdPedidoItem).FirstOrDefault<Model.PedidoItem>();
            
            if(item.Tipo == "Produtos")
            {
                if (Home.pedidoPage != "Compras")
                    new Controller.Estoque(IdPedidoItem, Home.pedidoPage, "Botão Cancelar Produto").Add().Item();
                else
                    new Controller.Estoque(IdPedidoItem, Home.pedidoPage, "Botão Cancelar Produto").Remove().Item();
            }

            item.Remove(IdPedidoItem);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Enter:
                    CancelItem();
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            //KeyDown += KeyDowns;
            //nr.KeyDown += KeyDowns;
            //btnContinuar.KeyDown += KeyDowns;

            btnContinuar.Click += (s, e) => CancelItem();

            nr.KeyPress += (s, e) => Masks.MaskOnlyNumbers(s, e);
        }
    }
}

using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
using Estoque = Emiplus.Controller.Estoque;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalCancelItem : Form
    {
        private readonly int idPedidoItem = AddPedidos.IdPedidoItem;

        public PedidoModalCancelItem()
        {
            InitializeComponent();
            Eventos();
        }

        private void CancelItem()
        {
            if (idPedidoItem <= 0)
                return;

            var item = new PedidoItem();
            item = item.FindById(idPedidoItem).FirstOrDefault<PedidoItem>();

            if (item.Tipo == "Produtos")
            {
                if (Home.pedidoPage != "Compras")
                    new Estoque(idPedidoItem, Home.pedidoPage, "Botão Cancelar Produto").Add().Item();
                else
                    new Estoque(idPedidoItem, Home.pedidoPage, "Botão Cancelar Produto").Remove().Item();
            }

            item.Remove(idPedidoItem);

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
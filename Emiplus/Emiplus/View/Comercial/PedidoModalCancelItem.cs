using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalCancelItem : Form
    {
        private int IdItem = AddPedidos.IdItem;
        private Model.ItemEstoqueMovimentacao _mItemEstoque = new Model.ItemEstoqueMovimentacao();

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

                var dados = item.FindById(IdItem).First();

                var itemData = new Model.Item().FindById(dados.ITEM).First<Model.Item>();
                _mItemEstoque.SetUsuario(0)
                   .SetQuantidade(dados.QUANTIDADE)
                   .SetTipo("A")
                   .SetLocal("Tela de Vendas")
                   .SetObs("Item cancelado na Venda.")
                   .SetItem(itemData)
                   .Save(_mItemEstoque);

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

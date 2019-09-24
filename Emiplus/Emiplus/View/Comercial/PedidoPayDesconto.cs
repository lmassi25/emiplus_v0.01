using SqlKata.Execution;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPayDesconto : Form
    {
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();

        public static int idPedido;

        public PedidoPayDesconto()
        {
            InitializeComponent();
            Events();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:

                    break;
                case Keys.Escape:

                    break;
            }
        }

        private void FormulaDesconto(string total, int idItem)
        {
            var dataPedido = _mPedido.Query().Select("id", "total").Where("id", idPedido).First<Model.Pedido>();
            _mPedidoItens = _mPedidoItens.Query().Where("id", idItem).First<Model.PedidoItem>();

            var soma1 = Validation.Round((_mPedidoItens.Total * 100) / dataPedido.Total);
            var soma2 = Validation.Round(soma1 / 100);
            var soma3 = Validation.Round(Validation.ConvertToDouble(total) * soma2);

            _mPedidoItens.Id = idItem;
            _mPedidoItens.Tipo = "Produtos";
            _mPedidoItens.DescontoPedido = Validation.ConvertToDouble(soma3);
            _mPedidoItens.SomarDescontoTotal();
            _mPedidoItens.SomarTotal();
            _mPedidoItens.Save(_mPedidoItens);
        }

        private void Save()
        {
            if (idPedido > 0)
            {
                var data = _mPedidoItens.Query().Select("id", "total").Where("pedido", idPedido).Get();

                foreach (var item in data)
                {
                    FormulaDesconto(dinheiro.Text, item.ID);
                }

                _mPedido.Tipo = "Vendas";
                _mPedido = _mPedido.SaveTotais(_mPedidoItens.SumTotais(idPedido));
                _mPedido.Save(_mPedido);

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void Events()
        {
            KeyDown += KeyDowns; 
            btnSalvar.KeyDown += KeyDowns;
            btnCancelar.KeyDown += KeyDowns;

            btnSalvar.Click += (s, e) => Save();

            btnCancelar.Click += (s, e) => Close();
        }
    }
}

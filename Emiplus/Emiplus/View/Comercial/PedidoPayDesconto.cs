using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Windows.Forms;

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
            Eventos();

            porcentagem.Focus();
        }

        private void FormulaDesconto(string total, int idItem)
        {
            _mPedido = _mPedido.Query().Where("id", idPedido).FirstOrDefault<Model.Pedido>();
            _mPedidoItens = _mPedidoItens.Query().Where("id", idItem).First<Model.PedidoItem>();

            var soma1 = Validation.Round((_mPedidoItens.Total * 100) / _mPedido.Total);
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
                var data = _mPedidoItens.Query().Select("id", "total").Where("pedido", idPedido).Where("excluir", "0").Get();
                _mPedido = _mPedido.Query().Where("id", idPedido).FirstOrDefault<Model.Pedido>();

                string descontoValor = string.Empty;
                if (porcentagem.Text != string.Empty)
                {
                    descontoValor = (Validation.ConvertToDouble(porcentagem.Text) / 100 * (_mPedido.Total)).ToString();
                }
                else if (dinheiro.Text != string.Empty)
                {
                    descontoValor = dinheiro.Text;
                }

                foreach (var item in data)
                {
                    FormulaDesconto(descontoValor, item.ID);
                }

                _mPedido.Tipo = "Vendas";
                _mPedido = _mPedido.SaveTotais(_mPedidoItens.SumTotais(idPedido));
                _mPedido.Save(_mPedido);

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Save();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            //KeyDown += KeyDowns;
            //btnSalvar.KeyDown += KeyDowns;
            //btnCancelar.KeyDown += KeyDowns;
            //porcentagem.KeyDown += KeyDowns;
            //dinheiro.KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                Model.Pedido data = _mPedido.Query().Select("desconto").Where("id", idPedido).FirstOrDefault<Model.Pedido>();

                if (data != null)
                {
                    if (data.Desconto > 0)
                    {
                        dinheiro.Text = Validation.FormatPrice(data.Desconto);
                    }
                }
            };

            btnSalvar.Click += (s, e) => Save();
            porcentagem.TextChanged += (s, e) =>
            {
                if (porcentagem.Text != string.Empty)
                {
                    var dataPedido = _mPedido.Query().Select("id", "total").Where("id", idPedido).First<Model.Pedido>();
                    var dP = (Validation.ConvertToDouble(porcentagem.Text) / 100 * (dataPedido.Total));
                    valorPorcentagem.Text = Validation.FormatPrice(dP);
                }
            };

            porcentagem.KeyPress += (s, e) => Masks.MaskDouble(s, e);
            dinheiro.KeyPress += (s, e) => Masks.MaskDouble(s, e);
        }
    }
}
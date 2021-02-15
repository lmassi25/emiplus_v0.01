using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
using System.Linq;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPayDesconto : Form
    {
        public static int idPedido;
        private Model.Pedido _mPedido = new Model.Pedido();
        private PedidoItem _mPedidoItens = new PedidoItem();

        public PedidoPayDesconto()
        {
            InitializeComponent();
            Eventos();

            porcentagem.Focus();
        }

        private void FormulaDesconto(string total, int idItem)
        {
            _mPedido = _mPedido.Query().Where("id", idPedido).FirstOrDefault<Model.Pedido>();
            _mPedidoItens = _mPedidoItens.Query().Where("id", idItem).First<PedidoItem>();

            //decimal argument = (decimal)_mPedido.Total;
            //int qtdDecimal = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];
            var qtdDecimall = Validation.GetNumberOfDigits((decimal) _mPedido.Total);
            var qtdD = qtdDecimall + 1;

            qtdD = 2;

            var soma1 = Validation.Round(_mPedidoItens.Total * 100 / _mPedido.Total, qtdD);
            var soma2 = Validation.Round(soma1 / 100, qtdD);

            if(soma2 == 0)
                soma2 = Validation.Round(soma1 / 100, 3);

            if (soma2 == 0)
                soma2 = Validation.Round(soma1 / 100, 4);

            if (soma2 == 0)
                soma2 = Validation.Round(soma1 / 100, 5);

            var soma3 = Validation.Round(Validation.ConvertToDouble(total) * soma2, qtdD);

            _mPedidoItens.Id = idItem;
            _mPedidoItens.Tipo = "Produtos";
            _mPedidoItens.DescontoPedido = Validation.ConvertToDouble(soma3);
            _mPedidoItens.SomarDescontoTotal();
            _mPedidoItens.SomarTotal();
            _mPedidoItens.Save(_mPedidoItens);
        }

        private void FormulaDesconto2(string descontoTotal, int idItem, int countItens = 0)
        {
            _mPedido = _mPedido.Query().Where("id", idPedido).FirstOrDefault<Model.Pedido>();
            _mPedidoItens = _mPedidoItens.Query().Where("id", idItem).First<PedidoItem>();

            var soma3 = Validation.Round(Validation.ConvertToDouble(descontoTotal) / countItens, 2);

            _mPedidoItens.Id = idItem;
            _mPedidoItens.Tipo = "Produtos";
            _mPedidoItens.DescontoPedido = Validation.ConvertToDouble(soma3);
            _mPedidoItens.SomarDescontoTotal();
            _mPedidoItens.SomarTotal();
            _mPedidoItens.Save(_mPedidoItens);
        }

        private void ConfereDesconto(string desconto)
        {
            var _desconto = Validation.ConvertToDouble(desconto);
            var _itens = _mPedidoItens.Query().SelectRaw("SUM(desconto) as TOTAL").Where("pedido", idPedido).Where("excluir", "0").FirstOrDefault();
            //return (double)Validation.ConvertToDouble(sum.TOTAL);

            if (Validation.ConvertToDouble(_itens.TOTAL) != _desconto)
            {
                var diff = Validation.Round(_desconto - Validation.ConvertToDouble(_itens.TOTAL));
                //var _item_max = _mPedidoItens.Query().SelectRaw("MAX(total) as TOTAL").Where("pedido", idPedido).Where("excluir", "0").FirstOrDefault();
                //var _item = _mPedidoItens.Query().Where("pedido", idPedido).Where("total", "=",  Validation.ConvertToDouble(_item_max.TOTAL)).Where("excluir", "0").Limit(1).First<PedidoItem>();
                //var _item = _mPedidoItens.Query().Where("pedido", idPedido).Where("total", "=", Validation.ConvertToDouble(_item_max.TOTAL)).Where("excluir", "0").Limit(1).FirstOrDefault<PedidoItem>();
                //var _item_value = Validation.ConvertToDouble(_item_max.TOTAL);
                var _item = _mPedidoItens.Query().Where("pedido", idPedido).Where("excluir", "0").First<PedidoItem>();

                if(_item.Total > diff)
                {
                    _item.DescontoPedido = Validation.Round(_item.DescontoPedido + diff);
                    _item.SomarDescontoTotal();
                    _item.SomarTotal();
                    _item.Save(_item);
                }                
            }
        }

        private void Save()
        {
            if (idPedido > 0)
            {
                var data = _mPedidoItens.Query().Select("id", "total").Where("pedido", idPedido).Where("excluir", "0").Get();
                int count = data.Count();

                //var count = _mPedidoItens.Query().Select("id").Where("pedido", idPedido).Where("excluir", "0").AsCount();

                _mPedido = _mPedido.Query().Where("id", idPedido).FirstOrDefault<Model.Pedido>();

                var descontoValor = string.Empty;
                if (!string.IsNullOrEmpty(porcentagem.Text))
                    descontoValor = Validation.Round(Validation.ConvertToDouble(porcentagem.Text) / 100 * _mPedido.Total).ToString();
                else if (!string.IsNullOrEmpty(dinheiro.Text)) descontoValor = dinheiro.Text;

                foreach (var item in data)
                {
                    if (count > 20)
                        FormulaDesconto2(descontoValor, item.ID, count);
                    else
                        FormulaDesconto(descontoValor, item.ID);
                }

                ConfereDesconto(descontoValor);

                //_mPedido.Tipo = "Vendas";
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

            Load += (s, e) =>
            {
                var data = _mPedido.Query().Select("desconto").Where("id", idPedido).FirstOrDefault<Model.Pedido>();
                if (data == null)
                    return;

                if (data.Desconto > 0)
                    dinheiro.Text = Validation.FormatPrice(data.Desconto);
            };

            btnSalvar.Click += (s, e) => Save();
            porcentagem.TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(porcentagem.Text))
                    return;

                var dataPedido = _mPedido.Query().Select("id", "total").Where("id", idPedido).First<Model.Pedido>();
                var dP = Validation.ConvertToDouble(porcentagem.Text) / 100 * dataPedido.Total;
                valorPorcentagem.Text = Validation.FormatPrice(dP);
            };

            porcentagem.KeyPress += (s, e) => Masks.MaskDouble(s, e);
            dinheiro.KeyPress += (s, e) => Masks.MaskDouble(s, e);
        }
    }
}
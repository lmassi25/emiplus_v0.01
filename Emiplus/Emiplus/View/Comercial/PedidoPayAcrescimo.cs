using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPayAcrescimo : Form
    {
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItens = new Model.PedidoItem();

        public static int idPedido { get; set; }

        public PedidoPayAcrescimo()
        {
            InitializeComponent();
            Eventos();

            Frete.Focus();
        }

        private void FormulaFrete(string total, int idItem)
        {
            var dataPedido = _mPedido.Query().Select("id", "total").Where("id", idPedido).First<Model.Pedido>();
            _mPedidoItens = _mPedidoItens.Query().Where("id", idItem).First<Model.PedidoItem>();

            var soma1 = Validation.Round((_mPedidoItens.Total * 100) / dataPedido.Total);
            var soma2 = Validation.Round(soma1 / 100);
            var soma3 = Validation.Round(Validation.ConvertToDouble(total) * soma2);

            _mPedidoItens.Id = idItem;
            _mPedidoItens.Tipo = "Produtos";
            _mPedidoItens.Frete = Validation.ConvertToDouble(soma3);
            _mPedidoItens.SomarTotal();
            _mPedidoItens.Save(_mPedidoItens);
        }

        private void Save()
        {
            if (idPedido > 0)
            {
                _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                var data = _mPedidoItens.Query().Select("id", "total").Where("pedido", idPedido).Get();

                string freteValor = Frete.Text;

                foreach (var item in data)
                    FormulaFrete(freteValor, item.ID);

                _mPedido.Tipo = "Vendas";
                _mPedido.SaveTotais(_mPedidoItens.SumTotais(idPedido));
                if (_mPedido.Save(_mPedido))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
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
                Model.Pedido data = _mPedido.Query().Select("frete").Where("id", idPedido).FirstOrDefault<Model.Pedido>();
                if (data != null)
                {
                    if (data.Desconto > 0)
                        Frete.Text = Validation.FormatPrice(data.Frete);
                }

            };

            btnSalvar.Click += (s, e) => Save();            
            btnCancelar.Click += (s, e) => Close();

            Frete.KeyPress += (s, e) => Masks.MaskDouble(s, e);
        }
    }
}

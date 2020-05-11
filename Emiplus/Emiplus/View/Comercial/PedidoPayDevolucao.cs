using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPayDevolucao : Form
    {
        public static int idPedido;

        private readonly Controller.Pedido _controller = new Controller.Pedido();

        private Model.Pedido _mDevolucao = new Model.Pedido();
        private Model.Pedido _mPedido = new Model.Pedido();
        private PedidoItem _mPedidoItens = new PedidoItem();

        public PedidoPayDevolucao()
        {
            InitializeComponent();
            Eventos();
        }

        private void FormulaDevolucao(int idItem)
        {
            _mPedido = _mPedido.Query().Where("id", idPedido).FirstOrDefault<Model.Pedido>();
            _mPedidoItens = _mPedidoItens.Query().Where("id", idItem).First<PedidoItem>();

            var data = new Model.Pedido().Query().SelectRaw("SUM(PEDIDO.total) as total").Where("tipo", "Devoluções")
                .Where("excluir", "0").Where("venda", idPedido).FirstOrDefault<Model.Pedido>();

            if (data == null)
            {
                Alert.Message("Opss", "Problema para encontrar total da Troca", Alert.AlertType.warning);
                return;
            }

            var total = Validation.ConvertToDouble(data.Total);
            var soma1 = Validation.Round(_mPedidoItens.Total * 100 / _mPedido.Total);
            var soma2 = Validation.Round(soma1 / 100);
            var soma3 = Validation.Round(Validation.ConvertToDouble(total) * soma2);

            _mPedidoItens.Id = idItem;
            _mPedidoItens.DevolucaoPedido = Validation.ConvertToDouble(soma3);
            _mPedidoItens.SomarTotal();
            _mPedidoItens.Save(_mPedidoItens);
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(Voucher.Text))
            {
                Alert.Message("Ação não permitida", "Voucher inválido!", Alert.AlertType.warning);
                return;
            }

            _mDevolucao = _mDevolucao.FindByVoucher(Voucher.Text).FirstOrDefault<Model.Pedido>();
            if (_mDevolucao == null)
            {
                Alert.Message("Ação não permitida", "Voucher inválido!", Alert.AlertType.warning);
                return;
            }

            if (Validation.ConvertToInt32(_mDevolucao.Venda) > 0)
            {
                Alert.Message("Ação não permitida", "Voucher inválido!", Alert.AlertType.warning);
                return;
            }

            _mPedido = _mPedido.Query().Where("id", idPedido).FirstOrDefault<Model.Pedido>();

            if (_mPedido.Total < _mDevolucao.Total)
            {
                Alert.Message("Opss", "Valor da troca é maior que o vendido", Alert.AlertType.warning);
                return;
            }

            _mDevolucao.Venda = idPedido;
            if (_mDevolucao.Save(_mDevolucao))
            {
                var data = _mPedidoItens.Query().Select("id", "total").Where("pedido", idPedido).Where("excluir", "0").Get();

                foreach (var item in data) FormulaDevolucao(item.ID);

                _mPedido = _mPedido.SaveTotais(_mPedidoItens.SumTotais(idPedido));
                _mPedido.Save(_mPedido);

                DataTable();
                Voucher.Text = "";
            }
            else
            {
                Alert.Message("Opss", "Problema ao salvar Troca!", Alert.AlertType.warning);
            }
        }

        private void DataTable()
        {
            _controller.GetDataTableDevolucoes(GridDevolucoes, idPedido);
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

            Load += (s, e) => { DataTable(); };

            btnSalvar.Click += (s, e) =>
            {
                Save();
                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
        }
    }
}
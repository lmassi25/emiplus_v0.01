namespace Emiplus.Controller
{
    using Emiplus.Data.Helpers;
    using Emiplus.Properties;
    using SqlKata.Execution;
    using System.Linq;

    internal class Estoque
    {
        public int Id { get; set; }
        private int User { get; set; }
        private double Amount { get; set; }
        private string Obs { get; set; }
        private string Local { get; set; }
        private string Action { get; set; }
        private int idPedido { get; set; }

        private Model.ItemEstoqueMovimentacao _mItemEstoque = new Model.ItemEstoqueMovimentacao();

        public Estoque(int id, string local, string obs = "")
        {
            Id = id;
            User = Settings.Default.user_id;
            Local = local;
            Obs = obs;
        }

        public Estoque Add()
        {
            Action = "A";
            return this;
        }

        public Estoque Remove()
        {
            Action = "R";
            return this;
        }

        private Model.Item GetItem(int id)
        {
            return new Model.Item().FindById(id).First<Model.Item>();
        }

        private Model.PedidoItem GetPedidoItem(int id)
        {
            return new Model.PedidoItem().FindById(id).WhereFalse("excluir").FirstOrDefault<Model.PedidoItem>();
        }

        public void Pedido()
        {
            if (Id <= 0)
            {
                return;
            }

            var itens = new Model.PedidoItem().FindAll().Where("pedido", Id).WhereFalse("excluir").Get();

            foreach (var data in itens)
            {
                var item = GetItem(data.ITEM);
                Amount = Validation.ConvertToDouble(data.QUANTIDADE);

                _mItemEstoque.SetUsuario(User).SetQuantidade(Amount).SetTipo(Action).SetLocal(Local).SetObs(Obs).SetIdPedido(Id).SetItem(item).Save(_mItemEstoque);
            }
        }

        public void Item()
        {
            if (Id <= 0)
            {
                return;
            }

            var pedidoItem = GetPedidoItem(Id);
            var item = GetItem(pedidoItem.Item);
            Amount = Validation.ConvertToDouble(pedidoItem.Quantidade);

            _mItemEstoque.SetUsuario(User).SetQuantidade(Amount).SetTipo(Action).SetLocal(Local).SetObs(Obs).SetIdPedido(pedidoItem.Pedido).SetItem(item).Save(_mItemEstoque);
        }
    }
}
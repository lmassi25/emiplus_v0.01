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
            return new Model.Item().FindById(id).FirstOrDefault<Model.Item>();
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
            
                if (data.ATRIBUTO != "0") {
                    string attrId = data.ATRIBUTO;
                    Model.ItemEstoque upEstoque = new Model.ItemEstoque().FindAll().Where("id", attrId).FirstOrDefault<Model.ItemEstoque>();
                    if (upEstoque != null) {
                        if (Action == "A") {
                            upEstoque.Estoque += Amount;
                            if (Local != "Compras" || Local != "Devoluções")
                                upEstoque.Vendido -= Validation.ConvertToInt32(data.QUANTIDADE);
                        }

                        if (Action == "R") {
                            upEstoque.Estoque -= Amount;
                            if (Local != "Remessas" || Local != "Compras" || Local != "Devoluções")
                                upEstoque.Vendido += Validation.ConvertToInt32(data.QUANTIDADE);

                            if (Local == "Devoluções")
                                upEstoque.Vendido -= Validation.ConvertToInt32(data.QUANTIDADE);
                        }

                        upEstoque.Save(upEstoque);
                    }
                }
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
        
            Model.ItemEstoque upEstoque = new Model.ItemEstoque().FindAll().Where("id", pedidoItem.Atributo).FirstOrDefault<Model.ItemEstoque>();
            if (upEstoque != null) {
                if (Action == "A") {
                    upEstoque.Estoque += Amount;

                    if (Local != "Compras" && Local != "Devoluções")
                        upEstoque.Vendido -= Validation.ConvertToInt32(pedidoItem.Quantidade);
                }

                if (Action == "R") {
                    upEstoque.Estoque -= Amount;
                    if (Local != "Remessas" && Local != "Compras" && Local != "Devoluções")
                        upEstoque.Vendido += Validation.ConvertToInt32(pedidoItem.Quantidade);

                    if (Local == "Devoluções")
                        upEstoque.Vendido -= Validation.ConvertToInt32(pedidoItem.Quantidade);
                }

                upEstoque.Save(upEstoque);
            }
        }
    }
}
namespace Emiplus.Controller
{
    using Emiplus.Data.Helpers;

    public class Item : Data.Core.Controller
    {
        private Model.Item _model = new Model.Item();
        public Item() : base() { }

        public Model.Item GetItem(int Id)
        {
            return _model.GetItem(Id);
        }

        public bool Salvar(Model.Item data)
        {
            if (_model.ValidarDados(data))
                return false;

            if (_model.Salvar(data) == true)
            {
                alert.Message("Tudo certo!", "Produto salvo com sucesso.", Alert.AlertType.success);
                return true;
            }
            else
            {
                alert.Message("Opss", "Algo deu errado na hora de salvar o produto.", Alert.AlertType.error);
                return false;
            }
        }

        public string Deletar(Model.Item data)
        {
            string _msg = "";

            _model.Deletar(data);

            return _msg;
        }
    }
}

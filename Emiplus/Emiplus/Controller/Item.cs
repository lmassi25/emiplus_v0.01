namespace Emiplus.Controller
{
    using Emiplus.Data.Helpers;

    public class Item
    {
        private Model.Item _model;

        public Item()
        {
            _model = new Model.Item();
        }

        public Model.Item GetItem(int Id)
        {
            return _model.GetItem(Id);
        }

        public bool Salvar(Model.Item data)
        {
            if (_model.Salvar(data) == true)
            {
                Alert.Message("Tudo certo!", "Produto salvo com sucesso.", Alert.AlertType.success);
                return true;
            } else
            {
                Alert.Message("Opss", "Algo deu errado na hora de salvar o produto.", Alert.AlertType.error);
                return false;
            }
        }

        public string bDeletar(Model.Item data)
        {
            string _msg = "";

            _model.Deletar(data);

            return _msg;
        }    
    }
}

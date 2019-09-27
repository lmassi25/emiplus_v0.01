using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class DetailsPedidoPgtos : Form
    {
        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

        public static int IdPedido;

        public DetailsPedidoPgtos()
        {
            InitializeComponent();
            Eventos();
        }

        private void DataTable()
        {
            GridListaFormaPgtos.Rows.Clear();

            var titulos = new Model.Titulo();

            var data = titulos.Query()
                .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "formapgto.nome as formapgto")
                .Where("titulo.excluir", 0)
                .Where("titulo.id_pedido", IdPedido)
                .OrderByDesc("titulo.id")
                .Get();

            foreach (var item in data)
            {
                GridListaFormaPgtos.Rows.Add(
                    item.ID,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.VENCIMENTO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true)
                );
            }
        }

        private void Eventos()
        {
            Load += (s, e) => DataTable();
        }
    }
}

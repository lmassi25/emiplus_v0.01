using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Comercial
{
    public partial class DetailsPedidoPgtos : Form
    {
        public static int IdPedido;
        private readonly Titulo _controllerTitulo = new Titulo();

        public DetailsPedidoPgtos()
        {
            InitializeComponent();
            Eventos();
        }

        private void DataTable()
        {
            GridListaFormaPgtos.Rows.Clear();

            var data = _controllerTitulo.GetDataPgtosLancados(IdPedido);
            foreach (var item in data)
                GridListaFormaPgtos.Rows.Add(
                    item.ID,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.VENCIMENTO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true)
                );
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += (s, e) => DataTable();
        }
    }
}
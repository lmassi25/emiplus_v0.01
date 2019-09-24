using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class Pedido : Form
    {
        public static int IdPedido { get; set; }
        private Controller.Pedido _cPedido = new Controller.Pedido();

        public Pedido()
        {
            InitializeComponent();
            Events();

            dataInicial.Text = DateTime.Now.AddMonths(-1).ToString();
            dataFinal.Text = DateTime.Now.ToString();
        }

        private void Filter()
        {
            _cPedido.GetDataTablePedidos(GridLista, search.Text, dataInicial.Text, dataFinal.Text);
        }

        private void EditPedido(bool create = false)
        {
            if (create)
            {
                IdPedido = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                IdPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                DetailsPedido detailsPedido = new DetailsPedido();
                detailsPedido.ShowDialog();
            }
        }

        private void Events()
        {
            Load += (s, e) => Filter();
            search.TextChanged += (s, e) => Filter();
            btnSearch.Click += (s, e) => Filter();

            btnAdicionar.Click += (s, e) => EditPedido(true);
            btnEditar.Click += (s, e) => EditPedido();
            GridLista.DoubleClick += (s, e) =>EditPedido();

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}

using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class Pedido : Form
    {
        public static int IdPedido { get; set; }

        public Pedido()
        {
            InitializeComponent();
            Events();
        }

        private void Events()
        {
            btnAdicionar.Click += (s, e) =>
            {
                IdPedido = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();
        }
    }
}

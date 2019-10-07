using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using System.Drawing;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro.TelasNota
{
    public partial class TelaDados : Form
    {
        public TelaDados()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Next.Click += (s, e) =>
            {
                OpenForm.Show<TelaProdutos>(this);
            };

            SelecionarCliente.Click += (s, e) =>
            {
                PedidoModalClientes f = new PedidoModalClientes();
                f.ShowDialog();
            };
        }
    }
}

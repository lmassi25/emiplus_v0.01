using Emiplus.Data.Helpers;
using Emiplus.View.Fiscal;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaFiscalInicial : Form
    {
        public TelaFiscalInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            novaNFE.Click += (s, e) =>
            {
                Nota.disableCampos = false;
                Nota.Id = 0;
                Nota nota = new Nota();
                nota.ShowDialog();
            };

            nfe.Click += (s, e) =>
            {
                Home.pedidoPage = "Notas";
                Comercial.Pedido Pedido = new Comercial.Pedido();
                Pedido.ShowDialog();
            };

            naturezaOP.Click += (s, e) =>
            {
                OpenForm.Show<Fiscal.Natureza>(this);
            };

            clientes.Click += (s, e) =>
            {
                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            transportadoras.Click += (s, e) =>
            {
                Home.pessoaPage = "Transportadoras";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            impostos.Click += (s, e) =>
            {
                OpenForm.Show<Produtos.Impostos>(this);
            };
        }
    }
}

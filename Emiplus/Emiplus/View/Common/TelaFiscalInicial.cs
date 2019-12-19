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
                if (UserPermission.SetControl(novaNFE, pictureBox6, "fiscal_novanfe"))
                    return;

                Nota.disableCampos = false;
                Nota.Id = 0;
                Nota nota = new Nota();
                nota.ShowDialog();
            };

            nfe.Click += (s, e) =>
            {
                if (UserPermission.SetControl(nfe, pictureBox7, "fiscal_nfe"))
                    return;

                Home.pedidoPage = "Notas";
                Comercial.Pedido Pedido = new Comercial.Pedido();
                Pedido.ShowDialog();
            };

            naturezaOP.Click += (s, e) =>
            {
                if (UserPermission.SetControl(naturezaOP, pictureBox5, "fiscal_natop"))
                    return;

                OpenForm.Show<Fiscal.Natureza>(this);
            };

            clientes.Click += (s, e) =>
            {
                if (UserPermission.SetControl(clientes, pictureBox11, "fiscal_clientes"))
                    return;

                Home.pessoaPage = "Clientes";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                if (UserPermission.SetControl(fornecedores, pictureBox2, "fiscal_fornecedores"))
                    return;

                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            transportadoras.Click += (s, e) =>
            {
                if (UserPermission.SetControl(transportadoras, pictureBox3, "fiscal_transportadoras"))
                    return;

                Home.pessoaPage = "Transportadoras";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            impostos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(impostos, pictureBox4, "fiscal_impostos"))
                    return;

                OpenForm.Show<Produtos.Impostos>(this);
            };
        }
    }
}

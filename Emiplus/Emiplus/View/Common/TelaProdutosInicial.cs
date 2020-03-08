using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using Emiplus.View.Reports;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaProdutosInicial : Form
    {
        public TelaProdutosInicial()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Produtos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Produtos, pictureBox9, "pdt_pdt"))
                    return;

                OpenForm.Show<Produtos.Produtos>(this);
            };

            Servicos.Click += (s, e) =>
            {
                //if (UserPermission.SetControl(Produtos, pictureBox9, "pdt_pdt"))
                //    return;

                OpenForm.Show<Produtos.Servicos>(this);
            };

            Etiquetas.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Etiquetas, pictureBox2, "pdt_etiquetas"))
                    return;

                OpenForm.Show<Produtos.Etiquetas>(this);
            };

            Categorias.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Categorias, pictureBox1, "pdt_cats"))
                    return;

                Home.CategoriaPage = "Produtos";
                OpenForm.Show<Produtos.Categorias>(this);
            };

            Impostos.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Impostos, pictureBox3, "pdt_impostos"))
                    return;

                OpenForm.Show<Produtos.Impostos>(this);
            };

            fornecedores.Click += (s, e) =>
            {
                if (UserPermission.SetControl(fornecedores, pictureBox5, "pdt_fornecedores"))
                    return;

                Home.pessoaPage = "Fornecedores";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            transportadoras.Click += (s, e) =>
            {
                if (UserPermission.SetControl(transportadoras, pictureBox6, "pdt_transportadoras"))
                    return;

                Home.pessoaPage = "Transportadoras";
                OpenForm.Show<Comercial.Clientes>(this);
            };

            ReajusteProduto.Click += (s, e) =>
            {
                if (UserPermission.SetControl(ReajusteProduto, pictureBox7, "pdt_reajuste"))
                    return;

                OpenForm.Show<Produtos.ReajusteDeProduto>(this);
            };

            Compras.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Compras, pictureBox8, "pdt_novacompra"))
                    return;

                Home.pedidoPage = "Compras";
                Comercial.Pedido Pedido = new Comercial.Pedido();
                Pedido.ShowDialog();
            };

            HistoricoEntradaSaida.Click += (s, e) =>
            {
                if (UserPermission.SetControl(HistoricoEntradaSaida, pictureBox11, "pdt_entradassaidas"))
                    return;

                OpenForm.Show<EstoqueEntradaSaida>(this);
            };

            Estoque.Click += (s, e) =>
            {
                if (UserPermission.SetControl(this.Estoque, pictureBox12, "pdt_inventario"))
                    return;

                OpenForm.Show<Inventario>(this);
            };

            CompraNova.Click += (s, e) =>
            {
                if (UserPermission.SetControl(Compras, pictureBox13, "pdt_compras"))
                    return;

                Home.pedidoPage = "Compras";
                AddPedidos.Id = 0;
                AddPedidos NovoPedido = new AddPedidos();
                NovoPedido.ShowDialog();
            };

            importarNfe.Click += (s, e) =>
            {
                if (UserPermission.SetControl(importarNfe, pictureBox14, "pdt_importarnfe"))
                    return;

                Produtos.ImportarNfe f = new Produtos.ImportarNfe();
                f.ShowDialog();
            };
        }
    }
}
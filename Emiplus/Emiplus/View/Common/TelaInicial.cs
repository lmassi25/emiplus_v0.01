using SqlKata.Execution;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
            
            var Pedidos = new Model.Pedido().Query().SelectRaw("COUNT(ID) AS TOTAL").Where("tipo", "Vendas").WhereFalse("excluir").FirstOrDefault();
            totalVendas.Text = Pedidos.TOTAL.ToString();

            var Clientes = new Model.Pessoa().Query().SelectRaw("COUNT(ID) AS TOTAL").Where("tipo", "Clientes").WhereFalse("excluir").FirstOrDefault();
            totalClientes.Text = Clientes.TOTAL.ToString();

            var Estoque = new Model.Item().Query().SelectRaw("SUM(ESTOQUEATUAL) AS TOTAL").Where("tipo", "Produtos").WhereFalse("excluir").FirstOrDefault();
            totalEstoque.Text = Estoque.TOTAL.ToString();
        }
    }
}

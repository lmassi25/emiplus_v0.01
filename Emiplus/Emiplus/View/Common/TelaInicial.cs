using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
            
            var Pedidos = new Model.Pedido().Query().SelectRaw("COUNT(ID) AS TOTAL").Where("tipo", "Vendas").WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-7).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            totalVendas.Text = Pedidos.TOTAL.ToString();

            var Clientes = new Model.Pessoa().Query().SelectRaw("COUNT(ID) AS TOTAL").Where("tipo", "Clientes").WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-7).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            totalClientes.Text = Clientes.TOTAL.ToString();

            var Estoque = new Model.Item().Query().SelectRaw("SUM(ESTOQUEATUAL) AS TOTAL").Where("tipo", "Produtos").WhereFalse("excluir")
                .Where("criado", ">=", Validation.ConvertDateToSql(DateTime.Today.AddDays(-7).ToString(), true))
                .Where("criado", "<=", Validation.ConvertDateToSql(DateTime.Now.ToString(), true)).FirstOrDefault();
            totalEstoque.Text = Estoque.TOTAL == null ? "0" : Estoque.TOTAL.ToString();
        }
    }
}

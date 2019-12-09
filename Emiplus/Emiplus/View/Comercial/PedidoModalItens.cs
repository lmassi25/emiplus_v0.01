using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalItens : Form
    {
        public static string NomeProduto { get; set; }
        public static double ValorVendaProduto { get; set; }
        public static string txtSearch { get; set; }

        private Controller.Item _controllerItem = new Controller.Item();

        public PedidoModalItens()
        {
            InitializeComponent();
            Eventos();
        }

        private void SelectItemGrid()
        {
            if (GridListaProdutos.SelectedRows.Count > 0)
            {
                if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) > 0)
                {
                    DialogResult = DialogResult.OK;
                    NomeProduto = GridListaProdutos.SelectedRows[0].Cells["Descrição"].Value.ToString();
                    if(Home.pedidoPage == "Compras")
                        ValorVendaProduto = Validation.ConvertToDouble(GridListaProdutos.SelectedRows[0].Cells["Custo"].Value);
                    else
                        ValorVendaProduto = Validation.ConvertToDouble(GridListaProdutos.SelectedRows[0].Cells["Venda"].Value);
                    Close();
                }
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    //GridListaProdutos.Focus();
                    Support.UpDownDataGrid(false, GridListaProdutos);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;
                case Keys.Down:
                    //GridListaProdutos.Focus();
                    Support.UpDownDataGrid(true, GridListaProdutos);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F1:
                    buscarProduto.Focus();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F10:
                    SelectItemGrid();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Enter:
                    SelectItemGrid();
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += async (s, e) => await _controllerItem.SetTable(GridListaProdutos, null, buscarProduto.Text, 1);
            
            //KeyDown += KeyDowns;
            buscarProduto.Text = txtSearch;
            //buscarProduto.KeyDown += KeyDowns;
            //Selecionar.KeyDown += KeyDowns;
            //GridListaProdutos.KeyDown += KeyDowns;

            buscarProduto.TextChanged += async (s, e) => await _controllerItem.SetTable(GridListaProdutos, null, buscarProduto.Text, 1);

            Selecionar.Click += (s, e) => SelectItemGrid();
            btnCancelar.Click += (s, e) => Close();
        }
    }
}

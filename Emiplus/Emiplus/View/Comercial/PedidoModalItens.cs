using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalItens : Form
    {
        private readonly Item _controllerItem = new Item();

        public PedidoModalItens()
        {
            InitializeComponent();
            Eventos();
        }

        public static string NomeProduto { get; set; }
        public static double ValorVendaProduto { get; set; }
        public static string txtSearch { get; set; }

        private void SelectItemGrid()
        {
            if (GridListaProdutos.SelectedRows.Count <= 0)
                return;

            if (Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value) <= 0)
                return;

            DialogResult = DialogResult.OK;

            var item = new Model.Item()
                .FindById(Validation.ConvertToInt32(GridListaProdutos.SelectedRows[0].Cells["ID"].Value))
                .FirstOrDefault<Model.Item>();
            if (item != null)
            {
                NomeProduto = item.Nome;
                ValorVendaProduto = Home.pedidoPage == "Compras" ? item.ValorCompra : item.ValorVenda;
            }

            Close();
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
            Masks.SetToUpper(this);

            Load += async (s, e) =>
                await _controllerItem.SetTable(GridListaProdutos, null, buscarProduto.Text, 1, false, true);

            buscarProduto.Text = txtSearch;

            buscarProduto.TextChanged += async (s, e) =>
                await _controllerItem.SetTable(GridListaProdutos, null, buscarProduto.Text, 1, false, true);

            GridListaProdutos.CellDoubleClick += (s, e) => SelectItemGrid();
            Selecionar.Click += (s, e) => SelectItemGrid();
        }
    }
}
using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalItens : Form
    {
        public static string NomeProduto { get; set; }

        private string searchItemTexto = AddPedidos.searchItemTexto;
        private Controller.Item _controllerItem = new Controller.Item();

        public PedidoModalItens()
        {
            InitializeComponent();

            KeyDown += KeyDowns; // this form

            buscarProduto.Text = searchItemTexto;
            buscarProduto.KeyDown += KeyDowns;
            Selecionar.KeyDown += KeyDowns;
            GridListaProdutos.KeyDown += KeyDowns;
        }

        private void DataTable()
        {
            _controllerItem.GetDataTable(GridListaProdutos, buscarProduto.Text);
        }

        private void BuscarProduto_TextChanged(object sender, EventArgs e)
        {
            DataTable();
        }

        private void SelectItemGrid()
        {
            if (GridListaProdutos.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                NomeProduto = GridListaProdutos.SelectedRows[0].Cells["Descrição"].Value.ToString();
                Close();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(false, GridListaProdutos);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    GridListaProdutos.Focus();
                    Support.UpDownDataGrid(true, GridListaProdutos);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    Close();
                    break;
                case Keys.F1:
                    buscarProduto.Focus();
                    break;
                case Keys.F10:
                    SelectItemGrid();
                    break;
                case Keys.Enter:

                    if (Validation.Event(sender, GridListaProdutos))
                    {
                        MessageBox.Show(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                    }

                    if (Validation.Event(sender, buscarProduto))
                    {
                        GridListaProdutos.Focus();
                    }

                    break;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Selecionar_Click(object sender, EventArgs e)
        {
            SelectItemGrid();
        }
    }
}

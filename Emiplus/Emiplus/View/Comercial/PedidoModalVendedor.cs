using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalVendedor : Form
    {
        public static int Id { get; set; }

        private Controller.Pedido _controller = new Controller.Pedido();

        public PedidoModalVendedor()
        {
            InitializeComponent();

            this.KeyDown += KeyDowns;
            search.KeyDown += KeyDowns;
            GridListaVendedores.KeyDown += KeyDowns;
        }

        private void DataTable()
        {
            _controller.GetDataTablePessoa(GridListaVendedores, search.Text, "Colaboradores");
        }

        private void PedidoModalVendedor_Load(object sender, EventArgs e)
        {
            search.Select();
        }

        private void BtnCancelar_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void BtnSelecionar_Click(object sender, System.EventArgs e)
        {
            SelectItemGrid();
        }
        
        private void SelectItemGrid()
        {
            if (GridListaVendedores.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Id = Convert.ToInt32(GridListaVendedores.SelectedRows[0].Cells["ID"].Value);
                Close();
            }
        }

        private void Search_TextChanged(object sender, System.EventArgs e)
        {
            DataTable();
        }

        private void Search_Enter(object sender, EventArgs e)
        {
            DataTable();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridListaVendedores.Focus();
                    Support.UpDownDataGrid(false, GridListaVendedores);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    GridListaVendedores.Focus();
                    Support.UpDownDataGrid(true, GridListaVendedores);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    Close();
                    break;
                case Keys.F1:
                    search.Focus();
                    break;
                case Keys.F10:
                    SelectItemGrid();
                    break;
                case Keys.Enter:

                    // Pressione 'Enter', se estiver na Grid, seleciona colaborador
                    if (Validation.Event(sender, GridListaVendedores))
                    {
                        SelectItemGrid();
                    }

                    break;
            }
        }
    }
}

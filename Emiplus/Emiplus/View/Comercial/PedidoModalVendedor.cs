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
            Eventos();
        }

        private void DataTable() => _controller.GetDataTableColaboradores(GridListaVendedores, search.Text, "Colaboradores");

        private void SelectItemGrid()
        {
            if (GridListaVendedores.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Id = Convert.ToInt32(GridListaVendedores.SelectedRows[0].Cells["ID"].Value);
                Close();
            }
        }
        
        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridListaVendedores.Focus();
                    Support.UpDownDataGrid(false, GridListaVendedores);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;
                case Keys.Down:
                    GridListaVendedores.Focus();
                    Support.UpDownDataGrid(true, GridListaVendedores);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F1:
                    search.Focus();
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
            //KeyDown += KeyDowns;
            //search.KeyDown += KeyDowns;
            //GridListaVendedores.KeyDown += KeyDowns;

            Load += (s, e) => search.Select();
            btnSelecionar.Click += (s, e) => SelectItemGrid();
            
            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            btnCancelar.Click += (s, e) => Close();

            search.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);
        }
    }
}

using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalVendedor : Form
    {
        private readonly Controller.Pedido _controller = new Controller.Pedido();

        public PedidoModalVendedor()
        {
            InitializeComponent();
            Eventos();
        }

        public static int Id { get; set; }

        private void DataTable()
        {
            _controller.GetDataTableColaboradores(GridListaVendedores, search.Text, "Colaboradores");
        }

        private void SelectItemGrid()
        {
            if (GridListaVendedores.SelectedRows.Count <= 0) 
                return;

            DialogResult = DialogResult.OK;
            Id = Validation.ConvertToInt32(GridListaVendedores.SelectedRows[0].Cells["ID"].Value);

            Close();
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
            Masks.SetToUpper(this);

            Load += (s, e) => search.Select();
            btnSelecionar.Click += (s, e) => SelectItemGrid();

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            search.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);
        }
    }
}
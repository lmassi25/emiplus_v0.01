using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalTransportadora : Form
    {
        private readonly Controller.Pedido _controller = new Controller.Pedido();

        public PedidoModalTransportadora()
        {
            InitializeComponent();
            Eventos();
        }

        public static int Id { get; set; }
        public static string page { get; set; }

        private void DataTable()
        {
            _controller.GetDataTablePessoa(GridLista, search.Text, "Transportadoras");
        }

        private void SelectItemGrid()
        {
            if (GridLista.SelectedRows.Count <= 0)
                return;

            DialogResult = DialogResult.OK;
            Id = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);

            Close();
        }

        private void FormNovoCliente()
        {
            Clientes.Id = 0;
            Home.pessoaPage = "Transportadoras";
            using (var f = new AddClientes())
            {
                f.btnSalvarText = "Salvar e Inserir";
                f.btnSalvarWidth = 150;
                f.btnSalvarLocation = 590;
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterParent;
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Id = AddClientes.Id;
                    Clientes.Id = 0;
                    Close();
                }
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridLista.Focus();
                    Support.UpDownDataGrid(false, GridLista);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Down:
                    GridLista.Focus();
                    Support.UpDownDataGrid(true, GridLista);
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

                case Keys.F9:
                    FormNovoCliente();
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

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            Novo.Click += (s, e) => FormNovoCliente();
            btnSelecionar.Click += (s, e) => SelectItemGrid();

            search.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);
        }
    }
}
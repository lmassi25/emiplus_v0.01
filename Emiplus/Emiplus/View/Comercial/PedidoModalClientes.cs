using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalClientes : Form
    {
        public static int Id { get; set; }
        public static string page { get; set; }

        private Controller.Pedido _controller = new Controller.Pedido();

        public PedidoModalClientes()
        {
            InitializeComponent();
            Eventos();
        }

        private void DataTable() => _controller.GetDataTablePessoa(GridListaClientes, search.Text, "Clientes");

        private void SelectItemGrid()
        {
            if (GridListaClientes.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Id = Convert.ToInt32(GridListaClientes.SelectedRows[0].Cells["ID"].Value);
                Close();
            }
        }

        private void FormNovoCliente()
        {
            Id = 0;
            Home.pessoaPage = "Clientes";
            AddClientes f = new AddClientes();
            f.btnSalvarText = "Salvar e Inserir";
            f.btnSalvarWidth = 150;
            f.btnSalvarLocation = 590;
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog() == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                Id = AddClientes.Id;
                Close();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridListaClientes.Focus();
                    Support.UpDownDataGrid(false, GridListaClientes);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    GridListaClientes.Focus();
                    Support.UpDownDataGrid(true, GridListaClientes);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    Close();
                    break;
                case Keys.F1:
                    search.Focus();
                    break;
                case Keys.F9:
                    FormNovoCliente();
                    break;
                case Keys.F10:
                    SelectItemGrid();
                    break;
                case Keys.Enter:
                    if (Validation.Event(sender, GridListaClientes))
                        SelectItemGrid();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            //KeyDown += KeyDowns;
            //search.KeyDown += KeyDowns;
            //GridListaClientes.KeyDown += KeyDowns;
            //btnSelecionar.KeyDown += KeyDowns;
            //btnCancelar.KeyDown += KeyDowns;

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            NovoCliente.Click += (s, e) => FormNovoCliente();
            btnSelecionar.Click += (s, e) => SelectItemGrid();

            btnCancelar.Click += (s, e) => Close();

            search.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);
        }
    }
}

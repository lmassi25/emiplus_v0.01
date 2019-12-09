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

            switch (Home.pedidoPage)
            {
                case "Compras":
                    label11.Text = "Selecione o Fornecedor!";
                    label2.Text = "Buscar fornecedor (F1):";
                    NovoCliente.Text = "Fornecedor Novo ? (F9)";
                    label1.Text = "Fornecedores encontrados:";
                    break;
                default:
                    label11.Text = "Selecione o Cliente!";
                    label2.Text = "Buscar cliente (F1):";
                    NovoCliente.Text = "Cliente Novo ? (F9)";
                    label1.Text = "Clientes encontrados:";
                    break;
            }

            Eventos();
        }

        private void DataTable() 
        {
            switch (Home.pedidoPage)
            {
                case "Compras":
                    _controller.GetDataTablePessoa(GridListaClientes, search.Text, "Fornecedores");
                    break;
                default:
                    _controller.GetDataTablePessoa(GridListaClientes, search.Text, "Clientes");
                    break;
            }
        } 

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

            switch (Home.pedidoPage)
            {
                case "Compras":
                    Home.pessoaPage = "Fornecedores";
                    break;
                default:
                    Home.pessoaPage = "Clientes";
                    break;
            }
            
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
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;
                case Keys.Down:
                    GridListaClientes.Focus();
                    Support.UpDownDataGrid(true, GridListaClientes);
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

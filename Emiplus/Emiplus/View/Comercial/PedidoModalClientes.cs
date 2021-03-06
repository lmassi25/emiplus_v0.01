﻿using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalClientes : Form
    {
        private readonly Controller.Pedido _controller = new Controller.Pedido();

        public PedidoModalClientes()
        {
            InitializeComponent();
            Eventos();
        }

        public static int Id { get; set; }
        public static string page { get; set; }

        private void DataTable()
        {
            switch (Home.pedidoPage)
            {
                case "Delivery":
                    _controller.GetDataTablePessoa(GridListaClientes, search.Text, page == "Entregadores" ? "Entregadores" : "Clientes");
                    break;

                case "Compras":
                    _controller.GetDataTablePessoa(GridListaClientes, search.Text, "Fornecedores");
                    break;

                case "Notas":
                    _controller.GetDataTablePessoa(GridListaClientes, search.Text, "");
                    break;

                case null:
                    _controller.GetDataTablePessoa(GridListaClientes, search.Text, "");
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
                Id = Validation.ConvertToInt32(GridListaClientes.SelectedRows[0].Cells["ID"].Value);

                Close();
            }
        }

        private void FormNovoCliente()
        {
            Id = 0;

            switch (Home.pedidoPage)
            {
                case "Delivery":
                    Home.pessoaPage = page == "Entregadores" ? "Entregadores" : "Clientes";
                    break;

                case "Compras":
                    Home.pessoaPage = "Fornecedores";
                    break;

                default:
                    Home.pessoaPage = "Clientes";
                    break;
            }

            Clientes.Id = 0;
            using (var f = new AddClientes())
            {
                f.btnSalvarText = "Salvar e Inserir";
                f.btnSalvarWidth = 150;
                f.btnSalvarLocation = 590;
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterParent;
                f.btnAtivoLocation = 500;
                f.TopMost = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Id = AddClientes.Id;

                    Close();
                }
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
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                switch (Home.pedidoPage)
                {
                    case "Delivery":
                        if (page == "Entregadores")
                        {
                            label11.Text = @"Selecione o Entregador!";
                            label2.Text = @"Buscar entregador (F1):";
                            NovoCliente.Text = @"Entregador Novo ? (F9)";
                            label1.Text = @"Entregadores encontrados:";
                            pictureBox1.Image = Resources.deliveryman;
                        }
                        else
                        {
                            label11.Text = @"Selecione o Cliente!";
                            label2.Text = @"Buscar cliente (F1):";
                            NovoCliente.Text = @"Cliente Novo ? (F9)";
                            label1.Text = @"Clientes encontrados:";
                        }

                        break;

                    case "Compras":
                        label11.Text = @"Selecione o Fornecedor!";
                        label2.Text = @"Buscar fornecedor (F1):";
                        NovoCliente.Text = @"Fornecedor Novo ? (F9)";
                        label1.Text = @"Fornecedores encontrados:";
                        break;

                    default:
                        label11.Text = @"Selecione o Cliente!";
                        label2.Text = @"Buscar cliente (F1):";
                        NovoCliente.Text = @"Cliente Novo ? (F9)";
                        label1.Text = @"Clientes encontrados:";
                        break;
                }
            };

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            NovoCliente.Click += (s, e) => FormNovoCliente();
            btnSelecionar.Click += (s, e) => SelectItemGrid();
            GridListaClientes.CellDoubleClick += (s, e) => SelectItemGrid();

            search.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);
        }
    }
}
using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalItens : Form
    {
        private string searchItemTexto = AddPedidos.searchItemTexto;
        private Controller.Item _controllerItem = new Controller.Item();

        public PedidoModalItens()
        {
            InitializeComponent();

            buscarProduto.Text = searchItemTexto;

            Selecionar.Click += delegate
            {
                MessageBox.Show("Selecionar Item");
            };

            KeyDown += KeyDowns; // this form
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

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.F1:
                    buscarProduto.Focus();
                    break;
                case Keys.F10:
                    MessageBox.Show("selecionar o item");
                    break;
                case Keys.Enter:

                    if (Validation.Event(sender, GridListaProdutos))
                    {
                        MessageBox.Show(GridListaProdutos.SelectedRows[0].Cells["ID"].Value.ToString());
                    }

                    if (Validation.Event(sender, buscarProduto))
                    {
                        if (string.IsNullOrEmpty(buscarProduto.Text))
                        {
                            Alert.Message("Opss", "O campo de busca está vazio.", Alert.AlertType.info);
                            return;
                        }

                        GridListaProdutos.Focus();
                    }

                    break;
            }
        }

        private void Actions(object sender, EventArgs e)
        {

        }
    }       
}

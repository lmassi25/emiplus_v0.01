using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class Clientes : Form
    {
        public static int Id { get; set; }

        private Controller.Pessoa _controller = new Controller.Pessoa();
        public Clientes()
        {
            InitializeComponent();
            label1.Text = Home.pessoaPage + ":";
            label6.Text = Home.pessoaPage;

            if (Home.pessoaPage == "Fornecedores")
            {
                label2.Text = "Gerencie os Fornecedores da sua empresa aqui! Adicione, edite ou delete um Fornecedor.";
            }
            else if (Home.pessoaPage == "Transportadoras")
            {
                label2.Text = "Gerencie as Transportadoras da sua empresa aqui! Adicione, edite ou delete uma Transportadora.";
            }

        }

        private void LoadData()
        {
            _controller.GetDataTableClientes(GridLista, search.Text);
        }

        private void LoadId()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddClientes>(this);
            }
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            Id = 0;
            OpenForm.Show<AddClientes>(this);
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            LoadId();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            search.Select();
            LoadData();
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Clientes_Activated(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Search_Enter(object sender, EventArgs e)
        {
            LoadData();
        }

        private void GridLista_DoubleClick(object sender, EventArgs e)
        {
            LoadId();
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                Support.UpDownDataGrid(false, GridLista);
                e.Handled = true;
            }
            else if (e.KeyValue == 40)
            {
                Support.UpDownDataGrid(true, GridLista);
                e.Handled = true;
            }
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

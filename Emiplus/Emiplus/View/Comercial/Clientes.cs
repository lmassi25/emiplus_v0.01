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
            Eventos();

            label1.Text = Home.pessoaPage + ":";
            label6.Text = Home.pessoaPage;

            if (Home.pessoaPage == "Fornecedores")
                label2.Text = "Gerencie os Fornecedores da sua empresa aqui! Adicione, edite ou delete um Fornecedor.";
            else if (Home.pessoaPage == "Transportadoras")
                label2.Text = "Gerencie as Transportadoras da sua empresa aqui! Adicione, edite ou delete uma Transportadora.";
        }

        private void LoadData() => _controller.GetDataTableClientes(GridLista, search.Text);

        private void EditClientes(bool create = false)
        {
            if (create)
            {
                Id = 0;
                OpenForm.Show<AddClientes>(this);
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddClientes>(this);
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                search.Select();
                LoadData();
            };
            Activated += (s, e) => LoadData();

            search.TextChanged += (s, e) => LoadData();
            search.Enter += (s, e) => LoadData();
            search.KeyDown += (s, e) =>
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
            };

            btnAdicionar.Click += (s, e) => EditClientes(true);
            btnEditar.Click += (s, e) => EditClientes();
            GridLista.DoubleClick += (s, e) => EditClientes();

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();
            label8.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

namespace Emiplus.View.Comercial
{
    public partial class Clientes : Form
    {
        public static int Id { get; set; }
        private string page = TelaComercialInicial.page;
        private Controller.Pessoa _controller = new Controller.Pessoa();
        public Clientes()
        {
            InitializeComponent();
            label1.Text = page + ":";
            label6.Text = page;
            Console.WriteLine(page);
        }

        private void LoadData()
        {
            switch (page)
            {
                case "Clientes":
                    _controller.GetDataTableClientes(GridLista, search.Text);
                    break;
                case "Transportadores":
                    break;
                case "Fornecedores":
                    break;
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
            Support.OpenLinkBrowser("http://google.com");
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            Id = 0;
            OpenForm.Show<AddClientes>(this);
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            OpenForm.Show<AddClientes>(this);
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
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
    }
}

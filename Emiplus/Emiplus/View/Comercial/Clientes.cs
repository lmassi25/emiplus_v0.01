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

        private void LoadId()
        {
            if(GridLista.SelectedRows.Count > 0)
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

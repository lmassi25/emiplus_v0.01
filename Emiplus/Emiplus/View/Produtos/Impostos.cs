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

namespace Emiplus.View.Produtos
{
    public partial class Impostos : Form
    {
        public static int idImpSelected { get; set; }

        private Controller.Imposto _controller = new Controller.Imposto();

        public Impostos()
        {
            InitializeComponent();
        }

        private void DataTable()
        {
            _controller.GetDataTable(GridListaImpostos, search.Text);
        }

        private void Impostos_Load(object sender, EventArgs e)
        {
            search.Select();
            DataTable();
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            idImpSelected = 0;
            OpenForm.Show<AddImpostos>(this);
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            DataTable();
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            idImpSelected = Convert.ToInt32(GridListaImpostos.SelectedRows[0].Cells["ID"].Value);
            OpenForm.Show<AddImpostos>(this);
        }

        private void GridListaImpostos_DoubleClick(object sender, EventArgs e)
        {
            idImpSelected = Convert.ToInt32(GridListaImpostos.SelectedRows[0].Cells["ID"].Value);
            OpenForm.Show<AddImpostos>(this);
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Search_Enter(object sender, EventArgs e)
        {
            DataTable();
        }
    }
}

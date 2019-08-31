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
using Emiplus.View.Produtos;

namespace Emiplus.View.Comercial
{
    public partial class AddClientes : Form
    {
        private Controller.Pessoa _controller = new Controller.Pessoa();

        public AddClientes()
        {
            InitializeComponent();
        }

        private void DataTableAddress()
        {
            _controller.GetDataTableEnderecos(ListaEnderecos);
        }

        private void BtnAdicionarEndereco_Click(object sender, EventArgs e)
        {
            OpenForm.ShowInPanel<AddClienteEndereco>(panelEnderecos);
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdicionarContato_Click(object sender, EventArgs e)
        {
            OpenForm.ShowInPanel<AddClienteContato>(panelContatos);
        }

        private void AddClientes_Load(object sender, EventArgs e)
        {
            DataTableAddress();
        }

        private void AddClientes_Activated(object sender, EventArgs e)
        {
            DataTableAddress();
        }
    }
}

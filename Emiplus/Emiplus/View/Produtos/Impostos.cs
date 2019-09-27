using Emiplus.Data.Helpers;
using System;
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
            Eventos();
        }

        private void DataTable()
        {
            _controller.GetDataTable(GridListaImpostos, search.Text);
        }

        private void EditImposto(bool create = false)
        {
            if (create)
            {
                idImpSelected = 0;
                OpenForm.Show<AddImpostos>(this);
                return;
            }

            if (GridListaImpostos.SelectedRows.Count > 0)
            {
                idImpSelected = Convert.ToInt32(GridListaImpostos.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddImpostos>(this);
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                search.Select();
                DataTable();
            };

            label5.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            Adicionar.Click += (s, e) => EditImposto(true);
            Editar.Click += (s, e) => EditImposto();
            GridListaImpostos.DoubleClick += (s, e) => EditImposto(); 

            search.TextChanged += (s, e) => DataTable();
            search.Enter += (s, e) => DataTable();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}

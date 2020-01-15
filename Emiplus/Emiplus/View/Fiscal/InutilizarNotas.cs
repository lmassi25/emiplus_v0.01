using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class InutilizarNotas : Form
    {
        private Controller.Nota _cNota = new Controller.Nota();

        public InutilizarNotas()
        {
            InitializeComponent();
        }
        
        private void Filter()
        {
            _cNota.GetDataTableInutilizar(GridLista, status.Text, dataInicial.Text, dataFinal.Text);
        }

        private void Edit(bool create = false)
        {
            if (create)
            {
                //EditarTitulo.IdTitulo = 0;
                //OpenForm.Show<EditarTitulo>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                //EditarTitulo.IdTitulo = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                //OpenForm.Show<EditarTitulo>(this);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += (s, e) => Filter();

            btnAdicionar.Click += (s, e) => Edit(true);
            btnEditar.Click += (s, e) => Edit();
            GridLista.DoubleClick += (s, e) => Edit();
            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            //imprimir.Click += async (s, e) => await RenderizarAsync();
        }
    }
}

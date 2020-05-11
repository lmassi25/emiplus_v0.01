using System.Collections;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarPagamentos : Form
    {
        public static ArrayList titulos = new ArrayList();
        private readonly ImportarNfe dataNfe = new ImportarNfe();

        public ImportarPagamentos()
        {
            InitializeComponent();
            Eventos();
        }

        private void GetTitulos()
        {
            var dadosTitulos = dataNfe.GetNotas();
            foreach (dynamic item in dadosTitulos)
                SetTable(item.GetPagamentos(), item.GetDados().Id, item.GetDados().Nr);
        }

        private void SetTable(dynamic dataTitulos, string id = "", string nr = "")
        {
            GridLista.ColumnCount = 5;

            var checkColumn = new DataGridViewCheckBoxColumn();
            {
                checkColumn.HeaderText = @"Importar";
                checkColumn.Name = "Importar";
                checkColumn.FlatStyle = FlatStyle.Standard;
                checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
                checkColumn.Width = 60;
            }
            GridLista.Columns.Insert(0, checkColumn);

            GridLista.Columns[1].Name = "Forma de Pagamento";
            GridLista.Columns[1].Width = 120;

            GridLista.Columns[2].Name = "Data";
            GridLista.Columns[2].Width = 120;

            GridLista.Columns[3].Name = "Valor";
            GridLista.Columns[3].Width = 120;
            GridLista.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            GridLista.Columns[4].Name = "id";
            GridLista.Columns[4].Visible = false;

            GridLista.Columns[5].Name = "nr";
            GridLista.Columns[5].Visible = false;

            foreach (var item in dataTitulos)
                GridLista.Rows.Add(
                    true,
                    item.Tipo,
                    Validation.ConvertDateToForm(item.dateTime),
                    item.Valor,
                    id,
                    nr
                );

            GridLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Load += (s, e) => { GetTitulos(); };

            btnImportar.Click += (s, e) =>
            {
                titulos.Clear();

                var i = -1;
                foreach (DataGridViewRow item in GridLista.Rows)
                {
                    i++;
                    if ((bool) item.Cells["Importar"].Value)
                        titulos.Add(new
                        {
                            Ordem = i,
                            FormaPgto = item.Cells["Forma de Pagamento"].Value.ToString(),
                            Data = item.Cells["Data"].Value.ToString(),
                            Valor = item.Cells["Valor"].Value.ToString(),
                            id = item.Cells["id"].Value.ToString(),
                            nr = item.Cells["nr"].Value.ToString()
                        });
                }

                OpenForm.Show<ImportarCompraConcluido>(this);
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Importar")
                {
                    GridLista.SelectedRows[0].Cells["Importar"].Value = (bool) GridLista.SelectedRows[0].Cells["Importar"].Value == false;
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Importar")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Importar")
                    dataGridView.Cursor = Cursors.Default;
            };

            Back.Click += (s, e) => Close();
        }
    }
}
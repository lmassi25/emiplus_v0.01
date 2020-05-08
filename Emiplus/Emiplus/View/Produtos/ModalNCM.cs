using System.ComponentModel;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using RestSharp;

namespace Emiplus.View.Produtos
{
    public partial class ModalNCM : Form
    {
        private readonly BackgroundWorker backWork = new BackgroundWorker();

        public ModalNCM()
        {
            InitializeComponent();
            Eventos();
        }

        public static string NCM { get; set; }
        private dynamic Ncms { get; set; }
        private string SearchTxt { get; set; }

        private void DataTable()
        {
            GetDataTable(GridLista);
        }

        public void GetDataTable(DataGridView Table)
        {
            Table.ColumnCount = 2;

            Table.Columns[0].Name = "NCM";
            Table.Columns[0].Width = 60;

            Table.Columns[1].HeaderText = @"Descrição";
            Table.Columns[1].Name = "Descricao";
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Rows.Clear();

            var jo = Ncms;
            if (jo["error"] != null && jo["error"].ToString() != "")
            {
                Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                return;
            }

            foreach (var item in jo)
                Table.Rows.Add(
                    item.Value["codigo"],
                    item.Value["descricao"]
                );
        }

        private void SelectItemGrid()
        {
            if (GridLista.SelectedRows.Count <= 0)
                return;

            DialogResult = DialogResult.OK;
            NCM = GridLista.SelectedRows[0].Cells["NCM"].Value.ToString();

            Close();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridLista.Focus();
                    Support.UpDownDataGrid(false, GridLista);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Down:
                    GridLista.Focus();
                    Support.UpDownDataGrid(true, GridLista);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F1:
                    search.Focus();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F10:
                    SelectItemGrid();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Enter:
                    SelectItemGrid();
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Shown += (s, e) =>
            {
                Refresh();
                backWork.RunWorkerAsync();
            };

            Load += (s, e) => search.Select();
            btnSelecionar.Click += (s, e) => SelectItemGrid();

            buscar.Click += (s, e) =>
            {
                SearchTxt = search.Text;
                backWork.RunWorkerAsync();
            };

            search.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);

            backWork.DoWork += (s, e) =>
            {
                dynamic obj = new
                {
                    token = Program.TOKEN,
                    id_empresa = IniFile.Read("idEmpresa", "APP")
                };

                Ncms = new RequestApi().URL(Program.URL_BASE + $"/api/ncm&search={SearchTxt}").Content(obj, Method.POST).Response();
            };

            backWork.RunWorkerCompleted += (s, e) =>
            {
                label3.Visible = false;
                DataTable();
            };
        }
    }
}
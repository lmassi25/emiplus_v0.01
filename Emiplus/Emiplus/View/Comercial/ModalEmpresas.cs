using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class ModalEmpresas : Form
    {
        public static string Id { get; set; }
        public static string RazaoSocial { get; set; }

        public ModalEmpresas()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Razão Social";
            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "CNPJ";
            Table.Columns[2].Width = 130;
            Table.Columns[2].Visible = true;
        }

        private void LoadTable(DataGridView Table)
        {
            SetHeadersTable(GridLista);

            Table.Rows.Clear();

            if (Support.CheckForInternetConnection())
            {
                int idUser = Settings.Default.user_sub_user != 0 ? Settings.Default.user_sub_user : Settings.Default.user_id;
                var jo = new RequestApi().URL($"{Program.URL_BASE}/api/empresas/{Program.TOKEN}/{idUser}").Content().Response();

                if (jo["error"] != null && jo["error"].ToString() != "")
                {
                    Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                foreach (dynamic item in jo)
                {
                    if (item.Value.id_unique != Settings.Default.empresa_unique_id) {
                        Table.Rows.Add(
                            item.Value.id_unique,
                            item.Value.razao_social,
                            item.Value.cnpj
                            );
                    }
                }
            }
            else
            {
                Alert.Message("Opps", "Você precisa estar conectado a internet.", Alert.AlertType.error);
            }
            
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SelectItemGrid()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Id = GridLista.SelectedRows[0].Cells["ID"].Value.ToString();
                RazaoSocial = GridLista.SelectedRows[0].Cells["Razão Social"].Value.ToString();

                Close();
            }
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
            Masks.SetToUpper(this);

            Shown += (s, e) =>
            {
                Refresh();
                LoadTable(GridLista);
            };

            btnSelecionar.Click += (s, e) => SelectItemGrid();
            GridLista.CellDoubleClick += (s, e) => SelectItemGrid();
        }
    }
}

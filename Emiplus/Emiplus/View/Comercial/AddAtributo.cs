using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class AddAtributo : Form
    {
        public AddAtributo()
        {
            InitializeComponent();
            Eventos();
        }

        public static int IdProduto { get; set; }
        public static int IdAttr { get; set; }

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            table.Columns[0].Name = "ID";
            table.Columns[0].Visible = false;

            table.Columns[1].Name = "Atributo";
            table.Columns[1].Width = 150;
            table.Columns[1].Visible = true;

            table.Columns[2].Name = "Estoque";
            table.Columns[2].Width = 70;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Referencia";
            table.Columns[3].Width = 70;
            table.Columns[3].Visible = true;

            table.Columns[4].Name = "Código de Barras";
            table.Columns[4].Width = 130;
            table.Columns[4].Visible = true;
        }

        private void LoadAttr()
        {
            SetHeadersTable(GridLista);

            var itemEstoque = new ItemEstoque().FindAll().WhereFalse("excluir").Where("item", IdProduto)
                .Get<ItemEstoque>();
            if (itemEstoque != null)
                foreach (var attr in itemEstoque)
                    GridLista.Rows.Add(
                        attr.Id,
                        attr.Title,
                        attr.Estoque,
                        attr.Referencia,
                        attr.Codebarras
                    );
        }

        private void SelectItemGrid()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                IdAttr = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);

                DialogResult = DialogResult.OK;
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

                if (IdProduto < 0)
                {
                    Alert.Message("Opps", "Não localizamos o produto.", Alert.AlertType.error);
                    Close();
                    return;
                }

                GridLista.Focus();
                LoadAttr();
            };

            btnContinuar.Click += (s, e) => SelectItemGrid();
        }
    }
}
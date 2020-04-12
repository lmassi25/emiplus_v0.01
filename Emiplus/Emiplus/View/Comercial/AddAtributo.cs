using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddAtributo : Form
    {
        public static int idProduto { get; set; }
        public static int idAttr { get; set; }

        public AddAtributo()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Atributo";
            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Estoque";
            Table.Columns[2].Width = 70;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Referencia";
            Table.Columns[3].Width = 70;
            Table.Columns[3].Visible = true;

            Table.Columns[4].Name = "Código de Barras";
            Table.Columns[4].Width = 130;
            Table.Columns[4].Visible = true;
        }

        private void LoadAttr()
        {
            SetHeadersTable(GridLista);

            IEnumerable<Model.ItemEstoque> itemEstoque = new Model.ItemEstoque().FindAll().WhereFalse("excluir").Where("item", idProduto).Get<Model.ItemEstoque>();
            if (itemEstoque != null)
            {
                foreach (Model.ItemEstoque attr in itemEstoque)
                {
                    GridLista.Rows.Add(
                        attr.Id,
                        attr.Title,
                        attr.Estoque,
                        attr.Referencia,
                        attr.Codebarras
                        );
                }
            }
        }

        private void SelectItemGrid()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                idAttr = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);

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

                if (idProduto < 0)
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

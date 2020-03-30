using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata.Execution;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddItemMesa : Form
    {
        private Model.Item _mItem = new Model.Item();

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public AddItemMesa()
        {
            InitializeComponent();
            Eventos();
        }

        private void actionEnviar()
        {
            if (string.IsNullOrEmpty(nrMesa.Text))
            {
                Alert.Message("Oppss", "É necessário informar uma mesa", Alert.AlertType.warning);
                return;
            }

            
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Item";
            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Valor";
            Table.Columns[2].Width = 80;
            Table.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[2].Visible = true;

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                // Autocomplete de produtos
                collection = _mItem.AutoComplete("Produtos");
                BuscarProduto.AutoCompleteCustomSource = collection;
                
                SetHeadersTable(GridLista);
            };

            BuscarProduto.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Model.Item item = _mItem.FindById(collection.Lookup(BuscarProduto.Text)).FirstOrDefault<Model.Item>();
                    if (item != null)
                    {
                        GridLista.Rows.Add(
                            item.Id,
                            item.Nome,
                            Validation.FormatPrice(Validation.ConvertToDouble(item.ValorVenda))
                        );

                        BuscarProduto.Text = "";
                        BuscarProduto.Select();
                    }
                }
            };

            btnEnviar.Click += (s, e) => actionEnviar();

            btnCancel.Click += (s, e) => Close();
        }
    }
}

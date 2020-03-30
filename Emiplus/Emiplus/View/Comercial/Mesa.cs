using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class Mesa : Form
    {
        public static string nrMesa { get; set; }

        public Mesa()
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

            Table.Columns[1].Name = "Item";
            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Valor";
            Table.Columns[2].Width = 80;
            Table.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[2].Visible = true;
        }

        private async Task SetContentTableAsync(DataGridView Table)
        {
            SetHeadersTable(Table);

            Table.Rows.Clear();

            var Data = new Model.PedidoItem().FindAll().Where("mesa", nrMesa).WhereFalse("excluir").Where("pedido", 0).Get();
            if (Data != null) {
                foreach (dynamic item in Data)
                {
                    Table.Rows.Add(
                        item.ID,
                        item.XPROD,
                        Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true)
                    );
                }
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Shown += async (s, e) =>
            {
                label1.Text = $"Mesa: {nrMesa}";

                await SetContentTableAsync(GridLista);


                dynamic sumData = new Model.PedidoItem().Query().SelectRaw("sum(total) as total").Where("mesa", nrMesa).WhereFalse("excluir").Where("pedido", 0).FirstOrDefault();
                double total = Validation.ConvertToDouble(sumData.TOTAL) ?? 0;

                txtQtd.Text = GridLista.Rows.Count.ToString();
                txtTotal.Text = $"Valor Total: {Validation.FormatPrice(total, true)}";
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}

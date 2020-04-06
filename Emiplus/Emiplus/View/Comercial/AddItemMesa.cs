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
        private Model.PedidoItem _mPedidoItem = new Model.PedidoItem();

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

            
            if (GridLista.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in GridLista.Rows)
                {
                    int id = Validation.ConvertToInt32(row.Cells["ID"].Value);
                    Model.Item dataItem = _mItem.FindById(id).WhereFalse("excluir").FirstOrDefault<Model.Item>();
                    if (dataItem != null)
                    {
                        string obs = row.Cells["Observação"].Value.ToString();

                        _mPedidoItem.Id = 0;
                        _mPedidoItem.Tipo = "Produtos";
                        _mPedidoItem.Excluir = 0;
                        _mPedidoItem.Pedido = 0;
                        _mPedidoItem.Item = dataItem.Id;
                        _mPedidoItem.CEan = dataItem.CodeBarras;
                        _mPedidoItem.CProd = dataItem.Referencia;
                        _mPedidoItem.xProd = dataItem.Nome;
                        _mPedidoItem.ValorVenda = dataItem.ValorVenda;
                        _mPedidoItem.Total = dataItem.ValorVenda;
                        _mPedidoItem.Quantidade = 1;
                        _mPedidoItem.TotalVenda = dataItem.ValorVenda;
                        _mPedidoItem.Info_Adicional = obs;
                        _mPedidoItem.Mesa = nrMesa.Text;
                        _mPedidoItem.Status = "FAZENDO";
                        _mPedidoItem.Save(_mPedidoItem, false);
                    }
                }

                Alert.Message("Pronto", "Pedido enviado com sucesso.", Alert.AlertType.success);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 4;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Item";
            Table.Columns[1].Width = 150;
            Table.Columns[1].Visible = true;
            Table.Columns[1].ReadOnly = true;

            Table.Columns[2].Name = "Valor";
            Table.Columns[2].Width = 80;
            Table.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[2].Visible = true;
            Table.Columns[2].ReadOnly = true;

            Table.Columns[3].Name = "Observação";
            Table.Columns[3].Width = 100;
            Table.Columns[3].Visible = true;

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
                            Validation.FormatPrice(Validation.ConvertToDouble(item.ValorVenda)),
                            ""
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

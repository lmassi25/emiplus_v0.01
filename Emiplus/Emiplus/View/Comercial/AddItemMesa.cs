using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class AddItemMesa : Form
    {
        private Model.Item _mItem = new Model.Item();
        private Model.PedidoItem _mPedidoItem = new Model.PedidoItem();

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();
        public List<int> listProdutos = new List<int>();

        public AddItemMesa()
        {
            InitializeComponent();
            Eventos();
        }

        private void actionEnviar()
        {
            if (IniFile.Read("MesasPreCadastrada", "Comercial") == "False") {
                if (string.IsNullOrEmpty(nrMesa.Text))
                {
                    Alert.Message("Oppss", "É necessário informar uma mesa", Alert.AlertType.warning);
                    return;
                }
            }
            else
            {
                if(Mesas.SelectedValue == null)
                {
                    if(nrMesa.Text == "")
                    {
                        Alert.Message("Oppss", "É necessário informar uma mesa", Alert.AlertType.warning);
                        return;
                    }
                }
                else if (Mesas.SelectedValue.ToString() == "0")
                {
                    Alert.Message("Oppss", "É necessário informar uma mesa", Alert.AlertType.warning);
                    return;
                }
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

                        if (IniFile.Read("MesasPreCadastrada", "Comercial") == "True")
                            _mPedidoItem.Mesa = Mesas.Text;
                        else
                            _mPedidoItem.Mesa = nrMesa.Text;

                        _mPedidoItem.Status = "FAZENDO";
                        _mPedidoItem.Usuario = Settings.Default.user_id;
                        _mPedidoItem.Save(_mPedidoItem, false);

                        new Controller.Pedido().ImprimirItens(0, _mPedidoItem.GetLastId());
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

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Selecione";
            checkColumn.Name = "Selecione";
            checkColumn.FlatStyle = FlatStyle.Standard;
            checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
            checkColumn.Width = 60;
            Table.Columns.Insert(0, checkColumn);

            Table.Columns[1].Name = "ID";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Item";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;
            Table.Columns[2].ReadOnly = true;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].Width = 80;
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Visible = true;
            Table.Columns[3].ReadOnly = true;

            Table.Columns[4].Name = "Observação";
            Table.Columns[4].Width = 100;
            Table.Columns[4].Visible = true;

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SetHeadersTableProdutos(DataGridView Table)
        {
            Table.ColumnCount = 4;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Referência";
            Table.Columns[1].Width = 80;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Item";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;
            Table.Columns[2].ReadOnly = true;

            Table.Columns[3].Name = "Valor";
            Table.Columns[3].Width = 80;
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[3].Visible = true;
            Table.Columns[3].ReadOnly = true;

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.Image = Resources.success16x;
            img.Name = "Adicionar";
            img.Width = 60;
            img.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns.Add(img);

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadProdutos()
        {
            if (Validation.ConvertToInt32(Categorias.SelectedValue) == 0)
            {
                Alert.Message("Opps", "Selecione uma categoria válida.", Alert.AlertType.error);
                return;
            }

            GridProdutos.Rows.Clear();
            IEnumerable<Model.Item> itens = new Model.Item().FindAll().WhereFalse("excluir").Where("tipo", "Produtos").Where("categoriaid", Validation.ConvertToInt32(Categorias.SelectedValue)).Get<Model.Item>();
            if (itens.Count() > 0)
            {
                foreach (Model.Item item in itens)
                {
                    GridProdutos.Rows.Add(
                        item.Id,
                        item.Referencia,
                        item.Nome,
                        Validation.FormatPrice(Validation.ConvertToDouble(item.ValorVenda)),
                        Resources.plus20x
                        );
                }
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
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                Categorias.DataSource = new Categoria().GetAll("Produtos");;
                Categorias.DisplayMember = "Nome";
                Categorias.ValueMember = "Id";
            };

            Shown += (s, e) =>
            {
                // Autocomplete de produtos
                collection = _mItem.AutoComplete("Produtos");
                BuscarProduto.AutoCompleteCustomSource = collection;
                
                SetHeadersTable(GridLista);
                SetHeadersTableProdutos(GridProdutos);

                if (IniFile.Read("MesasPreCadastrada", "Comercial") == "True")
                {
                    nrMesa.Visible = false;
                    Mesas.Visible = true;

                    var listMesas = new ArrayList();
                    listMesas.Add(new { Id = "0", Nome = $"SELECIONE" });
                    IEnumerable<Model.Mesas> getMesas = new Model.Mesas().FindAll().WhereFalse("excluir").Get<Model.Mesas>();
                    if (getMesas.Count() > 0)
                        foreach (Model.Mesas mesas in getMesas)
                            listMesas.Add(new { Id = $"{mesas.Id}", Nome = $"{mesas.Mesa}" });

                    Mesas.DataSource = listMesas;
                    Mesas.DisplayMember = "Nome";
                    Mesas.ValueMember = "Id";
                }
            };

            BuscarProduto.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Model.Item item = _mItem.FindById(collection.Lookup(BuscarProduto.Text)).FirstOrDefault<Model.Item>();
                    if (item != null)
                    {
                        GridLista.Rows.Add(
                            false,
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

            btnFiltrar.Click += (s, e) => LoadProdutos();
            btnEnviar.Click += (s, e) => actionEnviar();

            GridProdutos.CellClick += (s, e) =>
            {
                if (GridProdutos.Columns[e.ColumnIndex].Name == "Adicionar")
                {
                    GridLista.Rows.Add(
                        false,
                        GridProdutos.SelectedRows[0].Cells["ID"].Value,
                        GridProdutos.SelectedRows[0].Cells["Item"].Value,
                        GridProdutos.SelectedRows[0].Cells["Valor"].Value,
                        ""
                    );

                    Alert.Message("Pronto", "Item adicionado.", Alert.AlertType.success);
                }
            };

            GridProdutos.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridProdutos.Columns[e.ColumnIndex].Name == "Adicionar")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridProdutos.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridProdutos.Columns[e.ColumnIndex].Name == "Adicionar")
                    dataGridView.Cursor = Cursors.Default;
            };

            btnRemover.Click += (s, e) =>
            {
                var toBeDeleted = new List<DataGridViewRow>();
                toBeDeleted.Clear();

                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar os PRODUTOS selecionados, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (DataGridViewRow item in GridLista.Rows) {
                        System.Console.WriteLine(item.Cells["Selecione"].Value);
                        if ((bool)item.Cells["Selecione"].Value == true) {
                            toBeDeleted.Add(item);
                        }
                    }

                    toBeDeleted.ForEach(d => GridLista.Rows.Remove(d));
                }

                btnRemover.Visible = false;
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        bool hideBtns = false;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool)item.Cells["Selecione"].Value == true)
                            {
                                hideBtns = true;
                            }

                        btnRemover.Visible = hideBtns;
                    }
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}

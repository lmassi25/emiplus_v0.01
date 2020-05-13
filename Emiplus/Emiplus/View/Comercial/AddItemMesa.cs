using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Food;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class AddItemMesa : Form
    {
        private readonly Item _mItem = new Item();
        private readonly PedidoItem _mPedidoItem = new PedidoItem();

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();
        public List<int> ListProdutos = new List<int>();

        public AddItemMesa()
        {
            InitializeComponent();
            Eventos();
        }

        private void ActionEnviar()
        {
            if (IniFile.Read("MesasPreCadastrada", "Comercial") == "False")
            {
                if (string.IsNullOrEmpty(nrMesa.Text))
                {
                    Alert.Message("Oppss", "É necessário informar uma mesa", Alert.AlertType.warning);
                    return;
                }
            }
            else
            {
                if (Mesas.SelectedValue == null)
                {
                    if (nrMesa.Text == "")
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
                    var id = Validation.ConvertToInt32(row.Cells["ID"].Value);
                    var dataItem = _mItem.FindById(id).WhereFalse("excluir").FirstOrDefault<Item>();
                    if (dataItem != null)
                    {
                        var obs = row.Cells["Observação"].Value.ToString();

                        _mPedidoItem.Id = 0;
                        _mPedidoItem.Tipo = "Produtos";
                        _mPedidoItem.Excluir = 0;
                        _mPedidoItem.Pedido = 0;
                        _mPedidoItem.Item = dataItem.Id;
                        _mPedidoItem.CEan = dataItem.CodeBarras;
                        _mPedidoItem.CProd = dataItem.Referencia;
                        _mPedidoItem.xProd = dataItem.Nome;
                        _mPedidoItem.ValorVenda = Validation.ConvertToDouble(row.Cells["Valor"].Value);
                        _mPedidoItem.Total = Validation.ConvertToDouble(row.Cells["Valor"].Value);
                        _mPedidoItem.Quantidade = 1;
                        _mPedidoItem.TotalVenda = Validation.ConvertToDouble(row.Cells["Valor"].Value);
                        _mPedidoItem.Info_Adicional = obs;
                        _mPedidoItem.Adicional = row.Cells["AddonSelected"].Value.ToString();
                        _mPedidoItem.Mesa = IniFile.Read("MesasPreCadastrada", "Comercial") == "True" ? Mesas.Text : nrMesa.Text;
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

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Selecione",
                Name = "Selecione",
                FlatStyle = FlatStyle.Standard,
                CellTemplate = new DataGridViewCheckBoxCell(),
                Width = 60
            };
            table.Columns.Insert(0, checkColumn);

            table.Columns[1].Name = "ID";
            table.Columns[1].Visible = false;

            table.Columns[2].Name = "Item";
            table.Columns[2].Width = 150;
            table.Columns[2].Visible = true;
            table.Columns[2].ReadOnly = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].Width = 80;
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[3].Visible = true;
            table.Columns[3].ReadOnly = true;

            table.Columns[4].Name = "Observação";
            table.Columns[4].Width = 100;
            table.Columns[4].Visible = true;

            table.Columns[5].Name = "AddonSelected";
            table.Columns[5].Width = 100;
            table.Columns[5].Visible = false;

            table.Columns[6].Name = "Unitario";
            table.Columns[6].Width = 80;
            table.Columns[6].Visible = false;

            var imgDividir = new DataGridViewImageColumn
            {
                Image = Resources.menu20x,
                Name = "Adicional",
                Width = 60,
                DefaultCellStyle = {Alignment = DataGridViewContentAlignment.MiddleCenter}
            };
            table.Columns.Add(imgDividir);

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SetHeadersTableProdutos(DataGridView table)
        {
            table.ColumnCount = 4;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            table.Columns[0].Name = "ID";
            table.Columns[0].Visible = false;

            table.Columns[1].Name = "Referência";
            table.Columns[1].Width = 80;
            table.Columns[1].Visible = true;

            table.Columns[2].Name = "Item";
            table.Columns[2].Width = 150;
            table.Columns[2].Visible = true;
            table.Columns[2].ReadOnly = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].Width = 80;
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[3].Visible = true;
            table.Columns[3].ReadOnly = true;

            var img = new DataGridViewImageColumn
            {
                Image = Resources.success16x,
                Name = "Adicionar",
                Width = 60,
                DefaultCellStyle = {Alignment = DataGridViewContentAlignment.MiddleCenter}
            };
            table.Columns.Add(img);

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadProdutos()
        {
            if (Validation.ConvertToInt32(Categorias.SelectedValue) == 0)
            {
                Alert.Message("Opps", "Selecione uma categoria válida.", Alert.AlertType.error);
                return;
            }

            GridProdutos.Rows.Clear();
            var itens = new Item().FindAll().WhereFalse("excluir").Where("tipo", "Produtos")
                .Where("categoriaid", Validation.ConvertToInt32(Categorias.SelectedValue)).Get<Item>();
            if (itens.Any())
                foreach (var item in itens)
                    GridProdutos.Rows.Add(
                        item.Id,
                        item.Referencia,
                        item.Nome,
                        Validation.FormatPrice(Validation.ConvertToDouble(item.ValorVenda)),
                        Resources.plus20x
                    );
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
                Categorias.DataSource = new Categoria().GetAll("Produtos");
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

                    var listMesas = new ArrayList {new {Id = "0", Nome = "SELECIONE"}};
                    var getMesas = new Model.Mesas().FindAll().WhereFalse("excluir").Get<Model.Mesas>();
                    if (getMesas.Any())
                        foreach (var mesas in getMesas)
                            listMesas.Add(new {Id = $"{mesas.Id}", Nome = $"{mesas.Mesa}"});

                    Mesas.DataSource = listMesas;
                    Mesas.DisplayMember = "Nome";
                    Mesas.ValueMember = "Id";
                }
            };

            BuscarProduto.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter)
                    return;

                var item = _mItem.FindById(collection.Lookup(BuscarProduto.Text)).FirstOrDefault<Item>();
                if (item == null)
                    return;

                GridLista.Rows.Add(
                    false,
                    item.Id,
                    item.Nome,
                    Validation.FormatPrice(Validation.ConvertToDouble(item.ValorVenda)),
                    "",
                    "",
                    Validation.ConvertToDouble(item.ValorVenda),
                    Resources.menu20x
                );

                BuscarProduto.Text = "";
                BuscarProduto.Select();
            };

            btnFiltrar.Click += (s, e) => LoadProdutos();
            btnEnviar.Click += (s, e) => ActionEnviar();

            GridProdutos.CellClick += (s, e) =>
            {
                if (GridProdutos.Columns[e.ColumnIndex].Name == "Adicionar")
                {
                    GridLista.Rows.Add(
                        false,
                        GridProdutos.SelectedRows[0].Cells["ID"].Value,
                        GridProdutos.SelectedRows[0].Cells["Item"].Value,
                        GridProdutos.SelectedRows[0].Cells["Valor"].Value,
                        "",
                        "",
                        GridProdutos.SelectedRows[0].Cells["Valor"].Value,
                        Resources.menu20x
                    );

                    Alert.Message("Pronto", "Item adicionado.", Alert.AlertType.success);
                }
            };

            GridProdutos.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridProdutos.Columns[e.ColumnIndex].Name == "Adicionar")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridProdutos.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridProdutos.Columns[e.ColumnIndex].Name == "Adicionar")
                    dataGridView.Cursor = Cursors.Default;
            };

            btnRemover.Click += (s, e) =>
            {
                var toBeDeleted = new List<DataGridViewRow>();
                toBeDeleted.Clear();

                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a deletar os PRODUTOS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (DataGridViewRow item in GridLista.Rows)
                    {
                        Console.WriteLine(item.Cells["Selecione"].Value);
                        if ((bool) item.Cells["Selecione"].Value) toBeDeleted.Add(item);
                    }

                    toBeDeleted.ForEach(d => GridLista.Rows.Remove(d));
                }

                btnRemover.Visible = false;
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool) GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        var hideBtns = false;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool) item.Cells["Selecione"].Value)
                                hideBtns = true;

                        btnRemover.Visible = hideBtns;
                    }
                }

                if (GridLista.Columns[e.ColumnIndex].Name == "Adicional")
                {
                    AdicionaisDispon.ValorAddon = 0;
                    AdicionaisDispon.AddonSelected = GridLista.SelectedRows[0].Cells["AddonSelected"].Value != null
                        ? GridLista.SelectedRows[0].Cells["AddonSelected"].Value.ToString()
                        : "";
                    AdicionaisDispon.IdPedidoItem = 0;
                    AdicionaisDispon.IdItem = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    var form = new AdicionaisDispon();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var getValor = Validation.ConvertToDouble(GridLista.SelectedRows[0].Cells["Unitario"].Value
                            .ToString().Replace("R$ ", ""));
                        GridLista.SelectedRows[0].Cells["Valor"].Value =
                            Validation.FormatPrice(getValor + AdicionaisDispon.ValorAddon);
                        GridLista.SelectedRows[0].Cells["AddonSelected"].Value = AdicionaisDispon.AddonSelected;
                    }
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione" ||
                    GridLista.Columns[e.ColumnIndex].Name == "Adicional")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione" ||
                    GridLista.Columns[e.ColumnIndex].Name == "Adicional")
                    dataGridView.Cursor = Cursors.Default;
            };
        }
    }
}
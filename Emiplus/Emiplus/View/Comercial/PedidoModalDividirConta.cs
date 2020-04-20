using Emiplus.Data.Helpers;
using Emiplus.Properties;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoModalDividirConta : Form
    {
        public static ArrayList itens { get; set; }
        public static double ValorDividido { get; set; }

        public PedidoModalDividirConta()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTableItens(DataGridView Table)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Referência";
            Table.Columns[1].Width = 80;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Item";
            Table.Columns[2].Width = 100;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Qtd.";
            Table.Columns[3].Width = 50;
            Table.Columns[3].Visible = true;

            Table.Columns[4].Name = "Valor";
            Table.Columns[4].Width = 90;
            Table.Columns[4].Visible = true;

            DataGridViewImageColumn imgDividir = new DataGridViewImageColumn();
            imgDividir.Image = Resources.divide;
            imgDividir.Name = "Dividir";
            imgDividir.Width = 60;
            imgDividir.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns.Add(imgDividir);

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.Image = Resources.success16x;
            img.Name = "Adicionar";
            img.Width = 60;
            img.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns.Add(img);

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SetHeadersTableSelecionados(DataGridView Table)
        {
            Table.ColumnCount = 5;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Referência";
            Table.Columns[1].Width = 80;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "Item";
            Table.Columns[2].Width = 100;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Qtd.";
            Table.Columns[3].Width = 50;
            Table.Columns[3].Visible = true;

            Table.Columns[4].Name = "Valor";
            Table.Columns[4].Width = 90;
            Table.Columns[4].Visible = true;

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.Image = Resources.error;
            img.Name = "Remover";
            img.Width = 60;
            img.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns.Add(img);

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadItens()
        {
            foreach (dynamic item in itens)
            {
                GridLista.Rows.Add(
                    item.Id,
                    item.CProd,
                    item.xProd,
                    item.Quantidade,
                    Validation.FormatPrice(Validation.ConvertToDouble(item.Total), true),
                    Resources.divide20x,
                    Resources.plus20x
                    );
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

            Shown += (s, e) =>
            {
                txtQtdItens.Text = "0";
                txtValor.Text = "R$ 00,00";
                
                SetHeadersTableItens(GridLista);
                SetHeadersTableSelecionados(GridListaSelecionados);
                LoadItens();
            };

            txtDinheiro.KeyPress += (s, e) => Masks.MaskDouble(s, e);

            txtDinheiro.TextChanged += (s, e) =>
            {
                TextBox txt = (TextBox)s;
                Masks.MaskPrice(ref txt);

                double troco = Validation.ConvertToDouble(txtDinheiro.Text) - Validation.ConvertToDouble(txtValor.Text.Replace("R$ ", ""));
                label6.Text = Validation.FormatPrice(troco, true);
            };

            btnContinuar.Click += (s, e) =>
            {
                itens.Clear();

                if (GridLista.Rows.Count > 0) {
                    foreach (DataGridViewRow row in GridLista.Rows)
                    {
                        itens.Add(new {
                            Id = row.Cells[0].Value,
                            CProd = row.Cells[1].Value,
                            xProd = row.Cells[2].Value,
                            Quantidade = row.Cells[3].Value,
                            Total = Validation.ConvertToDouble(row.Cells[4].Value.ToString().Replace("R$ ", ""))
                        });
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            };

            #region Table Itens
            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Dividir")
                {
                    ModalDividirValor.Valor = Validation.ConvertToDouble(GridLista.SelectedRows[0].Cells["Valor"].Value.ToString().Replace("R$ ", ""));
                    ModalDividirValor form = new ModalDividirValor();
                    form.TopMost = true;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        GridListaSelecionados.Rows.Add(
                            GridLista.SelectedRows[0].Cells["ID"].Value,
                            GridLista.SelectedRows[0].Cells["Referência"].Value,
                            GridLista.SelectedRows[0].Cells["Item"].Value,
                            GridLista.SelectedRows[0].Cells["Qtd."].Value,
                            Validation.FormatPrice(ModalDividirValor.ValorDivido, true),
                            Resources.error20x
                        );

                        RefreshTotal();
                    
                        if (ModalDividirValor.ValorRestante <= 0)
                            GridLista.Rows.RemoveAt(e.RowIndex);
                        else
                            GridLista.Rows[e.RowIndex].Cells[4].Value = Validation.FormatPrice(ModalDividirValor.ValorRestante, true);

                        Alert.Message("Pronto", "Item adicionado.", Alert.AlertType.success);
                    }
                }

                if (GridLista.Columns[e.ColumnIndex].Name == "Adicionar")
                {
                    GridListaSelecionados.Rows.Add(
                        GridLista.SelectedRows[0].Cells["ID"].Value,
                        GridLista.SelectedRows[0].Cells["Referência"].Value,
                        GridLista.SelectedRows[0].Cells["Item"].Value,
                        GridLista.SelectedRows[0].Cells["Qtd."].Value,
                        GridLista.SelectedRows[0].Cells["Valor"].Value,
                        Resources.error20x
                    );

                    RefreshTotal();

                    GridLista.Rows.RemoveAt(e.RowIndex);

                    Alert.Message("Pronto", "Item adicionado.", Alert.AlertType.success);
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Adicionar" || GridLista.Columns[e.ColumnIndex].Name == "Dividir")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridLista.Columns[e.ColumnIndex].Name == "Adicionar" || GridLista.Columns[e.ColumnIndex].Name == "Dividir")
                    dataGridView.Cursor = Cursors.Default;
            };
            #endregion

            #region Itens Selecionados
            GridListaSelecionados.CellClick += (s, e) =>
            {
                if (GridListaSelecionados.Columns[e.ColumnIndex].Name == "Remover")
                {
                    bool notFound = true;
                    foreach (DataGridViewRow row in GridLista.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == GridListaSelecionados.Rows[e.RowIndex].Cells[0].Value.ToString())
                        {
                            double valor = Validation.ConvertToDouble(row.Cells[4].Value.ToString().Replace("R$ ", ""));
                            double addValor = Validation.ConvertToDouble(GridListaSelecionados.Rows[e.RowIndex].Cells[4].Value.ToString().Replace("R$ ", ""));
                            row.Cells[4].Value = Validation.FormatPrice(valor + addValor, true);
                            notFound = false;
                            break;
                        }
                        else
                        {
                            notFound = true;
                        }
                    }

                    if (notFound)
                    {
                        GridLista.Rows.Add(
                            GridListaSelecionados.SelectedRows[0].Cells["ID"].Value,
                            GridListaSelecionados.SelectedRows[0].Cells["Referência"].Value,
                            GridListaSelecionados.SelectedRows[0].Cells["Item"].Value,
                            GridListaSelecionados.SelectedRows[0].Cells["Qtd."].Value,
                            GridListaSelecionados.SelectedRows[0].Cells["Valor"].Value,
                            Resources.divide20x,
                            Resources.plus20x
                        );
                    }

                    GridListaSelecionados.Rows.RemoveAt(e.RowIndex);

                    RefreshTotal();
                }
            };

            GridListaSelecionados.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridListaSelecionados.Columns[e.ColumnIndex].Name == "Remover")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridListaSelecionados.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = (s as DataGridView);
                if (GridListaSelecionados.Columns[e.ColumnIndex].Name == "Remover")
                    dataGridView.Cursor = Cursors.Default;
            };
            #endregion
        }

        private void RefreshTotal()
        {
            double sum = 0;
            for (int i = 0; i < GridListaSelecionados.Rows.Count; ++i)
                sum += Validation.ConvertToDouble(GridListaSelecionados.Rows[i].Cells[4].Value.ToString().Replace("R$ ", ""));
                    
            txtValor.Text = Validation.FormatPrice(sum, true);
            txtQtdItens.Text = GridListaSelecionados.Rows.Count.ToString();

            ValorDividido = sum;
        }
    }
}

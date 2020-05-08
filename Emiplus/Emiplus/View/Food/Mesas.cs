using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Food
{
    public partial class Mesas : Form
    {
        private readonly Model.Mesas _mMesas = new Model.Mesas();

        // Lista de ID para exclusão
        public List<int> ListMesas = new List<int>();

        public Mesas()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 3;

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

            table.Columns[2].Name = "Mesa";
            table.Columns[2].Width = 150;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Qtd. de Pessoas";
            table.Columns[3].Width = 130;
            table.Columns[3].Visible = true;
        }

        private void DataTable(string txtSearch = "")
        {
            GridLista.Rows.Clear();

            var likeSearch = $"%{txtSearch}%";
            var mesas = _mMesas.FindAll().WhereFalse("excluir").Where("mesa", "like", likeSearch).OrderByRaw("mesa ASC")
                .Get<Model.Mesas>();
            if (mesas.Any())
                foreach (var data in mesas)
                    GridLista.Rows.Add(
                        false,
                        data.Id,
                        data.Mesa,
                        data.NrPessoas
                    );

            GridLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Support.UpDownDataGrid(false, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    Support.UpDownDataGrid(true, GridLista);
                    e.Handled = true;
                    break;

                case Keys.Enter:
                    EditMesa();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void EditMesa(bool create = false)
        {
            if (create)
            {
                CadastrarMesa.IdMesa = 0;
                var form = new CadastrarMesa();
                if (form.ShowDialog() == DialogResult.OK)
                    DataTable();

                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                CadastrarMesa.IdMesa = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                var form = new CadastrarMesa();
                if (form.ShowDialog() == DialogResult.OK)
                    DataTable();
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

                if (IniFile.Read("MesasPreCadastrada", "Comercial") == "False")
                    visualPanel2.Visible = true;

                SetHeadersTable(GridLista);
                DataTable();
            };

            search.TextChanged += (s, e) => DataTable(search.Text);

            btnAdicionar.Click += (s, e) => EditMesa(true);
            btnEditar.Click += (s, e) => EditMesa();
            GridLista.DoubleClick += (s, e) => EditMesa();

            btnRemover.Click += (s, e) =>
            {
                ListMesas.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        ListMesas.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a deletar todas MESAS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in ListMesas)
                        new Model.Mesas().Remove(item);

                    DataTable();
                }

                btnRemover.Visible = false;
                btnEditar.Visible = true;
                btnAdicionar.Visible = true;
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool) GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditar.Visible = false;
                        btnAdicionar.Visible = false;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        var hideBtns = false;
                        var hideBtnsTop = true;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool) item.Cells["Selecione"].Value)
                            {
                                hideBtns = true;
                                hideBtnsTop = false;
                            }

                        btnRemover.Visible = hideBtns;
                        btnEditar.Visible = hideBtnsTop;
                        btnAdicionar.Visible = hideBtnsTop;
                    }
                }
            };

            GridLista.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Hand;
            };

            GridLista.CellMouseLeave += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;

                var dataGridView = s as DataGridView;
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                    dataGridView.Cursor = Cursors.Default;
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
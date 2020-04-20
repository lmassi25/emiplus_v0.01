using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Food
{
    public partial class Mesas : Form
    {
        // Lista de ID para exclusão
        public List<int> listMesas = new List<int>();

        private Model.Mesas _mMesas = new Model.Mesas();

        public Mesas()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

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

            Table.Columns[2].Name = "Mesa";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Qtd. de Pessoas";
            Table.Columns[3].Width = 130;
            Table.Columns[3].Visible = true;
        }

        private void DataTable(string txtSearch = "")
        {
            GridLista.Rows.Clear();

            string likeSearch = $"%{txtSearch}%";
            IEnumerable<Model.Mesas> mesas = _mMesas.FindAll().WhereFalse("excluir").Where("mesa", "like", likeSearch).OrderByRaw("mesa ASC").Get<Model.Mesas>();
            if (mesas.Count() > 0)
            {
                foreach (Model.Mesas data in mesas)
                {
                    GridLista.Rows.Add(
                        false,
                        data.Id,
                        data.Mesa,
                        data.NrPessoas
                    );
                }
            }

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
                CadastrarMesa.idMesa = 0;
                CadastrarMesa form = new CadastrarMesa();
                if (form.ShowDialog() == DialogResult.OK)
                    DataTable();

                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                CadastrarMesa.idMesa = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                CadastrarMesa form = new CadastrarMesa();
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
                listMesas.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool)item.Cells["Selecione"].Value == true)
                        listMesas.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar todas MESAS selecionados, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in listMesas)
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
                    if ((bool)GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnRemover.Visible = true;
                        btnEditar.Visible = false;
                        btnAdicionar.Visible = false;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        bool hideBtns = false;
                        bool hideBtnsTop = true;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool)item.Cells["Selecione"].Value == true)
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
            
            btnExit.Click += (s, e) => Close();
        }
    }
}

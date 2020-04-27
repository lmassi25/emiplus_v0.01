using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Financeiro
{
    public partial class Taxas : Form
    {
        private Model.Taxas _mTaxas = new Model.Taxas();

        public List<int> listTaxas = new List<int>();

        public Taxas()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 7;

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

            Table.Columns[2].Name = "Gateway";
            Table.Columns[2].Width = 120;
            Table.Columns[2].MinimumWidth = 120;

            Table.Columns[3].Name = "Tarifa Fixa";
            Table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Taxa Crédito";
            Table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Taxa Débito";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = "Taxa Parcelas";
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns[6].Width = 100;
            
            Table.Columns[7].Name = "Parcelas Sem Juros";
            Table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns[7].Width = 110;
        }

        private void SetContentTable(DataGridView Table)
        {
            Table.Rows.Clear();

            IEnumerable<Model.Taxas> dataTaxa = _mTaxas.FindAll().WhereFalse("excluir").Get<Model.Taxas>();
            if (dataTaxa.Count() > 0)
            {
                foreach (Model.Taxas item in dataTaxa)
                {
                    Table.Rows.Add(
                        false,
                        item.Id,
                        item.Nome,
                        Validation.FormatPrice(item.Taxa_Fixa),
                        $"{item.Taxa_Credito}%",
                        $"{item.Taxa_Debito}%",
                        $"{item.Taxa_Parcela}%",
                        item.Parcela_Semjuros
                    );
                }
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void EditarTaxa(bool create = false)
        {
            if (create)
            {
                AddTaxa.Id = 0;
                OpenForm.Show<AddTaxa>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                AddTaxa.Id = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddTaxa>(this);
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
            GridLista.Focus();

            Shown += (s, e) =>
            {
                Refresh();

                SetHeadersTable(GridLista);
                SetContentTable(GridLista);
            };

            GridLista.GotFocus += (s, e) => SetContentTable(GridLista);

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
            btnExit.Click += (s, e) => Close();

            btnAdicionar.Click += (s, e) => EditarTaxa(true);
            btnEditar.Click += (s, e) => EditarTaxa();

            btnRemover.Click += (s, e) =>
            {
                listTaxas.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool)item.Cells["Selecione"].Value == true)
                        listTaxas.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!", $"Você está prestes a deletar os itens selecionados, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in listTaxas)
                        new Model.Taxas().Remove(item);

                    SetContentTable(GridLista);
                }

                btnRemover.Visible = false;
                btnEditar.Enabled = true;
                btnAdicionar.Enabled = true;
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
        }
    }
}

using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
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

namespace Emiplus.View.Produtos
{
    public partial class Variacoes : Form
    {
        // Armazena o ID dos grupos para serem deletados.
        public List<int> listGrupos = new List<int>();

        public Variacoes()
        {
            InitializeComponent();
            Eventos();
        }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 2;

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

            Table.Columns[2].Name = "Grupo";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;
        }

        private void LoadData(DataGridView Table)
        {
            Table.Rows.Clear();

            IEnumerable<Model.ItemGrupo> grupos = new Model.ItemGrupo().FindAll().WhereFalse("excluir").Get<Model.ItemGrupo>();
            foreach (Model.ItemGrupo item in grupos)
            {
                Table.Rows.Add(
                    false,
                    item.Id,
                    item.Title
                );
            }
            
            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                SetHeadersTable(GridLista);
                LoadData(GridLista);
            };

            btnAdicionar.Click += (s, e) =>
            {
                AddVariacao.Id = 0;
                AddVariacao form = new AddVariacao();
                if (form.ShowDialog() == DialogResult.OK)
                    LoadData(GridLista);
            };

            GridLista.DoubleClick += (s, e) =>
            {
                if (GridLista.SelectedRows.Count > 0)
                {
                    AddVariacao.Id = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    AddVariacao form = new AddVariacao();
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadData(GridLista);
                }
            };

            btnDelete.Click += (s, e) =>
            {
                listGrupos.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool)item.Cells["Selecione"].Value == true)
                        listGrupos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));
                
                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar os GRUPOS selecionados e seus atributos, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var attr in listGrupos) {
                        new Model.ItemGrupo().Remove(attr);
                        new Model.ItemAtributos().Remove(attr, "GRUPO");
                    }
                    
                    LoadData(GridLista);
                }
                
                btnDelete.Visible = false;
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool)GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnDelete.Visible = true;
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

                        btnDelete.Visible = hideBtns;
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
            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}

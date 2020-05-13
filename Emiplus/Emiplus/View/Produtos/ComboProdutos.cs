using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;
using Validation = Emiplus.Data.Helpers.Validation;

namespace Emiplus.View.Produtos
{
    public partial class ComboProdutos : Form
    {
        /// <summary>
        /// Armazena os ID selecionado na Grid para remoção
        /// </summary>
        public List<int> ListCombos = new List<int>();

        /// <summary>
        /// Model - Item Combo
        /// </summary>
        private readonly ItemCombo _mItemCombo = new ItemCombo();

        public ComboProdutos()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        /// Adiciona headers da tabela GridLista
        /// </summary>
        /// <param name="table"></param>
        private static void SetHeadersCombo(DataGridView table)
        {
            table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] { true });
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

            table.Columns[2].Name = "Combo";
            table.Columns[2].Width = 120;
            table.Columns[2].MinimumWidth = 120;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;
        }


        /// <summary>
        /// Carrega a grid com os combos de produtos
        /// </summary>
        /// <param name="table"></param>
        private void LoadDataTable(DataGridView table)
        {
            table.Rows.Clear();

            var data = _mItemCombo.FindAll().WhereFalse("excluir").Get<ItemCombo>();
            if (data.Any())
            {
                foreach (var item in data)
                {
                    table.Rows.Add(
                        false,
                        item.Id,
                        item.Nome,
                        Validation.FormatPrice(item.ValorVenda, true)
                    );
                }
            }

            table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// Func para editar ou criar novo combo
        /// </summary>
        /// <param name="create"></param>
        private void EditCombo(bool create = false)
        {
            if (create)
            {
                AddComboProdutos.IdCombo = 0;
                OpenForm.Show<AddComboProdutos>(this);
                return;
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                AddComboProdutos.IdCombo = Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<AddComboProdutos>(this);
            }
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
                    EditCombo();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        /// <summary>
        /// Manipula todos os eventos do form
        /// </summary>
        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Shown += (s, e) =>
            {
                SetHeadersCombo(GridLista);
                LoadDataTable(GridLista);
            };
            
            btnEditar.Click += (s, e) => EditCombo();
            btnAdicionar.Click += (s, e) => EditCombo(true);
            GridLista.DoubleClick += (s, e) => EditCombo();

            btnRemover.Click += (s, e) =>
            {
                ListCombos.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool)item.Cells["Selecione"].Value)
                        ListCombos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));
                
                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a deletar os COMBOS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var item in ListCombos)
                        new ItemCombo().Remove(item, false);
                    
                    LoadDataTable(GridLista);
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

                        var hideBtns = false;
                        var hideBtnsTop = true;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool)item.Cells["Selecione"].Value)
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

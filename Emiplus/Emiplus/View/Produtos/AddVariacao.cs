using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Produtos
{
    public partial class AddVariacao : Form
    {
        // Armazena o ID dos atributos para serem deletados.
        public List<int> ListAtributos = new List<int>();

        public AddVariacao()
        {
            InitializeComponent();
            Eventos();
        }

        public static int Id { get; set; }

        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 2;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Selecione",
                Name = "Selecione",
                FlatStyle = FlatStyle.Standard,
                CellTemplate = new DataGridViewCheckBoxCell(),
                Width = 60
            };
            Table.Columns.Insert(0, checkColumn);

            Table.Columns[1].Name = "ID";
            Table.Columns[1].Visible = false;

            Table.Columns[2].Name = "Variação";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;
        }

        private void LoadData(DataGridView Table)
        {
            Table.Rows.Clear();

            var atributos = new ItemAtributos().FindAll().WhereFalse("excluir").Where("grupo", Id).Get<ItemAtributos>();
            foreach (var item in atributos)
                Table.Rows.Add(
                    false,
                    item.Id,
                    item.Atributo
                );

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                SetHeadersTable(GridLista);

                if (Id == 0)
                {
                    txtVariacao.Visible = false;
                    btnAddVariacao.Visible = false;
                    GridLista.Visible = false;
                }

                if (Id <= 0)
                    return;

                var grupo = new ItemGrupo().FindAll().WhereFalse("excluir").Where("id", Id).FirstOrDefault<ItemGrupo>();
                if (grupo == null)
                    return;

                txtGrupo.Text = grupo.Title;

                txtVariacao.Visible = true;
                btnAddVariacao.Visible = true;
                GridLista.Visible = true;

                // Carrega os atributos
                LoadData(GridLista);
            };

            btnSalvarGrupo.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtGrupo.Text))
                {
                    Alert.Message("Opps", "O título do grupo não pode ficar vazio.", Alert.AlertType.error);
                    return;
                }

                var grupoCheck = new ItemGrupo().FindAll().WhereFalse("excluir").Where("title", txtGrupo.Text)
                    .FirstOrDefault<ItemGrupo>();
                if (grupoCheck != null)
                {
                    Alert.Message("Opps", "Já existe um grupo com esse título.", Alert.AlertType.error);
                    return;
                }

                var grupo = new ItemGrupo
                {
                    Id = Id, 
                    Title = txtGrupo.Text
                };
                if (!grupo.Save(grupo))
                    return;

                Id = grupo.GetLastId();
                txtVariacao.Visible = true;
                btnAddVariacao.Visible = true;
                GridLista.Visible = true;
            };

            btnAddVariacao.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtVariacao.Text))
                {
                    Alert.Message("Opps", "O título da variação não pode ficar vazio.", Alert.AlertType.error);
                    return;
                }

                if (Id == 0)
                {
                    Alert.Message("Opps", "Você deve adicionar um grupo antes.", Alert.AlertType.error);
                    return;
                }

                var attrCheck = new ItemAtributos().FindAll().WhereFalse("excluir").Where("atributo", txtVariacao.Text)
                    .Where("grupo", Id).FirstOrDefault<ItemAtributos>();
                if (attrCheck != null)
                {
                    Alert.Message("Opps", "Já existe um atributo com esse título.", Alert.AlertType.error);
                    return;
                }

                var add = new ItemAtributos
                {
                    Grupo = Id,
                    Atributo = txtVariacao.Text
                };
                if (!add.Save(add))
                    return;

                txtVariacao.Clear();

                // Carrega os atributos
                LoadData(GridLista);
            };

            btnDelete.Click += (s, e) =>
            {
                ListAtributos.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool) item.Cells["Selecione"].Value)
                        ListAtributos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));

                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a deletar os ATRIBUTOS selecionados, continuar?", AlertBig.AlertType.warning,
                    AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var attr in ListAtributos)
                        new ItemAtributos().Remove(attr);

                    LoadData(GridLista);
                }

                btnDelete.Visible = false;
            };

            GridLista.CellClick += (s, e) =>
            {
                if (GridLista.Columns[e.ColumnIndex].Name == "Selecione")
                {
                    if ((bool) GridLista.SelectedRows[0].Cells["Selecione"].Value == false)
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        GridLista.SelectedRows[0].Cells["Selecione"].Value = false;

                        var hideBtns = false;
                        foreach (DataGridViewRow item in GridLista.Rows)
                            if ((bool) item.Cells["Selecione"].Value)
                                hideBtns = true;

                        btnDelete.Visible = hideBtns;
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

            btnVoltar.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
        }
    }
}
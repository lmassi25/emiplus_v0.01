using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class AddVariacao : Form
    {
        public static int Id { get; set; }

        // Armazena o ID dos atributos para serem deletados.
        public List<int> listAtributos = new List<int>();

        public AddVariacao()
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

            Table.Columns[2].Name = "Variação";
            Table.Columns[2].Width = 150;
            Table.Columns[2].Visible = true;
        }

        private void LoadData(DataGridView Table)
        {
            Table.Rows.Clear();

            IEnumerable<Model.ItemAtributos> atributos = new Model.ItemAtributos().FindAll().WhereFalse("excluir").Where("grupo", Id).Get<Model.ItemAtributos>();
            foreach (Model.ItemAtributos item in atributos)
            {
                Table.Rows.Add(
                    false,
                    item.Id,
                    item.Atributo
                );
            }
            
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

                if (Id > 0)
                {
                    Model.ItemGrupo grupo = new Model.ItemGrupo().FindAll().WhereFalse("excluir").Where("id", Id).FirstOrDefault<Model.ItemGrupo>();
                    if (grupo != null) {
                        txtGrupo.Text = grupo.Title;

                        txtVariacao.Visible = true;
                        btnAddVariacao.Visible = true;
                        GridLista.Visible = true;

                        // Carrega os atributos
                        LoadData(GridLista);
                    }
                }
            };

            btnSalvarGrupo.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtGrupo.Text))
                {
                    Alert.Message("Opps", "O título do grupo não pode ficar vazio.", Alert.AlertType.error);
                    return;
                }

                Model.ItemGrupo grupoCheck = new Model.ItemGrupo().FindAll().WhereFalse("excluir").Where("title", txtGrupo.Text).FirstOrDefault<Model.ItemGrupo>();
                if (grupoCheck != null) {
                    Alert.Message("Opps", "Já existe um grupo com esse título.", Alert.AlertType.error);
                    return;
                }

                Model.ItemGrupo grupo = new Model.ItemGrupo();
                grupo.Id = Id;
                grupo.Title = txtGrupo.Text;
                if (grupo.Save(grupo))
                {
                    Id = grupo.GetLastId();

                    txtVariacao.Visible = true;
                    btnAddVariacao.Visible = true;
                    GridLista.Visible = true;
                }
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
                
                Model.ItemAtributos attrCheck = new Model.ItemAtributos().FindAll().WhereFalse("excluir").Where("atributo", txtVariacao.Text).Where("grupo", Id).FirstOrDefault<Model.ItemAtributos>();
                if (attrCheck != null) {
                    Alert.Message("Opps", "Já existe um atributo com esse título.", Alert.AlertType.error);
                    return;
                }

                Model.ItemAtributos add = new Model.ItemAtributos();
                add.Grupo = Id;
                add.Atributo = txtVariacao.Text;
                if (add.Save(add)) {
                    txtVariacao.Clear();

                    // Carrega os atributos
                    LoadData(GridLista);
                }
            };

            btnDelete.Click += (s, e) =>
            {
                listAtributos.Clear();
                foreach (DataGridViewRow item in GridLista.Rows)
                    if ((bool)item.Cells["Selecione"].Value == true)
                        listAtributos.Add(Validation.ConvertToInt32(item.Cells["ID"].Value));
                
                var result = AlertOptions.Message("Atenção!", "Você está prestes a deletar os ATRIBUTOS selecionados, continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    foreach (var attr in listAtributos)
                        new Model.ItemAtributos().Remove(attr);

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

            btnVoltar.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
        }
    }
}

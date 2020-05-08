using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class Comissão : Form
    {
        public Comissão()
        {
            InitializeComponent();
            Eventos();
        }

        public void SetTable(DataGridView Table)
        {
            Table.ColumnCount = 3;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Nome";

            Table.Columns[2].Name = "Comissão";
            Table.Columns[2].Width = 150;

            Table.Rows.Clear();

            dynamic Data = new Model.Usuarios().FindAll().Where("excluir", 0).Where("sub_user", "!=", 0).Get();
            foreach (var item in Data)
            {
                Table.Rows.Add(
                    item.ID_USER,
                    item.NOME,
                    item.COMISSAO + "%"
                );
            }

            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ViewComissao()
        {
            if (GridLista.SelectedRows.Count <= 0)
                return;

            DetalhesComissao.idUser = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
            OpenForm.Show<DetalhesComissao>(this);
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                SetTable(GridLista);
            };

            btnVerComissao.Click += (s, e) => ViewComissao();
            GridLista.DoubleClick += (s, e) => ViewComissao();

            btnExit.Click += (s, e) => Close();
        }
    }
}
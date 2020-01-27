using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Financeiro
{
    public partial class Caixa : Form
    {
        public Caixa()
        {
            InitializeComponent();
            Eventos();
        }

        private void AutoCompleteUsers()
        {
            Usuarios.DataSource = (new Model.Usuarios()).GetAllUsers();
            Usuarios.DisplayMember = "Nome";
            Usuarios.ValueMember = "Id";
        }

        private async Task DataTableAsync() => await SetTable(GridLista);

        public Task<IEnumerable<dynamic>> GetDataTable()
        {
            var model = new Model.Caixa().Query();

            model.Where("CAIXA.criado", ">=", Validation.ConvertDateToSql(dataInicial.Value, true))
                .Where("CAIXA.criado", "<=", Validation.ConvertDateToSql(dataFinal.Value, true));

            if (Validation.ConvertToInt32(Usuarios.SelectedValue) >= 1)
            {
                model.Where("CAIXA.usuario", Validation.ConvertToInt32(Usuarios.SelectedValue));
            }

            model.LeftJoin("USUARIOS", "USUARIOS.id_user", "CAIXA.usuario");
            model.Select("CAIXA.*", "USUARIOS.id_user", "USUARIOS.nome as nome_user");
            model.Where("CAIXA.excluir", "0");
            model.OrderByRaw("CAIXA.tipo ASC");
            model.OrderByDesc("CAIXA.criado");
            return model.GetAsync<dynamic>();
        }

        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 6;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Width = 60;

            Table.Columns[1].Name = "Terminal";
            Table.Columns[1].Width = 60;

            Table.Columns[2].Name = "Aberto em";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Fechado em";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Aberto por";
            Table.Columns[4].Width = 130;

            Table.Columns[5].Name = "Status";
            Table.Columns[5].Width = 80;

            Table.Rows.Clear();

            if (Data == null)
            {
                IEnumerable<dynamic> dados = await GetDataTable();
                Data = dados;
            }

            for (int i = 0; i < Data.Count(); i++)
            {
                var item = Data.ElementAt(i);

                Table.Rows.Add(
                    item.ID,
                    item.TERMINAL,
                    Validation.ConvertDateToForm(item.CRIADO, true),
                    Validation.ConvertDateToForm(item.FECHADO, true),
                    item.NOME_USER,
                    item.TIPO
                );
            }

            Table.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ShowDetailsCaixa()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                DetailsCaixa.idCaixa = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                OpenForm.Show<DetailsCaixa>(this);
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
                    ShowDetailsCaixa();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += async (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                AutoCompleteUsers();

                dataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00");
                dataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy 23:59");

                await DataTableAsync();
            };

            GridLista.CellFormatting += (s, e) =>
            {
                foreach (DataGridViewRow row in GridLista.Rows)
                {
                    if (row.Cells[5].Value.ToString().Contains("Fechado"))
                    {
                        row.Cells[5].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[5].Style.ForeColor = Color.White;
                        row.Cells[5].Style.BackColor = Color.FromArgb(139, 215, 146);
                    }
                    else
                    {
                        row.Cells[5].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[5].Style.ForeColor = Color.White;
                        row.Cells[5].Style.BackColor = Color.FromArgb(255, 89, 89);
                    }
                }
            };

            btnSearch.Click += async (s, e) => await DataTableAsync();

            GridLista.DoubleClick += (s, e) => ShowDetailsCaixa();

            label4.Click += (s, e) => Close();
            btnExit.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }
    }
}
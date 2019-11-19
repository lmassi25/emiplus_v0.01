using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
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
            var users = new ArrayList();

            var userId = Settings.Default.user_sub_user == 0 ? Settings.Default.user_id : Settings.Default.user_sub_user;
            var dataUser = new RequestApi().URL($"{Program.URL_BASE}/api/listall/{Program.TOKEN}/{userId}").Content().Response();

            users.Add(new { Id = "0", Nome = "Todos" });
            foreach (var item in dataUser)
            {
                var nameComplete = $"{item.Value["name"].ToString()} {item.Value["lastname"].ToString()}";
                users.Add(new { Id = item.Value["id"].ToString(), Nome = nameComplete });
            }

            Usuarios.DataSource = users;
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
            model.OrderByRaw("CAIXA.tipo ASC");
            model.OrderByDesc("CAIXA.criado");
            return model.GetAsync<dynamic>();
        }
        
        public async Task SetTable(DataGridView Table, IEnumerable<dynamic> Data = null)
        {
            Table.ColumnCount = 9;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            
            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Terminal";
            Table.Columns[1].Width = 60;

            Table.Columns[2].Name = "Aberto em";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Fechado em";
            Table.Columns[3].Width = 100;

            Table.Columns[4].Name = "Saldo Inicial";
            Table.Columns[4].Width = 80;

            Table.Columns[5].Name = "Saldo Final";
            Table.Columns[5].Width = 80;

            Table.Columns[6].Name = "Saldo Final Informado";
            Table.Columns[6].Width = 80;

            Table.Columns[7].Name = "Usuário";
            Table.Columns[7].Width = 130;

            Table.Columns[8].Name = "Status";
            Table.Columns[8].Width = 50;

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
                    Validation.FormatPrice(Validation.ConvertToDouble(item.SALDO_INICIAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.SALDO_FINAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.SALDO_FINAL_INFORMADO), true),
                    item.NOME_USER,
                    item.TIPO
                );
            }

            Table.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            }
        }

        private void Eventos()
        {
            Load += async (s, e) =>
            {
                AutoCompleteUsers();

                dataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00");
                dataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy 23:59");

                await DataTableAsync();
            };

            btnSearch.Click += async (s, e) => await DataTableAsync();

            GridLista.DoubleClick += (s, e) => ShowDetailsCaixa();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }
    }
}

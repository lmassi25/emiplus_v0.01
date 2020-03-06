using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos
{
    public partial class ModalNCM : Form
    {
        public static string NCM { get; set; }
        private dynamic ncms { get; set; }
        private string searchTxt { get; set; }

        private BackgroundWorker backWork = new BackgroundWorker();

        public ModalNCM()
        {
            InitializeComponent();
            Eventos();
        }

        private void DataTable() => GetDataTable(GridLista);

        public void GetDataTable(DataGridView Table)
        {
            Table.ColumnCount = 2;

            Table.Columns[0].Name = "NCM";
            Table.Columns[0].Width = 60;

            Table.Columns[1].HeaderText = "Descrição";
            Table.Columns[1].Name = "Descricao";
            Table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Table.Rows.Clear();

            var jo = ncms;
            if (jo["error"] != null && jo["error"].ToString() != "")
            {
                Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                return;
            }

            foreach (var item in jo)
            {
                Table.Rows.Add(
                    item.Value["codigo"],
                    item.Value["descricao"]
                );
            }
        }

        private void SelectItemGrid()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                DialogResult = DialogResult.OK;
                NCM = GridLista.SelectedRows[0].Cells["NCM"].Value.ToString();

                Close();
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GridLista.Focus();
                    Support.UpDownDataGrid(false, GridLista);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Down:
                    GridLista.Focus();
                    Support.UpDownDataGrid(true, GridLista);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    Close();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F1:
                    search.Focus();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F10:
                    SelectItemGrid();
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Enter:
                    SelectItemGrid();
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Shown += (s, e) =>
            {
                Refresh();
                backWork.RunWorkerAsync();
            };

            Load += (s, e) => search.Select();
            btnSelecionar.Click += (s, e) => SelectItemGrid();

            buscar.Click += (s, e) =>
            {
                searchTxt = search.Text;
                backWork.RunWorkerAsync();
            };

            search.KeyPress += (s, e) => Masks.MaskOnlyNumberAndCharAndMore(s, e);

            backWork.DoWork += (s, e) =>
            {
                dynamic obj = new
                {
                    token = Program.TOKEN,
                    id_empresa = IniFile.Read("idEmpresa", "APP")
                };

                ncms = new RequestApi().URL(Program.URL_BASE + $"/api/ncm&search={searchTxt}").Content(obj, Method.POST).Response();
            };

            backWork.RunWorkerCompleted += (s, e) =>
            {
                label3.Visible = false;
                DataTable();
            };
        }

    }
}

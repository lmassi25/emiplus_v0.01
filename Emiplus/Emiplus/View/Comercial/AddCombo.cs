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
using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class AddCombo : Form
    {
        private Item _mItem = new Item();
        private ItemCombo _mItemCombo = new ItemCombo();

        public AddCombo()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        /// Recupera o ID do produto, para encontrar os combos
        /// </summary>
        public static int IdProduto { get; set; }

        /// <summary>
        /// Adiciona as colunas na tabela dos itens
        /// </summary>
        /// <param name="table"></param>
        private void SetHeadersTableItens(DataGridView table)
        {
            table.ColumnCount = 5;

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

            table.Columns[2].Name = "Item";
            table.Columns[2].Width = 150;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Valor";
            table.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;

            table.Columns[4].Name = "Estoque Atual";
            table.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[4].Width = 120;
            table.Columns[4].Visible = true;
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

        /// <summary>
        /// Manipula todos os eventos do form
        /// </summary>
        private void Eventos()
        {
            Shown += (s, e) =>
            {
                KeyDown += KeyDowns;
                KeyPreview = true;

                if (IdProduto > 0)
                    _mItem = _mItem.FindById(IdProduto).FirstOrDefault<Item>();
                else
                    return;
                
                Combos.DataSource = _mItemCombo.GetCombos(_mItem.Combos);
                Combos.DisplayMember = "Nome";
                Combos.ValueMember = "Id";

                SetHeadersTableItens(GridListaItens);
            };

            btnCombo.Click += (s, e) =>
            {
                if (Combos.SelectedValue.ToString() == "0")
                {
                    Alert.Message("Opps", "Selecione um combo válido.", Alert.AlertType.error);
                    return;
                }

                var dataCombo = _mItemCombo.FindById(Validation.ConvertToInt32(Combos.SelectedValue.ToString())).FirstOrDefault<ItemCombo>();
                if (dataCombo != null)
                {
                    
                }
            };

            btnInserir.Click += (s, e) =>
            {

            };

            btnContinuar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Ignore;
                Close();
            };
        }
    }
}

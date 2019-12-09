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

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarProdutos : Form
    {
        private ImportarNfe dataNfe = new ImportarNfe();
        public ImportarProdutos()
        {
            InitializeComponent();
            Eventos();
        }

        private ArrayList GetAllProdutos()
        {
            ArrayList allProdutos = new ArrayList();

            var count = dataNfe.GetNotas();
            if (count.Count > 0)
            {
                foreach (Controller.ImportarNfe item in count)
                {
                    foreach (dynamic pdt in item.GetProdutos())
                    {
                        allProdutos.Insert(0, pdt);
                    }
                }

                return allProdutos;
            }

            return null;
        }

        private void LoadProdutos()
        {
            var pdt = GetAllProdutos();
            SetDataTable(GridLista, pdt);
        }

        /// <summary>
        /// Table dos produtos
        /// </summary>
        /// <param name="Table">GridLista</param>
        /// <param name="dataProdutos">array dos produtos</param>
        private void SetDataTable(DataGridView Table, ArrayList dataProdutos)
        {
            Table.ColumnCount = 8;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "Status";
            Table.Columns[0].Width = 80;

            Table.Columns[1].Name = "Referência";
            Table.Columns[1].Width = 70;

            Table.Columns[2].Name = "Cód. de Barras";
            Table.Columns[2].Width = 100;

            Table.Columns[3].Name = "Descrição";
            Table.Columns[3].Width = 130;

            Table.Columns[4].Name = "Medida";
            Table.Columns[4].Width = 60;

            Table.Columns[5].Name = "Qtd.";
            Table.Columns[5].Width = 60;

            Table.Columns[6].Name = "Vlr. Compra";
            Table.Columns[6].Width = 80;

            Table.Columns[7].Name = "Vlr. Venda";
            Table.Columns[7].Width = 80;

            Table.Rows.Clear();

            foreach (dynamic item in dataProdutos)
            {
                Table.Rows.Add(
                    "Não Vinculado",
                    item.Referencia,
                    item.CodeBarras,
                    item.Descricao,
                    item.Medida,
                    item.Quantidade,
                    item.VlrCompra,
                    "00.0"
                );
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void Eventos()
        {
            Load += (s, e) =>
            {
                LoadProdutos();
            };
        }
    }
}

using Emiplus.Data.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Produtos.TelasImportarNfe
{
    public partial class ImportarPagamentos : Form
    {

        private ImportarNfe dataNfe = new ImportarNfe();

        public static ArrayList titulos = new ArrayList();

        public ImportarPagamentos()
        {
            InitializeComponent();
            Eventos();
        }

        private void GetTitulos()
        {
            var dadosTitulos = dataNfe.GetNotas();
            foreach (dynamic item in dadosTitulos)
            {
                SetTable(item.GetPagamentos());
            }
        }

        private void SetTable(dynamic dataTitulos)
        {
            GridLista.ColumnCount = 3;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            {
                checkColumn.HeaderText = "Importar";
                checkColumn.Name = "Importar";
                checkColumn.FlatStyle = FlatStyle.Standard;
                checkColumn.CellTemplate = new DataGridViewCheckBoxCell();
                checkColumn.Width = 60;
            }
            GridLista.Columns.Insert(0, checkColumn);

            GridLista.Columns[1].Name = "Forma de Pagamento";
            GridLista.Columns[1].Width = 120;

            GridLista.Columns[2].Name = "Data";
            GridLista.Columns[2].Width = 120;

            GridLista.Columns[3].Name = "Valor";
            GridLista.Columns[3].Width = 120;
            GridLista.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (dynamic item in dataTitulos)
            {
                GridLista.Rows.Add(
                    true,
                    item.Tipo,
                    Validation.ConvertDateToForm(item.dateTime),
                    item.Valor
                );
            }

            GridLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                GetTitulos();
            };

            btnImportar.Click += (s, e) =>
            {
                titulos.Clear();

                int i = -1;
                foreach (DataGridViewRow item in GridLista.Rows)
                {
                    i++;
                    if ((bool)item.Cells["Importar"].Value == true)
                    {
                        titulos.Add(new
                        {
                            Ordem = i,
                            FormaPgto = item.Cells["Forma de Pagamento"].Value.ToString(),
                            Data = item.Cells["Data"].Value.ToString(),
                            Valor = item.Cells["Valor"].Value.ToString()
                        });
                    }
                }

            };

            Back.Click += (s, e) => Close();
        }
    }
}

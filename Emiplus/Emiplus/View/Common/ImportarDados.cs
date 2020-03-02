using Emiplus.Data.Helpers;
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

namespace Emiplus.View.Common
{
    public partial class ImportarDados : Form
    {
        private string PathCSV { get; set; }
        private int Count { get; set; }
        private OpenFileDialog ofd = new OpenFileDialog();

        public ImportarDados()
        {
            InitializeComponent();
            Eventos();
        }

        private void SaveItens()
        {
            if (Modelos.SelectedItem.ToString() == "Produtos")
            {
                List<Model.Item> values = File.ReadAllLines(PathCSV).Skip(1).Select(v => new Model.Item().FromCsv(v)).ToList();
                Count = values.Count();
            } else if (Modelos.SelectedItem.ToString() == "Clientes")
            {
                List<Model.Pessoa> values = File.ReadAllLines(PathCSV).Skip(1).Select(v => new Model.Pessoa().FromCsv(v)).ToList();
                Count = values.Count();
            }
            
            if (AlertOptions.Message("Pronto!", $"Importação feita com sucesso.\n{Count} itens importados!  ", AlertBig.AlertType.success, AlertBig.AlertBtn.OK))
                Close();
        }

        private void ContentItens(dynamic values)
        {
            if (values.Count > 0)
            {
                foreach (dynamic data in values)
                {
                    data.Save(data, false);
                }
            }
        }
        
        private void Eventos()
        {
            Load += (s, e) =>
            {
                Resolution.SetScreenMaximized(this);
                Modelos.SelectedIndex = 0;
            };

            btnSelecinar.Click += (s, e) =>
            {
                ofd.RestoreDirectory = true;
                ofd.DefaultExt = "csv";
                ofd.Filter = "CSV|*.csv";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = new DataTable();

                    PathCSV = ofd.FileName;
                    pathFile.Text = PathCSV;
                    string[] Linha = System.IO.File.ReadAllLines(PathCSV);

                    for (Int32 i = 0; i < Linha.Length; i++)
                    {
                        string[] campos = Linha[i].Split(Convert.ToChar(";"));

                        if (i == 0)
                        {
                            for (Int32 i2 = 0; i2 < campos.Length; i2++)
                            {
                                DataColumn col = new DataColumn();
                                dt.Columns.Add(campos.GetValue(i2).ToString());
                            }
                        }

                        dt.Rows.Add(campos);
                    }

                    dt.Rows.RemoveAt(0);
                    GridLista.DataSource = dt;
                }
            };

            btnImportar.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(PathCSV))
                {
                    Alert.Message("Oppss", "Selecione um arquivo CSV antes de continuar.", Alert.AlertType.error);
                    return;
                }

                SaveItens();
            };

            Back.Click += (s, e) => Close();
        }
    }
}

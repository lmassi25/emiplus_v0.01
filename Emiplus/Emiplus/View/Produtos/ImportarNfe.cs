using Emiplus.Data.Helpers;
using System;
using System.Collections;
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
    public partial class ImportarNfe : Form
    {
        private bool MultipleImports { get; set; }
        private static ArrayList notas { get; set; }

        /// <summary>
        /// 1 - Importar produtos
        /// 2 - Maipular Estoque
        /// 3 - Importar Compra
        /// </summary>
        public static int optionSelected { get; set; }

        OpenFileDialog ofd = new OpenFileDialog();
        Controller.ImportarNfe dataNfe;
        
        public ImportarNfe()
        {
            InitializeComponent();
            Eventos();
        }

        public ArrayList GetNotas()
        {
            return notas;
        }

        private void LoadDadosNota()
        {
            GridLista.Rows.Clear();

            var dataNotas = GetNotas();
            foreach (Controller.ImportarNfe item in dataNotas)
            {
                GridLista.Rows.Add(
                    item.GetDados().Emissao,
                    item.GetDados().Nr,
                    item.GetDados().Id
                    );
            }

            dadosNota.Visible = true;
            btnAvancar.Visible = true;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                MultipleImports = true;
                optionSelected = 1;
            };

            btnSelecinarNfe.Click += (s, e) =>
            {
                ofd.RestoreDirectory = true;
                ofd.DefaultExt = "xml";
                ofd.Filter = "XML|*.xml";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Multiselect = MultipleImports;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    notas = new ArrayList();
                    foreach (String file in ofd.FileNames)
                    {
                        dataNfe = new Controller.ImportarNfe(file);
                        notas.Add(dataNfe);
                    }

                    //pathXml = Path.GetDirectoryName(ofd.FileName) + @"\" + ofd.SafeFileName;
                    //pathFile.Text = pathXml;

                    //dataNfe = new Controller.ImportarNfe(pathXml);
                    LoadDadosNota();
                }
            };

            Op1.Click += (s, e) =>
            {
                MultipleImports = true;
                optionSelected = 1;
            };

            Op2.Click += (s, e) =>
            {
                MultipleImports = true;
                optionSelected = 2;
            };

            Op3.Click += (s, e) =>
            {
                MultipleImports = false;
                optionSelected = 3;
            };

            btnAvancar.Click += (s, e) =>
            {
                if (optionSelected == 1 || optionSelected == 2)
                {
                    OpenForm.Show<TelasImportarNfe.ImportarProdutos>(this);
                    return;
                }

                OpenForm.Show<TelasImportarNfe.ImportarFornecedor>(this);
            };
        }
    }
}

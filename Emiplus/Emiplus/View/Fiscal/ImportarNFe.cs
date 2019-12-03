using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using ChoETL;

namespace Emiplus.View.Fiscal
{
    public partial class ImportarNFe : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        private string pathXml { get; set; }
        private dynamic dataNota { get; set; }

        public ImportarNFe()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadDataNota()
        {
            LoadGridProdutos();
            LoadGridFornecedor();
        }

        private void LoadGridProdutos()
        {
            dynamic produtos = null;
            if (dataNota.ContainsKey("dets"))
                produtos = dataNota.dets;
            else
                produtos = dataNota.det;

            ArrayList dataProdutos = new ArrayList();
            if (dataNota.ContainsKey("dets"))
            {
                foreach (var item in produtos)
                {
                    dataProdutos.Add(
                        new
                        {
                            nItem = item.nItem,
                            cProd = item.prod.cProd,
                            cEAN = item.prod.cEAN,
                            xProd = item.prod.xProd,
                            NCM = item.prod.NCM,
                            CFOP = item.prod.CFOP,
                            uCom = item.prod.uCom,
                            qCom = item.prod.qCom,
                            vUnCom = item.prod.vUnCom,
                            vProd = item.prod.vProd,
                            cEANTrib = item.prod.cEANTrib,
                            uTrib = item.prod.uTrib,
                            indTot = item.prod.indTot
                        });
                }
            }
            else
            {
                dataProdutos.Add(
                        new
                        {
                            nItem = produtos.nItem,
                            cProd = produtos.prod.cProd,
                            cEAN = produtos.prod.cEAN,
                            xProd = produtos.prod.xProd,
                            NCM = produtos.prod.NCM,
                            CFOP = produtos.prod.CFOP,
                            uCom = produtos.prod.uCom,
                            qCom = produtos.prod.qCom,
                            vUnCom = produtos.prod.vUnCom,
                            vProd = produtos.prod.vProd,
                            cEANTrib = produtos.prod.cEANTrib,
                            uTrib = produtos.prod.uTrib,
                            indTot = produtos.prod.indTot
                        });
            }

            SetDataTable(GridLista, dataProdutos);
        }

        /// <summary>
        /// Table dos produtos
        /// </summary>
        /// <param name="Table">GridLista</param>
        /// <param name="dataProdutos">array dos produtos</param>
        private void SetDataTable(DataGridView Table, ArrayList dataProdutos)
        {
            Table.ColumnCount = 9;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "N°";
            Table.Columns[0].Width = 50;

            Table.Columns[1].Name = "Referência";
            Table.Columns[1].Width = 70;

            Table.Columns[2].Name = "Cód. de Barras";
            Table.Columns[2].Width = 130;

            Table.Columns[3].Name = "Descrição";
            Table.Columns[3].Width = 130;

            Table.Columns[4].Name = "NCM";
            Table.Columns[3].Width = 100;

            Table.Columns[5].Name = "CFOP";
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = "Medida";
            Table.Columns[6].Width = 70;

            Table.Columns[7].Name = "Qtd.";
            Table.Columns[7].Width = 100;

            Table.Columns[8].Name = "Vlr. Unitário";
            Table.Columns[8].Width = 100;

            Table.Rows.Clear();

            foreach (dynamic item in dataProdutos)
            {
                Table.Rows.Add(
                    item.nItem,
                    item.cProd,
                    item.cEAN,
                    item.xProd,
                    item.NCM,
                    item.CFOP,
                    item.uCom,
                    item.qCom,
                    item.vUnCom
                );
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void LoadGridFornecedor()
        {
            ArrayList dataFornecedor = new ArrayList();
            if (dataNota.ContainsKey("dest"))
            {
                string document = "";
                if (dataNota.dest.ContainsKey("CPF"))
                    document = dataNota.dest.CPF;

                if (dataNota.dest.ContainsKey("CNPJ"))
                    document = dataNota.dest.CNPJ;

                dataFornecedor.Add(
                    new
                    {
                        Document = document,
                        Nome = dataNota.dest.xNome,
                        Rua = dataNota.dest.enderDest.xLgr,
                        Nr = dataNota.dest.enderDest.nro,
                        Bairro = dataNota.dest.enderDest.xBairro,
                        IBGE = dataNota.dest.enderDest.cMun,
                        Cidade = dataNota.dest.enderDest.xMun,
                        UF = dataNota.dest.enderDest.UF,
                        CEP = dataNota.dest.enderDest.CEP
                    });
            }
            
            SetDataTableFornecedor(gridFornecedor, dataFornecedor);
        }

        private void SetDataTableFornecedor(DataGridView Table, ArrayList dataProdutos)
        {
            Table.ColumnCount = 9;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            //Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            #region Colunas
            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "CPF";
            Table.Columns[0].Width = 50;

            Table.Columns[1].Name = "Nome";
            Table.Columns[1].Width = 130;

            Table.Columns[2].Name = "Rua";
            Table.Columns[2].Width = 130;

            Table.Columns[3].Name = "N°";
            Table.Columns[3].Width = 50;

            Table.Columns[4].Name = "Bairro";
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "IBGE";
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = "Cidade";
            Table.Columns[6].Width = 70;

            Table.Columns[7].Name = "UF";
            Table.Columns[7].Width = 100;

            Table.Columns[8].Name = "CEP";
            Table.Columns[8].Width = 100;

            Table.Rows.Clear();
            #endregion

            foreach (dynamic item in dataProdutos)
            {
                Table.Rows.Add(
                    item.Document,
                    item.Nome,
                    item.Rua,
                    item.Nr,
                    item.Bairro,
                    item.IBGE,
                    item.Cidade,
                    item.UF,
                    item.CEP
                );
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {

            };

            btnSelecinarNfe.Click += (s, e) =>
            {
                ofd.RestoreDirectory = true;
                ofd.DefaultExt = "xml";
                ofd.Filter = "XML|*.xml";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pathXml = Path.GetDirectoryName(ofd.FileName) + @"\" + ofd.SafeFileName;
                    pathFile.Text = pathXml;
                    btnImportar.Visible = true;

                    ChoXmlRecordConfiguration config = new ChoXmlRecordConfiguration();
                    config.NamespaceManager.AddNamespace("x", "http://www.portalfiscal.inf.br/nfe");

                    dynamic loadNota = new ChoXmlReader(pathXml, config);
                    foreach (dynamic dataNota in loadNota)
                    {
                        if (dataNota.ContainsKey("infNFe"))
                            this.dataNota = dataNota.infNFe;
                        else
                            this.dataNota = dataNota;

                        break;
                    }

                    LoadDataNota();
                }
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}

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
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using SqlKata.Execution;

namespace Emiplus.View.Fiscal
{
    public partial class ImportarNFe : Form
    {
        private string pathXml { get; set; }
        private dynamic dataNota { get; set; }

        OpenFileDialog ofd = new OpenFileDialog();
        private Model.Item _mItem = new Model.Item();
        KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();

        public ImportarNFe()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoadDataNota()
        {
            dataEmissao.Text = Validation.ConvertDateToForm(dataNota.ide.dhEmi, true);
            nrNota.Text = dataNota.ide.nNF;
            chaveAcesso.Text = dataNota.Id;

            LoadGridProdutos();
            LoadGridFornecedor();

            BuscarProduto.Enabled = true;
            btnVincular.Enabled = true;
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
                    item.cProd,
                    item.cEAN,
                    item.xProd,
                    item.uCom,
                    item.qCom,
                    item.vUnCom,
                    "00.0"
                );
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadGridFornecedor()
        {
            if (dataNota.ContainsKey("dest"))
            {
                string document = "";
                if (dataNota.dest.ContainsKey("CPF"))
                    document = dataNota.dest.CPF;

                if (dataNota.dest.ContainsKey("CNPJ"))
                    document = dataNota.dest.CNPJ;

                CPFcnpj.Text = document;
                razaoSocial.Text = dataNota.dest.xNome;
            }
        }

        /// <summary>
        /// Autocomplete do campo de busca de produtos.
        /// </summary>
        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                collection.Add(itens.NOME, itens.ID);
            }

            BuscarProduto.AutoCompleteCustomSource = collection;
        }

        private void VincularProduto()
        {
            if (collection.Lookup(BuscarProduto.Text) > 0)
            {
                if (GridLista.SelectedRows.Count > 0)
                {
                    panelVinculacao.Visible = true;
                    
                    var codeBarras = GridLista.SelectedRows[0].Cells["Cód. de Barras"].Value;
                    MessageBox.Show(codeBarras.ToString());

                    return;
                }

                Alert.Message("Oppss", "Selecione um produto para vincular.", Alert.AlertType.warning);
            }
        }

        private void LoadPag()
        {
            ArrayList Pagamentos = new ArrayList();
            ArrayList datesTimes = new ArrayList();

            string dateTime = "";
            string Valor = "";
            string Tipo = "";

            if (dataNota.ContainsKey("cobr") == true)
            {
                if (dataNota.cobr is ChoETL.ChoDynamicObject)
                {
                    if (dataNota.cobr.ContainsKey("dups") == true)
                    {
                        int u = -1;
                        foreach (var dataCOBR in dataNota.cobr.dups)
                        {
                            u++;
                            //string dateTime = "";
                            //string Tipo = "";
                            //string Valor = "";

                            dateTime = dataCOBR.dVenc;
                            Valor = dataCOBR.vDup;

                            datesTimes.Add(new { dateTime });

                            Pagamentos.Add(new
                            {
                                dateTime = dateTime,
                                Tipo = Tipo,
                                Valor = Valor
                            });
                        }
                    }
                }
                else
                {
                    int u = -1;
                    foreach (var dataCOBR in dataNota.cobr)
                    {
                        u++;

                        dateTime = dataCOBR.dVenc;
                        Valor = dataCOBR.vDup;

                        
                        Pagamentos.Add(new
                        {
                            dateTime = dateTime,
                            Tipo = Tipo,
                            Valor = Valor
                        });
                    }
                }

            }

            if (dataNota.ContainsKey("pag") == true)
            {
                Pagamentos.Clear();

                int u = -1;
                foreach (var dataCOBR in dataNota.pag)
                {
                    u++;
                    
                    if (dataNota.ContainsKey("pag"))
                    {
                        if (dataCOBR.Key == "detPag")
                            Tipo = dataCOBR.Value.tPag;
                        else
                            Tipo = dataCOBR.tPag;

                        if (dataCOBR.Key == "detPag")
                            Valor = dataCOBR.Value.vPag;
                        else
                            Valor = dataCOBR.vPag;
                    }

                    if (datesTimes.Count > 0)
                    {
                        dateTime = datesTimes[u].ToString();
                    }

                    Pagamentos.Add(new
                    {
                        dateTime = dateTime,
                        Tipo = Tipo,
                        Valor = Valor
                    });
                }
            }

        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                AutoCompleteItens();
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

                    Controller.ImportarNfe clas = new Controller.ImportarNfe(pathXml);

                    var prod = clas.GetProdutos();
                    var forn = clas.GetFornecedor();
                    var pags = clas.GetPagamentos();

                    LoadDataNota();
                    //LoadPagamentos();
                }
            };

            btnVincular.Click += (s, e) => VincularProduto();

            btnChecarItem.Click += (s, e) =>
            {
                if (GridLista.SelectedRows.Count > 0)
                {
                    panelVinculacao.Visible = true;

                    var codeBarras = GridLista.SelectedRows[0].Cells["Cód. de Barras"].Value;
                    MessageBox.Show(codeBarras.ToString());

                    return;
                }

                Alert.Message("Oppss", "Selecione um produto para vincular.", Alert.AlertType.warning);
            };

            btnExit.Click += (s, e) => Close();
            btnHelp.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/ajuda");
        }

        private void nome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

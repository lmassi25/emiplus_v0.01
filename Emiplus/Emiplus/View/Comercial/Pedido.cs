using DotLiquid;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Fiscal;
using Emiplus.View.Fiscal.TelasNota;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Timer = System.Timers.Timer;

namespace Emiplus.View.Comercial
{
    /// <summary>
    /// Responsavel por Vendas, Compras, Orçamentos, Consignações, Devoluções
    /// </summary>
    public partial class Pedido : Form
    {
        #region V

        private Controller.Pedido _cPedido = new Controller.Pedido();
        private Model.Pessoa _mPessoa = new Model.Pessoa();
        private Model.Item _mItem = new Model.Item();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        private Timer timer = new Timer(Configs.TimeLoading);

        private KeyedAutoCompleteStringCollection collection = new KeyedAutoCompleteStringCollection();
        private KeyedAutoCompleteStringCollection collectionItem = new KeyedAutoCompleteStringCollection();

        private KeyedAutoCompleteStringCollection collectionA = new KeyedAutoCompleteStringCollection();
        private KeyedAutoCompleteStringCollection collectionB = new KeyedAutoCompleteStringCollection();
        private KeyedAutoCompleteStringCollection collectionC = new KeyedAutoCompleteStringCollection();
        private KeyedAutoCompleteStringCollection collectionD = new KeyedAutoCompleteStringCollection();
        private KeyedAutoCompleteStringCollection collectionE = new KeyedAutoCompleteStringCollection();
        private KeyedAutoCompleteStringCollection collectionF = new KeyedAutoCompleteStringCollection();

        private string controle;

        #endregion V

        public Pedido()
        {
            InitializeComponent();
            Eventos();

            label3.Text = Home.pedidoPage;
            label1.Text = Home.pedidoPage;
            Resolution.SetScreenMaximized(this);

            switch (Home.pedidoPage)
            {
                case "Orçamentos":
                    label2.Text = "Gerencie os " + Home.pedidoPage.ToLower() + " aqui! Adicione, edite ou apague um orçamento.";
                    //label13.Visible = false;
                    break;

                case "Consignações":
                    label2.Text = "Gerencie as " + Home.pedidoPage.ToLower() + " aqui! Adicione, edite ou apague uma consignação.";
                    //label13.Visible = false;
                    break;

                case "Trocas":
                    label2.Text = "Gerencie as Trocas aqui! Adicione, edite ou apague uma devolução.";
                    break;

                case "Compras":
                    label2.Text = "Gerencie as " + Home.pedidoPage.ToLower() + " aqui! Adicione, edite ou apague uma compra.";
                    label11.Text = "Procurar por fornecedor";
                    //label13.Visible = false;
                    break;

                case "Notas":
                    label1.Text = "NF-e";
                    label2.Text = "Gerencie as NF-e aqui! Adicione, edite ou apague uma nota.";
                    break;

                case "Cupons":
                    label1.Text = "CF-e S@T";
                    label2.Text = "Gerencie os CF-e S@T aqui! Adicione, edite ou apague um cupom.";
                    break;

                case "Vendas":
                    label2.Text = "Gerencie as " + Home.pedidoPage.ToLower() + " aqui! Adicione, edite ou apague uma venda.";
                    break;

                case "Ordens de Servico":
                    label2.Text = "Gerencie as O.S. aqui! Adicione, edite ou apague uma o.s.";
                    label14.Visible = false;
                    produtoId.Visible = false;
                    break;
            }

            dataInicial.Text = DateTime.Now.ToString();
            dataFinal.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// Autocomplete do campo de busca de pessoas.
        /// </summary>
        private void AutoCompletePessoas()
        {
            var data = _mPessoa.Query();

            data.Select("id", "nome");
            data.Where("excluir", 0);

            switch (Home.pedidoPage)
            {
                case "Compras":
                    data.Where("tipo", "Fornecedores");
                    break;

                case "Consignações":
                    data.Where("tipo", "Clientes");
                    break;

                case "Devoluções":
                    data.Where("tipo", "Clientes");
                    break;

                default:
                    data.Where("tipo", "Clientes");
                    break;
            }

            foreach (var itens in data.Get())
            {
                if (itens.NOME != "Novo registro")
                    collection.Add(itens.NOME, itens.ID);
            }

            BuscarPessoa.AutoCompleteCustomSource = collection;
        }

        /// <summary>
        /// Autocomplete do campo de busca de usuários.
        /// </summary>
        private void AutoCompleteUsers()
        {
            Usuarios.DataSource = (new Model.Usuarios()).GetAllUsers();
            Usuarios.DisplayMember = "Nome";
            Usuarios.ValueMember = "Id";
        }

        /// <summary>
        /// Autocomplete do campo de busca de os.
        /// </summary>
        private void AutoCompleteOS()
        {
            var data = new Model.Pedido().Query();

            data.Select("campoa");
            data.Where("campoa", "<>", "");
            data.WhereNotNull("campoa");

            foreach (var itens in data.Get())
            {
                collectionA.Add(itens.CAMPOA);
            }

            aText.AutoCompleteCustomSource = collectionA;

            data.Select("campob");
            data.Where("campob", "<>", "");
            data.WhereNotNull("campob");

            foreach (var itens in data.Get())
            {
                collectionB.Add(itens.CAMPOB);
            }

            bText.AutoCompleteCustomSource = collectionB;

            data.Select("campoc");
            data.Where("campoc", "<>", "");
            data.WhereNotNull("campoc");

            foreach (var itens in data.Get())
            {
                collectionC.Add(itens.CAMPOC);
            }

            cText.AutoCompleteCustomSource = collectionC;

            data.Select("campod");
            data.Where("campod", "<>", "");
            data.WhereNotNull("campod");

            foreach (var itens in data.Get())
            {
                collectionD.Add(itens.CAMPOD);
            }

            dText.AutoCompleteCustomSource = collectionD;

            data.Select("campoe");
            data.Where("campoe", "<>", "");
            data.WhereNotNull("campoe");

            foreach (var itens in data.Get())
            {
                collectionE.Add(itens.CAMPOE);
            }

            eText.AutoCompleteCustomSource = collectionE;

            data.Select("campof");
            data.Where("campof", "<>", "");
            data.WhereNotNull("campof");

            foreach (var itens in data.Get())
            {
                collectionF.Add(itens.CAMPOF);
            }

            fText.AutoCompleteCustomSource = collectionF;
        }

        private void DataTableStart()
        {
            BuscarPessoa.Select();

            GridLista.Visible = false;
            Loading.Size = new System.Drawing.Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void Filter()
        {
            int excluir = 0;

            Dictionary<string, string> dataFilters = new Dictionary<string, string>();

            if (Home.pedidoPage == "Ordens de Servico")
            {
                dataFilters["campoa"] = aText.Text;
                dataFilters["campob"] = bText.Text;
                dataFilters["campoc"] = cText.Text;
                dataFilters["campod"] = dText.Text;
                dataFilters["campoe"] = eText.Text;
                dataFilters["campof"] = fText.Text;
            }

            if (filterRemovido.Checked)
                excluir = 1;

            await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, noFilterData.Checked, null, BuscarPessoa.Text, excluir, Validation.ConvertToInt32(BuscaID.Text), Validation.ConvertToInt32(Status.SelectedValue), Validation.ConvertToInt32(Usuarios.SelectedValue), collectionItem.Lookup(produtoId.Text), dataFilters);
        }

        private void EditPedido(bool create = false)
        {
            OpcoesCfe.tipoTela = 0;

            if (create)
            {
                if (Home.pedidoPage == "Notas")
                {
                    Nota.disableCampos = false;
                    Nota.Id = 0;
                    //OpcoesNfeRapida.idPedido = 0;
                    Nota nota = new Nota();
                    nota.ShowDialog();
                    //Filter();
                    return;
                }
                else if (Home.pedidoPage == "Ordens de Servico")
                {
                    AddOs.Id = 0;
                    OpenForm.Show<Comercial.AddOs>(this);
                    return;
                }
                else
                {
                    AddPedidos.Id = 0;
                    AddPedidos NovoPedido = new AddPedidos();
                    NovoPedido.ShowDialog();
                    //Filter();
                    return;
                }
            }

            if (GridLista.SelectedRows.Count > 0)
            {
                Model.Pedido dataTipo = new Model.Pedido().FindById(Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value)).FirstOrDefault<Model.Pedido>();
                if (dataTipo != null && dataTipo.Tipo != Home.pedidoPage && Home.pedidoPage != "Notas" && Home.pedidoPage != "Cupons")
                {
                    Alert.Message("Opss", "Não é possível carregar este registro", Alert.AlertType.warning);
                    return;
                }

                if (Home.pedidoPage == "Ordens de Servico")
                {
                    AddOs.Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    OpenForm.Show<Comercial.AddOs>(this);
                    return;
                }

                if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Consignações" || Home.pedidoPage == "Devoluções")
                {
                    AddPedidos.Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    AddPedidos NovoPedido = new AddPedidos();
                    NovoPedido.ShowDialog();
                    return;
                }

                if (Home.pedidoPage == "Cupons")
                {
                    OpcoesCfe.tipoTela = 1;
                    OpcoesCfe.idPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    OpcoesCfe.idNota = Convert.ToInt32(GridLista.SelectedRows[0].Cells["IDNOTA"].Value);
                    OpcoesCfe f = new OpcoesCfe();
                    f.Show();
                    return;
                }

                if (Home.pedidoPage == "Notas")
                {
                    if (!GridLista.SelectedRows[0].Cells["Status"].Value.ToString().Contains("Pendente"))
                    {
                        OpcoesNfeRapida.idNota = Convert.ToInt32(GridLista.SelectedRows[0].Cells["IDNOTA"].Value);
                        OpcoesNfeRapida.idPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                        OpcoesNfeRapida f = new OpcoesNfeRapida();
                        f.Show();
                    }
                    else
                    {
                        Nota.Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["IDNOTA"].Value);
                        Nota nota = new Nota();
                        nota.ShowDialog();
                    }
                }
                else
                {
                    if (Validation.ConvertToInt32(GridLista.SelectedRows[0].Cells["EXCLUIR"].Value) > 0)
                    {
                        Alert.Message("Erro", "Esse registro foi excluido e não é permitido acessá-lo", Alert.AlertType.warning);
                        return;
                    }

                    if (Home.pedidoPage == "Compras" && GridLista.SelectedRows[0].Cells["Status"].Value.ToString() == "Pendente")
                    {
                        AddPedidos.Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                        AddPedidos NovoPedido = new AddPedidos();
                        NovoPedido.ShowDialog();
                        return;
                    }

                    if (Home.pedidoPage == "Vendas" && GridLista.SelectedRows[0].Cells["Status"].Value.ToString() == "Pendente")
                    {
                        AddPedidos.Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                        AddPedidos NovoPedido = new AddPedidos();
                        NovoPedido.ShowDialog();
                        return;
                    }

                    DetailsPedido.idPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    //DetailsPedido detailsPedido = new DetailsPedido();
                    //detailsPedido.ShowDialog();
                    OpenForm.Show<DetailsPedido>(this);
                }

                //Filter();
            }
            else
            {
                Alert.Message("Opps", "Selecione um item na tabela.", Alert.AlertType.info);
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
                    Filter();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            controle = "";
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                int diff = 0;

                if(Home.pedidoPage != "Ordens de Servico")
                {
                    aLabel.Visible = false;
                    aText.Visible = false;
                    bLabel.Visible = false;
                    bText.Visible = false;
                    cLabel.Visible = false;
                    cText.Visible = false;
                    dLabel.Visible = false;
                    dText.Visible = false;
                    eLabel.Visible = false;
                    eText.Visible = false;
                    fLabel.Visible = false;
                    fText.Visible = false;

                    diff = 112;
                    visualSeparator1.Location = new Point(visualSeparator1.Location.X, visualSeparator1.Location.Y - diff);
                    label8.Location = new Point(label8.Location.X, label8.Location.Y - diff);
                    label9.Location = new Point(label9.Location.X, label9.Location.Y - diff);
                    dataInicial.Location = new Point(dataInicial.Location.X, dataInicial.Location.Y - diff);
                    label10.Location = new Point(label10.Location.X, label10.Location.Y - diff);
                    dataFinal.Location = new Point(dataFinal.Location.X, dataFinal.Location.Y - diff);
                    noFilterData.Location = new Point(noFilterData.Location.X, noFilterData.Location.Y - diff);
                    btnSearch.Location = new Point(btnSearch.Location.X, btnSearch.Location.Y - diff);
                    panel2.Location = new Point(panel2.Location.X, panel2.Location.Y - diff);
                    panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height + diff);

                    return;
                }

                aLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_1_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_1_Pesquisa", "OS")) : false;
                aText.Visible = aLabel.Visible;
                aLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_1_Descr", "OS")) ? IniFile.Read("Campo_1_Descr", "OS") : "";

                bLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_2_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_2_Pesquisa", "OS")) : false;
                bText.Visible = bLabel.Visible;
                bLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_2_Descr", "OS")) ? IniFile.Read("Campo_2_Descr", "OS") : "";

                cLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_3_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_3_Pesquisa", "OS")) : false;
                cText.Visible = cLabel.Visible;
                cLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_3_Descr", "OS")) ? IniFile.Read("Campo_3_Descr", "OS") : "";

                dLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_4_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_4_Pesquisa", "OS")) : false;
                dText.Visible = dLabel.Visible;
                dLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_4_Descr", "OS")) ? IniFile.Read("Campo_4_Descr", "OS") : "";

                eLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_5_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_5_Pesquisa", "OS")) : false;
                eText.Visible = eLabel.Visible;
                eLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_5_Descr", "OS")) ? IniFile.Read("Campo_5_Descr", "OS") : "";

                fLabel.Visible = !String.IsNullOrEmpty(IniFile.Read("Campo_6_Pesquisa", "OS")) ? Convert.ToBoolean(IniFile.Read("Campo_6_Pesquisa", "OS")) : false;
                fText.Visible = fLabel.Visible;
                fLabel.Text = !String.IsNullOrEmpty(IniFile.Read("Campo_6_Descr", "OS")) ? IniFile.Read("Campo_6_Descr", "OS") : "";

                if (!dLabel.Visible && !eLabel.Visible && !eLabel.Visible)
                {
                    diff = 59;
                    visualSeparator1.Location = new Point(visualSeparator1.Location.X, visualSeparator1.Location.Y - diff);
                    label8.Location = new Point(label8.Location.X, label8.Location.Y - diff);
                    label9.Location = new Point(label9.Location.X, label9.Location.Y - diff);
                    dataInicial.Location = new Point(dataInicial.Location.X, dataInicial.Location.Y - diff);
                    label10.Location = new Point(label10.Location.X, label10.Location.Y - diff);
                    dataFinal.Location = new Point(dataFinal.Location.X, dataFinal.Location.Y - diff);
                    noFilterData.Location = new Point(noFilterData.Location.X, noFilterData.Location.Y - diff);
                    btnSearch.Location = new Point(btnSearch.Location.X, btnSearch.Location.Y - diff);
                    panel2.Location = new Point(panel2.Location.X, panel2.Location.Y - diff);
                    panel2.Size = new Size(panel2.Size.Width, panel2.Size.Height + diff);
                }
            };

            Shown += (s, e) =>
            {
                Refresh();

                DataTableStart();
                BuscarPessoa.Select();
                AutoCompletePessoas();
                AutoCompleteUsers();

                if (Home.pedidoPage == "Ordens de Servico")
                    AutoCompleteOS();

                produtoId.AutoCompleteCustomSource = _mItem.AutoComplete();

                dataInicial.Text = DateTime.Now.ToString();
                dataFinal.Text = DateTime.Now.ToString();

                var status = new ArrayList();
                status.Add(new { ID = 99, NOME = "Todos" });

                if (Home.pedidoPage == "Notas" || Home.pedidoPage == "Cupons")
                {
                    if (Home.pedidoPage == "Notas")
                        status.Add(new { ID = 1, NOME = "Pendentes" });
                    status.Add(new { ID = 2, NOME = "Autorizadas" });
                    status.Add(new { ID = 3, NOME = "Canceladas" });
                }
                else if (Home.pedidoPage == "Ordens de Servico" || Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações")
                {
                    status.Add(new { ID = 0, NOME = "Pendente" });
                    status.Add(new { ID = 1, NOME = "Finalizado" });
                }
                else
                {
                    status.Add(new { ID = 2, NOME = "Recebimento Pendente" });
                    status.Add(new { ID = 1, NOME = @"Finalizado\Recebido" });
                }

                Status.DataSource = status;
                Status.DisplayMember = "NOME";
                Status.ValueMember = "ID";
                Status.SelectedValue = 99;                
            };

            BuscarPessoa.KeyDown += (s, e) =>
            {
                controle = "BuscarPessoa";
                KeyDowns(s, e);
            };

            btnSearch.Click += (s, e) => Filter();

            btnAdicionar.Click += (s, e) =>
            {
                EditPedido(true);
            };

            btnEditar.Click += (s, e) => EditPedido();
            GridLista.DoubleClick += (s, e) => EditPedido();

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            GridLista.CellFormatting += (s, e) =>
            {
                foreach (DataGridViewRow row in GridLista.Rows)
                {
                    if (row.Cells[7].Value.ToString().Contains("Finalizado") || row.Cells[7].Value.ToString().Contains("Autorizada"))
                    {
                        row.Cells[7].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[7].Style.ForeColor = Color.White;
                        row.Cells[7].Style.BackColor = Color.FromArgb(139, 215, 146);
                    }
                    else if (row.Cells[7].Value.ToString().Contains("Cancelada"))
                    {
                        row.Cells[7].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[7].Style.ForeColor = Color.White;
                        row.Cells[7].Style.BackColor = Color.FromArgb(0, 150, 230);
                    }
                    else
                    {
                        row.Cells[7].Style.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        row.Cells[7].Style.ForeColor = Color.White;
                        row.Cells[7].Style.BackColor = Color.FromArgb(255, 89, 89);
                    }
                }
            };

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _cPedido.GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text, noFilterData.Checked);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, noFilterData.Checked, dataTable, BuscarPessoa.Text);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => BuscarPessoa.Invoke((MethodInvoker)delegate
            {
                Filter();
                Loading.Visible = false;
                GridLista.Visible = true;
            });

            BuscarPessoa.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                Filter();
            };

            imprimir.Click += async (s, e) => await RenderizarAsync();

            if (Home.pedidoPage == "Vendas")
                btnVideoAjuda.Click += (s, e) => Support.Video("https://www.youtube.com/watch?v=Z2pkMEAAk4Q");
            if (Home.pedidoPage == "Notas" || Home.pedidoPage == "Cupons" || Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações")
                btnVideoAjuda.Visible = false;
        }

        private async Task RenderizarAsync()
        {
            int excluir = 0;

            if (filterRemovido.Checked)
                excluir = 1;

            //IEnumerable<dynamic> dados = await _cPedido.GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text, noFilterData.Checked, BuscarPessoa.Text, excluir, Validation.ConvertToInt32(BuscaID.Text), Validation.ConvertToInt32(Status.SelectedValue), Validation.ConvertToInt32(Usuarios.SelectedValue), collectionItem.Lookup(produtoId.Text));

            IEnumerable<dynamic> dados;
            switch (Home.pedidoPage)
            {
                case "Cupons":
                case "Notas":
                    dados = await _cPedido.GetDataTableNotas(Home.pedidoPage, dataInicial.Text, dataFinal.Text, noFilterData.Checked, BuscarPessoa.Text, excluir, Validation.ConvertToInt32(BuscaID.Text), Validation.ConvertToInt32(Status.SelectedValue), Validation.ConvertToInt32(Usuarios.SelectedValue));
                    break;

                default:
                    dados = await _cPedido.GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text, noFilterData.Checked, BuscarPessoa.Text, excluir, Validation.ConvertToInt32(BuscaID.Text), Validation.ConvertToInt32(Status.SelectedValue), Validation.ConvertToInt32(Usuarios.SelectedValue), collectionItem.Lookup(produtoId.Text));
                    break;
            }

            ArrayList data = new ArrayList();
            double total = 0;
            foreach (var item in dados)
            {
                var statusNfePedido = "";

                if (Home.pedidoPage == "Compras")
                    statusNfePedido = item.STATUS == 0 ? "Pendente" : item.STATUS == 1 ? @"Finalizado\Pago" : item.STATUS == 2 ? "Pagamento Pendente" : "N/D";

                if (Home.pedidoPage == "Vendas")
                    statusNfePedido = item.STATUS == 0 ? "Pendente" : item.STATUS == 1 ? @"Finalizado\Recebido" : item.STATUS == 2 ? "Recebimento Pendente" : "N/D";

                if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações")
                    statusNfePedido = item.STATUS == 0 ? "Pendente" : item.STATUS == 1 ? "Finalizado" : "N/D";

                #region N° SEFAZ

                string n_cfe = "", n_nfe = "";
                var dataNota = Home.pedidoPage == "Cupons" ? _cPedido.GetDadosNota(0, item.IDNOTA) : Home.pedidoPage == "Notas" ? _cPedido.GetDadosNota(0, item.IDNOTA) : _cPedido.GetDadosNota(item.ID);

                if (dataNota == null)
                    return;

                foreach (dynamic item2 in dataNota)
                {
                    if (item2.TIPONFE == "NFe")
                    {
                        if (Home.pedidoPage == "Vendas" && item2.STATUSNFE == "Cancelada")
                            n_nfe = "";
                        else
                            n_nfe = item2.NFE + " / " + item2.SERIE;

                        if (Home.pedidoPage == "Notas")
                            statusNfePedido = item2.STATUSNFE == null ? "Pendente" : item2.STATUSNFE;
                    }

                    if (item2.TIPONFE == "CFe")
                    {
                        if (Home.pedidoPage == "Vendas" && item2.STATUSNFE == "Cancelado")
                            n_cfe = "";
                        else
                            n_cfe = item2.NFE;

                        if (Home.pedidoPage == "Cupons")
                            statusNfePedido = item2.STATUSNFE == null ? "Pendente" : item2.STATUSNFE;
                    }
                }

                #endregion N° SEFAZ


                total = Validation.ConvertToDouble(item.TOTAL) + Validation.ConvertToDouble(item.TOTAL);
                data.Add(new
                {
                    //.Select("pedido.id", "pedido.emissao", "pedido.total", "pessoa.nome", "colaborador.nome as colaborador", "usuario.nome as usuario", "pedido.criado", "pedido.excluir")
                    ID = item.ID,
                    EMISSAO = Validation.ConvertDateToForm(item.EMISSAO),
                    CLIENTE = item.NOME,
                    TOTAL = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL)),
                    n_nfe = n_nfe,
                    n_cfe = n_cfe,
                    COLABORADOR = item.COLABORADOR,
                    CRIADO = item.CRIADO,
                    STATUS = statusNfePedido,
                    CHAVEDEACESSO = item.CHAVEDEACESSO
                });
            }

            IEnumerable<dynamic> dados2 = await _cPedido.GetDataTableTotaisPedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text, BuscarPessoa.Text, excluir, Validation.ConvertToInt32(BuscaID.Text), Validation.ConvertToInt32(Status.SelectedValue), Validation.ConvertToInt32(Usuarios.SelectedValue), collectionItem.Lookup(produtoId.Text));
            ArrayList data2 = new ArrayList();
            foreach (var item in dados2)
            {
                data2.Add(new
                {
                    ID = item.ID,
                    TOTAL = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL)),
                });
            }

            IEnumerable<dynamic> dados3 = _cPedido.GetTotaisNota("NFe", dataInicial.Text, dataFinal.Text, noFilterData.Checked, Validation.ConvertToInt32(Status.SelectedValue));
            ArrayList data3 = new ArrayList();
            foreach (var item in dados3)
            {
                data3.Add(new
                {
                    ID = item.ID,
                    TOTAL = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL)),
                });
            }
            
            IEnumerable<dynamic> dados4 = _cPedido.GetTotaisNota("CFe", dataInicial.Text, dataFinal.Text, noFilterData.Checked, Validation.ConvertToInt32(Status.SelectedValue));
            ArrayList data4 = new ArrayList();
            foreach (var item in dados4)
            {
                data4.Add(new
                {
                    ID = item.ID,
                    TOTAL = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL)),
                });
            }

            if (!File.Exists($@"{Program.PATH_BASE}\html\Pedidos.html"))
            {
                Alert.Message("Opps", "Não encontramos os arquivos base de relatório", Alert.AlertType.warning);
                return;
            }
            
            Template html;
            if (Home.pedidoPage == "Notas" || Home.pedidoPage == "Cupons")
                html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Notas.html"));
            else
                html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Pedidos.html"));

            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                Total = data2,
                TotalNotas = data3,
                TotalCupons = data4,
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                dataInicial = dataInicial.Text,
                dataFinal = dataFinal.Text,
                Titulo = Home.pedidoPage
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}
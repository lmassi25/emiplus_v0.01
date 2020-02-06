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

                case "Devoluções":
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

        private void AutoCompleteItens()
        {
            var item = _mItem.Query().Select("id", "nome").Where("excluir", 0).Where("tipo", "Produtos").Get();

            foreach (var itens in item)
            {
                if (!String.IsNullOrEmpty(itens.NOME))
                    collectionItem.Add(itens.NOME, itens.ID);
            }

            produtoId.AutoCompleteCustomSource = collectionItem;
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

            if (filterRemovido.Checked)
                excluir = 1;

            await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, null, BuscarPessoa.Text, excluir, Validation.ConvertToInt32(BuscaID.Text), Validation.ConvertToInt32(Status.SelectedValue), Validation.ConvertToInt32(Usuarios.SelectedValue), collectionItem.Lookup(produtoId.Text));
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
                    Nota nota = new Nota();
                    nota.ShowDialog();
                    return;
                }
                else
                {
                    AddPedidos.Id = 0;
                    AddPedidos NovoPedido = new AddPedidos();
                    NovoPedido.ShowDialog();
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
                    DetailsPedido detailsPedido = new DetailsPedido();
                    detailsPedido.ShowDialog();
                }
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
                DataTableStart();
                BuscarPessoa.Select();
                AutoCompletePessoas();
                AutoCompleteUsers();
                AutoCompleteItens();

                dataInicial.Text = DateTime.Now.ToString();
                dataFinal.Text = DateTime.Now.ToString();

                var status = new ArrayList();
                status.Add(new { ID = 99, NOME = "Todos" });

                if (Home.pedidoPage == "Notas")
                {
                    status.Add(new { ID = 1, NOME = "Pendentes" });
                    status.Add(new { ID = 2, NOME = "Autorizadas" });
                    status.Add(new { ID = 3, NOME = "Canceladas" });
                }
                else if (Home.pedidoPage == "Orçamentos" || Home.pedidoPage == "Devoluções" || Home.pedidoPage == "Consignações")
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

                filterTodos.Checked = true;
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
                    dataTable = await _cPedido.GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, dataTable, BuscarPessoa.Text);

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

            imprimir.Click += async (s, e) =>
            {
                if (Home.pedidoPage == "Notas" || Home.pedidoPage == "Cupons")
                {
                    Alert.Message("Ação não permitida", "Relatório não disponível", Alert.AlertType.warning);
                    return;
                }

                await RenderizarAsync();
            };

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

            IEnumerable<dynamic> dados = await _cPedido.GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text, BuscarPessoa.Text, excluir, Validation.ConvertToInt32(BuscaID.Text), Validation.ConvertToInt32(Status.SelectedValue), Validation.ConvertToInt32(Usuarios.SelectedValue), collectionItem.Lookup(produtoId.Text));

            ArrayList data = new ArrayList();
            foreach (var item in dados)
            {
                data.Add(new
                {
                    //.Select("pedido.id", "pedido.emissao", "pedido.total", "pessoa.nome", "colaborador.nome as colaborador", "usuario.nome as usuario", "pedido.criado", "pedido.excluir")
                    ID = item.ID,
                    EMISSAO = Validation.ConvertDateToForm(item.EMISSAO),
                    CLIENTE = item.NOME,
                    TOTAL = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL)),
                    COLABORADOR = item.COLABORADOR,
                    CRIADO = item.CRIADO
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

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\Pedidos.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                INCLUDE_PATH = Program.PATH_BASE,
                URL_BASE = Program.PATH_BASE,
                Data = data,
                Total = data2,
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
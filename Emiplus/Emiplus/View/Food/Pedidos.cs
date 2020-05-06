using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.Model;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using SqlKata.Execution;
using Pedido = Emiplus.Model.Pedido;

namespace Emiplus.View.Food
{
    public partial class Pedidos : Form
    {
        private readonly Controller.Titulo _controllerTitulo = new Controller.Titulo();
        private readonly KeyedAutoCompleteStringCollection collectionClientes = new KeyedAutoCompleteStringCollection();

        public Pedidos()
        {
            InitializeComponent();
            Eventos();
        }

        private void AutoCompleteClientes()
        {
            var clientes = new Pessoa().GetAll();
            if (clientes.Count > 0)
            {
                foreach (dynamic itens in clientes)
                    if (itens.Nome != "Novo registro" || itens.Nome != "SELECIONE")
                        collectionClientes.Add(itens.Nome, Validation.ConvertToInt32(itens.Id));

                BuscarPessoa.AutoCompleteCustomSource = collectionClientes;
            }
        }

        private void LoadEntregadores()
        {
            Entregador.DataSource = new Pessoa().GetAll("Entregadores");
            Entregador.DisplayMember = "Nome";
            Entregador.ValueMember = "Id";
        }

        private void LoadStatus()
        {
            var status = new ArrayList
            {
                new {Id = "0", Nome = "Selecione"},
                new {Id = "FAZENDO", Nome = "Fazendo"},
                new {Id = "PRONTO", Nome = "Pronto / Para Retirar"},
                new {Id = "ENTREGANDO", Nome = "Saiu para Entrega"},
                new {Id = "FINALIZADO", Nome = "Finalizado / Entregue"},
                new {Id = "0", Nome = "Todos Pedidos"}
            };

            Status.DataSource = status;
            Status.DisplayMember = "Nome";
            Status.ValueMember = "Id";

            var statusPagamento = new ArrayList
            {
                new {Id = "", Nome = "Selecione"},
                new {Id = "RECEBER", Nome = "Receber"},
                new {Id = "RECEBIDO", Nome = "Recebido"}
            };
        }

        private string GetStatus(string status)
        {
            switch (status)
            {
                case "FAZENDO":
                    return "Fazendo";
                case "PRONTO":
                    return "Pronto / Para Retirar";
                case "ENTREGANDO":
                    return "Saiu para Entrega";
                case "FINALIZADO":
                    return "Finalizado / Entregue";
            }

            return "";
        }

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 8;
            
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            table.Columns[0].Name = "ID";
            table.Columns[0].Visible = false;

            table.Columns[1].Name = "Venda";
            table.Columns[1].Width = 100;
            table.Columns[1].Visible = true;

            table.Columns[2].Name = "N°";
            table.Columns[2].Width = 80;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Cliente";
            table.Columns[3].Width = 100;
            table.Columns[3].MinimumWidth = 100;
            table.Columns[3].Visible = true;

            table.Columns[4].Name = "Entregador";
            table.Columns[4].Width = 100;
            table.Columns[4].MinimumWidth = 120;
            table.Columns[4].Visible = true;

            table.Columns[5].Name = "Status";
            table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[5].Width = 130;
            table.Columns[5].Visible = true;

            table.Columns[6].Name = "Status Pagamento";
            table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.Columns[6].Width = 130;
            table.Columns[6].Visible = true;

            table.Columns[7].Name = "Valor";
            table.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[7].Width = 100;
            table.Columns[7].Visible = true;
        }

        private void LoadTable(DataGridView table)
        {
            table.Rows.Clear();

            var pedidos = new Pedido().Query();
            pedidos.WhereFalse("excluir");
            pedidos.Where(q => q.Where("tipo", "Delivery").OrWhere("tipo", "Balcao").OrWhere("campof", "MESA"));
            pedidos.OrderByDesc("id");

            #region Filtros

            if (!string.IsNullOrEmpty(BuscaID.Text))
                pedidos.Where("id", BuscaID.Text);

            if (Entregador.SelectedValue.ToString() != "0")
                pedidos.Where("id_transportadora", Entregador.SelectedValue.ToString());

            if (Status.SelectedValue.ToString() != "0")
                pedidos.Where("campoa", Status.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(BuscarPessoa.Text))
                pedidos.Where("cliente", collectionClientes.Lookup(BuscarPessoa.Text));

            if (!noFilterData.Checked)
            {
                pedidos.Where("emissao", ">=", Validation.ConvertDateToSql(dataInicial.Text));
                pedidos.Where("emissao", "<=", Validation.ConvertDateToSql(dataFinal.Text));
            }

            #endregion
            
            IEnumerable<Pedido> data = pedidos.Get<Pedido>();
            if (data.Any())
                foreach (var pedido in data)
                {
                    var cliente = "";
                    if (GetDataPessoa(pedido.Cliente) != null)
                        cliente = GetDataPessoa(pedido.Cliente).Nome;

                    var entregador = "";
                    if (GetDataPessoa(pedido.Id_Transportadora) != null)
                        entregador = GetDataPessoa(pedido.Id_Transportadora).Nome;

                    string statusPagamento = _controllerTitulo.GetLancados(pedido.Id) < _controllerTitulo.GetTotalPedido(pedido.Id) ? "Receber" : "Recebido";

                    table.Rows.Add(
                        pedido.Id,
                        pedido.Tipo,
                        pedido.Id,
                        cliente,
                        entregador,
                        GetStatus(pedido.campoa),
                        statusPagamento,
                        Validation.FormatPrice(pedido.Total, true)
                    );
                }

            table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private Pessoa GetDataPessoa(int id)
        {
            var data = new Pessoa().FindAll().WhereFalse("excluir").Where("id", id).FirstOrDefault<Pessoa>();
            return data;
        }

        private void EditarPedido()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                DetailsPedido.idPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                Home.pedidoPage = GridLista.SelectedRows[0].Cells["Venda"].Value.ToString();
                OpenForm.Show<DetailsPedido>(this);
            }
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                dataInicial.Text = DateTime.Now.ToString();
                dataFinal.Text = DateTime.Now.ToString();

                AutoCompleteClientes();
                LoadEntregadores();
                LoadStatus();

                SetHeadersTable(GridLista);
                LoadTable(GridLista);
            };

            btnSearch.Click += (s, e) => LoadTable(GridLista);

            GridLista.DoubleClick += (s, e) => EditarPedido();
            btnEditar.Click += (s, e) => EditarPedido();

            btnExit.Click += (s, e) => Close();
            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }
    }
}
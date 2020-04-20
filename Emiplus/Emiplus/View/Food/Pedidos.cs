using Emiplus.Data.Helpers;
using Emiplus.Data.SobreEscrever;
using Emiplus.View.Common;
using SqlKata.Execution;
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

namespace Emiplus.View.Food
{
    public partial class Pedidos : Form
    {
        private KeyedAutoCompleteStringCollection collectionClientes = new KeyedAutoCompleteStringCollection();

        public Pedidos()
        {
            InitializeComponent();
            Eventos();
        }

        private void AutoCompleteClientes()
        {
            ArrayList clientes = new Model.Pessoa().GetAll("Clientes");
            if (clientes.Count > 0) {
                foreach (dynamic itens in clientes)
                {
                    if (itens.Nome != "Novo registro" || itens.Nome != "SELECIONE")
                        collectionClientes.Add(itens.Nome, Validation.ConvertToInt32(itens.Id));
                }
                
                BuscarPessoa.AutoCompleteCustomSource = collectionClientes;
            }
        }

        private void LoadEntregadores()
        {
            Entregador.DataSource = new Model.Pessoa().GetAll("Entregadores");
            Entregador.DisplayMember = "Nome";
            Entregador.ValueMember = "Id";
        }

        private void LoadStatus()
        {
            ArrayList status = new ArrayList();
            status.Add(new { Id = "0", Nome = "Selecione" });
            status.Add(new { Id = "FAZENDO", Nome = "Fazendo" });
            status.Add(new { Id = "PRONTO", Nome = "Pronto / Para Retirar" });
            status.Add(new { Id = "ENTREGANDO", Nome = "Saiu para Entrega" });
            status.Add(new { Id = "FINALIZADO", Nome = "Finalizado / Entregue" });

            Status.DataSource = status;
            Status.DisplayMember = "Nome";
            Status.ValueMember = "Id";
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
        
        private void SetHeadersTable(DataGridView Table)
        {
            Table.ColumnCount = 7;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Venda";
            Table.Columns[1].Width = 100;
            Table.Columns[1].Visible = true;

            Table.Columns[2].Name = "N°";
            Table.Columns[2].Width = 80;
            Table.Columns[2].Visible = true;

            Table.Columns[3].Name = "Cliente";
            Table.Columns[3].Width = 100;
            Table.Columns[3].MinimumWidth = 100;
            Table.Columns[3].Visible = true;

            Table.Columns[4].Name = "Entregador";
            Table.Columns[4].Width = 100;
            Table.Columns[4].MinimumWidth = 120;
            Table.Columns[4].Visible = true;

            Table.Columns[5].Name = "Status";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns[5].Width = 130;
            Table.Columns[5].Visible = true;

            Table.Columns[6].Name = "Valor";
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[6].Width = 100;
            Table.Columns[6].Visible = true;
        }

        private void LoadTable(DataGridView Table)
        {
            Table.Rows.Clear();

            var pedidos = new Model.Pedido().Query();
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

            IEnumerable<Model.Pedido> data = pedidos.Get<Model.Pedido>();
            if (data.Count() > 0)
            {
                foreach (Model.Pedido pedido in data)
                {
                    string Cliente = "";
                    if (GetDataPessoa(pedido.Cliente) != null)
                        Cliente = GetDataPessoa(pedido.Cliente).Nome;

                    string Entregador = "";
                    if (GetDataPessoa(pedido.Id_Transportadora) != null)
                        Entregador = GetDataPessoa(pedido.Id_Transportadora).Nome;

                    Table.Rows.Add(
                        pedido.Id,
                        pedido.Tipo,
                        pedido.Id,
                        Cliente,
                        Entregador,
                        GetStatus(pedido.campoa),
                        Validation.FormatPrice(pedido.Total, true)
                    );
                }
            }

            Table.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private Model.Pessoa GetDataPessoa(int id)
        {
            Model.Pessoa data = new Model.Pessoa().FindAll().WhereFalse("excluir").Where("id", id).FirstOrDefault<Model.Pessoa>();
            if (data != null)
                return data;
            
            return null;
        }

        private void EditarPedido()
        {
            if (GridLista.SelectedRows.Count > 0)
            {
                Comercial.DetailsPedido.idPedido = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                Home.pedidoPage = GridLista.SelectedRows[0].Cells["Venda"].Value.ToString();
                OpenForm.Show<Comercial.DetailsPedido>(this);
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

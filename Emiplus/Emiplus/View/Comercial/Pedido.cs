using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Fiscal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Controller.Pedido _cPedido = new Controller.Pedido();

        private IEnumerable<dynamic> dataTable;
        private BackgroundWorker WorkerBackground = new BackgroundWorker();

        Timer timer = new Timer(Configs.TimeLoading);

        public Pedido()
        {
            InitializeComponent();
            Eventos();

            label3.Text = Home.pedidoPage;
            label1.Text = Home.pedidoPage;

            if (Home.pedidoPage == "Orçamentos")
                label2.Text = "Gerencie os orçamenos aqui! Adicione, edite ou delete um orçamento.";
            else if (Home.pedidoPage == "Consignações")
                label2.Text = "Gerencie as consignações aqui! Adicione, edite ou delete uma consignação.";
            else if (Home.pedidoPage == "Devoluções")
                label2.Text = "Gerencie as devoluções aqui! Adicione, edite ou delete uma devolução.";
            else if (Home.pedidoPage == "Compras")
                label2.Text = "Gerencie as compras aqui! Adicione, edite ou delete uma compra.";
            else if (Home.pedidoPage == "Notas")
                label2.Text = "Gerencie as Notas aqui! Adicione, edite ou delete uma Nota.";

            dataInicial.Text = DateTime.Now.ToString();
            dataFinal.Text = DateTime.Now.ToString();
        }

        private void DataTableStart()
        {
            search.Select();

            GridLista.Visible = false;
            Loading.Size = new System.Drawing.Size(GridLista.Width, GridLista.Height);
            Loading.Visible = true;
            WorkerBackground.RunWorkerAsync();
        }

        private async void Filter() => await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, null, search.Text);

        private void EditPedido(bool create = false)
        {
            if (create)
            {
                if (Home.pedidoPage == "Notas")
                {
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
            else if (GridLista.SelectedRows.Count > 0)
            {
                if (Home.pedidoPage == "Notas")
                {
                    Nota.Id = Convert.ToInt32(GridLista.SelectedRows[0].Cells["ID"].Value);
                    Nota nota = new Nota();
                    nota.ShowDialog();
                }
                else
                {
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
                    EditPedido();
                    break;
            }
        }

        private void Eventos()
        {
            Load += (s, e) => DataTableStart();

            search.KeyDown += KeyDowns;
            search.TextChanged += (s, e) =>
            {
                timer.Stop();
                timer.Start();
                Loading.Visible = true;
                GridLista.Visible = false;
            };

            btnSearch.Click += (s, e) => Filter();

            btnAdicionar.Click += (s, e) =>
            {
                if (Home.pedidoPage == "Orçamentos")
                    Home.pedidoPage = "Orçamentos";
                
                EditPedido(true);
            };

            btnEditar.Click += (s, e) => EditPedido();
            GridLista.DoubleClick += (s, e) =>EditPedido();
            GridLista.KeyDown += KeyDowns;

            btnExit.Click += (s, e) => Close();
            label5.Click += (s, e) => Close();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");

            using (var b = WorkerBackground)
            {
                b.DoWork += async (s, e) =>
                {
                    dataTable = await _cPedido.GetDataTablePedidos(Home.pedidoPage, dataInicial.Text, dataFinal.Text);
                };

                b.RunWorkerCompleted += async (s, e) =>
                {
                    await _cPedido.SetTablePedidos(GridLista, Home.pedidoPage, dataInicial.Text, dataFinal.Text, dataTable, search.Text);

                    Loading.Visible = false;
                    GridLista.Visible = true;
                };
            }

            timer.AutoReset = false;
            timer.Elapsed += (s, e) => search.Invoke((MethodInvoker)delegate {
                Filter();
                Loading.Visible = false;
                GridLista.Visible = true;
            });

            search.Enter += async (s, e) =>
            {
                await Task.Delay(100);
                Filter();
            };
        }
    }
}

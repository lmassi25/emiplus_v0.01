using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.View.Common;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class Mesa : Form
    {
        private readonly Model.Pedido _mPedido = new Model.Pedido();
        private readonly PedidoItem _mPedidoItem = new PedidoItem();

        public Mesa()
        {
            InitializeComponent();
            Eventos();
        }

        public static string nrMesa { get; set; }

        private void SetHeadersTable(DataGridView table)
        {
            table.ColumnCount = 4;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, table,
                new object[] {true});
            table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            table.RowHeadersVisible = false;

            table.Columns[0].Name = "ID";
            table.Columns[0].Visible = false;

            table.Columns[1].Name = "Item";
            table.Columns[1].Width = 150;
            table.Columns[1].Visible = true;

            table.Columns[2].Name = "Valor";
            table.Columns[2].Width = 80;
            table.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            table.Columns[2].Visible = true;

            table.Columns[3].Name = "Garçom";
            table.Columns[3].Width = 100;
            table.Columns[3].Visible = true;
        }

        private async Task SetContentTableAsync(DataGridView table)
        {
            SetHeadersTable(table);

            table.Rows.Clear();

            var data = new PedidoItem().FindAll().Where("mesa", nrMesa).WhereFalse("excluir").Where("pedido", 0).Get();
            if (data != null)
                foreach (var item in data)
                {
                    int user = item.USUARIO ?? 0;
                    var garcom = new Usuarios().FindAll().Where("id_user", user).WhereFalse("excluir")
                        .FirstOrDefault<Usuarios>();

                    table.Rows.Add(
                        item.ID,
                        item.XPROD,
                        Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true),
                        Validation.FirstCharToUpper(garcom.Nome)
                    );
                }

            table.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            Masks.SetToUpper(this);

            Shown += async (s, e) =>
            {
                label1.Text = $@"Mesa: {nrMesa}";

                await SetContentTableAsync(GridLista);

                var sumData = new PedidoItem().Query().SelectRaw("sum(total) as total").Where("mesa", nrMesa)
                    .WhereFalse("excluir").Where("pedido", 0).FirstOrDefault();
                double total = Validation.ConvertToDouble(sumData.TOTAL) ?? 0;

                var tempoMesa = new PedidoItem().Query().Where("mesa", nrMesa).WhereFalse("excluir").Where("pedido", 0)
                    .FirstOrDefault<PedidoItem>();
                if (tempoMesa != null)
                {
                    var date = DateTime.Now;
                    var hourMesa = date.AddHours(-tempoMesa.Criado.Hour);
                    var minMesa = date.AddMinutes(-tempoMesa.Criado.Minute);

                    tempo.Text = $@"{hourMesa.Hour}h {minMesa.Minute}m";
                }

                txtQtd.Text = GridLista.Rows.Count.ToString();
                txtTotal.Text = $@"Valor Total: {Validation.FormatPrice(total, true)}";
            };

            btnFechar.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!",
                    "Você está prestes a fechar uma mesa, ao continuar não será possível voltar!" +
                    Environment.NewLine + "Deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (!result)
                    return;

                if (nrMesa != "0")
                {
                    _mPedido.Id = 0;
                    _mPedido.Excluir = 0;
                    _mPedido.Tipo = "Vendas";
                    _mPedido.campof = "MESA";
                    _mPedido.Cliente = 1;
                    _mPedido.Save(_mPedido);
                    var idPedido = _mPedido.GetLastId();

                    var dataMesa = _mPedidoItem.FindAll().Where("mesa", nrMesa).WhereFalse("excluir").Where("pedido", 0)
                        .Get();
                    if (dataMesa != null)
                        foreach (var item in dataMesa)
                        {
                            int ID = item.ID;
                            var update = _mPedidoItem.FindById(ID).FirstOrDefault<PedidoItem>();
                            update.Pedido = idPedido;
                            update.Save(update);
                        }

                    Home.pedidoPage = "Vendas";
                    AddPedidos.Id = idPedido;
                    AddPedidos.PDV = false;
                    var NovoPedido = new AddPedidos {TopMost = true};
                    NovoPedido.ShowDialog();
                }
                else
                {
                    Alert.Message("Oppss", "Selecione uma mesa válida!", Alert.AlertType.warning);
                }
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
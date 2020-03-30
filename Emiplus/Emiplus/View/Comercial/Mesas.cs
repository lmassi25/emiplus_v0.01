using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Layout;

namespace Emiplus.View.Comercial
{
    public partial class Mesas : Form
    {
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.PedidoItem _mPedidoItem = new Model.PedidoItem();

        private List<int> checks = new List<int>();
        
        public Mesas()
        {
            InitializeComponent();
            Eventos();
        }

        private  void Eventos()
        {
            Shown += (s, e) =>
            {
                var mesas = _mPedidoItem.FindAll(new[] { "mesa" }).Where("pedido", 0).GroupBy("mesa").Get();
                if (mesas != null)
                {
                    foreach (var mesa in mesas)
                    {
                        VisualPanel panel = new VisualPanel();
                        panel.Width = 140;
                        panel.Height = 50;
                        panel.BackColorState.Enabled = Color.FromArgb(255, 184, 34);
                        panel.Border.Color = Color.FromArgb(228, 164, 28);
                        panel.Border.HoverColor = Color.FromArgb(228, 164, 28);
                        panel.Name = $"{mesa.MESA}mesa";
                        panel.Cursor = Cursors.Hand;

                        Label label = new Label();
                        label.AutoSize = false;
                        label.Text = mesa.MESA.ToString();
                        label.Width = 110;
                        label.Height = 48;
                        label.Location = new Point(27, 1);
                        label.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        label.ForeColor = Color.White;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.Click += actionMesa;
                        label.Name = $"{mesa.MESA}";
                        panel.Controls.Add(label);

                        CheckBox check = new CheckBox();
                        check.Checked = false;
                        check.Name = $"{mesa.MESA}check";
                        check.Location = new Point(6, 15);
                        check.Click += actionCheck;

                        panel.Controls.Add(check);

                        flowLayout.Controls.Add(panel);
                    }
                }
            };

            btnFechar.Click += (s, e) =>
            {
                if (checks.Count > 0)
                {
                    _mPedido.Id = 0;
                    _mPedido.Excluir = 0;
                    _mPedido.Tipo = "Vendas";
                    _mPedido.Cliente = 1;
                    _mPedido.Save(_mPedido);
                    int idPedido = _mPedido.GetLastId();

                    double valorTotal = 0;
                    foreach (int mesa in checks)
                    {
                        var dataMesa = _mPedidoItem.FindAll().Where("mesa", mesa).WhereFalse("excluir").Where("pedido", 0).Get();
                        if (dataMesa != null)
                        {
                            foreach (dynamic item in dataMesa)
                            {
                                valorTotal += Validation.ConvertToDouble(item.TOTAL);

                                int ID = item.ID;
                                Model.PedidoItem update = _mPedidoItem.FindById(ID).FirstOrDefault<Model.PedidoItem>();
                                update.Pedido = idPedido;
                                update.Save(update);
                            }
                        }
                    }

                    Model.Pedido updatePedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();
                    updatePedido.Produtos = valorTotal;
                    updatePedido.Total = valorTotal;
                    updatePedido.Save(updatePedido);

                    Home.pedidoPage = "Vendas";
                    AddPedidos.Id = idPedido;
                    AddPedidos.PDV = false;
                    AddPedidos NovoPedido = new AddPedidos();
                    NovoPedido.TopMost = true;
                    NovoPedido.ShowDialog();
                }
                else
                {
                    Alert.Message("Oppss", "Selecione uma mesa!", Alert.AlertType.warning);
                }
            };

            btnExit.Click += (s, e) => Close();
        }

        private void actionMesa(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            Mesa.nrMesa = label.Name;
            OpenForm.Show<Mesa>(this);
        }

        private void actionCheck(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            int idCheck = Validation.ConvertToInt32(check.Name.Replace("check", ""));
            if (check.Checked)
                checks.Add(idCheck);
            else
                checks.Remove(idCheck);

            if (checks.Count > 0)
                btnFechar.Visible = true;
            else
                btnFechar.Visible = false;
        }
    }
}

using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPayDevolucao : Form
    {
        public static int idPedido;
        
        private Model.Pedido _mPedido = new Model.Pedido();
        private Controller.Pedido _controller = new Controller.Pedido();

        public PedidoPayDevolucao()
        {
            InitializeComponent();
            Eventos();
        }

        private void Save()
        {
            if(String.IsNullOrEmpty(Voucher.Text))
            {
                Alert.Message("Ação não permitida", "Voucher inválido!", Alert.AlertType.warning);
                return;
            }

            _mPedido = _mPedido.FindByVoucher(Voucher.Text).FirstOrDefault<Model.Pedido>();

            if (_mPedido == null)
            {
                Alert.Message("Ação não permitida", "Voucher inválido!", Alert.AlertType.warning);
                return;
            }

            if (Validation.ConvertToInt32(_mPedido.Venda) > 0)
            {
                Alert.Message("Ação não permitida", "Voucher inválido!", Alert.AlertType.warning);
                return;
            }
            
            _mPedido.Venda = idPedido;
            if (_mPedido.Save(_mPedido))
            {
                DataTable();
                Voucher.Text = "";
            }
        }

        private void DataTable()
        {
            _controller.GetDataTableDevolucoes(GridDevolucoes, idPedido);
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Save();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;
            //KeyDown += KeyDowns; 
            //btnSalvar.KeyDown += KeyDowns;
            //btnCancelar.KeyDown += KeyDowns;
            //porcentagem.KeyDown += KeyDowns;
            //dinheiro.KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                DataTable();
            };

            btnSalvar.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
        }
    }
}

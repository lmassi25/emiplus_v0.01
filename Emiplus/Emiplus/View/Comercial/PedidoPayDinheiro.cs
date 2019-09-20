using Emiplus.Data.Helpers;
using Emiplus.Model;
using SqlKata.Execution;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class PedidoPayDinheiro : Form
    {
        public static int IdPedido = AddPedidos.Id;

        private Model.Pedido _mPedido = new Model.Pedido();
        private Titulo _mPagamento = new Titulo();
        private Controller.Titulo _cPagamento = new Controller.Titulo();

        public static bool Success = false;


        public PedidoPayDinheiro()
        {
            InitializeComponent();

            btnDoisReais.Click += (s, e) => { Dinheiro.Text = "2,00"; };
            btnCincoReais.Click += (s, e) => { Dinheiro.Text = "5,00"; };
            btnDezReais.Click += (s, e) => { Dinheiro.Text = "10,00"; };
            btnVinteReais.Click += (s, e) => { Dinheiro.Text = "20,00"; };
            btnCinquentaReais.Click += (s, e) => { Dinheiro.Text = "50,00"; };
            btnCemReais.Click += (s, e) => { Dinheiro.Text = "100,00"; };
            btnLimpar.Click += (s, e) => { Dinheiro.Clear(); };
            //btnFaltando.Click += (s, e) => { Dinheiro.Text = _cPagamento.GetRestante(IdPedido).ToString(); btnFaltando.Text = "[Enter] 00,00 (Faltando)"; };

            btnDoisReais.KeyDown += KeyDowns;
            btnCincoReais.KeyDown += KeyDowns;
            btnDezReais.KeyDown += KeyDowns;
            btnVinteReais.KeyDown += KeyDowns;
            btnCinquentaReais.KeyDown += KeyDowns;
            btnCemReais.KeyDown += KeyDowns;
            btnLimpar.KeyDown += KeyDowns;
            //btnFaltando.KeyDown += KeyDowns;
            btnCancelar.KeyDown += KeyDowns;
            btnSalvar.KeyDown += KeyDowns;

            //btnFaltando.Text = $"[Enter] {Validation.FormatPrice(_cPagamento.GetRestante(IdPedido))} (Faltando)";
        }

        private void AddPagamento()
        {
            _cPagamento.AddPagamento(IdPedido, 1, Dinheiro.Text, "0");

            //btnFaltando.Text = $"[Enter] {Validation.FormatPrice(_cPagamento.GetRestante(IdPedido))} (Faltando)";

           // PedidoPagamentos.atualiza = 1;

            Close();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    Dinheiro.Text = "2,00";
                    break;
                case Keys.B:
                    Dinheiro.Text = "5,00";
                    break;
                case Keys.C:
                    Dinheiro.Text = "10,00";
                    break;
                case Keys.D:
                    Dinheiro.Text = "20,00";
                    break;
                case Keys.E:
                    Dinheiro.Text = "50,00";
                    break;
                case Keys.F:
                    Dinheiro.Text = "100,00";
                    break;
                case Keys.G:
                    Dinheiro.Clear();
                    break;
                case Keys.Enter:
                    AddPagamento();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            AddPagamento();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

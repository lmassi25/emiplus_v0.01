using System.Windows.Forms;

namespace Emiplus.View.Comercial.TelasRecebimentos
{
    using Emiplus.Controller;
    using Emiplus.Data.Helpers;
    using SqlKata.Execution;

    public partial class TelaDinheiro : UserControl
    {
        public static int IdPedido = AddPedidos.Id;
        public static bool Success = false;

        private Titulo _cPagamento = new Titulo();

        public TelaDinheiro()
        {
            InitializeComponent();

            btnDoisReais.Click += (s, e) => { valor.Text = "2,00"; };
            btnCincoReais.Click += (s, e) => { valor.Text = "5,00"; };
            btnDezReais.Click += (s, e) => { valor.Text = "10,00"; };
            btnVinteReais.Click += (s, e) => { valor.Text = "20,00"; };
            btnCinquentaReais.Click += (s, e) => { valor.Text = "50,00"; };
            btnCemReais.Click += (s, e) => { valor.Text = "100,00"; };
            btnLimpar.Click += (s, e) => { valor.Clear(); };

            //btnFaltando.Click += (s, e) =>
            //{
            //    //valor.Text = _cPagamento.GetRestante(IdPedido).ToString();
            //    //btnFaltando.Text = "[Enter] 00,00 (Faltando)";
            //};

            btnCancelar.Click += (s, e) => { Hide(); };

            btnDoisReais.KeyDown += KeyDowns;
            btnCincoReais.KeyDown += KeyDowns;
            btnDezReais.KeyDown += KeyDowns;
            btnVinteReais.KeyDown += KeyDowns;
            btnCinquentaReais.KeyDown += KeyDowns;
            btnCemReais.KeyDown += KeyDowns;
            btnLimpar.KeyDown += KeyDowns;
            btnFaltando.KeyDown += KeyDowns;
            btnCancelar.KeyDown += KeyDowns;
            btnSalvar.KeyDown += KeyDowns;

            //if (_cPagamento.GetRestante(IdPedido) > 0)
            //{
            //    var r = Validation.FormatPrice(_cPagamento.GetRestante(IdPedido));
            //    btnFaltando.Text = $"[Enter] {r} (Faltando)";
            //}
        }

        public void AddPagamento()
        {
            _cPagamento.AddPagamento(IdPedido, 1, valor.Text, "0");
            btnFaltando.Text = $"[Enter] {Validation.FormatPrice(_cPagamento.GetRestante(IdPedido))} (Faltando)";
            Hide();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    valor.Text = "2,00";
                    break;
                case Keys.B:
                    valor.Text = "5,00";
                    break;
                case Keys.C:
                    valor.Text = "10,00";
                    break;
                case Keys.D:
                    valor.Text = "20,00";
                    break;
                case Keys.E:
                    valor.Text = "50,00";
                    break;
                case Keys.F:
                    valor.Text = "100,00";
                    break;
                case Keys.G:
                    valor.Clear();
                    break;
                case Keys.Enter:
                    AddPagamento();
                    break;
                case Keys.Escape:
                    Hide();
                    break;
            }
        }

        private void BtnDezReais_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(Validation.ConvertToDouble(new Model.Titulo().Query().SelectRaw("SUM(total) as total").Where("id", IdPedido).First().TOTAL).ToString());
        }
    }
}

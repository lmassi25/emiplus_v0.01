using System.Windows.Forms;

namespace Emiplus.View.Comercial.TelasRecebimentos
{
    using Emiplus.Controller;
    using Emiplus.Data.Helpers;

    public partial class TelaRecebimentos : UserControl
    {
        private int IdPedido = Comercial.Pedido.IdPedido;        

        public TelaRecebimentos()
        {
            InitializeComponent();

            //btnDoisReais.Click += (s, e) => { valor.Text = "2,00"; };
            //btnCincoReais.Click += (s, e) => { valor.Text = "5,00"; };
            //btnDezReais.Click += (s, e) => { valor.Text = "10,00"; };
            //btnVinteReais.Click += (s, e) => { valor.Text = "20,00"; };
            //btnCinquentaReais.Click += (s, e) => { valor.Text = "50,00"; };
            //btnCemReais.Click += (s, e) => { valor.Text = "100,00"; };
            //btnLimpar.Click += (s, e) => { valor.Clear(); };
            //btnCancelar.Click += (s, e) => { Hide(); };

            //btnDoisReais.KeyDown += KeyDowns;
            //btnCincoReais.KeyDown += KeyDowns;
            //btnDezReais.KeyDown += KeyDowns;
            //btnVinteReais.KeyDown += KeyDowns;
            //btnCinquentaReais.KeyDown += KeyDowns;
            //btnCemReais.KeyDown += KeyDowns;
            //btnLimpar.KeyDown += KeyDowns;
            //btnFaltando.KeyDown += KeyDowns;
            //btnCancelar.KeyDown += KeyDowns;
            //btnSalvar.KeyDown += KeyDowns;
        }

        public void AddPagamento()
        {
            //_cPagamento.AddPagamento(IdPedido, 1, valor.Text, "0");
            //Hide();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.A:
            //        valor.Text = "2,00";
            //        break;
            //    case Keys.B:
            //        valor.Text = "5,00";
            //        break;
            //    case Keys.C:
            //        valor.Text = "10,00";
            //        break;
            //    case Keys.D:
            //        valor.Text = "20,00";
            //        break;
            //    case Keys.E:
            //        valor.Text = "50,00";
            //        break;
            //    case Keys.F:
            //        valor.Text = "100,00";
            //        break;
            //    case Keys.G:
            //        valor.Clear();
            //        break;
            //    case Keys.Enter:
            //        AddPagamento();
            //        break;
            //    case Keys.Escape:
            //        Hide();
            //        break;
            //}
        }
    }
}

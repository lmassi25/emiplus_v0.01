using System.Collections.Generic;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesCfeCpf : Form
    {
        private Pessoa _mCliente = new Pessoa();
        private Model.Pedido _mPedido = new Model.Pedido();

        public OpcoesCfeCpf()
        {
            InitializeComponent();
            Eventos();
        }

        public static int idPedido { get; set; } // id pedido
        public static bool emitir { get; set; }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    Continuar();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Continuar()
        {
            if (nomeRS.Text != "Consumidor Final")
                _mPedido.cfe_nome = Validation.CleanStringForFiscal(nomeRS.Text);

            _mPedido.cfe_cpf = Validation.CleanStringForFiscal(cpfCnpj.Text.Replace(".", "").Replace(" ", ""));

            _mPedido.Save(_mPedido);

            if (emitir)
            {
                OpcoesCfeEmitir.idPedido = idPedido;
                var f = new OpcoesCfeEmitir {TopMost = true};
                f.Show();

                Close();
            }
            else
            {
                OpcoesCfe.idPedido = idPedido;
                var f = new OpcoesCfe {TopMost = true};
                f.Show();

                Close();
            }
        }

        /// <summary>
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            KeyPreview = true;
            KeyDown += KeyDowns;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                if (OpcoesCfe.tipo == "NFCe")
                {
                    pictureBox1.Image = Resources.nfce;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    label10.Text = @"Confirme as informações do cliente que serão enviadas ao NFCe";
                }

                _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

                if (_mPedido != null && _mPedido.Cliente > 0)
                    _mCliente = _mCliente.FindById(_mPedido.Cliente).FirstOrDefault<Pessoa>();

                pessoaJF.DataSource = new List<string> {"Física", "Jurídica"};

                nomeRS.Text = _mCliente.Nome ?? "";
                cpfCnpj.Text = _mCliente.CPF ?? "";
                pessoaJF.Text = _mCliente.Pessoatipo ?? "Física";

                if (!string.IsNullOrEmpty(_mPedido?.cfe_nome) && _mPedido.cfe_nome != "Consumidor Final")
                    nomeRS.Text = _mPedido.cfe_nome;

                if (!string.IsNullOrEmpty(_mPedido?.cfe_cpf))
                {
                    cpfCnpj.Text = _mPedido.cfe_cpf;

                    pessoaJF.SelectedItem = _mPedido.cfe_cpf.Length == 11 ? "Física" : "Jurídica";
                }
            };

            cpfCnpj.KeyPress += (s, e) =>
            {
                switch (pessoaJF.Text)
                {
                    case "Física":
                        Masks.MaskCPF(s, e);
                        break;
                    case "Jurídica":
                        Masks.MaskCNPJ(s, e);
                        break;
                }
            };

            btnSalvar.Click += (s, e) => { Continuar(); };

            btnCancelar.Click += (s, e) => { Close(); };
        }
    }
}
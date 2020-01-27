using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesCfeCpf : Form
    {
        public static int idPedido { get; set; } // id pedido
        public static bool emitir { get; set; }
        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Pessoa _mCliente = new Model.Pessoa();

        private string _msg;

        public OpcoesCfeCpf()
        {
            InitializeComponent();

            _mPedido = _mPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

            if (_mPedido != null && _mPedido.Cliente > 0)
                _mCliente = _mCliente.FindById(_mPedido.Cliente).FirstOrDefault<Model.Pessoa>();

            Eventos();
        }

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
                OpcoesCfeEmitir f = new OpcoesCfeEmitir();
                f.TopMost = true;
                f.Show();

                Close();
            }
            else
            {
                OpcoesCfe.idPedido = idPedido;
                OpcoesCfe f = new OpcoesCfe();
                f.TopMost = true;
                f.Show();

                Close();
            }
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            KeyPreview = true;
            KeyDown += KeyDowns;
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                pessoaJF.DataSource = new List<String> { "Física", "Jurídica" };

                nomeRS.Text = _mCliente.Nome ?? "";
                cpfCnpj.Text = _mCliente.CPF ?? "";
                pessoaJF.Text = _mCliente.Pessoatipo ?? "Física";

                if (!string.IsNullOrEmpty(_mPedido.cfe_nome) && _mPedido.cfe_nome != "Consumidor Final")
                    nomeRS.Text = _mPedido.cfe_nome;

                if (!String.IsNullOrEmpty(_mPedido.cfe_cpf))
                {
                    cpfCnpj.Text = _mPedido.cfe_cpf;

                    if (_mPedido.cfe_cpf.Length == 11)
                        pessoaJF.SelectedItem = "Física";
                    else
                        pessoaJF.SelectedItem = "Jurídica";
                }
            };

            cpfCnpj.KeyPress += (s, e) =>
            {
                if (pessoaJF.Text == "Física")
                    Masks.MaskCPF(s, e);

                if (pessoaJF.Text == "Jurídica")
                    Masks.MaskCNPJ(s, e);
            };

            btnSalvar.Click += (s, e) =>
            {
                Continuar();
            };

            btnCancelar.Click += (s, e) =>
            {
                Close();
            };
        }
    }
}
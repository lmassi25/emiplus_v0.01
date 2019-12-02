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
    public partial class OpcoesCfeCpf : Form
    {
        public static int idPedido { get; set; } // id pedido
        private Model.Pedido _mPedido = new Model.Pedido();        
        private Model.Pessoa _mCliente = new Model.Pessoa();

        public OpcoesCfeCpf()
        {
            InitializeComponent();

            _mPedido = _mPedido.FindById(idPedido).First<Model.Pedido>();
            
            if(_mPedido.Cliente > 0)            
                _mCliente = _mCliente.FindById(_mPedido.Cliente).First<Model.Pessoa>();

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
            if (String.IsNullOrEmpty(nomeRS.Text) && nomeRS.Text != "Consumidor Final")
                _mPedido.cfe_nome = Validation.CleanStringForFiscal(nomeRS.Text);

            if (String.IsNullOrEmpty(cpfCnpj.Text))
                _mPedido.cfe_cpf = Validation.CleanStringForFiscal(cpfCnpj.Text);

            _mPedido.Save(_mPedido);

            OpcoesCfe.idPedido = idPedido;
            OpcoesCfe f = new OpcoesCfe();
            f.Show();

            Close();
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            KeyPreview = true;
            KeyDown += KeyDowns;

            Load += (s, e) =>
            {
                pessoaJF.DataSource = new List<String> { "Física", "Jurídica" };

                nomeRS.Text = _mCliente?.Nome ?? "";
                cpfCnpj.Text = _mCliente?.CPF ?? "";
                pessoaJF.Text = _mCliente?.Pessoatipo ?? "Física";
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

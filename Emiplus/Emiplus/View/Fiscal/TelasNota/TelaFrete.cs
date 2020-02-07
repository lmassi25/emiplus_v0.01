using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaFrete : Form
    {
        private static int Id { get; set; } // id nota
        private int IdTransportadora { get; set; }

        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Pessoa _mTransportadora = new Model.Pessoa();
        private Model.Nota _mNota = new Model.Nota();

        public TelaFrete()
        {
            InitializeComponent();
            Id = Nota.Id;

            _mNota = _mNota.FindById(Id).FirstOrDefault<Model.Nota>();

            if (_mNota == null)
            {
                Alert.Message("Ação não permitida", "Referência de Pedido não identificada", Alert.AlertType.warning);
                return;
            }

            _mPedido = _mPedido.FindById(_mNota.id_pedido).FirstOrDefault<Model.Pedido>();

            DisableCampos();
            Eventos();
        }

        private void DisableCampos()
        {
            if (Nota.disableCampos)
            {
                tipo.Enabled = false;
                volumes.Enabled = false;
                pesoLiquido.Enabled = false;
                pesoBruto.Enabled = false;
                especie.Enabled = false;
                marca.Enabled = false;
                SelecionarTransportadora.Enabled = false;
                placa.Enabled = false;
                uf.Enabled = false;
                rntc.Enabled = false;
            }
        }

        private bool Validate()
        {
            return false;
        }

        private void LoadData()
        {
            var tipos = new ArrayList();
            //tipos.Add(new { Id = "9", Nome = "Sem frete" });
            //tipos.Add(new { Id = "0", Nome = "Por conta do emitente" });
            //tipos.Add(new { Id = "1", Nome = "Por conta do destinatário/remetente" });
            //tipos.Add(new { Id = "2", Nome = "Por conta de terceiros" });

            tipos.Add(new { Id = "0", Nome = "Contratação do Frete por conta do Remetente" });
            tipos.Add(new { Id = "1", Nome = "Contratação do Frete por conta do Destinatário" });
            tipos.Add(new { Id = "2", Nome = "Contratação do Frete por conta de Terceiros" });
            tipos.Add(new { Id = "3", Nome = "Transporte Próprio por conta do Remetente" });
            tipos.Add(new { Id = "4", Nome = "Transporte Próprio por conta do Destinatário" });
            tipos.Add(new { Id = "9", Nome = "Sem Ocorrência de Transporte" });

            tipo.DataSource = tipos;
            tipo.DisplayMember = "Nome";
            tipo.ValueMember = "Id";

            tipo.SelectedItem = _mPedido.TipoFrete > 0 ? _mPedido.TipoFrete : 9;
            volumes.Text = !String.IsNullOrEmpty(_mPedido.Volumes_Frete) ? _mPedido.Volumes_Frete : "";
            pesoLiquido.Text = !String.IsNullOrEmpty(_mPedido.PesoLiq_Frete) ? _mPedido.PesoLiq_Frete : "";
            pesoBruto.Text = !String.IsNullOrEmpty(_mPedido.PesoBruto_Frete) ? _mPedido.PesoBruto_Frete : "";
            especie.Text = !String.IsNullOrEmpty(_mPedido.Especie_Frete) ? _mPedido.Especie_Frete : "";
            marca.Text = !String.IsNullOrEmpty(_mPedido.Marca_Frete) ? _mPedido.Marca_Frete : "";

            LoadTransportadora();
        }

        private void LoadTransportadora()
        {
            _mTransportadora = new Model.Pessoa().FindById(_mPedido.Id_Transportadora).FirstOrDefault<Model.Pessoa>();
            if (_mTransportadora != null)
            {
                transportadoraSelecionada.Text = _mTransportadora.Nome;

                placa.Text = _mTransportadora.Transporte_placa;
                uf.Text = _mTransportadora.Transporte_uf;
                rntc.Text = _mTransportadora.Transporte_rntc;
            }
        }

        private void Eventos()
        {
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                LoadData();
                LoadTransportadora();

                if (_mNota.Status != "Pendente")
                {
                    progress5.Visible = false;
                    pictureBox1.Visible = false;
                    label9.Visible = false;
                }
            };

            SelecionarTransportadora.Click += (s, e) =>
            {
                PedidoModalTransportadora form = new PedidoModalTransportadora();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IdTransportadora = PedidoModalTransportadora.Id;

                    _mPedido.Id = _mNota.id_pedido;
                    _mPedido.Id_Transportadora = PedidoModalTransportadora.Id;
                    _mPedido.TipoFrete = Validation.ConvertToInt32(tipo.SelectedIndex);
                    _mPedido.Volumes_Frete = volumes.Text;
                    _mPedido.PesoLiq_Frete = pesoLiquido.Text;
                    _mPedido.PesoBruto_Frete = pesoBruto.Text;
                    _mPedido.Especie_Frete = especie.Text;
                    _mPedido.Marca_Frete = marca.Text;
                    _mPedido.Save(_mPedido);

                    LoadData();
                    LoadTransportadora();
                }
            };

            Next.Click += (s, e) =>
            {
                if (Validate())
                    return;

                _mPedido.Id = _mNota.id_pedido;
                _mPedido.TipoFrete = Validation.ConvertToInt32(tipo.SelectedIndex);
                _mPedido.Volumes_Frete = volumes.Text;
                _mPedido.PesoLiq_Frete = pesoLiquido.Text;
                _mPedido.PesoBruto_Frete = pesoBruto.Text;
                _mPedido.Especie_Frete = especie.Text;
                _mPedido.Marca_Frete = marca.Text;

                if (!Nota.disableCampos)
                    _mPedido.Save(_mPedido);

                if (_mPedido.Id_Transportadora > 0)
                {
                    _mTransportadora.Id = _mPedido.Id_Transportadora;
                    _mTransportadora.Transporte_rntc = rntc.Text;
                    _mTransportadora.Transporte_uf = uf.Text;
                    _mTransportadora.Transporte_placa = placa.Text;

                    if (!Nota.disableCampos)
                        _mTransportadora.Save(_mTransportadora, false);
                }

                OpenForm.Show<TelaPagamento>(this);
            };

            Back.Click += (s, e) => Close();
        }
    }
}
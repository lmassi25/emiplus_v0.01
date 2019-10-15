using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using SqlKata.Execution;
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

        public TelaFrete()
        {
            InitializeComponent();
            Id = Nota.Id;

            _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();

            Eventos();
        }

        private bool Validate()
        {
            if (IdTransportadora <= 0)
            {
                Alert.Message("Opss", "Selecione uma Transportadora.", Alert.AlertType.info);
                return true;
            }

            return false;
        }

        private void LoadData()
        {
            var tipos = new ArrayList();
            tipos.Add(new { Id = "0", Nome = "Por conta do emitente" });
            tipos.Add(new { Id = "1", Nome = "Por conta do destinatário/remetente" });
            tipos.Add(new { Id = "2", Nome = "Por conta de terceiros" });
            tipos.Add(new { Id = "9", Nome = "Sem frete" });
            tipo.DataSource = tipos;
            tipo.DisplayMember = "Nome";
            tipo.ValueMember = "Id";
        }

        private void Eventos()
        {
            Load += (s, e) => {
                LoadData();
            };

            SelecionarTransportadora.Click += (s, e) =>
            {
                PedidoModalTransportadora form = new PedidoModalTransportadora();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IdTransportadora = PedidoModalTransportadora.Id;

                    _mPedido.Id = Id;
                    _mPedido.Id_Transportadora = PedidoModalTransportadora.Id;
                    _mPedido.Save(_mPedido);
                    LoadData();

                    _mTransportadora = _mTransportadora.FindById(_mPedido.Id_Transportadora).FirstOrDefault<Model.Pessoa>();
                    if (_mTransportadora != null)
                    {
                        transportadoraSelecionada.Text = _mTransportadora.Nome;

                        placa.Text = _mTransportadora.Transporte_placa;
                        uf.Text = _mTransportadora.Transporte_uf;
                        rntc.Text = _mTransportadora.Transporte_rntc;
                    }
                }
            };

            Next.Click += (s, e) =>
            {
                if (Validate())
                {
                    return;
                }

                _mPedido.Id = Id;
                _mPedido.TipoFrete = Validation.ConvertToInt32(tipo.SelectedValue);
                _mPedido.Volumes_Frete = volumes.Text;
                _mPedido.PesoLiq_Frete = pesoLiquido.Text;
                _mPedido.PesoBruto_Frete = pesoBruto.Text;
                _mPedido.Especie_Frete = especie.Text;
                _mPedido.Marca_Frete = marca.Text;
                _mPedido.Save(_mPedido);

                _mTransportadora.Id = _mPedido.Id_Transportadora;
                _mTransportadora.Transporte_rntc = rntc.Text;
                _mTransportadora.Transporte_uf = uf.Text;
                _mTransportadora.Transporte_placa = placa.Text;
                _mTransportadora.Save(_mTransportadora, false);

                OpenForm.Show<TelaPagamento>(this);
            };

            Back.Click += (s, e) => Close();

        }
    }
}

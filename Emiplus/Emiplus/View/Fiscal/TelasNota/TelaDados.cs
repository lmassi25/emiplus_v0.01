using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using SqlKata.Execution;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaDados : Form
    {
        private static int Id { get; set; } // id nota
        public static int IdDetailsNota { get; set; }

        private int IdNatureza { get; set; }
        private int IdCliente { get; set; }
        private int IdAddr { get; set; }

        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.PessoaEndereco _mClienteAddr = new Model.PessoaEndereco();
        private Model.Nota _mNota = new Model.Nota();

        public TelaDados()
        {
            InitializeComponent();
            Id = Nota.Id;
            IdDetailsNota = Nota.IdDetailsNota;

            _mPedido = _mPedido.FindById(Id).FirstOrDefault<Model.Pedido>();
            _mNota = _mPedido.FindById(IdDetailsNota).FirstOrDefault<Model.Nota>();

            Eventos();
        }

        private void LoadNatureza()
        {
            var naturezaOps = new Model.Natureza().FindAll().WhereFalse("excluir").OrderByDesc("nome").Get();
            if (naturezaOps.Count() > 0)
            {
                naturezaOp.DataSource = naturezaOps;
                naturezaOp.DisplayMember = "NOME";
                naturezaOp.ValueMember = "ID";
            }

            if (IdNatureza > 0)
                naturezaOp.SelectedValue = IdNatureza;
        }

        /// <summary>
        /// Carrega o cliente selecionado.
        /// </summary>
        private void LoadCliente()
        {
            if (_mPedido.Cliente > 0)
            {
                var data = _mCliente.FindById(_mPedido.Cliente).First<Model.Pessoa>();
                NomeCliente.Text = data.Nome;
                IdCliente = data.Id;
            }
        }

        private void LoadData()
        {
            var tipos = new ArrayList();
            tipos.Add(new { Id = "0", Nome = "Entrada" });
            tipos.Add(new { Id = "1", Nome = "Saída" });
            tipo.DataSource = tipos;
            tipo.DisplayMember = "Nome";
            tipo.ValueMember = "Id";

            var finalidades = new ArrayList();
            finalidades.Add(new { Id = "1", Nome = "NF-e normal" });
            finalidades.Add(new { Id = "2", Nome = "NF-e complementar" });
            finalidades.Add(new { Id = "3", Nome = "NF-e de ajuste" });
            finalidades.Add(new { Id = "4", Nome = "Devolução de mercadoria" });
            finalidade.DataSource = finalidades;
            finalidade.DisplayMember = "Nome";
            finalidade.ValueMember = "Id";

            var localDestinos = new ArrayList();
            localDestinos.Add(new { Id = "1", Nome = "Operação Interna" });
            localDestinos.Add(new { Id = "2", Nome = "Operação Interestadual" });
            localDestinos.Add(new { Id = "3", Nome = "Operação com exterior" });
            localDestino.DataSource = localDestinos;
            localDestino.DisplayMember = "Nome";
            localDestino.ValueMember = "Id";

            LoadNatureza();
            LoadCliente();
        }

        private bool Validate()
        {
            if (IdCliente <= 0)
            {
                Alert.Message("Opss", "Selecione um Destinatário.", Alert.AlertType.info);
                return true;
            }

            if (IdAddr <= 0)
            {
                Alert.Message("Opss", "Selecione um endereço.", Alert.AlertType.info);
                return true;
            }

            if (string.IsNullOrEmpty(emissao.Text))
            {
                Validation.BorderInput(emissao, Validation.BorderColor.Vermelho);
                Alert.Message("Emissão é obrigatório!", "Preencha os campos obrigatórios.", Alert.AlertType.info);
                return true;
            }

            if (string.IsNullOrEmpty(saida.Text))
            {
                Validation.BorderInput(saida, Validation.BorderColor.Vermelho);
                Alert.Message("Saída é obrigatório!", "Preencha os campos obrigatórios.", Alert.AlertType.info);
                return true;
            }

            if (string.IsNullOrEmpty(hora.Text))
            {
                Validation.BorderInput(hora, Validation.BorderColor.Vermelho);
                Alert.Message("Hora é obrigatório!", "Preencha os campos obrigatórios.", Alert.AlertType.info);
                return true;
            }

            return false;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                if (Id > 0)
                {
                    LoadData();
                }
            };

            addNatureza.Click += (s, e) =>
            {
                AddNatureza f = new AddNatureza();
                f.btnSalvarText = "Salvar e Inserir";
                f.btnSalvarWidth = 150;
                f.btnSalvarLocation = 590;
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.StartPosition = FormStartPosition.CenterParent;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    IdNatureza = AddNatureza.idSelected;
                    LoadData();
                }
            };

            Next.Click += (s, e) =>
            {
                if (Validate())
                {
                    return;
                }

                _mPedido.Id = Id;
                _mPedido.Emissao = emissao.Text != "" ? DateTime.Parse(emissao.Text) : DateTime.Now;
                _mPedido.HoraSaida = saida.Text + "@" + hora.Text;
                _mPedido.Finalidade = Validation.ConvertToInt32(finalidade.SelectedValue);
                _mPedido.Destino = Validation.ConvertToInt32(localDestino.SelectedValue);
                _mPedido.TipoNFe = Validation.ConvertToInt32(tipo.SelectedValue);
                _mPedido.id_natureza = Validation.ConvertToInt32(naturezaOp.SelectedValue);
                _mPedido.info_contribuinte = infoContribuinte.Text;
                _mPedido.info_fisco = infoFisco.Text;
                _mPedido.Save(_mPedido);

                OpenForm.Show<TelaProdutos>(this);
            };

            SelecionarCliente.Click += (s, e) =>
            {
                PedidoModalClientes form = new PedidoModalClientes();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _mPedido.Id = Id;
                    _mPedido.Cliente = PedidoModalClientes.Id;
                    _mPedido.Save(_mPedido);
                    LoadData();

                    var dataAddrFirst = _mClienteAddr.Query().Where("id_pessoa", IdCliente).FirstOrDefault<Model.PessoaEndereco>();
                    if (dataAddrFirst != null)
                    {
                        AddrInfo.Visible = true;
                        AddrInfo.Text = $"Rua: {dataAddrFirst.Rua}, {dataAddrFirst.Cep} - {dataAddrFirst.Bairro} - {dataAddrFirst.Cidade}/{dataAddrFirst.Estado} - {dataAddrFirst.Pais}";
                        IdAddr = dataAddrFirst.Id;

                        _mPedido.Id = Id;
                        _mPedido.id_useraddress = IdAddr;
                        _mPedido.Save(_mPedido);

                        addAddr.Visible = true;
                        addAddr.Text = "Alterar Endereço.";
                    }
                    else
                    {
                        AddrInfo.Visible = true;
                        addAddr.Visible = true;
                        AddrInfo.Text = "Esse destinatário não possui um endereço cadastrado!";
                        addAddr.Text = "Adicionar Endereço!";
                    }
                }
            };

            addAddr.Click += (s, e) =>
            {
                if (_mPedido.Cliente > 0)
                {
                    DetailsClient.IdClient = _mPedido.Cliente;
                    DetailsClient form = new DetailsClient();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _mPedido.Id = Id;
                        _mPedido.id_useraddress = DetailsClient.IdAddress;
                        _mPedido.Save(_mPedido);
                        LoadData();

                        var dataAddr = _mClienteAddr.FindById(_mPedido.id_useraddress).First<Model.PessoaEndereco>();
                        AddrInfo.Text = $"Rua: {dataAddr.Rua}, {dataAddr.Cep} - {dataAddr.Bairro} - {dataAddr.Cidade}/{dataAddr.Estado} - {dataAddr.Pais}";
                        IdAddr = dataAddr.Id;

                        addAddr.Text = "Alterar Endereço.";
                    }
                }
                else
                {
                    Alert.Message("Opps", "Selecione um destinatário...", Alert.AlertType.info);
                }
            };

            emissao.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            saida.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            hora.KeyPress += (s, e) => Masks.MaskHour(s, e);
        }
    }
}

using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using SqlKata.Execution;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaDados : Form
    {
        public static int Id { get; set; } // id nota
        private int IdNatureza { get; set; }
        private int IdCliente { get; set; }
        private int IdAddr { get; set; }

        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.PessoaEndereco _mClienteAddr = new Model.PessoaEndereco();

        public TelaDados()
        {
            InitializeComponent();
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
                else
                {
                    _mPedido.Id = Id;
                    _mPedido.Tipo = "Nota";
                    if (_mPedido.Save(_mPedido))
                    {
                        Id = _mPedido.GetLastId();
                        LoadData();
                    }
                    else
                    {
                        Alert.Message("Opss", "Erro ao criar Nota.", Alert.AlertType.error);
                        Close();
                    }
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

        }
    }
}

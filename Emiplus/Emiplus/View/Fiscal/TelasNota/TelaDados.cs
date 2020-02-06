﻿using Emiplus.Data.Helpers;
using Emiplus.View.Comercial;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal.TelasNota
{
    public partial class TelaDados : Form
    {
        private static int Id { get; set; } // id nota
        private int IdNatureza { get; set; }
        private int IdCliente { get; set; }
        private int IdAddr { get; set; }

        private Model.Pedido _mPedido = new Model.Pedido();
        private Model.Pessoa _mCliente = new Model.Pessoa();
        private Model.PessoaEndereco _mClienteAddr = new Model.PessoaEndereco();
        private Model.Nota _mNota = new Model.Nota();

        public static bool telaDados { get; set; } = false;

        public TelaDados()
        {
            InitializeComponent();

            Id = Nota.Id;

            _mNota = new Model.Nota().FindById(Id).FirstOrDefault<Model.Nota>();

            if (_mNota == null)
            {
                Alert.Message("Ação não permitida", "Referência de Pedido não identificada", Alert.AlertType.warning);
                return;                
            }
                
            _mPedido = _mPedido.FindById(_mNota.id_pedido).FirstOrDefault<Model.Pedido>();
            
            IdNatureza = _mPedido.id_natureza;

            DisableCampos();
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

        private void DisableCampos()
        {
            if (Nota.disableCampos)
            {
                emissao.Enabled = false;
                saida.Enabled = false;
                hora.Enabled = false;
                tipo.Enabled = false;
                finalidade.Enabled = false;
                localDestino.Enabled = false;
                naturezaOp.Enabled = false;
                infoContribuinte.Enabled = false;
                infoFisco.Enabled = false;
                SelecionarCliente.Enabled = false;
            }
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
                LoadAddress();
            }
        }

        private void LoadAddress()
        {
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

        private void GetData()
        {
            _mPedido.Id = Id;
            _mPedido.Emissao = emissao.Text != "" ? DateTime.Parse(emissao.Text) : DateTime.Now;
            _mPedido.Saida = saida.Text != "" ? DateTime.Parse(saida.Text) : DateTime.Now;
            _mPedido.HoraSaida = hora.Text;
            _mPedido.Finalidade = Validation.ConvertToInt32(finalidade.SelectedValue);
            _mPedido.Destino = Validation.ConvertToInt32(localDestino.SelectedValue);
            _mPedido.TipoNFe = Validation.ConvertToInt32(tipo.SelectedValue);
            _mPedido.id_natureza = Validation.ConvertToInt32(naturezaOp.SelectedValue);
            _mPedido.info_contribuinte = infoContribuinte.Text;
            _mPedido.info_fisco = infoFisco.Text;
            _mPedido.id_natureza = Validation.ConvertToInt32(naturezaOp.SelectedValue) > 0 ? Validation.ConvertToInt32(naturezaOp.SelectedValue) : 0;

            if (PedidoModalClientes.Id > 0)
                _mPedido.Cliente = PedidoModalClientes.Id;

            if (DetailsClient.IdAddress > 0)
                _mPedido.id_useraddress = DetailsClient.IdAddress;
        }

        private void LoadData()
        {
            var tipos = new ArrayList();
            tipos.Add(new { Id = "1", Nome = "Saída" });
            tipos.Add(new { Id = "0", Nome = "Entrada" });
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

            emissao.Text = Validation.ConvertDateToForm(_mPedido.Emissao);
            saida.Text = Validation.ConvertDateToForm(_mPedido.Saida);
            hora.Text = String.IsNullOrEmpty(_mPedido.HoraSaida) ? "" : _mPedido.HoraSaida;
            finalidade.SelectedItem = _mPedido.Finalidade;
            localDestino.SelectedItem = _mPedido.Destino;
            tipo.SelectedItem = _mPedido.TipoNFe;
            naturezaOp.SelectedItem = _mPedido.id_natureza;
            infoContribuinte.Text = _mPedido.info_contribuinte;
            infoFisco.Text = _mPedido.info_fisco;
            IdNatureza = _mPedido.id_natureza;

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

            return false;
        }

        private void Eventos()
        {
            Masks.SetToUpper(this);

            Load += (s, e) =>
            {
                if (Id > 0)
                {
                    LoadData();
                }

                if (_mNota != null && !String.IsNullOrEmpty(_mNota.Status))
                {
                    progress5.Visible = false;
                    step5.Visible = false;
                    label16.Visible = false;
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
                    return;

                telaDados = true;

                GetData();

                if (!Nota.disableCampos)
                    _mPedido.Save(_mPedido);

                OpenForm.Show<TelaProdutos>(this);
            };

            SelecionarCliente.Click += (s, e) =>
            {
                PedidoModalClientes form = new PedidoModalClientes();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    GetData();
                    _mPedido.Save(_mPedido);
                    LoadData();
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
                        GetData();
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

            Documentos.Click += (s, e) =>
            {
                DocumentosReferenciados form = new DocumentosReferenciados();
                form.Show();
            };

            emissao.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            saida.KeyPress += (s, e) => Masks.MaskBirthday(s, e);
            hora.KeyPress += (s, e) => Masks.MaskHour(s, e);

            Apagar.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Você está prestes a excluir! Deseja continuar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    if (_mPedido != null)
                    {
                        _mPedido.Excluir = 1;
                        _mPedido.Save(_mPedido);
                    }

                    if (_mNota != null)
                    {
                        _mNota.Excluir = 1;
                        _mNota.Save(_mNota, false);
                    }

                    telaDados = true;
                    Alert.Message("Pronto!", "Removido com sucesso!", Alert.AlertType.info);
                    Close();
                }
            };
        }
    }
}
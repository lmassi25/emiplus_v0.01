using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Financeiro;
using Emiplus.View.Fiscal.TelasNota;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class DetailsPedido : Form
    {
        private Model.Pedido _modelPedido = new Model.Pedido();

        private Model.PessoaContato _modelPessoaContato = new Model.PessoaContato();
        private Model.PessoaEndereco _modelPessoaAddr = new Model.PessoaEndereco();
        private Model.Pessoa _modelPessoa = new Model.Pessoa();
        private Model.Usuarios _modelUsuario = new Model.Usuarios();

        private Model.PedidoItem _modelPedidoItem = new Model.PedidoItem();
        private Controller.PedidoItem _controllerPedidoItem = new Controller.PedidoItem();
        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

        public static int idPedido { get; set; }

        private int pessoaID;
        public DetailsPedido()
        {
            InitializeComponent();
            Eventos();

            if (idPedido > 0)
                LoadData();
        }

        private void LoadData()
        {
            _modelPedido = _modelPedido.FindById(idPedido).First<Model.Pedido>();

            nrPedido.Text = idPedido.ToString("D5");
            aberto.Text = Validation.ConvertDateToForm(_modelPedido.Criado, true);
            txtEntrega.Text = Validation.FormatPrice(_modelPedido.Frete, true);
            txtDesconto.Text = Validation.FormatPrice(_modelPedido.Desconto, true);

            txtTroco.Text = Validation.FormatPrice(_controllerTitulo.GetTroco(idPedido), true);
            txtSubtotal.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(idPedido), true);
            txtPagar.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(idPedido), true);
            txtRecebimento.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(idPedido), true);

            if (_modelPedido.Cliente > 0)
            {
                var pessoa = _modelPessoa.FindById(_modelPedido.Cliente).Select("id", "nome").First();
                pessoaID = pessoa.ID;
                cliente.Text = pessoa.NOME;
            }

            if (_modelPedido.Colaborador > 0)
            {
                var data = _modelUsuario.FindByUserId(_modelPedido.Colaborador).First<Model.Usuarios>();
                vendedor.Text = data.Nome;
            }

            if (_modelPedido.status == 0)
            {
                panel7.BackColor = Color.FromArgb(215, 90, 74);
                label7.Text = "Fechado";
            }

            _controllerPedidoItem.GetDataTableItens(GridLista, idPedido);
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            btnExit.Click += (s, e) => Close();

            btnPgtosLancado.Click += (s, e) =>
            {
                DetailsPedidoPgtos.IdPedido = idPedido;
                DetailsPedidoPgtos pgtos = new DetailsPedidoPgtos();
                pgtos.ShowDialog();
            };

            btnReceber.Click += (s, e) =>
            {
                if (Home.idCaixa == 0)
                {
                    var result = AlertOptions.Message("Atenção!", "É necessário ter o caixa aberto para lançar recebimentos. Deseja ABRIR o caixa?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                    if (result)
                    {
                        AbrirCaixa f = new AbrirCaixa();
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            OpenPedidoPagamentos();
                        }
                    }
                }
                else
                {
                    OpenPedidoPagamentos();
                }
            };

            btnImprimir.Click += (s, e) =>
            {

                IEnumerable<dynamic> dados = new Controller.PedidoItem().GetDataItens(idPedido);

                ArrayList data = new ArrayList();
                var nr = 0;
                foreach (var item in dados)
                {
                    nr++;
                    data.Add(new
                    {
                        Nr = nr,
                        Nome = item.NOME,
                        CodeBarras = item.CODEBARRAS,
                        Ref = item.REFERENCIA,
                        Qtd = item.QUANTIDADE,
                        ValorVenda = item.VALORVENDA,
                        Price = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL))
                    });
                }

                var dataPgtos = _controllerTitulo.GetDataPgtosLancados(idPedido);
                ArrayList newDataPgtos = new ArrayList();
                foreach (var item in dataPgtos)
                {
                    newDataPgtos.Add(new
                    {
                        Forma = item.FORMAPGTO,
                        Valor = Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true)
                    });
                }

                Model.Pessoa dataCliente = _modelPessoa.FindById(_modelPedido.Cliente).FirstOrDefault<Model.Pessoa>();
                Model.Usuarios dataVendedor = _modelUsuario.FindByUserId(_modelPedido.Colaborador).FirstOrDefault<Model.Usuarios>();
                _modelPessoaAddr = _modelPessoaAddr.FindByIdUser(dataCliente.Id).FirstOrDefault<Model.PessoaEndereco>();
                _modelPessoaContato = _modelPessoaContato.FindByIdUser(dataCliente.Id).FirstOrDefault<Model.PessoaContato>();

                var Addr = _modelPessoaAddr.Rua + " " + _modelPessoaAddr.Nr + " - CEP: " + _modelPessoaAddr.Cep + " - " + _modelPessoaAddr.Complemento + " | " + _modelPessoaAddr.Bairro + " - " + _modelPessoaAddr.Cidade + "/" + _modelPessoaAddr.Estado;

                var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\View\Reports\html\CupomComprovanteVendaA4.html"));
                var render = html.Render(Hash.FromAnonymousObject(new
                {
                    NomeFantasia = Settings.Default.empresa_nome_fantasia,
                    CNPJ = Settings.Default.empresa_cnpj,
                    AddressEmpresa = $"{Settings.Default.empresa_rua} {Settings.Default.empresa_nr} - {Settings.Default.empresa_cep} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
                    Logo = Settings.Default.empresa_logo,
                    Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                    Cliente = dataCliente.Nome,
                    Vendedor = dataVendedor.Nome,
                    Caixa = caixa.Text,
                    Endereco = Addr,
                    Telefone = _modelPessoaContato.Telefone,
                    Celular = _modelPessoaContato.Celular,
                    Data = data,
                    Troco = txtTroco.Text.Replace("-", ""),
                    Pagamentos = newDataPgtos,
                    subTotal = txtSubtotal.Text,
                    Descontos = txtDesconto.Text,
                    Acrescimo = txtAcrescimo.Text,
                    Total = txtPagar.Text,
                    NrVenda = idPedido
                }));

                Browser.htmlRender = render;
                var f = new Browser();
                f.ShowDialog();
            };

            btnNfe.Click += (s, e) =>
            {
                var checkNota = new Model.Nota().FindByIdPedido(idPedido).Get();

                if (checkNota.Count() == 0)
                {
                    OpcoesNfe.idPedido = idPedido;
                    OpcoesNfe f = new OpcoesNfe();
                    f.Show();
                }

                foreach (var item in checkNota)
                {
                    if (item.STATUS == null)
                    {
                        OpcoesNfe.idPedido = idPedido;
                        OpcoesNfe f = new OpcoesNfe();
                        f.Show();
                    }
                    else
                    {
                        OpcoesNfeRapida.idPedido = idPedido;
                        OpcoesNfeRapida f = new OpcoesNfeRapida();
                        f.Show();
                    }
                }
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("http://ajuda.emiplus.com.br/");
        }

        private void OpenPedidoPagamentos()
        {
            AddPedidos.Id = idPedido;
            PedidoPagamentos pagamentos = new PedidoPagamentos();
            pagamentos.ShowDialog();
            LoadData();
        }
    }
}

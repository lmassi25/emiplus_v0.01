using DotLiquid;
using Emiplus.Controller;
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

            switch (Home.pedidoPage)
            {
                case "Devoluções":
                    label1.Text = "Detalhes da Devolução:";
                    label6.Text = "Detalhes da Devolução";
                    label3.Text = "Devoluções";
                    button21.Visible = false;
                    button22.Visible = false;
                    btnNfe.Visible = false;
                    btnCFeSat.Visible = false;
                    btnPgtosLancado.Visible = false;
                    label11.Visible = false;
                    label43.Visible = false;
                    label41.Visible = false;
                    txtTroco.Visible = false;
                    txtRecebimento.Visible = false;
                    txtAcrescimo.Visible = false;
                    panel8.Visible = false;
                    nrPedido.Left = 542;
                    break;
                case "Compras":
                    label1.Text = "Detalhes da Compra:";
                    label6.Text = "Detalhes da Compra";
                    label3.Text = "Compras";
                    button22.Visible = false;
                    btnCFeSat.Visible = false;
                    label43.Text = "Total Pago:";
                    btnPgtosLancado.Text = "Ver Pagamentos Lançados!";
                    nrPedido.Left = 510;
                    btnNfe.Visible = false;
                    button21.Visible = false;
                    break;
            }
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
                if (Home.idCaixa == 0 && Home.pedidoPage == "Vendas")
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

            btnRemove.Click += (s, e) =>
            {
                var result = AlertOptions.Message("Atenção!", "Deseja realmente apagar?", AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    var remove = new Controller.Pedido();
                    remove.Remove(idPedido);
                    Close();
                }
            };

            btnImprimir.Click += (s, e) =>
            {
                PedidoImpressao print = new PedidoImpressao();
                print.Print(idPedido);
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
            PedidoPagamentos.hideFinalizar = true;
            PedidoPagamentos pagamentos = new PedidoPagamentos();
            pagamentos.ShowDialog();
            LoadData();
        }
    }
}

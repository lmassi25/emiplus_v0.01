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
                    label2.Text = "Confira nessa tela todas as informações da sua devolução.";
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
                    label2.Text = "Confira nessa tela todas as informações da sua compra.";
                    label6.Text = "Detalhes da Compra";
                    label3.Text = "Compras";
                    label8.Text = "Fornecedor";
                    button22.Visible = false;
                    btnCFeSat.Visible = false;
                    label43.Text = "Total Pago:";
                    btnPgtosLancado.Text = "Ver Pagamentos Lançados!";
                    nrPedido.Left = 510;
                    btnNfe.Visible = false;
                    button21.Visible = false;
                    SelecionarCliente.Text = "Alterar fornecedor";
                    break;
            }
        }

        public void Nfe()
        {
            var checkNota = new Model.Nota().FindByIdPedido(idPedido).Where("nota.tipo", "NFe").FirstOrDefault();

            if (checkNota == null)
            {
                OpcoesNfe.idPedido = idPedido;
                OpcoesNfe f1 = new OpcoesNfe();
                f1.Show();

                return;
            }

            if(checkNota.STATUS != null)
            {
                OpcoesNfeRapida.idPedido = idPedido;
                OpcoesNfeRapida f2 = new OpcoesNfeRapida();
                f2.Show();

                return;
            }

            OpcoesNfe.idPedido = idPedido;
            OpcoesNfe f3 = new OpcoesNfe();
            f3.Show();     
        }

        public void Cfe()
        {
            var checkNota = new Model.Nota().FindByIdPedidoAndTipo(idPedido, "CFe").FirstOrDefault<Model.Nota>();
            if(checkNota == null)
            {
                Model.Nota _modelNota = new Model.Nota();

                _modelNota.Id = 0;
                _modelNota.Tipo = "CFe";
                _modelNota.id_pedido = idPedido;
                _modelNota.Save(_modelNota, false);

                OpcoesCfeCpf.idPedido = idPedido;
                OpcoesCfeCpf.emitir = true;
                OpcoesCfeCpf f = new OpcoesCfeCpf();
                f.Show();

                return;
            }

            if (checkNota.Status != "Autorizada" && checkNota.Status != "Cancelada")
            {
                OpcoesCfeCpf.idPedido = idPedido;
                OpcoesCfeCpf f = new OpcoesCfeCpf();
                f.Show();
            }
            else
            {
                OpcoesCfe.idPedido = idPedido;
                OpcoesCfe f = new OpcoesCfe();
                f.Show();
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
                var pessoa = _modelPessoa.FindById(_modelPedido.Cliente).Select("id", "nome").FirstOrDefault();

                if (pessoa == null)
                    return;

                pessoaID = pessoa.ID;                
            }

            if (_modelPedido.Colaborador > 0)
            {
                var data = _modelUsuario.FindByUserId(_modelPedido.Colaborador).FirstOrDefault<Model.Usuarios>();

                if (data == null)
                    return;

                vendedor.Text = data.Nome;
            }

            if (_modelPedido.status != 0)
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
                case Keys.F9:
                    if (UserPermission.SetControl(btnNfe, pictureBox4, "fiscal_emissaonfe"))
                        return;

                    Cfe();
                    break;
                case Keys.F10:
                    if (UserPermission.SetControl(btnCFeSat, pictureBox6, "fiscal_emissaocfe"))
                        return;

                    Nfe();
                    break;
                case Keys.F11:
                    new PedidoImpressao().Print(idPedido);
                    e.SuppressKeyPress = true;
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
                new Controller.Pedido().Imprimir(idPedido);
            };

            btnNfe.Click += (s, e) =>
            {
                if (UserPermission.SetControl(btnNfe, pictureBox4, "fiscal_emissaonfe"))
                    return;

                Nfe();
            };

            btnCFeSat.Click += (s, e) =>
            {
                if (UserPermission.SetControl(btnCFeSat, pictureBox6, "fiscal_emissaocfe"))
                    return;

                Cfe();
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("http://ajuda.emiplus.com.br/");

            SelecionarCliente.Click += (s, e) =>
            {
                var checkNota = new Model.Nota().FindByIdPedido(idPedido).WhereNotNull("status").FirstOrDefault();
                if (checkNota != null)
                {
                    Alert.Message("Ação não permitida!", "Existe um documento fiscal vinculado.", Alert.AlertType.warning);
                    return;
                }
 
                ModalClientes();
            };

            SelecionarColaborador.Click += (s, e) =>
            {
                var checkNota = new Model.Nota().FindByIdPedido(idPedido).WhereNotNull("status").FirstOrDefault();
                if (checkNota != null)
                {
                    Alert.Message("Ação não permitida!", "Existe um documento fiscal vinculado.", Alert.AlertType.warning);
                    return;
                }

                ModalColaborador();
            };
        }

        private void OpenPedidoPagamentos()
        {
            AddPedidos.Id = idPedido;
            PedidoPagamentos.hideFinalizar = true;
            PedidoPagamentos pagamentos = new PedidoPagamentos();
            pagamentos.ShowDialog();
            LoadData();
        }

        private void ModalClientes()
        {
            PedidoModalClientes form = new PedidoModalClientes();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _modelPedido.Id = idPedido;
                _modelPedido.Cliente = PedidoModalClientes.Id;
                _modelPedido.Save(_modelPedido);
                LoadData();
            }
        }

        public void ModalColaborador()
        {
            PedidoModalVendedor form = new PedidoModalVendedor();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _modelPedido.Id = idPedido;
                _modelPedido.Colaborador = PedidoModalVendedor.Id;
                _modelPedido.Save(_modelPedido);
                LoadData();
            }
        }
    }
}
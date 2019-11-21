using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using Emiplus.View.Financeiro;
using SqlKata.Execution;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Emiplus.View.Comercial
{
    public partial class DetailsPedido : Form
    {
        private Model.Pedido _modelPedido = new Model.Pedido();

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

        private void Eventos()
        {
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
                    if (AlertOptions.Message("Atenção!", "É necessário ter o caixa aberto para lançar recebimentos. Deseja ABRIR o caixa?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo))
                    {
                        AbrirCaixa f = new AbrirCaixa();
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            OpenPedidoPagamentos();
                        }
                    }
                }

                OpenPedidoPagamentos();
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

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
    public partial class DetailsPedido : Form
    {
        private int idPedido = Pedido.IdPedido;
        private Model.Pedido _modelPedido = new Model.Pedido();
        private Model.Pessoa _modelPessoa = new Model.Pessoa();
        private Model.PedidoItem _modelPedidoItem = new Model.PedidoItem();
        private Controller.PedidoItem _controllerPedidoItem = new Controller.PedidoItem();
        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

        private int pessoaID;
        public DetailsPedido()
        {
            InitializeComponent();
            Events();

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
            txtSubtotal.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(idPedido), true);
            txtPagar.Text = Validation.FormatPrice(_controllerTitulo.GetLancados(idPedido), true);
            txtRecebimento.Text = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(idPedido), true);

            if (_modelPedido.Cliente > 0)
            {
                var pessoa = _modelPessoa.FindById(_modelPedido.Cliente).Select("id", "nome").First();
                pessoaID = pessoa.ID;
                cliente.Text = pessoa.NOME;
            }

            if (_modelPedido.Colaborador > 0)
            {
                var vendedor = _modelPessoa.FindById(_modelPedido.Colaborador).Select("id", "nome").First();
                vendedor.Text = vendedor.NOME;
            }

            if (_modelPedido.status == 0)
            {
                panel7.BackColor = Color.FromArgb(215, 90, 74);
                label7.Text = "Fechado";
            }

            _controllerPedidoItem.GetDataTableItens(GridLista, idPedido);
        }

        private void Events()
        {
            btnExit.Click += (s, e) => Close();

            btnPgtosLancado.Click += (s, e) =>
            {
                DetailsPedidoPgtos pgtos = new DetailsPedidoPgtos();
                pgtos.ShowDialog();
            };

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("http://ajuda.emiplus.com.br/");
        }
    }
}

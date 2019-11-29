using DotLiquid;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Reports;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Controller
{
    class PedidoImpressao
    {
        private Model.Pedido _modelPedido = new Model.Pedido();
        private Model.PessoaContato _modelPessoaContato = new Model.PessoaContato();
        private Model.PessoaEndereco _modelPessoaAddr = new Model.PessoaEndereco();
        private Model.Pessoa _modelPessoa = new Model.Pessoa();
        private Model.Usuarios _modelUsuario = new Model.Usuarios();

        private Model.PedidoItem _modelPedidoItem = new Model.PedidoItem();
        private Controller.PedidoItem _controllerPedidoItem = new Controller.PedidoItem();
        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

        public void Print(int idPedido)
        {
            _modelPedido = _modelPedido.FindById(idPedido).First<Model.Pedido>();

            IEnumerable<dynamic> dados = new Controller.PedidoItem().GetDataItens(idPedido);

            ArrayList data = new ArrayList();
            var nr = 0;
            var countItens = 0;
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
                countItens += item.QUANTIDADE;
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

            string titulo = "";
            string titulo2 = "";
            bool orcamento = false;
            switch (Home.pedidoPage)
            {
                case "Vendas":
                    titulo = "Venda";
                    titulo2 = "Vendido";
                    orcamento = true;
                    break;
                case "Orçamentos":
                    titulo = "Orçamento";
                    titulo2 = "Orçado";
                    orcamento = false;
                    break;
                case "Consignações":
                    titulo = "Consignação";
                    titulo2 = "Consignado";
                    orcamento = false;
                    break;
                case "Devoluções":
                    titulo = "Devolução";
                    titulo2 = "Devolvido";
                    orcamento = false;
                    break;
                case "Compras":
                    titulo = "Compra";
                    titulo2 = "Comprado";
                    orcamento = false;
                    break;
            }

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
                Caixa = _modelPedido.Id_Caixa,
                Endereco = Addr,
                Telefone = _modelPessoaContato.Telefone,
                Celular = _modelPessoaContato.Celular,
                Data = data,
                Troco = Validation.FormatPrice(_controllerTitulo.GetTroco(idPedido), true).Replace("-", ""),
                Pagamentos = newDataPgtos,
                subTotal = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(idPedido), true),
                Descontos = Validation.FormatPrice(_modelPedido.Desconto, true),
                Acrescimo = Validation.FormatPrice(0, true),
                Total = Validation.FormatPrice(_controllerTitulo.GetTotalPedido(idPedido), true),
                NrVenda = idPedido,
                titulo = titulo,
                titulo2 = titulo2,
                orcamento = orcamento,
                count = countItens
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.ShowDialog();
        }
    }
}

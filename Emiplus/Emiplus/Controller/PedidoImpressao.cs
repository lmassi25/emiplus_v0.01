using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using DotLiquid;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Reports;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    internal class PedidoImpressao
    {
        private readonly Titulo _controllerTitulo = new Titulo();
        private Model.Pedido _modelPedido = new Model.Pedido();

        private readonly Model.Pessoa _modelPessoa = new Model.Pessoa();
        private PessoaEndereco _modelPessoaAddr = new PessoaEndereco();
        private PessoaContato _modelPessoaContato = new PessoaContato();
        private readonly Usuarios _modelUsuario = new Usuarios();

        public bool Print(int idPedido)
        {
            _modelPedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

            var dados = new PedidoItem().GetDataItens(idPedido);

            var data = new ArrayList();
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
            var newDataPgtos = new ArrayList();
            foreach (var item in dataPgtos)
                newDataPgtos.Add(new
                {
                    Forma = item.FORMAPGTO,
                    Valor = Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true)
                });

            var dataCliente = _modelPessoa.FindById(_modelPedido.Cliente).FirstOrDefault<Model.Pessoa>();
            var dataVendedor = _modelUsuario.FindByUserId(_modelPedido.Colaborador).FirstOrDefault<Usuarios>();
            if (dataCliente != null && !string.IsNullOrEmpty(dataCliente.Id.ToString()))
            {
                _modelPessoaAddr = _modelPessoaAddr.FindByIdUser(dataCliente.Id).FirstOrDefault<PessoaEndereco>();
                _modelPessoaContato = _modelPessoaContato.FindByIdUser(dataCliente.Id).FirstOrDefault<PessoaContato>();
            }

            var Addr = "";
            if (_modelPessoaAddr != null)
                Addr = _modelPessoaAddr.Rua + " " + _modelPessoaAddr.Nr + " - CEP: " + _modelPessoaAddr.Cep + " - " +
                       _modelPessoaAddr.Complemento + " | " + _modelPessoaAddr.Bairro + " - " +
                       _modelPessoaAddr.Cidade + "/" + _modelPessoaAddr.Estado;

            var titulo = "";
            var titulo2 = "";
            var orcamento = false;
            switch (Home.pedidoPage)
            {
                case "Vendas":
                    titulo = "Venda";
                    titulo2 = "Cliente";
                    orcamento = true;
                    break;

                case "Orçamentos":
                    titulo = "Orçamento";
                    titulo2 = "Cliente";
                    orcamento = false;
                    break;

                case "Consignações":
                    titulo = "Consignação";
                    titulo2 = "Cliente";
                    orcamento = false;
                    break;

                case "Devoluções":
                    titulo = "Devolução";
                    titulo2 = "Cliente";
                    orcamento = false;
                    break;

                case "Compras":
                    titulo = "Compra";
                    titulo2 = "Fornecedor";
                    orcamento = false;
                    break;
            }

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\CupomComprovanteVendaA4.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                CNPJ = Settings.Default.empresa_cnpj,
                AddressEmpresa =
                    $"{Settings.Default.empresa_rua} {Settings.Default.empresa_nr} - {Settings.Default.empresa_cep} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                Cliente = dataCliente?.Nome ?? "",
                Vendedor = dataVendedor?.Nome ?? "",
                Caixa = _modelPedido.Id_Caixa,
                Obs = _modelPedido.Observacao,
                Endereco = Addr,
                Telefone = _modelPessoaContato != null ? _modelPessoaContato.Telefone : "",
                Celular = _modelPessoaContato != null ? _modelPessoaContato.Celular : "",
                Data = data,
                Troco = Validation.FormatPrice(_controllerTitulo.GetTroco(idPedido), true).Replace("-", ""),
                Pagamentos = newDataPgtos,
                subTotal = Validation.FormatPrice(_modelPedido.Produtos, true),
                Descontos = Validation.FormatPrice(_modelPedido.Desconto, true),
                Acrescimo = Validation.FormatPrice(0, true),
                Total = Validation.FormatPrice(_modelPedido.Total, true),
                NrVenda = idPedido,
                titulo,
                titulo2,
                orcamento,
                count = countItens
            }));

            Browser.htmlRender = render;
            var f = new Browser {TopMost = true};
            if (f.ShowDialog() == DialogResult.OK)
                return true;
            return false;
        }

        public bool PrintOS(int idPedido)
        {
            _modelPedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

            var dataCliente = _modelPessoa.FindById(_modelPedido.Cliente).FirstOrDefault<Model.Pessoa>();
            var dataVendedor = _modelUsuario.FindByUserId(_modelPedido.Colaborador).FirstOrDefault<Usuarios>();
            if (dataCliente != null && !string.IsNullOrEmpty(dataCliente.Id.ToString()))
            {
                _modelPessoaAddr = _modelPessoaAddr.FindByIdUser(dataCliente.Id).FirstOrDefault<PessoaEndereco>();
                _modelPessoaContato = _modelPessoaContato.FindByIdUser(dataCliente.Id).FirstOrDefault<PessoaContato>();
            }

            var Addr = "";
            if (_modelPessoaAddr != null)
                Addr = _modelPessoaAddr.Rua + " " + _modelPessoaAddr.Nr + " - CEP: " + _modelPessoaAddr.Cep + " - " +
                       _modelPessoaAddr.Complemento + " | " + _modelPessoaAddr.Bairro + " - " +
                       _modelPessoaAddr.Cidade + "/" + _modelPessoaAddr.Estado;

            string titulo = "Ordem de Serviço", titulo2 = "Cliente";
            string gridstyle = "",
                astyle = "",
                bstyle = "",
                cstyle = "",
                dstyle = "",
                estyle = "",
                fstyle = "";

            if (!string.IsNullOrEmpty(IniFile.Read("Campo_1_Visible", "OS")))
                if (!Convert.ToBoolean(IniFile.Read("Campo_1_Visible", "OS")))
                    astyle = " hidden='ON' ";

            if (!string.IsNullOrEmpty(IniFile.Read("Campo_2_Visible", "OS")))
                if (!Convert.ToBoolean(IniFile.Read("Campo_2_Visible", "OS")))
                    bstyle = " hidden='ON' ";

            if (!string.IsNullOrEmpty(IniFile.Read("Campo_3_Visible", "OS")))
                if (!Convert.ToBoolean(IniFile.Read("Campo_3_Visible", "OS")))
                    cstyle = " hidden='ON' ";

            if (!string.IsNullOrEmpty(IniFile.Read("Campo_4_Visible", "OS")))
                if (!Convert.ToBoolean(IniFile.Read("Campo_4_Visible", "OS")))
                    dstyle = " hidden='ON' ";

            if (!string.IsNullOrEmpty(IniFile.Read("Campo_5_Visible", "OS")))
                if (!Convert.ToBoolean(IniFile.Read("Campo_5_Visible", "OS")))
                    estyle = " hidden='ON' ";

            if (!string.IsNullOrEmpty(IniFile.Read("Campo_6_Visible", "OS")))
                if (!Convert.ToBoolean(IniFile.Read("Campo_6_Visible", "OS")))
                    fstyle = " hidden='ON' ";

            if (!string.IsNullOrEmpty(dstyle) && !string.IsNullOrEmpty(estyle) && !string.IsNullOrEmpty(fstyle))
                gridstyle = " hidden='ON' ";

            var alabel = !string.IsNullOrEmpty(IniFile.Read("Campo_1_Descr", "OS"))
                ? IniFile.Read("Campo_1_Descr", "OS")
                : "";
            var atext = _modelPedido.campoa;

            var blabel = !string.IsNullOrEmpty(IniFile.Read("Campo_2_Descr", "OS"))
                ? IniFile.Read("Campo_2_Descr", "OS")
                : "";
            var btext = _modelPedido.campob;

            var clabel = !string.IsNullOrEmpty(IniFile.Read("Campo_3_Descr", "OS"))
                ? IniFile.Read("Campo_3_Descr", "OS")
                : "";
            var ctext = _modelPedido.campoc;

            var dlabel = !string.IsNullOrEmpty(IniFile.Read("Campo_4_Descr", "OS"))
                ? IniFile.Read("Campo_4_Descr", "OS")
                : "";
            var dtext = _modelPedido.campod;

            var elabel = !string.IsNullOrEmpty(IniFile.Read("Campo_5_Descr", "OS"))
                ? IniFile.Read("Campo_5_Descr", "OS")
                : "";
            var etext = _modelPedido.campoe;

            var flabel = !string.IsNullOrEmpty(IniFile.Read("Campo_6_Descr", "OS"))
                ? IniFile.Read("Campo_6_Descr", "OS")
                : "";
            var ftext = _modelPedido.campof;

            var problemalabel = "Problema relatado";
            var problematext = _modelPedido.problema;
            var solucaolabel = "Serviço realizado";
            var solucaotext = _modelPedido.solucao;

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\OSComprovanteVendaA4.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                CNPJ = Settings.Default.empresa_cnpj,
                AddressEmpresa =
                    $"{Settings.Default.empresa_rua} {Settings.Default.empresa_nr} - {Settings.Default.empresa_cep} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                Cliente = dataCliente?.Nome ?? "",
                Vendedor = dataVendedor?.Nome ?? "",
                Obs = _modelPedido.Observacao,
                Endereco = Addr,
                Telefone = _modelPessoaContato != null ? _modelPessoaContato.Telefone : "",
                Celular = _modelPessoaContato != null ? _modelPessoaContato.Celular : "",
                NrVenda = idPedido,
                titulo,
                titulo2,
                astyle,
                alabel,
                atext,
                bstyle,
                blabel,
                btext,
                cstyle,
                clabel,
                ctext,
                dstyle,
                dlabel,
                dtext,
                estyle,
                elabel,
                etext,
                fstyle,
                flabel,
                ftext,
                problemalabel,
                problematext,
                solucaolabel,
                solucaotext
            }));

            Browser.htmlRender = render;
            var f = new Browser {TopMost = true};
            if (f.ShowDialog() == DialogResult.OK)
                return true;
            return false;
        }
    }
}
using DotLiquid;
using Emiplus.Data.Core;
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
using System.Windows.Forms;

namespace Emiplus.Controller
{
    internal class PedidoImpressao
    {
        private Model.Pedido _modelPedido = new Model.Pedido();
        private Model.PessoaContato _modelPessoaContato = new Model.PessoaContato();
        private Model.PessoaEndereco _modelPessoaAddr = new Model.PessoaEndereco();
        private Model.Pessoa _modelPessoa = new Model.Pessoa();
        private Model.Usuarios _modelUsuario = new Model.Usuarios();

        private Model.PedidoItem _modelPedidoItem = new Model.PedidoItem();
        private Controller.PedidoItem _controllerPedidoItem = new Controller.PedidoItem();
        private Controller.Titulo _controllerTitulo = new Controller.Titulo();

        public bool Print(int idPedido)
        {
            _modelPedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

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
            if (dataCliente != null && !string.IsNullOrEmpty(dataCliente.Id.ToString()))
            {
                _modelPessoaAddr = _modelPessoaAddr.FindByIdUser(dataCliente.Id).FirstOrDefault<Model.PessoaEndereco>();
                _modelPessoaContato = _modelPessoaContato.FindByIdUser(dataCliente.Id).FirstOrDefault<Model.PessoaContato>();
            }

            var Addr = "";
            if (_modelPessoaAddr != null)
                Addr = _modelPessoaAddr.Rua + " " + _modelPessoaAddr.Nr + " - CEP: " + _modelPessoaAddr.Cep + " - " + _modelPessoaAddr.Complemento + " | " + _modelPessoaAddr.Bairro + " - " + _modelPessoaAddr.Cidade + "/" + _modelPessoaAddr.Estado;

            string titulo = "";
            string titulo2 = "";
            bool orcamento = false;
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
                AddressEmpresa = $"{Settings.Default.empresa_rua} {Settings.Default.empresa_nr} - {Settings.Default.empresa_cep} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
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
                titulo = titulo,
                titulo2 = titulo2,
                orcamento = orcamento,
                count = countItens
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.TopMost = true;
            if (f.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PrintOS(int idPedido)
        {
            _modelPedido = _modelPedido.FindById(idPedido).FirstOrDefault<Model.Pedido>();

            Model.Pessoa dataCliente = _modelPessoa.FindById(_modelPedido.Cliente).FirstOrDefault<Model.Pessoa>();
            Model.Usuarios dataVendedor = _modelUsuario.FindByUserId(_modelPedido.Colaborador).FirstOrDefault<Model.Usuarios>();
            if (dataCliente != null && !string.IsNullOrEmpty(dataCliente.Id.ToString()))
            {
                _modelPessoaAddr = _modelPessoaAddr.FindByIdUser(dataCliente.Id).FirstOrDefault<Model.PessoaEndereco>();
                _modelPessoaContato = _modelPessoaContato.FindByIdUser(dataCliente.Id).FirstOrDefault<Model.PessoaContato>();
            }

            var Addr = "";
            if (_modelPessoaAddr != null)
                Addr = _modelPessoaAddr.Rua + " " + _modelPessoaAddr.Nr + " - CEP: " + _modelPessoaAddr.Cep + " - " + _modelPessoaAddr.Complemento + " | " + _modelPessoaAddr.Bairro + " - " + _modelPessoaAddr.Cidade + "/" + _modelPessoaAddr.Estado;

            string titulo = "Ordem de Serviço", titulo2 = "Cliente";
            string gridstyle = "", alabel = "", astyle = "", atext = "", blabel = "", bstyle = "", btext = "", clabel = "", cstyle = "", ctext = "", dlabel = "", dstyle = "", dtext = "", elabel = "", estyle = "", etext = "", flabel = "", fstyle = "", ftext = "", problemalabel = "", problematext = "", solucaolabel = "", solucaotext = "";

            if(!String.IsNullOrEmpty(IniFile.Read("Campo_1_Visible", "OS")))
            {
                if (!Convert.ToBoolean(IniFile.Read("Campo_1_Visible", "OS")))
                    astyle = " hidden='ON' ";
            }

            if (!String.IsNullOrEmpty(IniFile.Read("Campo_2_Visible", "OS")))
            {
                if (!Convert.ToBoolean(IniFile.Read("Campo_2_Visible", "OS")))
                    bstyle = " hidden='ON' ";
            }

            if (!String.IsNullOrEmpty(IniFile.Read("Campo_3_Visible", "OS")))
            {
                if (!Convert.ToBoolean(IniFile.Read("Campo_3_Visible", "OS")))
                    cstyle = " hidden='ON' ";
            }

            if (!String.IsNullOrEmpty(IniFile.Read("Campo_4_Visible", "OS")))
            {
                if (!Convert.ToBoolean(IniFile.Read("Campo_4_Visible", "OS")))
                    dstyle = " hidden='ON' ";
            }

            if (!String.IsNullOrEmpty(IniFile.Read("Campo_5_Visible", "OS")))
            {
                if (!Convert.ToBoolean(IniFile.Read("Campo_5_Visible", "OS")))
                    estyle = " hidden='ON' ";
            }

            if (!String.IsNullOrEmpty(IniFile.Read("Campo_6_Visible", "OS")))
            {
                if (!Convert.ToBoolean(IniFile.Read("Campo_6_Visible", "OS")))
                    fstyle = " hidden='ON' ";
            }

            if(!String.IsNullOrEmpty(dstyle) && !String.IsNullOrEmpty(estyle) && !String.IsNullOrEmpty(fstyle))
                gridstyle = " hidden='ON' ";

            alabel = !String.IsNullOrEmpty(IniFile.Read("Campo_1_Descr", "OS")) ? IniFile.Read("Campo_1_Descr", "OS") : "";
            atext = _modelPedido.campoa;
                        
            blabel = !String.IsNullOrEmpty(IniFile.Read("Campo_2_Descr", "OS")) ? IniFile.Read("Campo_2_Descr", "OS") : ""; 
            btext = _modelPedido.campob;
                        
            clabel = !String.IsNullOrEmpty(IniFile.Read("Campo_3_Descr", "OS")) ? IniFile.Read("Campo_3_Descr", "OS") : ""; 
            ctext = _modelPedido.campoc;

            dlabel = !String.IsNullOrEmpty(IniFile.Read("Campo_4_Descr", "OS")) ? IniFile.Read("Campo_4_Descr", "OS") : ""; 
            dtext = _modelPedido.campod;

            elabel = !String.IsNullOrEmpty(IniFile.Read("Campo_5_Descr", "OS")) ? IniFile.Read("Campo_5_Descr", "OS") : ""; 
            etext = _modelPedido.campoe;

            flabel = !String.IsNullOrEmpty(IniFile.Read("Campo_6_Descr", "OS")) ? IniFile.Read("Campo_6_Descr", "OS") : ""; 
            ftext = _modelPedido.campof; 

            problemalabel = "Problema relatado"; 
            problematext = _modelPedido.problema; 
            solucaolabel = "Serviço realizado"; 
            solucaotext = _modelPedido.solucao;

            var html = Template.Parse(File.ReadAllText($@"{Program.PATH_BASE}\html\OSComprovanteVendaA4.html"));
            var render = html.Render(Hash.FromAnonymousObject(new
            {
                NomeFantasia = Settings.Default.empresa_nome_fantasia,
                CNPJ = Settings.Default.empresa_cnpj,
                AddressEmpresa = $"{Settings.Default.empresa_rua} {Settings.Default.empresa_nr} - {Settings.Default.empresa_cep} - {Settings.Default.empresa_bairro} - {Settings.Default.empresa_cidade}/{Settings.Default.empresa_estado}",
                Logo = Settings.Default.empresa_logo,
                Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                Cliente = dataCliente?.Nome ?? "",
                Vendedor = dataVendedor?.Nome ?? "",                
                Obs = _modelPedido.Observacao,
                Endereco = Addr,
                Telefone = _modelPessoaContato != null ? _modelPessoaContato.Telefone : "",
                Celular = _modelPessoaContato != null ? _modelPessoaContato.Celular : "",                                
                NrVenda = idPedido,
                
                titulo = titulo,
                titulo2 = titulo2,

                astyle = astyle,
                alabel = alabel,
                atext = atext,
                
                bstyle = bstyle,
                blabel = blabel,
                btext = btext,
                
                cstyle = cstyle,
                clabel = clabel,
                ctext = ctext,
                
                dstyle = dstyle,
                dlabel = dlabel,
                dtext = dtext,
                
                estyle = estyle,
                elabel = elabel,
                etext = etext,
                
                fstyle = fstyle,
                flabel = flabel,
                ftext = ftext,
                
                problemalabel = problemalabel,
                problematext = problematext,

                solucaolabel = solucaolabel,
                solucaotext = solucaotext
            }));

            Browser.htmlRender = render;
            var f = new Browser();
            f.TopMost = true;
            if (f.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using ESC_POS_USB_NET.Printer;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    internal class Titulo : Data.Core.Controller
    {
        public static string status { get; set; }

        public double GetTroco(int idPedido)
        {
            if (string.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(recebido) as recebido, SUM(total) as total")
                .Where("id_pedido", idPedido).Where("excluir", 0).FirstOrDefault();
            var total = data.TOTAL ?? 0;
            var recebido = data.RECEBIDO ?? 0;

            return Validation.ConvertToDouble(total - recebido);
        }

        public double GetLancados(int idPedido)
        {
            if (string.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(recebido) as recebido").Where("id_pedido", idPedido)
                .Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.RECEBIDO ?? 0);
        }

        public double GetRestante(int idPedido)
        {
            if (string.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(total) as total").Where("id_pedido", idPedido)
                .Where("excluir", 0).FirstOrDefault();
            var lancado = Validation.ConvertToDouble(data.TOTAL ?? 0);

            double restante = 0;
            if (lancado < GetTotalPedido(idPedido)) restante = lancado - GetTotalPedido(idPedido);

            return restante * -1;
        }

        public double GetTotalPedido(int idPedido)
        {
            if (string.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("total").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data?.TOTAL ?? 0);
        }

        public double GetTotalProdutos(int idPedido)
        {
            if (string.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("produtos").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.PRODUTOS ?? 0);
        }

        public double GetTotalDesconto(int idPedido)
        {
            if (string.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("desconto").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.DESCONTO ?? 0);
        }

        public double GetTotalFrete(int idPedido)
        {
            if (string.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("frete").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.FRETE ?? 0);
        }

        public bool AddPagamento(int idPedido, int formaPgto, string valorS, string inicio, string parcela = "1",
            int idTaxa = 0)
        {
            var _mTaxa = new Taxas();
            var data = new Model.Titulo();
            var valor = Validation.ConvertToDouble(valorS);
            var vencimento = DateTime.Now;

            if (Validation.ConvertToDouble(valorS) <= 0)
            {
                Alert.Message("Opss", "O valor informado é inválido!", Alert.AlertType.error);
                return false;
            }

            if (idTaxa > 0)
                _mTaxa = _mTaxa.FindById(idTaxa).FirstOrDefault<Taxas>();

            if (idPedido > 0)
            {
                if (GetRestante(idPedido) <= 0)
                {
                    Alert.Message("Opss", "Valor total já recebido. Verifique os lançamentos!", Alert.AlertType.error);
                    return false;
                }

                data.Id_Pedido = idPedido;

                var clienteId = new Model.Pedido().FindById(idPedido).Select("cliente").Where("excluir", 0)
                    .FirstOrDefault();
                data.Id_Pessoa = clienteId.CLIENTE ?? 0;
            }

            if (valor < 0)
                return false;

            if (!string.IsNullOrEmpty(inicio))
                vencimento = Validation.ConvertStringDateTime(inicio);

            if (vencimento.ToString().Contains("01/01/0001"))
            {
                Alert.Message("Opss", "Data inválida", Alert.AlertType.error);
                return false;
            }

            //2 CHEQUE 4 CARTÃO DE CRÉDITO 5 CREDIÁRIO 6 BOLETO
            if (parcela.IndexOf("+") > 0)
            {
                //15+20+30+50+70 / dias e parcelas

                var numeros = parcela.Split(new[] {"+"}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray();

                var qtdDecimall = Validation.GetNumberOfDigits((decimal) valor);
                var qtdD = qtdDecimall + 1;
                data.Total = Validation.Round(valor / Validation.ConvertToInt32(parcela), qtdD);

                for (var i = 0; i < numeros.Length; i++)
                {
                    vencimento = vencimento.AddDays(numeros[i]);

                    data.Id = 0;
                    data.Id_FormaPgto = formaPgto;
                    data.Emissao = Validation.DateNowToSql();
                    data.Vencimento = Validation.ConvertDateToSql(vencimento);
                    data.Recebido = data.Total;

                    double taxaAntecipacao = 0;
                    bool parcelaJuros = false;
                    if (formaPgto == 4)
                    {
                        if (_mTaxa.Antecipacao_Auto == 1)
                            taxaAntecipacao = _mTaxa.Taxa_Antecipacao;

                        var taxacredito = valor / 100 * _mTaxa.Taxa_Credito;
                        var taxaparcelas = valor / 100 * _mTaxa.Taxa_Parcela;
                        var taxaFixa = _mTaxa.Taxa_Fixa / Validation.ConvertToInt32(parcela);
                        var taxaAntecipacaoParcela = taxaAntecipacao / Validation.ConvertToInt32(parcela);

                        if (_mTaxa.Parcela_Semjuros > 0)
                        {
                            if (i > _mTaxa.Parcela_Semjuros)
                            {
                                parcelaJuros = true;
                                data.Valor_Liquido = (valor - taxacredito - taxaFixa - taxaAntecipacaoParcela - taxaparcelas) /
                                                     Validation.ConvertToInt32(parcela); // com juros
                            }
                            else
                                data.Valor_Liquido = (valor - taxacredito - taxaFixa - taxaAntecipacaoParcela) /
                                                     Validation.ConvertToInt32(parcela); // sem juros
                        }
                        else
                            data.Valor_Liquido = (valor - taxacredito - taxaFixa - taxaAntecipacaoParcela) /
                                                 Validation.ConvertToInt32(parcela);
                    }

                    data.Taxas = $@"{_mTaxa.Taxa_Fixa}|{_mTaxa.Taxa_Credito}|{_mTaxa.Taxa_Parcela}|{taxaAntecipacao}|{_mTaxa.Dias_Receber}|{parcela}|{parcelaJuros}";
                    data.Id_Caixa = Home.idCaixa;
                    data.Tipo = "Receber";
                    data.Save(data, false);
                }
            }
            else if (Validation.ConvertToInt32(parcela) > 0 && formaPgto != 1 && formaPgto != 3)
            {
                var qtdDecimall = Validation.GetNumberOfDigits((decimal) valor);
                var qtdD = qtdDecimall + 1;
                data.Total = Validation.Round(valor / Validation.ConvertToInt32(parcela), qtdD);

                var count = 1;
                while (count <= Validation.ConvertToInt32(parcela))
                {
                    data.Id = 0;
                    data.Id_FormaPgto = formaPgto;
                    data.Emissao = Validation.DateNowToSql();
                    data.Vencimento = Validation.ConvertDateToSql(vencimento.AddMonths(count));
                    data.Recebido = data.Total;

                    double taxaAntecipacao = 0;
                    bool parcelaJuros = false;
                    if (formaPgto == 4)
                    {
                        if (_mTaxa.Antecipacao_Auto == 1)
                            taxaAntecipacao = _mTaxa.Taxa_Antecipacao;

                        // taxa de intermediação
                        var taxacredito = valor / 100 * _mTaxa.Taxa_Credito;
                        var taxaparcelas = valor / 100 * _mTaxa.Taxa_Parcela;
                        var taxaFixa = _mTaxa.Taxa_Fixa / Validation.ConvertToInt32(parcela);
                        var taxaAntecipacaoParcela = taxaAntecipacao / Validation.ConvertToInt32(parcela);

                        if (_mTaxa.Parcela_Semjuros > 0)
                        {
                            if (count > _mTaxa.Parcela_Semjuros)
                            {
                                parcelaJuros = true;
                                data.Valor_Liquido = (valor - taxacredito - taxaFixa - taxaAntecipacaoParcela - taxaparcelas) /
                                                     Validation.ConvertToInt32(parcela); // com juros
                            }
                            else
                                data.Valor_Liquido = (valor - taxacredito - taxaFixa - taxaAntecipacaoParcela) /
                                                     Validation.ConvertToInt32(parcela); // sem juros
                        }
                        else
                            data.Valor_Liquido = (valor - taxacredito - taxaFixa - taxaAntecipacaoParcela) /
                                                 Validation.ConvertToInt32(parcela);
                    }
                    
                    data.Taxas = $@"{_mTaxa.Taxa_Fixa}|{_mTaxa.Taxa_Credito}|{_mTaxa.Taxa_Parcela}|{taxaAntecipacao}|{_mTaxa.Dias_Receber}|{parcela}|{parcelaJuros}";
                    data.Id_Caixa = Home.idCaixa;
                    data.Tipo = "Receber";
                    data.Save(data, false);
                    count++;
                }
            }
            else
            {
                //1 DINHEIRO 3 CARTÃO DE DÉBITO

                data.Id = 0;
                data.Id_FormaPgto = formaPgto;
                data.Emissao = Validation.DateNowToSql();
                data.Vencimento = !string.IsNullOrEmpty(inicio)
                    ? Validation.ConvertDateToSql(inicio)
                    : Validation.DateNowToSql();

                double taxaAntecipacao = 0;
                if (formaPgto == 1 && valor > GetRestante(idPedido))
                {
                    data.Total = GetRestante(idPedido);
                    data.Recebido = valor;
                }
                else
                {
                    data.Total = valor;
                    data.Recebido = valor;

                    if (_mTaxa.Antecipacao_Auto == 1)
                        taxaAntecipacao = _mTaxa.Taxa_Antecipacao;

                    var taxadebito = valor / 100 * _mTaxa.Taxa_Debito;
                    data.Valor_Liquido = valor - taxadebito - _mTaxa.Taxa_Fixa - taxaAntecipacao;
                }

                data.Taxas = $@"{_mTaxa.Taxa_Fixa}|{_mTaxa.Taxa_Debito}|{_mTaxa.Taxa_Parcela}|{taxaAntecipacao}|{_mTaxa.Dias_Receber}|{parcela}|{_mTaxa.Parcela_Semjuros}";
                data.Id_Caixa = Home.idCaixa;
                data.Tipo = Home.pedidoPage == "Compras" ? "Pagar" : "Receber";

                return data.Save(data, false);
            }

            return false;
        }

        public IEnumerable<dynamic> GetDataPgtosLancados(int idPedido)
        {
            var data = new Model.Titulo().Query()
                .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                .Select("titulo.id", "titulo.total", "titulo.recebido", "titulo.vencimento",
                    "formapgto.nome as formapgto", "formapgto.id as formapgtoid")
                .Where("titulo.excluir", 0)
                .Where("titulo.id_pedido", idPedido)
                .OrderByDesc("titulo.id");

            return data.Get();
        }

        public IEnumerable<dynamic> GetDataTableTitulosGerados(string tela, string Search, int tipo, string dataInicial,
            string dataFinal)
        {
            var titulos = new Model.Titulo();

            var tipoPesquisa = tipo == 0 ? "titulo.vencimento" : "titulo.emissao";

            var search = "%" + Search + "%";
            var data = titulos.Query()
                .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                .LeftJoin("pessoa", "pessoa.id", "titulo.id_pessoa")
                .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "titulo.emissao", "titulo.total",
                    "titulo.id_pedido", "titulo.baixa_data", "titulo.baixa_total", "titulo.valor_liquido",
                    "formapgto.nome as formapgto", "pessoa.nome", "pessoa.fantasia", "pessoa.rg", "pessoa.cpf")
                .Where(tipoPesquisa, ">=", Validation.ConvertDateToSql(dataInicial))
                .Where(tipoPesquisa, "<=", Validation.ConvertDateToSql(dataFinal))
                .Where("titulo.excluir", 0)
                .Where("titulo.tipo", tela)
                .OrderByDesc("titulo.criado");

            switch (status)
            {
                case "Pendentes":
                    data.Where
                    (
                        q => q.Where("titulo.recebido", "=", 0)
                    );
                    break;
                case "Recebidos":
                case "Pagos":
                    data.Where
                    (
                        q => q.Where("titulo.recebido", "<>", 0)
                    );
                    break;
            }

            if (!string.IsNullOrEmpty(Search))
                data.Where
                (
                    q => q.Where("pessoa.nome", "like", search)
                );

            return data.Get();
        }

        public void GetDataTableTitulos(DataGridView Table, int idPedido)
        {
            Table.Rows.Clear();

            var fpgtos = new ArrayList();
            var fpgto = new FormaPagamento().FindAll().WhereFalse("excluir").OrderByDesc("id").Get();
            foreach (var item in fpgto)
                fpgtos.Add(new {Id = $"{item.ID}", Nome = $"{item.NOME}"});

            //var titulos = new Model.Titulo();
            //var data = titulos.Query()
            //    .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
            //    .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "formapgto.nome as formapgto")
            //    .Where("titulo.excluir", 0)
            //    .Where("titulo.id_pedido", idPedido)
            //    .OrderByDesc("titulo.id")
            //    .Get();

            var data = GetDataPgtosLancados(idPedido);

            //foreach (var item in data)
            //{
            //    Table.Rows.Add(
            //        item.ID,
            //        item.FORMAPGTO,
            //        Validation.ConvertDateToForm(item.VENCIMENTO),
            //        Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true),
            //        new Bitmap(Properties.Resources.bin16x)
            //    );
            //}

            for (var i = 0; i < data.Count(); i++)
            {
                var item = data.ElementAt(i);
                var n = Table.Rows.Add();

                var cellFPGTOS = new DataGridViewComboBoxCell();
                if (fpgtos.Count > 0)
                {
                    cellFPGTOS.ValueMember = "Id";
                    cellFPGTOS.DisplayMember = "Nome";
                    cellFPGTOS.Style.NullValue = item.FORMAPGTO;
                    cellFPGTOS.DataSource = fpgtos;
                }

                Table.Rows[n].Cells[0].Value = item.ID;
                Table.Rows[n].Cells[1] = cellFPGTOS;
                Table.Rows[n].Cells[2].Value = Validation.ConvertDateToForm(item.VENCIMENTO);
                Table.Rows[n].Cells[3].Value = Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), false);
                Table.Rows[n].Cells[4].Value = new Bitmap(Resources.bin16x);
            }
        }

        public void GetDataTableTitulosGeradosFilter(DataGridView Table, string tela, string Search, int tipo,
            string dataInicial, string dataFinal)
        {
            Table.ColumnCount = 7;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table,
                new object[] {true});
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Emissão";
            Table.Columns[1].Width = 100;

            Table.Columns[2].Name = Home.financeiroPage == "Receber" ? "Receber de" : "Pagar para";
            Table.Columns[2].Width = 150;

            Table.Columns[3].Name = "Forma de Pagamento";
            Table.Columns[3].Width = 160;

            Table.Columns[4].Name = "Vencimento";
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Total";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;

            Table.Columns[6].Name = Home.financeiroPage == "Receber" ? "Recebido" : "Pago";
            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[6].Width = 100;

            Table.Rows.Clear();

            foreach (var item in GetDataTableTitulosGerados(tela, Search, tipo, dataInicial, dataFinal))
                Table.Rows.Add(
                    item.ID,
                    Validation.ConvertDateToForm(item.EMISSAO),
                    item.NOME,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.VENCIMENTO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true)
                );

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void Imprimir(int idTitulo, string tipo = "Bobina 80mm", int modelo = 0)
        {
            if (IniFile.Read("Printer", "Comercial") == "Bobina 80mm" || tipo == "Bobina 80mm")
            {
                var printerModel = IniFile.Read("Model", "SAT");
                var printerPort = IniFile.Read("Port", "SAT");

                if (printerModel == "Bematech")
                {
                    #region EMITENTE

                    var _emitente = new Model.Pessoa();
                    var _emitenteEndereco = new PessoaEndereco();
                    var _emitenteContato = new PessoaContato();

                    _emitente.RG = Settings.Default.empresa_inscricao_estadual;
                    _emitente.CPF = Settings.Default.empresa_cnpj;

                    _emitente.Nome = Settings.Default.empresa_razao_social;
                    _emitente.Fantasia = Settings.Default.empresa_nome_fantasia;

                    _emitenteEndereco.Rua = Settings.Default.empresa_rua;
                    _emitenteEndereco.Nr = Settings.Default.empresa_nr;
                    _emitenteEndereco.Bairro = Settings.Default.empresa_bairro;
                    _emitenteEndereco.Cidade = Settings.Default.empresa_cidade;
                    _emitenteEndereco.Cep = Settings.Default.empresa_cep;
                    _emitenteEndereco.IBGE = Settings.Default.empresa_ibge;
                    _emitenteEndereco.Estado = Settings.Default.empresa_estado;

                    #endregion EMITENTE

                    var _titulo = new Model.Titulo().FindById(idTitulo).First<Model.Titulo>();
                    var _destinatario = new Model.Pessoa().FindById(_titulo.Id_Pessoa).FirstOrDefault<Model.Pessoa>();

                    int iRetorno = 0;
                    string linewithdot = "------------------------------------------------------------------";
                    string pLine = "\r\n";

                    iRetorno = Bematech.ConfiguraModeloImpressora(7);
                    //iRetorno = Bematech.IniciaPorta(printerPort);
                    iRetorno = Bematech.IniciaPorta("USB");
                    iRetorno = Bematech.FormataTX(linewithdot + pLine, 1, 0, 0, 0, 0);

                    iRetorno = Bematech.ComandoTX(Validation.alignBematech(1), Validation.alignBematech(1).Length);
                    iRetorno = Bematech.FormataTX(Validation.CleanStringForFiscal(_emitente.Fantasia) + pLine, 3, 0, 0, 0, 1);
                    iRetorno = Bematech.FormataTX(Validation.CleanStringForFiscal(_emitenteEndereco.Rua) + ", " + Validation.CleanStringForFiscal(_emitenteEndereco.Nr) + " - " + Validation.CleanStringForFiscal(_emitenteEndereco.Bairro) + pLine, 1, 0, 0, 0, 0);
                    iRetorno = Bematech.FormataTX(Validation.CleanStringForFiscal(_emitenteEndereco.Cidade) + "/" + Validation.CleanStringForFiscal(_emitenteEndereco.Estado) + pLine, 1, 0, 0, 0, 0);
                    
                    iRetorno = Bematech.FormataTX(linewithdot + pLine, 1, 0, 0, 0, 0);
                    iRetorno = Bematech.FormataTX("Extrato N." + _titulo.Id + pLine, 1, 0, 0, 0, 0);
                    
                    if (_destinatario != null)
                    {
                        if (_titulo.Tipo == "Pagar")
                            iRetorno = Bematech.FormataTX("Fornecedor: " + _destinatario.Nome + pLine, 1, 0, 0, 0, 0);
                        else
                            iRetorno = Bematech.FormataTX("Cliente: " + _destinatario.Nome + pLine, 1, 0, 0, 0, 0);
                    }

                    iRetorno = Bematech.FormataTX("Vencimento: " + Validation.ConvertDateToForm(_titulo.Vencimento) + pLine, 1, 0, 0, 0, 0);
                    
                    string formapgto = "";

                    switch (_titulo.Id_FormaPgto)
                    {
                        case 1:
                            formapgto = "Dinheiro";
                            break;

                        case 2:
                            formapgto = "Cheque";
                            break;

                        case 3:
                            formapgto = "Cartão de Débito";
                            break;

                        case 4:
                            formapgto = "Cartão de Crédito";
                            break;

                        case 5:
                            formapgto = "Crediário";
                            break;

                        case 6:
                            formapgto = "Boleto";
                            break;

                        default:
                            formapgto = "N/D";
                            break;
                    }
                    
                    iRetorno = Bematech.FormataTX(AddSpaces("Tipo: " + Validation.CleanStringForFiscal(formapgto), "Total R$: " + Validation.FormatPrice(Validation.ConvertToDouble(_titulo.Total)) + pLine), 1, 0, 0, 0, 0);
                    iRetorno = Bematech.FormataTX(linewithdot + pLine, 1, 0, 0, 0, 0);

                    if (_titulo.Recebido > 0)
                    {
                        try
                        {
                            iRetorno = Bematech.FormataTX(AddSpaces("Recebido: " + Validation.ConvertDateToForm(_titulo.Baixa_data), "Total R$: " + Validation.FormatPrice(Validation.ConvertToDouble(_titulo.Recebido)) + pLine), 1, 0, 0, 0, 0);
                        }
                        catch (Exception)
                        {
                            iRetorno = Bematech.FormataTX(AddSpaces("Recebido: ", "Total R$: " + Validation.FormatPrice(Validation.ConvertToDouble(_titulo.Recebido)) + pLine), 1, 0, 0, 0, 0);
                        }                        
                    }

                    iRetorno = Bematech.FormataTX(pLine, 1, 0, 0, 0, 0);
                    iRetorno = Bematech.FormataTX(pLine, 1, 0, 0, 0, 0);
                    iRetorno = Bematech.FormataTX(pLine, 1, 0, 0, 0, 0);

                    iRetorno = Bematech.AcionaGuilhotina(0);
                    iRetorno = Bematech.FechaPorta();
                }
                else
                {
                    #region EMITENTE

                    var _emitente = new Model.Pessoa();
                    var _emitenteEndereco = new PessoaEndereco();
                    var _emitenteContato = new PessoaContato();

                    _emitente.RG = Settings.Default.empresa_inscricao_estadual;
                    _emitente.CPF = Settings.Default.empresa_cnpj;

                    _emitente.Nome = Settings.Default.empresa_razao_social;
                    _emitente.Fantasia = Settings.Default.empresa_nome_fantasia;

                    _emitenteEndereco.Rua = Settings.Default.empresa_rua;
                    _emitenteEndereco.Nr = Settings.Default.empresa_nr;
                    _emitenteEndereco.Bairro = Settings.Default.empresa_bairro;
                    _emitenteEndereco.Cidade = Settings.Default.empresa_cidade;
                    _emitenteEndereco.Cep = Settings.Default.empresa_cep;
                    _emitenteEndereco.IBGE = Settings.Default.empresa_ibge;
                    _emitenteEndereco.Estado = Settings.Default.empresa_estado;

                    #endregion EMITENTE

                    var _titulo = new Model.Titulo().FindById(idTitulo).First<Model.Titulo>();
                    var _destinatario = new Model.Pessoa().FindById(_titulo.Id_Pessoa).FirstOrDefault<Model.Pessoa>();
                    
                    var printername = IniFile.Read("PrinterName", "Comercial");

                    if (printername == null)
                        return;

                    if (printername == "Selecione")
                    {
                        Alert.Message("Opps", "Você precisa configurar uma impressora.", Alert.AlertType.info);
                        return;
                    }

                    var printer = new Printer(printername);

                    printer.AlignCenter();
                    printer.BoldMode(_emitente.Fantasia);
                    printer.Append(_emitente.Nome);
                    printer.Append(_emitenteEndereco.Rua + ", " + _emitenteEndereco.Nr + " - " + _emitenteEndereco.Bairro);
                    printer.Append(_emitenteEndereco.Cidade + "/" + _emitenteEndereco.Estado);
                    printer.Append(_emitenteContato.Telefone);

                    printer.NewLines(2);
                    printer.Separator();

                    printer.BoldMode("Extrato N°" + _titulo.Id);

                    printer.Separator();

                    printer.AlignLeft();

                    if (_destinatario != null)
                    {
                        if (_titulo.Tipo == "Pagar")
                            printer.Append("Fornecedor: " + _destinatario.Nome);
                        else
                            printer.Append("Cliente: " + _destinatario.Nome);
                    }

                    printer.Append(("Vencimento: " + Validation.ConvertDateToForm(_titulo.Vencimento)));

                    string formapgto = "";

                    switch (_titulo.Id_FormaPgto)
                    {
                        case 1:
                            formapgto = "Dinheiro";
                            break;

                        case 2:
                            formapgto = "Cheque";
                            break;

                        case 3:
                            formapgto = "Cartão de Débito";
                            break;

                        case 4:
                            formapgto = "Cartão de Crédito";
                            break;

                        case 5:
                            formapgto = "Crediário";
                            break;

                        case 6:
                            formapgto = "Boleto";
                            break;

                        default:
                            formapgto = "N/D";
                            break;
                    }

                    printer.Append(AddSpaces("Tipo: " + Validation.CleanStringForFiscal(formapgto), "Total R$: " + Validation.FormatPrice(Validation.ConvertToDouble(_titulo.Total))));
                    printer.Separator();

                    if (_titulo.Recebido > 0)
                    {
                        try
                        {
                            printer.Append(AddSpaces("Recebido: " + Validation.ConvertDateToForm(_titulo.Baixa_data), "Total R$: " + Validation.FormatPrice(Validation.ConvertToDouble(_titulo.Recebido))));
                        }
                        catch (Exception)
                        {
                            printer.Append(AddSpaces("Recebido: ", "Total R$: " + Validation.FormatPrice(Validation.ConvertToDouble(_titulo.Recebido))));
                        }
                    }

                    printer.NewLines(5);                

                    printer.FullPaperCut();
                    printer.PrintDocument();
                }
            }
        }

        public static string AddSpaces(string valueF, string valueE)
        {
            if ((valueF + valueE).Length <= 48)
                return valueF + "".PadLeft(48 - (valueF.Length + valueE.Length)) + valueE;
            else
                return valueF + " " + valueE;
        }
    }
}
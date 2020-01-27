using Emiplus.Data.Helpers;
using Emiplus.View.Common;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    internal class Titulo : Data.Core.Controller
    {
        public static string status { get; set; }

        public double GetTroco(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(recebido) as recebido, SUM(total) as total").Where("id_pedido", idPedido).Where("excluir", 0).FirstOrDefault();
            var total = data.TOTAL ?? 0;
            var recebido = data.RECEBIDO ?? 0;

            return Validation.ConvertToDouble(total - recebido);
        }

        public double GetLancados(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(recebido) as recebido").Where("id_pedido", idPedido).Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.RECEBIDO ?? 0);
        }

        public double GetRestante(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Titulo().Query().SelectRaw("SUM(total) as total").Where("id_pedido", idPedido).Where("excluir", 0).FirstOrDefault();
            var lancado = Validation.ConvertToDouble(data.TOTAL ?? 0);

            double restante = 0;
            if (lancado < GetTotalPedido(idPedido))
            {
                restante = lancado - GetTotalPedido(idPedido);
            }

            return restante * -1;
        }

        public double GetTotalPedido(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("total").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.TOTAL ?? 0);
        }

        public double GetTotalProdutos(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("produtos").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.PRODUTOS ?? 0);
        }

        public double GetTotalDesconto(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("desconto").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.DESCONTO ?? 0);
        }

        public double GetTotalFrete(int idPedido)
        {
            if (String.IsNullOrEmpty(idPedido.ToString()))
                return 0;

            var data = new Model.Pedido().FindById(idPedido).Select("frete").Where("excluir", 0).FirstOrDefault();
            return Validation.ConvertToDouble(data.FRETE ?? 0);
        }

        public bool AddPagamento(int idPedido, int formaPgto, string valorS, string inicio, string parcela = "1")
        {
            var data = new Model.Titulo();
            double valor = Validation.ConvertToDouble(valorS);
            DateTime vencimento = DateTime.Now;

            if (Validation.ConvertToDouble(valorS) <= 0)
            {
                Alert.Message("Opss", "O valor informado é inválido!", Alert.AlertType.error);
                return false;
            }

            if (idPedido > 0)
            {
                if (GetRestante(idPedido) <= 0)
                {
                    Alert.Message("Opss", "Valor total já recebido. Verifique os lançamentos!", Alert.AlertType.error);
                    return false;
                }

                data.Id_Pedido = idPedido;

                var clienteId = new Model.Pedido().FindById(idPedido).Select("cliente").Where("excluir", 0).FirstOrDefault();
                data.Id_Pessoa = clienteId.CLIENTE ?? 0;
            }
            if (valor < 0)
            {
                return false;
            }

            if (!String.IsNullOrEmpty(inicio))
            {
                vencimento = Convert.ToDateTime(inicio);
            }

            //2 CHEQUE 4 CARTÃO DE CRÉDITO 5 CREDIÁRIO 6 BOLETO
            if (parcela.IndexOf("+") > 0)
            {
                //15+20+30+50+70 / dias e parcelas

                int[] numeros = parcela.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();

                data.Total = Validation.Round(valor / numeros.Count());

                for (int i = 0; i < numeros.Length; i++)
                {
                    vencimento = vencimento.AddDays(numeros[i]);

                    data.Id = 0;
                    data.Id_FormaPgto = formaPgto;
                    data.Emissao = Validation.DateNowToSql();
                    data.Vencimento = Validation.ConvertDateToSql(vencimento);
                    data.Recebido = data.Total;
                    data.Id_Caixa = Home.idCaixa;
                    data.Save(data, false);
                }
            }
            else if (Validation.ConvertToInt32(parcela) > 0 && formaPgto != 1 && formaPgto != 3)
            {
                data.Total = Validation.Round(valor / Validation.ConvertToInt32(parcela));

                int count = 1;
                while (count <= Validation.ConvertToInt32(parcela))
                {
                    data.Id = 0;
                    data.Id_FormaPgto = formaPgto;
                    data.Emissao = Validation.DateNowToSql();
                    data.Vencimento = Validation.ConvertDateToSql(vencimento.AddMonths(count));
                    data.Recebido = data.Total;
                    data.Id_Caixa = Home.idCaixa;
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

                if (!String.IsNullOrEmpty(inicio))
                    data.Vencimento = Validation.ConvertDateToSql(inicio);
                else
                    data.Vencimento = Validation.DateNowToSql();

                if (formaPgto == 1 && valor > GetRestante(idPedido))
                {
                    data.Total = GetRestante(idPedido);
                    data.Recebido = valor;
                }
                else
                {
                    data.Total = valor;
                    data.Recebido = valor;
                }

                data.Id_Caixa = Home.idCaixa;

                if (Home.pedidoPage == "Compras")
                    data.Tipo = "Pagar";
                else
                    data.Tipo = "Receber";

                if (data.Save(data, false))
                    return true;

                return false;
            }

            return false;
        }

        public IEnumerable<dynamic> GetDataPgtosLancados(int idPedido)
        {
            var data = new Model.Titulo().Query()
                .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "formapgto.nome as formapgto")
                .Where("titulo.excluir", 0)
                .Where("titulo.id_pedido", idPedido)
                .OrderByDesc("titulo.id");

            return data.Get();
        }

        public IEnumerable<dynamic> GetDataTableTitulosGerados(string tela, string Search, int tipo, string dataInicial, string dataFinal)
        {
            var titulos = new Model.Titulo();

            string tipoPesquisa = "titulo.vencimento";
            if (tipo == 0)
                tipoPesquisa = "titulo.vencimento";
            else
                tipoPesquisa = "titulo.emissao";

            var search = "%" + Search + "%";
            var data = titulos.Query()
                .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
                .LeftJoin("pessoa", "pessoa.id", "titulo.id_pessoa")
                .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "titulo.emissao", "titulo.total", "titulo.id_pedido", "titulo.baixa_data", "titulo.baixa_total", "formapgto.nome as formapgto", "pessoa.nome", "pessoa.fantasia", "pessoa.rg", "pessoa.cpf")
                .Where(tipoPesquisa, ">=", Validation.ConvertDateToSql(dataInicial))
                .Where(tipoPesquisa, "<=", Validation.ConvertDateToSql(dataFinal))
                .Where("titulo.excluir", 0)
                .Where("titulo.tipo", tela)
                .OrderByDesc("titulo.criado");

            if (Controller.Titulo.status == "Pendentes")
            {
                data.Where
                (
                    q => q.Where("titulo.recebido", "=", 0)
                );
            }

            if (Controller.Titulo.status == "Recebidos" || Controller.Titulo.status == "Pagos")
            {
                data.Where
                (
                    q => q.Where("titulo.recebido", "<>", 0)
                );
            }

            if (!string.IsNullOrEmpty(Search))
            {
                data.Where
                (
                    q => q.Where("pessoa.nome", "like", search)
                );
            }

            return data.Get();
        }

        public void GetDataTableTitulos(DataGridView Table, int idPedido)
        {
            Table.Rows.Clear();

            //var titulos = new Model.Titulo();
            //var data = titulos.Query()
            //    .LeftJoin("formapgto", "formapgto.id", "titulo.id_formapgto")
            //    .Select("titulo.id", "titulo.recebido", "titulo.vencimento", "formapgto.nome as formapgto")
            //    .Where("titulo.excluir", 0)
            //    .Where("titulo.id_pedido", idPedido)
            //    .OrderByDesc("titulo.id")
            //    .Get();

            var data = GetDataPgtosLancados(idPedido);

            foreach (var item in data)
            {
                Table.Rows.Add(
                    item.ID,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.VENCIMENTO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true),
                    new Bitmap(Properties.Resources.bin16x)
                );
            }
        }

        public void GetDataTableTitulosGeradosFilter(DataGridView Table, string tela, string Search, int tipo, string dataInicial, string dataFinal)
        {
            Table.ColumnCount = 7;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, Table, new object[] { true });
            Table.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            Table.RowHeadersVisible = false;

            Table.Columns[0].Name = "ID";
            Table.Columns[0].Visible = false;

            Table.Columns[1].Name = "Emissão";
            Table.Columns[1].Width = 100;

            if (Home.financeiroPage == "Receber")
                Table.Columns[2].Name = "Receber de";
            else
                Table.Columns[2].Name = "Pagar para";

            Table.Columns[2].Width = 150;

            Table.Columns[3].Name = "Forma de Pagamento";
            Table.Columns[3].Width = 160;

            Table.Columns[4].Name = "Vencimento";
            Table.Columns[4].Width = 100;

            Table.Columns[5].Name = "Total";
            Table.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[5].Width = 100;

            if (Home.financeiroPage == "Receber")
                Table.Columns[6].Name = "Recebido";
            else
                Table.Columns[6].Name = "Pago";

            Table.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns[6].Width = 100;

            Table.Rows.Clear();

            foreach (var item in GetDataTableTitulosGerados(tela, Search, tipo, dataInicial, dataFinal))
            {
                Table.Rows.Add(
                    item.ID,
                    Validation.ConvertDateToForm(item.EMISSAO),
                    item.NOME,
                    item.FORMAPGTO,
                    Validation.ConvertDateToForm(item.VENCIMENTO),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.TOTAL), true),
                    Validation.FormatPrice(Validation.ConvertToDouble(item.RECEBIDO), true)
                );
            }

            Table.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
using System;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Financeiro;
using SqlKata.Execution;

namespace Emiplus.Controller
{
    internal class Caixa
    {
        private readonly CaixaMovimentacao _modelCaixaMov = new CaixaMovimentacao();
        private readonly Model.Pedido _modelPedido = new Model.Pedido();
        private readonly Model.Titulo _modelTitulo = new Model.Titulo();

        public double SumSaidas(int idCaixa)
        {
            var sumSaidas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa)
                .Where(q => q.Where("tipo", 1).OrWhere("tipo", 2)).WhereFalse("excluir").FirstOrDefault();
            return (double) Validation.ConvertToDouble(sumSaidas.TOTAL);
        }

        public double SumEntradasDinheiro(int idCaixa)
        {
            var sumEntradas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa)
                .Where("tipo", 3).Where("id_formapgto", 1).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sumEntradas.TOTAL ?? 0) + SumPagamento(idCaixa, 1) ?? 0;
        }

        public double SumEntradas(int idCaixa)
        {
            var sumEntradas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa)
                .Where("tipo", 3).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sumEntradas.TOTAL ?? 0) + SumPagamento(idCaixa, 1) ?? 0;
        }

        public double SumSaldoFinal(int idCaixa)
        {
            return Validation.Round(SumEntradas(idCaixa) - SumSaidas(idCaixa));
        }

        public double SumVendasTotal(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa)
                .WhereFalse("excluir").FirstOrDefault();
            return (double) Validation.ConvertToDouble(sum.TOTAL);
        }

        public double SumVendasAcrescimos(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(FRETE) as TOTAL").Where("id_caixa", idCaixa)
                .WhereFalse("excluir").FirstOrDefault();
            return (double) Validation.ConvertToDouble(sum.TOTAL);
        }

        public double SumVendasDescontos(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(DESCONTO) as TOTAL").Where("id_caixa", idCaixa)
                .WhereFalse("excluir").FirstOrDefault();
            return (double) Validation.ConvertToDouble(sum.TOTAL);
        }

        public int SumVendasGeradas(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("COUNT(id) as TOTAL").Where("id_caixa", idCaixa)
                .WhereFalse("excluir").FirstOrDefault();
            return (int) Validation.ConvertToInt32(sum.TOTAL);
        }

        public double SumVendasMedia(int idCaixa)
        {
            if (Math.Abs(SumVendasTotal(idCaixa)) < 0)
                return 0;

            return Validation.Round(SumVendasTotal(idCaixa) / SumVendasGeradas(idCaixa));
        }

        public double SumVendasCanceladasTotal(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa)
                .Where("excluir", 1).FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL) ?? 0;
        }

        public int SumVendasCanceladasGeradas(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("COUNT(id) as TOTAL").Where("id_caixa", idCaixa)
                .Where("excluir", 1).FirstOrDefault();
            return (int) Validation.ConvertToInt32(sum.TOTAL);
        }

        public double SumPagamento(int idCaixa, int formaPgto)
        {
            var sum = _modelTitulo.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa)
                .Where("id_formapgto", formaPgto).WhereFalse("excluir")
                .Where(q => q.Where("tipo", "Receber").OrWhere("tipo", null)).FirstOrDefault();
            return (double) Validation.ConvertToDouble(sum.TOTAL);
        }

        public double SumPagamentoTodos(int idCaixa)
        {
            var sum = _modelTitulo.Query().SelectRaw("SUM(TOTAL) as TOTAL").WhereFalse("excluir")
                .Where("id_caixa", idCaixa)
                .Where(q => q.Where("tipo", "Receber").OrWhere("tipo", null)).FirstOrDefault();
            return (double) Validation.ConvertToDouble(sum.TOTAL);
        }

        public void CheckCaixaDate()
        {
            CheckCaixa();

            var caixa = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id)
                .Where("criado", "<", Validation.ConvertDateToSql(DateTime.Now)).WhereFalse("excluir").FirstOrDefault();
            if (caixa != null)
            {
                Home.idCaixa = caixa.ID;

                var msg =
                    $"Antes de começar, há um caixa aberto do dia: {Validation.ConvertDateToForm(caixa.CRIADO)}. {Environment.NewLine}Deseja realizar o FECHAMENTO agora?";
                var Title = "Atenção!";

                var result = AlertOptions.Message(Title, msg, AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    DetailsCaixa.idCaixa = Home.idCaixa;
                    using (var f = new DetailsCaixa())
                    {
                        f.ShowDialog();
                    }
                }
                else
                {
                    AlertOptions.Message("Atenção!",
                        "Os recebimentos gerados a partir de vendas serão lançados no caixa aberto!",
                        AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                }
            }

            var caixaAberto = new Model.Caixa().Query().Where("tipo", "Aberto")
                .Where("usuario", Settings.Default.user_id).WhereFalse("excluir").FirstOrDefault();
            if (caixaAberto == null)
            {
                var result = AlertOptions.Message("Atenção!",
                    $"Você não possui um Caixa aberto.{Environment.NewLine} Deseja abrir agora?",
                    AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                if (result)
                    using (var f = new AbrirCaixa())
                    {
                        f.ShowDialog();
                    }
            }
        }

        private static void CheckCaixa()
        {
            // Verifica se o caixa do usuário está aberto
            if (Home.idCaixa == 0)
            {
                var caixa = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id)
                    .WhereFalse("excluir").FirstOrDefault();
                if (caixa != null)
                    Home.idCaixa = caixa.ID;
            }
        }
    }
}
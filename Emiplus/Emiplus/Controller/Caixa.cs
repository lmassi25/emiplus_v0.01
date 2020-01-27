using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Financeiro;
using SqlKata.Execution;
using System;
using System.Linq;

namespace Emiplus.Controller
{
    internal class Caixa
    {
        private Model.CaixaMovimentacao _modelCaixaMov = new Model.CaixaMovimentacao();
        private Model.Pedido _modelPedido = new Model.Pedido();
        private Model.Titulo _modelTitulo = new Model.Titulo();

        public double SumSaidas(int idCaixa)
        {
            var sumSaidas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa).Where(q => q.Where("tipo", 1).OrWhere("tipo", 2)).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sumSaidas.TOTAL) ?? 0;
        }

        public double SumEntradas(int idCaixa)
        {
            var sumEntradas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa).Where("tipo", 3).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sumEntradas.TOTAL) + SumPagamento(idCaixa, 1) ?? 0;
        }

        public double SumSaldoFinal(int idCaixa)
        {
            return Validation.Round(SumEntradas(idCaixa) - SumSaidas(idCaixa));
        }

        public double SumVendasTotal(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL) ?? 0;
        }

        public double SumVendasAcrescimos(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(FRETE) as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL) ?? 0;
        }

        public double SumVendasDescontos(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(DESCONTO) as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL) ?? 0;
        }

        public int SumVendasGeradas(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("COUNT(id) as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToInt32(sum.TOTAL) ?? 0;
        }

        public double SumVendasMedia(int idCaixa)
        {
            if (SumVendasTotal(idCaixa) == 0 || SumVendasTotal(idCaixa) == 0)
                return 0;

            return Validation.Round(SumVendasTotal(idCaixa) / SumVendasGeradas(idCaixa));
        }

        public double SumVendasCanceladasTotal(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa).Where("excluir", 1).FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL) ?? 0;
        }

        public int SumVendasCanceladasGeradas(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("COUNT(id) as TOTAL").Where("id_caixa", idCaixa).Where("excluir", 1).FirstOrDefault();
            return Validation.ConvertToInt32(sum.TOTAL) ?? 0;
        }

        public double SumPagamento(int idCaixa, int formaPgto)
        {
            var sum = _modelTitulo.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa).Where("id_formapgto", formaPgto).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL) ?? 0;
        }

        public double SumPagamentoTodos(int idCaixa)
        {
            var sum = _modelTitulo.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL) ?? 0;
        }

        public void CheckCaixaDate()
        {
            CheckCaixa();

            var Caixa = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id).Where("criado", "<", Validation.ConvertDateToSql(DateTime.Now)).WhereFalse("excluir").FirstOrDefault();
            if (Caixa != null)
            {
                Home.idCaixa = Caixa.ID;

                string Msg = $"Antes de começar, há um caixa aberto do dia: {Validation.ConvertDateToForm(Caixa.CRIADO)}. {Environment.NewLine}Deseja realizar o FECHAMENTO agora?";
                string Title = "Atenção!";

                var result = AlertOptions.Message(Title, Msg, AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    DetailsCaixa.idCaixa = Home.idCaixa;
                    using (DetailsCaixa f = new DetailsCaixa())
                        f.ShowDialog();
                }
                else
                {
                    AlertOptions.Message("Atenção!", "Os recebimentos gerados a partir de vendas serão lançados no caixa aberto!", AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                }
            }

            var CaixaAberto = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id).WhereFalse("excluir").FirstOrDefault();
            if (CaixaAberto == null)
            {
                var result = AlertOptions.Message("Atenção!", $"Você não possui um Caixa aberto.{Environment.NewLine} Deseja abrir agora?", AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    using(AbrirCaixa f = new AbrirCaixa())
                        f.ShowDialog();
                }
            }
        }

        private static void CheckCaixa()
        {
            // Verifica se o caixa do usuário está aberto
            if (Home.idCaixa == 0)
            {
                var Caixa = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id).WhereFalse("excluir").FirstOrDefault();
                if (Caixa != null)
                    Home.idCaixa = Caixa.ID;
            }
        }
    }
}
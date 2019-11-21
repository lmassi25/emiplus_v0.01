using Emiplus.Data.Helpers;
using Emiplus.Properties;
using Emiplus.View.Common;
using Emiplus.View.Financeiro;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Controller
{
    class Caixa
    {
        private Model.CaixaMovimentacao _modelCaixaMov = new Model.CaixaMovimentacao();
        private Model.Pedido _modelPedido = new Model.Pedido();
        private Model.Titulo _modelTitulo = new Model.Titulo();

        public double SumSaidas(int idCaixa)
        {
            var sumSaidas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa).Where(q => q.Where("tipo", 1).OrWhere("tipo", 2)).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sumSaidas.TOTAL);
        }

        public double SumEntradas(int idCaixa)
        {
            var sumEntradas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa).Where("tipo", 3).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sumEntradas.TOTAL) + SumPagamento(idCaixa, 1);
        }

        public double SumSaldoFinal(int idCaixa)
        {
            return Validation.Round(SumEntradas(idCaixa) - SumSaidas(idCaixa));
        }

        public double SumVendasTotal(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL);
        }

        public double SumVendasAcrescimos(int idCaixa)
        {
            //var sum = _modelPedido.Query().SelectRaw("SUM() as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return 0;
        }

        public double SumVendasDescontos(int idCaixa)
        {
            //var sum = _modelPedido.Query().SelectRaw("SUM() as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return 0;
        }

        public int SumVendasGeradas(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("COUNT(id) as TOTAL").Where("id_caixa", idCaixa).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToInt32(sum.TOTAL);
        }

        public double SumVendasMedia(int idCaixa)
        {
            if (SumVendasTotal(idCaixa) == 0 || SumVendasTotal(idCaixa) == null)
                return 0;

            return Validation.Round(SumVendasTotal(idCaixa) / SumVendasGeradas(idCaixa));
        }

        public double SumVendasCanceladasTotal(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa).Where("excluir", 1).FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL);
        }

        public int SumVendasCanceladasGeradas(int idCaixa)
        {
            var sum = _modelPedido.Query().SelectRaw("COUNT(id) as TOTAL").Where("id_caixa", idCaixa).Where("excluir", 1).FirstOrDefault();
            return Validation.ConvertToInt32(sum.TOTAL);
        }

        public double SumPagamento(int idCaixa, int formaPgto)
        {
            var sum = _modelTitulo.Query().SelectRaw("SUM(TOTAL) as TOTAL").Where("id_caixa", idCaixa).Where("id_formapgto", formaPgto).WhereFalse("excluir").FirstOrDefault();
            return Validation.ConvertToDouble(sum.TOTAL);
        }

        public void CheckCaixaDate()
        {
            CheckCaixa();

            var Caixa = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id).Where("criado", "<", Validation.ConvertDateToSql(DateTime.Now)).FirstOrDefault();
            if (Caixa != null)
            {
                Home.idCaixa = Caixa.ID;

                string message = $"Antes de começar, há um caixa aberto do dia: {Validation.ConvertDateToForm(Caixa.CRIADO)}. {Environment.NewLine}Deseja realizar o FECHAMENTO agora?";
                string caption = "Atenção!";

                var result = AlertOptions.Message(caption, message, AlertBig.AlertType.warning, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    DetailsCaixa f = new DetailsCaixa();
                    f.ShowDialog();
                } 
                else
                {
                    AlertOptions.Message("Atenção!", "Os recebimentos gerados a partir de vendas serão lançados no caixa aberto!", AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                }
            }
        }

        public void CheckCaixa()
        {
            // Verifica se o caixa do usuário está aberto 
            if (Home.idCaixa == 0)
            {
                var Caixa = new Model.Caixa().Query().Where("tipo", "Aberto").Where("usuario", Settings.Default.user_id).FirstOrDefault();
                if (Caixa != null)
                {
                    Home.idCaixa = Caixa.ID;
                }
            }
        }
    }
}

using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Controller
{
    class Caixa
    {
        private Model.CaixaMovimentacao _modelCaixaMov = new Model.CaixaMovimentacao();

        public double SumDinheiro(int idCaixa)
        {
            var sumDinheiro = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa).Where("id_formapgto", 1).Where("tipo", 3).FirstOrDefault();
            return Validation.ConvertToDouble(sumDinheiro.TOTAL);
        }

        public double SumSaidas(int idCaixa)
        {
            var sumSaidas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa).Where(q => q.Where("tipo", 1).OrWhere("tipo", 2)).FirstOrDefault();
            return Validation.ConvertToDouble(sumSaidas.TOTAL);
        }

        public double SumEntradas(int idCaixa)
        {
            var sumEntradas = _modelCaixaMov.Query().SelectRaw("SUM(VALOR) as TOTAL").Where("id_caixa", idCaixa).Where("tipo", 3).FirstOrDefault();
            return Validation.ConvertToDouble(sumEntradas.TOTAL);
        }

        public double SumTotal(int idCaixa)
        {
            return SumSaidas(idCaixa) - SumEntradas(idCaixa);
        }
    }
}

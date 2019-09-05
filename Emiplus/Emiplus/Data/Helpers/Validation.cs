using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Data.Helpers
{
    public static class Validation
    {
        /// <summary>
        /// Converte valor double pra ordem de dinheiro REAL
        /// </summary>
        /// <param name="obj">1234.95</param>
        /// <returns>1.234,95</returns>
        public static string Price(double obj)
        {
           // return string.Format("{0:N2}", obj);

            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };
            double d = obj;
            string res = d.ToString("#,##.##", nfi);

            return res;
        }

        public static double ConvertToDouble(object obj)
        {
            if (obj == null)
                return 0;

            return Convert.ToDouble(obj);
        }
    }
}

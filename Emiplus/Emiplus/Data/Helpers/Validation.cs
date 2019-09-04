using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Data.Helpers
{
    public static class Validation
    {
        public static string Price(double obj)
        {
            return string.Format("{0:N2}", obj);
        }

        public static double ConvertToDouble(object obj)
        {
            if (obj == null)
                return 0;

            return Convert.ToDouble(obj);
        }
    }
}

using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Data.Core
{
    public class Controller
    {
        protected Log log;
        protected Alert alert;

        public Controller()
        {
            log = new Log();
            alert = new Alert();
        }
    }
}

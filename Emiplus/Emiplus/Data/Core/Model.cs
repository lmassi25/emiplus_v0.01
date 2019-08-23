using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using System.Data.Entity;

namespace Emiplus.Data.Core
{
    public class Model
    {
        protected Log log;
        protected ContextoData contexto;
        protected Alert alert;

        public Model()
        {
            log = new Log();
            contexto = new ContextoData();
            alert = new Alert();
        }
    }
}

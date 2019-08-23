using Emiplus.Data.Database;
using Emiplus.Data.GenericRepository;
using Emiplus.Data.Helpers;
using System.Data.Entity;

namespace Emiplus.Data.Core
{
    public class Model
    {
        protected Log log;        
        protected Alert alert;
        
        public Model()
        {
            log = new Log();
            alert = new Alert();
        }
    }
}

using Emiplus.Data.Helpers;

namespace Emiplus.Data.Core
{
    public class Controller
    {
        protected Log Log;
        protected Alert Alert;

        public Controller()
        {
            Log = new Log();
            Alert = new Alert();
        }
    }
}

using System.Net;

namespace Emiplus.Data.Core
{
    class Update
    {
        public static bool AtualizacaoDisponivel { get; set; }

        /// <summary>
        /// Pega versão recente do app na web
        /// </summary>
        public string GetVersionWebTxt()
        {
            WebClient client = new WebClient();
            string version = client.DownloadString("https://emiplus.com.br/version/version.txt");
            return version;
        }

        /// <summary>
        /// Verifica versão da web com a versão do app
        /// </summary>
        public void CheckUpdate()
        {
            if (GetVersionWebTxt() != Config.Get("APP_VERSION"))
            {
                AtualizacaoDisponivel = true;
                return;
            }

            AtualizacaoDisponivel = false;
        }
    }
}

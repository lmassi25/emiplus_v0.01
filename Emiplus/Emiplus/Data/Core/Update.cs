using System.Net;

namespace Emiplus.Data.Core
{
    internal class Update
    {
        public static bool AtualizacaoDisponivel { get; set; }

        /// <summary>
        /// Pega versão recente do app na web
        /// </summary>
        public string GetVersionWebTxt()
        {
            using (WebClient client = new WebClient())
            {
                string version = client.DownloadString(Program.URL_BASE + "/version/version.txt");
                return version;
            }
        }

        /// <summary>
        /// Verifica versão da web com a versão do app e atualiza o INI para disponibilizar a versão mais recente
        /// </summary>
        public void CheckUpdate()
        {
            if (GetVersionWebTxt() != IniFile.Read("Version", "APP"))
            {
                AtualizacaoDisponivel = true;
                IniFile.Write("Update", "true", "APP");
                return;
            }

            IniFile.Write("Update", "false", "APP");
            AtualizacaoDisponivel = false;
        }

        /// <summary>
        /// Verifica se existe as KEYs principais de configuração no arquivo INI, e adiciona caso não exista
        /// </summary>
        public void CheckIni()
        {
            if (!IniFile.KeyExists("none", "DEFAULT"))
                IniFile.Write("none", "none", "DEFAULT");

            if (!IniFile.KeyExists("Version", "APP"))
                IniFile.Write("Version", "1.0.0", "APP");

            if (!IniFile.KeyExists("URL_Ajuda", "APP"))
                IniFile.Write("URL_Ajuda", "http://ajuda.emiplus.com.br", "APP");

            if (!IniFile.KeyExists("URL_Base", "APP"))
                IniFile.Write("URL_Base", "http://www.emiplus.com.br", "APP");

            if (!IniFile.KeyExists("Update", "APP"))
                IniFile.Write("Update", "true", "APP");

            if (!IniFile.KeyExists("idEmpresa", "APP"))
                IniFile.Write("idEmpresa", "", "APP");

            if (!IniFile.KeyExists("Path", "LOCAL"))
                IniFile.Write("Path", @"C:\Emiplus", "LOCAL");

            if (!IniFile.KeyExists("PathDatabase", "LOCAL"))
                IniFile.Write("PathDatabase", @"C:\Emiplus", "LOCAL");

            if (!IniFile.KeyExists("dev", "DEV"))
                IniFile.Write("dev", "false", "DEV");

            if (!IniFile.KeyExists("encodeNS", "DEV"))
                IniFile.Write("encodeNS", "", "DEV");

            if (!IniFile.KeyExists("Printer", "SAT"))
                IniFile.Write("Printer", "", "SAT");

            if (!IniFile.KeyExists("N_Serie", "SAT"))
                IniFile.Write("N_Serie", "", "SAT");

            if (!IniFile.KeyExists("Assinatura", "SAT"))
                IniFile.Write("Assinatura", "", "SAT");

            if (!IniFile.KeyExists("Codigo_Ativacao", "SAT"))
                IniFile.Write("Codigo_Ativacao", "", "SAT");

            if (!IniFile.KeyExists("Servidor", "SAT"))
                IniFile.Write("Servidor", "", "SAT");

            if (!IniFile.KeyExists("Layout", "SAT"))
                IniFile.Write("Layout", "", "SAT");

            if (!IniFile.KeyExists("Maximizar", "TELAS"))
                IniFile.Write("Maximizar", "false", "TELAS");

            if (!IniFile.KeyExists("Maximizar", "TELAS"))
                IniFile.Write("Maximizar", "false", "TELAS");

            if (!IniFile.KeyExists("PrinterName", "Comercial"))
                IniFile.Write("PrinterName", "", "Comercial");

            if (!IniFile.KeyExists("Printer", "Comercial"))
                IniFile.Write("Printer", "false", "Comercial");

            if (!IniFile.KeyExists("RetomarVenda", "Comercial"))
                IniFile.Write("RetomarVenda", "True", "Comercial");

            if (!IniFile.KeyExists("LimiteDesconto", "Comercial"))
                IniFile.Write("LimiteDesconto", "0", "Comercial");

            if (!IniFile.KeyExists("ControlarEstoque", "Comercial"))
                IniFile.Write("ControlarEstoque", "False", "Comercial");

            if (!IniFile.KeyExists("UserNoDocument", "Comercial"))
                IniFile.Write("UserNoDocument", "False", "Comercial");

            if (!IniFile.KeyExists("ShowImagePDV", "Comercial"))
                IniFile.Write("ShowImagePDV", "False", "Comercial");

            if (!IniFile.KeyExists("MAIL_HOST", "EMAIL"))
                IniFile.Write("MAIL_HOST", "", "EMAIL");

            if (!IniFile.KeyExists("MAIL_MODE", "EMAIL"))
                IniFile.Write("MAIL_MODE", "tls", "EMAIL");

            if (!IniFile.KeyExists("MAIL_PASS", "EMAIL"))
                IniFile.Write("MAIL_PASS", "", "EMAIL");

            if (!IniFile.KeyExists("MAIL_PORT", "EMAIL"))
                IniFile.Write("MAIL_PORT", "587", "EMAIL");

            if (!IniFile.KeyExists("MAIL_SENDER", "EMAIL"))
                IniFile.Write("MAIL_SENDER", "", "EMAIL");

            if (!IniFile.KeyExists("MAIL_SMTP", "EMAIL"))
                IniFile.Write("MAIL_SMTP", "", "EMAIL");

            if (!IniFile.KeyExists("MAIL_USER", "EMAIL"))
                IniFile.Write("MAIL_USER", "", "EMAIL");

            if (!IniFile.KeyExists("MAIL_EMIPLUS", "EMAIL"))
                IniFile.Write("MAIL_EMIPLUS", "True", "EMAIL");

            if (!IniFile.KeyExists("Pimaco10Top", "ETIQUETAS"))
                IniFile.Write("Pimaco10Top", "15", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco10Right", "ETIQUETAS"))
                IniFile.Write("Pimaco10Right", "5", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco10Bottom", "ETIQUETAS"))
                IniFile.Write("Pimaco10Bottom", "10", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco10Left", "ETIQUETAS"))
                IniFile.Write("Pimaco10Left", "0", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco30Top", "ETIQUETAS"))
                IniFile.Write("Pimaco30Top", "13", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco30Right", "ETIQUETAS"))
                IniFile.Write("Pimaco30Right", "5", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco30Bottom", "ETIQUETAS"))
                IniFile.Write("Pimaco30Bottom", "10", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco30Left", "ETIQUETAS"))
                IniFile.Write("Pimaco30Left", "0", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco60Top", "ETIQUETAS"))
                IniFile.Write("Pimaco60Top", "14", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco60Right", "ETIQUETAS"))
                IniFile.Write("Pimaco60Right", "13.5", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco60Bottom", "ETIQUETAS"))
                IniFile.Write("Pimaco60Bottom", "13", "ETIQUETAS");

            if (!IniFile.KeyExists("Pimaco60Left", "ETIQUETAS"))
                IniFile.Write("Pimaco60Left", "13", "ETIQUETAS");

            if (!IniFile.KeyExists("GerarRecDiasAntecipado", "FINANCEIRO"))
                IniFile.Write("GerarRecDiasAntecipado", "7", "FINANCEIRO");
        }
    }
}
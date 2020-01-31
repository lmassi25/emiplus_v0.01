using Emiplus.Properties;
using Newtonsoft.Json;

namespace Emiplus.Data.Core
{
    class IBPT
    {
        private string URL = "https://apidoni.ibpt.org.br/api/v1/produtos";
        private string TOKEN = "XuQDaelCK27pcQRu0H8yvl4ZJj19W-xVsSSVG2I9KyC4ZuJ2D0rx7huWS7PeST01";
        private string CNPJ = "05681389000100";
        private string UF = Settings.Default.empresa_estado;

        private dynamic content { get; set; }

        private string Codigo { get; set; }
        private string Descricao { get; set; }
        private double Valor { get; set; }
        private string Medida { get; set; }
        private string CodeBarras { get; set; }

        public IBPT SetCodigoNCM(string value)
        {
            Codigo = value;
            return this;
        }

        public IBPT SetDescricao(string value)
        {
            Descricao = value;
            return this;
        }

        public IBPT SetValor(double value)
        {
            Valor = value;
            return this;
        }

        public IBPT SetMedida(string value)
        {
            Medida = value;
            return this;
        }

        public IBPT SetCodeBarras(string value)
        {
            CodeBarras = value;
            return this;
        }

        public dynamic GetDados()
        {
            content = new RequestApi().URL(URL + $"?token={TOKEN}&cnpj={CNPJ}&codigo={Codigo}&uf={UF}&ex=0&descricao={Descricao}&unidadeMedida={Medida}&valor={Valor}&gtin={CodeBarras}")
                .Content()
                .Response();

            return content;
        }
    }
}

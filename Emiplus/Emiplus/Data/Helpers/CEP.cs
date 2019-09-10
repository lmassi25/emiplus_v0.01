using System.Collections.Generic;

namespace Emiplus.Data.Helpers
{
    public static class CEP
    {
        public static List<string> GetEnderecoCompleto(string cep)
        {
            string aux = "";

            var ws = new WSCorreios.AtendeClienteClient();
            var resposta = ws.consultaCEP(cep);

            return new List<string> { resposta.end, resposta.complemento2, resposta.bairro, resposta.cidade, resposta.uf };
        }

        public static string GetIBGE(string cep)
        {
            string aux = "";



            return aux;
        }
    }
}

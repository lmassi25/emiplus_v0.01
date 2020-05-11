using System.Collections.Generic;
using System.Linq;
using System.Net;
using Emiplus.WSCorreios;
using Newtonsoft.Json.Linq;
using Exception = System.Exception;

namespace Emiplus.Data.Helpers
{
    public class CEP
    {
        public string cep { get; set; }

        public CEP SetCep(string c)
        {
            cep = c;
            return this;
        }

        public bool ValidationCep()
        {
            try
            {
                using (var ws = new AtendeClienteClient())
                {
                    var resposta = ws.consultaCEP(cep);

                    if (resposta == null)
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("CEP INVÁLIDO"))
                {
                    Alert.Message("Oppss!", "CEP não encontrado.", Alert.AlertType.warning);
                    return false;
                }

                if (ex.ToString().Contains("CEP NAO ENCONTRADO"))
                {
                    Alert.Message("Oppss!", "CEP não encontrado.", Alert.AlertType.warning);
                    return false;
                }
            }

            return false;
        }

        public dynamic GetRetornoCorreios()
        {
            using (var ws = new AtendeClienteClient())
            {
                var resposta = ws.consultaCEP(cep);

                return resposta;
            }
        }

        public string GetIBGE()
        {
            using (var json = new WebClient())
            {
                var download = json.DownloadString("https://www.emiplus.com.br/json/municipio");
                var googleSearch = JObject.Parse(download);

                IList<JToken> results = googleSearch["municipios"].Children().ToList();

                IList<MunicipioJson> searchResults = new List<MunicipioJson>();

                var resposta = GetRetornoCorreios();

                foreach (var result in results)
                {
                    var searchResult = result.ToObject<MunicipioJson>();
                    searchResults.Add(searchResult);

                    if (string.Compare(searchResult.nome, resposta.cidade, true) == 0)
                        return searchResult.id.ToString();
                }
            }

            return "0";
        }

        public class MunicipioJson
        {
            public int id { get; set; }
            public string ufid { get; set; }
            public string nome { get; set; }
        }
    }
}
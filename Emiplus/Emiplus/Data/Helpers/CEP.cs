using Emiplus.Model;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Emiplus.Data.Helpers
{
    public class CEP
    {
        public string cep { get; set; }

        public class MunicipioJson
        {
            public int id { get; set; }
            public string ufid { get; set; }
            public string nome { get; set; }
        }

        public CEP SetCep(string c)
        {
            cep = c;
            return this;
        }

        public bool ValidationCep()
        {
            try
            {
                using (var ws = new WSCorreios.AtendeClienteClient())
                {
                    var resposta = ws.consultaCEP(cep);
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex == null)
                {
                    return false;
                }

                if (ex.ToString() == "CEP INVÁLIDO")
                {
                    Alert.Message("Oppss!", "CEP não encontrado.", Alert.AlertType.warning);
                    return false;
                }

                if (ex.ToString() == "CEP NAO ENCONTRADO")
                {
                    Alert.Message("Oppss!", "CEP não encontrado.", Alert.AlertType.warning);
                    return false;
                }
            }

            return false;
        }

        public dynamic GetRetornoCorreios()
        {
            using (var ws = new WSCorreios.AtendeClienteClient())
            {
                var resposta = ws.consultaCEP(cep);

                return resposta;
            }
        }

        public string GetIBGE()
        {
            using (var json = new WebClient())
            {
                var download = json.DownloadString("https://www.emiplus.com.br/app/json/municipio");
                JObject googleSearch = JObject.Parse(download);
            
                IList<JToken> results = googleSearch["municipios"].Children().ToList();

                IList<MunicipioJson> searchResults = new List<MunicipioJson>();

                var resposta = GetRetornoCorreios();

                foreach (JToken result in results)
                {
                    MunicipioJson searchResult = result.ToObject<MunicipioJson>();
                    searchResults.Add(searchResult);

                    if (string.Compare(searchResult.nome, resposta.cidade, true) == 0)
                    {
                        return searchResult.id.ToString();
                    }
                }
            }

            return "0";
        }
    }
}

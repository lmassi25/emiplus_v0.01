using Newtonsoft.Json.Linq;
using RestSharp;

namespace Emiplus.Data.Core
{
    internal class RequestApi
    {
        private RestClient client;
        private IRestRequest request;

        public RequestApi Content(dynamic conte = null, Method method = Method.GET)
        {
            request = new RestRequest(method);

            if (conte != null)
                request.AddObject(conte);

            return this;
        }

        public RequestApi URL(string url)
        {
            client = new RestClient(url);

            return this;
        }

        public RequestApi AddHeader(string name, string value)
        {
            request.AddHeader(name, value);
            return this;
        }

        public RequestApi AddFile(string name, string path)
        {
            request.AddFile(name, path);
            request.AlwaysMultipartFormData = true;
            return this;
        }

        public JObject Response()
        {
            IRestResponse response = client.Execute(request);
            return JObject.Parse(response.Content);
        }
    }
}
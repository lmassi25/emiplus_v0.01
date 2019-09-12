using Emiplus.Data.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace Emiplus.View.Testes
{
    public partial class Form1 : Form
    {
        private int Backspace = 0;

        public class SponsorInfo
        {
            public int id { get; set; }
            public string ufid { get; set; }
            public string nome { get; set; }
        }

        public Form1()
        {
            InitializeComponent();

            double valor = 178.00;
            double percentual = 15.0;
            double qtd = 3;
            double valor_final = (percentual / 100.0 * (valor * qtd));

            //Console.WriteLine(valor_final);

            pessoaJF.DataSource = new List<String> { "Física", "Jurídica" };
            pessoaJF.SelectedItem = "Física";

            var json = new WebClient().DownloadString("https://www.emiplus.com.br/app/json/municipio");
            JObject googleSearch = JObject.Parse(json);

            IList<JToken> results = googleSearch["municipios"].Children().ToList();

            IList<SponsorInfo> searchResults = new List<SponsorInfo>();
            foreach (JToken result in results)
            {
                SponsorInfo searchResult = result.ToObject<SponsorInfo>();
                searchResults.Add(searchResult);

                Console.WriteLine(searchResult.nome);
            }

            SponsorInfo teste = new SponsorInfo();

            Console.WriteLine(teste.nome);
        }


        public double inteiro(double valor)
        {
            double id = valor == null ? 1 : valor;
            return id;
        }

        private void CpfCnpj_TextChanged(object sender, EventArgs e)
        {
            ChangeMask();
        }

        private void CpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                Backspace = 1;
            }
            else
            {
                Backspace = 0;
            }
        }

        private void ChangeMask()
        {
            if (cpfCnpj.Text != "")
            {
                if (Backspace == 0)
                {
                    //cpfCnpj.Text = Validation.ChangeMaskCPFCNPJ(cpfCnpj.Text, pessoaJF.Text);
                    cpfCnpj.Select(cpfCnpj.Text.Length, 0);
                }
            }
        }

        private class Municipio
        {
            public string id { get; set; }
            public string ufid { get; set; }
            public string nome { get; set; }

            public Municipio (string id, string ufid, string nome)
            {
                this.id = id;
                this.ufid = ufid;
                this.nome = nome;
            }
        }

        public static string GetJSONString(string url)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(
                    stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static T GetObjectFromJSONString<T>(
            string json) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(
                Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

        public static T[] GetArrayFromJSONString<T>(
            string json) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(
                Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(T[]));
                return (T[])serializer.ReadObject(stream);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //WebClient client = new WebClient();
            //Stream stream = client.OpenRead("https://www.emiplus.com.br/app/json/municipio");
            //StreamReader reader = new StreamReader(stream);

            //Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());

            //// Instead of WriteLine, 2 or 3 lines of code here using WebClient to download the file
            //Console.WriteLine((string)jObject["nome"]);

            //stream.Close();

            //Console.WriteLine(GetJSONString("https://www.emiplus.com.br/app/json/municipio"));

            //var obj = GetObjectFromJSONString<Municipio>(GetJSONString("https://www.emiplus.com.br/app/json/municipio"));

            //Municipio m = JsonConvert.DeserializeObject<Municipio>();

            //Console.WriteLine(m.nome);
        }
    }
}

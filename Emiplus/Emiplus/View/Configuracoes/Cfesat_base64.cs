using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Configuracoes
{
    public partial class Cfesat_base64 : Form
    {
        private readonly OpenFileDialog ofd = new OpenFileDialog();

        public Cfesat_base64()
        {
            InitializeComponent();
            Eventos();

            logs.Select();
        }

        /// <summary>
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            logs.Click += (s, e) =>
            {
                string _path_autorizada = @"C:\Emiplus\CFe\Autorizadas";

                ofd.RestoreDirectory = true;
                ofd.DefaultExt = "txt";
                ofd.Filter = @"TXT|*.txt";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                if (ofd.ShowDialog() == DialogResult.OK) caminho.Text = ofd.FileName;

                if (!File.Exists(caminho.Text))
                {
                    Alert.Message("Ação não permitida", "Arquivo não encontrado!", Alert.AlertType.warning);
                    return;
                }

                var conteudo = File.ReadAllText(caminho.Text);

                if (string.IsNullOrEmpty(conteudo))
                {
                    Alert.Message("Ação não permitida", "Conteúdo inválido!", Alert.AlertType.warning);
                    return;
                }

                if (!Directory.Exists(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00")))
                    Directory.CreateDirectory(_path_autorizada + "\\" + DateTime.Now.Year +
                                              DateTime.Now.Month.ToString("00"));

                var oXml = new XmlDocument();
                oXml.LoadXml(Base64ToString(Sep_Delimitador('|', 6, conteudo)));

                var chaveDeAcesso = oXml.SelectSingleNode(@"/CFe/infCFe")?.Attributes?.GetNamedItem("Id").Value;
                //var nr_Nota = oXML.SelectSingleNode("/CFe/infCFe/ide").ChildNodes[4].InnerText;
                //var assinatura_qrcode = oXML.SelectSingleNode("/CFe/infCFe/ide").ChildNodes[11].InnerText;

                var doc = XDocument.Parse(Base64ToString(Sep_Delimitador('|', 6, conteudo)));
                doc.Save(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + "\\" +
                         chaveDeAcesso + ".xml");

                Alert.Message("Sucesso", "Arquivo criado com sucesso!", Alert.AlertType.success);

                novoArquivo.Text = _path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") +
                                   "\\" + chaveDeAcesso + ".xml";
            };
        }

        public static string Base64ToString(string base64) // caso queira tirar o arquivo de base 64
        {
            //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            var enc = new UTF8Encoding();
            var arq = Convert.FromBase64String(base64);
            base64 = enc.GetString(arq);
            return base64;
        }

        private static string Sep_Delimitador(char sep, int posicao, string dados)
        {
            var retDados = dados.Split(sep);
            return retDados[posicao];
        }
    }
}
using Emiplus.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Emiplus.View.Configuracoes
{
    public partial class Cfesat_base64 : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();

        public Cfesat_base64()
        {
            InitializeComponent();
            Eventos();

            logs.Select();
        }
        
        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            logs.Click += (s, e) =>
            {
                string _path_autorizada = @"C:\Emiplus\CFe\Autorizadas", conteudo = "", ChaveDeAcesso = "", nr_Nota = "", assinatura_qrcode = "";

                ofd.RestoreDirectory = true;
                ofd.DefaultExt = "txt";
                ofd.Filter = "TXT|*.txt";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                //ofd.Multiselect = MultipleImports;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    caminho.Text = ofd.FileName;
                }

                //caminho.Text = @"D:\_CLIENTES\MIKE CENTER\3.txt";
                
                if (!File.Exists(caminho.Text))
                {
                    Alert.Message("Ação não permitida", "Arquivo não encontrado!", Alert.AlertType.warning);
                    return;
                }

                conteudo = File.ReadAllText(caminho.Text);

                if (String.IsNullOrEmpty(conteudo))
                {
                    Alert.Message("Ação não permitida", "Conteúdo inválido!", Alert.AlertType.warning);
                    return;
                }

                if (!Directory.Exists(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00")))
                    Directory.CreateDirectory(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00"));

                XmlDocument oXML = new XmlDocument();
                oXML.LoadXml(Base64ToString(Sep_Delimitador('|', 6, conteudo)));

                ChaveDeAcesso = oXML.SelectSingleNode("/CFe/infCFe").Attributes.GetNamedItem("Id").Value;
                nr_Nota = oXML.SelectSingleNode("/CFe/infCFe/ide").ChildNodes[4].InnerText;
                assinatura_qrcode = oXML.SelectSingleNode("/CFe/infCFe/ide").ChildNodes[11].InnerText;

                var doc = XDocument.Parse(Base64ToString(Sep_Delimitador('|', 6, conteudo)));
                doc.Save(_path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + "\\" + ChaveDeAcesso + ".xml");

                Alert.Message("Sucesso", "Arquivo criado com sucesso!", Alert.AlertType.success);

                novoArquivo.Text = _path_autorizada + "\\" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + "\\" + ChaveDeAcesso + ".xml";
            };
        }

        public static string Base64ToString(string base64)  // caso queira tirar o arquivo de base 64
        {
            byte[] arq;
            //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            arq = Convert.FromBase64String(base64);
            base64 = enc.GetString(arq);
            return base64;
        }

        private static string Sep_Delimitador(char sep, int posicao, string dados)
        {
            string[] ret_dados = dados.Split(sep);
            return ret_dados[posicao];
        }
    }
}

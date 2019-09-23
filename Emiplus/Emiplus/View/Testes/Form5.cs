using Emiplus.Controller;
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Emiplus.View.Testes
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //criarXMLTeste();

            var f = new Fiscal();
            f.CreateXml(200, "NFe");
        }

        private void criarXMLTeste()
        {
            string strFilePath = "C:\\emiplus_v0.01\\teste.xml";

            XmlTextWriter xtw = new XmlTextWriter(strFilePath, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;

            //Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>
            xtw.WriteStartDocument();

                xtw.WriteStartElement("blog");

                    xtw.WriteStartElement("artigos");

                        xtw.WriteAttributeString("linguagem", "asp.net");

                        xtw.WriteStartElement("artigo");
                            xtw.WriteElementString("titulo", "DataSet para XML em ASP.NET / C#");
                            xtw.WriteElementString("url", "http://cbsa.com.br/post/dataset-para-xml-em-aspnet-c.aspx");

                            xtw.WriteStartElement("artigo");
                            xtw.WriteElementString("titulo", "DataSet para XML em ASP.NET / C#");
                            xtw.WriteElementString("url", "http://cbsa.com.br/post/dataset-para-xml-em-aspnet-c.aspx");

                                xtw.WriteStartElement("artigo");
                                xtw.WriteElementString("titulo", "DataSet para XML em ASP.NET / C#");
                                xtw.WriteElementString("url", "http://cbsa.com.br/post/dataset-para-xml-em-aspnet-c.aspx");
                                xtw.WriteEndElement();
                                xtw.WriteEndElement();
                        xtw.WriteEndElement();

                        xtw.WriteStartElement("artigo");
                            xtw.WriteElementString("titulo", "XML para DataSet em ASP.NET / C#");
                            xtw.WriteElementString("url", "http://cbsa.com.br/post/xml-para-dataset-em-aspnet-c.aspx");
                        xtw.WriteEndElement();

                        xtw.WriteStartElement("artigo");
                            xtw.WriteElementString("titulo", "Ler arquivo XML usando XmlTextReader e XmlDocument em C# - ASP.NET");
                            xtw.WriteElementString("url", "http://cbsa.com.br/post/ler-arquivo-xml-usando-xmltextreader-e-xmldocument-em-c---aspnet.aspx");
                        xtw.WriteEndElement();

                    xtw.WriteEndElement();

                    xtw.WriteStartElement("artigos");
                        xtw.WriteAttributeString("linguagem", "C#");
                        xtw.WriteStartElement("artigo");
                        xtw.WriteElementString("titulo", "Calcular idade em C#");
                        xtw.WriteElementString("url", "http://cbsa.com.br/post/calcular-idade-em-c.aspx");
                        xtw.WriteEndElement();

                    xtw.WriteEndElement();

                xtw.WriteEndElement();

            xtw.WriteEndDocument();

            xtw.Flush();
            xtw.Close();
        }
    }
}

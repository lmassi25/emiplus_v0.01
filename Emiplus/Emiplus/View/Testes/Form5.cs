using Emiplus.Controller;
using Emiplus.Data.Database;
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Emiplus.Data.Helpers;
using System.IO;
using Emiplus.Data.Core;
using System.Diagnostics;

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

        private void Button2_Click(object sender, EventArgs e)
        {
            var create = new CreateTables();
            create.CheckTables();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var s = IniFile.Read("Path", "LOCAL");
            Console.WriteLine(s);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            //IniFile.Write("DefaultVolume", "100");

            //var DefaultVolume = IniFile.Read("DefaultVolume");
            //var teste = IniFile.Read("Teste", "Emiplus");
            //Console.WriteLine(DefaultVolume);
            //Console.WriteLine(teste);

            //IniFile.Write("HomePage", "http://www.google.com", "Web");
            //IniFile.Write("DefaultVolume", "100", "Audio");

            //if(IniFile.KeyExists("DefaultVolume", "Audio1"))
            //{
            //    IniFile.Write("DefaultVolume", "200", "Audio1");
            //}

            //IniFile.DeleteKey("DefaultVolume", "Audio1");

            //IniFile.DeleteSection("Emiplus");

            //var parser = new FileIniDataParser();
            //IniData data = parser.ReadFile(@"C:\Emiplus\Emiplus\Emiplus\Data\Core\Config.ini");

            //data["UI"]["fullscreen"] = "true";
            //data["U2"]["fullscreen"] = "false";
            //parser.WriteFile(@"C:\Emiplus\Emiplus\Emiplus\Data\Core\Config.ini", data);

            //Console.WriteLine(Support.GetIni()["LOCAL"]["path"]);
        }
    }
}

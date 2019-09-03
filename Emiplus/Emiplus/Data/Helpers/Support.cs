using System;
using System.Drawing.Text;
using System.IO;

namespace Emiplus.Data.Helpers
{
    public class Support
    {
        public static string BasePath()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            return projectDirectory;
        }

        public void AddFont()
        {
            DirectoryInfo diretorio = new DirectoryInfo(BasePath() + "/Assets/Fonts");
            FileInfo[] Arquivos = diretorio.GetFiles("*.ttf*");
            foreach (FileInfo fileinfo in Arquivos)
            {
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(fileinfo.Directory + @"\" + fileinfo.Name);
            }
        }

        /// <summary>
        /// Abre página no navegador padrão definido no windows
        /// </summary>
        /// <param name="link">https://www.google.com/</param>
        public static void OpenLinkBrowser(string link)
        {
            System.Diagnostics.Process.Start(link);
        }
    }
}
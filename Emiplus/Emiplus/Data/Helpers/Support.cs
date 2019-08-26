using System.Drawing.Text;
using System.IO;

namespace Emiplus.Data.Helpers
{
    public class Support
    {
        public void AddFont()
        {
            DirectoryInfo diretorio = new DirectoryInfo(@"C:\emiplus_v0.01\Emiplus\Emiplus\Assets\Fonts");
            FileInfo[] Arquivos = diretorio.GetFiles("*.ttf*");
            foreach (FileInfo fileinfo in Arquivos)
            {
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(fileinfo.Directory + @"\" + fileinfo.Name);
            }
        }
    }
}
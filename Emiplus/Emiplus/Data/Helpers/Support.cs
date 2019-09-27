using IniParser;
using IniParser.Model;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    public class Support
    {
        /// <summary>
        /// Recupera caminho base do projeto
        /// Cliente: vai recuperar o caminho do executavel
        /// Dev: vai recuperar o caminho do projeto
        /// </summary>
        public static string BasePath()
        {
            //if (File.Exists("C:\\emiplus_v0.01\\EMIPLUS.FDB"))
            //{
            //    return Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            //}

            //if (File.Exists(Directory.GetCurrentDirectory() + "\\EMIPLUS.FDB"))
            //{
            //    return Directory.GetCurrentDirectory();
            //}

            //string projectDirectory = Directory.GetCurrentDirectory();
            //return projectDirectory;

            return Support.GetIni()["LOCAL"]["path"];
        }

        public static IniData GetIni()
        {
            IniData data = new FileIniDataParser().ReadFile(GetLocalIni());
            return data;
        }

        private static string GetLocalIni()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Config.ini"))
            {
                return Directory.GetCurrentDirectory() + "\\Config.ini";
            }

            if (File.Exists("C:\\emiplus_v0.01\\Emiplus\\Emiplus\\Data\\Core\\Config.ini"))
            {
                return "C:\\emiplus_v0.01\\Emiplus\\Emiplus\\Data\\Core\\Config.ini";
            }

            return Directory.GetCurrentDirectory() + "\\Config.ini";
        }

        /// <summary>
        /// Abre página no navegador padrão definido no windows
        /// </summary>
        /// <param name="link">https://www.google.com/</param>
        public static void OpenLinkBrowser(string link)
        {
            System.Diagnostics.Process.Start(link);
        }

        public static void UpDownDataGrid(bool abaixo, DataGridView data)
        {
            if (data.CurrentRow == null)
            {
                return;
            }

            if (abaixo && data.CurrentRow.Index != data.Rows.Count - 1) //Verifica se é a prim
            {
                data.CurrentCell = data[data.CurrentCell.ColumnIndex, data.CurrentCell.RowIndex + 1];
            }
            else if (data.CurrentRow.Index != 0)
            {
                data.CurrentCell = data[data.CurrentCell.ColumnIndex, data.CurrentCell.RowIndex - 1];
            }
        }
    }
}
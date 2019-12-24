using Emiplus.Data.Core;
using Emiplus.View.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    public class Support
    {
     
        public static List<String> GetUnidades()
        {
            return new List<String> { "UN", "KG", "PC", "MÇ", "BD", "DZ", "GR", "L", "ML", "M", "M2", "ROLO", "CJ", "SC", "CX", "FD", "PAR", "PR", "KIT", "CNT", "PCT" };
        }

        public static void Video(string videoUrl)
        {
            VideoTutorial f = new VideoTutorial(videoUrl);
            f.Show();
        }

        public static string BasePath()
        {
            return IniFile.Read("Path", "LOCAL");
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
using Emiplus.Data.Core;
using Emiplus.View.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    public class Support
    {
     
        public static ArrayList GetOrigens()
        {
            var origens = new ArrayList();
            origens.Add(new { Id = "0", Nome = "0 - Nacional, exceto as indicadas nos códigos 3, 4, 5 e 8" });
            origens.Add(new { Id = "1", Nome = "1 - Estrangeira - Importação direta, exceto a indicada no código 6" });
            origens.Add(new { Id = "2", Nome = "2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7" });
            origens.Add(new { Id = "3", Nome = "3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40 % e inferior ou igual a 70 %" });
            origens.Add(new { Id = "4", Nome = "4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam as legislações citadas nos Ajustes" });
            origens.Add(new { Id = "5", Nome = "5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40 %" });
            origens.Add(new { Id = "6", Nome = "6 - Estrangeira - Importação direta, sem similar nacional, constante em lista da CAMEX e gás natural" });
            origens.Add(new { Id = "7", Nome = "7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante lista CAMEX e gás natural." });
            origens.Add(new { Id = "8", Nome = "8 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 70 %" });

            return origens;
        }

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
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using SqlKata.Execution;
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

namespace Emiplus.View.Configuracoes
{
    public partial class Balanças : Form
    {
        public Balanças()
        {
            InitializeComponent();
            Eventos();
        }

        /// <summary>
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            txtitens.Click += (s, e) =>
            {
                createTxtItem();
            };

            btnExit.Click += (s, e) => Close();
        }

        private void createTxtItem()
        {            
            string path = IniFile.Read("PathBalança", "Comercial");
            if (String.IsNullOrEmpty(path))
            {
                path = "C:\\Emiplus\\";
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            StreamWriter txt = new StreamWriter(path + @"\TXITENS.TXT", false, Encoding.UTF8);

            var data = GetDataTable();
            foreach (var item in data)
            {
                string DD = "", EE = "", T = "", CCCCCC = "", PPPPPP = "", VVV = "", D1 = "", D2 = "";
                int id = 0; string vlrvenda = "", auxVlrVenda = "", nome = "", unidade = "";
                
                if (!String.IsNullOrEmpty(item.NOME))
                {
                    id = item.ID;
                    vlrvenda = item.VALORVENDA.ToString();
                    unidade = item.MEDIDA;
                    if (vlrvenda.IndexOf(",") > 0)
                    {
                        auxVlrVenda = vlrvenda.Substring(vlrvenda.IndexOf(","), vlrvenda.Length - vlrvenda.IndexOf(","));
                        auxVlrVenda = auxVlrVenda.Replace(",", "");
                        if (auxVlrVenda.Length == 1)
                        {
                            vlrvenda = vlrvenda + "0";
                        }
                    }
                    else
                    {
                        if (vlrvenda.Length == 3)
                        {
                            if (item.VALORVENDA > 99)
                            {
                                vlrvenda = vlrvenda + "00";
                            }
                        }
                        if (vlrvenda.Length == 2)
                        {
                            if (item.VALORVENDA > 9)
                            {
                                vlrvenda = vlrvenda + "00";
                            }
                        }
                        if (vlrvenda.Length == 1)
                        {
                            if (item.VALORVENDA <= 9)
                            {
                                vlrvenda = vlrvenda + "00";
                            }
                        }
                    }

                    vlrvenda = vlrvenda.Replace(".", "");
                    vlrvenda = vlrvenda.Replace(",", "");
                    nome = item.NOME;

                    if (nome.Length > 24)
                    {
                        nome = nome.Substring(0, 24);
                    }

                    DD = "99";
                    EE = "00";

                    if (unidade == "KG")
                    {
                        T = "0";
                    }
                    else
                    {
                        T = "1";
                    }

                    CCCCCC = id.ToString("000000"); //id
                                                    //PPPPPP = dataTools.convertParaInt(vlrvenda).ToString("000000"); //vlrvenda
                    PPPPPP = vlrvenda.PadLeft(6, '0'); //vlrvenda
                    VVV = "000";
                    D1 = nome.PadLeft(25, ' '); //nome
                    D2 = nome.PadLeft(25, ' '); //nome

                    txt.WriteLine(DD + EE + T + CCCCCC + PPPPPP + VVV + D1 + D2);
                }
            }

            txt.Close();

            Alert.Message("Sucesso", "TXTItens gerado com sucesso", Alert.AlertType.success);
        }

        public IEnumerable<dynamic> GetDataTable()
        {
            return new Model.Item().Query()
                .Where("item.excluir", 0)                
                .Where("item.tipo", "Produtos")
                .Get<dynamic>();
        }
    }
}

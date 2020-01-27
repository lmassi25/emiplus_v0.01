using Emiplus.Data.Core;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Comercial : Form
    {
        public Comercial()
        {
            InitializeComponent();
            Eventos();
        }

        public void Eventos()
        {
            Shown += (s, e) =>
            {
                if (!String.IsNullOrEmpty(IniFile.Read("RetomarVenda", "Comercial")))
                {
                    if (IniFile.Read("RetomarVenda", "Comercial") == "True")
                        retomarVendaInicio.Toggled = true;
                    else
                        retomarVendaInicio.Toggled = false;
                }
            };

            retomarVendaInicio.Click += (s, e) =>
            {
                if (retomarVendaInicio.Toggled)
                    IniFile.Write("RetomarVenda", "False", "Comercial");
                else
                    IniFile.Write("RetomarVenda", "True", "Comercial");
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
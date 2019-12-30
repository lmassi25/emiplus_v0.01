using Emiplus.Data.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Comercial : Form
    {
        public Comercial()
        {
            InitializeComponent();
            Start();
            Eventos();
        }

        public void Start()
        {
            tipoimpressora.Items.Add("Folha A4");
            tipoimpressora.Items.Add("Bobina 80mm");

            if (!String.IsNullOrEmpty(IniFile.Read("Printer", "Comercial")))
                tipoimpressora.SelectedItem = IniFile.Read("Printer", "Comercial");

            if (!String.IsNullOrEmpty(IniFile.Read("PrinterName", "Comercial")))
                impressora.Text = IniFile.Read("PrinterName", "Comercial");
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            tipoimpressora.Leave += (s, e) =>
            {
                IniFile.Write("Printer", tipoimpressora.Text, "Comercial");
            };
            
            impressora.Leave += (s, e) =>
            {
                IniFile.Write("PrinterName", impressora.Text, "Comercial");
            };

        }
    }
}

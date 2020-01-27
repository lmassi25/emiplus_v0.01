using Emiplus.Data.Core;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Impressao : Form
    {
        public Impressao()
        {
            InitializeComponent();
            Start();
            Eventos();
        }

        private void Start()
        {
            tipoimpressora.Items.Add("Folha A4");
            tipoimpressora.Items.Add("Bobina 80mm");

            if (!String.IsNullOrEmpty(IniFile.Read("Printer", "Comercial")))
                tipoimpressora.SelectedItem = IniFile.Read("Printer", "Comercial");

            if (!String.IsNullOrEmpty(IniFile.Read("PrinterName", "Comercial")))
                impressora.Text = IniFile.Read("PrinterName", "Comercial");
        }

        private void Eventos()
        {
            tipoimpressora.Leave += (s, e) => IniFile.Write("Printer", tipoimpressora.Text, "Comercial");
            impressora.Leave += (s, e) => IniFile.Write("PrinterName", impressora.Text, "Comercial");

            btnExit.Click += (s, e) => Close();
        }
    }
}
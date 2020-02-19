using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Impressao : Form
    {
        public Impressao()
        {
            InitializeComponent();

            ToolHelp.Show("", pictureBox8, ToolHelp.ToolTipIcon.Info, "Ajuda!");

            Start();
            Eventos();
        }

        private void Start()
        {
            tipoimpressora.Items.Add("Folha A4");
            tipoimpressora.Items.Add("Bobina 80mm");

            impressora.DataSource = Support.GetImpressoras();
             
            if (!String.IsNullOrEmpty(IniFile.Read("Printer", "Comercial")))
                tipoimpressora.SelectedItem = IniFile.Read("Printer", "Comercial");

            if (!String.IsNullOrEmpty(IniFile.Read("PrinterName", "Comercial")))
                impressora.SelectedItem = IniFile.Read("PrinterName", "Comercial");

            // Pimaco 10
            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco10Top", "ETIQUETAS")))
                pi10Top.Text = IniFile.Read("Pimaco10Top", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco10Right", "ETIQUETAS")))
                pi10Right.Text = IniFile.Read("Pimaco10Right", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco10Bottom", "ETIQUETAS")))
                pi10Bottom.Text = IniFile.Read("Pimaco10Bottom", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco10Left", "ETIQUETAS")))
                pi10Left.Text = IniFile.Read("Pimaco10Left", "ETIQUETAS");

            // Pimaco 30
            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco30Top", "ETIQUETAS")))
                pi30Top.Text = IniFile.Read("Pimaco30Top", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco30Right", "ETIQUETAS")))
                pi30Right.Text = IniFile.Read("Pimaco30Right", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco30Bottom", "ETIQUETAS")))
                pi30Bottom.Text = IniFile.Read("Pimaco30Bottom", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco30Left", "ETIQUETAS")))
                pi30Left.Text = IniFile.Read("Pimaco30Left", "ETIQUETAS");

            // Pimaco 60
            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco60Top", "ETIQUETAS")))
                pi60Top.Text = IniFile.Read("Pimaco60Top", "ETIQUETAS");
            
            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco60Right", "ETIQUETAS")))
                pi60Right.Text = IniFile.Read("Pimaco60Right", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco60Bottom", "ETIQUETAS")))
                pi60Bottom.Text = IniFile.Read("Pimaco60Bottom", "ETIQUETAS");

            if (!String.IsNullOrEmpty(IniFile.Read("Pimaco60Left", "ETIQUETAS")))
                pi60Left.Text = IniFile.Read("Pimaco60Left", "ETIQUETAS");
        }

        private void Eventos()
        {
            tipoimpressora.Leave += (s, e) => IniFile.Write("Printer", tipoimpressora.Text, "Comercial");
            impressora.SelectedIndexChanged += (s, e) => IniFile.Write("PrinterName", impressora.SelectedItem.ToString(), "Comercial");

            pi10Top.Leave += (s, e) => IniFile.Write("Pimaco10Top", pi10Top.Text, "ETIQUETAS");
            pi10Right.Leave += (s, e) => IniFile.Write("Pimaco10Right", pi10Right.Text, "ETIQUETAS");
            pi10Bottom.Leave += (s, e) => IniFile.Write("Pimaco10Bottom", pi10Bottom.Text, "ETIQUETAS");
            pi10Left.Leave += (s, e) => IniFile.Write("Pimaco10Left", pi10Left.Text, "ETIQUETAS");

            pi30Top.Leave += (s, e) => IniFile.Write("Pimaco30Top", pi30Top.Text, "ETIQUETAS");
            pi30Right.Leave += (s, e) => IniFile.Write("Pimaco30Right", pi30Right.Text, "ETIQUETAS");
            pi30Bottom.Leave += (s, e) => IniFile.Write("Pimaco30Bottom", pi30Bottom.Text, "ETIQUETAS");
            pi30Left.Leave += (s, e) => IniFile.Write("Pimaco30Left", pi30Left.Text, "ETIQUETAS");

            pi60Top.Leave += (s, e) => IniFile.Write("Pimaco60Top", pi60Top.Text, "ETIQUETAS");
            pi60Right.Leave += (s, e) => IniFile.Write("Pimaco60Right", pi60Right.Text, "ETIQUETAS");
            pi60Bottom.Leave += (s, e) => IniFile.Write("Pimaco60Bottom", pi60Bottom.Text, "ETIQUETAS");
            pi60Left.Leave += (s, e) => IniFile.Write("Pimaco60Left", pi60Left.Text, "ETIQUETAS");

            btnRefreshImpressoras.Click += (s, e) =>
            {
                impressora.DataSource = Support.GetImpressoras();
                impressora.Refresh();
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
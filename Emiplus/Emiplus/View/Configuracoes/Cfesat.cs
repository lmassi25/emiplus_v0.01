using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Cfesat : Form
    {
        public Cfesat()
        {
            InitializeComponent();

            Start();
            Eventos();
        }

        public void Start()
        {
            servidor.Items.Add("Homologacao");
            servidor.Items.Add("Producao");

            if (!String.IsNullOrEmpty(IniFile.Read("Servidor", "SAT")))
                servidor.SelectedItem = IniFile.Read("Servidor", "SAT");

            if (!String.IsNullOrEmpty(IniFile.Read("Printer", "SAT")))
                impressora.Text = IniFile.Read("Printer", "SAT");

            if (!String.IsNullOrEmpty(IniFile.Read("N_Serie", "SAT")))
                serie.Text = IniFile.Read("N_Serie", "SAT");
        }

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            logs.Click += (s, e) =>
            {
                Cfesat_logs f = new Cfesat_logs();
                Cfesat_logs.tipo = 0;
                f.ShowDialog();
            };

            consultarsat.Click += (s, e) =>
            {
                AlertOptions.Message("Retorno", new Controller.Fiscal().Consulta(), Common.AlertBig.AlertType.info, Common.AlertBig.AlertBtn.OK);
            };

            consultarstatus.Click += (s, e) =>
            {
                Cfesat_logs f = new Cfesat_logs();
                Cfesat_logs.tipo = 1;
                f.ShowDialog();
            };

            servidor.Leave += (s, e) =>
            {
                IniFile.Write("Servidor", servidor.Text, "SAT");
            };

            impressora.Leave += (s, e) =>
            {
                IniFile.Write("Printer", impressora.Text, "SAT");
            };

            serie.Leave += (s, e) =>
            {
                IniFile.Write("N_Serie", serie.Text, "SAT");
            };
        }
    }
}

using System.IO;
using System.Windows.Forms;
using Emiplus.Controller;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

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
            impressora.DataSource = Support.GetImpressoras();

            if (!string.IsNullOrEmpty(IniFile.Read("Servidor", "SAT")))
                servidor.SelectedItem = IniFile.Read("Servidor", "SAT");

            if (!string.IsNullOrEmpty(IniFile.Read("Printer", "SAT")))
                impressora.SelectedItem = IniFile.Read("Printer", "SAT");

            if (!string.IsNullOrEmpty(IniFile.Read("N_Serie", "SAT")))
                serie.Text = IniFile.Read("N_Serie", "SAT");
        }

        /// <summary>
        ///     Eventos do form
        /// </summary>
        public void Eventos()
        {
            logs.Click += (s, e) =>
            {
                var f = new Cfesat_logs();
                Cfesat_logs.tipo = 0;
                f.ShowDialog();
            };

            consultarsat.Click += (s, e) =>
            {
                if (!File.Exists("Sat.Dll"))
                {
                    Alert.Message("Opps", "Não encontramos a DLL do SAT", Alert.AlertType.warning);
                    return;
                }

                AlertOptions.Message("Retorno", new Controller.Fiscal().Consulta(), AlertBig.AlertType.info,
                    AlertBig.AlertBtn.OK);
            };

            consultarstatus.Click += (s, e) =>
            {
                var f = new Cfesat_logs();
                Cfesat_logs.tipo = 1;
                f.ShowDialog();
            };

            base64.Click += (s, e) =>
            {
                var f = new Cfesat_base64();
                f.ShowDialog();
            };

            servidor.Leave += (s, e) => IniFile.Write("Servidor", servidor.Text, "SAT");
            impressora.SelectedIndexChanged +=
                (s, e) => IniFile.Write("Printer", impressora.SelectedItem.ToString(), "SAT");
            serie.Leave += (s, e) => IniFile.Write("N_Serie", serie.Text, "SAT");

            btnExit.Click += (s, e) => Close();
        }
    }
}
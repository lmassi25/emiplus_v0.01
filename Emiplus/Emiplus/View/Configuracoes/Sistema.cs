using Emiplus.Data.Core;
using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using SqlKata.Execution;
using System;
using System.IO;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Sistema : Form
    {
        public Sistema()
        {
            InitializeComponent();
            Eventos();
        }

        private void Eventos()
        {
            Shown += (s, e) =>
            {
                if (File.Exists(Program.PATH_BASE + "\\logs\\EXCEPTIONS.txt"))
                    erros.Text = File.ReadAllText(Program.PATH_BASE + "\\logs\\EXCEPTIONS.txt");

                if (!String.IsNullOrEmpty(IniFile.Read("Remoto", "LOCAL")))
                    ip.Text = IniFile.Read("Remoto", "LOCAL");
            };

            AtualizaDb.Click += (s, e) =>
            {
                new CreateTables().CheckTables();

                Alert.Message("Pronto!", "Banco de dados atualizado com sucesso!", Alert.AlertType.success);
            };

            btnClearErroLog.Click += (s, e) =>
            {
                bool result = AlertOptions.Message("Atenção!", $"Você está prestes a limpar o log de erros.{Environment.NewLine}Deseja continuar?", Common.AlertBig.AlertType.info, Common.AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    File.Delete(Program.PATH_BASE + "\\logs\\EXCEPTIONS.txt");
                    erros.Text = "";
                }
            };

            ip.Leave += (s, e) => IniFile.Write("Remoto", ip.Text, "LOCAL");

            btnExit.Click += (s, e) => Close();
        }
    }
}
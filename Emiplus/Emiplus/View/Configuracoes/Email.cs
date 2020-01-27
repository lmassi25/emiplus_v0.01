using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

namespace Emiplus.View.Configuracoes
{
    public partial class Email : Form
    {
        public Email()
        {
            InitializeComponent();
            Eventos();
        }

        public void Start()
        {
            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_HOST", "EMAIL")))
                mail_host.Text = IniFile.Read("MAIL_HOST", "EMAIL");

            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_MODE", "EMAIL")))
                mail_mode.Text = IniFile.Read("MAIL_MODE", "EMAIL");

            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_PASS", "EMAIL")))
            {
                mail_pass.Text = Validation.Decrypt(IniFile.Read("MAIL_PASS", "EMAIL"));
            }

            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_PORT", "EMAIL")))
                mail_port.Text = IniFile.Read("MAIL_PORT", "EMAIL");

            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_SENDER", "EMAIL")))
                mail_sender.Text = IniFile.Read("MAIL_SENDER", "EMAIL");

            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_SMTP", "EMAIL")))
                mail_smtp.Text = IniFile.Read("MAIL_SMTP", "EMAIL");

            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_USER", "EMAIL")))
                mail_user.Text = IniFile.Read("MAIL_USER", "EMAIL");

            if (!String.IsNullOrEmpty(IniFile.Read("MAIL_EMIPLUS", "EMAIL")))
            {
                if (IniFile.Read("MAIL_EMIPLUS", "EMAIL") == "True")
                    servidorEmiplus.Toggled = true;
                else
                    servidorEmiplus.Toggled = false;
            }
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                ToolHelp.Show("Caso você não possua um servidor de email, deixe essa opção habilitada para que os envios de e-mail funcionem corretamente.", pictureBox6, ToolHelp.ToolTipIcon.Info, "Ajuda!");
                Start();
            };

            servidorEmiplus.Click += (s, e) =>
            {
                if (servidorEmiplus.Toggled)
                    IniFile.Write("MAIL_EMIPLUS", "False", "EMAIL");
                else
                    IniFile.Write("MAIL_EMIPLUS", "True", "EMAIL");
            };

            mail_host.Leave += (s, e) => IniFile.Write("MAIL_HOST", mail_host.Text, "EMAIL");
            mail_mode.Leave += (s, e) => IniFile.Write("MAIL_MODE", mail_mode.Text, "EMAIL");
            mail_pass.Leave += (s, e) => IniFile.Write("MAIL_PASS", Validation.Encrypt(mail_pass.Text), "EMAIL");
            mail_port.Leave += (s, e) => IniFile.Write("MAIL_PORT", mail_port.Text, "EMAIL");
            mail_sender.Leave += (s, e) => IniFile.Write("MAIL_SENDER", mail_sender.Text, "EMAIL");
            mail_smtp.Leave += (s, e) =>
            {
                if (!Validation.validMail(mail_smtp.Text))
                {
                    Alert.Message("Opps", "Insira um e-mail válido.", Alert.AlertType.warning);
                    return;
                }

                IniFile.Write("MAIL_SMTP", mail_smtp.Text, "EMAIL");
            };

            mail_user.Leave += (s, e) =>
            {
                if (!Validation.validMail(mail_user.Text))
                {
                    Alert.Message("Opps", "Insira um e-mail válido.", Alert.AlertType.warning);
                    return;
                }

                IniFile.Write("MAIL_USER", mail_user.Text, "EMAIL");
            };

            btnExit.Click += (s, e) => Close();
        }
    }
}
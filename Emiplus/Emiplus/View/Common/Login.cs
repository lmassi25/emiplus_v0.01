using System;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    using System.Runtime.InteropServices; // Biblioteca para mover tela
    using System.Net.Http;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using Emiplus.Data.Helpers;
    using System.Threading.Tasks;
    using Emiplus.Data.Core;
    using Emiplus.Properties;
    using System.IO;
    using System.Diagnostics;
    using System.Net;

    public partial class Login : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Login()
        {
            InitializeComponent();
            password.Text = "123@qwe";
            Update update = new Update();
            update.CheckUpdate();
            update.CheckIni();
            Eventos();

            version.Text = "Versão " + IniFile.Read("Version", "APP");

            if (Data.Core.Update.AtualizacaoDisponivel)
            {
                btnUpdate.Visible = true;
                btnUpdate.Text = "Atualizar Versão " + update.GetVersionWebTxt();
                label5.Visible = false;
                email.Visible = false;
                label6.Visible = false;
                password.Visible = false;
                btnEntrar.Visible = false;
                label1.Text = "Antes de continuar..";
            }
            else
            {
                btnUpdate.Visible = false;
                label5.Visible = true;
                email.Visible = true;
                label6.Visible = true;
                password.Visible = true;
                btnEntrar.Visible = true;
                label1.Text = "Entre com sua conta";
            }

            InitData();
        }

        #region DLL
        // DLL que possibilita mover a janela pela barra de título: BarraTitulo_MouseDown
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private async Task LoginAsync()
        {
            var values = new Dictionary<string, string>
            {
                { "token", "f012622defec1e2bad3b8596e0642c" },
                { "email", email.Text },
                { "password", password.Text }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(Program.URL_BASE + "/api/user", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var jo = JObject.Parse(responseString);

            if (jo["error"] != null && jo["error"].ToString() != "")
            {
                Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                return;
            }

            if (remember.Checked)
                Settings.Default.login_remember = true;
            else
                Settings.Default.login_remember = false;

            Settings.Default.login_email = email.Text;

            if (Validation.ConvertToInt32(jo["user"]["status"]) <= 0)
            {
                Alert.Message("Opss", "Você precisa ativar sua conta, acesse seu e-mail!", Alert.AlertType.info);
                return;
            }
            
            if (Validation.ConvertToInt32(jo["user"]["plan_status"]) <= 0)
            {
                if (Validation.ConvertToInt32(jo["user"]["plan_trial"]) <= 0)
                {
                    Alert.Message("Opss", "Seus dias de avaliação acabou, contrate um plano.", Alert.AlertType.info);
                    return;
                }
            }

            Settings.Default.user_id = Validation.ConvertToInt32(jo["user"]["id"]);
            Settings.Default.user_name = jo["user"]["name"].ToString();
            Settings.Default.user_lastname = jo["user"]["lastname"].ToString();
            Settings.Default.user_document = jo["user"]["document"].ToString();
            Settings.Default.user_thumb = jo["user"]["thumb"].ToString();
            Settings.Default.user_email = jo["user"]["email"].ToString();
            Settings.Default.user_password = password.Text;
            Settings.Default.user_cell = jo["user"]["cell"].ToString();
            Settings.Default.user_level = Validation.ConvertToInt32(jo["user"]["level"]);
            Settings.Default.user_status = Validation.ConvertToInt32(jo["user"]["status"]);
            Settings.Default.user_plan_id = jo["user"]["plan_id"].ToString();
            Settings.Default.user_plan_trial = Validation.ConvertToInt32(jo["user"]["plan_trial"]);
            Settings.Default.user_plan_status = Validation.ConvertToInt32(jo["user"]["plan_status"]);
            Settings.Default.user_plan_recorrencia = jo["plano"]["recorrencia"].ToString();
            Settings.Default.user_plan_fatura = jo["plano"]["proxima_fatura"].ToString();
            Settings.Default.empresa_id = Validation.ConvertToInt32(jo["empresa"]["id"]);
            Settings.Default.empresa_logo = jo["empresa"]["logo"].ToString();
            Settings.Default.empresa_razao_social = jo["empresa"]["razao_social"].ToString();
            Settings.Default.empresa_nome_fantasia = jo["empresa"]["nome_fantasia"].ToString();
            Settings.Default.empresa_cnpj = jo["empresa"]["cnpj"].ToString();
            Settings.Default.empresa_inscricao_estadual = jo["empresa"]["inscricao_estadual"].ToString();
            Settings.Default.empresa_inscricao_municipal = jo["empresa"]["inscricao_municipal"].ToString();
            Settings.Default.empresa_telefone = jo["empresa"]["telefone"].ToString();
            Settings.Default.empresa_rua = jo["empresa"]["rua"].ToString();
            Settings.Default.empresa_cep = jo["empresa"]["cep"].ToString();
            Settings.Default.empresa_nr = jo["empresa"]["nr"].ToString();
            Settings.Default.empresa_cidade = jo["empresa"]["cidade"].ToString();
            Settings.Default.empresa_bairro = jo["empresa"]["bairro"].ToString();
            Settings.Default.empresa_estado = jo["empresa"]["estado"].ToString();
            Settings.Default.empresa_ibge = jo["empresa"]["ibge"].ToString();
            Settings.Default.empresa_nfe_ultnfe = jo["empresa"]["ultnfe"].ToString();
            Settings.Default.empresa_nfe_serienfe = jo["empresa"]["serienfe"].ToString();
            Settings.Default.empresa_nfe_servidornfe = Validation.ConvertToInt32(jo["empresa"]["nfe_servidornfe"]);
            Settings.Default.empresa_crt = jo["empresa"]["crt"].ToString();
            Settings.Default.Save();

            Hide();
            Home form = new Home();
            form.ShowDialog();
        }

        private void InitData()
        {
            if (Settings.Default.login_email != string.Empty)
            {
                if (Settings.Default.login_remember)
                {
                    email.Text = Settings.Default.login_email;
                    remember.CheckState = CheckState.Checked;
                }
                else
                {
                    email.Text = Settings.Default.login_email;
                }
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    LoginAsync();
                    break;
            }
        }

        private void Eventos()
        {
            email.KeyDown += KeyDowns;
            password.KeyDown += KeyDowns;

            btnEntrar.Click += (s, e) => LoginAsync();

            btnUpdate.Click += (s, e) =>
            {
                if (MessageBox.Show("Deseja iniciar o processo de atualização?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IniFile.Write("Update", "true", "APP");

                    if (File.Exists($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe"))
                    {
                        File.Delete($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe");
                    }

                    if (!Directory.Exists($"{Support.BasePath()}\\Update"))
                        Directory.CreateDirectory($"{Support.BasePath()}\\Update");

                    using (var d = new WebClient())
                    {
                        d.DownloadFile($"https://github.com/lmassi25/files/releases/download/Install/InstallEmiplus.exe", $"{Support.BasePath()}\\Update\\InstalarEmiplus.exe");
                    }

                    if (!File.Exists($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe"))
                    {
                        Alert.Message("Oopss", "Não foi possível iniciar a atualização. Verifique a conexão com a internet.", Alert.AlertType.error);
                    }

                    Process.Start($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe");
                    KillEmiplus();
                }
            };

            btnFechar.Click += (s, e) => Close();
            linkRecover.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/admin/forgotten");
            linkSupport.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
        }

        private void KillEmiplus()
        {
            Process[] processes = Process.GetProcessesByName("Emiplus");
            foreach (Process process in processes)
                process.Kill();
        }
    }
}

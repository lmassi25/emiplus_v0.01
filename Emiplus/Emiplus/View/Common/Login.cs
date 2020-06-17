using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using RestSharp;
using SqlKata.Execution; // Biblioteca para mover tela

namespace Emiplus.View.Common
{
    public partial class Login : Form
    {

        private Usuarios _mUsuarios = new Usuarios();

        public Login()
        {
            InitializeComponent();
            Eventos();
        }

        private void LoginAsync()
        {
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                Alert.Message("Opps", "Preencha todos os campos.", Alert.AlertType.error);
                return;
            }

            if (Support.CheckForInternetConnection())
            {
                dynamic obj = new
                {
                    token = Program.TOKEN,
                    email = email.Text,
                    password = password.Text,
                    id_empresa = IniFile.Read("idEmpresa", "APP")
                };

                var jo = new RequestApi().URL(Program.URL_BASE + "/api/user").Content(obj, Method.POST).Response();
                if (jo["error"] != null && jo["error"].ToString() != "")
                {
                    Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                Settings.Default.login_remember = remember.Checked;
                Settings.Default.login_email = email.Text;
                
                if (Validation.ConvertToInt32(jo["user"]["status"]) <= 0)
                {
                    Alert.Message("Opss", "Você precisa ativar sua conta, acesse seu e-mail!", Alert.AlertType.info);
                    return;
                }

                if (Validation.ConvertToInt32(jo["user"]["plan_status"]) <= 0)
                    if (Validation.ConvertToInt32(jo["user"]["plan_trial"]) <= 0)
                    {
                        Alert.Message("Opss", "Seus dias de avaliação acabou, contrate um plano.",
                            Alert.AlertType.info);
                        return;
                    }

                Settings.Default.user_id = Validation.ConvertToInt32(jo["user"]["id"]);
                Settings.Default.user_name = jo["user"]["name"].ToString();
                Settings.Default.user_lastname = jo["user"]["lastname"].ToString();
                Settings.Default.user_document = jo["user"]["document"].ToString();
                Settings.Default.user_thumb = jo["user"]["thumb"].ToString();
                Settings.Default.user_email = jo["user"]["email"].ToString();
                Settings.Default.user_password = password.Text;
                Settings.Default.user_sub_user = Validation.ConvertToInt32(jo["user"]["sub_user"]);
                Settings.Default.user_cell = jo["user"]["cell"]?.ToString();
                Settings.Default.user_level = Validation.ConvertToInt32(jo["user"]["level"]);
                Settings.Default.user_status = Validation.ConvertToInt32(jo["user"]["status"]);
                Settings.Default.user_plan_id = jo["user"]["plan_id"].ToString();
                Settings.Default.user_plan_trial = Validation.ConvertToInt32(jo["user"]["plan_trial"]);
                Settings.Default.user_plan_status = Validation.ConvertToInt32(jo["user"]["plan_status"]);
                Settings.Default.user_plan_recorrencia = jo["plano"]["recorrencia"].ToString();
                Settings.Default.user_plan_fatura = jo["plano"]["proxima_fatura"].ToString();
                Settings.Default.user_comissao = Validation.ConvertToInt32(jo["user"]["comissao"]);
                Settings.Default.user_dbhost = jo["user"]["db_host"].ToString();

                Settings.Default.empresa_id = Validation.ConvertToInt32(jo["empresa"]["id"]);
                Settings.Default.empresa_unique_id = jo["empresa"]["id_unique"].ToString();
                Settings.Default.empresa_logo = jo["empresa"]["logo"].ToString();
                Settings.Default.empresa_razao_social = jo["empresa"]["razao_social"].ToString();
                Settings.Default.empresa_nome_fantasia = jo["empresa"]["nome_fantasia"].ToString();
                Settings.Default.empresa_cnpj = jo["empresa"]["cnpj"].ToString();
                Settings.Default.empresa_email = jo["empresa"]["email"].ToString();
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
                Settings.Default.empresa_nfe_servidornfe = Validation.ConvertToInt32(jo["empresa"]["servidornfe"]);
                Settings.Default.empresa_crt = jo["empresa"]["crt"].ToString();

                Settings.Default.empresa_servidornfse = Validation.ConvertToInt32(jo["empresa"]["servidornfse"]);
                Settings.Default.empresa_rps = jo["empresa"]["rps"].ToString();
                Settings.Default.empresa_serienfse = jo["empresa"]["serienfse"].ToString();
                Settings.Default.empresa_codigoitemnfse = jo["empresa"]["codigoitemnfse"].ToString();
                Settings.Default.empresa_codigotributacaonfse = jo["empresa"]["codigotributacaonfse"].ToString();
                Settings.Default.empresa_aliquotanfse = Validation.ConvertToDouble(jo["empresa"]["aliquotanfse"]);
                Settings.Default.empresa_calculanfse = Validation.ConvertToInt32(jo["empresa"]["calculanfse"]);
                Settings.Default.empresa_issretido = Validation.ConvertToInt32(jo["empresa"]["issretido"]);
                Settings.Default.empresa_infadnfse = jo["empresa"]["infadnfse"].ToString();

                Settings.Default.Save();

                //var usuarios = new Usuarios();
                int idUser = Validation.ConvertToInt32(jo["user"]["id"]);
                _mUsuarios = _mUsuarios.FindByUserId(idUser).FirstOrDefault<Usuarios>();
                if (_mUsuarios == null)
                {
                    _mUsuarios = new Usuarios();
                    _mUsuarios.Id = 0;
                    _mUsuarios.Excluir = 0;
                    _mUsuarios.Nome = $@"{jo["user"]["name"].ToString()} {jo["user"]["lastname"].ToString()}";
                    _mUsuarios.Id_User = Validation.ConvertToInt32(jo["user"]["id"]);
                    _mUsuarios.Comissao = Validation.ConvertToInt32(jo["user"]["comissao"]);
                    _mUsuarios.Sub_user = Validation.ConvertToInt32(jo["user"]["sub_user"]);
                    _mUsuarios.email = jo["user"]["email"].ToString();
                    _mUsuarios.senha = password.Text;
                }
                else
                {
                    _mUsuarios.Nome = $@"{jo["user"]["name"].ToString()} {jo["user"]["lastname"].ToString()}";
                    _mUsuarios.Id_User = Validation.ConvertToInt32(jo["user"]["id"]);
                    _mUsuarios.Comissao = Validation.ConvertToInt32(jo["user"]["comissao"]);
                    _mUsuarios.Sub_user = Validation.ConvertToInt32(jo["user"]["sub_user"]);
                    _mUsuarios.email = jo["user"]["email"].ToString();
                    _mUsuarios.senha = password.Text;
                }
                _mUsuarios.Save(_mUsuarios);

                //var userId = Settings.Default.user_sub_user == 0
                //    ? Settings.Default.user_id
                //    : Settings.Default.user_sub_user;
                //var dataUser = new RequestApi().URL($"{Program.URL_BASE}/api/listall/{Program.TOKEN}/{userId}")
                //    .Content().Response();
                //usuarios.Delete("excluir", 0);
                //foreach (var item in dataUser)
                //{
                //    var nameComplete = $"{item.Value["name"]} {item.Value["lastname"]}";
                //    var exists = usuarios.FindByUserId(Validation.ConvertToInt32(item.Value["id"])).FirstOrDefault();
                //    if (exists == null)
                //    {
                //        usuarios.Id = 0;
                //        usuarios.Excluir = Validation.ConvertToInt32(item.Value["excluir"]);
                //        usuarios.Nome = nameComplete;
                //        usuarios.Id_User = Validation.ConvertToInt32(item.Value["id"]);
                //        usuarios.Comissao = Validation.ConvertToInt32(item.Value["comissao"]);
                //        usuarios.Sub_user = Validation.ConvertToInt32(item.Value["sub_user"]);
                //        usuarios.email = item.Value["email"]?.ToString();
                //        usuarios.senha = item.Value["senha"]?.ToString();
                //    }
                //    else
                //    {
                //        usuarios.Id = exists.ID;
                //        usuarios.Excluir = Validation.ConvertToInt32(item.Value["excluir"]);
                //        usuarios.Nome = nameComplete;
                //        usuarios.Id_User = Validation.ConvertToInt32(item.Value["id"]);
                //        usuarios.Comissao = Validation.ConvertToInt32(item.Value["comissao"]);
                //        usuarios.Sub_user = Validation.ConvertToInt32(item.Value["sub_user"]);
                //        usuarios.email = item.Value["email"]?.ToString();
                //        usuarios.senha = item.Value["senha"]?.ToString();
                //    }

                //    usuarios.Save(usuarios);
                //}
            }
            else
            {
                var user = new Usuarios();

                var checkUsuarios = user.FindAll().Get();
                if (!checkUsuarios.Any())
                {
                    Alert.Message("Opps", "Você precisa estar conectado a internet no seu primeiro Login ao sistema.",
                        Alert.AlertType.error);
                    return;
                }

                var getUser = user.FindAll().Where("email", email.Text).FirstOrDefault<Usuarios>();
                if (getUser != null)
                {
                    if (getUser.senha != password.Text)
                    {
                        Alert.Message("Opps", "Senha incorreta!", Alert.AlertType.error);
                        return;
                    }

                    Settings.Default.user_id = getUser.Id_User;
                    Settings.Default.empresa_unique_id = getUser.id_empresa;
                }
            }

            Hide();
            var form = new Home();
            form.ShowDialog();
        }

        private void InitData()
        {
            if (!string.IsNullOrEmpty(Settings.Default.login_email))
                if (Settings.Default.login_remember)
                {
                    email.Text = Settings.Default.login_email;
                    remember.Checked = true;
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

            Load += (s, e) =>
            {
                version.Text = @"Versão " + IniFile.Read("Version", "APP");

                if (Support.CheckForInternetConnection())
                {
                    var update = new Update();
                    update.CheckUpdate();
                    update.CheckIni();

                    if (Data.Core.Update.AtualizacaoDisponivel)
                    {
                        btnUpdate.Visible = true;
                        btnUpdate.Text = $@"Atualizar Versão {update.GetVersionWebTxt()}";
                        label5.Visible = false;
                        email.Visible = false;
                        label6.Visible = false;
                        password.Visible = false;
                        btnEntrar.Visible = false;
                        label1.Text = @"Antes de continuar..";
                    }
                    else
                    {
                        btnUpdate.Visible = false;
                        label5.Visible = true;
                        email.Visible = true;
                        label6.Visible = true;
                        password.Visible = true;
                        btnEntrar.Visible = true;
                        label1.Text = @"Entre com sua conta";
                    }
                }

                // Valida a resolução do monitor e exibe uma mensagem
                Resolution.ValidateResolution();

                // Verifica se existe uma empresa vinculada a instalação da aplicação
                if (string.IsNullOrEmpty(IniFile.Read("idEmpresa", "APP")))
                {
                    panelEmpresa.Visible = true;
                    panelEmpresa.Dock = DockStyle.Fill;
                }

                InitData();
            };

            btnEntrar.Click += (s, e) => LoginAsync();

            btnUpdate.Click += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    AlertOptions.Message("Atenção!", "Você precisa estar conectado a internet para atualizar.",
                        AlertBig.AlertType.info, AlertBig.AlertBtn.OK);
                    return;
                }

                var result = AlertOptions.Message("Atenção!", "Deseja iniciar o processo de atualização?",
                    AlertBig.AlertType.info, AlertBig.AlertBtn.YesNo);
                if (result)
                {
                    IniFile.Write("Update", "true", "APP");

                    if (File.Exists($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe"))
                        File.Delete($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe");

                    if (!Directory.Exists($"{Support.BasePath()}\\Update"))
                        Directory.CreateDirectory($"{Support.BasePath()}\\Update");

                    using (var d = new WebClient())
                    {
                        d.DownloadFile("https://github.com/lmassi25/files/releases/download/Install/InstallEmiplus.exe",
                            $"{Support.BasePath()}\\Update\\InstalarEmiplus.exe");
                    }

                    if (!File.Exists($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe"))
                        Alert.Message("Oopss",
                            "Não foi possível iniciar a atualização. Verifique a conexão com a internet.",
                            Alert.AlertType.error);

                    Process.Start($"{Support.BasePath()}\\Update\\InstalarEmiplus.exe");
                    Validation.KillEmiplus();
                }
            };

            btnConfirmar.Click += (s, e) =>
            {
                var id_empresa = idEmpresa.Text;

                if (id_empresa.Length < 36)
                {
                    Alert.Message("Opps", "ID da empresa está incorreto! Por favor insira um ID válido.",
                        Alert.AlertType.error);
                    return;
                }

                if (Support.CheckForInternetConnection())
                {
                    dynamic objt = new
                    {
                        token = Program.TOKEN, id_empresa
                    };

                    var validar = new RequestApi().URL(Program.URL_BASE + "/api/validarempresa")
                        .Content(objt, Method.POST).Response();

                    if (validar["error"] == "true")
                    {
                        Alert.Message("Opps", "O ID da empresa informado não existe.", Alert.AlertType.error);
                        return;
                    }

                    IniFile.Write("idEmpresa", id_empresa, "APP");
                    panelEmpresa.Visible = false;
                }
                else
                {
                    Alert.Message("Opps", "Você precisa estar conectado a internet para continuar.",
                        Alert.AlertType.error);
                }
            };

            FormClosed += (s, e) => Validation.KillEmiplus();
            btnFechar.Click += (s, e) => Close();
            linkRecover.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/admin/forgotten");
            linkSupport.Click += (s, e) => Support.OpenLinkBrowser(Configs.LinkAjuda);
        }

        #region DLL

        // DLL que possibilita mover a janela pela barra de título: BarraTitulo_MouseDown
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        #endregion DLL
    }
}
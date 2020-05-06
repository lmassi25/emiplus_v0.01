using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Model;
using Emiplus.Properties;
using RestSharp;
using SqlKata.Execution;

namespace Emiplus.View.Common
{
    public partial class Home : Form
    {
        private readonly BackgroundWorker backWork = new BackgroundWorker();

        public Home()
        {
            InitializeComponent();
            Eventos();

            // new EmailSMTP().SetEmailTo("curruwilla@gmail.com", "William alvares").SetSubject("Teste de email2").SetBody("Corpo da mensagem em <strong>htmssl</strong>").Send();
        }

        public static string pessoaPage { get; set; }
        public static string pedidoPage { get; set; }
        public static string financeiroPage { get; set; }
        public static int idCaixa { get; set; }
        public static string CategoriaPage { get; set; }
        public static bool syncActive { get; set; }
        private static string pageClicked { get; set; }
        private static bool MenuExpansive { get; set; }

        /// <summary>
        ///     Inicia a tela inicial ao abrir o programa
        /// </summary>
        private void StartInicio()
        {
            homeMenuInicio.BackColor = Color.FromArgb(26, 32, 44);
            homeMenuInicio.ForeColor = Color.WhiteSmoke;
            pictureBox2.BackColor = Color.FromArgb(26, 32, 44);
            OpenForm.ShowInPanel<TelaInicial>(panelFormularios);

            homeMenuFiscal.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuProducts.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuComercial.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFinanceiro.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuSettings.BackColor = Color.FromArgb(46, 55, 72);
            //pictureBox2.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox3.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox4.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox5.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox6.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox7.BackColor = Color.FromArgb(46, 55, 72);
            panel3.Refresh();
        }

        /// <summary>
        ///     Realiza cadastro nota salva - tecnospeed
        /// </summary>
        private void CadastroNotaSalva()
        {
            if (Support.CheckForInternetConnection())
                // Realiza o cadastro no nota segura da empresa
                if (string.IsNullOrEmpty(IniFile.Read("encodeNS", "DEV")))
                {
                    var cnpjLimpo = Settings.Default.empresa_cnpj.Replace("-", "").Replace(".", "").Replace("/", "");

                    dynamic obj = new
                    {
                        corporate_name = Settings.Default.empresa_razao_social,
                        name = Settings.Default.empresa_nome_fantasia,
                        cnpj = cnpjLimpo,
                        email = Settings.Default.user_email,
                        password = "123@emiplus"
                    };

                    if (cnpjLimpo != "00000000000000")
                    {
                        new RequestApi()
                            .URL(
                                "https://app.notasegura.com.br/api/users/?softwarehouse=f278b338e853ed759383cca7da6dcf22e1c61301")
                            .Content(obj, Method.POST)
                            .AddHeader("Accept", "application/json")
                            .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                            .Response();

                        IniFile.Write("encodeNS",
                            Validation.Base64Encode($"{Settings.Default.empresa_email}:123@emiplus"), "DEV");
                    }
                }
        }

        private void MenuInicio(object sender, EventArgs e)
        {
            if (UserPermission.SetControl(homeMenuInicio, pictureBox13, "tela_inicial"))
                return;

            StartInicio();
        }

        private void MenuProdutos(object sender, EventArgs e)
        {
            if (UserPermission.SetControl(homeMenuProducts, pictureBox9, "all_produtos"))
                return;

            homeMenuProducts.BackColor = Color.FromArgb(26, 32, 44);
            homeMenuProducts.ForeColor = Color.WhiteSmoke;
            pictureBox3.BackColor = Color.FromArgb(26, 32, 44);
            OpenForm.ShowInPanel<TelaProdutosInicial>(panelFormularios);

            homeMenuInicio.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuComercial.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFinanceiro.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFiscal.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuSettings.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox2.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox4.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox5.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox6.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox7.BackColor = Color.FromArgb(46, 55, 72);

            panel3.Refresh();

            pageClicked = "homeMenuProducts";
        }

        private void MenuComercial(object sender, EventArgs e)
        {
            if (UserPermission.SetControl(homeMenuComercial, pictureBox10, "all_comercial"))
                return;

            homeMenuComercial.BackColor = Color.FromArgb(26, 32, 44);
            homeMenuComercial.ForeColor = Color.WhiteSmoke;
            pictureBox4.BackColor = Color.FromArgb(26, 32, 44);

            if (IniFile.Read("Alimentacao", "Comercial") == "True")
                OpenForm.ShowInPanel<TelaFood>(panelFormularios);
            else
                OpenForm.ShowInPanel<TelaComercialInicial>(panelFormularios);

            homeMenuInicio.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuProducts.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFinanceiro.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFiscal.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuSettings.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox2.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox3.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox5.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox6.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox7.BackColor = Color.FromArgb(46, 55, 72);

            panel3.Refresh();

            pageClicked = "homeMenuComercial";
        }

        private void MenuFinanceiro(object sender, EventArgs e)
        {
            if (UserPermission.SetControl(homeMenuFinanceiro, pictureBox11, "all_financeiro"))
                return;

            homeMenuFinanceiro.BackColor = Color.FromArgb(26, 32, 44);
            homeMenuFinanceiro.ForeColor = Color.WhiteSmoke;
            pictureBox5.BackColor = Color.FromArgb(26, 32, 44);
            OpenForm.ShowInPanel<TelaFinanceiroInicial>(panelFormularios);

            homeMenuInicio.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuProducts.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuComercial.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFiscal.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuSettings.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox2.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox3.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox4.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox6.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox7.BackColor = Color.FromArgb(46, 55, 72);

            panel3.Refresh();

            pageClicked = "homeMenuFinanceiro";
        }

        private void MenuFiscal(object sender, EventArgs e)
        {
            if (UserPermission.SetControl(homeMenuFiscal, pictureBox12, "all_fiscal"))
                return;

            homeMenuFiscal.BackColor = Color.FromArgb(26, 32, 44);
            homeMenuFiscal.ForeColor = Color.WhiteSmoke;
            pictureBox6.BackColor = Color.FromArgb(26, 32, 44);
            OpenForm.ShowInPanel<TelaFiscalInicial>(panelFormularios);

            homeMenuInicio.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuProducts.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuComercial.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFinanceiro.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuSettings.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox2.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox3.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox4.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox5.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox7.BackColor = Color.FromArgb(46, 55, 72);

            panel3.Refresh();

            pageClicked = "homeMenuFiscal";
        }

        private void MenuConfig(object sender, EventArgs e)
        {
            homeMenuSettings.BackColor = Color.FromArgb(26, 32, 44);
            homeMenuSettings.ForeColor = Color.WhiteSmoke;
            pictureBox7.BackColor = Color.FromArgb(26, 32, 44);
            OpenForm.ShowInPanel<TelaConfigInicial>(panelFormularios);

            homeMenuInicio.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFiscal.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuProducts.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuComercial.BackColor = Color.FromArgb(46, 55, 72);
            homeMenuFinanceiro.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox2.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox3.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox4.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox5.BackColor = Color.FromArgb(46, 55, 72);
            pictureBox6.BackColor = Color.FromArgb(46, 55, 72);

            panel3.Refresh();

            pageClicked = "homeMenuSettings";
        }

        private void Eventos()
        {
            Shown += async (s, e) =>
            {
                if (!UserPermission.SetControl(homeMenuInicio, pictureBox13, "tela_inicial", false))
                    StartInicio();

                // Pega versão
                version.Text = $@"Versão {IniFile.Read("Version", "APP")}";
                // Pega nome do usuario
                label1.Text =
                    $@"Olá, {Validation.FirstCharToUpper(Settings.Default.user_name)} {Validation.FirstCharToUpper(Settings.Default.user_lastname)}";

                // Atualiza menu Comercial -> Alimentação
                if (IniFile.Read("Alimentacao", "Comercial") == "True")
                {
                    homeMenuComercial.Text = @"            Alimentação";
                    pictureBox4.Image = Resources.waiter;
                }

                // Starta ações em segundo plano
                backWork.RunWorkerAsync();

                # region ###########  Menu expansivo

                MenuExpansive = IniFile.Read("MENU_EXPANSIVE", "LAYOUT") != "false";
                if (!MenuExpansive)
                {
                    panel3.Width = 241;
                    btnShowMenu.Image = Resources.seta1;
                    version.Visible = true;
                    MenuExpansive = false;
                    IniFile.Write("MENU_EXPANSIVE", "false", "LAYOUT");
                }
                else
                {
                    panel3.Width = 47;
                    btnShowMenu.Image = Resources.seta2;
                    version.Visible = false;
                    MenuExpansive = true;
                    IniFile.Write("MENU_EXPANSIVE", "true", "LAYOUT");
                }

                #endregion

                #region ###########  Dados do plano

                if (string.IsNullOrEmpty(Settings.Default.user_plan_id))
                {
                    plano.Text = @"Contrate um Plano";
                    btnPlano.Text = @"Contratar Plano";
                    recorrencia.Text = @"Contrate um Plano";
                    fatura.Visible = false;
                    label3.Visible = false;
                }
                else
                {
                    plano.Text = Validation.FirstCharToUpper(Settings.Default.user_plan_id);
                    recorrencia.Text = Validation.FirstCharToUpper(Settings.Default.user_plan_recorrencia);
                    fatura.Text = Settings.Default.user_plan_fatura;
                }

                if (Settings.Default.user_plan_status == 1)
                {
                    label6.Visible = false;
                    trialdias.Visible = false;
                }
                else
                {
                    trialdias.Text = $@"{Settings.Default.user_plan_trial} dias";
                }

                #endregion

                #region ###########  Menu developer

                if (IniFile.Read("dev", "DEV") == "true")
                    developer.Visible = true;

                #endregion

                ToolHelp.Show("Sistema de sincronização em andamento.", syncOn, ToolHelp.ToolTipIcon.Info,
                    "Sincronização!");
                timer1.Start();

                await Task.Delay(5000);
                // Janela de sincronização
                if (Support.CheckForInternetConnection())
                {
                    var f = new Sync();
                    f.Show();
                    f.Hide();
                }
            };

            developer.Click += (s, e) => OpenForm.ShowInPanel<Developer>(panelFormularios);
            btnPlano.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/admin");

            #region ###########  Menu CSS

            pictureBox2.Click += MenuInicio;
            homeMenuInicio.Click += MenuInicio;
            pictureBox2.MouseHover += (s, e) => pictureBox2.BackColor = Color.FromArgb(26, 32, 44);
            homeMenuInicio.MouseHover += (s, e) => pictureBox2.BackColor = Color.FromArgb(26, 32, 44);

            homeMenuProducts.Click += MenuProdutos;
            pictureBox3.Click += MenuProdutos;
            homeMenuProducts.MouseHover += (s, e) =>
            {
                if (pageClicked == "homeMenuProducts")
                    pictureBox3.BackColor = Color.FromArgb(26, 32, 44);

                pictureBox3.Refresh();
            };

            homeMenuComercial.Click += MenuComercial;
            pictureBox4.Click += MenuComercial;
            homeMenuComercial.MouseHover += (s, e) =>
            {
                if (pageClicked == "homeMenuComercial")
                    pictureBox4.BackColor = Color.FromArgb(26, 32, 44);

                pictureBox4.Refresh();
            };

            homeMenuFinanceiro.Click += MenuFinanceiro;
            pictureBox5.Click += MenuFinanceiro;
            homeMenuFinanceiro.MouseHover += (s, e) =>
            {
                if (pageClicked == "homeMenuFinanceiro")
                    pictureBox5.BackColor = Color.FromArgb(26, 32, 44);

                pictureBox5.Refresh();
            };

            homeMenuFiscal.Click += MenuFiscal;
            pictureBox6.Click += MenuFiscal;
            homeMenuFiscal.MouseHover += (s, e) =>
            {
                if (pageClicked == "homeMenuFiscal")
                    pictureBox6.BackColor = Color.FromArgb(26, 32, 44);

                pictureBox6.Refresh();
            };

            homeMenuSettings.Click += MenuConfig;
            pictureBox7.Click += MenuConfig;
            homeMenuSettings.MouseHover += (s, e) =>
            {
                if (pageClicked == "homeMenuFiscal")
                    pictureBox7.BackColor = Color.FromArgb(26, 32, 44);

                pictureBox7.Refresh();
            };

            #endregion

            btnSendSugesttion.Click += (s, e) =>
            {
                var f = new Suggestion();
                f.ShowDialog();
            };

            btnShowMenu.Click += (s, e) =>
            {
                if (MenuExpansive)
                {
                    panel3.Width = 241;
                    btnShowMenu.Image = Resources.seta1;
                    version.Visible = true;
                    MenuExpansive = false;
                    IniFile.Write("MENU_EXPANSIVE", "false", "LAYOUT");
                }
                else
                {
                    panel3.Width = 47;
                    btnShowMenu.Image = Resources.seta2;
                    version.Visible = false;
                    MenuExpansive = true;
                    IniFile.Write("MENU_EXPANSIVE", "true", "LAYOUT");
                }
            };

            FormClosed += (s, e) => Validation.KillEmiplus();
            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
            btnAccount.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/admin");

            timer1.Tick += (s, e) =>
            {
                if (syncActive)
                {
                    syncOn.Visible = true;
                    barraTituloHome.Refresh();
                }
                else
                {
                    syncOn.Visible = false;
                    barraTituloHome.Refresh();
                }
            };

            backWork.DoWork += (s, e) =>
            {
                if (File.Exists($@"{Program.PATH_BASE}\Suporte Emiplus.exe"))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(
                            new Uri("https://github.com/lmassi25/files/releases/download/Install/Suporte.Emiplus.exe"),
                            "Suporte Emiplus.exe");
                    }

                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(
                            new Uri(
                                "https://github.com/lmassi25/files/releases/download/Install/Suporte.Emiplus.exe.config"),
                            "Suporte Emiplus.exe.config");
                    }
                }

                var backup = new BackupAutomatico();
                backup.StartBackup();
                backup.BackupLocalDocuments();
                backup.StartBackupCupom();

                GerarTitulosRecorrentes();
                LimparRegistros();

                Program.SetPermissions();
                CadastroNotaSalva();
            };

            btnSuporteOnline.Click += (s, e) => Process.Start("C:\\Emiplus\\Suporte Emiplus.exe");
        }

        private void LimparRegistros()
        {
            var result = new Pessoa().FindAll().Where("nome", "NOVO REGISTRO").Get<Pessoa>();
            if (result != null)
                foreach (dynamic item in result)
                    new Pessoa().Delete("id", item.Id);
        }

        private void GerarTitulosRecorrentes()
        {
            var dias = Validation.ConvertToInt32(IniFile.Read("GerarRecDiasAntecipado", "FINANCEIRO"));
            var dataAtual = DateTime.Now.AddDays(-dias).ToString("dd.MM.yyyy");

            var titulo = new Titulo().Query().Where("excluir", 0).Where("vencimento", "<=", dataAtual)
                .Where("tipo_recorrencia", "!=", "0").Where("qtd_recorrencia", "0").Get();
            if (titulo != null)
                foreach (var item in titulo)
                {
                    DateTime dataVencimento = Convert.ToDateTime(item.VENCIMENTO);
                    switch (item.TIPO_RECORRENCIA)
                    {
                        case 1:
                            dataVencimento = dataVencimento.AddDays(1);
                            break;
                        case 2:
                            dataVencimento = dataVencimento.AddDays(1 * 7);
                            break;
                        case 3:
                            dataVencimento = dataVencimento.AddDays(1 * 14);
                            break;
                        case 4:
                            dataVencimento = dataVencimento.AddMonths(1);
                            break;
                        case 5:
                            dataVencimento = dataVencimento.AddMonths(1 * 3);
                            break;
                        case 6:
                            dataVencimento = dataVencimento.AddMonths(1 * 6);
                            break;
                        case 7:
                            dataVencimento = dataVencimento.AddYears(1);
                            break;
                    }

                    int idPai = item.ID_RECORRENCIA_PAI;
                    int nrParcela = item.NR_RECORRENCIA + 1;

                    var checkTitulo = new Titulo().Query().Where("id_recorrencia_pai", idPai)
                        .Where("nr_recorrencia", nrParcela).FirstOrDefault<Titulo>();
                    if (checkTitulo == null)
                    {
                        var gerarTitulo = new Titulo
                        {
                            Id = 0,
                            Tipo = item.TIPO,
                            Emissao = item.EMISSAO.ToString(),
                            Nome = item.NOME,
                            Id_Categoria = item.ID_CATEGORIA,
                            Id_Caixa = item.ID_CAIXA,
                            Id_FormaPgto = item.ID_FORMAPGTO,
                            Id_Pedido = item.ID_PEDIDO,
                            Vencimento = dataVencimento.ToString("dd.MM.yyyy"),
                            Total = Validation.ConvertToDouble(item.TOTAL),
                            Id_Pessoa = item.ID_PESSOA,
                            Obs = item.OBS,
                            id_usuario = item.ID_USUARIO,
                            ID_Recorrencia_Pai = item.ID_RECORRENCIA_PAI,
                            Tipo_Recorrencia = item.TIPO_RECORRENCIA,
                            Qtd_Recorrencia = item.QTD_RECORRENCIA,
                            Nr_Recorrencia = item.NR_RECORRENCIA + 1
                        };
                        gerarTitulo.Save(gerarTitulo, false);
                    }
                }
        }
    }
}
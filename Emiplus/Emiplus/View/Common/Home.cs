using Emiplus.Data.Core;
using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class Home : Form
    {
        #region Shadow box

        /********************************************************************
         * CÓDIGO ABAIXO ADICIONA SOMBRA NO WINDOWS FORM \/ \/ \/ \/
         ********************************************************************/

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
     );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }
                    break;

                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;
        }

        /********************************************************************
         * CÓDIGO ACIMA, ADICIONA SOMBRA NO WINDOWS FORM /\ /\ /\ /\
         ********************************************************************/

        #endregion Shadow box

        public static string pessoaPage { get; set; }
        public static string pedidoPage { get; set; }
        public static string financeiroPage { get; set; }
        public static int idCaixa { get; set; }
        public static string CategoriaPage { get; set; }
        
        public Home()
        {
            InitializeComponent();
            Eventos();

            version.Text = "Versão " + IniFile.Read("Version", "APP");
            label1.Text = $"Olá, {Validation.FirstCharToUpper(Settings.Default.user_name)} {Validation.FirstCharToUpper(Settings.Default.user_lastname)}";

            if (string.IsNullOrEmpty(Settings.Default.user_plan_id))
            {
                plano.Text = "Contrate um Plano";
                btnPlano.Text = "Contratar Plano";
                recorrencia.Text = "Contrate um Plano";
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
            } else
            {
                trialdias.Text = $"{Settings.Default.user_plan_trial} dias";
            }

            if (IniFile.Read("dev", "DEV") == "true")
                developer.Visible = true;
        }

        private void Eventos()
        {
            Load += (s, e) => {
                new Controller.Caixa().CheckCaixaDate();
            };

            developer.Click += (s, e) =>
            {
                OpenForm.ShowInPanel<Developer>(panelFormularios);
            };

            btnPlano.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/admin");

            homeMenuInicio.Click += (s, e) =>
            {
                homeMenuInicio.BackColor = Color.FromArgb(37, 48, 50);
                homeMenuInicio.ForeColor = Color.WhiteSmoke;
                pictureBox2.BackColor = Color.FromArgb(37, 48, 50);
                OpenForm.ShowInPanel<TelaInicial>(panelFormularios);

                homeMenuFiscal.BackColor = Color.Transparent;
                homeMenuProducts.BackColor = Color.Transparent;
                homeMenuComercial.BackColor = Color.Transparent;
                homeMenuFinanceiro.BackColor = Color.Transparent;
                homeMenuSettings.BackColor = Color.Transparent;
            };

            homeMenuProducts.Click += (s, e) =>
            {
                homeMenuProducts.BackColor = Color.FromArgb(37, 48, 50);
                homeMenuProducts.ForeColor = Color.WhiteSmoke;
                pictureBox3.BackColor = Color.FromArgb(37, 48, 50);
                OpenForm.ShowInPanel<TelaProdutosInicial>(panelFormularios);

                homeMenuInicio.BackColor = Color.Transparent;
                homeMenuComercial.BackColor = Color.Transparent;
                homeMenuFinanceiro.BackColor = Color.Transparent;
                homeMenuFiscal.BackColor = Color.Transparent;
                homeMenuSettings.BackColor = Color.Transparent;
            };

            homeMenuComercial.Click += (s, e) =>
            {
                homeMenuComercial.BackColor = Color.FromArgb(37, 48, 50);
                homeMenuComercial.ForeColor = Color.WhiteSmoke;
                pictureBox4.BackColor = Color.FromArgb(37, 48, 50);
                OpenForm.ShowInPanel<TelaComercialInicial>(panelFormularios);

                homeMenuInicio.BackColor = Color.Transparent;
                homeMenuProducts.BackColor = Color.Transparent;
                homeMenuFinanceiro.BackColor = Color.Transparent;
                homeMenuFiscal.BackColor = Color.Transparent;
                homeMenuSettings.BackColor = Color.Transparent;
            };

            homeMenuFinanceiro.Click += (s, e) =>
            {
                homeMenuFinanceiro.BackColor = Color.FromArgb(37, 48, 50);
                homeMenuFinanceiro.ForeColor = Color.WhiteSmoke;
                pictureBox5.BackColor = Color.FromArgb(37, 48, 50);
                OpenForm.ShowInPanel<TelaFinanceiroInicial>(panelFormularios);

                homeMenuInicio.BackColor = Color.Transparent;
                homeMenuProducts.BackColor = Color.Transparent;
                homeMenuComercial.BackColor = Color.Transparent;
                homeMenuFiscal.BackColor = Color.Transparent;
                homeMenuSettings.BackColor = Color.Transparent;
            };

            homeMenuFiscal.Click += (s, e) =>
            {
                homeMenuFiscal.BackColor = Color.FromArgb(37, 48, 50);
                homeMenuFiscal.ForeColor = Color.WhiteSmoke;
                pictureBox6.BackColor = Color.FromArgb(37, 48, 50);
                OpenForm.ShowInPanel<TelaFiscalInicial>(panelFormularios);

                homeMenuInicio.BackColor = Color.Transparent;
                homeMenuProducts.BackColor = Color.Transparent;
                homeMenuComercial.BackColor = Color.Transparent;
                homeMenuFinanceiro.BackColor = Color.Transparent;
                homeMenuSettings.BackColor = Color.Transparent;
            };

            homeMenuSettings.Click += (s, e) =>
            {
                homeMenuSettings.BackColor = Color.FromArgb(37, 48, 50);
                homeMenuSettings.ForeColor = Color.WhiteSmoke;
                pictureBox7.BackColor = Color.FromArgb(37, 48, 50);
                OpenForm.ShowInPanel<TelaConfigInicial>(panelFormularios);

                homeMenuInicio.BackColor = Color.Transparent;
                homeMenuFiscal.BackColor = Color.Transparent;
                homeMenuProducts.BackColor = Color.Transparent;
                homeMenuComercial.BackColor = Color.Transparent;
                homeMenuFinanceiro.BackColor = Color.Transparent;
            };

            FormClosed += (s, e) => Validation.KillEmiplus();

            btnHelp.Click += (s, e) => Support.OpenLinkBrowser("https://ajuda.emiplus.com.br");
            btnAccount.Click += (s, e) => Support.OpenLinkBrowser(Program.URL_BASE + "/admin");
        }
    }
}
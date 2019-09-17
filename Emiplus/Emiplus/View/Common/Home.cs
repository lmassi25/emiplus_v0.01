using Emiplus.Data.Helpers;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
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

        public Home()
        {
            InitializeComponent();
        }

        private void homeMenuProducts_Click(object sender, EventArgs e)
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
        }

        private void homeMenuComercial_Click(object sender, EventArgs e)
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
        }

        private void homeMenuFinanceiro_Click(object sender, EventArgs e)
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
        }

        private void homeMenuFiscal_Click(object sender, EventArgs e)
        {
            homeMenuFiscal.BackColor = Color.FromArgb(37, 48, 50);
            homeMenuFiscal.ForeColor = Color.WhiteSmoke;
            pictureBox6.BackColor = Color.FromArgb(37, 48, 50);
            OpenForm.ShowInPanel<TelaProdutosInicial>(panelFormularios);

            homeMenuInicio.BackColor = Color.Transparent;
            homeMenuProducts.BackColor = Color.Transparent;
            homeMenuComercial.BackColor = Color.Transparent;
            homeMenuFinanceiro.BackColor = Color.Transparent;
            homeMenuSettings.BackColor = Color.Transparent;
        }

        private void HomeMenuSettings_Click(object sender, EventArgs e)
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
        }

        private void HomeMenuInicio_Click(object sender, EventArgs e)
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
        }

        private void LinkSuporteWeb_Click(object sender, EventArgs e)
        {
            Support.OpenLinkBrowser("https://www.google.com");
        }
    }
}
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Common
{
    using Financeiro;
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
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
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

        public Home()
        {
            InitializeComponent();
        }

        #region Barra de tarefa
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        int lx, ly;
        int sw, sh;

        private void BtnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        // *BarraTitulo_MouseDown - DLL que possibilita mover a janela pela barra de título: BarraTitulo_MouseDown
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void BarraTituloHome_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private void homeMenuProducts_Click(object sender, EventArgs e)
        {
            homeMenuProducts.BackColor = Color.WhiteSmoke;
            OpenForm.Show<TelaProdutosInicial>(this);

            homeMenuComercial.BackColor = Color.White;
            homeMenuFinanceiro.BackColor = Color.White;
            homeMenuFiscal.BackColor = Color.White;
            homeMenuSettings.BackColor = Color.White;
        }

        private void homeMenuComercial_Click(object sender, EventArgs e)
        {
            homeMenuComercial.BackColor = Color.WhiteSmoke;
            OpenForm.Show<TelaComercialInicial>(this);

            homeMenuProducts.BackColor = Color.White;
            homeMenuFinanceiro.BackColor = Color.White;
            homeMenuFiscal.BackColor = Color.White;
            homeMenuSettings.BackColor = Color.White;
        }

        private void homeMenuFinanceiro_Click(object sender, EventArgs e)
        {
            homeMenuFinanceiro.BackColor = Color.WhiteSmoke;
            OpenForm.Show<TelaFinanceiroInicial>(this);

            homeMenuProducts.BackColor = Color.White;
            homeMenuComercial.BackColor = Color.White;
            homeMenuFiscal.BackColor = Color.White;
            homeMenuSettings.BackColor = Color.White;
        }

        private void homeMenuFiscal_Click(object sender, EventArgs e)
        {
            homeMenuFiscal.BackColor = Color.WhiteSmoke;
            OpenForm.Show<TelaFiscalInicial>(this);

            homeMenuProducts.BackColor = Color.White;
            homeMenuComercial.BackColor = Color.White;
            homeMenuFinanceiro.BackColor = Color.White;
            homeMenuSettings.BackColor = Color.White;
        }

        private void HomeMenuSettings_Click(object sender, EventArgs e)
        {
            homeMenuSettings.BackColor = Color.WhiteSmoke;
            OpenForm.Show<TelaConfigInicial>(this);

            homeMenuFiscal.BackColor = Color.White;
            homeMenuProducts.BackColor = Color.White;
            homeMenuComercial.BackColor = Color.White;
            homeMenuFinanceiro.BackColor = Color.White;
        }
    }
}
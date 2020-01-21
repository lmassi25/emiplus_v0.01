using CefSharp;
using CefSharp.WinForms;
using Emiplus.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class Suporte : Form
    {
        #region DLL SHADOW
        /********************************************************************
         * CÓDIGO ABAIXO ADICIONA SOMBRA NO WINDOWS FORM \/ \/ \/ \/
         ********************************************************************/
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,     // x-coordinate of upper-left corner
        int nTopRect,      // y-coordinate of upper-left corner
        int nRightRect,    // x-coordinate of lower-right corner
        int nBottomRect,   // y-coordinate of lower-right corner
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
        private const int WS_EX_TOPMOST = 0x00000008;

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

                cp.ExStyle |= WS_EX_TOPMOST;

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
        #endregion

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public ChromiumWebBrowser chromeBrowser;

        public Suporte()
        {
            InitializeComponent();
            InitializeChromiumAsync();
            Eventos();
        }

        public void InitializeChromiumAsync()
        {
            var embed = "<!DOCTYPE html> <html><head>" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/>" +
                "</head><body style=\"margin:0px!important\" style=\"text-align: center\">" +
                "<img src=\"https://www.emiplus.com.br/app/templates/default/assets/img/smile.png\" style=\"width: 32%; display: block; text-align: center; margin: 0 auto; padding: 40px 40px 25px;\">" +
                "<span style=\"width: 51%; display: block; text-align: center; margin: 0 auto; font-size: 1.2em; font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif; font-weight: 300; padding: 30px 0px;\">" +
                "Clique no ícone flutuante para falar com o suporte!</span>" +
                "<script>" +
                "(function(o, c, t, a, d, e, s, k) {" +
                "o.octadesk = o.octadesk || { };" +
                "s = c.getElementsByTagName(\"body\")[0];" +
                "k = c.createElement(\"script\");" +
                "k.async = 1;" +
                "k.src = t + '/' + a + '?showButton=' + d + '&openOnMessage=' + e;" +
                "s.appendChild(k);" +
                "})(window, document, 'https://chat.octadesk.services/api/widget', 'emiplus', true, true);" +
                "window.addEventListener('onOctaChatReady', function(e) {" +
                "octadesk.chat.login({" +
                "user:" +
                "{" +
                $"name: '{Settings.Default.user_name} {Settings.Default.user_lastname}'," +
                $"email: '{Settings.Default.user_email}'" +
                "}" +
                "})" +
                "})" +
                "</script> " +
            "</body></html>";

            CefSettings settings = new CefSettings();

            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            chromeBrowser = new ChromiumWebBrowser(string.Empty);

            panelBrowser.Controls.Add(chromeBrowser);

            chromeBrowser.LoadHtml(embed, "https://rendering/");
            chromeBrowser.Dock = DockStyle.Fill;

            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            btnFechar.Click += (s, e) =>
            {
                Close();
            };

            minimize.Click += (s, e) =>
            {
                this.WindowState = FormWindowState.Minimized;
            };
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class VideoTutorial : Form
    {
        private string video { get; set; }

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

        #endregion DLL SHADOW

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public VideoTutorial(string _urlVideo)
        {
            InitializeComponent();

            //watch?v=iqIAhAJMKFE
            video = _urlVideo.Replace("watch?v=", "embed/");

            btnFechar.Click += (s, e) =>
            {
                Close();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var embed = "<html><head>" +
            "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
            "</head><body style=\"margin:0px!important\">" +
            "<iframe style=\"display: block;background: #000;border: none;height: 100vh;width: 100vw;\" src =\"{0}\"" +
            "frameborder=\"0\" allow=\"autoplay; encrypted-media\" allowfullscreen></iframe>" +
            "</body></html>";
            var url = video;
            this.webBrowser1.DocumentText = string.Format(embed, url);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized
                        ? FormWindowState.Normal
                        : FormWindowState.Maximized;
        }
    }
}
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using RestSharp;

namespace Emiplus.View.Common
{
    public partial class Suggestion : Form
    {
        public const int WmNclbuttondown = 0xA1;
        public const int HtCaption = 0x2;

        private readonly BackgroundWorker backWorker = new BackgroundWorker();

        public Suggestion()
        {
            InitializeComponent();
            Eventos();
        }

        private bool Result { get; set; }
        private object Obj { get; set; }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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

            btnSend.Click += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    Alert.Message("Oppss",
                        "Você parece estar sem internet no momento e não conseguimos receber sua sugestão :(",
                        Alert.AlertType.error);
                    return;
                }

                if (Sugestao.Text.Length < 15)
                {
                    Alert.Message("Oppss", "Sua sugestão é muito curta, escreva em mais detalhes.",
                        Alert.AlertType.error);
                    return;
                }

                Obj = new
                {
                    token = Program.TOKEN,
                    user = Settings.Default.user_name + " " + Settings.Default.user_lastname,
                    empresa = Settings.Default.empresa_razao_social,
                    comment = Sugestao.Text
                };

                backWorker.RunWorkerAsync();
                loading.Visible = true;
                btnSend.Enabled = false;
            };

            backWorker.DoWork += (s, e) =>
            {
                var jo = new RequestApi().URL(Program.URL_BASE + "/api/suggestion").Content(Obj, Method.POST)
                    .Response();

                if (jo["error"] != null && jo["error"].ToString() != "")
                {
                    Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                Result = jo["result"]?.ToString() == "True";
            };

            backWorker.RunWorkerCompleted += (s, e) =>
            {
                if (Result)
                {
                    Alert.Message("Obrigado :)", "Sugestão enviada com sucesso.", Alert.AlertType.success);
                    btnSend.Enabled = true;
                    loading.Visible = false;
                    Sugestao.Text = string.Empty;
                    Close();
                }
                else
                {
                    Alert.Message("Opps", "Algo deu errado.", Alert.AlertType.error);
                    btnSend.Enabled = true;
                    loading.Visible = false;
                }
            };

            btnFechar.Click += (s, e) => Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WmNclbuttondown, HtCaption, 0);
            }
        }

        #region DLL SHADOW

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

        private bool m_aeroEnabled; // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WS_EX_TOPMOST = 0x00000008;

        public struct MARGINS // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84; // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                var cp = base.CreateParams;
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
                var enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return enabled == 1 ? true : false;
            }

            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT: // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        var margins = new MARGINS
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }

                    break;
            }

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int) m.Result == HTCLIENT) // drag the form
                m.Result = (IntPtr) HTCAPTION;
        }

        /********************************************************************
         * CÓDIGO ACIMA, ADICIONA SOMBRA NO WINDOWS FORM /\ /\ /\ /\
         ********************************************************************/

        #endregion DLL SHADOW
    }
}
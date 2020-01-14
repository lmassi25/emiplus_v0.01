using Emiplus.Data.Core;
using Emiplus.Data.Helpers;
using Emiplus.Properties;
using RestSharp;
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
    public partial class Suggestion : Form
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

        private BackgroundWorker backWorker = new BackgroundWorker();
        private bool Result { get; set; }
        private object obj { get; set; }

        public Suggestion()
        {
            InitializeComponent();
            Eventos();
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

            btnSend.Click += (s, e) =>
            {
                if (!Support.CheckForInternetConnection())
                {
                    Alert.Message("Oppss", "Você parece estar sem internet no momento e não conseguimos receber sua sugestão :(", Alert.AlertType.error);
                    return;
                }

                if (Sugestao.Text.Length < 15)
                {
                    Alert.Message("Oppss", "Sua sugestão é muito curta, escreva em mais detalhes.", Alert.AlertType.error);
                    return;
                }

                obj = new
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
                var jo = new RequestApi().URL(Program.URL_BASE + "/api/suggestion").Content(obj, Method.POST).Response();

                if (jo["error"] != null && jo["error"].ToString() != "")
                {
                    Alert.Message("Opss", jo["error"].ToString(), Alert.AlertType.error);
                    return;
                }

                if (jo["result"].ToString() == "True")
                    Result = true;
                else
                    Result = false;
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
                } else
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
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Emiplus.View.Common
{
    public partial class AlertBig : Form
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

        #endregion DLL SHADOW

        public AlertBig(string _title, string _message, AlertType type, AlertBtn btn, bool focus = false)
        {
            InitializeComponent();

            title.Text = _title;
            message.Text = _message;

            switch (type)
            {
                case AlertType.success:
                    icon.Image = Properties.Resources.success;
                    break;

                case AlertType.info:
                    icon.Image = Properties.Resources.info;
                    break;

                case AlertType.warning:
                    icon.Image = Properties.Resources.warning;
                    break;

                case AlertType.error:
                    icon.Image = Properties.Resources.error;
                    break;
            }

            switch (btn)
            {
                case AlertBtn.YesNo:
                    btnNo.Visible = true;
                    btnSim.Visible = true;
                    if (focus)
                        btnNo.Focus();
                    else
                        btnSim.Focus();
                    break;

                case AlertBtn.OK:
                    btnOk.Visible = true;
                    btnOk.Focus();
                    break;
            }

            Eventos();
            Retorno();
        }

        public enum AlertType
        {
            success, info, warning, error
        }

        public enum AlertBtn
        {
            YesNo, OK
        }

        public void Retorno()
        {
            KeyDown += KeyDowns;
            btnOk.KeyDown += KeyDowns;
            btnSim.KeyDown += KeyDowns;
            btnNo.KeyDown += KeyDowns;

            btnOk.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            btnNo.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            btnSim.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
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

        /// <summary>
        /// Eventos do form
        /// </summary>
        public void Eventos()
        {
            btnSim.Enter += (s, e) =>
            {
                btnSim.BackColorState.Enabled = Color.FromArgb(46, 204, 113);
                btnSim.BackColorState.Hover = Color.FromArgb(46, 204, 113);
                btnSim.Border.Color = Color.FromArgb(39, 192, 104);
                btnSim.Border.HoverColor = Color.FromArgb(39, 192, 104);

                btnNo.BackColorState.Enabled = Color.FromArgb(170, 9, 40);
                btnNo.BackColorState.Hover = Color.FromArgb(170, 9, 40);
                btnNo.Border.Color = Color.FromArgb(135, 4, 29);
                btnNo.Border.HoverColor = Color.FromArgb(135, 4, 29);
                this.Refresh();
            };

            btnNo.Enter += (s, e) =>
            {
                btnSim.BackColorState.Enabled = Color.FromArgb(16, 150, 73);
                btnSim.BackColorState.Hover = Color.FromArgb(16, 150, 73);
                btnSim.Border.Color = Color.FromArgb(6, 102, 47);
                btnSim.Border.HoverColor = Color.FromArgb(6, 102, 47);

                btnNo.BackColorState.Enabled = Color.FromArgb(255, 40, 81);
                btnNo.BackColorState.Hover = Color.FromArgb(255, 40, 81);
                btnNo.Border.Color = Color.FromArgb(241, 33, 73);
                btnNo.Border.HoverColor = Color.FromArgb(241, 33, 73);
                this.Refresh();
            };
        }
    }
}
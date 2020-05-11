using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emiplus.Model;
using Emiplus.View.Fiscal.TelasNota;
using SqlKata.Execution;

namespace Emiplus.View.Comercial
{
    public partial class OpcoesNfe : Form
    {
        private readonly Nota _modelNota = new Nota();

        public OpcoesNfe()
        {
            InitializeComponent();
            Eventos();
        }

        public static int idPedido { get; set; }

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

            Load += (s, e) =>
            {
                var checkNota = _modelNota.FindByIdPedidoAndTipo(idPedido, "NFe").FirstOrDefault<Nota>();
                if (checkNota == null)
                {
                    _modelNota.Id = 0;
                    _modelNota.Tipo = "NFe";
                    _modelNota.Status = "Pendente";
                    _modelNota.id_pedido = idPedido;
                    _modelNota.Save(_modelNota, false);
                }
            };

            Close.Click += (s, e) => Close();

            btnEmissaoRapida.Click += (s, e) =>
            {
                OpcoesNfeRapida.idPedido = idPedido;
                var f = new OpcoesNfeRapida {TopMost = true};
                f.Show();
                Hide();
            };

            btnEmissaoPasso.Click += (s, e) =>
            {
                Fiscal.Nota.Id = idPedido;
                var nota = new Fiscal.Nota {TopMost = true};
                nota.Show();
                Hide();
            };
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
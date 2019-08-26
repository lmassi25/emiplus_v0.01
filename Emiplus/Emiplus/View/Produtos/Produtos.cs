using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emiplus.Data.Helpers;
using Emiplus.View.Common;

// Biblioteca para mover tela

namespace Emiplus.View.Produtos
{
    public partial class Produtos : Form
    {
        public Produtos()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        //private const int cGrip = 16;      // Grip size
        //private const int cCaption = 32;   // Caption bar height;

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
        //    ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        //    rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
        //}

        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x84)
        //    {  // Trap WM_NCHITTEST
        //        Point pos = new Point(m.LParam.ToInt32());
        //        pos = this.PointToClient(pos);
        //        if (pos.Y < cCaption)
        //        {
        //            m.Result = (IntPtr)2;  // HTCAPTION
        //            return;
        //        }
        //        if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
        //        {
        //            m.Result = (IntPtr)17; // HTBOTTOMRIGHT
        //            return;
        //        }
        //    }
        //    base.WndProc(ref m);
        //}

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdicionarProduto_Click(object sender, EventArgs e)
        {
            OpenForm.Show<AddProduct>(this);
        }

        //// DLL que possibilita mover a janela pela barra de título: BarraTitulo_MouseDown
        //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        //private extern static void ReleaseCapture();
        //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        //private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        //{
        //    ReleaseCapture();
        //    SendMessage(this.Handle, 0x112, 0xf012, 0);
        //}

    }
}

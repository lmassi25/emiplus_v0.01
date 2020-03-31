using ESC_POS_USB_NET.Printer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.View.Testes
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        static WebBrowser webBrowser;

        private void button1_Click(object sender, EventArgs e)
        {
            var msg = new Controller.Fiscal().Imprimir(729, "CFe");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer("Jetway JP-800");
            printer.TestPrinter();
            printer.FullPaperCut();
            printer.PrintDocument();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fiscal = new Controller.Fiscal().RequestConsultCpf();
        }
    }
}

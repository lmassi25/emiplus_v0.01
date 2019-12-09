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
            string arq_filename = @"C:\Emiplus\NFe\Autorizadas\impressao.pdf";
            string printerName = "Jetway JP-800";

            FileInfo file = new FileInfo(arq_filename);
            if (file.Exists)
            {
                //ProcessStartInfo info = new ProcessStartInfo();
                //info.Verb = "print";
                //info.FileName = filename;
                //info.CreateNoWindow = true;
                //info.WindowStyle = ProcessWindowStyle.Hidden;

                //Process p = new Process();
                //p.StartInfo = info;
                //p.Start();

                //p.WaitForInputIdle();
                //System.Threading.Thread.Sleep(3000);
                //if (false == p.CloseMainWindow())
                //    p.Kill();

                //Process proc = new Process();
                //proc.StartInfo.FileName = @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                //proc.StartInfo.Arguments = @" /t /h " + "\"" + arq_filename + "\"" + " " + "\"" + printerName + "\"";
                //proc.StartInfo.UseShellExecute = true;
                //proc.StartInfo.CreateNoWindow = true;
                //proc.Start();
                //Thread.Sleep(1000);
                //proc.WaitForInputIdle();

                //proc.Kill();

                //Process.Start(
                //Registry.LocalMachine.OpenSubKey(
                //    @"SOFTWARE\Microsoft\Windows\CurrentVersion" +
                //    @"\App Paths\AcroRd32.exe").GetValue("").ToString(),
                //string.Format("/h /t \"{0}\" \"{1}\"", arq_filename, "\\192.168.1.188\\Jetway JP-800"));

                //System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                //    Form7.ShellExecuteA(0, "print", arq_filename, null, null, 0);
                //}

                //ShellExecuteA(0, "print", arq_filename, null, null, 0);

                //Process process = new Process
                //{
                //    StartInfo = new ProcessStartInfo
                //    {
                //        Verb = "print",
                //        FileName = _path_autorizada + "\\impressao.pdf",
                //    },
                //};

                //Process proc = new Process();
                //proc.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                //proc.StartInfo.Arguments = @" /t /h " + "\"" + arq_filename + "\"" + " " + "\"" + printerName + "\"";
                //proc.StartInfo.UseShellExecute = true;
                //proc.StartInfo.CreateNoWindow = true;
                //proc.Start();
                //Thread.Sleep(1000);
                //proc.WaitForInputIdle();

                //webBrowser1.Navigate("file:///C:/Emiplus/NFe/Autorizadas/impressao.pdf");
                //webBrowser1.ShowPrintPreviewDialog();
                

                
            }
        }

        [DllImport("shell32.dll", EntryPoint = "ShellExecute")]
        public static extern int ShellExecuteA(int hwnd, string lpOperation,
        string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            /// Print example but you can do whatever you want here
            /// 
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }

        private static void NavigateToUrl(String address)
        {
            //
            var uri = GetUriFromAddress(address);
            try
            {
                if (uri != null)
                {
                    /// set uri to the webBrowser object
                    webBrowser.Url = uri;
                    webBrowser.Navigate(uri);
                }
                else
                    return;
            }
            catch (UriFormatException)
            {
                return;
            }
        }

        private static Uri GetUriFromAddress(String address)
        {
            // can accept both HTTP and HTTPS URLs as valid 
            Uri uriResult;
            if (Uri.TryCreate(address, UriKind.Absolute, out uriResult)
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                return uriResult;
            else
                return null;

        }
    }
}

using System;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Emiplus.View.Reports
{
    public partial class Browser : Form
    {
        public static string htmlRender { get; set; }
        public ChromiumWebBrowser chromeBrowser;

        public Browser()
        {
            InitializeComponent();
            InitializeChromiumAsync();
            Eventos();
        }

        public void InitializeChromiumAsync()
        {
            CefSettings settings = new CefSettings();

            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            chromeBrowser = new ChromiumWebBrowser(string.Empty);

            panelBrowser.Controls.Add(chromeBrowser);

            chromeBrowser.LoadHtml(htmlRender, "https://rendering/");
            chromeBrowser.Dock = DockStyle.Fill;

            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;
        }

        private void Eventos()
        {
            Load += (s, e) =>
            {
                search.Select();
            };

            search.KeyUp += (s, e) =>
            {
                if (search.Text.Length <= 0)
                    chromeBrowser.StopFinding(true);
                else
                    chromeBrowser.Find(0, search.Text, true, false, false);
            };

            anterior.Click += (s, e) => chromeBrowser.Find(0, search.Text, false, false, false);
            proximo.Click += (s, e) => chromeBrowser.Find(0, search.Text, true, false, false);
            imprimir.Click += (s, e) =>
            {
                chromeBrowser.Print();
            };

            pdf.Click += (s, e) =>
            {
                using (var fileDialog = new OpenFileDialog())
                {
                    DialogResult res = fileDialog.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        chromeBrowser.PrintToPdfAsync(fileDialog.FileName);
                    }
                }
            };
        }
    }
}

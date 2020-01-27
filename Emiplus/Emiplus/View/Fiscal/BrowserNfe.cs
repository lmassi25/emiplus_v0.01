using CefSharp;
using CefSharp.WinForms;
using Emiplus.Data.Helpers;
using System.Windows.Forms;

namespace Emiplus.View.Fiscal
{
    public partial class BrowserNfe : Form
    {
        public static string Render { get; set; }
        public ChromiumWebBrowser chromeBrowser;

        public BrowserNfe()
        {
            InitializeComponent();
            Eventos();

            InitializeChromiumAsync();
        }

        public void InitializeChromiumAsync()
        {
            CefSettings settings = new CefSettings();

            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            chromeBrowser = new ChromiumWebBrowser(Render);

            panel.Controls.Add(chromeBrowser);
            panel.Dock = DockStyle.Fill;

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

            Load += (s, e) => Resolution.SetScreenMaximized(this);
        }
    }
}
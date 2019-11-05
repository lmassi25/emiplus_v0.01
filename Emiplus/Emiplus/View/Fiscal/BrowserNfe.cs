using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Emiplus.View.Fiscal
{
    public partial class BrowserNfe : Form
    {
        public static string Render { get; set; }
        public ChromiumWebBrowser chromeBrowser;

        public BrowserNfe()
        {
            InitializeComponent();
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
    }
}

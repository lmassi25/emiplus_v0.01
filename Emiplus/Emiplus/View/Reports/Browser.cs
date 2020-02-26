using CefSharp;
using CefSharp.WinForms;
using Emiplus.Data.Helpers;
using System;
using System.Windows.Forms;

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

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    DialogResult = DialogResult.OK;
                    break;
            }
        }

        private void Eventos()
        {
            KeyDown += KeyDowns;
            KeyPreview = true;

            Load += (s, e) =>
            {
                Resolution.SetScreenMaximized(this);

                search.Select();
            };

            FormClosed += (s, e) =>
            {
                DialogResult = DialogResult.OK;
            };

            search.KeyUp += (s, e) =>
            {
                try
                {
                    if (search.Text.Length <= 0)
                        chromeBrowser.StopFinding(true);
                    else
                        chromeBrowser.Find(0, search.Text, true, false, false);
                }
                catch (Exception)
                {
                }
            };

            anterior.Click += (s, e) => chromeBrowser.Find(0, search.Text, false, false, false);
            proximo.Click += (s, e) => chromeBrowser.Find(0, search.Text, true, false, false);

            imprimir.Click += (s, e) =>
            {
                chromeBrowser.Print();
            };

            pdf.Click += (s, e) =>
            {
                using (var fileDialog = new SaveFileDialog())
                {
                    fileDialog.DefaultExt = "pdf";
                    fileDialog.Filter = "PDF(*.pdf)|*.pdf";
                    fileDialog.AddExtension = true;
                    fileDialog.FileName = $"PDF-{Validation.RandomNumeric(15)}";
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
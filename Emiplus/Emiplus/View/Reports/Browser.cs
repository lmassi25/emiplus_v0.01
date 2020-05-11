using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Emiplus.Data.Helpers;

namespace Emiplus.View.Reports
{
    public partial class Browser : Form
    {
        public ChromiumWebBrowser chromeBrowser;

        public Browser()
        {
            InitializeComponent();
            InitializeChromiumAsync();
            Eventos();
        }

        public static string htmlRender { get; set; }

        public void InitializeChromiumAsync()
        {
            var settings = new CefSettings();

            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            chromeBrowser = new ChromiumWebBrowser(string.Empty);

            panelBrowser.Controls.Add(chromeBrowser);

            chromeBrowser.LoadHtml(htmlRender, "https://rendering/");
            chromeBrowser.Dock = DockStyle.Fill;

            var browserSettings = new BrowserSettings
            {
                FileAccessFromFileUrls = CefState.Enabled,
                UniversalAccessFromFileUrls = CefState.Enabled
            };
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

            FormClosed += (s, e) => { DialogResult = DialogResult.OK; };

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
                    // ignored
                }
            };

            anterior.Click += (s, e) => chromeBrowser.Find(0, search.Text, false, false, false);
            proximo.Click += (s, e) => chromeBrowser.Find(0, search.Text, true, false, false);

            imprimir.Click += (s, e) => { chromeBrowser.Print(); };

            pdf.Click += (s, e) =>
            {
                using (var fileDialog = new SaveFileDialog())
                {
                    fileDialog.DefaultExt = "pdf";
                    fileDialog.Filter = @"PDF(*.pdf)|*.pdf";
                    fileDialog.AddExtension = true;
                    fileDialog.FileName = $"PDF-{Validation.RandomNumeric(15)}";
                    var res = fileDialog.ShowDialog();
                    if (res == DialogResult.OK) chromeBrowser.PrintToPdfAsync(fileDialog.FileName);
                }
            };
        }
    }
}
using CefSharp;
using CefSharp.WinForms;
using Emiplus.Properties;
using System.Windows.Forms;

namespace Emiplus.Data.Core
{
    public class ChatSupport
    {
        public ChromiumWebBrowser chromeBrowser;

        public void InitializeChromiumAsync(Panel panel)
        {
            var embed = "<!DOCTYPE html> <html><head>" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/>" +
                "</head><body style=\"margin:0px!important\" style=\"text-align: center\">" +
                "<img src=\"https://www.emiplus.com.br/app/templates/default/assets/img/smile.png\" style=\"width: 32%; display: block; text-align: center; margin: 0 auto; padding: 40px 40px 25px;\">" +
                "<span style=\"width: 51%; display: block; text-align: center; margin: 0 auto; font-size: 1.2em; font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif; font-weight: 300; padding: 30px 0px;\">" +
                "Clique no ícone flutuante para falar com o suporte!</span>" +
                "<script>" +
                "(function(o, c, t, a, d, e, s, k) {" +
                "o.octadesk = o.octadesk || { };" +
                "s = c.getElementsByTagName(\"body\")[0];" +
                "k = c.createElement(\"script\");" +
                "k.async = 1;" +
                "k.src = t + '/' + a + '?showButton=' + d + '&openOnMessage=' + e;" +
                "s.appendChild(k);" +
                "})(window, document, 'https://chat.octadesk.services/api/widget', 'emiplus', true, true);" +
                "window.addEventListener('onOctaChatReady', function(e) {" +
                "octadesk.chat.login({" +
                "user:" +
                "{" +
                $"name: '{Settings.Default.user_name} {Settings.Default.user_lastname}'," +
                $"email: '{Settings.Default.user_email}'" +
                "}" +
                "})" +
                "})" +
                "</script> " +
            "</body></html>";

            CefSettings settings = new CefSettings();

            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            chromeBrowser = new ChromiumWebBrowser(string.Empty);

            panel.Controls.Add(chromeBrowser);

            chromeBrowser.LoadHtml(embed, "https://rendering/");
            chromeBrowser.Dock = DockStyle.Fill;

            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;
        }
    }
}

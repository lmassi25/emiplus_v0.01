using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Emiplus.Properties;

namespace Emiplus.Data.Core
{
    public class ChatSupport
    {
        public ChromiumWebBrowser ChromeBrowser;

        public void InitializeChromiumAsync(Panel panel)
        {
            var embed = "<!DOCTYPE html> <html><head>" +
                        "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/>" +
                        "</head><body style=\"margin:0px!important\" style=\"text-align: center\">" +
                        "<img src=\"https://www.emiplus.com.br/app/templates/default/assets/img/smile.png\" style=\"width: 32%; display: block; text-align: center; margin: 0 auto; padding: 40px 40px 25px;\">" +
                        "<span style=\"width: 51%; display: block; text-align: center; margin: 0 auto; font-size: 1.2em; font-family: Segoe UI,Frutiger,Frutiger Linotype,Dejavu Sans,Helvetica Neue,Arial,sans-serif; font-weight: 300; padding: 30px 0px;\">" +
                        "Clique no ícone flutuante para falar com o suporte!</span>" +
                        //"<script>jivo_onLoadCallback = function () {jivo_api.setContactInfo({\r\n    " +
                        //$"\"name\": \"{Settings.Default.user_name} {Settings.Default.user_lastname}\",\r\n   " +
                        //$" \"email\": \"{Settings.Default.user_email}\",\r\n   " +
                        //$" \"phone\": \"{Settings.Default.empresa_telefone}\",\r\n    " +
                        //"\"description\": \"Suporte através do programa\"\r\n " +
                        //"});}; jivo_onLoadCallback(); </script>" +
                        "<script src=\"//code.jivosite.com/widget/2RL9rGtLcm\" async></script>" +
                        "</body></html>";

            var settings = new CefSettings();

            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            ChromeBrowser = new ChromiumWebBrowser(string.Empty);

            panel.Controls.Add(ChromeBrowser);

            ChromeBrowser.LoadHtml(embed, "https://rendering/");
            ChromeBrowser.Dock = DockStyle.Fill;

            var browserSettings = new BrowserSettings
            {
                FileAccessFromFileUrls = CefState.Enabled,
                UniversalAccessFromFileUrls = CefState.Enabled
            };
            ChromeBrowser.BrowserSettings = browserSettings;
        }
    }
}
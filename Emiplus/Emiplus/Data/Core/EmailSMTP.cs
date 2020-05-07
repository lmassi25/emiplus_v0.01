using System.Net;
using System.Net.Mail;
using System.Text;
using Emiplus.Data.Helpers;

namespace Emiplus.Data.Core
{
    internal class EmailSMTP
    {
        private MailAddress fromAddress;

        private MailMessage message;

        private readonly SmtpClient smtpClient;
        private readonly NetworkCredential smtpCredentials;
        private MailAddress toAddress;

        public EmailSMTP()
        {
            if (IniFile.Read("MAIL_EMIPLUS", "EMAIL") == "True")
            {
                Host = "mail.emiplus.com.br";
                Mode = "tls";
                Port = 587;
                User = "noresponse@emiplus.com.br";
                Smtp = "noresponse@emiplus.com.br";
                Pass = "123@emiplus";
                Sender = "Emiplus";
            }
            else
            {
                Host = IniFile.Read("MAIL_HOST", "EMAIL");
                Mode = IniFile.Read("MAIL_MODE", "EMAIL");
                Port = Validation.ConvertToInt32(IniFile.Read("MAIL_PORT", "EMAIL"));
                User = IniFile.Read("MAIL_USER", "EMAIL");
                Smtp = IniFile.Read("MAIL_SMTP", "EMAIL");
                Pass = Validation.Decrypt(IniFile.Read("MAIL_PASS", "EMAIL"));
                Sender = IniFile.Read("MAIL_SENDER", "EMAIL");
            }

            smtpClient = new SmtpClient();
            smtpCredentials = new NetworkCredential(Smtp, Pass);
        }

        private string Host { get; }
        private int Port { get; }
        private string Mode { get; }
        private string Smtp { get; }
        private string Pass { get; }
        private string Sender { get; }
        private string User { get; }
        private string From { get; set; }
        private string To { get; set; }
        private string ToName { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }

        public EmailSMTP SetEmailTo(string email, string name)
        {
            To = Validation.validMail(email) ? email : "";
            ToName = name ?? "";
            return this;
        }

        public EmailSMTP SetSubject(string subject)
        {
            Subject = subject;
            return this;
        }

        public EmailSMTP SetBody(string body)
        {
            Body = body;
            return this;
        }

        public void Send()
        {
            message = new MailMessage();
            fromAddress = new MailAddress(User, Sender);
            toAddress = new MailAddress(To, ToName);

            smtpClient.Host = Host;
            smtpClient.Port = Port;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = smtpCredentials;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Timeout = 20000;

            message.From = fromAddress;
            message.To.Add(toAddress);
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = Subject;
            message.Body = Body;

            smtpClient.SendAsync(message, null);
        }
    }
}
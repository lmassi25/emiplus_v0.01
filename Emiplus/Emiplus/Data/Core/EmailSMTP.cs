using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace Emiplus.Data.Core
{
    class EmailSMTP
    {
        protected string _defaultHost = "mail.emiplus.com.br";
        private string Host
        {
            get { return _defaultHost; }
            set { _defaultHost = value; }
        }

        protected int _defaultPort = 587;
        private int Port
        {
            get { return _defaultPort; }
            set { _defaultPort = value; }
        }

        private string From { get; set; }
        private string To { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }

        private SmtpClient smtpClient;
        private NetworkCredential smtpCredentials;

        private MailMessage message;
        private MailAddress fromAddress;
        private MailAddress toAddress;

        public EmailSMTP()
        {
            smtpClient = new SmtpClient();
            smtpCredentials = new NetworkCredential("suporte@emiplus.com.br", "4586928wW#0");
        }

        public EmailSMTP SetEmailFrom(string from)
        {
            From = validMail(from) ? from : "";
            return this;
        }

        public EmailSMTP SetEmailTo(string to)
        {
            To = validMail(to) ? to : "";
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

        public EmailSMTP SetPort(int port = 587)
        {
            Port = port != 0 ? port : 587;
            return this;
        }

        public void Send()
        {

            message = new MailMessage();
            fromAddress = new MailAddress(From);
            toAddress = new MailAddress(To);

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
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = Subject;
            message.Body = Body;

            smtpClient.SendAsync(message, null);
        }

        private bool validMail(string address)
        {
            EmailAddressAttribute e = new EmailAddressAttribute();
            if (e.IsValid(address))
                return true;

            return false;
        }
    }
}

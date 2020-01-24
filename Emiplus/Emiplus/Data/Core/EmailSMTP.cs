using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using Emiplus.Data.Helpers;
using System;

namespace Emiplus.Data.Core
{
    class EmailSMTP
    {
        private string Host { get; set; }
        private int Port { get; set; }
        private string Mode { get; set; }
        private string Smtp { get; set; }
        private string Pass { get; set; }
        private string Sender { get; set; }
        private string User { get; set; }
        private string From { get; set; }
        private string To { get; set; }
        private string ToName { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }

        private SmtpClient smtpClient;
        private NetworkCredential smtpCredentials;

        private MailMessage message;
        private MailAddress fromAddress;
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

                //Host = "mail.uilia.com.br";
                //Mode = "tls";
                //Port = 587;
                //User = "contato@uilia.com.br";
                //Smtp = "contato@uilia.com.br";
                //Pass = "4586928wW#0";
                //Sender = "UILIA";
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

        public EmailSMTP SetEmailTo(string email, string name)
        {
            To = validMail(email) ? email : "";
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

            try
            {
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
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
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

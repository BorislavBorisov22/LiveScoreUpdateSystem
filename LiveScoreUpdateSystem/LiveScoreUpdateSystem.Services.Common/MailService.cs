using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using LiveScoreUpdateSystem.Services.Common.Contracts;

namespace LiveScoreUpdateSystem.Services.Common
{
    public class MailService : IMailService
    {
        private const int SmtpPort = 587;
        private const string LiveScoreSystemMailUsername = "livescoreupdatesystem@gmail.com";
        private const string LiveScoreSystemMailPassword = "123updatesystem";
        public const string MailName = "LiveScoreUpdateSystem";

        public void SendEmail(string subject, string content, IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient();
                smtpServer.Credentials = new NetworkCredential(LiveScoreSystemMailUsername,LiveScoreSystemMailPassword);
                smtpServer.Port = SmtpPort;
                smtpServer.Host = "smtp.gmail.com";

                smtpServer.EnableSsl = true;
                smtpServer.EnableSsl = true;
                mail.To.Add(email);
                mail.From = new MailAddress(LiveScoreSystemMailUsername, MailName);
                mail.Subject = subject;
                mail.Body = content;
                smtpServer.Send(mail);
            }
        }
    }
}

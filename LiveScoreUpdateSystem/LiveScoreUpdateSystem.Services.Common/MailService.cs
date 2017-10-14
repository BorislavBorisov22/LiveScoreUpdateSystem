using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace LiveScoreUpdateSystem.Services.Common
{
    public class MailService
    {
        public void SendEmail(string subject, string content, IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient();
                smtpServer.Credentials = new NetworkCredential("BorislavAdmin@gmail.com", "123456");
                smtpServer.Port = 587;
                smtpServer.Host = "smtp.gmail.com";

                smtpServer.EnableSsl = true;
                smtpServer.EnableSsl = true;
                mail.To.Add(email);
                mail.From = new MailAddress("BorislavAdmin@gmail.com");
                mail.Subject = subject;
                mail.Body = content;
                smtpServer.Send(mail);
            }
        }
    }
}

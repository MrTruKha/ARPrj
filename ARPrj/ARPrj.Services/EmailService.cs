using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Service
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string SystemEmail, string SystemEmailPassword, string SystemEmailSmtp, string SystemEmailSmtpPort, string email, string subject, string content)
        {
            MailMessage mail = new MailMessage(SystemEmail, email);
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = true;
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = SystemEmailSmtp;
                    smtpClient.Port = Convert.ToInt32(SystemEmailSmtpPort);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new NetworkCredential(SystemEmail, SystemEmailPassword);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(mail);
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
                Subject = "Şifre Sıfırlama İsteği",
                Body = $"Şifrenizi sıfırlamak için <a href='{resetLink}'>bu linke</a> tıklayın.", 
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using var smtpClient = new SmtpClient(_smtpSettings.Server)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value ?? throw new ArgumentNullException(nameof(smtpSettings));
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var fromAddress = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName);
            var toAddress = new MailAddress(email);
            const string subject = "HireSync - Şifre Sıfırlama İsteği";

            var htmlBody = @"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {
            font-family: Arial, sans-serif;
            color: #333;
            background-color: #f4f4f4;
            padding: 20px;
            margin: 0;
        }
        .container {
            max-width: 600px;
            margin: auto;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }
        .header {
            background-color: #004080;
            padding: 30px;
            text-align: center;
        }
        .header img {
            width: 150px;
        }
        .content {
            background-color: #ffffff;
            padding: 30px 40px 40px 40px;
            text-align: center;
            color: #333;
        }
        .content h2 {
            margin-top: 0;
            color: #004080;
        }
        .button {
            display: inline-block;
            background-color: #004080;
            color: white !important;
            padding: 12px 30px;
            text-decoration: none;
            border-radius: 5px;
            font-weight: bold;
            margin-top: 25px;
            transition: background-color 0.3s ease;
        }
        .button:hover {
            background-color: #002952;
        }
        .footer {
            font-size: 12px;
            color: #888;
            margin-top: 40px;
            text-align: center;
            padding-bottom: 20px;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <img src=""cid:hiresynclogo"" alt=""HireSync Logo"" />
        </div>
        <div class=""content"">
            <h2>Şifre Sıfırlama İsteği</h2>
            <p>Şifrenizi sıfırlamak için aşağıdaki butona tıklayın.</p>
            <a href=""" + resetLink + @""" class=""button"">Şifreyi Sıfırla</a>
            <p class=""footer"">Eğer bu isteği siz yapmadıysanız, bu maili görmezden gelin.</p>
        </div>
    </div>
</body>
</html>";

            using var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
            };

            var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            string logoPath = "/Users/furkankuzucuk/Documents/GitHub/HireSync/HireSync/hiresync-frontend/src/image/hiresync.jpeg";

            var logoResource = new LinkedResource(logoPath, MediaTypeNames.Image.Jpeg)
            {
                ContentId = "hiresynclogo",
                TransferEncoding = TransferEncoding.Base64,
                ContentType = new ContentType(MediaTypeNames.Image.Jpeg)
            };

            htmlView.LinkedResources.Add(logoResource);
            mailMessage.AlternateViews.Add(htmlView);

            using var smtpClient = new SmtpClient(_smtpSettings.Server)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

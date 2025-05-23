using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RcFinal.Data;
using RcFinal.Models;
using System.Net;
using System.Net.Mail;

namespace RcFinal.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            var subject = "Confirm your email";
            var htmlMessage = $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.";
            return SendEmailAsync(email, subject, htmlMessage);
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            var subject = "Reset your password";
            var htmlMessage = $"Please reset your password by <a href='{resetLink}'>clicking here</a>.";
            return SendEmailAsync(email, subject, htmlMessage);
        }

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            var subject = "Reset your password";
            var htmlMessage = $"Please reset your password using the following code: {resetCode}";
            return SendEmailAsync(email, subject, htmlMessage);
        }

        private Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            return client.SendMailAsync(mailMessage);
        }
    }
}

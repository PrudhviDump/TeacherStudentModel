using Backend.Business_Logic_Layer.Interfaces;
using Backend.Infrastructure;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
//using Backend.Business_Logic_Layer.Interfaces;

namespace Business_Logic_Layer.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendStudentEnrollmentEmailAsync(string toEmail, string studentName)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail),
                Subject = "Student Enrollment Successful",
                Body = $"Hello {studentName},\n\nYou have been successfully enrolled.\n\nRegards,\nStudent Management System",
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);

            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(
                    _emailSettings.Username,
                    _emailSettings.Password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

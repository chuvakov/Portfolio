using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Portfolio.WebApi.Dto;
using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailConfig _emailConfig;

        public EmailController(IConfiguration configuration)
        {
            _emailConfig = new EmailConfig();
            configuration.GetSection("Email").Bind(_emailConfig);
        }

        [HttpPost]
        public async Task SendEmailAsync (SendEmailRequest sendEmailRequest)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Сайт портфолио", _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress("", _emailConfig.To));
            emailMessage.Subject = sendEmailRequest.Title;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = $"Имя: {sendEmailRequest.Name}\nПочта: {sendEmailRequest.Email}\n" + sendEmailRequest.Message
            };

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_emailConfig.SmtpAddress, _emailConfig.SmtpPort);
                await smtpClient.AuthenticateAsync(_emailConfig.From, _emailConfig.SmtpPassword);
                await smtpClient.SendAsync(emailMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}


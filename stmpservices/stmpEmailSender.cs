using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
namespace EducoreSuite.stmpservices;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpSettings _settings;
    public SmtpEmailSender(IOptions<SmtpSettings> smtpSettings)
    {
        _settings = smtpSettings.Value;
    }
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = new NetworkCredential(_settings.Username, _settings.Password)
        };
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_settings.Username),
            Subject = subject,
            Body = body,
            IsBodyHtml = false // Set to true if the body is HTML
        };
        
        mailMessage.To.Add(to);
        await client.SendMailAsync(mailMessage);
    }
}

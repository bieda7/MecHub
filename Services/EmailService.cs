using System.Net;
using System.Net.Mail;

namespace MecHub.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task EnviarEmailAsync(string destinatario, string assunto, string mensagem)
    {
        var smtpHost = _configuration["Email:SmtpHost"];
        var smtpPort = int.Parse(_configuration["Email:SmtpPort"]);
        var smtpUser = _configuration["Email:Usuario"];
        var smtpPass = _configuration["Email:Senha"];

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(smtpUser, "MecHub"),
            Subject = assunto,
            Body = mensagem,
            IsBodyHtml = false
        };

        mail.To.Add(destinatario);

        await client.SendMailAsync(mail);
    }
}
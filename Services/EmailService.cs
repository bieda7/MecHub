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
        var smtpPortTexto = _configuration["Email:SmtpPort"];
        var smtpUser = _configuration["Email:Usuario"];
        var smtpPass = _configuration["Email:Senha"];

        if (string.IsNullOrWhiteSpace(smtpHost) ||
            string.IsNullOrWhiteSpace(smtpPortTexto) ||
            string.IsNullOrWhiteSpace(smtpUser) ||
            string.IsNullOrWhiteSpace(smtpPass))
        {
            throw new Exception("Configurações SMTP não encontradas. Verifique appsettings ou variáveis do Railway.");
        }

        if (!int.TryParse(smtpPortTexto, out var smtpPort))
        {
            throw new Exception("Porta SMTP inválida.");
        }

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true,
            Timeout = 10000
        };

        using var mail = new MailMessage
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
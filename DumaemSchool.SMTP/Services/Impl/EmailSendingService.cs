using DumaemSchool.SMTP.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace DumaemSchool.SMTP.Services.Impl;

public sealed class EmailSendingService : IEmailSendingService
{
    private readonly MailSettings _mailSettings;
    private readonly ILogger<EmailSendingService> _logger;
    private readonly ISmtpClient _smtpClient;
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public EmailSendingService(MailSettings mailSettings,
        ILogger<EmailSendingService> logger,
        ISmtpClient smtpClient)
    {
        _mailSettings = mailSettings;
        _logger = logger;
        _smtpClient = smtpClient;
    }

    public async Task<bool> SendEmailAsync(MailingMessage message)
    {
        try
        {
            await _semaphore.WaitAsync();

            var messageToSend = new MimeMessage();
            messageToSend.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            messageToSend.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            messageToSend.To.Add(MailboxAddress.Parse(message.ToEmail));
            messageToSend.Subject = message.Subject;
            var builder = new BodyBuilder()
            {
                HtmlBody = message.Body
            };

            messageToSend.Body = builder.ToMessageBody();
            if (_mailSettings.Port == 465)
                await _smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, true);
            else
                await _smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

            await _smtpClient.SendAsync(messageToSend);
            await _smtpClient.DisconnectAsync(true);
            return true;
        }
        catch (Exception)
        {
            _logger.LogError("Ошибка при отправки E-mail на почту {Email} с темой {Subject}", message.ToEmail,
                message.Subject);
            return false;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
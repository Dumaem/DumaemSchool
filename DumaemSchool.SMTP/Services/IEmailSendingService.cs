using DumaemSchool.SMTP.Models;

namespace DumaemSchool.SMTP.Services;

public interface IEmailSendingService
{
    public Task<bool> SendEmailAsync(MailingMessage message);
}
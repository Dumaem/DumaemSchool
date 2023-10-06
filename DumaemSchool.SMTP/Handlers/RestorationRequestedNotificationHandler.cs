using DumaemSchool.Core.Notifications;
using DumaemSchool.SMTP.Models;
using DumaemSchool.SMTP.Services;
using MediatR;

namespace DumaemSchool.SMTP.Handlers;

public sealed class RestorationRequestedNotificationHandler : INotificationHandler<RestorationRequestedNotification>
{
    private readonly IEmailSendingService _emailSendingService;

    public RestorationRequestedNotificationHandler(IEmailSendingService emailSendingService)
    {
        _emailSendingService = emailSendingService;
    }

    public async Task Handle(RestorationRequestedNotification notification,
        CancellationToken cancellationToken)
    {
        var message = new MailingMessage
        {
            Subject = EmailSubjects.AccountRestorationCode,
            ToEmail = notification.Email,
            Body = $"Ваш код для восстановления доступа: {notification.RestorationCode}"
        };

        await _emailSendingService.SendEmailAsync(message);
    }
}
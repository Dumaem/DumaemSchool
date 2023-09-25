using DumaemSchool.SMTP.Commands;
using DumaemSchool.SMTP.Models;
using DumaemSchool.SMTP.Services;
using MediatR;

namespace DumaemSchool.SMTP.Handlers;

public sealed class SendRestorationCodeEmailCommandHandler : IRequestHandler<SendRestorationCodeEmailCommand, bool>
{
    private readonly IEmailSendingService _emailSendingService;

    public SendRestorationCodeEmailCommandHandler(IEmailSendingService emailSendingService)
    {
        _emailSendingService = emailSendingService;
    }

    public async Task<bool> Handle(SendRestorationCodeEmailCommand request,
        CancellationToken cancellationToken)
    {
        var message = new MailingMessage
        {
            Subject = EmailSubjects.AccountRestorationCode,
            ToEmail = request.Email,
            Body = $"Ваш код для восстановления доступа: {request.RestorationCode}"
        };

        return await _emailSendingService.SendEmailAsync(message);
    }
}
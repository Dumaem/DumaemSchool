using DumaemSchool.Auth.Models;
using DumaemSchool.Core.Commands.Auth;
using DumaemSchool.Core.Notifications;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DumaemSchool.Auth.Handlers;

internal sealed class
    GenerateRestorationCodeCommandHandler : IRequestHandler<GenerateRestorationCodeCommand, Result<Guid>>
{
    private readonly IPublisher _publisher;
    private readonly SchoolIdentityContext _context;
    private readonly UserManager<SchoolUser> _userManager;

    public GenerateRestorationCodeCommandHandler(SchoolIdentityContext context,
        UserManager<SchoolUser> userManager, 
        IPublisher publisher)
    {
        _context = context;
        _userManager = userManager;
        _publisher = publisher;
    }

    public async Task<Result<Guid>> Handle(GenerateRestorationCodeCommand request,
        CancellationToken cancellationToken)
    {
        var userId = (await _userManager.FindByEmailAsync(request.Email))?.Id;
        if (userId is null)
        {
            return new Result<Guid>(new Exception("User with this email does not exist"));
        }

        var sessionId = Guid.NewGuid();
        var code = Random.Shared.Next(1000, 10000);
        var model = new RestorationCode
        {
            UserId = userId.Value, Code = code, SessionId = sessionId 
        };
        await _context.AddAsync(model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _context.ChangeTracker.Clear();

        var notification = new RestorationRequestedNotification(request.Email, code);
        await _publisher.Publish(notification, cancellationToken);

        return sessionId;
    }
}
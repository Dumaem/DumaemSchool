using System.Security.Claims;
using DumaemSchool.Auth.Models;
using DumaemSchool.Core.Exceptions;
using DumaemSchool.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DumaemSchool.Auth.Handlers;

internal sealed class TeacherCreatedNotificationHandler : INotificationHandler<TeacherCreatedNotification>
{
    private readonly UserManager<SchoolUser> _userManager;
    private readonly SchoolIdentityContext _context;

    public TeacherCreatedNotificationHandler(UserManager<SchoolUser> userManager, SchoolIdentityContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task Handle(TeacherCreatedNotification notification,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (await _userManager.FindByEmailAsync(notification.Email) is not null)
            {
                throw new TeacherCreationException("User with this email already exists");
            }

            var user = new SchoolUser { Email = notification.Email, UserName = notification.Email };
            // TODO: генерация пароля
            var password = "kredit200";

            var res = await _userManager.CreateAsync(user, password);
            if (!res.Succeeded)
            {
                throw new TeacherCreationException($"Could not create a user: {string.Join("; ", res.Errors.Select(x => $"{x.Code} - {x.Description}"))}");
            }

            await _userManager.AddToRoleAsync(user, Role.Teacher);
            await _userManager.AddClaimAsync(user, new Claim(UserClaims.TeacherId, notification.Teacher.Id.ToString()));
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
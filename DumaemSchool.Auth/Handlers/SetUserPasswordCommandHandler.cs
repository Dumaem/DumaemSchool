using DumaemSchool.Auth.Models;
using DumaemSchool.Core.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DumaemSchool.Auth.Handlers;

public sealed class SetUserPasswordCommandHandler : IRequestHandler<SetUserPasswordCommand>
{
    private readonly UserManager<SchoolUser> _userManager;

    public SetUserPasswordCommandHandler(UserManager<SchoolUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(SetUserPasswordCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return;

        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, request.NewPassword);
    }
}
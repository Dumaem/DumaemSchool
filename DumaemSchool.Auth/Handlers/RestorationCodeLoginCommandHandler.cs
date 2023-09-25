using DumaemSchool.Auth.Models;
using DumaemSchool.Core.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DumaemSchool.Auth.Handlers;

internal sealed class RestorationCodeLoginCommandHandler : IRequestHandler<RestorationCodeLoginCommand, bool>
{
    private readonly SchoolIdentityContext _context;
    private readonly UserManager<SchoolUser> _userManager;


    public RestorationCodeLoginCommandHandler(SchoolIdentityContext context,
        UserManager<SchoolUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> Handle(RestorationCodeLoginCommand request,
        CancellationToken cancellationToken)
    {
        var userId = (await _userManager.FindByEmailAsync(request.Email))?.Id;
        if (userId is null)
            return false;

        var storedCode =
            _context.RestorationCodes.FirstOrDefault(x => x.SessionId == request.SessionId && x.UserId == userId);
        if (storedCode is null)
            return false;
        if (request.CodeProvided != storedCode.Code)
        {
            return false;
        }

        if (storedCode.DateCreated - DateTime.Now > AuthConstants.MaximumCodeLifetime)
        {
            return false;
        }

        return true;
    }
}
using DumaemSchool.Auth.Models;
using DumaemSchool.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DumaemSchool.Auth.Managers;

public sealed class SchoolSignInManager : SignInManager<SchoolUser>
{
    private readonly ISender _sender;

    public SchoolSignInManager(UserManager<SchoolUser> userManager, IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<SchoolUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
        ILogger<SchoolSignInManager> logger, IAuthenticationSchemeProvider schemes,
        IUserConfirmation<SchoolUser> confirmation, ISender sender) : base(userManager, contextAccessor, claimsFactory,
        optionsAccessor,
        logger, schemes, confirmation)
    {
        _sender = sender;
    }

    public override async Task<bool> CanSignInAsync(SchoolUser user)
    {
        var isTeacher = await UserManager.IsInRoleAsync(user, Role.Teacher);
        if (!isTeacher)
            return true;

        var claims = await UserManager.GetClaimsAsync(user);
        var teacherId = int.Parse(claims.First(x => x.Type == UserClaims.TeacherId).Value);
        return !await _sender.Send(new CheckTeacherFiredCommand(teacherId));
    }
}
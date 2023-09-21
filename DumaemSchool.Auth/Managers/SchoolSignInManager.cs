using DumaemSchool.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DumaemSchool.Auth.Managers;

public sealed class SchoolSignInManager : SignInManager<SchoolUser>
{
    public override async Task<bool> CanSignInAsync(SchoolUser user)
    {
        var isTeacher = await UserManager.IsInRoleAsync(user, Role.Teacher);
        if (!isTeacher)
            return true;
        return false;
    }

    public SchoolSignInManager(UserManager<SchoolUser> userManager, IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<SchoolUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<SchoolUser>> logger, IAuthenticationSchemeProvider schemes,
        IUserConfirmation<SchoolUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
        logger, schemes, confirmation)
    {
    }
}
using System.Collections.Concurrent;
using DumaemSchool.Auth.Models;
using DumaemSchool.BlazorWeb.Data;
using Microsoft.AspNetCore.Identity;

namespace DumaemSchool.BlazorWeb.Middleware;

public class AuthenticationMiddleware
{
    public static IDictionary<Guid, LoginCredentials> Logins { get; }
        = new ConcurrentDictionary<Guid, LoginCredentials>();


    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, SignInManager<SchoolUser> signInManager)
    {
        if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
        {
            var key = Guid.Parse(context.Request.Query["key"]!);
            var info = Logins[key];

            var user = await signInManager.UserManager.FindByEmailAsync(info.Email);
            var result = await signInManager.PasswordSignInAsync(user!, info.Password,
                info.RememberMe, lockoutOnFailure: false);
            info.Password = string.Empty;
            Logins.Remove(key);
            if (!result.Succeeded)
            {
                context.Response.Redirect("/login");
                return;
            }

            context.Response.Redirect("/");
        }
        else if (context.Request.Path == "/logout")
        {
            await signInManager.SignOutAsync();
            context.Response.Redirect("/login");
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}
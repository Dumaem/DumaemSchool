using System.Reflection;
using DumaemSchool.Auth.Managers;
using DumaemSchool.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DumaemSchool.Auth;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SchoolIdentityContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseNpgsql(configuration.GetConnectionString("Default")!,
                npgsqlOptions =>
                {
                    npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
        });
        services.AddIdentity<SchoolUser, SchoolRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<SchoolRole>()
            .AddEntityFrameworkStores<SchoolIdentityContext>()
            .AddSignInManager<SchoolSignInManager>();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
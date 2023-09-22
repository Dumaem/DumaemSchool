using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DumaemSchool.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        if (connectionString is null)
            throw new Exception("No database connection string");

        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<NpgsqlConnectionProvider>();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
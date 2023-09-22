using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DumaemSchool.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
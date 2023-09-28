using System.Reflection;
using DumaemSchool.SMTP.Models;
using DumaemSchool.SMTP.Services;
using DumaemSchool.SMTP.Services.Impl;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DumaemSchool.SMTP;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSMTP(this IServiceCollection services, IConfiguration configuration)
    {
        var mailSettings = new MailSettings();

        configuration.Bind(nameof(mailSettings), mailSettings);
        services.AddSingleton(mailSettings);

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddScoped<ISmtpClient, SmtpClient>();
        services.AddScoped<IEmailSendingService, EmailSendingService>();
        return services;
    }
}
using Application.Abstraction.ConsoleWrapper;
using Application.Abstraction.Loggers;
using Application.Abstraction.Managers;
using Application.Implementation.ConsoleWrapper;
using Application.Implementation.Loggers;
using Application.Implementation.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Application;

public static class ConfigureApplication
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPayrollManager, PayrollManager>();
        services.AddScoped<IConsoleWrapper, ConsoleWrapper>();
        services.AddScoped<ILogger>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            return LoggerFactory.CreateLogger(configuration, provider.GetRequiredService<IConsoleWrapper>());
        });
    }
}
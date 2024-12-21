using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementOfFunds.Modules;

public static class DbModule
{
    public static async Task InitialiseDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        await initializer.InitializeAsync();

        if (bool.Parse(config["AllowSeeders"] ?? "false"))
        {
            await seeder.SeedAsync();
        }
    }
}
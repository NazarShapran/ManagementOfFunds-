using Application.Abstraction.Managers;
using Application.Abstraction.Queries;
using Application.Abstraction.Repositories;
using Application.Implementation.Managers;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;


namespace Infrastructure.Persistence;

public static class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuild = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Default"));
        dataSourceBuild.EnableDynamicJson();
        var dataSource = dataSourceBuild.Build();

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddRepositories();
        services.AddManagers();
    } 
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<EmployeeRepository>();
        services.AddScoped<IEmployeesRepository>(provider => provider.GetRequiredService<EmployeeRepository>());
        services.AddScoped<IEmployeesQueries>(provider => provider.GetRequiredService<EmployeeRepository>());
        
        services.AddScoped<TransactionRepository>();
        services.AddScoped<ITransactionsRepository>(provider => provider.GetRequiredService<TransactionRepository>());
        services.AddScoped<ITransactionsQueries>(provider => provider.GetRequiredService<TransactionRepository>());
    }
    private static void AddManagers(this IServiceCollection services)
    {
        services.AddSingleton<IPayrollManager, PayrollManager>();
    }
}
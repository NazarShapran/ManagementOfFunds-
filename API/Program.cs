using Application;
using Application.Abstraction.Managers;
using Infrastructure;
using Infrastructure.Persistence;
using ManagementOfFunds.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<Seeder>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    await scope.ServiceProvider.InitialiseDb();
    
    var payrollManager = scope.ServiceProvider.GetRequiredService<IPayrollManager>();
    var startDate = new DateTime(2024, 1, 1);
    var endDate = new DateTime(2024, 12, 31);
                
    try
    {
        var totalPayments = await payrollManager.GetTotalPayments(startDate, endDate);
        Console.WriteLine($"Загальна сума транзакцій за період з {startDate.ToShortDateString()} по {endDate.ToShortDateString()} становить: {totalPayments}.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

await host.RunAsync();
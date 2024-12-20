using Domain.Employees;
using Domain.Transactions;

namespace Infrastructure.Persistence;

public class Seeder(ApplicationDbContext context)
{
    public async Task SeedAsync()
    {
        await SeedEmployeesAsync();
        await SeedTransactionsAsync();
    }

    private async Task SeedEmployeesAsync()
    {
        var employees = new List<Employee>
        {
            new(new EmployeeId(Guid.NewGuid()), "John Doe", "Manager", 5000),
            new(new EmployeeId(Guid.NewGuid()), "Jane Smith", "Developer", 4000),
            new(new EmployeeId(Guid.NewGuid()), "Alice Brown", "Designer", 3000)
        };
        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();
    }

    private async Task SeedTransactionsAsync()
    {
        var employees = context.Employees.ToList();

        var transactions = new List<Transaction>
        {
            new(new TransactionId(Guid.NewGuid()), employees[0].Id, 2000, DateTime.Now, TransactionType.Salary),
            new(new TransactionId(Guid.NewGuid()), employees[1].Id, 1500, DateTime.Now, TransactionType.Bonus),
            new(new TransactionId(Guid.NewGuid()), employees[2].Id, 1200, DateTime.Now, TransactionType.Salary),
            new(new TransactionId(Guid.NewGuid()), employees[0].Id, 500, DateTime.Now.AddMonths(1),
                TransactionType.Bonus),
            new(new TransactionId(Guid.NewGuid()), employees[1].Id, 1600, DateTime.Now.AddMonths(1),
                TransactionType.Salary)
        };
        context.Transactions.AddRange(transactions);
        await context.SaveChangesAsync();
    }
}
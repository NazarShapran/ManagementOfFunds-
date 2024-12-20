using Application.Abstraction.Queries;
using Application.Abstraction.Repositories;
using Domain.Employees;
using Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TransactionRepository(ApplicationDbContext context) : ITransactionsRepository, ITransactionsQueries
{
    public async Task<Transaction> Create(Transaction transaction)
    {
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> Delete(Transaction transaction)
    {
        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();
        return transaction;
    }

    public async Task<List<Transaction>> GetAll()
    {
        return await context.Transactions.ToListAsync();
    }

    public async Task<List<Transaction>> GetByEmployeeId(EmployeeId employeeId)
    {
        return await context.Transactions
            .Where(x => x.EmployeeId == employeeId)
            .ToListAsync();
    }
}
using Domain.Transactions;

namespace Application.Abstraction.Repositories;

public interface ITransactionsRepository
{
    Task<Transaction> Create(Transaction transaction);
    Task<Transaction> Delete(Transaction transaction);
}
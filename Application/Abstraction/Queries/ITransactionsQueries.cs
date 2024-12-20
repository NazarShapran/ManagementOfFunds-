using Domain.Employees;
using Domain.Transactions;

namespace Application.Abstraction.Queries;

public interface ITransactionsQueries
{
    Task<List<Transaction>> GetAll();
    Task<List<Transaction>> GetByEmployeeId(EmployeeId employeeId);
}
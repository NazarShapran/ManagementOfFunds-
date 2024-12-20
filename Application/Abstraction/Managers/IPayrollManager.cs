using Domain.Employees;
using Domain.Transactions;

namespace Application.Abstraction.Managers;

public interface IPayrollManager
{
    Task<Employee> AddEmployee(Employee employee);
    Task RemoveEmployee(Guid employeeId);
    Task<Transaction> AddTransaction(Transaction transaction);
    Task<List<Transaction>> GetTransactions(Guid employeeId);
    Task<decimal> GetTotalPayments(DateTime startDate, DateTime endDate);
}
using Application.Abstraction.Managers;
using Application.Abstraction.Queries;
using Application.Abstraction.Repositories;
using Domain.Employees;
using Domain.Transactions;

namespace Application.Implementation.Managers;

public class PayrollManager(
    IEmployeesRepository employeesRepository,
    IEmployeesQueries employeesQueries,
    ITransactionsQueries transactionsQueries,
    ITransactionsRepository transactionsRepository) : IPayrollManager
{
    public async Task<Employee> AddEmployee(Employee employee)
    {
        var existingEmployee = await employeesQueries.GetById(employee.Id);
        if (existingEmployee != null)
        {
            throw new InvalidOperationException($"Працівник із ID {employee.Id.Value} вже існує.");
        }
        return await employeesRepository.Create(employee);
    }

    public async Task  RemoveEmployee(Guid employeeId)
    {
        var employee = await employeesQueries.GetById(new EmployeeId(employeeId));
        if (employee == null)
        {
            throw new KeyNotFoundException($"Працівника з ID {employeeId} не знайдено.");
        }

        await employeesRepository.Delete(employee);
    }

    public async Task<Transaction> AddTransaction(Transaction transaction)
    {
        var employee = await employeesQueries.GetById(transaction.EmployeeId);
        if (employee == null)
        {
            throw new KeyNotFoundException($"Працівника з ID {transaction.EmployeeId.Value} не знайдено.");
        }
        
        if (transaction.Amount <= 0)
        {
            throw new ArgumentException("Сума транзакції повинна бути більшою за 0.");
        }
        
        return await transactionsRepository.Create(transaction);
    }

    public async Task<List<Transaction>> GetTransactions(Guid employeeId)
    {
        var transactions = await transactionsQueries.GetByEmployeeId(new EmployeeId(employeeId));
        if (!transactions.Any())
        {
            throw new KeyNotFoundException($"Для працівника з ID {employeeId} транзакцій не знайдено.");
        }
        return transactions;
    }

    public async Task<decimal> GetTotalPayments(DateTime startDate, DateTime endDate)
    {
        var transactions = await transactionsQueries.GetAll();

        return transactions
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .Sum(t => t.Amount);
    }
}
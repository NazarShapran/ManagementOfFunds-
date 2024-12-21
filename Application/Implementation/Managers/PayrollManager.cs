using Application.Abstraction.Loggers;
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
    ITransactionsRepository transactionsRepository,
    ILogger logger) : IPayrollManager
{
    public async Task<Employee> AddEmployee(Employee employee)
    {
        try
        {
            var existingEmployee = await employeesQueries.GetById(employee.Id);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException($"Працівник з ID {employee.Id.Value} вже існує.");
            }
            return await employeesRepository.Create(employee);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Помилка при додаванні працівника з ID {employee.Id.Value}.");
            throw;
        }
    }

    public async Task RemoveEmployee(Guid employeeId)
    {
        try
        {
            var employee = await employeesQueries.GetById(new EmployeeId(employeeId));
            if (employee == null)
            {
                throw new KeyNotFoundException($"Працівника з ID {employeeId} не знайдено.");
            }

            await employeesRepository.Delete(employee);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Помилка при видаленні працівника з ID {employeeId}.");
            throw;
        }
    }

    public async Task<Transaction> AddTransaction(Transaction transaction)
    {
        try
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
        catch (Exception ex)
        {
            logger.LogError(ex, $"Помилка при додаванні транзакції для працівника з ID {transaction.EmployeeId.Value}.");
            throw;
        }
    }

    public async Task<List<Transaction>> GetTransactions(Guid employeeId)
    {
        try
        {
            var transactions = await transactionsQueries.GetByEmployeeId(new EmployeeId(employeeId));
            if (!transactions.Any())
            {
                throw new KeyNotFoundException($"Для працівника з ID {employeeId} транзакцій не знайдено.");
            }
            logger.LogInfo($"Отримано {transactions.Count} транзакцій для працівника з ID {employeeId}");
            return transactions;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Помилка при отриманні транзакцій для працівника з ID {employeeId}.");
            throw;
        }
    }

    public async Task<decimal> GetTotalPayments(DateTime startDate, DateTime endDate)
    {
        try
        {
            var transactions = await transactionsQueries.GetAll();
            
            logger.LogInfo($"Отримано {transactions.Count} транзакцій.");

            return transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .Sum(t => t.Amount);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Помилка при отриманні загальної суми транзакцій за період з {startDate} по {endDate}.");
            throw;
        }
    }
}
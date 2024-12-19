using ManagementOfFunds.Implementation;

namespace ManagementOfFunds.Abstraction;

public interface IPayrollManager
{
    void AddEmployee(Employee employee);
    void RemoveEmployee(Guid employeeId);
    void AddTransaction(Transaction transaction);
    List<Transaction> GetTransactions(Guid employeeId);
    decimal GetTotalPayments(DateTime startDate, DateTime endDate);
}
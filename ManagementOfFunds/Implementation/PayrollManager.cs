using ManagementOfFunds.Abstraction;

namespace ManagementOfFunds.Implementation;

public class PayrollManager : IPayrollManager
{
    public void AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void RemoveEmployee(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public void AddTransaction(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public List<Transaction> GetTransactions(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public decimal GetTotalPayments(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}
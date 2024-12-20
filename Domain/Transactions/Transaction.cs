using Domain.Employees;

namespace Domain.Transactions;

public class Transaction(
    TransactionId id,
    EmployeeId employeeId,
    decimal amount,
    DateTime date,
    TransactionType type)
{
    public TransactionId Id { get; } = id;
    public Employee? Employee { get; private set; }
    public EmployeeId EmployeeId { get; private set; } = employeeId;
    public decimal Amount { get; private set; } = amount;
    public DateTime Date { get; private set; } = date;
    public TransactionType Type { get; private set; } = type;
    
    public static Transaction New(TransactionId id, EmployeeId employeeId, decimal amount, DateTime date, TransactionType type) 
        => new(id, employeeId, amount, date, type);
}
using ManagementOfFunds.Abstraction;

namespace ManagementOfFunds.Implementation;

public class Transaction : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
}
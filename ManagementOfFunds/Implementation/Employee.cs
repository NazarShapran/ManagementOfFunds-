using ManagementOfFunds.Abstraction;

namespace ManagementOfFunds.Implementation;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
}
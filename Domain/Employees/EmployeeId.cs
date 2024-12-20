namespace Domain.Employees;

public class EmployeeId(Guid Value)
{
    public static EmployeeId New() => new(Guid.NewGuid());
    public static EmployeeId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}
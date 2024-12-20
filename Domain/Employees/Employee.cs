namespace Domain.Employees;

public class Employee(EmployeeId id, string name, string position, decimal salary)
{
    public EmployeeId Id { get; } = id;
    public string Name { get; private set; } = name;
    public string Position { get; private set; } = position;
    public decimal Salary { get; private set; } = salary;
    
    
    public static Employee New(EmployeeId id, string name, string position, decimal salary) 
        => new(id, name, position, salary);
}
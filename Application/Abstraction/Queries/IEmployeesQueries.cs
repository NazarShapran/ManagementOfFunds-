using Domain.Employees;

namespace Application.Abstraction.Queries;

public interface IEmployeesQueries
{
    Task<List<Employee>> GetAll();
    Task<Employee?> GetById(EmployeeId id);
}
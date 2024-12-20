using Domain.Employees;

namespace Application.Abstraction.Repositories;

public interface IEmployeesRepository
{
    Task<Employee> Create(Employee employee);
    Task<Employee> Delete(Employee employee);
}
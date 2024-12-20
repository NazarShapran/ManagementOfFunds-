using Application.Abstraction.Queries;
using Application.Abstraction.Repositories;
using Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeesRepository, IEmployeesQueries
{
    public async Task<Employee> Create(Employee employee)
    {
        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> Delete(Employee employee)
    {
        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<List<Employee>> GetAll()
    {
        return await context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetById(EmployeeId id)
    {
        return await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
    }
}
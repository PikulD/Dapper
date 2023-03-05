using EmployeesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(int id);
        Task<List<Employee>> GetCompany(int id);
        Task<List<Employee>> GetDepartment(int id);
        Task<Employee> Create(Employee employee);
        Task Update(Employee employee);
        Task Delete(int id);
    }
}

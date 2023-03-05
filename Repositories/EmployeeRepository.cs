using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using EmployeesAPI.Models;
using System.Linq;
using System.Collections;

namespace EmployeesAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<Employee> Get(int id)
        {
            var sqlQuery = "SELECT * FROM Employee WHERE( Id = @ID);" +
                           "SELECT * FROM Passport WHERE( EmployeeId = @ID);";

            using (var connection = new SqlConnection(_connectionString))
            using (var multi = await connection.QueryMultipleAsync(sqlQuery, new { id }))
            {
                var employee = await multi.ReadSingleOrDefaultAsync<Employee>();
                if (employee != null)
                    employee.Passports = (await multi.ReadAsync<Passport>()).ToList();
                return employee;
            }
        }
        public async Task<List<Employee>> GetCompany(int id)
        {
            var sqlQuery =  
                "SELECT* FROM Employee e JOIN Passport p ON e.Id = p.EmployeeId "+ 
                "WHERE( e.CompanyId = @CompanyId); ";
                
            using (var connection = new SqlConnection(_connectionString))
            {
                var companyDict = new Dictionary<int, Employee>();
                var companies = await connection.QueryAsync<Employee, Passport, Employee>(
                    sqlQuery, (employee, passport) =>
                    {
                        
                        if (!companyDict.TryGetValue(employee.Id, out var currentEmployee))
                        {
                            currentEmployee = employee;
                            companyDict.Add(currentEmployee.Id, currentEmployee);
                        }
                        currentEmployee.Passports.Add(passport);
                        return currentEmployee;
                    }, new { CompanyId = id }
                );
                return companies.Distinct().ToList();
            }
        }

        public async Task<List<Employee>> GetDepartment(int id)
        {
            var sqlQuery =
                "SELECT* FROM Employee e JOIN Passport p ON e.Id = p.EmployeeId "+
                "WHERE( e.DepartmentId = @DepartmentId); ";

            using (var connection = new SqlConnection(_connectionString))
            {
                var companyDict = new Dictionary<int, Employee>();
                var companies = await connection.QueryAsync<Employee, Passport, Employee>(
                    sqlQuery, (employee, passport) =>
                    {

                        if (!companyDict.TryGetValue(employee.Id, out var currentEmployee))
                        {
                            currentEmployee = employee;
                            companyDict.Add(currentEmployee.Id, currentEmployee);
                        }
                        currentEmployee.Passports.Add(passport);
                        return currentEmployee;
                    }, new { DepartmentId = id }
                );
                return companies.Distinct().ToList();
            }
        }

        public async Task<Employee> Create(Employee employee)
        {
            var sqlQuery = "Insert Into Employee (Name, Surname, Phone, CompanyId, DepartmentId )  VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    employee.Name,
                    employee.Surname,
                    employee.Phone,
                    employee.CompanyId,
                    employee.DepartmentId
                });

                return employee;
            }
        }

        public async Task Update(Employee employee)
        {
            var sqlQuery = "UPDATE Employee SET Name=@Name Surname=@Surname, Phone=@Phone, CompanyId=@CompanyId, DepartmentId=@DepartmentId WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    employee.Name,
                    employee.Surname,
                    employee.Phone,
                    employee.CompanyId,
                    employee.DepartmentId
                });
            }
        }

        public async Task Delete(int id)
        {
            var sqlQuery = "DELETE from Employee WHERE Id=@id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}

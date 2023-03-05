using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<Passport> Passports { get; set; } = new List<Passport>();
    }
    public class Passport
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public int EmployeeId { get; set; }

    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

    }
}

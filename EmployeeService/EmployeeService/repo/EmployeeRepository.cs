using EmployeeService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService.repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpDbContext _db;
        public EmployeeRepository(EmpDbContext db)
        {
            _db = db;
        }

        public Employee AddEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(Employee employeee)
        {
            var employee= _db.Employees.FirstOrDefault(x=>x.Id==employeee.Id);
            _db.SaveChanges();
            return employee;
        }

        public List<Employee> GetAll()
        {
            return _db.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _db.Employees.FirstOrDefault(x=>x.Id==id);
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var existing =_db.Employees.FirstOrDefault(y=>y.Id==employee.Id);
            if (existing != null)
            {
                return existing;
            }
            existing.Name = employee.Name;
            existing.gender = employee.gender;
            existing.salary = employee.salary;
            existing.Age = employee.Age;
            _db.SaveChanges();
            return existing;
        }
    }
}
using EmpWebApp.Database;
using EmpWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmpWebApp.Reositories;

namespace EmpWebApp.Reositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void AddEmployee(Employee employee)
        {
            EmployeeDb.Employees.Add(employee);
        }
        public List<Employee> GetAllEmployees()
        {
            return EmployeeDb.Employees;
        }
        public void DeleteEmployee(int id)
        {
            var employee = EmployeeDb.Employees.First(e => e.Id == id);
            EmployeeDb.Employees.Remove(employee);
        }
        public EmployeeRepository GetEmployeeById(int id)
        {
            return EmployeeDb.Employees.Single(e => e.Id == id);
        }
        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = EmployeeDb.Employees.First(e => e.Id == employee.Id);
            existingEmployee.Name = employee.Name;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.Age = employee.Age;
            existingEmployee.Salary = employee.Salary;
        }
    }
}
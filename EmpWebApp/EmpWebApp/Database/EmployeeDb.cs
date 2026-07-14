using EmpWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpWebApp.Database
{
    public static class EmployeeDb
    {
        public static List<Employee> Employees {  get; set; }
        static EmployeeDb()
        {
            Employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Gender="Male",Age=30, Salary = 50000 },
                new Employee { Id = 2, Name = "Jane Smith", Gender="Female", Age=28, Salary = 55000 },
                new Employee { Id = 3, Name = "Bob Johnson", Gender="Male", Age=35, Salary = 60000 }


            };
        }
    }
}
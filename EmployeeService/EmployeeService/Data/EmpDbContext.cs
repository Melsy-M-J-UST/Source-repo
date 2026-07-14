using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeService.Data
{
    public class EmpDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

    }
}
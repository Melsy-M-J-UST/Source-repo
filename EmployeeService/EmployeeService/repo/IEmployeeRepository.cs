using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.repo
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int id);
        List<Employee> GetAll();
        Employee AddEmployee(Employee employee);
        Employee DeleteEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);


    }
}

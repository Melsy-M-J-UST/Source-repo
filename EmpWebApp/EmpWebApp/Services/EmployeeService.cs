using EmpWebApp.Exceptions;
using EmpWebApp.Models;
using EmpWebApp.Reositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpWebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }
        public Employee GetEmployeeById(int id)
        {
            try
            {
                return _employeeRepository.GetEmployeeById(id);

            }
            catch (Exception ex)
            {
                throw new EmployeeAppException($"Error retrieving employee with id {id}: {ex.Message}");
            }
        }
        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }
        public void DeleteEmployee(int id)
        {
            try
            {
                _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                throw new EmployeeAppException($"Error deleting employee with id {id}: {ex.Message}");
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            try
            {
                _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeAppException($"Error updating employee with id {employee.Id}: {ex.Message}");
            }
        }
    }
}
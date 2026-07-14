using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService.repo
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository repository;
        public EmployeeService(IEmployeeRepository _repository) {
            _repository = repository;
        }

        public List<EmployeeDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
        }
        public EmployeeDto Add(EmployeeDto entity)
        {
            throw new NotImplementedException();
        }
        public EmployeeDto Delete(int id) {
        var employee=_repository.DeleteById(id);

        }
    }
}
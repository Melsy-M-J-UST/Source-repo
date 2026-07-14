using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpWebApp.Exceptions
{
    public class EmployeeAppException : Exception
    {
        public EmployeeAppException() { }
        public EmployeeAppException(string message) : base(message) { }
    }
}
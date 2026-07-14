using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new AppDbContext();
            var employees=context.Employees;
        }
    }
}

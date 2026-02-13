using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IEmployee
    {
        Employee GetEmployeeById(int id);

        List<Employee> GetAllEmployees();

        void AddEmployee(Employee employee);

        bool RemoveEmployee(Employee employee);

    }
}

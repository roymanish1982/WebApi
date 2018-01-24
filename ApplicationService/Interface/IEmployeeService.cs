using ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Interface
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployee();

        Employee GetEmployee(int id);

        int AddEmployee(Employee employee);

        int DeleteEmployee(int id);

        int UpdateEmployee(int id, Employee employee);

        IEnumerable<Employee> GetEmployeeByGender(string gender);
    }
}

using ApplicationService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationService.Models;


namespace ApplicationService.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<Employee> GetEmployee()
        {
            List<Employee> employeeCollection = new List<Employee>();
            //using (DA.PractiseSessionEntities dbEntities = new DA.PractiseSessionEntities())
            //{
            //    List<DA.Employee> emp = dbEntities.Employees.ToList();
            //    emp.ForEach(employee =>
            //    {
            //        employeeCollection.Add(AutoMapper.Mapper.Map<Employee>(emp));
            //    });
               
            //}

            return employeeCollection;
        }

        public Employee GetEmployee(int id)
        {
        //    using (PractiseSessionEntities dbEntities = new PractiseSessionEntities())
        //    {
        //        return dbEntities.Employees.FirstOrDefault(emp=>emp.EmployeeId == id);
        //    }

            throw new  NotImplementedException();
        }
    }
}

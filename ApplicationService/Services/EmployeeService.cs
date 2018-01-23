using ApplicationService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationService.Models;
using DA = WebApiDataAccess;
using AutoMapper;

namespace ApplicationService.Services
{
    public class EmployeeService : IEmployeeService
    {
        IMapper iMapper;
        public EmployeeService()
        {
            iMapper = AutomMapperInstaller.InitializeAutoMapper();
        }

        public IEnumerable<Employee> GetEmployee()
        {
            List<Employee> employeeCollection = new List<Employee>();
            using (DA.PractiseSessionEntities dbEntities = new DA.PractiseSessionEntities())
            {
                List<DA.Employee> emp = dbEntities.Employees.ToList();
                emp.ForEach(employee =>
                {  
                    employeeCollection.Add(iMapper.Map<Employee>(employee));
                });

            }

            return employeeCollection;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee = null;
            using (DA.PractiseSessionEntities dbEntities = new DA.PractiseSessionEntities())
            {
                var dbEmp = dbEntities.Employees.FirstOrDefault(emp => emp.EmployeeId == id);
                if(dbEmp!=null)
                {
                    employee = iMapper.Map<Employee>(dbEmp);
                }
            }

            return employee;
        }
    }
}

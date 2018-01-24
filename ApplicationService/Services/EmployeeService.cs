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
                if (dbEmp != null)
                {
                    employee = iMapper.Map<Employee>(dbEmp);
                }
            }

            return employee;
        }

        public int AddEmployee(Employee employee)
        {
            DA.Employee emp = iMapper.Map<DA.Employee>(employee);
            using (DA.PractiseSessionEntities dbEntities = new DA.PractiseSessionEntities())
            {
                dbEntities.Employees.Add(emp);
                dbEntities.SaveChanges();
            }

            return emp.EmployeeId;
        }

        public int DeleteEmployee(int id)
        {
            using (DA.PractiseSessionEntities dbEntities = new DA.PractiseSessionEntities())
            {
                var emp = dbEntities.Employees.FirstOrDefault(x => x.EmployeeId == id);
                if (emp != null)
                {
                    dbEntities.Employees.Remove(emp);
                    dbEntities.SaveChanges();
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }

        public int UpdateEmployee(int id, Employee employee)
        {
            using (DA.PractiseSessionEntities dbEntities = new DA.PractiseSessionEntities())
            {
                DA.Employee emp = dbEntities.Employees.FirstOrDefault(x => x.EmployeeId == id);

                if (emp != null)
                {
                    emp.FirstName = employee.FirstName;
                    emp.LastName = employee.LastName;
                    emp.Salary = employee.Salary;
                    emp.Gender = emp.Gender;
                    dbEntities.SaveChanges();
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }

        public IEnumerable<Employee> GetEmployeeByGender(string gender)
        {
            switch (gender.ToLower())
            {
                case "all":
                    return  GetEmployee();
                case "male":
                    return GetEmployee().Where(x=>x.Gender.ToLower() == "male");
                case "female":
                    return GetEmployee().Where(x => x.Gender.ToLower() == "female");
                default:
                    return new List<Employee>();
            }
        }
    }
}

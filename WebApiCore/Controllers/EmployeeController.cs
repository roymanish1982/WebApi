using ApplicationService.Interface;
using ApplicationService.Models;
using ApplicationService.Services;
using Microsoft.Web.Http;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiCore.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}")]
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

        [Route("Employee")]
        public IHttpActionResult Get()
        {
          return Ok(_employeeService.GetEmployee());
        }

        [Route("Employee/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Employee emp = _employeeService.GetEmployee(id);
            if(emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
    }
}
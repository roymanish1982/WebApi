using ApplicationService.Interface;
using ApplicationService.Services;
using Microsoft.Web.Http;
using System.Web.Http;

namespace WebApiCore.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Employee")]
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }


        public IHttpActionResult Get()
        {
            return Ok(_employeeService.GetEmployee());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_employeeService.GetEmployee(id));
        }
    }
}
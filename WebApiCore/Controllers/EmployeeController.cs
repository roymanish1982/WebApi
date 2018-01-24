using ApplicationService.Interface;
using ApplicationService.Models;
using ApplicationService.Services;
using Microsoft.Web.Http;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

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
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [Route("Employee")]
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Invalid Input. employee is null");
            }

            try
            {
                int empId = _employeeService.AddEmployee(employee);

                if (empId != 0)
                {
                    employee.EmployeeId = empId;
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new System.Uri($"{Request.RequestUri}/{ empId.ToString()}");
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Internal Server Exception");
                }
            }
            catch (System.Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Internal Server Exception", exception);
            }
        }

        [Route("Employee/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                int status = _employeeService.DeleteEmployee(id);
                if (status == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee id {id} Not Found for delete");
                }
            }
            catch (System.Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Internal Server Exception", exception);
            }
        }

        [Route("Employee/{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                int status = _employeeService.UpdateEmployee(id, employee);
                if (status == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee id {id} Not Found for update");
                }
            }
            catch (System.Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Internal Server Exception", exception);
            }
        }

        [Route("Employee/Gender/{gender}")]
        public HttpResponseMessage GetByGender(string gender)
        {
            try
            {
                IEnumerable<Employee> employee = _employeeService.GetEmployeeByGender(gender);
                if (employee.ToList().Count()==0)
                {
                    var message =  Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            $"Value for gender must be All, Male or Female. {gender} is invalid " +
                            $"Or Employee list is empty . Please verify by evaluting the URL from location section using Http 'GET'");
                    message.Headers.Location = new System.Uri($"{Request.RequestUri}");

                    return message;
                }

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (System.Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Internal Server Exception", exception);
            }
        }
    }
}
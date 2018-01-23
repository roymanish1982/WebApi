using Microsoft.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCore.Helper;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/ResponseCode")]
    public class HttpResponseCodeController : ApiController
    {
        private List<Product> products = new List<Product>();

        [NonAction]
        private List<Product> All()
        {
            Product[] p = new Product[]
            {
                new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
                new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
                new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
            };

            products.AddRange(p.ToList());

            return products;
        }

        [Route("All")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage message =  Request.CreateResponse(HttpStatusCode.OK, All());
            return message;
        }

        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var product = All().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        [Route("AllByActionResult")]
        public IHttpActionResult GetByActionResult()
        {
            return Ok(All());
        }

        [Route("GetByActionResult/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var product = All().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("CustomActionResult")]
        [HttpGet]
        public IHttpActionResult JsonResultFrom()
        {
            return new JsonReasult<Product>(All(), Request);
        }
    }
}
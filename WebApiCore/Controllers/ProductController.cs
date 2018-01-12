using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCore.Models;

/*

WebApi Attribute Routing Using Version

Steps : 
    
    1. Add Neuget Package Microsoft.AspNet.WebApi.Versioning
    2. Add below Code in WebApiConfig

    //// Web API configuration and services
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                 {
                         ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                 }
            };
            config.MapHttpAttributeRoutes(constraintResolver);
            config.AddApiVersioning();
*/

namespace WebApiCore.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/product")]
    public class ProductController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }


        [Route("api/v{version:apiVersion}/product/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("api/v{version:apiVersion}/product/{id}/category")]
        public IHttpActionResult GetCategoryListByProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.Category);
        }


        [Route("api/v{version:apiVersion}/product/{id}/category/{category}")]
        public IHttpActionResult GetProductByCategory(int id, string category)
        {
            var product = products.FirstOrDefault((p) => p.Id == id && p.Category == category);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [MapToApiVersion("2.0")]
        [Route("api/v{version:apiVersion}/product/{id}/category/{category}")]
        public IHttpActionResult GetProductByCategoryV2(int id, string category)
        {
            var product = products.FirstOrDefault((p) => p.Id == id && p.Category == category);
            if (product == null)
            {
                return NotFound();
            }
            return Ok($"CategoryName : {product.Category} With Api Version 2 ");
        }
    }
}

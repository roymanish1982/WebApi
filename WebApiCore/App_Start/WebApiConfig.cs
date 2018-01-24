using Microsoft.Web.Http.Routing;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using WebApiCore.Helper;

namespace WebApiCore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                    {
                        ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                    }
            };

            config.MapHttpAttributeRoutes(constraintResolver);
            config.AddApiVersioning();

            // Web API routes
            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            /// To remove any Media Type Formatter form web APi.
            /// In this case we are removeing the xmlFormater from API.
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            /*
             * How to return JSON instead of XML from ASP.Net Web API
             * service when a request is made from the browser
             * Approach 1:
             *
             *  config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
             *
             *In this approach if the request is made from the browser we will always get the JSON as out put but the Response ContentType will 
             * still be text/html. Coz from brower the Request Accept is
             * Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*; q = 0.8
             * Approach II will fix this issue.
             * 
             * Approach II
             * Create a CustomFormatter like CustomJsonFormatter this will fix the 
             * above issue.
             */

           /// config.Formatters.Add(new CustomJsonFormatter());
             ////config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            ///To Set the Json formatting for proper indention
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //// To use camel case for jsoan output.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}

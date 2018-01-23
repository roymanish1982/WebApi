using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiCore.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/TutorialSample")]
    public class TutorialSampleController : ApiController
    {

        static List<string> stringCollection = new List<string>()
        {
            "value1", "value2","value3"
        };
        // GET: api/TutorialSample
        public IEnumerable<string> Get()
        {
            return stringCollection;
        }

        // GET: api/TutorialSample/5
        public string Get(int id)
        {
            return stringCollection[id];
        }

        // POST: api/TutorialSample

        public void Post([FromBody]string value)
        {
            stringCollection.Add(value);
        }

        [Route("api/v{version:apiVersion}/TutorialSample/{id:int}")]
        // PUT: api/TutorialSample/5
        public void Put(int id, [FromBody]string value)
        {
            stringCollection[id] = value;
        }

        [Route("api/v{version:apiVersion}/TutorialSample/{id:int}")]
        // DELETE: api/TutorialSample/5
        public void Delete(int id)
        {
            stringCollection.RemoveAt(id);
        }
    }
}

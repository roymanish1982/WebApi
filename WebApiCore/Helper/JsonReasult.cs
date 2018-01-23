using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApiCore.Helper
{
    public class JsonReasult<T> : IHttpActionResult
    {

        T _value;
       IEnumerable<T> _ValueCollection;

        HttpRequestMessage _request;
        public JsonReasult(T value, HttpRequestMessage request)
        {
            _value = value;
            _request = request;
        }
        public JsonReasult(IEnumerable<T> value, HttpRequestMessage request)
        {
            _ValueCollection = value;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            dynamic data = _value;
            if(_ValueCollection !=null)
            {
                data = _ValueCollection;
            }
            data = JsonConvert.SerializeObject(data);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(data),
                RequestMessage = _request
            };

            return Task.FromResult(response);
        }
    }
}
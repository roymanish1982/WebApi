using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApiCore.Resources;
namespace WebApiCore.Helper
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Initialization.   
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            string userName = null;
            string password = null;

            if (request.Headers.TryGetValues(Constants.API_KEY_HEADER, out IEnumerable<string> apiKeyHeaderValues)
                && !string.IsNullOrEmpty(authorization.Parameter))
            {

                var apiKeyHeaderValue = apiKeyHeaderValues.First();
                // Get the auth token   
                string authToken = authorization.Parameter;
                // Decode the token from BASE64   
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                // Extract username and password from decoded token   
                userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
                // Verification.   
                if (apiKeyHeaderValue.Equals(Constants.API_KEY_VALUE)
                    && userName.Equals(Constants.USERNAME_VALUE)
                    && password.Equals(Constants.PASSWORD_VALUE))
                {
                    // Setting   
                    var identity = new GenericIdentity(userName);
                    SetPrincipal(new GenericPrincipal(identity, null));
                }               
            }

            return base.SendAsync(request, cancellationToken);

        }

        /// <summary>   
        /// Set principal method.   
        /// </summary>   
        /// <param name="principal">Principal parameter</param>   
        private static void SetPrincipal(IPrincipal principal)
        {
            // setting.   
            Thread.CurrentPrincipal = principal;
            // Verification.   
            if (HttpContext.Current != null)
            {
                // Setting.   
                HttpContext.Current.User = principal;
            }
        }
    }
}
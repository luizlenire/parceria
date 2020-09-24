using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace ParceriaAPI.SeveralFunctions
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class CorsMiddleware
    {
        #region --> Private properties. <--

        private readonly RequestDelegate restDelegate;

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public CorsMiddleware(RequestDelegate _restDelegate) => restDelegate = _restDelegate;

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Accept-Encoding, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");

            if (httpContext.Request.Method == "OPTIONS")
            {
                httpContext.Response.StatusCode = HttpStatusCode.OK.GetHashCode();
                await httpContext.Response.WriteAsync(string.Empty);
            }

            await restDelegate(httpContext);
        }

        #endregion --> Public methods. <--
    }
}

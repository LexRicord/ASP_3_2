using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace aspNetCore_lab1a
{
    public class task1_middleware
    {
        private readonly RequestDelegate _next;

        public task1_middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            if (string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                    StringValues parmA;
                req.Query.TryGetValue("parmA", out parmA);

                StringValues parmB;
                req.Query.TryGetValue("parmB", out parmB);

                res.Headers.Add("Content-Type", "text/plain");
                await res.WriteAsync($"===== GET_HTTP_HAE =====\n" +
                                     $"parmA = {(parmA.Count > 0 ? parmA[0] : "null")}\n" +
                                     $"parmB = {(parmB.Count > 0 ? parmB[0] : "null")}");   
            }
            else
            {
                context.Response.StatusCode = 405;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<h2>Only GET method allowed.</h2>");
            }
        }
    }
}
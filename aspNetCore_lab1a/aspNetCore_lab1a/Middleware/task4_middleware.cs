using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace aspNetCore_lab1a
{
    public class task4_middleware
    {
        private readonly RequestDelegate _next;

        public task4_middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;

            if (req.Method == HttpMethods.Post)
            {
                try
                {
                    var x = int.Parse(req.Form["x"]);
                    var y = int.Parse(req.Form["y"]);
                    var sum = x + y;
                    res.ContentType = "text/plain";
                    await res.WriteAsync(sum.ToString());
                }
                catch
                {
                    res.StatusCode = 400;
                    res.ContentType = "text/html";
                    await res.WriteAsync("Enter correct numbers!");
                }
            }
            else
            {
                res.StatusCode = 405;
                res.ContentType = "text/html";
                await res.WriteAsync("<h2>Only POST method allowed.</h2>");
            }
        }
    }
}

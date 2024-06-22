using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace aspNetCore_lab1a
{
    public class task3_middleware
    {
        private readonly RequestDelegate _next;

        public task3_middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Request Path 3_2: {context.Request.Path}");
            if (context.Request.Method == "PUT")
            {
                if (context.Request.Path.StartsWithSegments("/task3") && context.Request.Path.Value.EndsWith(".hae"))
                {
                    var parmA = context.Request.Query["parmA"];
                    var parmB = context.Request.Query["parmB"];

                    if (!string.IsNullOrEmpty(parmA) && !string.IsNullOrEmpty(parmB))
                    {
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync($"===== PUT-HTTP-HAE =====\n" +
                                                         $"parmA = {parmA}\n" +
                                                         $"parmB = {parmB}\n");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync("Parameters parmA and parmB are required in the URL.");
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("<h2>Resource not found.</h2>");
                }
            }
            else
            {
                context.Response.StatusCode = 405;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<h2>Only PUT method allowed.</h2>");
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace aspNetCore_lab1a
{
    public class task2_middleware
    {
        private readonly RequestDelegate _next;

        public task2_middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;
            Console.WriteLine($"Request Path 2: {path}");
            if (string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase))
            {
                var parmA = context.Request.Query["parmA"];
                var parmB = context.Request.Query["parmB"];

                var requestBody = await ReadRequestBodyAsync(context.Request.Body);

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"===== POST-HTTP-HAE =====\n" +
                                                 $"parmA = {(parmA.Count > 0 ? parmA[0] : "null")}\n" +
                                                 $"parmB = {(parmB.Count > 0 ? parmB[0] : "null")}\n" +
                                                 $"Request Body:\n{requestBody}");
            }
            else
            {
                context.Response.StatusCode = 405;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<h2>Only POST method allowed.</h2>");
            }
        }

        private async Task<string> ReadRequestBodyAsync(Stream body)
        {
            using (var reader = new StreamReader(body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}

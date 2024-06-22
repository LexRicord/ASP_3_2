using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace aspNetCore_lab1a
{
    public class task6_middleware
    {
        private readonly RequestDelegate _next;
        private readonly ITempDataProvider _tempDataProvider;

        public task6_middleware(RequestDelegate next, ITempDataProvider tempDataProvider)
        {
            _next = next;
            _tempDataProvider = tempDataProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            res.ContentType = "text/html";

            if (req.Method == HttpMethods.Get)
            {
                await HandleGetAsync(context);
            }
            else if (req.Method == HttpMethods.Post)
            {
                await HandlePostAsync(context);
            }
            else
            {
                res.StatusCode = 405;
                await res.WriteAsync("<h2>Only GET/POST methods allowed.</h2>");
            }
        }

        private async Task HandleGetAsync(HttpContext context)
        {
            try
            {
                var htmlContent = "<form method=\"post\">" +
                                  "   <label for=\"x\">X:</label>" +
                                  "   <input type=\"text\" name=\"x\" id=\"x\" /><br/>" +
                                  "   <label for=\"y\">Y:</label>" +
                                  "   <input type=\"text\" name=\"y\" id=\"y\" /><br/>" +
                                  "   <input type=\"submit\" value=\"Multiply Numbers\" />" +
                                  "</form>";

                await context.Response.WriteAsync(htmlContent);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }

        private async Task HandlePostAsync(HttpContext context)
        {
            try
            {
                var x = int.Parse(context.Request.Form["x"]);
                var y = int.Parse(context.Request.Form["y"]);
                var mul = x * y;

                var result = $"{x} * {y} = {mul}";

                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(result);
            }
            catch
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("<h2>X and Y parameters are not provided or are invalid.</h2>");
            }
        }
    }
}

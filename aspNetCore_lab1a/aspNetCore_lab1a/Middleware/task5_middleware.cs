using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace aspNetCore_lab1a
{
    public class task5_middleware
    {
        private readonly RequestDelegate _next;

        public task5_middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;

                if (req.Method == HttpMethods.Post)
                {
                    await HandlePostRequestAsync(context);
                }
                else if (req.Method == HttpMethods.Get)
                {
                    await HandleGetRequestAsync(context);
                }
                else
                {
                    res.StatusCode = 405;
                    res.ContentType = "text/html";
                    await res.WriteAsync("<h2>Only POST and GET methods allowed.</h2>");
                }
        }

        private async Task HandlePostRequestAsync(HttpContext context)
        {
            HttpResponse res = context.Response;
            try
            {
                var x = int.Parse(context.Request.Form["x"]);
                var y = int.Parse(context.Request.Form["y"]);
                var sum = x * y;
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

        private async Task HandleGetRequestAsync(HttpContext context)
        {
            HttpResponse res = context.Response;
            var html = @"<!DOCTYPE html>
                        <html>
                        <head>
                            <title>Calculator</title>
                        </head>
                        <body>
                            <h2>Calculator</h2>
                            <form id='calcForm'>
                                <label for='x'>Enter x:</label>
                                <input type='number' id='x' name='x'><br><br>
                                <label for='y'>Enter y:</label>
                                <input type='number' id='y' name='y'><br><br>
                                <button type='button' onclick='calculate()'>Calculate</button>
                            </form>
                            <div id='result'></div>
                            <script>
                                function calculate() {
                                    var xhr = new XMLHttpRequest();
                                    xhr.open('POST', '/task5', true);
                                    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
                                    xhr.onreadystatechange = function () {
                                        if (xhr.readyState === XMLHttpRequest.DONE) {
                                            if (xhr.status === 200) {
                                                document.getElementById('result').innerHTML = 'Result: ' + xhr.responseText;
                                            } else {
                                                document.getElementById('result').innerHTML = 'Error occurred.';
                                            }
                                        }
                                    };
                                    var formData = new FormData(document.getElementById('calcForm'));
                                    xhr.send(new URLSearchParams(formData).toString());
                                }
                            </script>
                        </body>
                        </html>";

            res.ContentType = "text/html";
            await res.WriteAsync(html);
        }
    }
}

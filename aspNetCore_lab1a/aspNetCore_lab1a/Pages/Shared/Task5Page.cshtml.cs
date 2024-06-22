using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace aspNetCore_lab1a.Pages.Shared
{
    public class Task5Model : PageModel
    {
        [BindProperty]
        public int X { get; set; }
        [BindProperty]
        public int Y { get; set; }
        [BindProperty]
        public string Result { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostMultiplyNumbersAsync()
        {
            try
            {
                var x = X;
                var y = Y;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44319/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsJsonAsync("task5", new CalculationModel { X = x, Y = y });
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<CalculationResult>();
                        Result = result?.Result.ToString();
                    }
                    else
                    {
                        Result = "Error";
                    }
                }

                return Page();
            }
            catch
            {
                Response.StatusCode = 400;
                return Content("<h2>X and Y parameters are not provided.</h2>", "text/html");
            }
        }

    }

    public class CalculationModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class CalculationResult
    {
        public int Result { get; set; }
    }
}

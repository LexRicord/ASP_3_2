using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspNetCore_lab1a.Pages
{
    public class Task6Model : PageModel
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

        public IActionResult OnPostMultiplyNumbers()
        {
            try
            {
                int result = X * Y;
                Result = $"{X} * {Y} = {result}";

                return Page();
            }
            catch
            {
                Response.StatusCode = 400;
                return Content("<h2>X and Y parameters are not provided.</h2>", "text/html");
            }
        }
    }
}

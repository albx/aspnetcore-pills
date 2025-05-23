using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCorePills.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(Input.Name))
            {
                ModelState.AddModelError(nameof(Input.Name), "Name is required.");
            }

            if (!ModelState.IsValid)
            {
                return;
            }
            // Process the input (e.g., save to database, etc.)
            // Redirect or display a success message
        }

        public class InputModel
        {
            public string Name { get; set; } = string.Empty;
        }
    }
}

using AspNetCorePills.Todo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCorePills.Web.RazorPages.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly TodoService _service;

    [BindProperty]
    public TodoItem[] Items { get; set; } = [];

    [BindProperty]
    public TodoItem Todo { get; set; } = new();

    public IndexModel(ILogger<IndexModel> logger, TodoService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task OnGetAsync()
    {
        _logger.LogInformation("OnGet called");
        Items = (await _service.GetItems()).ToArray();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            await _service.AddItem(Todo);
            Todo = new();
        }

        Items = (await _service.GetItems()).ToArray();
    }
}

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

    public void OnGet()
    {
        _logger.LogInformation("OnGet called");
        Items = _service.GetItems().ToArray();
    }

    public void OnPost()
    {
        if (ModelState.IsValid)
        {
            _service.AddItem(Todo);
            Todo = new();
        }

        Items = _service.GetItems().ToArray();
    }
}

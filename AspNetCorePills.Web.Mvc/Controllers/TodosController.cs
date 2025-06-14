using AspNetCorePills.Todo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePills.Web.Mvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    public TodosController(TodoService service)
    {
        Service = service;
    }

    public TodoService Service { get; }

    [HttpGet]
    public IActionResult Get()
    {
        var items = Service.GetItems();
        return Ok(items);
    }

    [HttpPost]
    public IActionResult Post([FromBody] TodoItem item)
    {
        if (item == null)
        {
            return BadRequest("Todo item cannot be null.");
        }

        Service.AddItem(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }
}

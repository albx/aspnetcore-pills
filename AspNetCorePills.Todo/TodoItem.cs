using System.ComponentModel.DataAnnotations;

namespace AspNetCorePills.Todo;

public class TodoItem
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
}

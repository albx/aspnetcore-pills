using System.ComponentModel.DataAnnotations;

namespace AspNetCorePills.Web.Mvc.Models.Home;

public class IndexViewModel
{
    [Required]
    public string Title { get; set; } = string.Empty;
}

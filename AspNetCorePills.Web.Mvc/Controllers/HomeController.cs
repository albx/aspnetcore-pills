using AspNetCorePills.Web.Mvc.Models;
using AspNetCorePills.Web.Mvc.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCorePills.Web.Mvc.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new IndexViewModel
        {
            Title = "Welcome to AspNetCorePills!"
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Index(IndexViewModel model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("Form submitted successfully with Title: {Title}", model.Title);
        }
        else
        {
            _logger.LogWarning("Form submission failed. Model state is invalid.");
        }

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

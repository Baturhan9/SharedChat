using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SharedChatWebSite.Models;
using SharedChatWebSite.Services;

namespace SharedChatWebSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() => View();
         
    [HttpPost] 
    public IActionResult Index(string userName)
    {
        string userId = IdService.GetId(userName);
        HttpContext.Response.Cookies.Append("userId", userId.ToString());
        return RedirectToAction("Chat");
    }
    public IActionResult Chat()
    {
        ViewData["Hello"] = HttpContext.Request.Cookies["userId"];
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Exit()
    {
        HttpContext.Response.Cookies.Delete("userId");
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

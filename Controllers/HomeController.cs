using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SharedChatWebSite.Models;
using SharedChatWebSite.Repository;
using SharedChatWebSite.Services;

namespace SharedChatWebSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMessageRepository _repository;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _repository = new MessageRepository();
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
        var messages = _repository.GetAll();
        return View(messages);
    }
    [HttpPost]
    public IActionResult Chat(string userText)
    {
        if(string.IsNullOrEmpty(userText))
        {
            ViewBag.Empty = "Message must be not empty";
            return RedirectToAction("Chat");
        }
        _repository.Create(userText, HttpContext.Request.Cookies["userId"]);
        return RedirectToAction("Chat");
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

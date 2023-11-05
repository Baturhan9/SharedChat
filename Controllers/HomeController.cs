using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SharedChatWebSite.Models;
using SharedChatWebSite.Repository;
using SharedChatWebSite.Services;
using SharedChatWebSite.StorageMessages;

namespace SharedChatWebSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMessageRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly MessageService _service;
    public HomeController(ILogger<HomeController> logger, IMemoryCache cache)
    {
        _logger = logger;
        _repository = new MessageRepository();
        _cache = cache;
        _service = new MessageService(_cache); 
    }

    public IActionResult Index() => View();
         
    [HttpPost] 
    public IActionResult Index(string userName)
    {
        string userId = IdService.GetId(userName);
        HttpContext.Response.Cookies.Append("userId", userId.ToString());
        Config.Messages = _service.GetAllMessagesFromCache();
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
        _service.SetMessagesInServer();
        return RedirectToAction("Chat");
    }

    public IActionResult YourMessage()
    {
        var userId = HttpContext.Request.Cookies["userId"];
        var messages = _repository.GetUsersAll(userId);
        return View(messages);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Exit()
    {
        HttpContext.Response.Cookies.Delete("userId");
        _cache.Set("message", Config.Messages);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

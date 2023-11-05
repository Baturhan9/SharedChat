
using Microsoft.Extensions.Caching.Memory;
using SharedChatWebSite.Services;
using SharedChatWebSite.StorageMessages;

namespace SharedChatWebSite.Middlewares;

public class CacheMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly MessageService _service;
    public CacheMiddleware(RequestDelegate next, IMemoryCache cache)
    {
       _next = next; 
       _service = new MessageService(cache);
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        if(context.Request.Path == "/")
        {
            Config.Messages = _service.GetAllMessagesFromCache();
        }

        await this._next(context);
    }
}
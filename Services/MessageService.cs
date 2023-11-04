using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using SharedChatWebSite.Models;
using SharedChatWebSite.StorageMessages;

namespace SharedChatWebSite.Services;

public class MessageService
{
    private readonly IMemoryCache _cache;
    public MessageService(IMemoryCache cache)
    {
       _cache = cache; 
    }

    public List<Message> GetAllMessagesFromCache()
    {

        
        _cache.TryGetValue("message", out List<Message>? messages);
        if(messages == null)
        {
            using(FileStream fs = new FileStream("messages.json",FileMode.OpenOrCreate))
            {
                if(new FileInfo("messages.json").Length != 0)
                {
                    List<Message>? listMessages = JsonSerializer.Deserialize<List<Message>>(fs);
                    messages = listMessages;
                }
            }
        }
        return messages ?? new List<Message>();
    }

    public void SetMessagesInServer()
    {
        using(FileStream fs = new FileStream("messages.json", FileMode.Truncate))
        {
            JsonSerializer.Serialize<List<Message>>(fs,Config.Messages);
        }
    }
}
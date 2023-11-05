using SharedChatWebSite.Models;
using SharedChatWebSite.StorageMessages;

namespace SharedChatWebSite.Repository;

public class MessageRepository : IMessageRepository
{
    public void Create(string text, string userId)
    {
        var message = new Message()
        {
            DateCreate = DateTime.Now,
            UserId = userId,
            Text = text 
        };
        DeleteFirst();
        Config.Messages.Add(message);
    }

    public List<Message> GetAll()
    {
        return Config.Messages.OrderByDescending(x => x.DateCreate).Take(20).ToList();
    }

    public List<Message> GetUsersAll(string userId)
    {
        return Config.Messages.Where(x => x.UserId == userId)
                              .OrderByDescending(x=> x.DateCreate)
                              .Take(10)
                              .ToList();
    }

    private void DeleteFirst()
    {
        if(Config.Messages.Count > 100)
        {
            Config.Messages.RemoveAt(0);
        }
    }
}